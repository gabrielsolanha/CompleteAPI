#!/bin/bash
set -e

echo "🏁 Iniciando pipeline..."

# Testes
echo "🧪 Executando testes unitários..."
dotnet test tests/Completeapi.CsharpModel.Unit --no-build || { echo "❌ Testes falharam"; exit 1; }

# Variável de ambiente
if [ -n "$COMPLETEAPIENV" ]; then
  echo "⚙️ Sobrescrevendo appsettings.json com valor de COMPLETEAPIENV..."
  echo "$COMPLETEAPIENV" > src/Completeapi.CsharpModel.WebApi/appsettings.json
else
  echo "⚠️ Variável COMPLETEAPIENV não definida, usando appsettings.json padrão."
fi

# Esperar Postgres
echo "⏳ Aguardando PostgreSQL..."
until pg_isready -h db -p 5432 -U completeuser; do
  sleep 2
done
echo "✅ PostgreSQL pronto!"

# Rodar migrações
echo "📦 Executando migrações..."
cd src/Completeapi.CsharpModel.WebApi
dotnet ef database update || { echo "❌ Migrações falharam"; exit 1; }

# Gambiarra: povoar tabelas com dump.sql
echo "📥 Aplicando dump.sql..."
psql "host=db port=5432 user=completeuser dbname=completeapidb password=completepass" -f ../../postigresbkp/dump.sql
echo "✅ Dump aplicado!"

# Rodar API
echo "🚀 Iniciando API..."
dotnet run --urls "http://0.0.0.0:8080"
