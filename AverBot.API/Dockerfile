FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AverBot.API.Main/AverBot.API.Main.csproj", "AverBot.API.Main/"]
RUN dotnet restore "AverBot.API.Main/AverBot.API.Main.csproj"
COPY . .
WORKDIR "/src/AverBot.API.Main"
RUN dotnet build "AverBot.API.Main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AverBot.API.Main.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AverBot.API.Main.dll"]
