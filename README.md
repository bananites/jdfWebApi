## Documentation

1. Install new webapi project

```bash
dotnet new webapi --use-controllers -o nameOfProject
```

2. Add Mysql.EntityFrameworkCore and Microsoft.EntityFrameworkCore.Design

```bash
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.3
dotnet add package Microsoft.EntityFrameworkCore.Design
```

3. Install new dotnet-ef tool

```bash
dotnet tool install --create-manifest-if-needed dotnet-ef
```

4. Create the DbContext

```bash
dotnet ef dbcontext scaffold "server=server;user=user;password=pwd;database=db" Pomelo.EntityFrameWorkCore.Mysql -o Models
```

## dotnet ef commands

1. Run migration, in this case DbInit

```bash
dotnet ef migrations add DbInit
```

2. To update the Database

```bash
dotnet ef database update
```

## Documentation of dotnet ef core tools
<https://learn.microsoft.com/en-us/ef/core/cli/dotnet>

## Dotnet documentation API

```bash
# trust the dev certs
dotnet dev-certs https --trust

# run the dev server
dotnet run --launch-profile https

# install auto code generation
dotnet tool install dotnet-aspnet-codegenerator

```

### Aspnet-Codegenerator

```bash
dotnet aspnet-codegenerator controller -name TodoItemsController -async -api -m TodoItem -dc TodoContext -outDir Controllers
