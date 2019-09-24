FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS build
WORKDIR /src
COPY ["HomeLink.Server/HomeLink.Server.csproj", "HomeLink.Server/"]
RUN dotnet restore "HomeLink.Server/HomeLink.Server.csproj"
COPY . .
WORKDIR "/src/HomeLink.Server"
RUN dotnet build "HomeLink.Server.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HomeLink.Server.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HomeLink.Server.dll"]