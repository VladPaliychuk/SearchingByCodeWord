FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SBCW.API/SBCW.API.csproj", "SBCW.API/"]
COPY ["SBCW.DAL/SBCW.DAL.csproj", "SBCW.DAL/"]
COPY ["SBCW.BLL/SBCW.BLL.csproj", "SBCW.BLL/"]
RUN dotnet restore "SBCW.API/SBCW.API.csproj"
COPY . .
WORKDIR "/src/SBCW.API"
RUN dotnet build "SBCW.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SBCW.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SBCW.API.dll"]
