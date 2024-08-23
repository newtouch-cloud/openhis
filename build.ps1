## ʹ�÷�ʽ�� �ҵ��Լ�visual studio ��װĿ¼�� MSBuild Ŀ¼������Current\Bin Ŀ¼���뻷������
## eg: C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin
## ���뻷����������Ч����powershell ���� ./build.ps1


Write-Host "��ʼ���벢����Base"
MSBuild.exe .\Newtouch.HIS.Base\Newtouch.HIS.Base.HOSP\Newtouch.HIS.Base.HOSP.csproj /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����Baseʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "��ʼ���벢����BaseAPI"
MSBuild.exe .\Newtouch.HIS.Base\Newtouch.HIS.Base.HOSP.API\Newtouch.HIS.Base.HOSP.API.csproj /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����BaseAPIʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}


Write-Host "��ʼ���벢����Sett"
MSBuild.exe  .\Newtouch.HIS.Sett\Newtouch.Web\Newtouch.HIS.Sett.Web.csproj /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����Settʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "��ʼ���벢����SettAPI"
MSBuild.exe .\Newtouch.HIS.Sett\Newtouch.HIS.Sett.API\Newtouch.HIS.Sett.API.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����SettAPIʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "��ʼ���벢����CIS" -ForegroundColor Red
MSBuild.exe .\Newtouch.HIS.CIS\Newtouch.CIS.Web\Newtouch.CIS.Web.csproj /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����CISʧ�ܣ�."
    exit $LASTEXITCODE
}

Write-Host  "��ʼ���벢����CISAPI"
MSBuild.exe Newtouch.HIS.CIS/NewtouchCIS.API/NewtouchCIS.API.csproj /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����CISAPIʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "��ʼ���벢����PDS"
MSBuild.exe Newtouch.HIS.PDS/Newtouch.HIS.Web/Newtouch.HIS.PDS.Web.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����PDSʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "��ʼ���벢����PDSAPI"
MSBuild.exe Newtouch.HIS.PDS/Newtouch.PDS.API/Newtouch.Pds.Api.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����PDSAPIʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "��ʼ���벢����EMR"
MSBuild.exe Newtouch.HIS.EMR/Newtouch.EMR.Web/Newtouch.EMR.Web.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����EMRʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "��ʼ���벢����OR"
MSBuild.exe Newtouch.HIS.OR/Newtouch.OR.ManageSystem.Web/Newtouch.HIS.OR.Web.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����ORʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "��ʼ���벢����ORAPI"
MSBuild.exe Newtouch.HIS.OR/Newtouch.OR.ManageSystem.API/Newtouch.HIS.OR.API.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����ORAPIʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "��ʼ���벢����Herp"
MSBuild.exe Newtouch.HIS.Herp/Newtouch.Herp.Web/Newtouch.Herp.Web.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����Herpʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "��ʼ���벢����MRMS"
MSBuild.exe Newtouch.HIS.MRMS/Newtouch.MR.ManageSystem.Web/Newtouch.HIS.MRMS.Web.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����MRMSʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "��ʼ���벢����MRQC"
MSBuild.exe Newtouch.HIS.MRQC/Newtouch.MRQC.Web/Newtouch.MRQC.Web.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����MRQCʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "��ʼ���벢����AuthCenterAPI"
MSBuild.exe Newtouch.HIS.WebAPI.Manage/NewtouchHIS.AuthenticationCenter/NewtouchHIS.AuthenticationCenter.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����AuthCenterAPIʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "��ʼ���벢����ApiManage"
MSBuild.exe Newtouch.HIS.WebAPI.Manage/NewtouchHIS.WebAPI.Manage/NewtouchHIS.WebAPI.Manage.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����ApiManageʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "��ʼ���벢����his.baseapi"
MSBuild.exe Newtouch.HIS.SSO/HIS.BaseAPI/HIS.BaseAPI.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����his.baseapiʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host  "��ʼ���벢����Union"
MSBuild.exe Newtouch.HIS.SSO/HIS.SSO/HIS.SSO.csproj  /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
if ($LASTEXITCODE -ne 0) {
    Write-Host "����Unionʧ�ܣ�." -ForegroundColor Red
    exit $LASTEXITCODE
}
