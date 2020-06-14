FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
COPY . ./
RUN dotnet build

FROM build AS unit_test
WORKDIR /app/Urly.UnitTests
RUN dotnet test --logger:trx

FROM build AS integration_test
WORKDIR /app/Urly.IntegrationTests
RUN dotnet test --logger:trx

FROM build AS publish
WORKDIR /app/Urly.WebApi
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=publish /app/Urly.WebApi/out .
ENTRYPOINT ["dotnet", "Urly.WebApi.dll"]
