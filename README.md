# Repositório - API de Cadastro e Vendas

## Passos para Execução

### Swagger do projeto? Https://localhost:8081/swagger/index.html

### 1️⃣ **Cadastro de Usuário**
#### **Endpoint:** `/api/users` (POST)
#### **JSON de Exemplo:**
```json
{
  "username": "dermeterio",
  "password": "D@n160889",
  "phone": "16992074751",
  "email": "ermeterio@gmail.com",
  "status": 1,
  "role": 2
}
```

---

### 2️⃣ **Cadastro de Empresa**
#### **Endpoint:** `/api/company` (POST)
#### **JSON de Exemplo:**
```json
{
  "name": "TESTE S/A",
  "cnpj": "50664631000183"
}
```

---

### 3️⃣ **Cadastro de Categoria**
#### **Endpoint:** `/api/category` (POST)
#### **JSON de Exemplo:**
```json
{
    "Name" : "Category test",
    "CompanyId" : "fcc436b1-43f0-4b43-92e8-6ea56fa025b1"
}
```

---

### 4️⃣ **Cadastro de Produto**
#### **Endpoint:** `/api/product` (POST)
#### **JSON de Exemplo:**
```json
{
  "name": "Teste 3",
  "price": 100.50,
  "categoryId": "0565c0eb-9a12-416f-b413-a51b08e464b6",
  "description": "Produto Teste",
  "actualStock": 100,
  "companyId": "fcc436b1-43f0-4b43-92e8-6ea56fa025b1",
  "maxItemsForSale" : 5
}
```

---

### 5️⃣ **Cadastro de Desconto**
#### **Endpoint:** `/api/product/discount` (POST)
#### **JSON de Exemplo:**
```json
{
  "productId": "473a62e5-6b09-40c9-8c6c-477c95cb2e96",
  "value": 10,
  "quantity": 1,
  "startDate": "2025-01-01T01:22:05.277Z",
  "endDate": "2027-03-05T01:22:05.277Z"
}
```

---

### 6️⃣ **Cadastro de Venda**
#### **Endpoint:** `/api/sale` (POST)
#### **JSON de Exemplo:**
```json
{
  "companyId": "0b751aa8-81aa-4f0a-9fa9-f6697e2d8c00",
  "userId": "c8cd7ebe-5a7d-4910-a092-482efb9b6722",
  "items": [
    {
      "productId": "473a62e5-6b09-40c9-8c6c-477c95cb2e96",      
      "quantity": 1
    }
  ]
}
```

---

### 7️⃣ **Consulta de Produtos com Paginação**
#### **Endpoint:** `/api/product/GetByFilter` (GET)
- Utilize esse endpoint para obter produtos com suporte a paginação.
- Antes de realizar as chamadas subsequentes, consulte no banco de dados as **chaves de empresa, categoria e produto**.

---

### **Observações**
- Certifique-se de que os endpoints estão corretamente configurados na API.
- Utilize uma ferramenta como **Postman** ou **Swagger** para testar as requisições.
- Confirme os **IDs de empresa, categoria e produto** antes de realizar os cadastros dependentes.

---

Se tiver alguma dúvida ou precisar de suporte, entre em contato! 🚀

