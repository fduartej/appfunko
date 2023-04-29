## Funko App

## App con Autentificacion y Autorizacion (Login)

dotnet new mvc --auth Individual

## Migration

dotnet ef migrations add InitialMigration --context appfunko.Data.ApplicationDbContext -o "C:\Users\Inteligo\Code\netcore\usmp\2023\appfunko\Data\Migrations"

dotnet tool update --global dotnet-ef --version 7.0.3

dotnet ef database update

## Generacion del Codigo Login y Registrar Cuenta de Identity para modificarlo

dotnet aspnet-codegenerator identity -dc appfunko.Data.ApplicationDbContext --files "Account.Register;Account.Login"