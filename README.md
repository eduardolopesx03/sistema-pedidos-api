# SistemaPedidosAPI

**SistemaPedidosAPI** é uma API RESTful desenvolvida em **ASP.NET Core** para gerenciar clientes, produtos e pedidos. Utiliza **PostgreSQL** como banco de dados, permitindo a manipulação eficiente dos dados essenciais para um sistema de vendas e inventário.

A API oferece endpoints para a criação, leitura, atualização e exclusão de **clientes**, **produtos** e **pedidos**. É altamente configurável e pode ser integrada com sistemas externos de e-commerce ou ERP.

## Tecnologias Utilizadas

- **ASP.NET Core 8.0** – Framework utilizado para desenvolver a API.
- **Entity Framework Core** – ORM para facilitar a interação com o banco de dados.
- **PostgreSQL** – Sistema de gerenciamento de banco de dados relacional.
- **Swagger** – Para documentação e testes interativos da API.
- **MediatR** – Para gerenciar as requisições e respostas de forma organizada.
- **AutoMapper** – Para mapeamento automático entre entidades e DTOs (Data Transfer Objects).

---

## Instalação

### 1. Clone o repositório
Clone o repositório para a sua máquina local:
```bash
git clone https://github.com/seu-usuario/SistemaPedidosAPI.git
cd SistemaPedidosAPI
```

### 2. Instale as dependências
Instale as dependências do projeto usando o comando:
```bash
dotnet restore
```

### 3. Instale o PostgreSQL
Se você ainda não tem o **PostgreSQL** instalado, siga as instruções abaixo:

- **Debian/Ubuntu**:
  ```bash
  sudo apt update
  sudo apt install postgresql postgresql-contrib
  ```

- **Windows**: Baixe e instale o PostgreSQL a partir do [site oficial](https://www.postgresql.org/download/).

---

## Configuração

### 1. Configuração do Banco de Dados
No arquivo `appsettings.json`, atualize a string de conexão para o PostgreSQL, substituindo `seu_usuario` e `sua_senha` pelos dados do seu banco de dados:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=sistema_pedidos;Username=seu_usuario;Password=sua_senha;"
}
```

### 2. Configuração do DbContext
No arquivo `Program.cs`, verifique se a configuração do `ApplicationDbContext` está usando o PostgreSQL corretamente:

```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
```

---

## Migrations e Inicialização do Banco de Dados

### 1. Gerar as Migrações
Execute o seguinte comando para gerar a migração inicial:
```bash
dotnet ef migrations add InitialCreate
```

### 2. Aplicar as Migrações
Execute o comando abaixo para atualizar o banco de dados com as migrações geradas:
```bash
dotnet ef database update
```

---

## Execução do Projeto

### 1. Inicie o Servidor
Execute o seguinte comando para iniciar o servidor da API:
```bash
dotnet run
```

### 2. Acessar a API
A API estará disponível no endereço `https://localhost:5045` (ou outro configurado em sua aplicação).

### 3. Swagger
A documentação interativa do Swagger estará disponível em: `https://localhost:5045/swagger`. Você pode acessar todos os endpoints da API, testar requisições e visualizar as respostas diretamente através dessa interface.

---

## Endpoints Principais

### **Clientes**
- **GET** `/v1/clientes` – Retorna todos os clientes cadastrados.
- **GET** `/v1/clientes/{id}` – Retorna os dados de um cliente específico.
- **POST** `/v1/clientes` – Cria um novo cliente.
- **PUT** `/v1/clientes/{id}` – Atualiza os dados de um cliente existente.
- **DELETE** `/v1/clientes/{id}` – Exclui um cliente.

### **Produtos**
- **GET** `/v1/produtos` – Retorna todos os produtos cadastrados.
- **GET** `/v1/produtos/{id}` – Retorna as informações de um produto específico.
- **POST** `/v1/produtos` – Cria um novo produto.
- **PUT** `/v1/produtos/{id}` – Atualiza as informações de um produto existente.
- **DELETE** `/v1/produtos/{id}` – Exclui um produto.

### **Pedidos**
- **GET** `/v1/pedidos` – Retorna todos os pedidos realizados.
- **GET** `/v1/pedidos/{id}` – Retorna os detalhes de um pedido específico.
- **POST** `/v1/pedidos` – Cria um novo pedido com produtos e quantidades.
- **PUT** `/v1/pedidos/{id}` – Atualiza um pedido, como o status ou os produtos associados.
- **DELETE** `/v1/pedidos/{id}` – Exclui um pedido.

---

## Exemplos de Uso

### Criar um Novo Cliente
- **Requisição**: `POST /v1/clientes`
- **Corpo da Requisição**:
  ```json
  {
    "nome": "Carlos Oliveira",
    "email": "carlos.oliveira@exemplo.com",
    "numero_contato": "11987654321",
    "data_nascimento": "1990-08-15"
  }
  ```

### Listar Todos os Produtos
- **Requisição**: `GET /v1/produtos`
- **Resposta**:
  ```json
  [
    {
      "id": 1,
      "nome": "Produto A",
      "preco": 100.00,
      "quantidade_estoque": 50
    },
    {
      "id": 2,
      "nome": "Produto B",
      "preco": 200.00,
      "quantidade_estoque": 30
    }
  ]
  ```

### Criar um Novo Pedido
- **Requisição**: `POST /v1/pedidos`
- **Corpo da Requisição**:
  ```json
  {
    "clienteId": 1,
    "status": "Em Andamento",
    "pedidoProdutos": [
      { "produtoId": 1, "quantidade": 2 },
      { "produtoId": 2, "quantidade": 1 }
    ]
  }
  ```

### Atualizar o Status de um Pedido
- **Requisição**: `PUT /v1/pedidos/1`
- **Corpo da Requisição**:
  ```json
  {
    "status": "Concluído",
    "pedidoProdutos": [
      { "produtoId": 1, "quantidade": 2 }
    ]
  }
  ```

---

## Observações Importantes

- Certifique-se de que o **PostgreSQL** está em execução antes de iniciar o projeto.
- Caso altere a string de conexão no arquivo `appsettings.json`, não se esqueça de atualizar as configurações e regenerar as migrações caso necessário.
- Utilize o Swagger (`https://localhost:5045/swagger`) para testar interativamente a API e ver exemplos de respostas.
```
