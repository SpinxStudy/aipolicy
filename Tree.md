AIPolicy/                         // Raiz do projeto
├── API/                          // Projeto da API (.NET 8)
│   └── AIPolicy/                 // Solução/projeto principal
│       ├── AIPolicy.API/         // Projeto Web API
│       │   ├── Controllers/      // Controllers da API
│       │   ├── Program.cs
│       │   ├── appsettings.json
│       │   └── ...               // Outros arquivos e pastas
│       ├── AIPolicy.Application/ // Camada de aplicação (services, use cases, etc.)
│       │   └── ...               
│       ├── AIPolicy.Core/        // Camada de domínio
│       │   └── Entity/
│       │       ├── Policy.cs
│       │       ├── Trigger.cs
│       │       └── Condition.cs
│       └── AIPolicy.Infrastructure/ // Camada de persistência
│           └── Persistency/
│               └── Repository/
│                   └── PolicyRepository.cs
│       └── Dockerfile           // Dockerfile para containerizar a API
├── APP/                          // Projeto da aplicação
│   └── Angular/                  // Aplicação Angular 19
│       ├── Dockerfile.dev        // Dockerfile para desenvolvimento (linka a pasta Angular)
│       ├── angular.json          // Configuração do Angular CLI
│       ├── package.json
│       ├── tsconfig.json
│       └── src/
│           ├── app/
│           │   ├── app.component.ts
│           │   ├── app.config.ts
│           │   ├── app.route.ts
│           │   └── features/
│           │       └── policy/
│           │           ├── policy-list/
│           │           │   ├── policy-list.component.ts
│           │           │   ├── policy-list.component.html
│           │           │   └── policy-list.component.css
│           │           ├── policy-form/      // Novo componente para formulário (CRUD)
│           │           │   ├── policy-form.component.ts
│           │           │   ├── policy-form.component.html
│           │           │   └── policy-form.component.css
│           │           ├── models/
│           │           │   └── policy.model.ts
│           │           └── services/
│           │               └── policy.service.ts
│           ├── assets/
│           └── environments/
│               ├── environment.ts
│               └── environment.prod.ts
└── Database/                     // Configuração e scripts do banco de dados
    ├── init.sql                  // Script de criação de usuário, banco, tabelas e dados mock
    └── Dockerfile                // Dockerfile para containerizar o PostgreSQL