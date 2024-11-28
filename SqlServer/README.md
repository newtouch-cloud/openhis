# HIS数据库的操作规范
  注意： 本教程使用的是SQL Server 2022版本  

## 使用
1. V1.8版本使用 `./V1.8/V1.8.zip` 进行还原

## 备份还原
1. 备份使用 `./Script/Backup.ps1` 该文件按天，一次性备份所有的数据库和定时脚本
	1. 修改备份路径为自己想要的path
	2. 放在数据库服务器上执行
2. 还原使用 `./Script/Restore.ps1`
	1. 放在数据库服务器上执行
	2. BackupFolder 是备份的源数据
	3. DataFolder 是SQLServer的安装路径（脚本中的安装路径是`C:\Program Files\Microsoft SQL Server`自行寻找自己的路径）
3.  `./Script/SqlServer全量备份和还原.md` 该数据库脚本可以直接在SSMS执行，但是只能备份/还原 数据库。
4.  数据库的管理员账号 000000/000000

## 新增数据库要求
   新增的数据库库必须执行 `./ADDDataBase/CreateDDLChangeLogTable.sql` 和 `./ADDDataBase/CreateTrackDDLChanges.sql` 保存表结构/存储过程变更的表和触发器

## 数据库的变更
 1. 以1.8为数据库原型，后续版本不提供数据库的完整备份，都是使用数据库变更文件进行执行
 2. 使用脚本 `./Script/GetChange.ps1` 进行对数据库变更的收集
 	1. 修改文件的生成目录
 	2. 修改获取的时间段
 	3. 会获得一个如 `20241101_20241110`的文件夹，直接拖入对应的版本目录下提交即可。（如直接拖入V1.8文件夹）
 3. 使用脚本 `./Script/GetModuleChange.ps1` 进行对数据库菜单变更的收集
 4. 使用脚本 `./Script/Get_GLY_RoleAuthorize.ps1` 进行对数据库管理员角色的菜单变更的收集
 5. 使用脚本 `./Script/GetReportChange.ps1` 进行对报表的变更的收集
 6. 使用脚本 `./Script/ExecSqlInFolder.ps1` 执行一个文件夹下的所有文件
 
## 数据库的清理（敬请期待）
   为方便用户体验查看，当前数据库有少量业务数据，可使用如下脚本进行清理