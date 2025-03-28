USE [NewtouchHIS_Sett]
GO

/****** Object:  Table [dbo].[Dzcf_CFYP_output] 

Script Date: 2024/10/21 11:25:14 
remark:医保电子处方药品目录落地表
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


