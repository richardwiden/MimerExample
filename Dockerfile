#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
RUN mkdir -p /root/.config/MimerSQLDataProvider
RUN ln -s /app/MimerTrace.config ~/.config/MimerSQLDataProvider/MimerTrace.config
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["MimerExample.csproj", "."]
RUN dotnet restore "./MimerExample.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MimerExample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MimerExample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MimerExample.dll"]