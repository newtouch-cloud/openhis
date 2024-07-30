# 在部署包中构建镜像

FROM mcr.microsoft.com/dotnet/aspnet:6.0-windowsservercore-ltsc2019
WORKDIR /app
EXPOSE 80

COPY . .
ENTRYPOINT ["dotnet", "HIS.SSO.dll"]