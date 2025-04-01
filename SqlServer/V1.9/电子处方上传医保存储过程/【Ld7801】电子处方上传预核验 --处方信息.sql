USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_ElectronicPrescription_D001_PrescriptionData]    Script Date: 2024/11/11 10:28:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






/**
 ��Ld7801�����Ӵ����ϴ�Ԥ���� --������Ϣ
exec  [usp_ElectronicPrescription_D001_PrescriptionData] '6d5752a7-234a-403e-aa1c-df8b45d3469f','2410309600005','R20241030N000010'
*/

CREATE proc [dbo].[usp_ElectronicPrescription_D001_PrescriptionData]
	@orgId varchar(50),
	@hisId varchar(50),
	@cfh varchar(500)
as
begin
select 
case gh.CardType when '2' then '03' when '3' then '01' when '6' then '01' else  '02' end  mdtrtCertType ,--01-����ƾ֤���ơ�02-���֤�š�03-��ᱣ�Ͽ���
case gh.CardType when '2' then CardNo else gh.zjh end  mdtrtCertNo,--����ƾ֤����Ϊ��01��ʱ��д����ƾ֤���ƣ�Ϊ��02�� ʱ��д���֤�ţ�Ϊ��03�� ʱ��д��ᱣ�Ͽ����ţ�ע������ƾ֤���͡�03�� ʱ��� 
case gh.cardtype when '2' then CardNo else '' end cardSn,--����ƾ֤����Ϊ��03��ʱ����
'01' bizTypeCode,--01-����ҽ�ƻ������02-������ҽԺ����
'' rxExraAttrCode,--01-˫ͨ��������02-����ͳ�ﴦ����03-��
case when gh.CardType='3' or gh.cardtype='6' then ectoken  else '' end ecToken,--ʹ��ҽ������ƾ֤����ʱ����
case when gh.CardType='3' or gh.cardtype='6' then ectoken  else '' end  authNo,--���ϳ���������ҽԺ����ʱʹ�ã�����ƾ֤���ͣ�01�ұ���
kh.cbdbm insuPlcNo, --Ĭ��ȡ����ƾ֤���صĲα��ػ����Ǽ�ʱ�α�����Ϣ�������ж���α�����Ϣ��ʡ��α���ʱ�ش� 
'' mdtrtareaNo,--Ĭ��ȡ����Ǽ�ʱ��ҽ����Ϣ��ҽ�ƻ����ж������Э��ͳ������ʡ��α���ʱ�ش� 
cf.cfh hospRxno,--Ժ���ڲ������ţ����ʴ��������ظ� 
'' initRxno ,--������ԭ�������
case cf.cflx when '1' then '1' when '2' then '9' end rxTypeCode, --����������
convert (varchar(50),cf.CreateTime,120) prscTime, --����ʱ�� 
isnull(cf.tieshu,'1') rxDrugCnt,--��ҩ���г�ҩʱΪҩƷ����Ŀ��������ҩ��ƬʱΪ��ҩ���ܼ���
case isnull(cf.cfyf,'p') when 'p' then '' else '9' end rxUsedWayCodg,--���������÷����
case isnull(ypyf.yfmc,'p') when 'p' then '' else ypyf.yfmc end rxUsedWayName,--���������÷�����
case isnull(cf.cfyf,'p')when 'p' then '' else '11' end rxFrquCodg,--��������Ƶ�α�� 
case isnull(cf.cfyf,'p')when 'p' then '' else '��' end rxFrquName,--��������Ƶ������
case isnull(cf.cfyf,'p')when 'p' then '' else 'g' end rxDosunt,--��������������λ 
case isnull(cf.cfyf,'p')when 'p' then '' else '1' end rxDoscnt,--�����������μ�����  
'' rxDrordDscr,--��������ҽ��˵�� 
'2' valiDays,-- ������Ч����
convert (varchar(50),dateadd(day,2,cf.CreateTime),120)  valiEndTime, --��Ч��ֹʱ��
'' reptFlag --���ã���Σ�ʹ�ñ�־��
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


