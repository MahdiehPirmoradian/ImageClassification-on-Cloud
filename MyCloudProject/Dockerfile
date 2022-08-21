#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN apt-get update
RUN apt-get install -y libfreetype6
RUN apt-get install -y libfontconfig1

WORKDIR /src
COPY ["MyCloudProject/Nugets", "/root/.nuget/packages"]

COPY ["MyCloudProject/MyCloudProject.csproj", "MyCloudProject/"]
COPY ["MyCloudProject.Common/MyCloudProject.Common.csproj", "MyCloudProject.Common/"]
COPY ["MyExperiment/MyExperiment.csproj", "MyExperiment/"]
RUN dotnet nuget add source /root/.nuget/packages
RUN dotnet restore "MyCloudProject/MyCloudProject.csproj"
COPY . .
WORKDIR "/src/MyCloudProject"
RUN dotnet build "MyCloudProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyCloudProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyCloudProject.dll"]