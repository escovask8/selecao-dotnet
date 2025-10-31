# Instruções de Execução

## Configuração do Ambiente

### Backend (.NET 8.0)

1. **Verificar instalação do .NET SDK:**
```bash
dotnet --version
```
Deve retornar versão 8.0 ou superior.

2. **Instalar dependências:**
```bash
cd src/Backend
dotnet restore
```

3. **Compilar o projeto:**
```bash
dotnet build
```

4. **Executar a API:**
```bash
cd PlataformaCursos.API
dotnet run
```

A API estará disponível em:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger: `http://localhost:5000` (raiz)

### Frontend (Angular 17)

1. **Verificar instalação do Node.js:**
```bash
node --version
```
Deve retornar versão 18 ou superior.

2. **Instalar Angular CLI globalmente (se necessário):**
```bash
npm install -g @angular/cli@17
```

3. **Instalar dependências:**
```bash
cd src/Frontend
npm install
```

4. **Executar o frontend:**
```bash
npm start
# ou
ng serve
```

O frontend estará disponível em `http://localhost:4200`

## Testando a Aplicação

### 1. Cadastrar um Estudante

**Swagger:** `POST /api/estudantes/cadastro`
```json
{
  "nome": "João Silva",
  "email": "joao@example.com"
}
```

**Angular:** Navegue para `/estudantes/cadastrar`

### 2. Adicionar Cartão de Crédito

**Swagger:** `POST /api/estudantes/{id}/cartoes`
```json
{
  "numero": "1234567890123456",
  "nomeTitular": "João Silva",
  "validade": "2025-12-31T00:00:00",
  "cvv": "123"
}
```

**Angular:** Navegue para `/estudantes/{id}/cartoes/adicionar`

### 3. Listar Cursos

**Swagger:** `GET /api/cursos`

**Angular:** Navegue para `/cursos`

### 4. Realizar Matrícula

**Swagger:** `POST /api/matriculas`
```json
{
  "estudanteId": "guid-do-estudante",
  "cursoId": "guid-do-curso"
}
```

**Angular:** Na página de cursos, clique em "Matricular-se"

### 5. Consultar Pagamentos

**Swagger:** `GET /api/pagamentos/estudante/{estudanteId}`

**Angular:** Navegue para `/pagamentos` e informe o ID do estudante

## Executando Testes

### Backend

```bash
cd src/Backend
dotnet test
```

### Frontend

```bash
cd src/Frontend
ng test
```

## Dados de Seed

O sistema possui dados iniciais (seed data):
- 2 estudantes de exemplo
- 3 cursos de exemplo
- 1 pagamento aprovado para o primeiro estudante (para permitir matrícula)

## Troubleshooting

### Erro de CORS

Se o frontend não conseguir conectar à API, verifique:
- A API está rodando na porta 5000
- O CORS está configurado em `Program.cs`
- A URL da API no `ApiService` está correta (`http://localhost:5000`)

### Erro de Compilação

- Verifique se todas as dependências foram instaladas (`dotnet restore` / `npm install`)
- Verifique se está usando as versões corretas (.NET 8.0, Angular 17)

### Erro ao Executar

- Verifique se as portas 5000 e 4200 estão livres
- Verifique os logs no console para mais detalhes

## Estrutura de Pastas Esperada

```
src/
├── Backend/
│   ├── PlataformaCursos.API/
│   ├── PlataformaCursos.Application/
│   ├── PlataformaCursos.Domain/
│   ├── PlataformaCursos.Infrastructure/
│   ├── PlataformaCursos.Tests/
│   └── PlataformaCursos.sln
└── Frontend/
    ├── src/
    ├── angular.json
    └── package.json
```

## Próximos Passos

Após executar a aplicação:
1. Acesse o Swagger para explorar a API
2. Teste os endpoints
3. Use o frontend Angular para interagir com a plataforma
4. Verifique os logs para entender o fluxo de execução

