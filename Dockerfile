# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/HexaCleanHybArch.Template.Api/HexaCleanHybArch.Template.Api.csproj", "src/HexaCleanHybArch.Template.Api/"]
COPY ["src/HexaCleanHybArch.Template.Config/HexaCleanHybArch.Template.Config.csproj", "src/HexaCleanHybArch.Template.Config/"]
COPY ["src/HexaCleanHybArch.Template.Shared/HexaCleanHybArch.Template.Shared.csproj", "src/HexaCleanHybArch.Template.Shared/"]
COPY ["src/HexaCleanHybArch.Template.Core/HexaCleanHybArch.Template.Core.csproj", "src/HexaCleanHybArch.Template.Core/"]
COPY ["src/Adapters/Auth/Adapters.Auth.csproj", "src/Adapters/Auth/"]
COPY ["src/Adapters/User/Adapters.User.csproj", "src/Adapters/User/"]
COPY ["src/HexaCleanHybArch.Template.Infra/HexaCleanHybArch.Template.Infra.csproj", "src/HexaCleanHybArch.Template.Infra/"]
RUN dotnet restore "./src/HexaCleanHybArch.Template.Api/HexaCleanHybArch.Template.Api.csproj"
COPY . .
WORKDIR "/src/src/HexaCleanHybArch.Template.Api"
RUN dotnet build "./HexaCleanHybArch.Template.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HexaCleanHybArch.Template.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HexaCleanHybArch.Template.Api.dll"]