FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AverBot.Core/AverBot.Core.csproj", "AverBot.Core/"]
RUN dotnet restore "AverBot.Core/AverBot.Core.csproj"
COPY . .
WORKDIR "/src/AverBot.Core"
RUN dotnet build "AverBot.Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AverBot.Core.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AverBot.Core.dll"]
