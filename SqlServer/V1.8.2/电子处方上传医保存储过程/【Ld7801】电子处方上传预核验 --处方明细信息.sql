USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_ElectronicPrescription_D001_Prescription_DetailData]    Script Date: 2024/11/11 10:27:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







/**
 ��Ld7801�����Ӵ����ϴ�Ԥ���� --������ϸ��Ϣ
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
cfmx.gjybdm medListCodg, 	    --ҽ��Ŀ¼���� 	�ַ��� 	50 	 	Y 	��ҽ��ҩƷ���루ע������Ϊҽ��ҩƷ�����ʶ�ͼ�¼���ɰ�ͨ����������ȡҩ�� 
cfmx.ypcode fixmedinsHilistId 	--����ҽҩ����Ŀ¼��� 	�ַ��� 	30 	 	 	��Ժ��ҩƷ���� 
,'' hospPrepFlag 	    --ҽ�ƻ����Ƽ���־ 	�ַ��� 	3 	Y 	 	0-��1-�ǣ�Ĭ�Ϸ� 
,isnull(case cfyp.listtype when '102' then '13' when '101' then '11' else null end ,(case yp.dlCode when '01' then '11' when '02' then '12' when '03' then '13' end)) rxItemTypeCode 	    --������Ŀ������� 	�ַ��� 	30 	Y 	Y 	11:��ҩ,12:�г�ҩ,13:��ҩ��Ƭ���ο�������Ŀ������루rx_item_type_code�� 
,'' rxItemTypeName 	    --������Ŀ�������� 	�ַ��� 	100 	 	 	 
,isnull(case cfyp.listtype when '102' then '3' else null end ,(case yp.dlCode when '03' then '3' else '' end)) tcmdrugTypeCode 	--��ҩ������ 	�ַ��� 	30 	Y 	 	�ο���ҩ�����루tcmdrug_type_code������ҩ���������ҩ��Ƭ�̶���3 
,'' tcmdrugTypeName 	--��ҩ������� 	�ַ��� 	20 	 	 	 
,'' tcmherbFoote 	--��ҩ��ע 	�ַ��� 	200 	 	 	 
,'' mednTypeCode 	--ҩ�����ʹ��� 	�ַ��� 	100 	Y 	 	�ο�ҩ�����ʹ��루medn_type_co de������ע���ɰ�Ժ���ڲ���ҩƷ���ͷ��ࣩ					
,'' mednTypeName 	--ҩ������ 	�ַ��� 	100 	 	 	 
,'' mainMedcFlag 	--��Ҫ��ҩ��־ 	�ַ��� 	3 	Y 	 	0-��1-�� 
,'' urgtFlag 	--�Ӽ���־ 	�ַ��� 	3 	Y 	 	0-��1-�� 
,'' basMednFlag 	--����ҩ���־ 	�ַ��� 	3 	Y 	 	0-��1-�� 
,'' impDrugFlag 	--�Ƿ����ҩƷ 	�ַ��� 	3 	Y 	 	0-��1-�� 
,'' otcFlag 	--�Ƿ�OTCҩƷ 	�ַ��� 	3 	Y 	 	0-����ҩƷ��Ĭ�ϣ���1-OTCҩƷ 
,isnull(yp.ypmc,cfyp.regname) drugGenname 	--ҩƷͨ���� 	�ַ��� 	100 	 	Y 	 
,isnull(case cfyp.listtype when '102' then '��ҩ����' when '101' then '��ҩ����' else null end ,yp.jx) drugDosform 	--ҩƷ���� 	�ַ��� 	30 	 	Y 	 
--,cfyp.specName drugSpec
,isnull(ypsx.ypgg,cfyp.specName+'*'+minPacCnt+minPrepunt+'/'+minPacunt) drugSpec 	--ҩƷ��� 	�ַ��� 	40 	 	Y 	 
,'' drugProdname 	--ҩƷ��Ʒ�� 	�ַ��� 	255 	 	 	�Ǳ���ɰ�ͨ�������� 
,cfyp.prdrName prdrName 	--�������� 	�ַ��� 	100 	 	 	�Ǳ���ɰ�ͨ�������� 
,case isnull(cf.cfyf,'p') when 'p' then cfmx.yfCode else '9' end  medcWayCodg 	--��ҩ;������ 	�ַ��� 	10 	Y 	Y 	rx_item_type_codeΪ��ҩ���г�ҩʱ�����ҩ��Ƭʹ���ֶ� rxUsedWayCodg ���ο�ҩ��ʹ��-;������(drug_medc_way_code)��ע����ʹ��Ժ���ڲ����룩 
,ypyf.yfmc medcWayDscr 	--��ҩ;������ 	�ַ��� 	100 	 	Y 	rx_item_type_codeΪ��ҩ���г�ҩʱ�����ҩ��Ƭʹ���ֶ�rxUsedWayName  
,convert (varchar(50),cf.CreateTime,120)  medcBegntime 	--��ҩ��ʼʱ�� 	����ʱ���� 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
--,convert (varchar(50),dateadd(day,isnull(ts,7),cf.CreateTime),120) medcEndtime 
,convert (varchar(50),dateadd(day,case when isnull(ts,7)>0 then isnull(ts,7) else '1' end,cf.CreateTime),120) medcEndtime 	--��ҩ����ʱ�� 	����ʱ���� 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
--,isnull(cfmx.ts,'7.0') medcDays
,case when  isnull(cfmx.ts,'7.0')>0 then isnull(cfmx.ts,'7.0') else '1.0' end medcDays 	--��ҩ���� 	��ֵ�� 	8,2 	 	Y 	 
,cfmx.mcjldw sinDosunt 	--���μ�����λ ����������λ�������λ���硰mg���� 	�ַ��� 	20 	 	Y 	rx_item_type_c odeΪ��ҩ���г�ҩʱ�����ҩ��Ƭʹ���ֶ�rxDosunt 
,cfmx.mcjl sinDoscnt 	--�������� 	��ֵ�� 	16,4 	 	Y 	rx_item_type_codeΪ��ҩ���г�ҩʱ�����ҩ��Ƭʹ���ֶ�rxDoscnt 
,case isnull(cf.cfyf,'p')when 'p' then cfmx.pccode else '11' end usedFrquCodg 	--ʹ��Ƶ�α��� 	�ַ��� 	10 	Y 	Y 	rx_item_type_c odeΪ��ҩ���г�ҩʱ�����ҩ��Ƭʹ���ֶ� rxFrquCodg,�ο�ʹ��Ƶ�Σ�used_frqu��������Ŀ������루ע����ʹ��Ժ���ڲ����룩 
,case isnull(cf.cfyf,'p')when 'p' then pc.yzpcmc else '��' end  usedFrquName 	--ʹ��Ƶ������ 	�ַ��� 	30 	 	Y 	rx_item_type_codeΪ��ҩ���г�ҩʱ����, ��ҩ��Ƭʹ���ֶ�rxFrquName  
,cfmx.dw drugDosunt 	--ҩƷ����ҩ����λ������ҩ�Ƽ۵�λ��ȡҩ�򴦷���תʱҩƷҽ������ʹ�õĵ�λ���硰Ƭ���򡱺С��� 	�ַ��� 	20 	 	Y 	��ע������ʱʹ����С�Ƽ���λ�硰Ƭ�����ǲ���ʱʹ�ÿ���װ��λ���硰�� ������ͳһʹ�ù���ҽ��ҩƷĿ¼�ġ���С�Ƽ���λ������С��װ��λ���� 
,cfmx.sl drugCnt 	--ҩƷ����ҩ�� ��ȡҩ�򴦷���תʱҩƷҽ������ʹ�õ������� 	��ֵ�� 	16,4 	 	Y 	���ݵ��μ�������λ��Ƶ�εȺ� drugDosunt��Ժ�ڡ�ҩƷ��λת��ϵ�������� 
,cfmx.dj drugPric 	--ҩƷ���� 	��ֵ�� 	16,6 	 	 	�Ǳ��Ժ�ڼ۸�drugDosunt �Ƽ� 
,cfmx.je drugSumamt 	--ҩƷ�ܽ�� 	��ֵ�� 	16,2 	 	 	�Ǳ��Ժ���ܽ��=drugCnt�� ҩƷ���� 
,case isnull(cfmx.zzfbz,'0') when '1' then '2' else '1' end hospApprFlag 	--ҽԺ������־ 	�ַ��� 	3 	Y 	Y 	
,'' selfPayRea 	--�Է�ԭ������ 	�ַ��� 	6 	Y 	 	ҽԺ������־ hospApprFlagֵΪ2���Էѣ�ʱ���ֵ�ο��ֵ�self_pay_rea 
,'' realDscr 	--�Է�ԭ������ 	�ַ��� 	1000 	N 	 	self_pay_rea�Է�ԭ������Ϊ6������ԭ��ʱ���� 
,'' extras 	--��չ���� 	������ 	 	 	 	�ط�ҵ����չ��Ϣ�������������ʱ�ɴ�����ط�����ҵ��ϵͳ 
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


