FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DotNetOverflow.CommentAPI/DotNetOverflow.CommentAPI.csproj", "DotNetOverflow.CommentAPI/"]
RUN dotnet restore "DotNetOverflow.CommentAPI/DotNetOverflow.CommentAPI.csproj"
COPY . .
WORKDIR "/src/DotNetOverflow.CommentAPI"
RUN dotnet build "DotNetOverflow.CommentAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetOverflow.CommentAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetOverflow.CommentAPI.dll"]
