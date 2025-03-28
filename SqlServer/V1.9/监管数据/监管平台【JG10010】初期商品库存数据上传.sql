USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_Inp_InventoryUpload_invinfo]    Script Date: 2025/1/16 16:00:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO











-- 监管平台【JG10010】初期商品库存数据上传
--exec  [usp_Inp_RegulatoryDataJg10010] '6d5752a7-234a-403e-aa1c-df8b45d3469f',''
--select top 1244 * from [NewtouchHIS_PDS]..xt_yp_pdxxmx order by createtime desc
--select * from [NewtouchHIS_PDS].dbo.xt_yp_pdxx order by createtime desc
CREATE proc [dbo].[usp_Inp_RegulatoryDataJg10010]
	@orgId varchar(50), --组织机构
	@pdId varchar(50)  --盘点明细id
as
begin
select  top 20
    org.gjjgdm+pdmx.pdmxid goods_id,
	yp.ypCode xm_id,
	yp.ypmc goods_name,
	yp.py goods_brevity_code, --商品名称拼音码
	yp.ypgg spec,  --规格
	yp.djdw  prcunt,-- 销售单位 
	yp.jldw dosform,-- 剂型名称
	case when sfdl.dlmc like '西药%' then '09' when sfdl.dlmc like '中药%' then '10' 
	when sfdl.dlmc like '中成%' then '11' else '14' end charge_category_coed,  --收费类别编码
	sfdl.dlmc charge_category_name,   --收费类别名称
	yp.ycmc prodentp_name, --生产厂家名称
	pdmx.pc aprvno,   --批准文号
	case when sfdl.dlmc like '西药%' then '101' when sfdl.dlmc like '中药%' then '103' 
	when sfdl.dlmc like '中成%' then '102' else '999' end drug_class_code,  --药品分类编码
	sfdl.dlmc drug_class_name,  --药品分类名称
	yp.gjybdm med_list_codg,--医疗目录编码  
	yp.ypmc med_list_name,  --医保目录名称
	'' drug_prod_barc, --条形码
	'' selfcode,--自编码
	case when sfdl.dlmc like '西药%' then '101' when sfdl.dlmc like '中药%' then '103' 
	when sfdl.dlmc like '中成%' then '102' else '999' end commodity_class_code,--商品分类编码
	sfdl.dlmc commodity_class_name,  --商品分类名称
	yp.pfj purchase_pric ,--采购价 数值型 18,4 Y
	yp.lsj pric, --零售价 数值型 18,4 Y
	'1' is_rcp,-- 是否处方药 0 否，1 是
	case when yp.gjybdm is null then '0' else '1' end is_medicare_drugs ,--是否医保药品 字符型 2 Y 0 否，1 是
	pdmx.sjsl  inout_cnt ,--入出库数量 数值型 18,4 Y 出库时传负数

	-- 1药品入库  2外部出库 3 直接出库  4申领出库 5 内部发药退回  6科室发药  13 基药出库  14 批量出库
	--01 采购入库 02 销售出库 03 报损出库 05 采购退货出库 06 报溢入库 07 盘点入库 08 盘点出库 09 调拨入库 10 调拨出库 11 清斗（出库）
	--12 装斗（入库） 13 销售退货入库 99 初期库存（入库）
	'99' inout_type_code ,					--入出库类型编码 字符型 10 YY 数据字典 1.8.4
	'初期库存' inout_type_name,   -- 入出库类型名称 字符型 100 Y 
	'01' bills_type_code,  -- 单据类型编码 字符型 10 Y
	'入库单据' bills_type_name,  --   单据类型名称 字符型 100 Y 
	convert(varchar(50),substring(pdmx.pc,0,5)+'-'+substring(pdmx.pc,5,2)+'-'+substring(pdmx.pc,7,2))  manu_date,  -- 生产日期 日期型 Y
    convert(varchar(50),isnull(pdmx.Yxq,'2033-07-01'),23) expy_end,   -- 有效期 日期型 Y yyyy-MM-dd
	pdmx.pc manu_lotnum ,  --生产批号 字符型 100
    '/' spler_name ,  --供应商名称 字符型 500 Y
	convert(varchar(19),pdmx.CreateTime,121) inout_inventory_date --入出库时间 日期型 Y yyyy-MM-dd
    into #ypkc
from [NewtouchHIS_PDS].dbo.xt_yp_pdxx (NOLOCK) pdxx
inner join [NewtouchHIS_PDS].dbo.xt_yp_pdxxmx (NOLOCK) pdmx on  pdxx.pdid=pdmx.pdid
--inner join NewtouchHIS_Base..[xt_ypgys] gys on pdmx.ckbm=gys.gyscode
inner join NewtouchHIS_Base..V_C_xt_yp yp on pdmx.ypdm=yp.ypcode and pdxx.organizeid=yp.organizeid and yp.zt='1'
inner join NewtouchHIS_Base..xt_sfdl sfdl on sfdl.dlCode=yp.dlCode and sfdl.organizeid=yp.organizeid and sfdl.zt='1'
left join NewtouchHIS_Base..Sys_Organize org on org.Id=pdxx.organizeId and org.zt='1'
left join Drjk_RegulatoryDataUpload_Output jxcxc on jxcxc.mlbm_id=pdmx.pdmxid and pdxx.organizeid=jxcxc.organizeid and jxcxc.zt='1' and jxcxc.type='JG10010'
where  pdxx.organizeid=@orgId  and pdxx.Jssj is not null 
   --and yp.gjybdm is not null
  -- and pdmx.Llsl!=pdmx.Sjsl
   and pdxx.zt='1'
   and (jxcxc.mlbm_id is null or jxcxc.issuccess='False')
   and pdxx.CreateTime>=convert(datetime,(convert(varchar(50),GETDATE()-31,23)+' 00:00:00'))
  
   if(@pdId!=NULL OR @pdId!='')
   BEGIN
	 select * from #ypkc WHERE goods_id=@pdId  
   END
   ELSE
   BEGIN
    select * from #ypkc 
   END
   drop table #ypkc
end

GO


