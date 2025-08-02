# 1. Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy sln and csproj files first for restore
COPY TrainComponentManagement.sln ./
COPY TrainComponentManagement.Domain/*.csproj TrainComponentManagement.Domain/
COPY TrainComponentManagement.Application/*.csproj TrainComponentManagement.Application/
COPY TrainComponentManagement.Infrastructure/*.csproj TrainComponentManagement.Infrastructure/
COPY TrainComponentManagement.Api/*.csproj TrainComponentManagement.Api/

# Restore via solution
RUN dotnet restore TrainComponentManagement.sln

# Copy all source
COPY . .

# Build and publish
WORKDIR /src/TrainComponentManagement.Api
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "TrainComponentManagement.Api.dll"]
