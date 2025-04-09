Collecting workspace information# Sistema de Cadastro de Clientes - Teste Dev Rommanel

Este projeto consiste em um sistema CRUD para cadastro de clientes, desenvolvido utilizando Angular no front-end e .NET 8 no back-end, seguindo os princípios de Domain-Driven Design (DDD) e o padrão CQRS.

## Estrutura do Projeto

### Back-end
- **Clientes.Api**: API REST que serve como ponto de entrada para o sistema
- **Clientes.Aplicacao**: Implementação do padrão CQRS com Commands e Queries
- **Clientes.Dominio**: Entidades, objetos de valor e regras de domínio
- **Clientes.Infra**: Acesso a dados e implementação de repositórios

### Front-end
- **cliente-app**: Aplicação Angular com módulo de gerenciamento de clientes
- **Componentes principais**: Lista, Novo, Editar, Excluir e Detalhes

## Arquitetura e Padrões Utilizados

### Domain-Driven Design (DDD)
O projeto utiliza DDD para organizar o código em camadas bem definidas e isolar a lógica de negócio. A estrutura separa claramente:
- **Domínio**: Regras de negócio e validações
- **Aplicação**: Orquestração e fluxo de comando/consultas
- **Infraestrutura**: Acesso a dados e implementações concretas
- **API**: Interface de comunicação com o cliente

### CQRS (Command Query Responsibility Segregation)
Implementado usando o MediatR para separar as operações de leitura (Queries) e escrita (Commands):
- **Commands**: CreateCliente, UpdateCliente, DeleteCliente
- **Queries**: GetAllClientes, GetClienteById

### FluentValidation
Utilizado para implementar validações robustas:
- Validação de CPF/CNPJ
- Validação de idade mínima
- Validação de campos obrigatórios
- Validação de unicidade de e-mail e documento

## Instruções para Execução com Docker

### Pré-requisitos
- Docker
- Docker Compose

### Passos para Execução
1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/TesteDevRommanel.git
   cd TesteDevRommanel
   ```

2. Execute o Docker Compose:
   ```bash
   docker-compose up -d
   ```

3. Acesse a aplicação:
   - Front-end: http://localhost:4200
   - API: http://localhost:8090/swagger

### Serviços do Docker Compose
O arquivo docker-compose.yml na raiz do projeto configura:
- **postgres**: Banco de dados PostgreSQL na porta 5434
- **db-init**: Serviço para inicializar o banco com migrations e dados iniciais
- **api**: API .NET 8 na porta 8090
- **frontend**: Aplicação Angular na porta 4200

## Regras de Negócio Implementadas

1. **Validação de documentos**
   - Apenas um cadastro por CPF/CNPJ
   - Validação de formato e dígitos verificadores

2. **Validação por tipo de pessoa**
   - Pessoa física: idade mínima de 18 anos
   - Pessoa jurídica: obrigatório informar Inscrição Estadual ou marcar como Isento

3. **Validação de unicidade**
   - E-mail único para cada cliente
   - Documento (CPF/CNPJ) único para cada cliente

4. **Validação de campos obrigatórios**
   - Nome/Razão Social
   - Documento
   - Telefone
   - E-mail
   - Campos de endereço

## Tecnologias Utilizadas

### Back-end
- **.NET 8**: Framework moderno com alto desempenho
- **Entity Framework Core**: ORM para acesso ao banco de dados PostgreSQL
- **MediatR**: Implementação do padrão Mediator para CQRS
- **FluentValidation**: Validação de modelos
- **Ardalis.Result**: Padronização de resultados de operações

### Front-end
- **Angular**: Framework para desenvolvimento front-end
- **PrimeNG**: Biblioteca de componentes UI
- **Reactive Forms**: Para formulários e validações
- **RxJS**: Para programação reativa

### Banco de Dados
- **PostgreSQL**: Banco de dados relacional

## Justificativas Técnicas

### Padrão DDD
Escolhido para separar claramente as responsabilidades e proteger a lógica de negócio, permitindo manutenção mais fácil e melhor testabilidade.

### CQRS
Implementado para separar as operações de leitura e escrita, permitindo otimizações específicas para cada tipo de operação e melhor organização do código.

### FluentValidation
Utilizado para criar validações complexas de forma legível e manutenível, como validação de CPF/CNPJ e regras específicas por tipo de cliente.

### Docker Compose
Facilita a configuração e execução do ambiente completo, garantindo que todos os componentes (banco de dados, API e front-end) funcionem integrados sem necessidade de configuração manual.

## Considerações Finais

Este projeto implementa todas as funcionalidades solicitadas, utilizando boas práticas de desenvolvimento e arquitetura. A estrutura foi pensada para ser escalável, testável e de fácil manutenção.

Para aprimoramento futuro, poderia ser considerado:
- Implementação completa de Event Sourcing
- Testes unitários mais abrangentes
- Integração com serviços externos (validação de CEP, por exemplo)

---

## Screenshots (Opcional)

Lista de Clientes:  
![Lista de Clientes]()

Formulário de Cadastro:  
![Formulário]()