FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DbSeeder/DbSeeder.csproj", "DbSeeder/"]
RUN dotnet restore "DbSeeder/DbSeeder.csproj"
COPY . .
WORKDIR "/src/DbSeeder"
RUN dotnet build "DbSeeder.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DbSeeder.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DbSeeder.dll"]
