FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Aktitic.HrProject.Api/Aktitic.HrProject.Api.csproj", "Aktitic.HrProject.Api/"]
COPY ["Aktitic.HrProject.BL/Aktitic.HrProject.BL.csproj", "Aktitic.HrProject.BL/"]
COPY ["Aktitic.HrProject.DAL/Aktitic.HrProject.DAL.csproj", "Aktitic.HrProject.DAL/"]
RUN dotnet restore "Aktitic.HrProject.Api/Aktitic.HrProject.Api.csproj"
COPY . .
WORKDIR "/src/Aktitic.HrProject.Api"
RUN dotnet build "Aktitic.HrProject.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aktitic.HrProject.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aktitic.HrProject.Api.dll"]
