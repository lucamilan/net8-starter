# Build and Ship with Development Certificates

Just a sample

## Docker NET8 Chiseled image

mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled

## Before

```bash
dotnet dev-certs https --clean
dotnet dev-certs https -ep $env:USERPROFILE/.aspnet/https/aspnetapp.pfx -p mysecret
dotnet dev-certs https --check --trust

docker-compose up
```
