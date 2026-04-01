# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy everything and restore dependencies
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Configure the port the app will run on (default: 5151)
ENV ASPNETCORE_URLS=http://+:5151
EXPOSE 5151

ENTRYPOINT ["dotnet", "Frontend.dll"]
