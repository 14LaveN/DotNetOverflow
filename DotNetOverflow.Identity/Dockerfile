FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7004
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DotNetOverflow.Identity/DotNetOverflow.Identity.csproj", "DotNetOverflow.Identity/"]
COPY ["DotNetOverflow.Identity.DAL/DotNetOverflow.Identity.DAL.csproj", "DotNetOverflow.Identity.DAL/"]
COPY ["DotNetOverflow.Core/DotNetOverflow.Core.csproj", "DotNetOverflow.Core/"]
COPY ["DotNetOverflow.RabbitMq/DotNetOverflow.RabbitMq.csproj", "DotNetOverflow.RabbitMq/"]
COPY ["DotNetOverflow.Email/DotNetOverflow.Email.csproj", "DotNetOverflow.Email/"]
COPY ["DotNetOverflow.QuartZ/DotNetOverflow.QuartZ.csproj", "DotNetOverflow.QuartZ/"]
RUN dotnet restore "DotNetOverflow.Identity/DotNetOverflow.Identity.csproj"
COPY . .
WORKDIR "/src/DotNetOverflow.Identity"
RUN dotnet build "DotNetOverflow.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetOverflow.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetOverflow.Identity.dll"]
