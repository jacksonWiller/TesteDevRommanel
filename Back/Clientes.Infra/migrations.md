Collecting workspace informationFiltering to most relevant information# Script para Gerenciamento de Migrações do Entity Framework Core

```bash
# Navegue até o diretório do projeto API
cd C:\Users\jacks\Desktop\TesteDevRommanel\Back\Cliente.Api

# Reverter todas as migrações
dotnet ef database update 0 --project "..\Clientes.Infra\Clientes.Infra.csproj" --startup-project "Clientes.Api.csproj" --context Clientes.Infra.Contexto.ClienteContext --configuration Debug

# Reaplicar as migrações existentes
dotnet ef database update --project "..\Clientes.Infra\Clientes.Infra.csproj" --startup-project "Clientes.Api.csproj" --context Clientes.Infra.Contexto.ClienteContext --configuration Debug
```

Para remover todas as migrações e criar uma nova:

```bash
# Remova todas as migrações
dotnet ef migrations remove --project "..\Clientes.Infra\Clientes.Infra.csproj" --startup-project "Clientes.Api.csproj" --context Clientes.Infra.Contexto.ClienteContext --configuration Debug

# Adicione uma nova migração inicial
dotnet ef migrations add Initial --project "..\Clientes.Infra\Clientes.Infra.csproj" --startup-project "Clientes.Api.csproj" --context Clientes.Infra.Contexto.ClienteContext --configuration Debug

# Atualize o banco de dados
dotnet ef database update --project "..\Clientes.Infra\Clientes.Infra.csproj" --startup-project "Clientes.Api.csproj" --context Clientes.Infra.Contexto.ClienteContext --configuration Debug
```

Nota: Certifique-se de que a ferramenta `dotnet ef` esteja instalada. Se não estiver, você pode instalá-la usando:

```bash
dotnet tool install --global dotnet-ef
```

Também certifique-se de que o Microsoft.EntityFrameworkCore.Design está instalado no projeto de inicialização.