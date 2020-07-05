#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Wellness.Api/Wellness.Api.csproj", "Wellness.Api/"]
RUN dotnet restore "Wellness.Api/Wellness.Api.csproj"
COPY . .
WORKDIR "/src/Wellness.Api"
RUN dotnet build "Wellness.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wellness.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wellness.Api.dll"]