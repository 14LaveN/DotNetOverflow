FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7004
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BigSocietyProgrammers.ArticleAPI/BigSocietyProgrammers.ArticleAPI.csproj", "BigSocietyProgrammers.ArticleAPI/"]
RUN dotnet restore "BigSocietyProgrammers.ArticleAPI/BigSocietyProgrammers.ArticleAPI.csproj"
COPY . .
WORKDIR "/src/BigSocietyProgrammers.ArticleAPI"
RUN dotnet build "BigSocietyProgrammers.ArticleAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BigSocietyProgrammers.ArticleAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BigSocietyProgrammers.ArticleAPI.dll"]
