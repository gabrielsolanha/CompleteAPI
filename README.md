  CompleteAPI - README :root { --bg: #0b1020; --card: #121a2b; --text: #e6eefc; --muted: #a4b1c9; --accent: #6aa8ff; --code: #0e1a33; --border: #223151; } html, body { margin: 0; padding: 0; background: var(--bg); color: var(--text); font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Oxygen, Ubuntu, Cantarell, "Fira Sans", "Droid Sans", "Helvetica Neue", Arial, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", sans-serif; line-height: 1.6; } .container { max-width: 980px; margin: 48px auto; padding: 0 20px; } .card { background: linear-gradient(180deg, rgba(255,255,255,0.02), rgba(255,255,255,0.0)); border: 1px solid var(--border); border-radius: 16px; padding: 28px; box-shadow: 0 10px 30px rgba(0,0,0,0.25); } h1, h2, h3 { line-height: 1.25; margin-top: 1.2em; margin-bottom: 0.6em; } h1 { font-size: 2rem; } h2 { font-size: 1.5rem; color: var(--accent); } h3 { font-size: 1.25rem; } p { color: var(--text); margin: 0.5em 0 1em; } code, pre, kbd, samp { font-family: ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, "Liberation Mono", "Courier New", monospace; } pre { background: var(--code); padding: 14px 16px; border-radius: 12px; overflow: auto; border: 1px solid var(--border); } code { background: rgba(255,255,255,0.06); padding: 2px 6px; border-radius: 6px; border: 1px solid var(--border); } ul, ol { padding-left: 1.2rem; } .muted { color: var(--muted); } .hr { height: 1px; background: linear-gradient(to right, transparent, var(--border), transparent); border: 0; margin: 28px 0; } .pill { display: inline-block; padding: 4px 10px; border: 1px solid var(--border); border-radius: 999px; color: var(--muted); font-size: 0.85rem; } a { color: var(--accent); text-decoration: none; } a:hover { text-decoration: underline; }

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
    
    A API ficará disponível em `http://localhost:8080`.

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