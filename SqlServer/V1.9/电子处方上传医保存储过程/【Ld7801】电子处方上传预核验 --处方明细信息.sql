USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_ElectronicPrescription_D001_Prescription_DetailData]    Script Date: 2024/11/11 10:27:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







/**
 【Ld7801】电子处方上传预核验 --处方明细信息
exec [usp_ElectronicPrescription_D001_Prescription_DetailData] '6d5752a7-234a-403e-aa1c-df8b45d3469f','2411080500003','R20241108N000008'
select * from [Newtouch_CIS]..xt_cfmx cfmx order by createtime desc
*/
CREATE proc [dbo].[usp_ElectronicPrescription_D001_Prescription_DetailData]
	@orgId varchar(500),
	@hisId varchar(500),
	@cfh varchar(500)
as
begin
select  
cfmx.gjybdm medListCodg, 	    --医疗目录编码 	字符型 	50 	 	Y 	即医保药品编码（注：仅作为医保药品代码标识和记录，可按通用名开方和取药） 
cfmx.ypcode fixmedinsHilistId 	--定点医药机构目录编号 	字符型 	30 	 	 	即院内药品编码 
,'' hospPrepFlag 	    --医疗机构制剂标志 	字符型 	3 	Y 	 	0-否、1-是，默认否 
,isnull(case cfyp.listtype when '102' then '13' when '101' then '11' else null end ,(case yp.dlCode when '01' then '11' when '02' then '12' when '03' then '13' end)) rxItemTypeCode 	    --处方项目分类代码 	字符型 	30 	Y 	Y 	11:西药,12:中成药,13:中药饮片，参考处方项目分类代码（rx_item_type_code） 
,'' rxItemTypeName 	    --处方项目分类名称 	字符型 	100 	 	 	 
,isnull(case cfyp.listtype when '102' then '3' else null end ,(case yp.dlCode when '03' then '3' else '' end)) tcmdrugTypeCode 	--中药类别代码 	字符型 	30 	Y 	 	参考中药类别代码（tcmdrug_type_code），中药处方必填，中药饮片固定传3 
,'' tcmdrugTypeName 	--中药类别名称 	字符型 	20 	 	 	 
,'' tcmherbFoote 	--草药脚注 	字符型 	200 	 	 	 
,'' mednTypeCode 	--药物类型代码 	字符型 	100 	Y 	 	参考药物类型代码（medn_type_co de），（注：可按院内内部的药品类型分类）					
,'' mednTypeName 	--药物类型 	字符型 	100 	 	 	 
,'' mainMedcFlag 	--主要用药标志 	字符型 	3 	Y 	 	0-否、1-是 
,'' urgtFlag 	--加急标志 	字符型 	3 	Y 	 	0-否、1-是 
,'' basMednFlag 	--基本药物标志 	字符型 	3 	Y 	 	0-否、1-是 
,'' impDrugFlag 	--是否进口药品 	字符型 	3 	Y 	 	0-否、1-是 
,'' otcFlag 	--是否OTC药品 	字符型 	3 	Y 	 	0-处方药品（默认）、1-OTC药品 
,isnull(yp.ypmc,cfyp.regname) drugGenname 	--药品通用名 	字符型 	100 	 	Y 	 
,isnull(case cfyp.listtype when '102' then '中药剂型' when '101' then '西药剂型' else null end ,yp.jx) drugDosform 	--药品剂型 	字符型 	30 	 	Y 	 
--,cfyp.specName drugSpec
,isnull(ypsx.ypgg,cfyp.specName+'*'+minPacCnt+minPrepunt+'/'+minPacunt) drugSpec 	--药品规格 	字符型 	40 	 	Y 	 
,'' drugProdname 	--药品商品名 	字符型 	255 	 	 	非必填，可按通用名开方 
,cfyp.prdrName prdrName 	--生厂厂家 	字符型 	100 	 	 	非必填，可按通用名开方 
,case isnull(cf.cfyf,'p') when 'p' then cfmx.yfCode else '9' end  medcWayCodg 	--用药途径代码 	字符型 	10 	Y 	Y 	rx_item_type_code为西药、中成药时必填，中药饮片使用字段 rxUsedWayCodg ，参考药物使用-途径代码(drug_medc_way_code)（注：可使用院内内部代码） 
,ypyf.yfmc medcWayDscr 	--用药途径描述 	字符型 	100 	 	Y 	rx_item_type_code为西药、中成药时必填，中药饮片使用字段rxUsedWayName  
,convert (varchar(50),cf.CreateTime,120)  medcBegntime 	--用药开始时间 	日期时间型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
--,convert (varchar(50),dateadd(day,isnull(ts,7),cf.CreateTime),120) medcEndtime 
,convert (varchar(50),dateadd(day,case when isnull(ts,7)>0 then isnull(ts,7) else '1' end,cf.CreateTime),120) medcEndtime 	--用药结束时间 	日期时间型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
--,isnull(cfmx.ts,'7.0') medcDays
,case when  isnull(cfmx.ts,'7.0')>0 then isnull(cfmx.ts,'7.0') else '1.0' end medcDays 	--用药天数 	数值型 	8,2 	 	Y 	 
,cfmx.mcjldw sinDosunt 	--单次剂量单位 （即开方单位或剂量单位，如“mg“） 	字符型 	20 	 	Y 	rx_item_type_c ode为西药、中成药时必填，中药饮片使用字段rxDosunt 
,cfmx.mcjl sinDoscnt 	--单次用量 	数值型 	16,4 	 	Y 	rx_item_type_code为西药、中成药时必填，中药饮片使用字段rxDoscnt 
,case isnull(cf.cfyf,'p')when 'p' then cfmx.pccode else '11' end usedFrquCodg 	--使用频次编码 	字符型 	10 	Y 	Y 	rx_item_type_c ode为西药、中成药时必填，中药饮片使用字段 rxFrquCodg,参考使用频次（used_frqu）处方项目分类代码（注：可使用院内内部代码） 
,case isnull(cf.cfyf,'p')when 'p' then pc.yzpcmc else '无' end  usedFrquName 	--使用频次名称 	字符型 	30 	 	Y 	rx_item_type_code为西药、中成药时必填, 中药饮片使用字段rxFrquName  
,cfmx.dw drugDosunt 	--药品总用药量单位（即发药计价单位，取药或处方流转时药品医保结算使用的单位；如“片“或”盒“） 	字符型 	20 	 	Y 	（注：拆零时使用最小制剂单位如“片”，非拆零时使用库存包装单位，如“盒 “，须统一使用国家医保药品目录的“最小制剂单位”或“最小包装单位”） 
,cfmx.sl drugCnt 	--药品总用药量 （取药或处方流转时药品医保结算使用的数量） 	数值型 	16,4 	 	Y 	根据单次剂量及单位、频次等和 drugDosunt按院内“药品单位转换系数“换算 
,cfmx.dj drugPric 	--药品单价 	数值型 	16,6 	 	 	非必填，院内价格按drugDosunt 计价 
,cfmx.je drugSumamt 	--药品总金额 	数值型 	16,2 	 	 	非必填，院内总金额=drugCnt× 药品单价 
,case isnull(cfmx.zzfbz,'0') when '1' then '2' else '1' end hospApprFlag 	--医院审批标志 	字符型 	3 	Y 	Y 	
,'' selfPayRea 	--自费原因类型 	字符型 	6 	Y 	 	医院审批标志 hospApprFlag值为2（自费）时必填，值参考字典self_pay_rea 
,'' realDscr 	--自费原因描述 	字符型 	1000 	N 	 	self_pay_rea自费原因类型为6（其他原因）时必填 
,'' extras 	--扩展数据 	对象型 	 	 	 	地方业务扩展信息，处方结算核验时可传递予地方核心业务系统 
from [Newtouch_CIS]..xt_cf cf
left join [Newtouch_CIS]..xt_cfmx cfmx on cf.cfId=cfmx.cfId and cf.OrganizeId=cfmx.OrganizeId and cfmx.zt='1' 
left join [NewtouchHIS_Base]..xt_yp yp on yp.ypcode=cfmx.ypcode and yp.OrganizeId=cfmx.OrganizeId and yp.zt='1'
left join [NewtouchHIS_Base]..xt_ypsx ypsx on yp.ypcode=ypsx.ypcode and yp.OrganizeId=ypsx.OrganizeId and ypsx.zt='1'
left join [NewtouchHIS_Sett]..Dzcf_CFYP_output cfyp on cfmx.gjybdm=cfyp.medListCodg and cf.isdzcf='1'
--left join [NewtouchHIS_Sett]..Dzcf_CFYP_output cfyp on ypsx.gjybdm=cfyp.cfypcode 
left join [NewtouchHIS_Base].[dbo].[xt_ypyf] ypyf on (cf.cfyf=ypyf.yfCode or cfmx.yfCode=ypyf.yfCode) and ypyf.zt='1'
left join [NewtouchHIS_Base].[dbo].[xt_yzpc] pc on pc.yzpccode=cfmx.pcCode and pc.OrganizeId=cfmx.OrganizeId and pc.zt='1' 

where cf.cfh =@cfh --and ypsx.gjybdm is not null--and cf.isdzcf='1'
and cf.OrganizeId=@orgId
and cf.cflx in('1','2')
 order by cf.CreateTime desc
end

--select * from [NewtouchHIS_Base].[dbo].[xt_ypyf] where yfCode='1'
--select * from [Newtouch_CIS]..xt_cf where cfh='R20240118N000033'
--select * from [Newtouch_CIS]..xt_cfmx where cfid='ab9aadbb-9558-4121-8fdb-65380a4bf9e3'

GO


