# SQL Server 实例名称
$ServerName = "."  # 替换为你的服务器名称
$BaseBackupFolder = "C:\SQLBackup"  # 基本备份路径
$DateFolder = (Get-Date -Format "yyyyMMdd")  # 生成带日期的文件夹名称

# 生成数据库备份路径
$BackupFolder = Join-Path -Path $BaseBackupFolder -ChildPath $DateFolder

# 确保备份目录存在
if (!(Test-Path -Path $BackupFolder)) {
    New-Item -ItemType Directory -Path $BackupFolder
}

# 获取所有数据库名称并过滤空白行
$query = "SELECT name FROM sys.databases WHERE state_desc = 'ONLINE' AND name NOT IN ('master', 'tempdb', 'model', 'msdb','DWDiagnostics','DWConfiguration','DWQueue')"
$Databases = sqlcmd -S $ServerName -Q $query | Select-Object -Skip 2 | Where-Object { $_ -ne "" }  # 跳过空白行

foreach ($Database in $Databases) {
    $DatabaseName = $Database.Trim()
    if ($DatabaseName) {  # 确保数据库名称不为空
        $BackupFile = Join-Path -Path $BackupFolder -ChildPath "$($DatabaseName)_$(Get-Date -Format 'yyyyMMdd').bak"
        
        # 备份数据库的 T-SQL 脚本
        $backupQuery = "BACKUP DATABASE [$DatabaseName] TO DISK = N'$BackupFile' WITH NOFORMAT, INIT, NAME = N'$DatabaseName-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10"
        
        Write-Host "正在备份数据库: $DatabaseName 到 $BackupFile"
        try {
            sqlcmd -S $ServerName -Q $backupQuery
            Write-Host "成功备份数据库: $DatabaseName"
        } catch {
            Write-Host "备份数据库 $DatabaseName 时出错: $_"
        }
    }
}

Write-Host "所有数据库已备份至 $BackupFolder"
Write-Host " "
Write-Host "==========开始备份数据库作业=========="
$JobBackupFolderName = "BackUpJobs"

# 生成数据库备份路径
$JobBackupFolder = Join-Path -Path $BackupFolder -ChildPath $JobBackupFolderName

# 确保备份目录存在
if (!(Test-Path -Path $JobBackupFolder)) {
    New-Item -ItemType Directory -Path $JobBackupFolder
}

# 加载 SQL Server SMO 依赖项
[System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.Smo') | Out-Null

# 创建 SQL Server 对象
$Server = New-Object Microsoft.SqlServer.Management.Smo.Server $ServerName

# 遍历所有作业并生成脚本
foreach ($Job in $Server.JobServer.Jobs) {
    $JobNameClean = $Job.Name -replace '[\\/:*?"<>|]', '_'  # 清理作业名称
    $FilePath = Join-Path -Path $JobBackupFolder -ChildPath "$JobNameClean.sql"

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

Write-Host "所有作业已备份至 $JobBackupFolder"
