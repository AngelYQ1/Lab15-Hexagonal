FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Lab11-AngelYucra/Lab11-AngelYucra.csproj", "Lab11-AngelYucra/"]
COPY ["Lab11-AngelYucra.Application/Lab11-AngelYucra.Application.csproj", "Lab11-AngelYucra.Application/"]
COPY ["Lab11-AngelYucra.Domain/Lab11-AngelYucra.Domain.csproj", "Lab11-AngelYucra.Domain/"]
COPY ["Lab11-AngelYucra.Infrastructure/Lab11-AngelYucra.Infrastructure.csproj", "Lab11-AngelYucra.Infrastructure/"]

RUN dotnet restore "Lab11-AngelYucra/Lab11-AngelYucra.csproj"

COPY . .
WORKDIR "/src/Lab11-AngelYucra"
RUN dotnet build "Lab11-AngelYucra.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lab11-AngelYucra.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab11-AngelYucra.dll"]
