-- 医保电子处方目录落地表
USE [NewtouchHIS_Sett]
GO

/****** Object:  Table [dbo].[Dzcf_CFYP_output] 

Script Date: 2024/10/21 11:25:14 
remark:Ò½±£µç×Ó´¦·½Ò©Æ·Ä¿Â¼ÂäµØ±í
******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Dzcf_CFYP_output](
	[cfypcode] [int] IDENTITY(1,1) NOT NULL,
	[medListCodg] [varchar](500) NULL,
	[natDrugNo] [varchar](500) NULL,
	[genname] [varchar](500) NULL,
	[prodname] [varchar](500) NULL,
	[regName] [varchar](500) NULL,
	[listType] [varchar](500) NULL,
	[listTypeName] [varchar](500) NULL,
	[specName] [varchar](500) NULL,
	[prdrName] [varchar](500) NULL,
	[aprvno] [varchar](500) NULL,
	[dosformName] [varchar](500) NULL,
	[minPacunt] [varchar](500) NULL,
	[minPacCnt] [varchar](500) NULL,
	[minPrepunt] [varchar](500) NULL,
	[poolareaNo] [varchar](500) NULL,
	[poolareaName] [varchar](500) NULL,
	[dualchnlFlag] [varchar](500) NULL,
	[oppoolFlag] [varchar](500) NULL,
	[begntime] [varchar](500) NULL,
	[endtime] [varchar](500) NULL,
	[dj] [decimal](18, 2) NOT NULL,
	[py] [varchar](500) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Dzcf_CFYP_output] ADD  CONSTRAINT [DF_Dzcf_CFYP_output_dj]  DEFAULT ((0.01)) FOR [dj]
GO


-- 电子处方上传预核验出参落地
USE [NewtouchHIS_Sett]
GO

/****** Object:  Table [dbo].[Dzcf_D001_output]    
remark:µç×Ó´¦·½ÉÏ´«Ô¤ºËÑé³ö²ÎÂäµØ
Script Date: 2024/10/21 11:27:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Dzcf_D001_output](
	[mzh] [varchar](100) NULL,
	[cfh] [varchar](100) NULL,
	[OrganizeId] [varchar](100) NULL,
	[InputContent] [varchar](max) NULL,
	[rxTraceCode] [varchar](500) NULL,
	[hiRxno] [varchar](500) NULL,
	[czydm] [varchar](50) NULL,
	[czrq] [datetime] NULL,
	[zt] [int] NULL,
	[zt_czy] [varchar](50) NULL,
	[zt_rq] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


-- 电子处方医保电子签名出参落地
USE [NewtouchHIS_Sett]
GO

/****** Object:  Table [dbo].[Dzcf_D002_output]    
remark:µç×Ó´¦·½Ò½±£µç×ÓÇ©Ãû³ö²ÎÂäµØ
Script Date: 2024/10/21 11:28:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Dzcf_D002_output](
	[mzh] [varchar](100) NULL,
	[cfh] [varchar](100) NULL,
	[OrganizeId] [varchar](100) NULL,
	[InputContent] [varchar](max) NULL,
	[originalValue] [varchar](max) NULL,
	[originalRxFile] [varchar](max) NULL,
	[rxFile] [varchar](max) NULL,
	[signDigest] [varchar](max) NULL,
	[signCertSn] [varchar](1000) NULL,
	[signCertDn] [varchar](1000) NULL,
	[czydm] [varchar](50) NULL,
	[czrq] [datetime] NULL,
	[zt] [int] NULL,
	[zt_czy] [varchar](50) NULL,
	[zt_rq] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


-- 电子处方上传出参落地表
USE [NewtouchHIS_Sett]
GO

/****** Object:  Table [dbo].[Dzcf_D003_output]   
µç×Ó´¦·½ÉÏ´«³ö²ÎÂäµØ±í
Script Date: 2024/10/21 11:29:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Dzcf_D003_output](
	[mzh] [varchar](100) NULL,
	[cfh] [varchar](100) NULL,
	[OrganizeId] [varchar](100) NULL,
	[InputContent] [varchar](max) NULL,
	[rxTraceCode] [varchar](500) NULL,
	[hiRxno] [varchar](500) NULL,
	[rxStasCodg] [varchar](500) NULL,
	[rxStasName] [varchar](500) NULL,
	[cxyy] [varchar](5000) NULL,
	[cxsj] [varchar](500) NULL,
	[czydm] [varchar](50) NULL,
	[czrq] [datetime] NULL,
	[zt] [int] NULL,
	[zt_czy] [varchar](50) NULL,
	[zt_rq] [datetime] NULL,
	[rxChkStasCodg] [varchar](500) NULL,
	[rxChkOpnn] [varchar](500) NULL,
	[rxChkTime] [varchar](500) NULL,
	[rxChkStasName] [varchar](500) NULL,
	[rxUsedStasCodg] [varchar](500) NULL,
	[rxUsedStasName] [varchar](500) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


-- 电子处方开立脚本
alter table Newtouch_CIS..xt_cf add isdzcf varchar(5)
alter table Newtouch_CIS..xt_cf add ysshyj varchar(30)
alter table Newtouch_CIS..xt_cfmx add gjybdm varchar(100)
