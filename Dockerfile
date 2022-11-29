FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiDafi.csproj", "./"]
RUN dotnet restore "ApiDafi.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "ApiDafi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiDafi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiDafi.dll"]
