FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 64756

# Set ASP.NET Core environment variables
#ENV ASPNETCORE_URLS="http://*:64756"
#ENV ASPNETCORE_ENVIRONMENT="Production"

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY SpotifyListener.sln ./
COPY Partify.Web/Partify.Web.csproj Partify.Web/
COPY Partify.Server/Partify.Server.csproj Partify.Server/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/Partify.Web
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Partify.Web.dll"]
