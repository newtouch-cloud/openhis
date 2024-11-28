# 设置 SQL Server 实例名称
$ServerName = "."
$OutputFolder = "C:\Users\Administrator\Desktop\SQLChange"  # 输出目录路径
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
    Write-Host "检查数据库: $DatabaseName 是否存在 [Sys_Module] 表"

    # 检查是否存在 [Sys_Module] 表
    $TableCheckQuery = @"
    SELECT COUNT(*) AS TableExists
    FROM [$DatabaseName].INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'Sys_Module';
"@
    $TableExists = Invoke-Sqlcmd -ServerInstance $ServerName -Query $TableCheckQuery | Select-Object -ExpandProperty TableExists

    if ($TableExists -eq 0) {
        Write-Host "数据库 $DatabaseName 中不存在 [Sys_Module] 表，跳过。"
        continue
    }

    Write-Host "数据库 $DatabaseName 中存在 [Sys_Module] 表，开始处理。"

    # 定义文件路径
    $FileName = "${DatabaseName}_moduleChange.sql"
    $FilePath = Join-Path -Path $DateOutputFolder -ChildPath $FileName

    # 初始化文件内容
    $SQLContent = ""

    # SQL 查询，获取满足条件的数据
    $query = @"
    SELECT *
    FROM [$DatabaseName].[dbo].[Sys_Module]
    WHERE 
        ([CreateTime] < '$StartDate' AND [LastModifyTime] >= '$StartDate' AND [LastModifyTime] <= '$EndDate')
        OR ([CreateTime] >= '$StartDate' AND [CreateTime] <= '$EndDate');
"@

    try {
        # 执行查询并获取数据
        $Changes = Invoke-Sqlcmd -ServerInstance $ServerName -Database $DatabaseName -Query $query

        # 遍历每条记录并生成 SQL 内容
        foreach ($Change in $Changes) {
            # 动态获取字段及其值，排除不必要字段
            $Fields = $Change.PSObject.Properties | Where-Object {
                $_.Name -notin @('RowError', 'RowState', 'Table', 'ItemArray', 'HasErrors')
            } | ForEach-Object {
                $Name = $_.Name
                $Value = $_.Value
                
                # 如果值是字符串类型，处理单引号
                if ($Value -is [string]) {
                    $Value = $Value -replace "'", "''"
                    $Value = "'$Value'"  # 字符串值用单引号括起来
                } elseif ($Value -is [datetime]) {
                    # 日期类型格式化为 SQL 标准格式
                    $Value = "'" + $Value.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'"
                } elseif ($Value -eq $null -or [string]::IsNullOrWhiteSpace([string]$Value)) {
                    Write-Host "处理空值"
                    $Value = "''"  # 处理 NULL 值
                }
                Write-Host "修改后的值是$Name，$Value "

                # 返回字段名和值的键值对
                [PSCustomObject]@{
                    FieldName = $Name
                    FieldValue = $Value
                }
            }

            # 生成字段列表和值列表
            $FieldNames = ($Fields | ForEach-Object { $_.FieldName }) -join ", "
            $FieldValues = ($Fields | ForEach-Object { $_.FieldValue }) -join ", "

            if ($Change.CreateTime -lt $StartDate -and $Change.LastModifyTime -ge $StartDate -and $Change.LastModifyTime -le $EndDate) {
                # 添加 UPDATE 语句到内容
                $SetClause = ($Fields | ForEach-Object { "$($_.FieldName) = $($_.FieldValue)" }) -join ", "
                $SQLContent += @"
UPDATE [$DatabaseName].[dbo].[Sys_Module]
SET $SetClause
WHERE [Id] = '$($Change.Id)';
"@ + "`n"
            } elseif ($Change.CreateTime -ge $StartDate -and $Change.CreateTime -le $EndDate) {
                # 添加 INSERT 语句到内容
                $SQLContent += @"
INSERT INTO [$DatabaseName].[dbo].[Sys_Module] ($FieldNames)
VALUES ($FieldValues);
"@ + "`n"
            }
        }

        # 将内容写入文件
        if (-not [string]::IsNullOrWhiteSpace($SQLContent)) {
            $SQLContent | Out-File -FilePath $FilePath -Encoding utf8
            Write-Host "已生成文件: $FilePath"
        } else {
            Write-Host "数据库 $DatabaseName 中没有满足条件的数据。"
        }

    } catch {
        Write-Host "在数据库 $DatabaseName 中处理数据时出错: $($_.Exception.Message)"
    }
}

Write-Host "所有语句已保存至 $DateOutputFolder"