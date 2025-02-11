USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_Inp_InventoryUpload_sale]    Script Date: 2025/1/17 10:10:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--【JG10012】诊所挂号和结算数据上传  mdtrtinfo 节点 selinfo节点信息
--exec  [usp_Inp_RegulatoryDataJg10012_mdtrtinfo] '6d5752a7-234a-403e-aa1c-df8b45d3469f','',''
--select * from mz_js
--select * from mz_gh where '3'='3' order by createtime desc select distinct cardtype,cardtypename from mz_gh
alter proc [dbo].[usp_Inp_RegulatoryDataJg10012_mdtrtinfo]
	@orgId varchar(50),
	@hisId varchar(50),
	@setl_id varchar(50)
as
begin
select  
	gh.mzh  ipt_otp_no, --门诊挂号编码 字符型 50 Y ERP 系统门诊登记唯一标识
	gh.CreateTime begntime ,--挂号时间 字符型 50 Y yyyy-MM-dd HH:mm:ss
	gh.xm psn_name ,--患者姓名 字符型 50 医保必传
	'1' psn_cert_type ,--证件类型编码 字符型 50 医保必传，数据字典 1.8.12
	gh.zjh certno ,--证件号码 字符型 50 医保必传
	case when gh.xb='1' then '男' else '女' end gend ,--性别 字符型 10 医保必传，传汉字：男，女
	REPLACE(nlshow,'岁','') Age ,--年龄 字符型 50 医保必传
	gh.csny brdy ,--出生日期 字符型 10 医保必传，格式：yyyy-MM-dd
	'' tel ,--联系电话 字符型 50
	case gh.cardtype when '2' then '03' when '3' then '01' when '4' then '02' when '6' then '04' else '' end mdtrt_cert_type ,--就诊凭证类型编码 字符型 3 医保必传，数据字典 1.8.9
	case gh.cardtype when '2' then kh when '3' then ecToken when '4' then gh.zjh when '6' then gh.zjh else '' end mdtrt_cert_no ,--就诊凭证编号 字符型 50医保必传，就诊凭证类型为“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
	kh.xzlx insutype ,--险种类型编码 字符型 50 医保必传，数据字典 1.8.8
	'/' emp_name ,--参保单位名称 字符型 50 医保返回值，医保必传
	gh.jzid mdtrt_id ,--医保就诊 ID 字符型 50 医保返回值，医保必传
	grbh psn_no ,--医保人员编码 字符型 50
	case when gh.brxz='0' then '1206' else '11' end med_type ,--医疗类别编码 字符型 6 Y Y 数据字典 1.8.10
	zdicd10 dise_codg ,--病种编码 字符型 30 Y 疾病 ICD 编码
	zdmc dise_name ,--病种名称 字符型 500 Y
	'' main_cond_dscr ,--主要病情描述 字符型 1000
	staff.gjybdm atddr_no ,--就诊医师编码 字符型 30 Y 医师贯标编码
	staff.Name dr_name ,--就诊医师姓名 字符型 50 Y
	dept.ybksbm dept_code ,--就诊科室编码 字符型 100
	dept.Name dept_name ,--就诊科室名称 字符型 100
	case gh.brxz when '0' then '1' else '0' end is_medicare_code, --是否医保患者标识 字符型 1 Y 0 否，1 是

	mzjs.jsnm medins_setl_id ,--医药机构结算 ID 字符型 30 Y ERP 系统结算唯一标识
	mzjs.fph invono ,--发票号 字符型 20
	case when jsout.setl_id is null then '101' else '104' end payment_type_code ,--支付方式编码 字符型 10 Y Y数据字典 1.8.6
	mzjs.createtime org_settle_time ,--医药机构结算时间 日期型 Y yyyy-MM-dd HH:mm:ss
    setl_id ,--医保结算 ID 字符型 30 医保返回值，医保必传
    setl_time ,--医保结算时间 日期型 医保返回值，医保必传。 格式 yyyy-MM-dd HH:mm:ss
	isnull(medfee_sumamt,zje) medfee_sumamt,-- 销售总金额 数值型 18,4 Y
	fulamt_ownpay_amt ,--全自费金额数值型 18,2 医保返回值，医保必传
	overlmt_selfpay ,--超限价自费费用 数值型 18,2 医保返回值，医保必传
	preselfpay_amt ,--先行自付金额 数值型 18,2 医保返回值，医保必传
	inscp_scp_amt ,--符合政策范围金额 数值型 18,2 医保返回值，医保必传
	act_pay_dedc ,--实际支付起付线 数值型 18,2 医保返回值，医保必传
	hifp_pay ,--基本医疗保险统筹基金支出数值型 18,2 医保返回值，医保必传
	pool_prop_selfpay ,--基本医疗保险统筹基金支付比例 数值型 5,4 医保返回值，医保必传
	cvlserv_pay ,--公务员医疗补助资金支出 数值型 18,2 医保返回值，医保必传
	hifes_pay ,--企业补充医疗保险基金支出 数值型 18,2 医保返回值，医保必传
	hifmi_pay ,--居民大病保险资金支出 数值型 18,2 医保返回值，医保必传
	hifob_pay ,--职工大额医疗费用补助基金支出 数值型 18,2 医保返回值，医保必传
	maf_pay ,--医疗救助基金支出 数值型 18,2 医保返回值，医保必传
	oth_pay ,--其他支出 数值型 18,2 医保返回值，医保必传
	fund_pay_sumamt ,--基金支付总额 数值型 18,2 医保返回值，医保必传
	psn_part_amt ,--个人负担总金额 数值型 18,2 医保返回值，医保必传
	acct_pay ,--个人账户支出 数值型 18,2 医保返回值，医保必传
	psn_cash_pay ,--个人现金支出 数值型 18,2 医保返回值，医保必传
	hosp_part_amt ,--医院负担金额 数值型 18,2 医保返回值，医保必传
	acct_mulaid_pay ,--个人账户共济支付金额 数值型 18,2 医保返回值，医保必传
    balc --账户余额 数值型 18,2 医保返回值，医保必传
	into #temp
