# Etapa 1: Build da aplicação (usando o SDK do .NET)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de solução e todos os projetos
COPY AIPolicy/AIPolicy.sln .
COPY AIPolicy/AIPolicy.Core/AIPolicy.Core.csproj AIPolicy.Core/
COPY AIPolicy/AIPolicy.Application/AIPolicy.Application.csproj AIPolicy.Application/
COPY AIPolicy/AIPolicy.Infrastructure/AIPolicy.Infrastructure.csproj AIPolicy.Infrastructure/
COPY AIPolicy/AIPolicy.API/AIPolicy.API.csproj AIPolicy.API/

# Restaura as dependências da solução
RUN dotnet restore AIPolicy.sln

# Copia o código-fonte de todos os projetos
COPY AIPolicy/AIPolicy.Core/ AIPolicy.Core/
COPY AIPolicy/AIPolicy.Application/ AIPolicy.Application/
COPY AIPolicy/AIPolicy.Infrastructure/ AIPolicy.Infrastructure/
COPY AIPolicy/AIPolicy.API/ AIPolicy.API/

# Compila e publica o projeto da API
WORKDIR /src/AIPolicy.API
RUN dotnet publish AIPolicy.API.csproj -c Release -o /app/publish

# Etapa 2: Criação da imagem de runtime (usando apenas o runtime do .NET)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080

# Define a variável de ambiente para a porta
ENV ASPNETCORE_URLS=http://+:8080

# Comando para iniciar a API
ENTRYPOINT ["dotnet", "AIPolicy.API.dll"]