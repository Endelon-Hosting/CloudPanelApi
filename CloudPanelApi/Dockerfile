FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CloudPanelApi/CloudPanelApi.csproj", "CloudPanelApi/"]
RUN dotnet restore "CloudPanelApi/CloudPanelApi.csproj"
COPY . .
WORKDIR "/src/CloudPanelApi"
RUN dotnet build "CloudPanelApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CloudPanelApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudPanelApi.dll"]
