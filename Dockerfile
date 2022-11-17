# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0-windowsservercore-ltsc2022 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY Leftovers/Leftovers/*.csproj .
RUN dotnet restore -r win-x64

# copy everything else and build app
COPY Leftovers/Leftovers/. .
RUN dotnet publish -c Release -o /app -r win-x64 --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0-windowsservercore-ltsc2022 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["Leftovers"]
