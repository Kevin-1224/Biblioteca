FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Biblioteca.Api/Biblioteca.Api.csproj", "Biblioteca.Api/"]
RUN dotnet restore "Biblioteca.Api/Biblioteca.Api.csproj"
COPY . .


WORKDIR "/src/Biblioteca.Api"

RUN dotnet build "Biblioteca.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Biblioteca.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Biblioteca.Api.dll"]