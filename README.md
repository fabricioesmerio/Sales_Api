# 🧾 SalesSolution — Sales Management API

API RESTful para gerenciamento de **vendas**, aplicando **regras de desconto**, **cancelamento**, e **eventos de domínio simulados (logados no console)**.  
Desenvolvida em **.NET 8**, utilizando **Entity Framework Core + PostgreSQL (Docker)** e **xUnit** para testes unitários.  
Arquitetura baseada em **DDD (Domain-Driven Design)** e **Clean Architecture**.

---

## 📦 Estrutura do Projeto

```
Sales_Api/
├── src/
│   ├── Sales.Api/               # Camada de apresentação (Web API)
│   ├── Sales.Application/       # Casos de uso e Handlers (CQRS)
│   ├── Sales.Domain/            # Entidades e Regras de Negócio
│   └── Sales.Infrastructure/    # Acesso a dados (EF Core + PostgreSQL)
├── tests/
│   └── Sales.Tests/             # Testes unitários (xUnit + FluentAssertions)
└── docker-compose.yml           # Configuração do PostgreSQL
```

---

## ⚙️ Pré-requisitos

| Ferramenta | Versão recomendada | Instalação |
|-------------|--------------------|-------------|
| [.NET SDK](https://dotnet.microsoft.com/download/dotnet/8.0) | 8.0+ | `dotnet --version` |
| [Docker Desktop](https://www.docker.com/products/docker-desktop/) | 20.10+ | `docker --version` |
| [Git](https://git-scm.com/downloads) | 2.0+ | `git --version` |

---

## 🚀 Passo a Passo para Rodar o Projeto

### 1️⃣ Clonar o repositório

```bash
git clone https://github.com/fabricioesmerio/Sales_Api.git
cd Sales_Api
```

---

### 2️⃣ Subir o banco de dados PostgreSQL com Docker

```bash
docker-compose up -d
```

Esse comando cria e sobe um container com:
- PostgreSQL rodando na **porta 5432**
- Database: `salesdb`
- Usuário: `postgres`
- Senha: `postgres`

Verifique se está rodando:
```bash
docker ps
```

---

### 3️⃣ Restaurar dependências

```bash
dotnet restore
```

Instalará todos os pacotes NuGet necessários.

---

### 4️⃣ Aplicar migrations automaticamente

> 💡 As migrations são aplicadas automaticamente no startup.  
> Na primeira execução da API, o banco será criado se não existir.

---

### 5️⃣ Rodar a API

```bash
dotnet run --project src/Sales.Api
```

A API ficará disponível em:

- 🔹 HTTP → `http://localhost:5000`  
- 🔹 HTTPS → `https://localhost:5001`

---

### 6️⃣ Rodar os testes unitários

```bash
dotnet test tests/Sales.Tests
```

---

## 🧩 Tecnologias Utilizadas

- **.NET 8.0**
- **Entity Framework Core 8 + PostgreSQL**
- **CQRS Pattern (Command Handlers)**
- **Repository Pattern**
- **Dependency Injection**
- **xUnit**
- **Docker Compose**

---

## 📂 Principais Projetos

| Projeto | Descrição |
|----------|------------|
| `Sales.Api` | Camada de apresentação (Controllers REST, Startup) |
| `Sales.Application` | Lógica de aplicação e Handlers (CQRS) |
| `Sales.Domain` | Entidades e regras de negócio |
| `Sales.Infrastructure` | Implementação do EF Core e persistência |
| `Sales.Tests` | Testes unitários com xUnit |

---

## 📚 Endpoints

### 🔹 Criar uma venda
```bash
curl -X POST https://localhost:5001/api/sales   -H "Content-Type: application/json"   -d '{
        "saleNumber": "A123",
        "date": "2025-10-24T00:00:00Z",
        "customer": "John Doe",
        "branch": "Main",
        "items": [
          { "product": "Laptop", "quantity": 5, "unitPrice": 2000 }
        ]
      }'
```

### 🔹 Buscar todas as vendas
```bash
curl -X GET https://localhost:5001/api/sales
```

### 🔹 Buscar venda por ID
```bash
curl -X GET https://localhost:5001/api/sales/{id}
```

### 🔹 Atualizar uma venda
```bash
curl -X PUT https://localhost:5001/api/sales/{id} \
  -H "Content-Type: application/json" \
  -d '{
        "saleNumber": "A123",
        "date": "2025-10-24T00:00:00Z",
        "customer": "John Doe",
        "branch": "Main",
        "items": [
          { 
            "id": "b1c6f90a-2b7f-4e48-9d44-1b5e29c9d121", 
            "product": "Laptop", 
            "quantity": 10, 
            "unitPrice": 1900 
          },
          { 
            "id": "a41ff6f2-42c0-4a3c-8e5c-7dca9a29e98e", 
            "product": "Mouse", 
            "quantity": 5, 
            "unitPrice": 120 
          }
        ],
        "totalAmount": 0
      }'
```

### 🔹 Cancelar uma venda
```bash
curl -X PATCH https://localhost:5001/api/sales/{id}
```

### 🔹 Deletar uma venda
```bash
curl -X DELETE https://localhost:5001/api/sales/{id}
```

---

## 🧠 Regras de Negócio

- **4–9 itens:** 10% de desconto  
- **10–20 itens:** 20% de desconto  
- **>20 itens:** não permitido  
- **<4 itens:** sem desconto

---

## 🪵 Eventos simulados (Rebus)

Os seguintes eventos são logados no console:

- `saleCreated`
- `saleModified`
- `saleCancelled`
- `itemCancelled`

---

## 🧭 Swagger

Após rodar a API, acesse:

👉 [https://localhost:5001/swagger](https://localhost:5001/swagger)

---


## 🧰 Comandos úteis

| Ação | Comando |
|------|----------|
| Restaurar pacotes | `dotnet restore` |
| Compilar o projeto | `dotnet build` |
| Rodar a API | `dotnet run --project src/Sales.Api` |
| Rodar migrations manualmente | `dotnet ef database update --project src/Sales.Infrastructure --startup-project src/Sales.Api` |
| Rodar testes | `dotnet test tests/Sales.Tests` |
| Parar containers Docker | `docker-compose down` |

---

## 👤 Autor

**Fabrício Esmério**  
🔗 [github.com/fabricioesmerio](https://github.com/fabricioesmerio)
