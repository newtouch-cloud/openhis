# 设置 SQL Server 实例和文件夹路径
$SqlServerInstance = "."  # 替换为你的 SQL Server 实例
$SqlFilesDirectory = "C:\Users\Administrator\Desktop\test"  # 替换为你的 SQL 文件所在路径
$ErrorDirectory = "$SqlFilesDirectory\err"

# 创建错误目录，如果不存在
if (-not (Test-Path -Path $ErrorDirectory)) {
    New-Item -ItemType Directory -Path $ErrorDirectory
}

# 获取所有 SQL 文件
$sqlFiles = Get-ChildItem -Path $SqlFilesDirectory -Filter *.sql

foreach ($file in $sqlFiles) {
    # 读取 SQL 文件内容
    $sqlContent = Get-Content -Path $file.FullName -Raw

    try {
        # 执行 SQL 语句
        Invoke-Sqlcmd -ServerInstance $SqlServerInstance -Database "Newtouch_His_Log" -Query $sqlContent
        Write-Host "成功执行: $($file.Name)"
    } catch {
        # 记录错误文件
        $errorMessage = $_.Exception.Message
        $errorFilePath = Join-Path -Path $ErrorDirectory -ChildPath $file.Name

        # 将错误信息写入到错误文件中
        $errorLog = "Error executing $($file.Name): $errorMessage`r`n"
        Add-Content -Path $errorFilePath -Value $errorLog

        # 复制出错的 SQL 文件到错误目录
        Copy-Item -Path $file.FullName -Destination $ErrorDirectory -Force

        Write-Host "执行失败: $($file.Name)。错误信息已记录到: $errorFilePath"
        Write-Host "出错的文件已复制到错误目录: $ErrorDirectory"
    }
}
