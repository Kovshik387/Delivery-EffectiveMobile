FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Delivery.Systems.DeliveryAPI/Delivery.Systems.DeliveryAPI.csproj", "Systems/Delivery.Systems.DeliveryAPI/"]
RUN dotnet restore "Systems/Delivery.Systems.DeliveryAPI/Delivery.Systems.DeliveryAPI.csproj"
COPY . .
WORKDIR "/src/Systems/Delivery.Systems.DeliveryAPI"
RUN dotnet build "Delivery.Systems.DeliveryAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Delivery.Systems.DeliveryAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Delivery.Systems.DeliveryAPI.dll"]
