FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DotNetOverflow.QuestionAPI/DotNetOverflow.QuestionAPI.csproj", "DotNetOverflow.QuestionAPI/"]
RUN dotnet restore "DotNetOverflow.QuestionAPI/DotNetOverflow.QuestionAPI.csproj"
COPY . .
WORKDIR "/src/DotNetOverflow.QuestionAPI"
RUN dotnet build "DotNetOverflow.QuestionAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetOverflow.QuestionAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetOverflow.QuestionAPI.dll"]
