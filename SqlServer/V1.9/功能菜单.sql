select top 100 * from NewtouchHIS_Sett.[dbo].[Sys_Module] where name in ('ҽ���������ѯ','���ƽ̨��Ϣ��ѯ')
select top 100 * from NewtouchHIS_Base.[dbo].[Sys_Module] where name in ('������Ϣ������')


--insert into NewtouchHIS_Base.[dbo].[Sys_Module](Id,ParentId,name,UrlAddress,Target,px,CreateTime,CreatorCode,zt)
--select NEWID(),'1','������Ϣ������','/CommonLibrary/Index','iframe','5',GETDATE(),'admin','1'
--insert into NewtouchHIS_Sett.[dbo].[Sys_Module](Id,ParentId,name,UrlAddress,Target,px,CreateTime,CreatorCode,zt)
--select NEWID(),'1','���ƽ̨��Ϣ��ѯ','/JGManage/JGTradUpload/Index','iframe','5',GETDATE(),'admin','1'
--insert into NewtouchHIS_Sett.[dbo].[Sys_Module](Id,ParentId,name,UrlAddress,Target,px,CreateTime,CreatorCode,zt)
--select NEWID(),'1','ҽ���������ѯ','/DRGManage/DRGTradUpload/Index','iframe','5',GETDATE(),'admin','1'