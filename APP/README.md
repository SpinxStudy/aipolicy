--------- AIPolicy DB ---------
ASP.NET Core Web API - .NET 8
Author: Wladson Cedraz
-------------------------------

1. Garanta estar no diretorio (/APP):
cd /APP

2. Construa a imagem a partir do Dockerfile:
docker build -t aipolicy-api:1.0 .

3. Verifique a imagem:
docker images

4. Execute o container a partir da imagem criada:
docker run -d `
  --name aipolicy-api-container `
  -p 8080:8080 `
  --link postgres-container:db `
  -e "ConnectionStrings__DefaultConnection=Host=db;Port=5432;Database=pw_tools_db;Username=app_user;Password=app#aipolicypwd" `
  aipolicy-api:1.0

5. Verifique o container:
docker ps

6. Teste a API:
curl http://localhost:8080/weatherforecast