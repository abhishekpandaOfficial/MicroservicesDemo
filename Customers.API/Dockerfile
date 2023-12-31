#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Customers.API/Customers.API.csproj", "Customers.API/"]
RUN dotnet restore "Customers.API/Customers.API.csproj"
COPY . .
WORKDIR "/src/Customers.API"
RUN dotnet build "Customers.API.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS migration
WORKDIR /src
COPY . .
RUN dotnet restore "Customers.API.Migration/Customers.API.Migration.csproj"
COPY . .
WORKDIR "/src/Customers.API.Migration"
RUN dotnet build "Customers.API/Customers.API.Migration.csproj" -c Release -o /app/migration

FROM build AS publish
RUN dotnet publish "Customers.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Customers.API.dll"]

FROM base AS final
WORKDIR /migration
COPY --from=migration /app/migration .

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customers.API.dll"]