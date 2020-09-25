FROM registry.cn-shenzhen.aliyuncs.com/basecore/aspnet:3.1-buster-slim-ex AS base
WORKDIR /app
COPY Puss.Api/obj/Docker/publish .
ENTRYPOINT ["dotnet", "Puss.Api.dll"]


#FROM registry.cn-shenzhen.aliyuncs.com/basecore/aspnet:3.1-buster-slim
#RUN ln -s /lib/x86_64-linux-gnu/libdl.so.2 /lib/x86_64-linux-gnu/libdl.so
#RUN apt-get update
#RUN apt install libgdiplus -y
#RUN ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll