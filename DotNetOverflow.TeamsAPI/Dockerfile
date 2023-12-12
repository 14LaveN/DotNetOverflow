FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DotNetOverflow.TeamsAPI/DotNetOverflow.TeamsAPI.csproj", "DotNetOverflow.TeamsAPI/"]
RUN dotnet restore "DotNetOverflow.TeamsAPI/DotNetOverflow.TeamsAPI.csproj"
COPY . .
WORKDIR "/src/DotNetOverflow.TeamsAPI"
RUN dotnet build "DotNetOverflow.TeamsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetOverflow.TeamsAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetOverflow.TeamsAPI.dll"]
