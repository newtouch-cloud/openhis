# 设置 SQL Server 实例和文件夹路径
$ServerName = "127.0.0.1"  # 本地 SQL Server 实例
$User = "sa"  # 用户名
$Password = ""  # 密码
$SqlFilesDirectory = "C:\Users\mohaijiang\Desktop\20241020_20241129_old\test"  # 替换为你的 SQL 文件所在路径
$ErrorDirectory = "$SqlFilesDirectory\err"  # 错误文件夹路径

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
        # 构建 SQL Server 连接字符串
        $ConnectionString = "Server=$ServerName;Database=NewtouchHIS_Base;User ID=$User;Password=$Password;"
        
        # 执行 SQL 语句
        Invoke-Sqlcmd -ConnectionString $ConnectionString -Query $sqlContent -ErrorAction Stop
        Write-Host "成功执行: $($file.Name)"
    } catch {
        # 捕获 SQL 错误并记录
        $errorMessage = $_.Exception.Message
        $errorFilePath = Join-Path -Path $ErrorDirectory -ChildPath $file.Name

        # 记录错误类型
        Write-Host "错误: $errorMessage"
        
        # 将错误信息写入到错误文件中
        $errorLog = "Error executing $($file.Name): $errorMessage`r`n"
        Add-Content -Path $errorFilePath -Value $errorLog

        # 复制出错的 SQL 文件到错误目录
        Copy-Item -Path $file.FullName -Destination $ErrorDirectory -Force

        Write-Host "执行失败: $($file.Name)。错误信息已记录到: $errorFilePath"
        Write-Host "出错的文件已复制到错误目录: $ErrorDirectory"
    }
}
