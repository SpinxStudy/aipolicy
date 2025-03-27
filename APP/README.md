# AIPolicy APP

Frontend desenvolvido em Angular 19 com Node.js 20.

**Autor:** Wladson Cedraz

## Instalação e Execução

### Pré-requisitos

- Docker instalado
- Node.js 20 (instalado automaticamente no container)

### Passos para Execução

1. Certifique-se de estar na pasta correta do projeto:
   ```bash
   cd /APP
   ```

2. Construa a imagem docker de desenvolvimento:
   ```bash
   docker build -t angular-dev:1.0 -f Dockerfile.dev .
   ```

3. Verifique a imagem criada:
   ```bash
   docker images
   ```

4. Execute o container docker para desenvolvimento mapeando o código fonte:
   ```bash
   docker run -d \
       --name angular-dev-container \
       -v ${PWD}:/app \
       -p 4200:4200 \
       angular-dev:1.0 \
       tail -f /dev/null
   ```

5. Conecte-se ao container:
   ```bash
   docker exec -it angular-dev-container /bin/bash
   ```

6. Instale as dependências:
   ```bash
   cd Angular
   npm install
   ```

7. Execute a aplicação em modo de desenvolvimento:
   ```bash
   ng serve --host 0.0.0.0 --port 4200 --hmr --poll=2000
   ```

8. Start container:
   ```bash
   docker start angular-dev-container
   ```

9. Conecte-se ao container:
   ```bash
   docker exec -it angular-dev-container bash
   ```

## Estrutura do Projeto

O código fonte da aplicação Angular está organizado no diretório `/Angular`.

## Referências

Para mais informações sobre o Angular, consulte a [documentação oficial](https://angular.io/docs).