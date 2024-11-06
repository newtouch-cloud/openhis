# SQL Server ʵ������
$ServerName = "."  # �滻Ϊʵ�ʷ���������
$BackupFolder = "C:\Backup\BackUpJobs"

# ȷ������Ŀ¼����
if (!(Test-Path -Path $BackupFolder)) {
    New-Item -ItemType Directory -Path $BackupFolder
}

# ���� SQL Server SMO ������
[System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.Smo') | Out-Null

# ���� SQL Server ����
$Server = New-Object Microsoft.SqlServer.Management.Smo.Server $ServerName

# ����������ҵ�����ɽű�
foreach ($Job in $Server.JobServer.Jobs) {
    $JobNameClean = $Job.Name -replace '[\\/:*?"<>|]', '_'  # ������ҵ����
    $FilePath = Join-Path -Path $BackupFolder -ChildPath "$JobNameClean.sql"

    Write-Host "���ڱ�����ҵ: $Job.Name �� $FilePath"  # ���·�����

    # ��� USE [msdb] �� GO ��ÿ����ҵ�ű�
    $Header = @"
USE [msdb]
GO

"@

    # ����ҵ�ű����浽�ļ�
    $Header | Out-File -FilePath $FilePath -Encoding utf8
    $Job.Script() | Out-File -FilePath $FilePath -Encoding utf8 -Append
}

Write-Host "������ҵ�ѱ����� $BackupFolder"
