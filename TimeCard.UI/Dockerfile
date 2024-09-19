FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 9080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TimeCard.UI/TimeCard.UI.csproj", "TimeCard.UI/"]
COPY ["TimeCard.Service/TimeCard.Service.csproj", "TimeCard.Service/"]
COPY ["TimeCard.Domain/TimeCard.Domain.csproj", "TimeCard.Domain/"]
RUN dotnet restore "./TimeCard.UI/TimeCard.UI.csproj"
COPY . .
WORKDIR "/src/TimeCard.UI"
RUN dotnet build "./TimeCard.UI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TimeCard.UI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TimeCard.UI.dll"]