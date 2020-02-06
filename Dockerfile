FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY /GullrekkaSlackbot/GullrekkaSlackbot.csproj GullrekkaSlackbot.csproj
RUN dotnet restore

# Copy everything else
COPY /GullrekkaSlackbot/ .

# Publish
RUN dotnet publish -c Release -o /app/out/

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /gullrekka
COPY --from=build-env /app/out/ . 
