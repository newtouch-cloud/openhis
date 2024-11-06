# SQL Server 实例名称
$ServerName = "."  # 替换为 SQL Server 实例名称
$BackupFolder = "C:\SQLBackup"  # 备份文件目录
$DataFolder = "C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA"  # 数据文件路径
$LogFolder = $DataFolder  # 如果日志文件路径相同

# 确保目录存在
if (!(Test-Path -Path $BackupFolder)) {
    Write-Host "备份文件夹不存在: $BackupFolder"
    exit
}

# 获取所有备份文件
Get-ChildItem -Path $BackupFolder -Filter *.bak | ForEach-Object {
    $FilePath = $_.FullName
    $fileNameWithoutExtension = [System.IO.Path]::GetFileNameWithoutExtension($_.Name)

    # 截取数据库名称
    $dbName = $fileNameWithoutExtension.Substring(0, $fileNameWithoutExtension.LastIndexOf('_'))
    Write-Host "准备还原的数据库: $dbName 从备份文件: $FilePath"

    # 获取备份文件中逻辑文件名列表
    $restoreFileList = sqlcmd -S $ServerName -Q "RESTORE FILELISTONLY FROM DISK = N'$FilePath'" | Out-String

   # Write-Host "restoreFileList is: $restoreFileList"

    # 提取数据文件和日志文件的逻辑名称
    $dataFile = $null
    $logFile = $null

    # 正则表达式匹配行首的逻辑文件名称
$restoreFileList -split "`r?`n" | ForEach-Object {
    $line = $_.Trim()
    if ($line -match "^(?<LogicalName>\S+)\s+.*\s+D\s") {
        $dataFile = $matches['LogicalName']
        Write-Host "数据文件逻辑名称: $dataFile"
    }
    elseif ($line -match "^(?<LogicalName>\S+)\s+.*\s+L\s") {
        $logFile = $matches['LogicalName']
        Write-Host "日志文件逻辑名称: $logFile"
    }
}

    # 设置还原的物理文件路径
    $dataFilePath = Join-Path -Path $DataFolder -ChildPath "$dbName.mdf"
    $logFilePath = Join-Path -Path $LogFolder -ChildPath "$dbName.ldf"
    
    # 输出变量内容
    Write-Host "备份文件路径: $FilePath"
    Write-Host "数据库名称: $dbName"
    Write-Host "数据文件逻辑名称: $dataFile"
    Write-Host "日志文件逻辑名称: $logFile"
    Write-Host "数据文件物理路径: $dataFilePath"
    Write-Host "日志文件物理路径: $logFilePath"

    # 构建还原命令
    $restoreQuery = @"
    RESTORE DATABASE [$dbName]
    FROM DISK = N'$FilePath'
    WITH MOVE '$dataFile' TO '$dataFilePath',
         MOVE '$logFile' TO '$logFilePath',
         REPLACE, RECOVERY, STATS = 10;
"@

    # 执行还原命令
    try {
        sqlcmd -S $ServerName -Q $restoreQuery
        Write-Host "成功还原数据库: $dbName"
        Write-Host "---------------------------------------------"
    } catch {
        Write-Host "还原数据库 $dbName 时出错: $_"
        Write-Host "---------------------------------------------"
    }
}

Write-Host "~~~~~~~~~~~~~~所有数据库还原完成~~~~~~~~~~~~"
Write-Host " "
Write-Host "==========开始还原数据库作业=========="
$JobBackupFolder = "C:\SQLBackup\BackUpJobs"  # 替换为备份文件所在目录

# 加载 SQL Server SMO 依赖项
[System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.Smo') | Out-Null

# 创建 SQL Server 对象
$Server = New-Object Microsoft.SqlServer.Management.Smo.Server $ServerName

# 获取现有作业名称列表
$ExistingJobNames = $Server.JobServer.Jobs | ForEach-Object { $_.Name }

# 遍历备份目录中的所有 .sql 文件
Get-ChildItem -Path $JobBackupFolder -Filter *.sql | ForEach-Object {
    $FilePath = $_.FullName
    $JobName = $_.BaseName  # 从文件名中获取作业名称
    Write-Host "正在恢复作业脚本: $FilePath"
    Write-Host "提取的 JobName 是: '$JobName'"  # 输出提取的作业名称

    # 检查作业是否已存在
    if ($ExistingJobNames -contains $JobName) {
        Write-Host "作业 '$JobName' 已存在，跳过恢复。"
        return  # 跳过恢复此作业
    }

    # 读取文件内容
    $Script = Get-Content -Path $FilePath -Raw

    # 执行脚本内容
    try {
        $Server.ConnectionContext.ExecuteNonQuery($Script)
        Write-Host "成功恢复作业 '$JobName'"
    } catch {
        Write-Host "恢复作业 '$JobName' 时出错: $_"
    }
}

Write-Host "所有作业已成功恢复"
