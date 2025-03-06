# EFCoreEnvironmentBasedApi

This project demonstrates how to configure **Entity Framework Core** with **environment-based database connections** in an ASP.NET Core Web API application. The API can be configured to use different databases based on the environment (e.g., Development, Production).

### Configuration Files
The project uses **different configuration files** to manage the connection strings for different environments:

- **`appsettings.json`**: Contains common settings for all environments.
- **`appsettings.Development.json`**: Contains settings specifically for the **Development** environment.
- **`appsettings.Production.json`**: Contains settings for the **Production** environment.

Each of these configuration files contains a **connection string** used by the application to connect to the database.

### Environment Variables

The project uses **environment variables** to differentiate between Development, Production, or any other environment configurations. The connection string is selected based on the current environment.

### Configure Environment-Specific Settings

The application reads the correct configuration file based on the **current environment**. You can specify the environment using the `ASPNETCORE_ENVIRONMENT` environment variable:

- For **Development** environment:
  ```bash
  set ASPNETCORE_ENVIRONMENT=Development
  ```

- For **Production** environment:
  ```bash
  set ASPNETCORE_ENVIRONMENT=Production
  ```

### Apply Migrations

Once the environment is set and the connection strings are configured, you can create and apply **migrations** to the database.

1. Create a migration:
   ```bash
   dotnet ef migrations add InitialCreate
   ```

2. Apply the migration to the database:
   ```bash
   dotnet ef database update
   ```

This will apply the migrations to the appropriate database based on the environment configuration (Development or Production).

