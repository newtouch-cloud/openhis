USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_mz_SelfCost_data]    Script Date: 2025/2/18 17:04:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/**
【4201】自费病人费用明细信息上传 -门诊
exec  usp_mz_SelfCost_data '6d5752a7-234a-403e-aa1c-df8b45d3469f','89603'
**/
ALTER PROCEDURE [dbo].[usp_mz_SelfCost_data]
@orgId varchar(50),
@jsnm  int
as
select js.jsnm,mzh+cast(mzxm.xmnm as varchar) mdtrt_sn
,mzh ipt_otp_no
,'11' med_type
,js.jsnm chrg_bchno
,mzxm.xmnm feedetl_sn
,gh.zjlx psn_cert_type
,gh.zjh certno
,gh.xm psn_name
,CONVERT(varchar(19),js.CreateTime,12) fee_ocur_time
,mzxm.sl cnt
,mzxm.dj pric
,je det_item_fee_sumamt
,sfxm.gjybdm med_list_codg
,mzxm.sfxm medins_list_codg
,sfxm.sfxmmc medins_list_name
,case when fjmc like '%床位费%' then '01'
	when fjmc like '%诊察费%' then '02'
	when fjmc like '%检查费%' then '03'
	when fjmc like '%化验费%' then '04'
	when fjmc like '%治疗费%' then '05'
	when fjmc like '%手术费%' then '06'
	when fjmc like '%护理费%' then '07'
	when fjmc like '%材料费%' then '08'
	when fjmc like '%西药费%' then '09'
	when fjmc like '%中药饮片费%' then '10'
	when fjmc like '%中成药费%' then '11'
	when fjmc like '%一般诊疗费%' then '12'
	when fjmc like '%挂号费%' then '13' else '14' end med_chrgitm_type  --医保 医疗收费项目类别  xt_sfdl 需做对照
,sfxm.sfxmmc prodname
,ybksbm bilg_dept_codg
,ksmc bilg_dept_name
,isnull(staff.gjybdm,gh.ys) bilg_dr_codg
,staff.Name bilg_dr_name
,'' acord_dept_codg
,'' acord_dept_name
,isnull(staff.gjybdm,gh.ys) orders_dr_code
,staff.Name orders_dr_name
,NULL tcmdrug_used_way
,NULL etip_flag
,'' etip_hosp_code
,NULL dscg_tkdrug_flag
,'' memo
into #mztemp
from mz_js js with(nolock)
join mz_gh gh with(nolock) on gh.ghnm=js.ghnm and gh.OrganizeId=js.OrganizeId and gh.zt='1'
join NewtouchHIS_Base..Sys_Staff staff on staff.gh=gh.ys and staff.OrganizeId=gh.OrganizeId --and staff.zt='1'
join mz_jsmx jsmx with(nolock) on jsmx.jsnm=js.jsnm and jsmx.OrganizeId=js.OrganizeId and jsmx.zt='1'
join mz_xm mzxm with(nolock) on mzxm.xmnm=jsmx.mxnm and mzxm.OrganizeId=jsmx.OrganizeId and mzxm.zt='1'
join NewtouchHIS_Base..xt_sfxm sfxm on sfxm.sfxmCode=mzxm.sfxm and sfxm.OrganizeId=mzxm.OrganizeId and sfxm.zt='1'
join NewtouchHIS_Base..xt_sfdl sfdl on dlCode=sfxm.sfdlCode and sfdl.OrganizeId=sfxm.OrganizeId and sfdl.zt='1'
join NewtouchHIS_Base..Sys_Department dept on dept.Code=mzxm.ks and dept.OrganizeId=mzxm.OrganizeId and dept.zt='1'
where js.jsnm=@jsnm and js.OrganizeId=@orgId and mxnm is not null and sfxm.gjybdm is not null
 
 insert into #mztemp
