--------- AIPolicy DB ---------
PostgreSQL v16
Author: Wladson Cedraz
-------------------------------

1. Garanta que esteja trabalhando no diretorio correto:
cd aipolicy/Database

2. Construa a imagem docker:
docker build -t docker-postgresdb:1.0 .

3. Verifique a imagem criada:
docker images

4. Inicie o container com a imagem construida:
docker run -d --name postgres-container -p 5432:5432 -v pgdata:/var/lib/postgresql/data docker-postgresdb:1.0

5. Verifique os containers em execucao:
docker ps

6. Realize um teste de conexao:
psql -h localhost -p 5432 -U admin -d pw_tools_db


Utils:
docker logs nome_do_container

{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=pw_tools_db;Username=app_user;Password=app#aipolicypwd"
  }
}