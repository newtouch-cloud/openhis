USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_Inp_RegulatoryDataJg10012_feedetail]    Script Date: 2025/1/17 14:11:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--【JG10012】诊所挂号和结算数据上传  feedetail 节点
--exec  [usp_Inp_RegulatoryDataJg10012_feedetail] '6d5752a7-234a-403e-aa1c-df8b45d3469f','','89914'
ALTER proc [dbo].[usp_Inp_RegulatoryDataJg10012_feedetail]
	@orgId varchar(50),
	@hisId varchar(50),
	@setl_id varchar(50)
as
begin
select  
    yp.ypcode,mzjs.organizeid,
	isnull(fymx.feedetl_sn,cfmx.cfmxid)  feedetl_sn ,--费用明细流水号 字符型 30 Y
	jsmx.createtime fee_ocur_time ,--费用发生时间 日期型 
	isnull(jsout.chrg_bchno,mzjs.jsnm) chrg_bchno ,--收费批次号 字符型 30 Y
	'' dise_codg ,--病种编码 字符型 30
	mzcf.cfh rxno ,--处方号 字符型 30
	--'' goods_id ,--商品 ID 字符型 50 Y
	yp.ypmc goods_name ,--商品名称 字符型 500 Y
	yp.gjybdm med_list_codg ,--医保目录编码 字符型 200
	yp.ypmc med_list_name ,--医保目录名称 字符型 500
	cfmx.jl sin_dos_dscr ,--单次剂量描述 字符型 200
	cfmx.pccode used_frqu_dscr ,--使用频次描述 字符型 200
	NULL prd_days ,--周期天数 数值型 4,2
	NULL medc_way_dscr ,--用药途径描述 字符型 200
	staff.gjybdm bilg_dr_codg ,--开单医生编码 字符型 30 Y 医师贯标编码
	staff.Name bilg_dr_name ,--开单医师姓名 字符型 50 Y
	cfmx.sl cnt ,--销售数量 数值型 18,4 Y
	cfmx.dj pric ,--销售单价 数值型 18,6 Y
	cfmx.je total_amount ,--明细费用总金额 数值型 18,4 Y
	case when yp.gjybdm is null then '03' else '01' end chrgitm_lv ,--医保收费项目等级编码字符型 50 Y
	'09' med_chrgitm_type --收费类别编码 字符型 6 Y Y 数据字典 1.8.14
	--'' manu_date ,--生产日期 日期型 Y yyyy-MM-dd
	--'' expy_end ,--有效期 日期型 Y yyyy-MM-dd
	--'' manu_lotnum --生产批号 字符型 100 Y
	into #fymx
from [NewtouchHIS_Sett]..mz_js mzjs with(nolock)
left join mz_jsmx jsmx  with(nolock)  ON jsmx.jsnm=mzjs.jsnm and jsmx.OrganizeId=mzjs.OrganizeId AND jsmx.zt='1'
left join mz_cfmx cfmx  with(nolock)  ON cfmx.cfmxId=jsmx.cf_mxnm and cfmx.OrganizeId=jsmx.OrganizeId AND cfmx.zt='1' and jsmx.cf_mxnm is not null
inner join NewtouchHIS_Base.dbo.V_C_xt_yp yp ON yp.ypCode = cfmx.yp   AND yp.OrganizeId = cfmx.OrganizeId   AND yp.zt = '1'  
left join mz_cf mzcf with(nolock) on mzcf.cfnm=cfmx.cfnm and mzcf.OrganizeId=cfmx.OrganizeId and mzcf.zt='1'
LEFT JOIN NewtouchHIS_Base..Sys_Staff (NOLOCK) staff ON staff.OrganizeId = mzcf.OrganizeId    AND staff.gh = mzcf.ys   AND staff.zt = '1'  
--left join mz_xm mzxm  with(nolock)  ON mzxm.xmnm=jsmx.mxnm and mzxm.OrganizeId=jsmx.OrganizeId AND mzxm.zt='1' and jsmx.mxnm is not null
left JOIN [NewtouchHIS_Sett].dbo.drjk_mzjs_input jsout ON jsout.setl_id=mzjs.ybjslsh --AND jsout.zt='1'
left join [NewtouchHIS_Sett]..[drjk_mzfymxxxsc_input] fymx on fymx.chrg_bchno= jsout.chrg_bchno
--left join Drjk_RegulatoryDataUpload_Output jxcxc on jxcxc.mlbm_id=cast(a.Id as varchar) and jxcxc.organizeid=@orgId and jxcxc.zt='1'
where mzjs.jsnm=@setl_id and  mzjs.OrganizeId=@orgId  --and c.gjybdm is not null
--and (jxcxc.mlbm_id is null or jxcxc.issuccess='False') 
and mzjs.CreateTime>=convert(datetime,(convert(varchar(50),GETDATE()-30,23)+' 00:00:00'))

select fymx.*,
	org.gjjgdm+cfmx.crkmxId goods_id ,--商品 ID 字符型 50 Y
	convert(varchar(50),cfph.CreateTime,23) manu_date ,--生产日期 日期型 Y yyyy-MM-dd
	convert(varchar(50),cfph.yxq,23) expy_end ,--有效期 日期型 Y yyyy-MM-dd
	cfph.ph manu_lotnum --生产批号 字符型 100 Y
from #fymx fymx
left join NewtouchHIS_Base..Sys_Organize org on org.Id=fymx.organizeId and org.zt='1'
left join  NewtouchHIS_PDS..mz_cfmxph cfph on cfph.cfh=fymx.rxno and cfph.organizeid=fymx.organizeid
left join NewtouchHIS_PDS..xt_yp_crkmx  cfmx on cfmx.Ypdm=cfph.yp and cfmx.ph=cfph.ph and  cfmx.pc=cfph.pc
left join NewtouchHIS_PDS..xt_yp_crkdj crkdj on crkdj.crkid= cfmx.crkid and crkdj.organizeid=@orgId and crkdj.zt='1'
where crkdj.djlx!='1'

end
--select * from [NewtouchHIS_PDS]..zy_ypyzxx order by CreateTime desc
--select * from mz_jsmx where 1=1 order by createtime desc
--select * from mz_cf where cfnm='233383'
--select * from mz_cfmx where cfmxid='313229'
--select * from NewtouchHIS_Base.dbo.V_C_xt_yp where xmnm='370189'
--select * from NewtouchHIS_PDS..mz_cfmxph where cfh='R20241227N000034'
--select * from  NewtouchHIS_PDS..mz_cf where cfh='R20241227N000034'
--select * from  NewtouchHIS_PDS..mz_cfmx where cfh='R20241227N000034'
--select * from NewtouchHIS_PDS..xt_yp_crkmx order by createtime desc
--select * from NewtouchHIS_PDS..xt_yp_crkmx where ypdm='00001976' and ph='CSHPDSC123 ' and pc='20241225124047'
--select * from NewtouchHIS_PDS..xt_yp_crkdj where crkid='a103ed5b-6598-4aac-91aa-6d34a72cf3fb' or crkid='b9e93aa4-52f1-47ef-afd5-27d98a7550fb'

GO


