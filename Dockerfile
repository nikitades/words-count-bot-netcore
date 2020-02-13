FROM mcr.microsoft.com/dotnet/core-nightly/sdk:3.0.100-rc1-alpine3.10 AS build-env
WORKDIR /image

COPY ./TelegramBot.Core/*.csproj ./
RUN dotnet restore

COPY ./TelegramBot.Core/* ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core-nightly/aspnet:3.0.0-rc1-alpine3.10
WORKDIR /image
COPY --from=build-env /image/out .
ENTRYPOINT [ "dotnet", "TelegramBot.Core.dll" ]