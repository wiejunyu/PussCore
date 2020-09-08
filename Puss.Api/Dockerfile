#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#WORKDIR /src
#COPY ["Puss.Api/Puss.Api.csproj", "Puss.Api/"]
#RUN dotnet restore "Puss.Api/Puss.Api.csproj"
#COPY . .
#WORKDIR "/src/Puss.Api"
#RUN dotnet build "Puss.Api.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Puss.Api.csproj" -c Release -o /app/publish
#s
FROM registry.cn-shenzhen.aliyuncs.com/mynetcore/basecore:3.1.0 AS base
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Puss.Api.dll"]