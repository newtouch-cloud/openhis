USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_ElectronicPrescription_D006_Prescription]    Script Date: 2024/11/4 15:11:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/**
【Ld7805】电子处方审核结果查询
*/

CREATE proc [dbo].[usp_ElectronicPrescription_D006_Prescription]
	@orgId varchar(500),
	@hisId varchar(500),
	@cfh varchar(500),
	@fixmedinsCode varchar(500)
as
begin
select 
D003.hiRxno hiRxno 	--医保处方编号 	字符型 	30 	 	Y 	 
,@fixmedinsCode fixmedinsCode 	--定点医疗机构编号 	字符型 	20 	 	Y 	 
,ybdj.mdtrt_id mdtrtId 	--医保就诊ID 	字符型 	30 	 	Y 	医保门诊挂号时返回 
,gh.xm psnName 	--人员名称 	字符型 	50 	 	Y 	 
,'01' psnCertType 	--人员证件类型 	字符型 	6 	Y 	Y 	 
,gh.zjh certno 	--证件号码 	字符型 	50 	 	Y 	 
 from Dzcf_D003_output D003
  inner join Drjk_mzjzxxsc_input ybdj on D003.mzh=ybdj.mzh and ybdj.zt=1
  inner join mz_gh gh on D003.mzh=gh.mzh and gh.zt=1 and gh.OrganizeId=d003.OrganizeId
  where D003.organizeid=@orgId
  and D003.mzh=@hisId
  and D003.cfh=@cfh
  and D003.zt=1
end

GO


