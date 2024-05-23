FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["src/Services/BankAccount/BankAccount.Api/BankAccount.Api.csproj", "src/Services/BankAccount/BankAccount.Api/"]
COPY ["src/Services/BankAccount/BankAccount.Application/BankAccount.Application.csproj", "src/Services/BankAccount/BankAccount.Application/"]
COPY ["src/BuildingBlocks/BuildingBlocks.csproj", "src/BuildingBlocks/"]
COPY ["src/Services/BankAccount/BankAccount.Domain/BankAccount.Domain.csproj", "src/Services/BankAccount/BankAccount.Domain/"]
COPY ["src/Services/BankAccount/BankAccount.Infrastructure/BankAccount.Infrastructure.csproj", "src/Services/BankAccount/BankAccount.Infrastructure/"]
RUN dotnet restore "src/Services/BankAccount/BankAccount.Api/BankAccount.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/BankAccount/BankAccount.Api"
RUN dotnet build "BankAccount.Api.csproj" --configuration Release --no-restore --nologo --output /app/build 

FROM build AS publish
RUN dotnet publish "BankAccount.Api.csproj" --configuration Release --no-restore --nologo --output /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankAccount.Api.dll"]