from mz_js mzjs with(nolock) 
join mz_gh gh  with(nolock) on gh.ghnm=mzjs.ghnm and gh.organizeid=mzjs.organizeid and gh.zt='1'
join xt_card kh with(nolock) on kh.CardNo=gh.kh and kh.CardType=gh.CardType and kh.OrganizeId=gh.OrganizeId and kh.zt='1'
join NewtouchHIS_Base.dbo.Sys_Department dept on dept.Code=gh.ks and dept.OrganizeId=gh.OrganizeId and dept.zt='1'
join NewtouchHIS_Base.dbo.Sys_Staff staff ON staff.gh=gh.ys AND staff.OrganizeId=gh.OrganizeId
left JOIN [NewtouchHIS_Sett].dbo.drjk_mzjs_output jsout ON jsout.setl_id=mzjs.ybjslsh --AND jsout.zt='1'
left join Drjk_RegulatoryDataUpload_Output jxcxc on jxcxc.mlbm_id=cast(mzjs.jsnm as varchar) and jxcxc.organizeid=@orgId and jxcxc.zt='1' 
	and jxcxc.type='JG10012'
where  mzjs.OrganizeId=@orgId and mzjs.zje>0 and mzjs.jszt='1'
and mzjs.CreateTime>=convert(datetime,(convert(varchar(50),GETDATE()-30,23)+' 00:00:00'))
and (jxcxc.mlbm_id is null or jxcxc.issuccess='False') 

if(@hisId!=NULL OR @hisId!='')
   BEGIN
	select * from #temp where ipt_otp_no=@hisId  and (medins_setl_id=@setl_id or @setl_id='')
   END
   ELSE
   BEGIN
    select * from #temp 
   END

end
--select * from [NewtouchHIS_PDS]..zy_ypyzxx order by CreateTime desc




GO


