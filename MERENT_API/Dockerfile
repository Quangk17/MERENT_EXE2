#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MERENT_API/MERENT_API.csproj", "MERENT_API/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructures/Infrastructures.csproj", "Infrastructures/"]
COPY ["MERENT/Application.csproj", "MERENT/"]
RUN dotnet restore "./MERENT_API/MERENT_API.csproj"
COPY . .
WORKDIR "/src/MERENT_API"
RUN dotnet build "./MERENT_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MERENT_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MERENT_API.dll"]