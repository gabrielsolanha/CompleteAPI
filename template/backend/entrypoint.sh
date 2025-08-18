#!/bin/bash
set -e

echo "🏁 Iniciando pipeline..."

# Testes unitários simples antes (já valida que tudo compila)
echo "🧪 Executando testes unitários..."
dotnet test tests/Completeapi.CsharpModel.Unit --no-build || { echo "❌ Testes falharam"; exit 1; }

# Rodar análise de cobertura
echo "📊 Gerando relatório de cobertura..."
./coverage-report.sh || { echo "❌ Falha ao gerar relatório de cobertura"; exit 1; }

# Validar cobertura mínima (10%)
COVERAGE_FILE="./tests/Completeapi.CsharpModel.Unit/TestResults/coverage.cobertura.xml"
if [ ! -f "$COVERAGE_FILE" ]; then
  echo "❌ Arquivo de cobertura não encontrado!"
  exit 1
fi

LINE_RATE=$(xmllint --xpath "string(//coverage/@line-rate)" "$COVERAGE_FILE")
LINE_RATE_PERCENT=$(echo "$LINE_RATE * 100" | bc)

echo "Cobertura atual: $LINE_RATE_PERCENT%"

# Verifica se é menor que MIN_COVERAGE
MIN_COVERAGE=10
if (( $(echo "$LINE_RATE_PERCENT < $MIN_COVERAGE" | bc -l) )); then
  echo "❌ Cobertura insuficiente ($LINE_RATE_PERCENT%). Mínimo exigido: ${MIN_COVERAGE}%"
  exit 1
else
  echo "✅ Cobertura aceitável ($LINE_RATE_PERCENT%)"
fi

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

# Povoar tabelas com dump.sql
echo "📥 Aplicando dump.sql..."
psql "host=db port=5432 user=completeuser dbname=completeapidb password=completepass" -f ../../postigresbkp/dump.sql
echo "✅ Dump aplicado!"

# Rodar API
echo "🚀 Iniciando API..."
dotnet run --urls "http://0.0.0.0:8080"
