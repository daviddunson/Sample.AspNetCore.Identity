# Adding Migrations

For more information, see [EF Quickstart](https://identityserver4.readthedocs.io/en/latest/quickstarts/5_entityframework.html#refentityframeworkquickstart).

```
dotnet ef migrations add InitialCreate -c ApplicationIdentityDbContext -o Data/Migrations/IdentityDb
dotnet ef migrations add InitialCreate -c PersistedGrantDbContext -o Data/Migrations/PersistedGrantDb
dotnet ef migrations add InitialCreate -c ConfigurationDbContext -o Data/Migrations/ConfigurationDb
```

# Initialize Database

```
dotnet ef database update -c ApplicationIdentityDbContext
dotnet ef database update -c PersistedGrantDbContext
dotnet ef database update -c ConfigurationDbContext
```
