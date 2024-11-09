# 设置 SQL Server 实例名称
$ServerName = "."  # 替换为你的 SQL Server 实例名称
$OutputFolder = "C:\Users\Administrator\Desktop\SQLChange"  "  # 输出目录路径
$StartDate = "2024-11-01"  # 起始日期
$EndDate = "2024-11-10"    # 结束日期

# 创建输出目录（如果不存在）
$DateFolderName = "$($StartDate.Replace('-', ''))_$($EndDate.Replace('-', ''))"
$DateOutputFolder = Join-Path -Path $OutputFolder -ChildPath $DateFolderName
if (!(Test-Path -Path $DateOutputFolder)) {
    New-Item -ItemType Directory -Path $DateOutputFolder | Out-Null
}

# 获取所有非系统数据库
$databases = Invoke-Sqlcmd -ServerInstance $ServerName -Query "
SELECT name 
FROM sys.databases 
WHERE state_desc = 'ONLINE' 
AND name NOT IN ('master', 'tempdb', 'model', 'msdb');
" | Select-Object -ExpandProperty name

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
        # 执行查询并获取变更记录
        $Changes = Invoke-Sqlcmd -ServerInstance $ServerName -Database $DatabaseName -Query $query

        # 遍历每条变更记录并生成 SQL 文件
        foreach ($Change in $Changes) {
            $EventID = $Change.ID
            $ObjectName = $Change.ObjectName -replace '[\\/:*?"<>|]', '_'  # 清理文件名
            $EventType = $Change.EventType
            $TSQLCommand = $Change.TSQLCommand
            $ChangeDate = $Change.ChangeDate

            # 创建文件名，如 ObjectName_EventID.sql
            $FileName = "${ObjectName}_${EventID}.sql"
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

    } catch {
        Write-Host "在数据库 $DatabaseName 中获取变更记录时出错: $($_.Exception.Message)"
    }
}

Write-Host "所有变更记录已保存至 $DateOutputFolder"