select js.jsnm,mzh+cast(cfmx.cfmxId as varchar) mdtrt_sn
,mzh ipt_otp_no
,'11' med_type
,js.jsnm chrg_bchno
,cfmx.cfmxId feedetl_sn
,gh.zjlx psn_cert_type
,gh.zjh certno
,gh.xm psn_name
,CONVERT(varchar(19),js.CreateTime,12) fee_ocur_time
,cfmx.sl cnt
,cfmx.dj pric
,je det_item_fee_sumamt
,ypsx.gjybdm med_list_codg
,cfmx.yp medins_list_codg
,yp.ypmc medins_list_name
,case when fjmc like '%床位费%' then '01'
	when fjmc like '%诊察费%' then '02'
	when fjmc like '%检查费%' then '03'
	when fjmc like '%化验费%' then '04'
	when fjmc like '%治疗费%' then '05'
	when fjmc like '%手术费%' then '06'
	when fjmc like '%护理费%' then '07'
	when fjmc like '%材料费%' then '08'
	when fjmc like '%西药费%' then '09'
	when fjmc like '%中药饮片费%' then '10'
	when fjmc like '%中成药费%' then '11'
	when fjmc like '%一般诊疗费%' then '12'
	when fjmc like '%挂号费%' then '13' else '14' end med_chrgitm_type  --医保 医疗收费项目类别  xt_sfdl 需做对照
,yp.ypmc prodname
,ybksbm bilg_dept_codg
,ksmc bilg_dept_name
,isnull(staff.gjybdm,gh.ys) bilg_dr_codg
,ysmc bilg_dr_name
,'' acord_dept_codg
,'' acord_dept_name
,isnull(staff.gjybdm,gh.ys) orders_dr_code
,ysmc orders_dr_name
,NULL tcmdrug_used_way
,NULL etip_flag
,'' etip_hosp_code
,NULL dscg_tkdrug_flag
,'' memo
from mz_js js with(nolock)
join mz_gh gh with(nolock) on gh.ghnm=js.ghnm and gh.OrganizeId=js.OrganizeId and gh.zt='1'
join NewtouchHIS_Base..Sys_Staff staff on staff.gh=gh.ys and staff.OrganizeId=gh.OrganizeId --and staff.zt='1'
join mz_jsmx jsmx with(nolock) on jsmx.jsnm=js.jsnm and jsmx.OrganizeId=js.OrganizeId and jsmx.zt='1'
join mz_cfmx cfmx with(nolock) on cfmx.cfmxId=jsmx.cf_mxnm and cfmx.OrganizeId=jsmx.OrganizeId and cfmx.zt='1'
join mz_cf cf with(nolock) on cf.cfnm=cfmx.cfnm and cf.OrganizeId=cfmx.OrganizeId and cf.zt='1'
join NewtouchHIS_Base..xt_yp yp on yp.ypCode=cfmx.yp and yp.OrganizeId =cfmx.OrganizeId and yp.zt='1'
join NewtouchHIS_Base..xt_ypsx ypsx on ypsx.ypId=yp.ypId and ypsx.OrganizeId=yp.OrganizeId and ypsx.zt='1'
join NewtouchHIS_Base..xt_sfdl sfdl on sfdl.dlCode=yp.dlCode and sfdl.OrganizeId=yp.OrganizeId and sfdl.zt='1'
join NewtouchHIS_Base..Sys_Department dept on dept.Code=cf.ks and dept.OrganizeId=cf.OrganizeId and dept.zt='1'
where js.jsnm=@jsnm and js.OrganizeId=@orgId and cf_mxnm is not null and ypsx.gjybdm is not null

select * from #mztemp temp
left join Drjk_jxcsc_output jxcxc on jxcxc.mlbm_id=cast(temp.jsnm as varchar)and jxcxc.type='4201'
where (jxcxc.mlbm_id is null or jxcxc.issuccess='False') 

drop table #mztemp

--select ghnm,* from mz_js  where jsnm='88439' order by CreateTime desc
--select * from mz_jsmx order by CreateTime desc
--select * from mz_jsmx where jsnm='88439'
--select * from mz_xm where xmnm='313068'
--select * from mz_cf where cfnm='233174'
--select * from mz_cfmx where cfmxId='313068'
--select ys,* from mz_gh where ghnm='1057318' order by CreateTime desc
--select * from NewtouchHIS_Base..xt_yp
--select * from NewtouchHIS_Base..xt_sfxm
--select * from NewtouchHIS_Base..  where dlmc='疫苗储运费'
--select distinct zydzfplbmc from NewtouchHIS_Base..xt_sfdl
GO


