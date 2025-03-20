--------- AIPolicy DB ---------
Angular 19 - Node.js 20
Author: Wladson Cedraz
-------------------------------

1. Certifique-se de estar na pasta correta do projeto (/APP):
cd APP

2. Construa a imagem docker de desenvolvimento:
docker build -t angular-dev:1.0 -f Dockerfile.dev .

3. Verifique a imagem criada:
docker images

4. Execute o container docker para desenvolvimento mapeando o fonte do projeto /APP/SRC/ para a /app no container:
docker run -d `
    --name angular-dev-container `
    -v ${PWD}:/app `
    -p 4200:4200 `
    angular-dev:1.0 `
    tail -f /dev/null

5. Conecte-se ao container:
docker exec -it angular-dev-container /bin/bash

6. Instale as dependencias:
cd Angular
npm install

6. Testando a execucao local (container):
ng serve --host 0.0.0.0 --port 4200 --hmr --poll=2000