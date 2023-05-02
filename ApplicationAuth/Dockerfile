#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 1312

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ApplicationAuth/ApplicationAuth.csproj", "ApplicationAuth/"]
COPY ["ApplicationAuth.Common/ApplicationAuth.Common.csproj", "ApplicationAuth.Common/"]
COPY ["ApplicationAuth.DAL/ApplicationAuth.DAL.csproj", "ApplicationAuth.DAL/"]
COPY ["ApplicationAuth.Domain/ApplicationAuth.Domain.csproj", "ApplicationAuth.Domain/"]
COPY ["ApplicationAuth.Models/ApplicationAuth.Models.csproj", "ApplicationAuth.Models/"]
COPY ["ApplicationAuth.ResourceLibrary/ApplicationAuth.ResourceLibrary.csproj", "ApplicationAuth.ResourceLibrary/"]
COPY ["ApplicationAuth.Services/ApplicationAuth.Services.csproj", "ApplicationAuth.Services/"]
RUN dotnet restore "ApplicationAuth/ApplicationAuth.csproj"
COPY . .
WORKDIR "/src/ApplicationAuth"
RUN dotnet build "ApplicationAuth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApplicationAuth.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ApplicationAuth/database.db /app
COPY ApplicationAuth/wwwroot/Resources/RoadmapImages /app/wwwroot/Resources/RoadmapImages
COPY bin/Debug/ApplicationAuth.xml /app
ENTRYPOINT ["dotnet", "ApplicationAuth.dll"]