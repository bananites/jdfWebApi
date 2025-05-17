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

```bash
# Run migration, in this case DbInit
dotnet ef migrations add DbInit

# To update the Database
dotnet ef database update

#drop database
dotnet ef database drop

```

### Documentation of dotnet ef core tools

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
# generate Controller
dotnet aspnet-codegenerator controller -name UserController -async -api -m User -dc Ts31JdfMachineHandlerContext -outDir Controllers

```
