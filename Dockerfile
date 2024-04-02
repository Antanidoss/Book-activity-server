#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["src/BookActivity.Api/BookActivity.Api.csproj", "src/BookActivity.Api/"]
COPY ["src/BookActivity.Api.Common/BookActivity.Api.Common.csproj", "src/BookActivity.Api.Common/"]
COPY ["src/BookActivity.Infrastrucuture.Data/BookActivity.Infrastructure.Data.csproj", "src/BookActivity.Infrastrucuture.Data/"]
COPY ["src/BookActivity.Domain.Core/BookActivity.Domain.Core.csproj", "src/BookActivity.Domain.Core/"]
COPY ["src/BookActivity.Domain/BookActivity.Domain.csproj", "src/BookActivity.Domain/"]
COPY ["src/BookActivity.Shared/BookActivity.Shared.csproj", "src/BookActivity.Shared/"]
COPY ["src/BookActivity.Application/BookActivity.Application.csproj", "src/BookActivity.Application/"]
COPY ["src/BookActivity.Infrastructure/BookActivity.Infrastructure.csproj", "src/BookActivity.Infrastructure/"]
COPY ["src/BookActivity.Initialization/BookActivity.Initialization.csproj", "src/BookActivity.Initialization/"]
RUN dotnet restore "./src/BookActivity.Api/BookActivity.Api.csproj"
COPY . .
WORKDIR "/src/src/BookActivity.Api"
RUN dotnet build "./BookActivity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./BookActivity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookActivity.Api.dll"]