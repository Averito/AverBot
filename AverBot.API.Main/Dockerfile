FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AverBot.API/AverBot.API.csproj", "AverBot.API/"]
RUN dotnet restore "AverBot.API/AverBot.API.csproj"
COPY . .
WORKDIR "/src/AverBot.API"
RUN dotnet build "AverBot.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AverBot.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AverBot.API.dll"]
