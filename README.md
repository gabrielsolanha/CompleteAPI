
CompleteAPI
===========

Este projeto é uma API completa desenvolvida em **.NET** com **PostgreSQL** e suporte a **pipelines Docker**.

* * *

🚀 Rodando com Pipeline (Docker)
--------------------------------

Para subir o ambiente completo (API + PostgreSQL + pipeline):

    docker compose up --build

Isso irá:

*   Subir um container PostgreSQL com as credenciais definidas no `docker-compose.yml`
*   Aplicar as migrações automaticamente
*   Inicializar a API em `http://localhost:8080`

* * *

💻 Rodando Localmente (Desenvolvimento/Depuração)
-------------------------------------------------

Se preferir rodar **fora do Docker**, siga os passos abaixo:

1.  **Suba um PostgreSQL local** (pode usar Docker ou uma instância instalada na sua máquina).  
    Ajuste o `src/Completeapi.CsharpModel.WebApi/appsettings.Development.json` para apontar para o banco correto.
2.  Entre na pasta do projeto da API:
    
        cd src/Completeapi.CsharpModel.WebApi
    
3.  **Rodar migrações:**
    
        dotnet ef database update
    
4.  **Buildar a aplicação:**
    
        dotnet build
    
5.  **Rodar em modo depuração:**
    
        dotnet run
    

* * *

📦 Estrutura
------------

*   `src/Completeapi.CsharpModel.WebApi` → Projeto principal da API
*   `tests/Completeapi.CsharpModel.Unit` → Testes unitários
*   `postigresbkp/dump.sql` → Backup do banco para inicialização em containers

* * *

✅ Observações
-------------

*   Quando rodar via **pipeline/Docker**, não é necessário rodar as migrações manualmente.
*   Quando rodar **localmente**, é necessário rodar `dotnet ef database update` antes de executar a API.
*   O arquivo `appsettings.Development.json` deve conter as credenciais corretas do PostgreSQL em execução.

Usuario adiministrador pré para testes inserido:
```
                {  
                "username": "gs7",
                "password": "Gs7@12345678",
                "email": "teste@mail.com"
                }
```
