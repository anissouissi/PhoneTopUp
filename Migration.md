cd src/Services/TopUp/TopUp.Infrastructure

dotnet tool install dotnet-ef -g

dotnet ef migrations add Initial --help

dotnet ef migrations add Initial -s ../TopUp.Api/TopUp.Api.csproj

dotnet ef migrations remove -s ../TopUp.Api/TopUp.Api.csproj

dotnet ef database update -s ../TopUp.Api/TopUp.Api.csproj

---

cd src/Services/BankAccount/BankAccount.Infrastructure

dotnet tool install dotnet-ef -g

dotnet ef migrations add Initial --help

dotnet ef migrations add Initial -s ../BankAccount.Api/BankAccount.Api.csproj

dotnet ef migrations remove -s ../BankAccount.Api/BankAccount.Api.csproj

dotnet ef database update -s ../BankAccount.Api/BankAccount.Api.csproj
