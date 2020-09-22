FROM registry.cn-shenzhen.aliyuncs.com/basecore/aspnet:3.1-buster-slim-ex AS base
WORKDIR /app
COPY Puss.Api/obj/Docker/publish .
ENTRYPOINT ["dotnet", "Puss.Api.dll"]