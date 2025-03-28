USE [NewtouchHIS_Sett]
GO

/****** Object:  Table [dbo].[Dzcf_D001_output]    
remark:电子处方上传预核验出参落地
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


