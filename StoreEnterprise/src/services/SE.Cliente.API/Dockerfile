#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/services/SE.Cliente.API/SE.Cliente.API.csproj", "src/services/SE.Cliente.API/"]
RUN dotnet restore "src/services/SE.Cliente.API/SE.Cliente.API.csproj"
COPY . .
WORKDIR "/src/src/services/SE.Cliente.API"
RUN dotnet build "SE.Cliente.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SE.Cliente.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SE.Cliente.API.dll"]