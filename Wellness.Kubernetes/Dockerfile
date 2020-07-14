FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Wellness.Kubernetes/Wellness.Kubernetes.csproj", "Wellness.Kubernetes/"]

RUN dotnet restore "Wellness.Kubernetes/Wellness.Kubernetes.csproj"
COPY . .
WORKDIR "/src/Wellness.Kubernetes"
RUN dotnet build "Wellness.Kubernetes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wellness.Kubernetes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wellness.Kubernetes.dll"]