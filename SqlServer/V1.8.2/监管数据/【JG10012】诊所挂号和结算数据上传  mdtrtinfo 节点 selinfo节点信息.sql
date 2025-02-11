USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_Inp_InventoryUpload_sale]    Script Date: 2025/1/17 10:10:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--��JG10012�������Һźͽ��������ϴ�  mdtrtinfo �ڵ� selinfo�ڵ���Ϣ
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
	gh.mzh  ipt_otp_no, --����Һű��� �ַ��� 50 Y ERP ϵͳ����Ǽ�Ψһ��ʶ
	gh.CreateTime begntime ,--�Һ�ʱ�� �ַ��� 50 Y yyyy-MM-dd HH:mm:ss
	gh.xm psn_name ,--�������� �ַ��� 50 ҽ���ش�
	'1' psn_cert_type ,--֤�����ͱ��� �ַ��� 50 ҽ���ش��������ֵ� 1.8.12
	gh.zjh certno ,--֤������ �ַ��� 50 ҽ���ش�
	case when gh.xb='1' then '��' else 'Ů' end gend ,--�Ա� �ַ��� 10 ҽ���ش��������֣��У�Ů
	REPLACE(nlshow,'��','') Age ,--���� �ַ��� 50 ҽ���ش�
	gh.csny brdy ,--�������� �ַ��� 10 ҽ���ش�����ʽ��yyyy-MM-dd
	'' tel ,--��ϵ�绰 �ַ��� 50
	case gh.cardtype when '2' then '03' when '3' then '01' when '4' then '02' when '6' then '04' else '' end mdtrt_cert_type ,--����ƾ֤���ͱ��� �ַ��� 3 ҽ���ش��������ֵ� 1.8.9
	case gh.cardtype when '2' then kh when '3' then ecToken when '4' then gh.zjh when '6' then gh.zjh else '' end mdtrt_cert_no ,--����ƾ֤��� �ַ��� 50ҽ���ش�������ƾ֤����Ϊ��01��ʱ��д����ƾ֤���ƣ�Ϊ��02��ʱ��д���֤�ţ�Ϊ��03��ʱ��д��ᱣ�Ͽ�����
	kh.xzlx insutype ,--�������ͱ��� �ַ��� 50 ҽ���ش��������ֵ� 1.8.8
	'/' emp_name ,--�α���λ���� �ַ��� 50 ҽ������ֵ��ҽ���ش�
	gh.jzid mdtrt_id ,--ҽ������ ID �ַ��� 50 ҽ������ֵ��ҽ���ش�
	grbh psn_no ,--ҽ����Ա���� �ַ��� 50
	case when gh.brxz='0' then '1206' else '11' end med_type ,--ҽ�������� �ַ��� 6 Y Y �����ֵ� 1.8.10
	zdicd10 dise_codg ,--���ֱ��� �ַ��� 30 Y ���� ICD ����
	zdmc dise_name ,--�������� �ַ��� 500 Y
	'' main_cond_dscr ,--��Ҫ�������� �ַ��� 1000
	staff.gjybdm atddr_no ,--����ҽʦ���� �ַ��� 30 Y ҽʦ������
	staff.Name dr_name ,--����ҽʦ���� �ַ��� 50 Y
	dept.ybksbm dept_code ,--������ұ��� �ַ��� 100
	dept.Name dept_name ,--����������� �ַ��� 100
	case gh.brxz when '0' then '1' else '0' end is_medicare_code, --�Ƿ�ҽ�����߱�ʶ �ַ��� 1 Y 0 ��1 ��

	mzjs.jsnm medins_setl_id ,--ҽҩ�������� ID �ַ��� 30 Y ERP ϵͳ����Ψһ��ʶ
	mzjs.fph invono ,--��Ʊ�� �ַ��� 20
	case when jsout.setl_id is null then '101' else '104' end payment_type_code ,--֧����ʽ���� �ַ��� 10 Y Y�����ֵ� 1.8.6
	mzjs.createtime org_settle_time ,--ҽҩ��������ʱ�� ������ Y yyyy-MM-dd HH:mm:ss
    setl_id ,--ҽ������ ID �ַ��� 30 ҽ������ֵ��ҽ���ش�
    setl_time ,--ҽ������ʱ�� ������ ҽ������ֵ��ҽ���ش��� ��ʽ yyyy-MM-dd HH:mm:ss
	isnull(medfee_sumamt,zje) medfee_sumamt,-- �����ܽ�� ��ֵ�� 18,4 Y
	fulamt_ownpay_amt ,--ȫ�Էѽ����ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	overlmt_selfpay ,--���޼��Էѷ��� ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	preselfpay_amt ,--�����Ը���� ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	inscp_scp_amt ,--�������߷�Χ��� ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	act_pay_dedc ,--ʵ��֧������ ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	hifp_pay ,--����ҽ�Ʊ���ͳ�����֧����ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	pool_prop_selfpay ,--����ҽ�Ʊ���ͳ�����֧������ ��ֵ�� 5,4 ҽ������ֵ��ҽ���ش�
	cvlserv_pay ,--����Աҽ�Ʋ����ʽ�֧�� ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	hifes_pay ,--��ҵ����ҽ�Ʊ��ջ���֧�� ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	hifmi_pay ,--����󲡱����ʽ�֧�� ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	hifob_pay ,--ְ�����ҽ�Ʒ��ò�������֧�� ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	maf_pay ,--ҽ�ƾ�������֧�� ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	oth_pay ,--����֧�� ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	fund_pay_sumamt ,--����֧���ܶ� ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	psn_part_amt ,--���˸����ܽ�� ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	acct_pay ,--�����˻�֧�� ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	psn_cash_pay ,--�����ֽ�֧�� ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	hosp_part_amt ,--ҽԺ������� ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
	acct_mulaid_pay ,--�����˻�����֧����� ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
    balc --�˻���� ��ֵ�� 18,2 ҽ������ֵ��ҽ���ش�
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


