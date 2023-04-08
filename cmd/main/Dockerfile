FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["cmd/main/main.csproj", "cmd/main/"]
RUN dotnet restore "cmd/main/main.csproj"
COPY . .
WORKDIR "/src/cmd/main"
RUN dotnet build "main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "main.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "main.dll"]
