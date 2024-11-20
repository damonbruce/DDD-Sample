# Use the official .NET image as the build environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8888

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["demo-api.csproj", "./"]
RUN dotnet restore "demo-api.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "demo-api.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "demo-api.csproj" -c Release -o /app/publish

# Final stage: Copy build artifacts and run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "demo-api.dll"]