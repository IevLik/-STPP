# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY Leftovers/Leftovers/*.csproj .
RUN dotnet restore -r linux-musl-arm64

# copy everything else and build app
COPY Leftovers/Leftovers/. .
RUN dotnet publish -c release -o /app -r linux-musl-arm64 --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine-arm64v8
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["./Leftovers"]
