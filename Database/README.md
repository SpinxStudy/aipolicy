# AIPolicy Database

Banco de dados PostgreSQL v16 para a aplicação AIPolicy.

**Autor:** Wladson Cedraz

## Instalação e Execução

### Pré-requisitos

- Docker instalado
- Conhecimentos básicos de PostgreSQL

### Passos para Execução

1. Certifique-se de estar no diretório correto:
   ```bash
   cd /Database
   ```

2. Construa a imagem Docker:
   ```bash
   docker build -t docker-postgresdb:1.0 .
   ```

3. Verifique a imagem criada:
   ```bash
   docker images
   ```

4. Inicie o container com a imagem construída:
   ```bash
   docker run -d --name postgres-container -p 5432:5432 -v pgdata:/var/lib/postgresql/data docker-postgresdb:1.0
   ```

5. Verifique os containers em execução:
   ```bash
   docker ps
   ```

6. Realize um teste de conexão:
   ```bash
   psql -h localhost -p 5432 -U admin -d pw_tools_db
   ```

## Informações Úteis

### Verificação de Logs
Para verificar os logs do container:
```bash
docker logs postgres-container
```

### String de Conexão
A string de conexão para aplicações:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=pw_tools_db;Username=app_user;Password=app#aipolicypwd"
  }
}
```

## Estrutura do Banco de Dados

O script de inicialização do banco está disponível no arquivo `init.sql`.

## Referências

Para mais informações sobre PostgreSQL, consulte a [documentação oficial](https://www.postgresql.org/docs/16/index.html).