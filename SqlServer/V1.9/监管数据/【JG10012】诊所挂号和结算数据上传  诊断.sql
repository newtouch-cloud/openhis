USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_Inp_InventoryUpload_sale]    Script Date: 2025/1/17 10:10:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--【JG10012】诊所挂号和结算数据上传  诊断
--exec [usp_Inp_RegulatoryDataJg10012_diseinfo] '6d5752a7-234a-403e-aa1c-df8b45d3469f','2412257950044'
--select * from mz_js
--select * from mz_gh where '3'='3' order by createtime desc select distinct cardtype,cardtypename from mz_gh
CREATE proc [dbo].[usp_Inp_RegulatoryDataJg10012_diseinfo]
	@orgId varchar(50),
	@hisId varchar(50)
as
begin
select 
	1 diag_type,
	zy.zdlx diag_srt_no,
	zd.icd10 diag_code,
	zy.zdmc diag_name,
	--,isnull(dept.ybksbm, jz.jzks) diag_dept,  
	isnull(staff.gjybdm, jz.jzys) dise_dor_no, 
	jz.jzysmc dise_dor_name
	,convert(varchar(20), zy.createtime, 120)diag_time
	--zy.zt vali_flag 
	--into #temp
from Newtouch_CIS.dbo.xt_jz jz
join Newtouch_CIS.dbo.xt_xyzd zy  on jz.jzId = zy.jzId and jz.OrganizeId = zy.OrganizeId  and  jz.zt = 1
join NewtouchHIS_Base..V_S_Sys_Staff staff on staff.gh = jz.jzys and staff.OrganizeId = jz.OrganizeId
--left join NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.Code = jz.jzks and dept.OrganizeId = jz.OrganizeId
left join NewtouchHIS_Base..V_S_xt_zd zd on zd.zdCode=zy.zdCode and (zd.OrganizeId=zy.OrganizeId or zd.OrganizeId='*') and zd.zt='1'
where  jz.mzh =@hisId and jz.OrganizeId=@orgId and jz.zt='1'
union all
--insert into #temp
select 
	2 diag_type, 
	zy.zdlx diag_srt_no, 
	zd.icd10 diag_code, 
	zy.zdmc diag_name, 
	--isnull(dept.ybksbm,jz.jzks) diag_dept,
	isnull(staff.gjybdm, jz.jzys) dise_dor_no, 
	jz.jzysmc dise_dor_name,
	convert(varchar(20), zy.createtime, 120)diag_time
	--zy.zt vali_flag
from Newtouch_CIS.dbo.xt_jz jz
join Newtouch_CIS.dbo.xt_zyzd zy  on jz.jzId = zy.jzId and jz.OrganizeId = zy.OrganizeId and  jz.zt = 1
join NewtouchHIS_Base..V_S_Sys_Staff staff on staff.gh = jz.jzys and staff.OrganizeId = jz.OrganizeId
--left join NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.Code = jz.jzks and dept.OrganizeId = jz.OrganizeId
left join NewtouchHIS_Base..V_S_xt_zd zd on zd.zdCode=zy.zdCode and zd.OrganizeId=zy.OrganizeId and zd.zt='1'
where  jz.mzh = @hisId and jz.OrganizeId=@orgId and jz.zt='1'

--if(@hisId!=NULL OR @hisId!='')
--   BEGIN
--	select * from #temp where mzh=@hisId 
--   END
--   ELSE
--   BEGIN
--    select * from #temp 
--   END
--   drop table #temp

end




GO






