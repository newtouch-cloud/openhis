USE [Newtouch_EMR]
GO

-- 事件类型: CREATE_TABLE
-- 变更时间: 11/19/2024 16:48:49
CREATE TABLE dbo.Tmp_mr_basy_zd
	(
	Id varchar(50) NOT NULL,
	BAH varchar(100) NOT NULL,
	ZYH varchar(50) NOT NULL,
	ZDLB varchar(5) NULL,
	ZDLX varchar(5) NULL,
	ZDOrder int NOT NULL,
	JBDM varchar(100) NULL,
	JBMC varchar(100) NULL,
	RYBQ varchar(100) NULL,
	RYBQMS varchar(200) NULL,
	CYQK varchar(100) NULL,
	CYQKMS varchar(200) NULL,
	zt char(1) NOT NULL,
	CreateTime datetime NOT NULL,
	CreatorCode varchar(50) NOT NULL,
	LastModifyTime datetime NULL,
	LastModifierCode varchar(50) NULL,
	OrganizeId varchar(50) NULL
	)  ON [PRIMARY]
