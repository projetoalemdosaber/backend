#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV ASPNETCORE_URLS http://*:80
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RedeSocial.csproj", "."]
RUN dotnet restore "./RedeSocial.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RedeSocial.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedeSocial.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedeSocial.dll"]