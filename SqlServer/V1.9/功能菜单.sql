select top 100 * from NewtouchHIS_Sett.[dbo].[Sys_Module] where name in ('医保进销存查询','监管平台信息查询')
select top 100 * from NewtouchHIS_Base.[dbo].[Sys_Module] where name in ('基础信息公共库')


--insert into NewtouchHIS_Base.[dbo].[Sys_Module](Id,ParentId,name,UrlAddress,Target,px,CreateTime,CreatorCode,zt)
--select NEWID(),'1','基础信息公共库','/CommonLibrary/Index','iframe','5',GETDATE(),'admin','1'
--insert into NewtouchHIS_Sett.[dbo].[Sys_Module](Id,ParentId,name,UrlAddress,Target,px,CreateTime,CreatorCode,zt)
--select NEWID(),'1','监管平台信息查询','/JGManage/JGTradUpload/Index','iframe','5',GETDATE(),'admin','1'
--insert into NewtouchHIS_Sett.[dbo].[Sys_Module](Id,ParentId,name,UrlAddress,Target,px,CreateTime,CreatorCode,zt)
--select NEWID(),'1','医保进销存查询','/DRGManage/DRGTradUpload/Index','iframe','5',GETDATE(),'admin','1'