#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Valhalla.App/Valhalla.App.csproj", "Valhalla.App/"]
COPY ["Valhalla.Domain/Valhalla.Domain.csproj", "Valhalla.Domain/"]
COPY ["Valhalla.Infraestrutura/Valhalla.Infraestrutura.csproj", "Valhalla.Infraestrutura/"]
RUN dotnet restore "Valhalla.App/Valhalla.App.csproj"
COPY . .
WORKDIR "/src/Valhalla.App"
RUN dotnet build "Valhalla.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Valhalla.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Valhalla.App.dll"]
