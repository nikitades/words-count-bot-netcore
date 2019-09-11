FROM mcr.microsoft.com/dotnet/core-nightly/sdk:3.0.100-rc1-alpine3.10 AS build-env
WORKDIR /image

COPY ./*.csproj ./
RUN dotnet restore

COPY ./* ./
RUN dotnet publish -c Release -o out
RUN cat appsettings.Development.json

FROM mcr.microsoft.com/dotnet/core-nightly/aspnet:3.0.0-rc1-alpine3.10
WORKDIR /image
COPY --from=build-env /image/out .
RUN cat appsettings.Development.json
ENTRYPOINT [ "dotnet", "WordsCountBot.dll" ]