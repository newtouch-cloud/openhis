-----------------------------------
INSERT INTO [NewtouchHIS_Sett].[dbo].[Sys_ConfigTemplate]
    ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
SELECT 
    NEWID() AS [Id],
    sc.[Code],
    sc.[Name],
    sc.[Memo],
    sc.[CreateTime],
    sc.[CreatorCode],
    sc.[LastModifyTime],
    sc.[LastModifierCode],
    sc.[zt],
    sc.[px],
    sc.Value AS [DefaultVal]
FROM 
    [NewtouchHIS_Sett].[dbo].[Sys_Config] sc
LEFT JOIN 
    [NewtouchHIS_Sett].[dbo].[Sys_ConfigTemplate] sct
    ON sc.[Code] = sct.[Code]
WHERE 
    sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
    AND sct.[Code] IS NULL;


--------------------------------
INSERT INTO [Newtouch_CIS].[dbo].[Sys_ConfigTemplate]
    ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
SELECT 
    NEWID() AS [Id],
    sc.[Code],
    sc.[Name],
    sc.[Memo],
    sc.[CreateTime],
    sc.[CreatorCode],
    sc.[LastModifyTime],
    sc.[LastModifierCode],
    sc.[zt],
    sc.[px],
    sc.Value AS [DefaultVal]
FROM 
    [Newtouch_CIS].[dbo].[Sys_Config] sc
LEFT JOIN 
    [Newtouch_CIS].[dbo].[Sys_ConfigTemplate] sct
    ON sc.[Code] = sct.[Code]
WHERE 
    sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
    AND sct.[Code] IS NULL;


----------------------------------------
INSERT INTO [NewtouchHIS_PDS].[dbo].[Sys_ConfigTemplate]
    ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
SELECT 
    NEWID() AS [Id],
    sc.[Code],
    sc.[Name],
    sc.[Memo],
    sc.[CreateTime],
    sc.[CreatorCode],
    sc.[LastModifyTime],
    sc.[LastModifierCode],
    sc.[zt],
    sc.[px],
    sc.Value AS [DefaultVal]
FROM 
    [NewtouchHIS_PDS].[dbo].[Sys_Config] sc
LEFT JOIN 
    [NewtouchHIS_PDS].[dbo].[Sys_ConfigTemplate] sct
    ON sc.[Code] = sct.[Code]
WHERE 
    sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
    AND sct.[Code] IS NULL;

INSERT INTO [Newtouch_EMR].[dbo].[Sys_ConfigTemplate]
    ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
SELECT 
    NEWID() AS [Id],
    sc.[Code],
    sc.[Name],
    sc.[Memo],
    sc.[CreateTime],
    sc.[CreatorCode],
    sc.[LastModifyTime],
    sc.[LastModifierCode],
    sc.[zt],
    sc.[px],
    sc.Value AS [DefaultVal]
FROM 
    [Newtouch_EMR].[dbo].[Sys_Config] sc
LEFT JOIN 
    [Newtouch_EMR].[dbo].[Sys_ConfigTemplate] sct
    ON sc.[Code] = sct.[Code]
WHERE 
    sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
    AND sct.[Code] IS NULL;


-----------------------------------
INSERT INTO [Newtouch_EMR].[dbo].[Sys_ConfigTemplate]
    ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
SELECT 
    NEWID() AS [Id],
    sc.[Code],
    sc.[Name],
    sc.[Memo],
    sc.[CreateTime],
    sc.[CreatorCode],
    sc.[LastModifyTime],
    sc.[LastModifierCode],
    sc.[zt],
    sc.[px],
    sc.Value AS [DefaultVal]
FROM 
    [Newtouch_EMR].[dbo].[Sys_Config] sc
LEFT JOIN 
    [Newtouch_EMR].[dbo].[Sys_ConfigTemplate] sct
    ON sc.[Code] = sct.[Code]
WHERE 
    sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
    AND sct.[Code] IS NULL;


