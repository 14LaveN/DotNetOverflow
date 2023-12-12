FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DotNetOverflow.ImageAPI/DotNetOverflow.ImageAPI.csproj", "DotNetOverflow.ImageAPI/"]
RUN dotnet restore "DotNetOverflow.ImageAPI/DotNetOverflow.ImageAPI.csproj"
COPY . .
WORKDIR "/src/DotNetOverflow.ImageAPI"
RUN dotnet build "DotNetOverflow.ImageAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetOverflow.ImageAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetOverflow.ImageAPI.dll"]
