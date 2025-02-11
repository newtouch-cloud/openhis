

/**
ҽ�����ף�2203  2402 2403  �����Ϣ��ѯ
exec [mzzy_diagxx] '03296','6d5752a7-234a-403e-aa1c-df8b45d3469f','3','1212','1212'

*/

alter PROCEDURE [dbo].[mzzy_diagxx]  

@hisId varchar(20), --סԺ��  
@orgId varchar(50),--��֯����  
@model varchar(5), --����   1��ѯ���� 2��ѯסԺ��Ժ��� 3 ��ѯסԺ��Ժ���
@mdtrt_id varchar(50), --����ƾ֤���  
@psn_no varchar(50) --��Ա���
as
if @model=1
begin
	select 1 diag_type,zy.zdlx diag_srt_no,zd.icd10 diag_code,zy.zdmc diag_name
		,isnull(dept.ybksbm, jz.jzks) diag_dept,  isnull(staff.gjybdm, jz.jzys) dise_dor_no, jz.jzysmc dise_dor_name
		,convert(varchar(20), zy.createtime, 120)diag_time, zy.zt vali_flag from Newtouch_CIS.dbo.xt_jz jz
	 join Newtouch_CIS.dbo.xt_xyzd zy  on jz.jzId = zy.jzId and jz.OrganizeId = zy.OrganizeId and  jz.zt = 1
	 join NewtouchHIS_Base..V_S_Sys_Staff staff on staff.gh = jz.jzys and staff.OrganizeId = jz.OrganizeId
	 left join NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.Code = jz.jzks and dept.OrganizeId = jz.OrganizeId
	 left join NewtouchHIS_Base..V_S_xt_zd zd on zd.zdCode=zy.zdCode and zd.OrganizeId=zy.OrganizeId and zd.zt='1'
	 where  jz.mzh =@hisId and jz.OrganizeId=@orgId and jz.zt='1'
	 union all 
	 select 2 diag_type, zy.zdlx diag_srt_no, zd.icd10 diag_code, zy.zdmc diag_name, isnull(dept.ybksbm,jz.jzks) diag_dept,
	     isnull(staff.gjybdm, jz.jzys) dise_dor_no, jz.jzysmc dise_dor_name, convert(varchar(20), zy.createtime, 120)diag_time, zy.zt vali_flag
	 from Newtouch_CIS.dbo.xt_jz jz
	 join Newtouch_CIS.dbo.xt_zyzd zy  on jz.jzId = zy.jzId and jz.OrganizeId = zy.OrganizeId and  jz.zt = 1
	 join NewtouchHIS_Base..V_S_Sys_Staff staff on staff.gh = jz.jzys and staff.OrganizeId = jz.OrganizeId
	 left join NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.Code = jz.jzks and dept.OrganizeId = jz.OrganizeId
	 left join NewtouchHIS_Base..V_S_xt_zd zd on zd.zdCode=zy.zdCode and zd.OrganizeId=zy.OrganizeId and zd.zt='1'
	 where  jz.mzh = @hisId and jz.OrganizeId=@orgId and jz.zt='1'
end
else if @model=2
begin
	select 1 diag_type, zy.zdpx diag_srt_no, zy.icd10 diag_code, zy.zdmc diag_name, isnull(dept.ybksbm,jz.ks) diag_dept
		, isnull(ys.gjybdm,jz.doctor) dise_dor_no,ys.Name dise_dor_name, convert(varchar(20), zy.createtime, 120)diag_time
		, zy.zt vali_flag ,case zy.zdpx when 1 then 1 else 0 end maindiag_flag, @mdtrt_id mdtrt_id ,@psn_no psn_no 
    from NewtouchHis_sett.[dbo].[zy_brjbxx]  jz
    join NewtouchHis_sett.dbo.zy_rydzd zy on jz.zyh = zy.zyh and jz.OrganizeId = zy.OrganizeId and  jz.zt = 1 and zy.zt=1
    join NewtouchHIS_Base.dbo.Sys_Staff ys on jz.doctor = ys.gh 
    left join NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.Code=jz.ks and dept.OrganizeId=jz.OrganizeId
	where  jz.zyh = @hisId and jz.OrganizeId=@orgId and jz.zt='1'
end
else if @model=3
begin
	select --zy.zddl,zy.zdlx,zy.zdlb,zy.px,
		case when isnull(zddl,'WM')='WM' THEN  1 WHEN isnull(zddl,'TCM')='TCM' AND isnull(zy.zdlx,'2')='1' THEN 2
		 WHEN isnull(zddl,'TCM')='TCM' AND isnull(zy.zdlx,'2')='2' THEN 3 ELSE 1 END diag_type
		,case zy.zdlx when 1 then 1 else 0 end maindiag_flag, ROW_NUMBER() over(order by zddl DESC,zy.zdlx) diag_srt_no
		--,isnull(zy.px,1) diag_srt_no
		, isnull(zd.icd10,zh.zhCode) diag_code, zy.zdmc diag_name
	    ,isnull(dept.ybksbm,jz.ks)  diag_dept, isnull(ys.gjybdm,jz.doctor)  dise_dor_no, ys.Name dise_dor_name
		, convert(varchar(20), zy.createtime, 120)diag_time, @mdtrt_id mdtrt_id ,@psn_no psn_no 
    from NewtouchHis_sett.[dbo].[zy_brjbxx]  jz
    join [Newtouch_CIS].[dbo].[zy_PatDxInfo]  zy on jz.zyh = zy.zyh and jz.OrganizeId = zy.OrganizeId and  jz.zt = 1  
		and zy.zdlb = 2 and zy.zt = 1 and zy.zdmc <> '999999999'
    join NewtouchHIS_Base.dbo.Sys_Staff ys on jz.doctor = ys.gh
    left join NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.Code = jz.ks and dept.OrganizeId = jz.OrganizeId
    left join NewtouchHIS_Base.[dbo].[xt_zd] zd on (zd.zdCode=zy.zddm or zd.icd10=zy.zddm) and zd.zt='1' --and zd.OrganizeId=zy.OrganizeId
	left join NewtouchHIS_Base..xt_zyzh zh on zh.zhCode=zy.zddm and zh.zt='1'
    where  jz.zyh =@hisId and jz.OrganizeId=@orgId and jz.zt='1'
	--where jz.zyh='03296' and jz.zt='1'
	order by zddl DESC,zy.zdlx
end
