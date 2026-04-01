# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy everything and restore dependencies
COPY . .
RUN dotnet restore

# Build and publish the app
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy the build output from the previous stage
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5150

# Expose port (adjust if needed)
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "Api.dll"]
