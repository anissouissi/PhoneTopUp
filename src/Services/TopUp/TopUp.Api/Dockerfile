FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["src/Services/TopUp/TopUp.Api/TopUp.Api.csproj", "src/Services/TopUp/TopUp.Api/"]
COPY ["src/Services/TopUp/TopUp.Application/TopUp.Application.csproj", "src/Services/TopUp/TopUp.Application/"]
COPY ["src/BuildingBlocks/BuildingBlocks.csproj", "src/BuildingBlocks/"]
COPY ["src/Services/TopUp/TopUp.Domain/TopUp.Domain.csproj", "src/Services/TopUp/TopUp.Domain/"]
COPY ["src/Services/TopUp/TopUp.Infrastructure/TopUp.Infrastructure.csproj", "src/Services/TopUp/TopUp.Infrastructure/"]
RUN dotnet restore "src/Services/TopUp/TopUp.Api/TopUp.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/TopUp/TopUp.Api"
RUN dotnet build "TopUp.Api.csproj" --configuration Release --no-restore --nologo --output /app/build 

FROM build AS publish
RUN dotnet publish "TopUp.Api.csproj" --configuration Release --no-restore --nologo --output /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TopUp.Api.dll"]
