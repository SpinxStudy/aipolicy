FROM postgres:latest

# Variaveis de ambiente
ENV POSTGRES_USER=admin
ENV POSTGRES_PASSWORD=default
ENV POSTGRES_DB=pw_tools_db

# Utiliza o script init para configuracoes iniciais do banco de dados
COPY init.sql /docker-entrypoint-initdb.d/

# Expor a porta do postgres
EXPOSE 5432

# Executa comandos
CMD ["postgres"]