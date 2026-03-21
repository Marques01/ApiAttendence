# =============================================================
# Stage 1 — base runtime
# =============================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 9090
EXPOSE 9091

# =============================================================
# Stage 2 — build
# =============================================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os csproj e restaura dependências (cache de camadas)
COPY ["src/API/API.csproj",                       "src/API/"]
COPY ["src/Application/Application.csproj",       "src/Application/"]
COPY ["src/Domain/Domain.csproj",                 "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]

RUN dotnet restore "src/Infrastructure/Infrastructure.csproj"
RUN dotnet restore "src/API/API.csproj"

# Copia o restante do código
COPY . .

# Compila
WORKDIR "/src/src/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

# =============================================================
# Stage 3 — testes (opcional, removível se não quiser no CI)
# =============================================================
FROM build AS test
WORKDIR "/src/src/UnitaryTests"
COPY ["src/UnitaryTests/UnitaryTests.csproj", "src/UnitaryTests/"]
RUN dotnet restore "src/UnitaryTests/UnitaryTests.csproj"
COPY . .
RUN dotnet test "UnitaryTests.csproj" --no-restore --verbosity normal

# =============================================================
# Stage 4 — publish
# =============================================================
FROM build AS publish
WORKDIR "/src/src/API"
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# =============================================================
# Stage 5 — imagem final
# =============================================================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]