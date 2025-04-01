USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_Inp_InventoryUpdate_invinfo]    Script Date: 2025/1/16 11:48:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ���ƽ̨ ��JG10004���������ϸ�����ϴ�
--exec  [usp_Inp_RegulatoryDataJg0004] '6d5752a7-234a-403e-aa1c-df8b45d3469f',''
CREATE proc [dbo].[usp_Inp_RegulatoryDataJg0004]
	@orgId varchar(50), --��֯����
	@crkId varchar(50)   --�������ϸid
as
begin
select top 20
	org.gjjgdm+crkmx.crkmxid goods_id,
	yp.ypCode xm_id,
	yp.ypmc goods_name,
	yp.py goods_brevity_code, --��Ʒ����ƴ����
	yp.ypgg spec,  --���
	yp.djdw  prcunt,-- ���۵�λ 
	yp.jldw dosform,-- ��������
	case when sfdl.dlmc like '��ҩ%' then '09' when sfdl.dlmc like '��ҩ%' then '10' 
	when sfdl.dlmc like '�г�%' then '11' else '14' end charge_category_coed,  --�շ�������
	sfdl.dlmc charge_category_name,   --�շ��������
	yp.ycmc prodentp_name, --������������
	crkmx.pc aprvno,   --��׼�ĺ�
	case when sfdl.dlmc like '��ҩ%' then '101' when sfdl.dlmc like '��ҩ%' then '103' 
	when sfdl.dlmc like '�г�%' then '102' else '999' end drug_class_code,  --ҩƷ�������
	sfdl.dlmc drug_class_name,  --ҩƷ��������
	yp.gjybdm med_list_codg,--ҽ��Ŀ¼����  
	yp.ypmc med_list_name,  --ҽ��Ŀ¼����
	'' drug_prod_barc, --������
	'' selfcode,--�Ա���
	case when sfdl.dlmc like '��ҩ%' then '101' when sfdl.dlmc like '��ҩ%' then '103' 
	when sfdl.dlmc like '�г�%' then '102' else '999' end commodity_class_code,--��Ʒ�������
	sfdl.dlmc commodity_class_name,  --��Ʒ��������
	yp.pfj purchase_pric ,--�ɹ��� ��ֵ�� 18,4 Y
	yp.lsj pric, --���ۼ� ��ֵ�� 18,4 Y
	'1' is_rcp,-- �Ƿ񴦷�ҩ 0 ��1 ��
	case when yp.gjybdm is null then '0' else '1' end is_medicare_drugs ,--�Ƿ�ҽ��ҩƷ �ַ��� 2 Y 0 ��1 ��
	case when crkdj.djlx in('1','5') then crkmx.sl else -crkmx.sl end inout_cnt ,--��������� ��ֵ�� 18,4 Y ����ʱ������
	-- 1ҩƷ���  2�ⲿ���� 3 ֱ�ӳ���  4������� 5 �ڲ���ҩ�˻�  6���ҷ�ҩ  13 ��ҩ����  14 ��������
	--01 �ɹ���� 02 ���۳��� 03 ������� 05 �ɹ��˻����� 06 ������� 07 �̵���� 08 �̵���� 09 ������� 10 �������� 11 �嶷�����⣩
	--12 װ������⣩ 13 �����˻���� 99 ���ڿ�棨��⣩
	case crkdj.djlx when '1' then '01' when '2' then '05' --when '3' then '10' when '4' then '10'  when '6' then '10' 
		when '5'  then '13' else '10' end inout_type_code ,					--��������ͱ��� �ַ��� 10 YY �����ֵ� 1.8.4
	case crkdj.djlx when '1' then '�ɹ����' when '2' then '�ɹ��˻�����'--when '3' then '��������' when '4' then '��������'when '6' then '��������' 
		when '5'  then '�����˻����' else '��������' end inout_type_name,   -- ������������� �ַ��� 100 Y 
	case when crkdj.djlx in('1','5') then '01' else '02' end bills_type_code,  -- �������ͱ��� �ַ��� 10 Y
	case when crkdj.djlx in('1','5') then '��ⵥ��' else '���ⵥ��' end bills_type_name,  --   ������������ �ַ��� 100 Y 
	convert(varchar(50),substring(crkmx.pc,0,5)+'-'+substring(crkmx.pc,5,2)+'-'+substring(crkmx.pc,7,2))  manu_date,  -- �������� ������ Y
    convert(varchar(50),isnull(crkmx.Yxq,'2033-07-01'),23) expy_end,   -- ��Ч�� ������ Y yyyy-MM-dd
	crkmx.pc manu_lotnum ,  --�������� �ַ��� 100
    gys.gysmc spler_name ,  --��Ӧ������ �ַ��� 500 Y
	convert(varchar(19),crkmx.CreateTime,121) inout_inventory_date --�����ʱ�� ������ Y yyyy-MM-dd
	--convert(int,yp.bzs) min_pac_cnt,-- ��С��װ���� Int Y
	--bzdw min_pacunt ,--��С��װ��λ �ַ��� 50
	--'' standard_code ,--ҩƷ��λ�� �ַ��� 100
	--jldw min_prepunt ,--��С�Ƽ���λ �ַ��� 50 Y
	into #ypkcbg
from [NewtouchHIS_PDS].[dbo].[xt_yp_crkdj] (nolock) crkdj 
inner join NewtouchHIS_Base..[xt_ypgys] gys on crkdj.ckbm=gys.gyscode
inner join [NewtouchHIS_PDS].[dbo].xt_yp_crkmx (nolock) crkmx on crkdj.crkId=crkmx.crkId 
inner join NewtouchHIS_Base..V_C_xt_yp yp on crkmx.ypdm=yp.ypcode
inner join NewtouchHIS_Base..xt_sfdl sfdl on sfdl.dlCode=yp.dlCode and sfdl.organizeid=yp.organizeid and sfdl.zt='1'
left join Drjk_RegulatoryDataUpload_Output jxcxc on jxcxc.mlbm_id=crkmx.crkmxid and crkdj.organizeid=jxcxc.organizeid and jxcxc.zt='1' and jxcxc.type='JG10004'
left join NewtouchHIS_Base..Sys_Organize org on org.Id=crkdj.organizeId and org.zt='1'
where crkdj.organizeid=@orgId
	and crkdj.zt='1' --and yp.gjybdm is not null
	and (jxcxc.mlbm_id is null or jxcxc.issuccess='False')
	and crkdj.CreateTime>=convert(datetime,(convert(varchar(50),GETDATE()-31,23)+' 00:00:00'))

   if(@crkId!=NULL OR @crkId!='')
   BEGIN
	 select * from #ypkcbg WHERE goods_id=@crkId  
   END
   ELSE
   BEGIN
    select * from #ypkcbg 

   END
   
   drop table #ypkcbg
end

GO


