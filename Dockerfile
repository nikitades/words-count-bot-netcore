FROM mcr.microsoft.com/dotnet/core/sdk:3.1.101-alpine AS build-env
WORKDIR /image

COPY ./TelegramBot.Core/*.csproj ./
RUN dotnet restore

COPY ./TelegramBot.Core/* ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.101-alpine
WORKDIR /image
COPY --from=build-env /image/out .
EXPOSE 5000
ENTRYPOINT [ "dotnet", "TelegramBot.Core.dll" ]