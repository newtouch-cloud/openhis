USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_Inp_InventoryUpdate_purchase]    Script Date: 2025/1/13 10:55:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- 监管平台 【JG10001】新增商品信息
--exec  usp_Inp_RegulatoryDataJg0001 '6d5752a7-234a-403e-aa1c-df8b45d3469f',''
--select top 2 * from NewtouchHIS_Base..V_C_xt_yp
--select top 2 * from NewtouchHIS_Base..xt_sfdl 
CREATE proc [dbo].[usp_Inp_RegulatoryDataJg0001]
	@orgId varchar(50),
	@crkId varchar(50)
as
begin
select top 20  --批量上传不可超20
	org.gjjgdm+crkmx.crkmxid goods_id,
	yp.ypCode xm_id,
	yp.ypmc goods_name,
	yp.py goods_brevity_code,
	yp.ypgg spec,
	yp.djdw  prcunt,-- 销售单位 
	yp.jldw dosform,-- 剂型名称
	case when sfdl.dlmc like '西药%' then '09' when sfdl.dlmc like '中药%' then '10' 
	when sfdl.dlmc like '中成%' then '11' else '14' end charge_category_coed,  --收费类别编码
	sfdl.dlmc charge_category_name,
	yp.ycmc prodentp_name,
	crkmx.pc aprvno,
	case when sfdl.dlmc like '西药%' then '101' when sfdl.dlmc like '中药%' then '103' 
	when sfdl.dlmc like '中成%' then '102' else '999' end drug_class_code,  --药品分类编码
	sfdl.dlmc drug_class_name,
	yp.gjybdm med_list_codg,--医疗目录编码  
	yp.ypmc med_list_name,
	'' drug_prod_barc, --条形码
	'' selfcode,--自编码
	case when sfdl.dlmc like '西药%' then '101' when sfdl.dlmc like '中药%' then '103' 
	when sfdl.dlmc like '中成%' then '102' else '999' end commodity_class_code,--商品分类编码
	sfdl.dlmc commodity_class_name,
	'1' is_rcp,-- 是否处方药 0 否，1 是
	case when yp.gjybdm is null then '0' else '1' end is_medicare_drugs ,--是否医保药品 字符型 2 Y 0 否，1 是
	convert(int,yp.bzs) min_pac_cnt,-- 最小包装数量 Int Y
	bzdw min_pacunt ,--最小包装单位 字符型 50
	'' standard_code ,--药品本位码 字符型 100
	jldw min_prepunt ,--最小制剂单位 字符型 50 Y
	yp.pfj purchase_price ,--采购价 数值型 18,4 Y
	yp.lsj pric --零售价 数值型 18,4 Y
	into #ypkc
from [NewtouchHIS_PDS].[dbo].[xt_yp_crkdj] (nolock) crkdj 
inner join [NewtouchHIS_PDS].[dbo].xt_yp_crkmx (nolock) crkmx on crkdj.crkId=crkmx.crkId
inner join NewtouchHIS_Base..V_C_xt_yp yp on crkmx.ypdm=yp.ypcode
inner join NewtouchHIS_Base..[xt_ypgys] gys on crkdj.ckbm=gys.gyscode
inner join NewtouchHIS_Base..xt_sfdl sfdl on sfdl.dlCode=yp.dlCode and sfdl.organizeid=yp.organizeid and sfdl.zt='1'
left join Drjk_RegulatoryDataUpload_Output jxcxc on jxcxc.mlbm_id=crkmx.crkmxid and crkdj.organizeid=jxcxc.organizeid and jxcxc.zt='1' and jxcxc.type='JG10001' 
left join NewtouchHIS_Base..Sys_Organize org on org.Id=crkdj.organizeId and org.zt='1'
where  crkdj.organizeid=@orgId
   and crkdj.djlx='1'
   and crkdj.zt='1'
   and (jxcxc.mlbm_id is null or jxcxc.issuccess='False')
   and crkdj.CreateTime>=convert(datetime,(convert(varchar(50),GETDATE()-31,23)+' 00:00:00'))

   if(@crkId!=null or @crkId!='')
   begin
	select * from #ypkc where goods_id=@crkId
   end
   else
   begin
   select * from #ypkc
   end
   drop table #ypkc
end

GO


