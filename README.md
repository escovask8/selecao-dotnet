# Plataforma de Cursos Online

Plataforma de cursos online desenvolvida com Clean Architecture, princípios SOLID, ASP.NET Core Web API e Angular 17.

## 🏗️ Arquitetura

O projeto segue os princípios de **Clean Architecture** com separação clara de responsabilidades:

```
src/
├── Backend/ (ASP.NET Core Web API)
│   ├── PlataformaCursos.API/          # Camada de apresentação (Controllers, Swagger)
│   ├── PlataformaCursos.Application/  # Casos de uso (Commands, Queries, Handlers)
│   ├── PlataformaCursos.Domain/       # Entidades e regras de negócio
│   ├── PlataformaCursos.Infrastructure/ # Implementações (Repositórios, Serviços)
│   └── PlataformaCursos.Tests/       # Testes unitários e de integração
└── Frontend/ (Angular 17)
    └── src/
        └── app/
            ├── core/                   # Serviços e modelos compartilhados
            ├── features/               # Módulos de funcionalidades
            └── shared/                 # Componentes compartilhados
```

## 🎯 Principais Funcionalidades

- ✅ Cadastro de Estudantes
- ✅ Gerenciamento de Cartões de Crédito
- ✅ Listagem de Cursos Disponíveis
- ✅ Realização de Matrículas (com validação de pagamentos)
- ✅ Histórico de Pagamentos
- ✅ Envio de Email de Confirmação (mockado)

## 🛠️ Tecnologias Utilizadas

### Backend
- **.NET 8.0**
- **ASP.NET Core Web API**
- **MediatR** (CQRS)
- **FluentValidation** (Validações)
- **AutoMapper** (Mapeamento de DTOs)
- **Swagger/OpenAPI** (Documentação)
- **Serilog** (Logging estruturado)
- **xUnit** (Testes)
- **Moq** (Mocking)
- **FluentAssertions** (Assertions)

### Frontend
- **Angular 17**
- **TypeScript**
- **RxJS**
- **Standalone Components**

## 📋 Pré-requisitos

- .NET 8.0 SDK
- Node.js 18+ e npm
- Angular CLI 17+

## 🚀 Como Executar

### Backend

1. Navegue até a pasta do backend:
```bash
cd src/Backend
```

2. Restaure as dependências:
```bash
dotnet restore
```

3. Execute o projeto:
```bash
cd PlataformaCursos.API
dotnet run
```

A API estará disponível em:
- **API**: `http://localhost:5000` ou `https://localhost:5001`
- **Swagger**: `http://localhost:5000` (configurado como rota raiz)

### Frontend

1. Navegue até a pasta do frontend:
```bash
cd src/Frontend
```

2. Instale as dependências:
```bash
npm install
```

3. Execute o projeto:
```bash
npm start
```

O frontend estará disponível em `http://localhost:4200`

## 🧪 Executando os Testes

### Backend

```bash
cd src/Backend
dotnet test
```

## 📡 Endpoints da API

### Estudantes

- `POST /api/estudantes/cadastro` - Cadastra um novo estudante
- `POST /api/estudantes/{id}/cartoes` - Adiciona cartão de crédito ao estudante

### Cursos

- `GET /api/cursos` - Lista todos os cursos disponíveis (com cache)

### Matrículas

- `POST /api/matriculas` - Realiza matrícula de estudante em curso

### Pagamentos

- `GET /api/pagamentos/estudante/{estudanteId}` - Lista pagamentos do estudante

### Health Check

- `GET /health` - Status da aplicação

## 🏛️ Princípios SOLID Implementados

### Single Responsibility Principle (SRP)
- Cada classe tem uma única responsabilidade
- Handlers processam apenas um comando/query
- Serviços têm responsabilidades bem definidas

### Open/Closed Principle (OCP)
- Uso de interfaces permite extensão sem modificação
- `IMetodoPagamento` permite adicionar novos métodos de pagamento

### Liskov Substitution Principle (LSP)
- Repositórios implementam interfaces que podem ser substituídas
- Serviços podem ser mockados facilmente

### Interface Segregation Principle (ISP)
- Interfaces específicas (`IEmailService`, `IMetodoPagamento`)
- Clientes não dependem de interfaces que não usam

### Dependency Inversion Principle (DIP)
- Dependências através de interfaces
- Injeção de dependência em todos os serviços

## 🎨 Padrões de Projeto

### Repository Pattern
- Abstração de acesso a dados
- Implementação em memória para desenvolvimento

### CQRS (Command Query Responsibility Segregation)
- Separação de leitura e escrita
- Commands para modificações
- Queries para consultas

### Mediator Pattern
- MediatR para desacoplamento
- Comunicação via commands/queries

### Domain Events
- Eventos de domínio para ações importantes
- Matrícula gera evento que dispara email

## 📦 Estrutura do Backend

### Domain Layer
- **Entities**: Estudante, Curso, Matricula, Pagamento, CartaoCredito
- **Value Objects**: Email
- **Domain Events**: MatriculaRealizadaEvent
- **Interfaces**: IRepository, IUnitOfWork

### Application Layer
- **Commands**: CadastrarEstudante, AdicionarCartaoCredito, RealizarMatricula
- **Queries**: ObterTodosCursos, ObterPagamentosEstudante
- **Handlers**: Implementações dos commands/queries
- **Validators**: FluentValidation para validação
- **DTOs**: Objetos de transferência de dados

### Infrastructure Layer
- **Repositories**: Implementações em memória
- **Services**: MockEmailService, CartaoCreditoService
- **UnitOfWork**: Gerenciamento de transações

### API Layer
- **Controllers**: Endpoints RESTful
- **Configuration**: Swagger, CORS, DI, Logging

## 🎯 Diferenciais Implementados

- ✅ **Value Objects**: Email como value object
- ✅ **Domain Events**: MatriculaRealizadaEvent
- ✅ **Cache Estratégico**: Cache de listagem de cursos
- ✅ **Tratamento de Erros Global**: Middleware de exceções
- ✅ **Logging Estruturado**: Serilog
- ✅ **Health Checks**: Endpoint de status
- ✅ **Swagger com Exemplos**: Documentação interativa

## 📝 Validações de Negócio

### Matrícula
- Estudante deve existir
- Curso deve existir e estar ativo
- Estudante deve ter pelo menos um pagamento aprovado
- Estudante não pode ter matrícula ativa duplicada

### Cartão de Crédito
- Número deve ter 13-19 dígitos
- CVV deve ter 3-4 dígitos
- Validade não pode ser no passado

### Estudante
- Email deve ser válido e único
- Nome deve ter no mínimo 3 caracteres

## 🔒 Segurança

- Validações no backend (FluentValidation)
- Validações no frontend (reactive forms)
- CORS configurado para Angular
- Tratamento de erros padronizado

## 📚 Documentação Adicional

A documentação interativa da API está disponível no Swagger quando a aplicação está em execução.

## 🤝 Contribuindo

Este é um projeto de demonstração desenvolvido para processo seletivo, demonstrando:
- Arquitetura limpa e bem estruturada
- Aplicação dos princípios SOLID
- Testes unitários e de integração
- Código limpo e manutenível
- Boas práticas de desenvolvimento

## 📄 Licença

Este projeto foi desenvolvido para fins de demonstração técnica.

---

**Desenvolvido com ❤️ seguindo Clean Architecture e SOLID Principles**
