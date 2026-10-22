# syntax=docker/dockerfile:1
FROM node:24-bookworm-slim AS frontend
WORKDIR /src/frontend
COPY frontend/package.json frontend/package-lock.json ./
RUN npm ci --no-audit --no-fund
COPY frontend/ ./
# Origem relativa fixa no build de produção; nenhum segredo é argumento de build.
ENV VITE_API_BASE_URL=""
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS backend
WORKDIR /src
COPY backend/Sesi.Catalogo.Api.csproj backend/
RUN dotnet restore backend/Sesi.Catalogo.Api.csproj -p:ProductionImage=true
COPY backend/ backend/
RUN dotnet publish backend/Sesi.Catalogo.Api.csproj -c Release --no-restore -o /publish -p:UseAppHost=false -p:ProductionImage=true

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=backend /publish ./
COPY --from=frontend /src/frontend/dist ./wwwroot
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080
USER $APP_UID
# PORT é validada pela aplicação. Render faz GET /health na porta configurada.
ENTRYPOINT ["dotnet", "Sesi.Catalogo.Api.dll"]
