FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ReportingWith Queue/ReportingWith Queue.csproj", "ReportingWith Queue/"]
RUN dotnet restore "ReportingWith Queue/ReportingWith Queue.csproj"
COPY . .
WORKDIR "/src/ReportingWith Queue"
RUN dotnet build "ReportingWith Queue.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportingWith Queue.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportingWith Queue.dll"]
