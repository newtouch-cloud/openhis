USE [NewtouchHIS_Sett]
GO

/****** Object:  Table [dbo].[Dzcf_D002_output]    
remark:电子处方医保电子签名出参落地
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


