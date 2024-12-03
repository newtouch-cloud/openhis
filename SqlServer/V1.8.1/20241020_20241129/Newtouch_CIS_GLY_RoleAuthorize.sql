DELETE FROM [Newtouch_CIS].[dbo].[Sys_RoleAuthorize]
WHERE RoleId = (
    SELECT Id 
    FROM [Newtouch_CIS].[dbo].[Sys_Role]
    WHERE OrganizeId = '6d5752a7-234a-403e-aa1c-df8b45d3469f' AND Name = '管理员'
);
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('00d95314-0dbd-4c94-b64c-e796dfc549aa', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'e91e2f1c-98ce-4d09-9e93-11e6b06cbec3', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('019c32da-4b5f-482b-9b69-e482ffb3ae99', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'aacda3bc-c2a0-42c5-b0bd-20a2fbc1419d', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('0336b1df-d867-422a-ac6d-744354c0136f', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'cbedbc30-49cf-403e-a30d-a245bc6fa25c', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('0d6154c3-63f6-4688-8bf6-b4174803f127', 'a2d5efe6-24c4-4893-a804-abd66beec991', '1', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('1e63e3db-aaec-4c27-b562-22c02a39cdd4', 'a2d5efe6-24c4-4893-a804-abd66beec991', '9ff56939-e6d9-4387-b195-d8d80ff2e65e', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('1e92cc65-6058-499f-b42c-bb4aabcfc184', 'a2d5efe6-24c4-4893-a804-abd66beec991', '18d68872-7ae2-4913-9f7f-9dbb0cdac820', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('2feb482b-9e6d-44df-bb4c-ff779d246254', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'e2a9dcdc-5544-40be-870a-a48b1bb46a64', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('3c2d73cc-3df4-4dab-85fe-99f299fa1793', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'cb7145fc-3925-4017-ada7-df6658c4ae8d', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('3c383d50-80cb-45fa-915e-9c90389cc2dd', 'a2d5efe6-24c4-4893-a804-abd66beec991', '4b5bd543-cabf-4185-9749-ada3fa7f4270', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('3d2bde91-456e-48cb-a9cf-d07cac01b08e', 'a2d5efe6-24c4-4893-a804-abd66beec991', '0cad71db-ddc9-4ab2-a4c2-8e05c99304a2', '', '2024-10-21 11:08:05.073', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('420faaf6-2179-4f69-899f-2c386eaa263f', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'f3bf07b8-2b40-497d-83d6-a09594cd0be8', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('4ae57fa3-457c-4ecb-8dc5-93c9eb30809f', 'a2d5efe6-24c4-4893-a804-abd66beec991', '2922513b-e4ff-4645-8c7f-9a241ff8e46b', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('4e7b4853-9b24-47aa-9217-cf632f2dd24a', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'e9f868f1-e104-4892-8f03-8de9a8e7b3db', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('4ffe710a-4824-4a0c-bfb9-6d335bb53ef2', 'a2d5efe6-24c4-4893-a804-abd66beec991', '2c1221e3-2921-4913-967d-d67a286f2a1b', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('51d12c25-6dba-4faa-b059-f7efcaa972ef', 'a2d5efe6-24c4-4893-a804-abd66beec991', '89a5fd47-5679-441c-b34c-1b4d968b65c6', '', '2024-10-21 11:08:05.073', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('539371ba-c122-4572-868c-0f659f4f3869', 'a2d5efe6-24c4-4893-a804-abd66beec991', '2', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('54f837f9-0afa-4a4c-b998-7c795023c2cc', 'a2d5efe6-24c4-4893-a804-abd66beec991', '44e0024d-f544-4cba-aaeb-02dbf06acd33', '', '2024-10-21 11:08:05.073', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('5a2ab2b5-235b-48ae-a4d9-4c43230097ac', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'ea43f30c-3f75-4696-a60d-e1d2405e7351', '', '2024-10-21 11:08:05.073', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('5b3434e8-990c-416b-a0c8-5a74cd6fb09b', 'a2d5efe6-24c4-4893-a804-abd66beec991', '7c23f256-c1e5-4bb6-9c5a-3e31e2e47512', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('5b979faa-9adc-4e03-bbfc-a1aea1682940', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'd039d28f-62fa-4e63-bda2-23f56423a7ca', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('5faf9a00-6ef6-4803-8931-60749e682c16', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'e3903c39-9c8d-48a3-83db-e6d499294c19', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('60d2781b-df66-41f9-8675-2bf3ab201727', 'a2d5efe6-24c4-4893-a804-abd66beec991', '706da95d-5515-4854-97ae-d1825674920f', '', '2024-10-21 11:08:05.073', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('62acea71-445c-4022-9c37-aa1cf8efa887', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'f9a033ce-3574-4710-af93-17938cc1ccaa', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('63f377d8-d6ca-4060-b82b-247a70dd3970', 'a2d5efe6-24c4-4893-a804-abd66beec991', '9d4d2ca3-ca01-4450-8895-88ed51e6112b', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('6ec53e83-4729-4ad6-90ef-98c9a60cf0e0', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'c11271b2-8ffe-459d-bf54-515f30cba23d', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('70e9d077-5699-4bed-b3c1-63b0b8bad613', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'dd2b2a17-9306-4617-b70c-efd8483688b3', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('73546488-5a83-4166-90b6-bf4ec9615310', 'a2d5efe6-24c4-4893-a804-abd66beec991', '106ca787-08ab-42be-9d62-e9ba3cfa84a4', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('778aa0a9-e258-402c-8775-a1e04ab9f261', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'c6b4497b-d863-40e1-8502-f0a3af48fcae', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('7ccba8f9-0c4a-4538-92b0-4f7326e100b4', 'a2d5efe6-24c4-4893-a804-abd66beec991', '3123ff13-5ef2-4b5c-8c3b-76289f4313e8', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('810413b7-168d-433d-bac6-7b1a54080f68', 'a2d5efe6-24c4-4893-a804-abd66beec991', '30d2b894-6d8e-4a49-b880-7329832e36c6', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('841b4d88-a316-48e4-9520-cbc64ed80b2f', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'b316e1fc-adbf-44c1-86db-48ec5ed1c798', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('87d676f6-1a7d-4e95-b582-fb4398af0bf1', 'a2d5efe6-24c4-4893-a804-abd66beec991', '3dfe68cd-1166-4149-b787-f83c0c421a69', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('95d6ec56-10ad-46ff-ab76-4286efd5a32a', 'a2d5efe6-24c4-4893-a804-abd66beec991', '15619628-617e-47b6-a1b4-422ac6bea0e6', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('9c683249-8c1f-4ab3-b21a-b52573ebf9b6', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'ad39502d-69a9-46d4-a617-cddcf0b2daff', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('9e391403-ae22-425f-ac4f-e0c555f74fd4', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'e0f986ef-e3fe-4322-a51d-f4a327714beb', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('aaa2a7fd-436e-44bb-b49c-b18b83673363', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'a84f299e-5f33-434f-9ef0-d644715fb808', '', '2024-10-21 11:08:05.073', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('b41ee444-061a-4e59-b74a-7d0c89ddb344', 'a2d5efe6-24c4-4893-a804-abd66beec991', '14ac56d4-7e26-4552-aaac-e00111769acb', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('b860b5e0-d73e-4739-8835-0e3946970d0f', 'a2d5efe6-24c4-4893-a804-abd66beec991', '5f25f9f9-7de0-448e-93be-b9e6851c57b9', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('b9e7f36e-461d-48a6-979d-7636c7ce32d9', 'a2d5efe6-24c4-4893-a804-abd66beec991', '432341e9-9c52-4d36-9a1d-dbd687884486', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('bb5e0a7f-1390-48e2-a5c8-a27ea7bce982', 'a2d5efe6-24c4-4893-a804-abd66beec991', '766e304b-a76f-478f-8b4e-5f247c974c8f', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('bd56a3f7-f2f2-4ca7-ae22-370799ece3c7', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'ed2d0140-e45c-4450-9739-090f3f35711a', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('c3260a05-759d-4b7e-83dd-e64a07a481d4', 'a2d5efe6-24c4-4893-a804-abd66beec991', '7e442d0b-f9e1-4f96-b55d-4b94c0a0796e', '', '2024-10-21 11:08:05.073', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('c65f7cfb-9c4c-497c-91e7-8ea9a5eb1906', 'a2d5efe6-24c4-4893-a804-abd66beec991', '0598a674-e1f2-4b4c-8cd7-da4d02ae38e7', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('d1bdb4ba-8bd6-4b24-b2ab-81568560a87a', 'a2d5efe6-24c4-4893-a804-abd66beec991', '3909b521-f8c1-4169-9cfc-3dc9704bbd97', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('d9054468-888d-4704-9372-4f0641273c24', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'b1d0f117-2f74-415e-9b88-7435a3ea57fc', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('dbfbf54b-29a6-4cdf-be6d-19f5af37ef4e', 'a2d5efe6-24c4-4893-a804-abd66beec991', '6e562c5a-4cb6-4252-b257-2560a5eb6948', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('dc4eb9bc-3568-4762-989c-7c62664ec137', 'a2d5efe6-24c4-4893-a804-abd66beec991', '3e8ff835-2206-4974-ab5b-e1b2f3af86fb', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('dc7f75f9-af01-40cb-86e6-22e369496f07', 'a2d5efe6-24c4-4893-a804-abd66beec991', '0bcd4e12-9e73-43ee-baab-3496e7d5ebeb', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('dd50cfb3-5a66-4006-8bbe-491987aa3cb9', 'a2d5efe6-24c4-4893-a804-abd66beec991', '1ee89ab6-5536-440e-a5e5-5703d1b792fd', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('ebc7f65b-b031-4a0b-8dd7-56be4b4d7414', 'a2d5efe6-24c4-4893-a804-abd66beec991', '93ae5fe2-1382-47d2-b5cc-f3caaaec0487', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('ee23bbca-19bf-45a4-8d93-643d4dad902a', 'a2d5efe6-24c4-4893-a804-abd66beec991', '2cc00150-1263-4fb0-b81d-fff88532c36c', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('f0d1f78e-b71f-49ff-bb59-0e04607e3eb5', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'b0db35b6-05cb-4461-ac99-5aaee314a3bb', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('f12da00b-8c48-41cd-bcc4-0e0e9dfa2f11', 'a2d5efe6-24c4-4893-a804-abd66beec991', '0b40c37b-615f-49aa-bd66-efae1854fd6d', '', '2024-10-21 11:08:05.073', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('f37c65d5-334b-4c50-aaf3-2592edb0e963', 'a2d5efe6-24c4-4893-a804-abd66beec991', '84dc1ab7-feda-4141-8f0b-5d780e44e8f8', '', '2024-10-21 11:08:05.073', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('f42ad3af-a475-42e4-8013-9012be92856b', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'b60ce6f8-3bf1-4942-ae4c-024026fcd00c', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('fad20207-740d-4bb1-ae38-63d3bb443d7b', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'be7f8895-9257-4c1a-938a-537caaec266d', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('2892a880-0c08-4dfc-b767-25deb2edc1f7', 'a2d5efe6-24c4-4893-a804-abd66beec991', '40aed36d-9556-434f-b132-ace7772533b7', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('fc91c6d1-8e54-497f-a35a-e0f52507d608', 'a2d5efe6-24c4-4893-a804-abd66beec991', '18c6e18e-7a69-43ea-a04a-927c3897a6fa', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('115861ff-c983-4db3-bafc-2277b8a583e8', 'a2d5efe6-24c4-4893-a804-abd66beec991', '8468231f-1ee4-4926-a74a-d9bcf48124c9', '', '2024-10-21 11:08:05.073', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('15daa16f-37d1-4d40-a65b-8eefcc079a87', 'a2d5efe6-24c4-4893-a804-abd66beec991', '19c1763e-70ed-419d-bb2f-af8a16db3486', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('1933e0b0-a057-4c5b-94fb-22f591167a1c', 'a2d5efe6-24c4-4893-a804-abd66beec991', '63eb9fff-780b-40ea-b3bf-b4ef26efd4d6', '', '2024-10-21 11:08:05.073', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('1a79ed6e-79a7-436c-988f-c35e1139bc77', 'a2d5efe6-24c4-4893-a804-abd66beec991', '25941fbe-d059-4fa7-9f63-999a7db37af1', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('1c502681-c024-4b75-ada2-efe93d38f142', 'a2d5efe6-24c4-4893-a804-abd66beec991', '22fc0466-e626-4edc-9edb-86ba949e58cb', '', '2024-10-21 11:08:05.073', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('1cce22c7-e255-4055-9b3d-74363ded7cbe', 'a2d5efe6-24c4-4893-a804-abd66beec991', 'cb8eace9-dfc8-4400-90b6-2a5fa81cd005', '', '2024-10-21 11:08:05.087', '000000', '', '', '1');
INSERT INTO [Newtouch_CIS].[dbo].[Sys_RoleAuthorize] (Id, RoleId, ItemId, px, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt)
VALUES ('1d96f01a-9ac7-494b-9f61-ec9e6c303a5c', 'a2d5efe6-24c4-4893-a804-abd66beec991', '41088b97-cc81-45f6-b7c3-31bc77b66ad9', '', '2024-10-21 11:08:05.073', '000000', '', '', '1');

