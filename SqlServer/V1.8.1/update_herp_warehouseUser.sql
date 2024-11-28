USE [NewtouchHIS_herp]
GO
DELETE FROM [dbo].[rel_warehouseUser]
GO
INSERT [dbo].[rel_warehouseUser] ([Id], [OrganizeId], [warehouseId], [gh], [userName], [zt], [CreatorCode], [CreateTime], [LastModifyTime], [LastModifierCode]) VALUES (N'81b8bab3-638e-4305-b087-b141d3aab88e', N'6d5752a7-234a-403e-aa1c-df8b45d3469f', N'9E645D49-AE90-4D9B-BDB0-364BEA48AB77', N'000000', N'管理员', N'1', N'xzadmin', CAST(N'2024-11-12T10:40:31.277' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[rel_warehouseUser] ([Id], [OrganizeId], [warehouseId], [gh], [userName], [zt], [CreatorCode], [CreateTime], [LastModifyTime], [LastModifierCode]) VALUES (N'ab4cd44a-d1c0-43be-b8b7-c70dad1dd03c', N'6d5752a7-234a-403e-aa1c-df8b45d3469f', N'9E645D49-AE90-4D9B-BDB0-364BEA48AB77', N'xzadmin', N'新致演示用户', N'1', N'xzadmin', CAST(N'2024-11-12T10:40:31.277' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[rel_warehouseUser] ([Id], [OrganizeId], [warehouseId], [gh], [userName], [zt], [CreatorCode], [CreateTime], [LastModifyTime], [LastModifierCode]) VALUES (N'be04dd7a-2e31-4c94-a888-dd4253edeb6a', N'd15b5415-c3c3-4815-a702-570ae07afc16', N'1D7F9DFE-EBD0-44AE-A4BA-F26AB098DAFF', N'admin', N'admin', N'1', N'MDKyk01', CAST(N'2020-12-09T14:38:34.053' AS DateTime), NULL, NULL)
GO
