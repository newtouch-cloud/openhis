USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_ElectronicPrescription_D001_PrescriptionData]    Script Date: 2024/11/11 10:28:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






/**
 【Ld7801】电子处方上传预核验 --处方信息
exec  [usp_ElectronicPrescription_D001_PrescriptionData] '6d5752a7-234a-403e-aa1c-df8b45d3469f','2410309600005','R20241030N000010'
*/

CREATE proc [dbo].[usp_ElectronicPrescription_D001_PrescriptionData]
	@orgId varchar(50),
	@hisId varchar(50),
	@cfh varchar(500)
as
begin
select 
case gh.CardType when '2' then '03' when '3' then '01' when '6' then '01' else  '02' end  mdtrtCertType ,--01-电子凭证令牌、02-身份证号、03-社会保障卡号
case gh.CardType when '2' then CardNo else gh.zjh end  mdtrtCertNo,--就诊凭证类型为“01”时填写电子凭证令牌，为“02” 时填写身份证号，为“03” 时填写社会保障卡卡号（注：就诊凭证类型“03” 时必填） 
case gh.cardtype when '2' then CardNo else '' end cardSn,--就诊凭证类型为“03”时必填
'01' bizTypeCode,--01-定点医疗机构就诊，02-互联网医院问诊
'' rxExraAttrCode,--01-双通道处方，02-门诊统筹处方，03-其
case when gh.CardType='3' or gh.cardtype='6' then ectoken  else '' end ecToken,--使用医保电子凭证就诊时必填
case when gh.CardType='3' or gh.cardtype='6' then ectoken  else '' end  authNo,--线上场景互联网医院问诊时使用，就诊凭证类型：01且必填
kh.cbdbm insuPlcNo, --默认取电子凭证返回的参保地或就诊登记时参保地信息；患者有多个参保地信息、省外参保人时必传 
'' mdtrtareaNo,--默认取就诊登记时就医地信息；医疗机构有多个定点协议统筹区、省外参保人时必传 
cf.cfh hospRxno,--院内内部处方号，单笔处方不可重复 
'' initRxno ,--续方的原处方编号
case cf.cflx when '1' then '1' when '2' then '9' end rxTypeCode, --处方类别代码
convert (varchar(50),cf.CreateTime,120) prscTime, --开方时间 
isnull(cf.tieshu,'1') rxDrugCnt,--西药、中成药时为药品的类目数量，中药饮片时为草药的总剂数
case isnull(cf.cfyf,'p') when 'p' then '' else '9' end rxUsedWayCodg,--处方整剂用法编号
case isnull(ypyf.yfmc,'p') when 'p' then '' else ypyf.yfmc end rxUsedWayName,--处方整剂用法名称
case isnull(cf.cfyf,'p')when 'p' then '' else '11' end rxFrquCodg,--处方整剂频次编号 
case isnull(cf.cfyf,'p')when 'p' then '' else '无' end rxFrquName,--处方整剂频次名称
case isnull(cf.cfyf,'p')when 'p' then '' else 'g' end rxDosunt,--处方整剂剂量单位 
case isnull(cf.cfyf,'p')when 'p' then '' else '1' end rxDoscnt,--处方整剂单次剂量数  
'' rxDrordDscr,--处方整剂医嘱说明 
'2' valiDays,-- 处方有效天数
convert (varchar(50),dateadd(day,2,cf.CreateTime),120)  valiEndTime, --有效截止时间
'' reptFlag --复用（多次）使用标志，
,'' maxReptCnt 
,'' minInrvDays 
,'' rxCotnFlag 
,'' longRxFlag 
from [NewtouchHIS_Sett]..mz_gh gh 
inner join [NewtouchHIS_Sett]..xt_card kh on kh.CardNo=gh.kh and kh.CardType=gh.CardType and kh.OrganizeId=gh.OrganizeId and kh.zt='1'
--inner join [NewtouchHIS_Sett]..drjk_mzjs_input brxx on brxx.mzh=gh.mzh and brxx.zt='1'
inner join [Newtouch_CIS]..xt_jz jz on jz.mzh=gh.mzh and jz.OrganizeId=gh.OrganizeId
left join [NewtouchHIS_Sett]..xt_brjbxx xtbrxx on xtbrxx.patid=gh.patid and xtbrxx.OrganizeId=gh.OrganizeId and xtbrxx.zt='1'
left join [Newtouch_CIS]..xt_cf cf on cf.jzId=jz.jzid and cf.OrganizeId=jz.OrganizeId and cf.zt='1'
left join [NewtouchHIS_Base].[dbo].[xt_ypyf] ypyf on cf.cfyf=ypyf.yfCode and ypyf.zt='1'
where gh.zt='1' and gh.brxz!='0'
and gh.mzh=@hisId
and cf.cfh = @cfh
and gh.OrganizeId=@orgId

and cf.cflx in('1','2')
 order by gh.CreateTime desc
end


--select * from [NewtouchHIS_Sett]..mz_gh where mzh='2401290690007'
--select * from [Newtouch_CIS]..xt_jz where mzh='2401290690007'
--select * from [NewtouchHIS_Sett]..drjk_mzjs_input where mzh='2409220600007'
GO


