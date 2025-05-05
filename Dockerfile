FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /Razdor.Backend

COPY . .
RUN dotnet restore

#Miqrations
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

#RUN dotnet ef migrations add --project ./Source/Razdor.Communities/Razdor.Communities.DataAccess.EF/Razdor.Communities.DataAccess.EF.csproj --startup-project ./Source/Razdor.StartApp/Razdor.StartApp.csproj --context Razdor.Communities.DataAccess.EF.RazdorDataContext --configuration Debug Initial --output-dir Migrations
#RUN dotnet ef database update --project ./Source/Razdor.Communities/Razdor.Communities.DataAccess.EF/Razdor.Communities.DataAccess.EF.csproj --startup-project ./Source/Razdor.StartApp/Razdor.StartApp.csproj --context Razdor.Communities.DataAccess.EF.RazdorDataContext --configuration Debug 

RUN dotnet ef migrations add --project Source\Razdor.Identity\Razdor.Identity.Infrastructure\Razdor.Identity.Infrastructure.csproj --startup-project Source\Razdor.StartApp\Razdor.StartApp.csproj --context Razdor.Identity.Infrastructure.DataAccess.Sql.IdentitySqlliteDbContext Initial --output-dir Migrations
RUN dotnet ef database update --project Source\Razdor.Identity\Razdor.Identity.Infrastructure\Razdor.Identity.Infrastructure.csproj --startup-project Source\Razdor.StartApp\Razdor.StartApp.csproj --context Razdor.Identity.Infrastructure.DataAccess.Sql.IdentitySqlliteDbContext


WORKDIR /Razdor.Backend/Source/Razdor.StartApp

RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app ./
COPY --from=build /Razdor.Backend/Source/Razdor.StartApp/**.db ./

ENTRYPOINT ["dotnet", "Razdor.StartApp.dll"]