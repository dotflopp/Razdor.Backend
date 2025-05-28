FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /Razdor.Backend

COPY . .

WORKDIR /Razdor.Backend/Source/Razdor.StartApp
RUN dotnet publish -c release -o /app

WORKDIR /Razdor.Backend/Source/Razdor.Identity/Razdor.Identity.MigrationService
RUN dotnet publish -c release -o /identity-migrations

FROM mcr.microsoft.com/dotnet/aspnet:9.0

COPY --from=build /app ./app
COPY --from=build /identity-migrations ./identity-migrations

ENTRYPOINT ["dotnet"]