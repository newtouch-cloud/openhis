## 使用方式： 找到自己visual studio 安装目录的 MSBuild 目录，将其Current\Bin 目录加入环境变量
## eg: C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin
## 加入环境变量，生效后，在powershell 运行 ./build.ps1

$OutputEncoding = [System.Text.Encoding]::UTF8

[Console]::OutputEncoding = [System.Text.Encoding]::UTF8


Write-Host "开始编译并发布Static"
MSBuild.exe .\Newtouch.HIS.Static/Newtouch.HIS.Static/Newtouch.HIS.Static.csproj /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译Static失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}


Write-Host "开始编译并发布Base"
MSBuild.exe .\Newtouch.HIS.Base\Newtouch.HIS.Base.HOSP\Newtouch.HIS.Base.HOSP.csproj /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译Base失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "开始编译并发布BaseAPI"
MSBuild.exe .\Newtouch.HIS.Base\Newtouch.HIS.Base.HOSP.API\Newtouch.HIS.Base.HOSP.API.csproj /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译BaseAPI失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}


Write-Host "开始编译并发布Sett"
MSBuild.exe  .\Newtouch.HIS.Sett\Newtouch.Web\Newtouch.HIS.Sett.Web.csproj /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译Sett失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "开始编译并发布SettAPI"
MSBuild.exe .\Newtouch.HIS.Sett\Newtouch.HIS.Sett.API\Newtouch.HIS.Sett.API.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译SettAPI失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "开始编译并发布CIS"
MSBuild.exe .\Newtouch.HIS.CIS\Newtouch.CIS.Web\Newtouch.CIS.Web.csproj /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译CIS失败！."  -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "开始编译并发布CISAPI"
MSBuild.exe Newtouch.HIS.CIS/NewtouchCIS.API/NewtouchCIS.API.csproj /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译CISAPI失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "开始编译并发布PDS"
MSBuild.exe Newtouch.HIS.PDS/Newtouch.HIS.Web/Newtouch.HIS.PDS.Web.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译PDS失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "开始编译并发布PDSAPI"
MSBuild.exe Newtouch.HIS.PDS/Newtouch.PDS.API/Newtouch.Pds.Api.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译PDSAPI失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "开始编译并发布EMR"
MSBuild.exe Newtouch.HIS.EMR/Newtouch.EMR.Web/Newtouch.EMR.Web.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译EMR失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "开始编译并发布OR"
MSBuild.exe Newtouch.HIS.OR/Newtouch.OR.ManageSystem.Web/Newtouch.HIS.OR.Web.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译OR失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "开始编译并发布ORAPI"
MSBuild.exe Newtouch.HIS.OR/Newtouch.OR.ManageSystem.API/Newtouch.HIS.OR.API.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译ORAPI失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "开始编译并发布Herp"
MSBuild.exe Newtouch.HIS.Herp/Newtouch.Herp.Web/Newtouch.Herp.Web.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译Herp失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "开始编译并发布MRMS"
MSBuild.exe Newtouch.HIS.MRMS/Newtouch.MR.ManageSystem.Web/Newtouch.HIS.MRMS.Web.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译MRMS失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "开始编译并发布MRQC"
MSBuild.exe Newtouch.HIS.MRQC/Newtouch.MRQC.Web/Newtouch.MRQC.Web.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译MRQC失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "开始编译并发布AuthCenterAPI"
MSBuild.exe Newtouch.HIS.WebAPI.Manage/NewtouchHIS.AuthenticationCenter/NewtouchHIS.AuthenticationCenter.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译AuthCenterAPI失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "开始编译并发布ApiManage"
MSBuild.exe Newtouch.HIS.WebAPI.Manage/NewtouchHIS.WebAPI.Manage/NewtouchHIS.WebAPI.Manage.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译ApiManage失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "开始编译并发布his.baseapi"
MSBuild.exe Newtouch.HIS.SSO/HIS.BaseAPI/HIS.BaseAPI.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译his.baseapi失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "开始编译并发布Union"
MSBuild.exe Newtouch.HIS.SSO/HIS.SSO/HIS.SSO.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=PublishProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "编译Union失败！." -ForegroundColor Red
    exit $LASTEXITCODE
}
