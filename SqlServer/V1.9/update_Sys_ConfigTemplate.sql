-----------------------------------
MERGE INTO [NewtouchHIS_Sett].[dbo].[Sys_ConfigTemplate] AS target
USING (
    SELECT 
        NEWID() AS [Id], -- ����ʱ������ ID
        sc.[Code],
        sc.[Name],
        sc.[Memo],
        sc.[CreateTime],  -- �õ�ǰʱ��
        sc.[CreatorCode],
        sc. [LastModifyTime],  -- �õ�ǰʱ��
        sc.[LastModifierCode],
        sc.[zt],
        sc.[px],
        sc.[Value] AS [DefaultVal]
    FROM [NewtouchHIS_Sett].[dbo].[Sys_Config] sc
    WHERE sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
) AS source
ON target.[Code] = source.[Code]  -- �� Code ��Ϊƥ������
WHEN MATCHED THEN 
    -- **��� Code ���ڣ���������**
    UPDATE SET 
        target.[Name] = source.[Name],
        target.[Memo] = source.[Memo],
        target.[LastModifyTime] = source.[LastModifyTime], 
        target.[LastModifierCode] = source.[LastModifierCode],
        target.[zt] = source.[zt],
        target.[px] = source.[px],
        target.[DefaultVal] = source.[DefaultVal]
