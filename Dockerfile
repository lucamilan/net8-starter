# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining source code
COPY . .

# Build the application
RUN dotnet publish -o out --os linux --arch x64 -c Release

# Use the official .NET runtime image as the base image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled

# Set the working directory inside the container
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/out .

# Expose the port the app will run on
EXPOSE 80
USER $APP_UID 
# Set the entry point for the application
ENTRYPOINT ["dotnet", "sample.dll"]
