USE [Newtouch_CIS]
GO

-- 事件类型: CREATE_TABLE
-- 变更时间: 11/19/2024 17:09:58
CREATE TABLE dbo.Tmp_zy_PatDxInfo
	(
	Id varchar(50) NOT NULL,
	OrganizeId varchar(50) NOT NULL,
	zyh varchar(50) NOT NULL,
	zddl varchar(5) NULL,
	zdlb varchar(10) NOT NULL,
	zdlx varchar(10) NOT NULL,
	zddm varchar(50) NOT NULL,
	zdmc varchar(200) NULL,
	zdyzdmc varchar(200) NULL,
	CreateTime datetime NOT NULL,
	CreatorCode varchar(50) NOT NULL,
	LastModifyTime datetime NULL,
	LastModifierCode varchar(50) NULL,
	zt char(1) NOT NULL,
	cyqk int NULL,
	px int NULL
	)  ON [PRIMARY]
