# AIPolicy API

Backend desenvolvido em ASP.NET Core Web API (.NET 8).

**Autor:** Wladson Cedraz

## Instalação e Execução

### Pré-requisitos

- Docker instalado
- Conexão com o container do banco de dados PostgreSQL

### Passos para Execução

1. Certifique-se de estar no diretório correto:
   ```bash
   cd /API
   ```

2. Construa a imagem a partir do Dockerfile:
   ```bash
   docker build -t aipolicy-api:1.0 .
   ```

3. Verifique se a imagem foi criada:
   ```bash
   docker images
   ```

4. Execute o container a partir da imagem criada:
   ```bash
   docker run -d \
     --name aipolicy-api-container \
     -p 8080:8080 \
     --link postgres-container:db \
     -e "ConnectionStrings__DefaultConnection=Host=db;Port=5432;Database=pw_tools_db;Username=app_user;Password=app#aipolicypwd" \
     aipolicy-api:1.0
   ```

5. Verifique se o container está em execução:
   ```bash
   docker ps
   ```

6. Teste a API:
   ```bash
   curl http://localhost:8080/api/Trigger
   ```

## Estrutura do Projeto

O código fonte da API está organizado no diretório `/AIPolicy`.

## Referências

Para mais informações sobre ASP.NET Core Web API, consulte a [documentação oficial](https://learn.microsoft.com/pt-br/aspnet/core/web-api/?view=aspnetcore-8.0).