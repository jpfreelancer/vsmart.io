FROM gcr.io/google-appengine/aspnetcore:2.0
COPY . /app
WORKDIR /app
ENTRYPOINT ["dotnet", "SmartAdmin.Seed.dll"]
