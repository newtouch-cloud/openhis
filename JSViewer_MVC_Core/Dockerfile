## 需要将此文件copy 到构建物中，进行docker 镜像构建
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "JSViewer_MVC_Core.dll"]