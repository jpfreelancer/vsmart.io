FROM gcr.io/google-appengine/aspnetcore:2.0 AS base
WORKDIR /app

FROM gcr.io/google-appengine/aspnetcore:2.0 AS build
WORKDIR /src
COPY SmartAdmin.Seed.csproj ./
RUN dotnet restore SmartAdmin.Seed.csproj
COPY . .
WORKDIR /src/
RUN dotnet build SmartAdmin.Seed.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SmartAdmin.Seed.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "smartadmin.seed.dll"]
