# 配置数据库连接信息
$ServerName = "127.0.0.1"
$User = "sa"
$Password = ""
$DatabaseName = "NewtouchHIS_Base"
$OutputFolder = [System.IO.Path]::Combine($env:USERPROFILE, "Desktop", "SQLChange")  # 输出目录路径-当前用户桌面的SQLChange文件夹
$StartDate = "2024-10-20"  # 起始日期
$EndDate = "2024-11-29"    # 结束日期


# 创建输出目录（如果不存在）
$DateFolderName = "$($StartDate.Replace('-', ''))_$($EndDate.Replace('-', ''))"
$DateOutputFolder = Join-Path -Path $OutputFolder -ChildPath $DateFolderName
if (!(Test-Path -Path $DateOutputFolder)) {
    New-Item -ItemType Directory -Path $DateOutputFolder | Out-Null
}

# 定义目标表和文件
$Tables = @(
    @{
        TableName = "Sys_Report"
        PrimaryKey = "ReportID"
        OutputFile = Join-Path -Path $DateOutputFolder -ChildPath "ReportChange.sql"
    },
    @{
        TableName = "Sys_ReportTemplate"
        PrimaryKey = "TemplateID"
        OutputFile = Join-Path -Path $DateOutputFolder -ChildPath "ReportTemplateChange.sql"
    }
)

# 定义数据库连接字符串
$ConnectionString = "Server=$ServerName;Database=$DatabaseName;User ID=$User;Password=$Password;"

foreach ($Table in $Tables) {
    $TableName = $Table.TableName
    $PrimaryKey = $Table.PrimaryKey
    $OutputFile = $Table.OutputFile

    # 初始化 SQL 内容
    $SQLContent = ""

    # 查询符合条件的数据
    $Query = @"
SELECT *
FROM [$DatabaseName].[dbo].[$TableName]
WHERE (CreateTime BETWEEN '$StartDate' AND '$EndDate')
   OR (CreateTime < '$StartDate' AND LastModifyTime BETWEEN '$StartDate' AND '$EndDate');
"@

    try {
        # 创建数据库连接
        $Connection = New-Object System.Data.SqlClient.SqlConnection($ConnectionString)
        $Connection.Open()

        # 创建 SQL 命令
        $Command = $Connection.CreateCommand()
        $Command.CommandText = $Query

        # 执行查询并使用 SqlDataReader 逐行读取数据
        $Reader = $Command.ExecuteReader()

        while ($Reader.Read()) {
            # 动态获取字段和值，排除不必要的字段
            $Fields = @()
            for ($i = 0; $i -lt $Reader.FieldCount; $i++) {
                $FieldName = $Reader.GetName($i)
                $FieldValue = $Reader.GetValue($i)

                # 处理值类型
                if ($FieldValue -is [string]) {
                    # 针对 XML 或 HTML 内容处理
                    if ($FieldValue.StartsWith('<?xml') -or $FieldValue.StartsWith('<')) {
                        $FieldValue = $FieldValue -replace "'", "''"  # 替换单引号为双单引号
                        $FieldValue = "N'$FieldValue'"               # 使用 N 前缀支持 Unicode
                    } else {
                        $FieldValue = $FieldValue -replace "'", "''" # 替换单引号为双单引号
                        $FieldValue = "'$FieldValue'"               # 普通字符串用单引号括起来
                    }
                } elseif ($FieldValue -is [datetime]) {
                    $FieldValue = "'" + $FieldValue.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'"
                } elseif ($FieldValue -eq $null -or [string]::IsNullOrWhiteSpace([string]$FieldValue)) {
                    $FieldValue = "''"
                }

                $Fields += [PSCustomObject]@{
                    FieldName = $FieldName
                    FieldValue = $FieldValue
                }
            }

            # 生成字段列表和值列表
            $FieldNames = ($Fields | ForEach-Object { $_.FieldName }) -join ", "
            $FieldValues = ($Fields | ForEach-Object { $_.FieldValue }) -join ", "

            if ($Reader["CreateTime"] -ge $StartDate -and $Reader["CreateTime"] -le $EndDate) {
                # 添加 INSERT 语句到内容
                $SQLContent += @"
INSERT INTO [$DatabaseName].[dbo].[$TableName] ($FieldNames)
VALUES ($FieldValues);
"@ + "`n"
            } elseif ($Reader["CreateTime"] -lt $StartDate -and $Reader["LastModifyTime"] -ge $StartDate -and $Reader["LastModifyTime"] -le $EndDate) {
                # 添加 UPDATE 语句到内容
                $SetClause = ($Fields | ForEach-Object {
                    if ($_.FieldName -ne $PrimaryKey) {
                        "$($_.FieldName) = $($_.FieldValue)"
                    }
                }) -join ", "
                $SQLContent += @"
UPDATE [$DatabaseName].[dbo].[$TableName]
SET $SetClause
WHERE [$PrimaryKey] = $($Reader[$PrimaryKey]);
"@ + "`n"
            }
        }

        # 关闭 SqlDataReader 和数据库连接
        $Reader.Close()
        $Connection.Close()

        # 将 SQL 内容写入文件
        if ($SQLContent -ne "") {
            $SQLContent | Out-File -FilePath $OutputFile -Encoding UTF8
            Write-Host "生成文件: $OutputFile"
        } else {
            Write-Host "表 $TableName 没有符合条件的数据。"
        }
    } catch {
        Write-Host "查询表 $TableName 时出错: $($_.Exception.Message)"
    }
}
