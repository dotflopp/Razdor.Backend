FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /Razdor.Backend

COPY . .
RUN dotnet restore

#Miqrations
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet ef migrations add --project ./Source/Razdor.Communities/Razdor.Communities.DataAccess.EF --startup-project ./Source/Razdor.StartApp --context Razdor.Communities.DataAccess.RazdorDataContext --configuration Debug Initial --output-dir Migrations
RUN dotnet ef database update --project ./Source/Razdor.Communities/Razdor.Communities.DataAccess.EF --startup-project ./Source/Razdor.StartApp --context Razdor.Communities.DataAccess.RazdorDataContext --configuration Debug 

RUN dotnet ef migrations add --project ./Source/Razdor.Identity/Razdor.Identity.DataAccess.EF --startup-project ./Source/Razdor.StartApp --context Razdor.Identity.DataAccess.IdentityDbContext --configuration Debug Initial --output-dir Migrations
RUN dotnet ef database update --project ./Source/Razdor.Identity/Razdor.Identity.DataAccess.EF --startup-project ./Source/Razdor.StartApp --context Razdor.Identity.DataAccess.IdentityDbContext --configuration Debug 


WORKDIR /Razdor.Backend/Source/Razdor.StartApp

RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app ./
COPY --from=build /Razdor.Backend/Source/Razdor.StartApp/razdor.db ./

ENTRYPOINT ["dotnet", "Razdor.StartApp.dll"]