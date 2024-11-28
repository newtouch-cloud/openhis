# 配置参数
$ServerName = "."  # 替换为你的 SQL Server 实例连接
$OutputFolder = "C:\Users\Administrator\Desktop\SQLChange"  # 输出目录路径
$StartDate = "2024-10-20"  # 起始日期
$EndDate = "2024-11-10"    # 结束日期
$OrganizeId = "6d5752a7-234a-403e-aa1c-df8b45d3469f"  # 组织 ID
$RoleName = "管理员"  # 角色名称

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

# 遍历每个数据库
foreach ($DatabaseName in $databases) {
    Write-Host "检查数据库: $DatabaseName 是否存在 [Sys_RoleAuthorize] 表"

    # 检查是否存在 [Sys_Module] 表
    $TableCheckQuery = @"
    SELECT COUNT(*) AS TableExists
    FROM [$DatabaseName].INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'Sys_RoleAuthorize';
"@
    $TableExists = Invoke-Sqlcmd -ServerInstance $ServerName -Query $TableCheckQuery | Select-Object -ExpandProperty TableExists

    if ($TableExists -eq 0) {
        Write-Host "数据库 $DatabaseName 中不存在 [Sys_RoleAuthorize] 表，跳过。"
        continue
    }
    
    Write-Host "处理数据库: $DatabaseName"

    # 查询符合条件的数据
    $query = @"
    SELECT *
    FROM [$DatabaseName].[dbo].[Sys_RoleAuthorize]
    WHERE [CreateTime] BETWEEN '$StartDate' AND '$EndDate'
      AND RoleId = (
          SELECT Id 
          FROM [$DatabaseName].[dbo].[Sys_Role]
          WHERE OrganizeId = '$OrganizeId' AND Name = '$RoleName'
      );
"@

    try {
        # 执行查询
        $Changes = Invoke-Sqlcmd -ServerInstance $ServerName -Database $DatabaseName -Query $query

        if ($Changes.Count -gt 0) {
            # 创建文件名
            $FileName = "${DatabaseName}_GLY_RoleAuthorize.sql"
            $FilePath = Join-Path -Path $DateOutputFolder -ChildPath $FileName

            # 初始化文件内容
            $SQLContent = @"
DELETE FROM [$DatabaseName].[dbo].[Sys_RoleAuthorize]
WHERE RoleId = (
    SELECT Id 
    FROM [$DatabaseName].[dbo].[Sys_Role]
    WHERE OrganizeId = '$OrganizeId' AND Name = '$RoleName'
);
"@ + "`n"

            # 遍历查询结果，生成 INSERT 语句
            foreach ($Change in $Changes) {
                # 动态获取字段和值
                $Fields = $Change.PSObject.Properties | Where-Object {
                $_.Name -notin @('RowError', 'RowState', 'Table', 'ItemArray', 'HasErrors')
            } | ForEach-Object {
                    $Name = $_.Name
                    $Value = $_.Value
                    
                    # 处理值类型
                    if ($Value -is [string]) {
                        $Value = $Value -replace "'", "''"
                        $Value = "'$Value'"
                    } elseif ($Value -is [datetime]) {
                        $Value = "'" + $Value.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'"
                    } elseif ($Value -eq $null -or [string]::IsNullOrWhiteSpace([string]$Value)) {
                        $Value = "''"
                    }
                    
                    [PSCustomObject]@{
                        FieldName = $Name
                        FieldValue = $Value
                    }
                }

                # 生成字段和值列表
                $FieldNames = ($Fields | ForEach-Object { $_.FieldName }) -join ", "
                $FieldValues = ($Fields | ForEach-Object { $_.FieldValue }) -join ", "

                # 添加 INSERT 语句到内容
                $SQLContent += @"
INSERT INTO [$DatabaseName].[dbo].[Sys_RoleAuthorize] ($FieldNames)
VALUES ($FieldValues);
"@ + "`n"
            }

            # 写入文件
            $SQLContent | Out-File -FilePath $FilePath -Encoding utf8
            Write-Host "生成文件: $FilePath"
        } else {
            Write-Host "数据库 $DatabaseName 无符合条件的数据。"
        }

    } catch {
        Write-Host "在数据库 $DatabaseName 中查询数据时出错: $($_.Exception.Message)"
    }
}

Write-Host "所有处理完成，生成的 SQL 文件保存在 $OutputFolder"
