# PhoneTopUp

## **Technologies**

- ASP.NET Core 8
- Entity Framework Core 8
- Unit & Integration Tests + xUnit + NSubstitute + FluentAssertions + Autofixture
- Polly
- Mapster
- FluentValidator
- MediatR
- Swagger UI
- HealthChecks
- Postgres
- MassTransit + RabbitMQ
- Docker & Docker Compose

## Running the application

After cloning the repository to the desired folder, run the command in the terminal at the root of the project:

```csharp
dotnet clean && dotnet build
```

Next step, run the command in the terminal:

```csharp
docker-compose up --build
```

Now open BankAccount API in the browser:

```csharp
http://localhost:7001/swagger/
```

You can see its health check on this url:

```csharp
http://localhost:7001/health/
```

And open TopUp API in the browser:

```csharp
http://localhost:7002/swagger/
```

You can see its health check on this url:

```csharp
http://localhost:7002/health/
```
