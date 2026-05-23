FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["BookStore.Api/BookStore.Api.csproj", "./"]
RUN dotnet restore

COPY . .
RUN dotnet build "BookStore.Api/BookStore.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookStore.Api/BookStore.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "BookStore.Api.dll"]