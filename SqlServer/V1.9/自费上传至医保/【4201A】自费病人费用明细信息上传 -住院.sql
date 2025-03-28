USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_zy_SelfCost_data]    Script Date: 2025/2/18 17:18:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/**
【4201A】自费病人费用明细信息上传 -住院
exec  usp_zy_SelfCost_data '6d5752a7-234a-403e-aa1c-df8b45d3469f','4277'
**/
ALTER PROCEDURE [dbo].[usp_zy_SelfCost_data]
@orgId varchar(50),
@jsnm  int
as
select jsnm,xmjfb.zyh fixmedins_mdtrt_id,
	'21' med_type,
	xmjfb.jfbbh bkkp_sn,
	CONVERT(varchar(19),tdrq,121) fee_ocur_time,
	org.gjjgdm fixmedins_code,
	org.Name fixmedins_name,
	sl cnt,
	xmjfb.dj pric,
	convert(decimal(18,2),xmjfb.sl*xmjfb.dj) det_item_fee_sumamt,
	sfxm.gjybdm med_list_codg,
	sfxm.sfxmCode medins_list_codg,
	sfxm.sfxmmc medins_list_name,
	case when fjmc like '%床位费%' then '01'
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
		when fjmc like '%挂号费%' then '13' else '14' end  med_chrgitm_type,
	sfxm.sfxmmc prodname,
	isnull(dept.ybksbm,xmjfb.ks) bilg_dept_codg,
	dept.Name bilg_dept_name,
	isnull(staff.gjybdm,xmjfb.ys) bilg_dr_codg,
	staff.Name bilg_dr_name,
	isnull(dept.ybksbm,xmjfb.ks) acord_dept_codg,
	dept.Name acord_dept_name,
	isnull(staff.gjybdm,xmjfb.ys) acord_dr_code,
	staff.Name acord_dr_name
	into #zytemp
from zy_js  zyjs with(nolock)
join zy_brjbxx zybrxx with(nolock) on zybrxx.zyh=zyjs.zyh and zybrxx.OrganizeId=zyjs.OrganizeId and zybrxx.zt='1'
join zy_xmjfb xmjfb with(nolock) on xmjfb.zyh=zyjs.zyh and xmjfb.OrganizeId=zyjs.OrganizeId and xmjfb.zt='1'
join NewtouchHIS_Base..xt_sfxm sfxm on sfxm.sfxmCode=xmjfb.sfxm and sfxm.OrganizeId=xmjfb.OrganizeId and sfxm.zt='1'
join NewtouchHIS_Base..xt_sfdl sfdl on dlCode=sfxm.sfdlCode and sfdl.OrganizeId=sfxm.OrganizeId and sfdl.zt='1'
join NewtouchHIS_Base..Sys_Department dept on dept.Code=xmjfb.ks and dept.OrganizeId=xmjfb.OrganizeId and dept.zt='1'
join NewtouchHIS_Base..Sys_Staff staff on staff.gh=xmjfb.ys and staff.OrganizeId=xmjfb.OrganizeId and staff.zt='1'
join NewtouchHIS_Base..Sys_Organize org on org.Id=zyjs.OrganizeId and org.zt='1'
where zyjs.jsnm=@jsnm and zyjs.OrganizeId=@orgId

insert into #zytemp
select jsnm,ypjfb.zyh fixmedins_mdtrt_id,
	'21' med_type,
	ypjfb.jfbbh bkkp_sn,
	CONVERT(varchar(19),tdrq,121) fee_ocur_time,
	org.gjjgdm fixmedins_code,
	org.Name fixmedins_name,
	sl cnt,
	ypjfb.dj pric,
	convert(decimal(18,2),ypjfb.sl*ypjfb.dj) det_item_fee_sumamt,
	ypsx.gjybdm med_list_codg,
	ypjfb.yp medins_list_codg,
	yp.ypmc medins_list_name,
	case when fjmc like '%床位费%' then '01'
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
		when fjmc like '%挂号费%' then '13' else '14' end  med_chrgitm_type,
	yp.ypmc prodname,
	isnull(dept.ybksbm,ypjfb.ks) bilg_dept_codg,
	dept.Name bilg_dept_name,
	isnull(staff.gjybdm,ypjfb.ys) bilg_dr_codg,
	staff.Name bilg_dr_name,
	isnull(dept.ybksbm,ypjfb.ks) acord_dept_codg,
	dept.Name acord_dept_name,
	isnull(staff.gjybdm,ypjfb.ys) acord_dr_code,
	staff.Name acord_dr_name
from zy_js zyjs with(nolock)
join zy_brjbxx zybrxx  with(nolock) on zybrxx.zyh=zyjs.zyh and zybrxx.OrganizeId=zyjs.OrganizeId and zybrxx.zt='1'
join zy_ypjfb ypjfb with(nolock) on ypjfb.zyh=zyjs.zyh and ypjfb.OrganizeId=zyjs.OrganizeId and ypjfb.zt='1'
join NewtouchHIS_Base..xt_yp yp on yp.ypCode=ypjfb.yp and yp.OrganizeId=ypjfb.OrganizeId and yp.zt='1'
join NewtouchHIS_Base..xt_ypsx ypsx on ypsx.ypId=yp.ypId and ypsx.OrganizeId=yp.OrganizeId and ypsx.zt='1'
join NewtouchHIS_Base..xt_sfdl sfdl on sfdl.dlCode=yp.dlCode and sfdl.OrganizeId=yp.OrganizeId and sfdl.zt='1'
join NewtouchHIS_Base..Sys_Department dept on dept.Code=ypjfb.ks and dept.OrganizeId=ypjfb.OrganizeId and dept.zt='1'
join NewtouchHIS_Base..Sys_Staff staff on staff.gh=ypjfb.ys and staff.OrganizeId=ypjfb.OrganizeId and staff.zt='1'
join NewtouchHIS_Base..Sys_Organize org on org.Id=zyjs.OrganizeId and org.zt='1'
where zyjs.jsnm=@jsnm and zyjs.OrganizeId=@orgId

select temp.* from #zytemp temp
left join Drjk_jxcsc_output jxcxc on jxcxc.mlbm_id=temp.jsnm and jxcxc.type='4201A'
where (jxcxc.mlbm_id is  null or jxcxc.issuccess='False') 

drop table #zytemp

GO


