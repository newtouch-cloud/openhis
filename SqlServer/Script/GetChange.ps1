# 配置参数
$ServerName = "127.0.0.1"  # 本地 SQL Server 实例
$User = "sa"  # 
$Password = ""  # 
$OutputFolder = [System.IO.Path]::Combine($env:USERPROFILE, "Desktop", "SQLChange")  # 输出目录路径-当前用户桌面的SQLChange文件夹
$StartDate = "2024-10-20"  # 起始日期
$EndDate = "2024-11-29"    # 结束日期

# 创建输出目录（如果不存在）
$DateFolderName = "$($StartDate.Replace('-', ''))_$($EndDate.Replace('-', ''))"
$DateOutputFolder = Join-Path -Path $OutputFolder -ChildPath $DateFolderName
if (!(Test-Path -Path $DateOutputFolder)) {
    New-Item -ItemType Directory -Path $DateOutputFolder | Out-Null
}

# 获取所有非系统数据库
$databasesQuery = @"
SELECT name 
FROM sys.databases 
WHERE state_desc = 'ONLINE' 
AND name NOT IN ('master', 'tempdb', 'model', 'msdb');
"@

# 获取所有数据库
$ConnectionString = "Server=$ServerName;Database=master;Integrated Security=True;"
$Connection = New-Object System.Data.SqlClient.SqlConnection($ConnectionString)
$Connection.Open()

# 创建 SQL 命令
$Command = $Connection.CreateCommand()
$Command.CommandText = $databasesQuery

# 执行查询并使用 SqlDataReader 逐行读取数据
$Reader = $Command.ExecuteReader()

$databases = @()
while ($Reader.Read()) {
    $databases += $Reader["name"]
}

$Reader.Close()
$Connection.Close()

# 遍历所有数据库
foreach ($DatabaseName in $databases) {
    Write-Host "处理数据库: $DatabaseName"

    # SQL 查询，获取指定时间段内的变更记录
    $query = @"
    SELECT [ID], [EventType], [ObjectName], [TSQLCommand], [ChangeDate]
    FROM [$DatabaseName].[dbo].[DDLChangeLog]
    WHERE [ChangeDate] BETWEEN '$StartDate' AND '$EndDate'
    ORDER BY [ChangeDate];
"@

    try {
        # 创建数据库连接
        $ConnectionString = "Server=$ServerName;Database=$DatabaseName;Integrated Security=True;"
        $Connection = New-Object System.Data.SqlClient.SqlConnection($ConnectionString)
        $Connection.Open()

        # 创建 SQL 命令
        $Command = $Connection.CreateCommand()
        $Command.CommandText = $query

        # 执行查询并使用 SqlDataReader 逐行读取数据
        $Reader = $Command.ExecuteReader()

        # 遍历每条变更记录并生成 SQL 文件
        while ($Reader.Read()) {
            $EventID = $Reader["ID"]
            $ObjectName = $Reader["ObjectName"] -replace '[\\/:*?"<>|]', '_'  # 清理文件名
            $EventType = $Reader["EventType"]
            $TSQLCommand = $Reader["TSQLCommand"]
            $ChangeDate = $Reader["ChangeDate"]

            # 将 ChangeDate 格式化为 "yyyyMMddHHmmssfff" 格式
            $FormattedChangeDate = $ChangeDate.ToString("yyyyMMddHHmmssfff")

            # 创建文件名，格式为 ChangeDate_ObjectName_EventID.sql
            $FileName = "${FormattedChangeDate}_${ObjectName}_${EventID}.sql"
            $FilePath = Join-Path -Path $DateOutputFolder -ChildPath $FileName

            # 构造 SQL 内容，包含 USE 语句和事件信息
            $Content = @"
USE [$DatabaseName]
GO

-- 事件类型: $EventType
-- 变更时间: $ChangeDate
$TSQLCommand
"@

            # 将内容写入文件
            $Content | Out-File -FilePath $FilePath -Encoding utf8

            Write-Host "已生成文件: $FilePath"
        }

        # 关闭 SqlDataReader 和数据库连接
        $Reader.Close()
        $Connection.Close()

    } catch {
        Write-Host "在数据库 $DatabaseName 中获取变更记录时出错: $($_.Exception.Message)"
    }
}

Write-Host "所有变更记录已保存至 $DateOutputFolder"
