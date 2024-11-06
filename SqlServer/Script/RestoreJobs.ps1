# SQL Server 实例名称
$ServerName = "."  # 替换为新 SQL Server 实例名称
$BackupFolder = "C:\Backup\BackUpJobs"  # 替换为备份文件所在目录

# 加载 SQL Server SMO 依赖项
[System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.Smo') | Out-Null

# 创建 SQL Server 对象
$Server = New-Object Microsoft.SqlServer.Management.Smo.Server $ServerName

# 获取现有作业名称列表
$ExistingJobNames = $Server.JobServer.Jobs | ForEach-Object { $_.Name }

# 遍历备份目录中的所有 .sql 文件
Get-ChildItem -Path $BackupFolder -Filter *.sql | ForEach-Object {
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

Write-Host "所有作业已成功恢复到 $ServerName"