-----------------------------
INSERT INTO [Newtouch_MRMS].[dbo].[Sys_ConfigTemplate]
    ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
SELECT 
    NEWID() AS [Id],
    sc.[Code],
    sc.[Name],
    sc.[Memo],
    sc.[CreateTime],
    sc.[CreatorCode],
    sc.[LastModifyTime],
    sc.[LastModifierCode],
    sc.[zt],
    sc.[px],
    sc.Value AS [DefaultVal]
FROM 
    [Newtouch_MRMS].[dbo].[Sys_Config] sc
LEFT JOIN 
    [Newtouch_MRMS].[dbo].[Sys_ConfigTemplate] sct
    ON sc.[Code] = sct.[Code]
WHERE 
    sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
    AND sct.[Code] IS NULL;


-------------------------------
INSERT INTO [Newtouch_MRQC].[dbo].[Sys_ConfigTemplate]
    ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
SELECT 
    NEWID() AS [Id],
    sc.[Code],
    sc.[Name],
    sc.[Memo],
    sc.[CreateTime],
    sc.[CreatorCode],
    sc.[LastModifyTime],
    sc.[LastModifierCode],
    sc.[zt],
    sc.[px],
    sc.Value AS [DefaultVal]
FROM 
    [Newtouch_MRQC].[dbo].[Sys_Config] sc
LEFT JOIN 
    [Newtouch_MRQC].[dbo].[Sys_ConfigTemplate] sct
    ON sc.[Code] = sct.[Code]
WHERE 
    sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
    AND sct.[Code] IS NULL;


------------------------------------------
INSERT INTO [Newtouch_OR].[dbo].[Sys_ConfigTemplate]
    ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
SELECT 
    NEWID() AS [Id],
    sc.[Code],
    sc.[Name],
    sc.[Memo],
    sc.[CreateTime],
    sc.[CreatorCode],
    sc.[LastModifyTime],
    sc.[LastModifierCode],
    sc.[zt],
    sc.[px],
    sc.Value AS [DefaultVal]
FROM 
    [Newtouch_OR].[dbo].[Sys_Config] sc
LEFT JOIN 
    [Newtouch_OR].[dbo].[Sys_ConfigTemplate] sct
    ON sc.[Code] = sct.[Code]
WHERE 
    sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
    AND sct.[Code] IS NULL;


-----------------------------------------------
INSERT INTO [Newtouch_Union].[dbo].[Sys_ConfigTemplate]
    ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
SELECT 
    NEWID() AS [Id],
    sc.[Code],
    sc.[Name],
    sc.[Memo],
    sc.[CreateTime],
    sc.[CreatorCode],
    sc.[LastModifyTime],
    sc.[LastModifierCode],
    sc.[zt],
    sc.[px],
    sc.Value AS [DefaultVal]
FROM 
    [Newtouch_Union].[dbo].[Sys_Config] sc
LEFT JOIN 
    [Newtouch_Union].[dbo].[Sys_ConfigTemplate] sct
    ON sc.[Code] = sct.[Code]
WHERE 
    sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
    AND sct.[Code] IS NULL;


----------------------------------------------
INSERT INTO [NewtouchHIS_herp].[dbo].[Sys_ConfigTemplate]
    ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
SELECT 
    NEWID() AS [Id],
    sc.[Code],
    sc.[Name],
    sc.[Memo],
    sc.[CreateTime],
    sc.[CreatorCode],
    sc.[LastModifyTime],
    sc.[LastModifierCode],
    sc.[zt],
    sc.[px],
    sc.Value AS [DefaultVal]
FROM 
    [NewtouchHIS_herp].[dbo].[Sys_Config] sc
LEFT JOIN 
    [NewtouchHIS_herp].[dbo].[Sys_ConfigTemplate] sct
    ON sc.[Code] = sct.[Code]
WHERE 
    sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
    AND sct.[Code] IS NULL;