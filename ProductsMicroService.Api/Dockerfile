FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProductsMicroService.Api/ProductsMicroService.Api.csproj", "ProductsMicroService.Api/"]
COPY ["ProductsMicroService.Application/ProductsMicroService.Application.csproj", "ProductsMicroService.Application/"]
COPY ["ProductsMicroService.Domain/ProductsMicroService.Domain.csproj", "ProductsMicroService.Domain/"]
COPY ["ProductsMicroService.Infrastructure/ProductsMicroService.Infrastructure.csproj", "ProductsMicroService.Infrastructure/"]
RUN dotnet restore "ProductsMicroService.Api/ProductsMicroService.Api.csproj"
COPY . .
WORKDIR "/src/ProductsMicroService.Api"
RUN dotnet build "./ProductsMicroService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductsMicroService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductsMicroService.Api.dll"]
