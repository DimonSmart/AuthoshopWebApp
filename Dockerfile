FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY AutoshopWebApp/*.csproj ./AutoshopWebApp/
RUN dotnet restore

# copy everything else and build app
COPY AutoshopWebApp/. ./AutoshopWebApp/
WORKDIR /app/AutoshopWebApp
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
EXPOSE 80/tcp
WORKDIR /app
COPY --from=build /app/AutoshopWebApp/out ./
ENTRYPOINT ["dotnet", "AutoshopWebApp.dll"]