WHEN NOT MATCHED THEN 
    -- **��� Code �����ڣ���������**
    INSERT ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], 
            [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
    VALUES (source.[Id], source.[Code], source.[Name], source.[Memo], 
            source.[CreateTime], source.[CreatorCode], source.[LastModifyTime], 
            source.[LastModifierCode], source.[zt], source.[px], source.[DefaultVal]);

--------------------------------
MERGE INTO [Newtouch_CIS].[dbo].[Sys_ConfigTemplate] AS target
USING (
    SELECT 
        NEWID() AS [Id], -- ����ʱ������ ID
        sc.[Code],
        sc.[Name],
        sc.[Memo],
        sc.[CreateTime],  -- �õ�ǰʱ��
        sc.[CreatorCode],
        sc. [LastModifyTime],  -- �õ�ǰʱ��
        sc.[LastModifierCode],
        sc.[zt],
        sc.[px],
        sc.[Value] AS [DefaultVal]
    FROM [Newtouch_CIS].[dbo].[Sys_Config] sc
    WHERE sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
) AS source
ON target.[Code] = source.[Code]  -- �� Code ��Ϊƥ������
WHEN MATCHED THEN 
    -- **��� Code ���ڣ���������**
    UPDATE SET 
        target.[Name] = source.[Name],
        target.[Memo] = source.[Memo],
        target.[LastModifyTime] = source.[LastModifyTime], 
        target.[LastModifierCode] = source.[LastModifierCode],
        target.[zt] = source.[zt],
        target.[px] = source.[px],
        target.[DefaultVal] = source.[DefaultVal]
WHEN NOT MATCHED THEN 
    -- **��� Code �����ڣ���������**
    INSERT ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], 
            [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
    VALUES (source.[Id], source.[Code], source.[Name], source.[Memo], 
            source.[CreateTime], source.[CreatorCode], source.[LastModifyTime], 
            source.[LastModifierCode], source.[zt], source.[px], source.[DefaultVal]);

----------------------------------------
MERGE INTO [NewtouchHIS_PDS].[dbo].[Sys_ConfigTemplate] AS target
USING (
    SELECT 
        NEWID() AS [Id], -- ����ʱ������ ID
        sc.[Code],
        sc.[Name],
        sc.[Memo],
        sc.[CreateTime],  -- �õ�ǰʱ��
        sc.[CreatorCode],
        sc. [LastModifyTime],  -- �õ�ǰʱ��
        sc.[LastModifierCode],
        sc.[zt],
        sc.[px],
        sc.[Value] AS [DefaultVal]
    FROM [NewtouchHIS_PDS].[dbo].[Sys_Config] sc
    WHERE sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
) AS source
ON target.[Code] = source.[Code]  -- �� Code ��Ϊƥ������
WHEN MATCHED THEN 
    -- **��� Code ���ڣ���������**
    UPDATE SET 
        target.[Name] = source.[Name],
        target.[Memo] = source.[Memo],
        target.[LastModifyTime] = source.[LastModifyTime], 
        target.[LastModifierCode] = source.[LastModifierCode],
        target.[zt] = source.[zt],
        target.[px] = source.[px],
        target.[DefaultVal] = source.[DefaultVal]
WHEN NOT MATCHED THEN 
    -- **��� Code �����ڣ���������**
    INSERT ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], 
            [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
    VALUES (source.[Id], source.[Code], source.[Name], source.[Memo], 
            source.[CreateTime], source.[CreatorCode], source.[LastModifyTime], 
            source.[LastModifierCode], source.[zt], source.[px], source.[DefaultVal]);

----------------------------------------
MERGE INTO [Newtouch_EMR].[dbo].[Sys_ConfigTemplate] AS target
USING (
    SELECT 
        NEWID() AS [Id], -- ����ʱ������ ID
        sc.[Code],
        sc.[Name],
        sc.[Memo],
        sc.[CreateTime],  -- �õ�ǰʱ��
        sc.[CreatorCode],
        sc. [LastModifyTime],  -- �õ�ǰʱ��
        sc.[LastModifierCode],
        sc.[zt],
        sc.[px],
        sc.[Value] AS [DefaultVal]
    FROM [Newtouch_EMR].[dbo].[Sys_Config] sc
    WHERE sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
) AS source
ON target.[Code] = source.[Code]  -- �� Code ��Ϊƥ������
WHEN MATCHED THEN 
    -- **��� Code ���ڣ���������**
    UPDATE SET 
        target.[Name] = source.[Name],
        target.[Memo] = source.[Memo],
        target.[LastModifyTime] = source.[LastModifyTime], 
        target.[LastModifierCode] = source.[LastModifierCode],
        target.[zt] = source.[zt],
        target.[px] = source.[px],
        target.[DefaultVal] = source.[DefaultVal]
WHEN NOT MATCHED THEN 
    -- **��� Code �����ڣ���������**
    INSERT ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], 
            [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
    VALUES (source.[Id], source.[Code], source.[Name], source.[Memo], 
            source.[CreateTime], source.[CreatorCode], source.[LastModifyTime], 
            source.[LastModifierCode], source.[zt], source.[px], source.[DefaultVal]);


-----------------------------------
MERGE INTO [Newtouch_MRMS].[dbo].[Sys_ConfigTemplate] AS target
USING (
    SELECT 
        NEWID() AS [Id], -- ����ʱ������ ID
        sc.[Code],
        sc.[Name],
        sc.[Memo],
        sc.[CreateTime],  -- �õ�ǰʱ��
        sc.[CreatorCode],
        sc. [LastModifyTime],  -- �õ�ǰʱ��
        sc.[LastModifierCode],
        sc.[zt],
        sc.[px],
        sc.[Value] AS [DefaultVal]
    FROM [Newtouch_MRMS].[dbo].[Sys_Config] sc
    WHERE sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
) AS source
ON target.[Code] = source.[Code]  -- �� Code ��Ϊƥ������
WHEN MATCHED THEN 
    -- **��� Code ���ڣ���������**
    UPDATE SET 
        target.[Name] = source.[Name],
        target.[Memo] = source.[Memo],
        target.[LastModifyTime] = source.[LastModifyTime], 
        target.[LastModifierCode] = source.[LastModifierCode],
        target.[zt] = source.[zt],
        target.[px] = source.[px],
        target.[DefaultVal] = source.[DefaultVal]
WHEN NOT MATCHED THEN 
    -- **��� Code �����ڣ���������**
    INSERT ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], 
            [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
    VALUES (source.[Id], source.[Code], source.[Name], source.[Memo], 
            source.[CreateTime], source.[CreatorCode], source.[LastModifyTime], 
            source.[LastModifierCode], source.[zt], source.[px], source.[DefaultVal]);


-------------------------------
MERGE INTO [Newtouch_MRQC].[dbo].[Sys_ConfigTemplate] AS target
USING (
    SELECT 
        NEWID() AS [Id], -- ����ʱ������ ID
        sc.[Code],
        sc.[Name],
        sc.[Memo],
        sc.[CreateTime],  -- �õ�ǰʱ��
        sc.[CreatorCode],
        sc. [LastModifyTime],  -- �õ�ǰʱ��
        sc.[LastModifierCode],
        sc.[zt],
        sc.[px],
        sc.[Value] AS [DefaultVal]
    FROM [Newtouch_MRQC].[dbo].[Sys_Config] sc
    WHERE sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
) AS source
ON target.[Code] = source.[Code]  -- �� Code ��Ϊƥ������
WHEN MATCHED THEN 
    -- **��� Code ���ڣ���������**
    UPDATE SET 
        target.[Name] = source.[Name],
        target.[Memo] = source.[Memo],
        target.[LastModifyTime] = source.[LastModifyTime], 
        target.[LastModifierCode] = source.[LastModifierCode],
        target.[zt] = source.[zt],
        target.[px] = source.[px],
        target.[DefaultVal] = source.[DefaultVal]
WHEN NOT MATCHED THEN 
    -- **��� Code �����ڣ���������**
    INSERT ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], 
            [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
    VALUES (source.[Id], source.[Code], source.[Name], source.[Memo], 
            source.[CreateTime], source.[CreatorCode], source.[LastModifyTime], 
            source.[LastModifierCode], source.[zt], source.[px], source.[DefaultVal]);


------------------------------------------
MERGE INTO [Newtouch_OR].[dbo].[Sys_ConfigTemplate] AS target
USING (
    SELECT 
        NEWID() AS [Id], -- ����ʱ������ ID
        sc.[Code],
        sc.[Name],
        sc.[Memo],
        sc.[CreateTime],  -- �õ�ǰʱ��
        sc.[CreatorCode],
        sc. [LastModifyTime],  -- �õ�ǰʱ��
        sc.[LastModifierCode],
        sc.[zt],
        sc.[px],
        sc.[Value] AS [DefaultVal]
    FROM [Newtouch_OR].[dbo].[Sys_Config] sc
    WHERE sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
) AS source
ON target.[Code] = source.[Code]  -- �� Code ��Ϊƥ������
WHEN MATCHED THEN 
    -- **��� Code ���ڣ���������**
    UPDATE SET 
        target.[Name] = source.[Name],
        target.[Memo] = source.[Memo],
        target.[LastModifyTime] = source.[LastModifyTime], 
        target.[LastModifierCode] = source.[LastModifierCode],
        target.[zt] = source.[zt],
        target.[px] = source.[px],
        target.[DefaultVal] = source.[DefaultVal]
WHEN NOT MATCHED THEN 
    -- **��� Code �����ڣ���������**
    INSERT ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], 
            [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
    VALUES (source.[Id], source.[Code], source.[Name], source.[Memo], 
            source.[CreateTime], source.[CreatorCode], source.[LastModifyTime], 
            source.[LastModifierCode], source.[zt], source.[px], source.[DefaultVal]);


-----------------------------------------------
MERGE INTO [Newtouch_Union].[dbo].[Sys_ConfigTemplate] AS target
USING (
    SELECT 
        NEWID() AS [Id], -- ����ʱ������ ID
        sc.[Code],
        sc.[Name],
        sc.[Memo],
        sc.[CreateTime],  -- �õ�ǰʱ��
        sc.[CreatorCode],
        sc. [LastModifyTime],  -- �õ�ǰʱ��
        sc.[LastModifierCode],
        sc.[zt],
        sc.[px],
        sc.[Value] AS [DefaultVal]
    FROM [Newtouch_Union].[dbo].[Sys_Config] sc
    WHERE sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
) AS source
ON target.[Code] = source.[Code]  -- �� Code ��Ϊƥ������
WHEN MATCHED THEN 
    -- **��� Code ���ڣ���������**
    UPDATE SET 
        target.[Name] = source.[Name],
        target.[Memo] = source.[Memo],
        target.[LastModifyTime] = source.[LastModifyTime], 
        target.[LastModifierCode] = source.[LastModifierCode],
        target.[zt] = source.[zt],
        target.[px] = source.[px],
        target.[DefaultVal] = source.[DefaultVal]
WHEN NOT MATCHED THEN 
    -- **��� Code �����ڣ���������**
    INSERT ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], 
            [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
    VALUES (source.[Id], source.[Code], source.[Name], source.[Memo], 
            source.[CreateTime], source.[CreatorCode], source.[LastModifyTime], 
            source.[LastModifierCode], source.[zt], source.[px], source.[DefaultVal]);

----------------------------------------------
MERGE INTO [NewtouchHIS_herp].[dbo].[Sys_ConfigTemplate] AS target
USING (
    SELECT 
        NEWID() AS [Id], -- ����ʱ������ ID
        sc.[Code],
        sc.[Name],
        sc.[Memo],
        sc.[CreateTime],  -- �õ�ǰʱ��
        sc.[CreatorCode],
        sc. [LastModifyTime],  -- �õ�ǰʱ��
        sc.[LastModifierCode],
        sc.[zt],
        sc.[px],
        sc.[Value] AS [DefaultVal]
    FROM [NewtouchHIS_herp].[dbo].[Sys_Config] sc
    WHERE sc.[OrganizeId] = '6d5752a7-234a-403e-aa1c-df8b45d3469f'
) AS source
ON target.[Code] = source.[Code]  -- �� Code ��Ϊƥ������
WHEN MATCHED THEN 
    -- **��� Code ���ڣ���������**
    UPDATE SET 
        target.[Name] = source.[Name],
        target.[Memo] = source.[Memo],
        target.[LastModifyTime] = source.[LastModifyTime], 
        target.[LastModifierCode] = source.[LastModifierCode],
        target.[zt] = source.[zt],
        target.[px] = source.[px],
        target.[DefaultVal] = source.[DefaultVal]
WHEN NOT MATCHED THEN 
    -- **��� Code �����ڣ���������**
    INSERT ([Id], [Code], [Name], [Memo], [CreateTime], [CreatorCode], 
            [LastModifyTime], [LastModifierCode], [zt], [px], [DefaultVal])
    VALUES (source.[Id], source.[Code], source.[Name], source.[Memo], 
            source.[CreateTime], source.[CreatorCode], source.[LastModifyTime], 
            source.[LastModifierCode], source.[zt], source.[px], source.[DefaultVal]);