# Documentação de Arquitetura

## Visão Geral

A Plataforma de Cursos Online foi desenvolvida seguindo os princípios de **Clean Architecture** e **SOLID**, garantindo:
- Separação de responsabilidades
- Baixo acoplamento
- Alta testabilidade
- Facilidade de manutenção

## Camadas da Aplicação

### 1. Domain (Domínio)

**Responsabilidade**: Regras de negócio puras, sem dependências externas.

#### Entidades
- `Estudante`: Representa um estudante na plataforma
- `Curso`: Representa um curso disponível
- `Matricula`: Relacionamento entre estudante e curso
- `Pagamento`: Registro de pagamentos realizados
- `CartaoCredito`: Cartão de crédito vinculado a um estudante

#### Value Objects
- `Email`: Value object para email com validação

#### Domain Events
- `MatriculaRealizadaEvent`: Disparado quando uma matrícula é realizada

#### Interfaces
- `IRepository<T>`: Contrato para repositórios
- `IUnitOfWork`: Contrato para gerenciamento de transações

### 2. Application (Aplicação)

**Responsabilidade**: Casos de uso e orquestração.

#### Commands (Escrita)
- `CadastrarEstudanteCommand`
- `AdicionarCartaoCreditoCommand`
- `RealizarMatriculaCommand`

#### Queries (Leitura)
- `ObterTodosCursosQuery`
- `ObterPagamentosEstudanteQuery`

#### Handlers
- Implementam a lógica dos commands/queries
- Utilizam repositórios para acesso a dados
- Aplicam regras de negócio

#### Validators
- FluentValidation para validação de commands

#### DTOs
- Objetos de transferência de dados entre camadas

### 3. Infrastructure (Infraestrutura)

**Responsabilidade**: Implementações técnicas.

#### Repositories
- Implementações concretas de `IRepository<T>`
- Atualmente em memória (pode ser substituído por EF Core, MongoDB, etc.)

#### Services
- `MockEmailService`: Serviço mockado de email
- `CartaoCreditoService`: Processamento de pagamentos

#### UnitOfWork
- Gerenciamento de transações (mockado)

### 4. API (Apresentação)

**Responsabilidade**: Interface HTTP.

#### Controllers
- `EstudantesController`
- `CursosController`
- `MatriculasController`
- `PagamentosController`

#### Configuration
- Swagger/OpenAPI
- CORS
- Dependency Injection
- Logging
- Health Checks
- Exception Handling

## Fluxo de Dados

```
Client (Angular)
    ↓
API Controllers
    ↓
MediatR (CQRS)
    ↓
Command/Query Handlers
    ↓
Repositories (via Interfaces)
    ↓
Domain Entities
```

## Princípios Aplicados

### Single Responsibility
Cada classe tem uma única responsabilidade:
- Handlers processam um único comando/query
- Repositories gerenciam apenas acesso a dados
- Services têm responsabilidades específicas

### Open/Closed
Sistema aberto para extensão, fechado para modificação:
- Novos métodos de pagamento via `IMetodoPagamento`
- Novos repositórios implementando `IRepository<T>`

### Liskov Substitution
Subtipos podem ser substituídos por seus tipos base:
- Qualquer implementação de `IRepository<T>` pode ser usada
- Serviços podem ser mockados facilmente

### Interface Segregation
Interfaces específicas:
- `IEmailService`: Apenas para envio de emails
- `IMetodoPagamento`: Apenas para processamento de pagamentos

### Dependency Inversion
Dependências através de abstrações:
- Handlers dependem de interfaces, não de implementações
- Dependency Injection em toda a aplicação

## Padrões Utilizados

### Repository Pattern
Abstração de acesso a dados, permitindo troca de implementação sem alterar código de negócio.

### CQRS
Separação de leitura e escrita:
- Commands: Modificam estado
- Queries: Apenas leitura (com cache quando apropriado)

### Mediator Pattern
MediatR desacopla controllers de handlers, facilitando testes e manutenção.

### Domain Events
Eventos de domínio permitem reações a ações importantes sem acoplamento direto.

## Testes

### Testes Unitários
- Testam handlers isoladamente
- Utilizam mocks para dependências

### Testes de Integração
- Testam controllers completos
- Utilizam HttpClient para chamadas HTTP

## Cache

Cache estratégico implementado para:
- Listagem de cursos (5 minutos)
- Reduz carga no repositório

## Logging

Logging estruturado com Serilog:
- Logs de ações importantes
- Tratamento de erros
- Facilita debugging e monitoramento

