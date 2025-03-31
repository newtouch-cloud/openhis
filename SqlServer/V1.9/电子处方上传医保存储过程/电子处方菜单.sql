insert into NewtouchHIS_Base.[dbo].[Sys_Module](Id,ParentId,name,UrlAddress,Target,px,CreateTime,CreatorCode,zt)
select '03FA43D8-A5B9-4498-8296-B1A0BE9677BA','1','电子处方药品管理',' /SysMedicineElectronicPrescription/Index','iframe','5',GETDATE(),'admin','1';

INSERT INTO [NewtouchHIS_Base].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES (NEWID(), '1f827896-8a31-46f7-966c-26a1dbc5d28f', '03FA43D8-A5B9-4498-8296-B1A0BE9677BA', '',GETDATE(), '000000', '', '', '1');
