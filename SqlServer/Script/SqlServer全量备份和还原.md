# Sql Server全量备份和还原

## 备份
```sql

DECLARE @name VARCHAR(50)           -- 数据库名称
DECLARE @path VARCHAR(256)          -- 备份文件路径
DECLARE @fileName VARCHAR(256)      -- 完整备份文件路径
DECLARE @fileDate VARCHAR(20)       -- 用于备份文件名称的日期格式
DECLARE @backupName VARCHAR(100)    -- 备份名称

-- 指定备份文件的存储路径
SET @path = 'C:\Backup\'  -- 修改为你需要的备份目录路径

-- 获取当前日期并转换为字符串格式
SET @fileDate = CONVERT(VARCHAR(20), GETDATE(), 112)

-- 使用游标遍历所有数据库
DECLARE db_cursor CURSOR FOR
SELECT name
FROM master.dbo.sysdatabases
WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb','DWDiagnostics','DWConfiguration','DWQueue')  -- 排除系统数据库

OPEN db_cursor
FETCH NEXT FROM db_cursor INTO @name

WHILE @@FETCH_STATUS = 0
BEGIN
    -- 构建备份文件的完整路径和名称
    SET @fileName = @path + @name + '_' + @fileDate + '.bak'

    -- 设置备份名称
    SET @backupName = @name + '_FullBackup'

    -- 执行备份命令
    BACKUP DATABASE @name TO DISK = @fileName
    WITH NOFORMAT, NOINIT,  
    NAME = @backupName, SKIP, NOREWIND, NOUNLOAD,  STATS = 10

    FETCH NEXT FROM db_cursor INTO @name
END

CLOSE db_cursor
DEALLOCATE db_cursor
```


## 还原
```sql

DECLARE @path NVARCHAR(256)          -- 备份文件路径
DECLARE @fileName NVARCHAR(256)      -- 备份文件名
DECLARE @dbName NVARCHAR(50)         -- 数据库名称
DECLARE @backupFile NVARCHAR(256)
DECLARE @logicalDataName NVARCHAR(128)
DECLARE @logicalLogName NVARCHAR(128)
DECLARE @dataFilePath NVARCHAR(256)  -- 数据文件路径
DECLARE @logFilePath NVARCHAR(256)   -- 日志文件路径

-- 备份文件存储路径
SET @path = 'C:\Backup\'  -- 修改为备份文件所在的目录

-- 临时表存储目录中的备份文件名
CREATE TABLE #BackupFiles (FileName NVARCHAR(256) NULL)

-- 填充临时表，读取备份目录中的所有 .bak 文件
INSERT INTO #BackupFiles (FileName)
EXEC xp_cmdshell 'dir C:\Backup\*.bak /b'

-- 去除 NULL 值的文件名（可能是 xp_cmdshell 返回的空行）
DELETE FROM #BackupFiles WHERE FileName IS NULL

DECLARE db_cursor CURSOR FOR
SELECT FileName FROM #BackupFiles

OPEN db_cursor
FETCH NEXT FROM db_cursor INTO @fileName

WHILE @@FETCH_STATUS = 0
BEGIN
    -- 构建完整的备份文件路径
    SET @backupFile = @path + @fileName

    -- 获取数据库名称，截取最后一个下划线前的部分
    SET @dbName = LEFT(@fileName, LEN(@fileName) - CHARINDEX('_', REVERSE(@fileName)))

    -- 创建临时表来存储 FILELISTONLY 的输出
    CREATE TABLE #FileList (
        LogicalName NVARCHAR(128),
        PhysicalName NVARCHAR(260),
        Type CHAR(1),
        FileGroupName NVARCHAR(128),
        Size BIGINT,
        MaxSize BIGINT,
        FileId BIGINT,
        CreateLSN NUMERIC(25,0),
        DropLSN NUMERIC(25,0),
        UniqueId UNIQUEIDENTIFIER,
        ReadOnlyLSN NUMERIC(25,0),
        ReadWriteLSN NUMERIC(25,0),
        BackupSizeInBytes BIGINT,
        SourceBlockSize INT,
        FileGroupId INT,
        LogGroupGUID UNIQUEIDENTIFIER,
        DifferentialBaseLSN NUMERIC(25,0),
        DifferentialBaseGUID UNIQUEIDENTIFIER,
        IsReadOnly BIT,
        IsPresent BIT,
        TDEThumbprint NVARCHAR(32),
        SnapshortUrl NVARCHAR(32)
    )

    -- 插入 RESTORE FILELISTONLY 的结果到临时表中
    INSERT INTO #FileList 
    EXEC ('RESTORE FILELISTONLY FROM DISK = ''' + @backupFile + '''')

    -- 从临时表中获取数据文件和日志文件的逻辑名称
    SELECT 
        @logicalDataName = LogicalName 
    FROM #FileList 
    WHERE Type = 'D'

    SELECT 
        @logicalLogName = LogicalName 
    FROM #FileList 
    WHERE Type = 'L'

    -- 设置还原文件的物理路径
    SET @dataFilePath = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\' + @dbName + '.mdf'
    SET @logFilePath = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\' + @dbName + '.ldf'

    -- 输出变量内容
    PRINT N'备份文件路径: ' + @backupFile
    PRINT N'数据库名称: ' + @dbName
    PRINT N'数据文件逻辑名称: ' + ISNULL(@logicalDataName, N'未找到数据文件逻辑名称')
    PRINT N'日志文件逻辑名称: ' + ISNULL(@logicalLogName, N'未找到日志文件逻辑名称')
    PRINT N'数据文件物理路径: ' + @dataFilePath
    PRINT N'日志文件物理路径: ' + @logFilePath
    PRINT N'---------------------------------------------'

    -- 还原数据库
    RESTORE DATABASE @dbName
    FROM DISK = @backupFile
    WITH MOVE @logicalDataName TO @dataFilePath, 
         MOVE @logicalLogName TO @logFilePath, 
         REPLACE,
         RECOVERY, 
         STATS = 10

    -- 删除临时表，以便下次循环重新创建
    DROP TABLE #FileList

    FETCH NEXT FROM db_cursor INTO @fileName
END

CLOSE db_cursor
DEALLOCATE db_cursor
DROP TABLE #BackupFiles



--问题1：如果出现行数对应不上执行下面的命令，对比临时表#FileList的字段
--RESTORE FILELISTONLY FROM DISK = 'C:\Backup\NewtouchHIS_Base_20241017.bak'

--问题2：SQL Server 阻止了对组件 'xp_cmdshell' 的 过程 'sys.xp_cmdshell' 的访问，因为此组件已作为此服务器安全配置的一部分而被关闭。
--sp_configure 'show advanced options',1
--reconfigure
--go
--sp_configure 'xp_cmdshell',1
--reconfigure
--go

```