FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia todo o código
COPY . .


# Remove appsettings.Development.json e todos os bin/obj do projeto
RUN find . -type d \( -name bin -o -name obj \) -exec rm -rf {} + \
    && rm -f src/Completeapi.CsharpModel.WebApi/appsettings.Development.json

# Instala dotnet-ef globalmente e adiciona ao PATH
RUN apt-get update && apt-get install -y postgresql-client && rm -rf /var/lib/apt/lists/*
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Rodar testes unitários
RUN dotnet test tests/Completeapi.CsharpModel.Unit --no-build || (echo "❌ Testes falharam" && exit 1)

# Define entrypoint
ENTRYPOINT ["/bin/bash", "/app/entrypoint.sh"]
