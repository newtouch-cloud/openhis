# SQL Server 实例名称
$ServerName = "."  # 替换为实际服务器名称
$BackupFolder = "C:\Backup\BackUpJobs"

# 确保备份目录存在
if (!(Test-Path -Path $BackupFolder)) {
    New-Item -ItemType Directory -Path $BackupFolder
}

# 加载 SQL Server SMO 依赖项
[System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.Smo') | Out-Null

# 创建 SQL Server 对象
$Server = New-Object Microsoft.SqlServer.Management.Smo.Server $ServerName

# 遍历所有作业并生成脚本
foreach ($Job in $Server.JobServer.Jobs) {
    $JobNameClean = $Job.Name -replace '[\\/:*?"<>|]', '_'  # 清理作业名称
    $FilePath = Join-Path -Path $BackupFolder -ChildPath "$JobNameClean.sql"

    Write-Host "正在备份作业: $Job.Name 到 $FilePath"  # 输出路径检查

    # 添加 USE [msdb] 和 GO 到每个作业脚本
    $Header = @"
USE [msdb]
GO

"@

    # 将作业脚本保存到文件
    $Header | Out-File -FilePath $FilePath -Encoding utf8
    $Job.Script() | Out-File -FilePath $FilePath -Encoding utf8 -Append
}

Write-Host "所有作业已备份至 $BackupFolder"
