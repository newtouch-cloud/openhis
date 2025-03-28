USE [NewtouchHIS_Sett]
GO

/****** Object:  Table [dbo].[Dzcf_D003_output]   
电子处方上传出参落地表
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


