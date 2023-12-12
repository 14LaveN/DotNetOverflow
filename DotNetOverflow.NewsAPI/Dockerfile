FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DotNetOverflow.NewsAPI/DotNetOverflow.NewsAPI.csproj", "DotNetOverflow.NewsAPI/"]
RUN dotnet restore "DotNetOverflow.NewsAPI/DotNetOverflow.NewsAPI.csproj"
COPY . .
WORKDIR "/src/DotNetOverflow.NewsAPI"
RUN dotnet build "DotNetOverflow.NewsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetOverflow.NewsAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetOverflow.NewsAPI.dll"]
