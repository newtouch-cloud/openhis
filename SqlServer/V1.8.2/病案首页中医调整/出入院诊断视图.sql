USE [Newtouch_EMR]
GO

/****** Object:  View [dbo].[V_HIS_InpPatDiag]    Script Date: 2024/11/14 11:21:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

alter view [dbo].[V_HIS_InpPatDiag]
as
select a.OrganizeId,a.ZYH,a.zddm JBDM ,a.zdmc JBMC,zdlb,case when zdlx='0' then '1' when zdlx>2 then '2' else zdlx end zdlx
,isnull(px,1) px,isnull(zddl,'WM') zddl
from  [Newtouch_CIS].dbo.zy_PatDxInfo a with(nolock)
where a.zt='1'
GO


