# Collection Postman

Para facilitar os testes da API, você pode importar os seguintes exemplos de requisições no Postman.

## Endpoints Disponíveis

### Base URL
```
http://localhost:5000/api
```

### 1. Cadastrar Estudante

**POST** `/estudantes/cadastro`

**Body (JSON):**
```json
{
  "nome": "João Silva",
  "email": "joao@example.com"
}
```

**Response (200 OK):**
```json
{
  "id": "guid",
  "nome": "João Silva",
  "email": "joao@example.com",
  "dataCadastro": "2024-01-01T00:00:00Z",
  "status": "Ativo"
}
```

### 2. Adicionar Cartão de Crédito

**POST** `/estudantes/{estudanteId}/cartoes`

**Body (JSON):**
```json
{
  "numero": "1234567890123456",
  "nomeTitular": "João Silva",
  "validade": "2025-12-31T00:00:00",
  "cvv": "123"
}
```

**Response (200 OK):**
```json
{
  "id": "guid",
  "estudanteId": "guid",
  "numero": "1234567890123456",
  "nomeTitular": "João Silva",
  "validade": "2025-12-31T00:00:00",
  "cvv": "123",
  "dataCadastro": "2024-01-01T00:00:00Z"
}
```

### 3. Listar Cursos

**GET** `/cursos`

**Response (200 OK):**
```json
[
  {
    "id": "guid",
    "titulo": "ASP.NET Core Avançado",
    "descricao": "Curso completo sobre ASP.NET Core",
    "preco": 299.90,
    "duracao": 40,
    "professor": "Prof. Carlos Mendes",
    "ativo": true
  }
]
```

### 4. Realizar Matrícula

**POST** `/matriculas`

**Body (JSON):**
```json
{
  "estudanteId": "guid-do-estudante",
  "cursoId": "guid-do-curso"
}
```

**Response (200 OK):**
```json
{
  "id": "guid",
  "estudanteId": "guid",
  "cursoId": "guid",
  "dataMatricula": "2024-01-01T00:00:00Z",
  "status": "Ativa",
  "nomeEstudante": "João Silva",
  "tituloCurso": "ASP.NET Core Avançado"
}
```

**Erro (400 Bad Request) - Sem pagamentos aprovados:**
```json
{
  "error": "É necessário ter pelo menos um pagamento aprovado para realizar matrícula",
  "errors": []
}
```

### 5. Listar Pagamentos do Estudante

**GET** `/pagamentos/estudante/{estudanteId}`

**Response (200 OK):**
```json
[
  {
    "id": "guid",
    "estudanteId": "guid",
    "valor": 100.00,
    "dataPagamento": "2024-01-01T00:00:00Z",
    "status": "Aprovado",
    "descricao": "Pagamento inicial"
  }
]
```

## Health Check

**GET** `/health`

**Response (200 OK):**
```
Healthy
```

## Importar no Postman

1. Abra o Postman
2. Clique em "Import"
3. Crie uma nova collection manualmente ou use os exemplos acima
4. Configure a variável de ambiente `baseUrl` como `http://localhost:5000/api`

## Exemplos de Testes

### Teste de Validação
Tente cadastrar um estudante com email inválido:
```json
{
  "nome": "Teste",
  "email": "email-invalido"
}
```

Deve retornar 400 Bad Request com mensagens de validação.

### Teste de Regra de Negócio
Tente realizar uma matrícula sem ter pagamentos aprovados:
```json
{
  "estudanteId": "guid-sem-pagamento",
  "cursoId": "guid-do-curso"
}
```

Deve retornar 400 Bad Request informando que é necessário ter pagamento aprovado.

