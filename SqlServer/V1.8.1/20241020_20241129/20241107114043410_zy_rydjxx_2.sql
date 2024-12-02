USE [NewtouchHIS_Sett]
GO

-- 事件类型: ALTER_PROCEDURE
-- 变更时间: 11/07/2024 11:40:43



















/**
exec [zy_rydjxx] '03334','6d5752a7-234a-403e-aa1c-df8b45d3469f','02','152524194801044224','21','','1',''
*/

  
ALTER PROCEDURE [dbo].[zy_rydjxx]     
@hisId varchar(20), --住院号  
@orgId varchar(50),--组织机构  
@mdtrt_cert_type varchar(5), --就诊类型  
@mdtrt_cert_no varchar(50), --就诊凭证编号  
@med_type varchar(20),--医疗类别  
@mdtrt_id varchar(50), --就诊ID  
@type varchar(2), --类型 1登记  2信息修改 3出院办理  
@hisIdNum varchar(10) --2位随机数
as  
if @type='1'  
begin  
--declare @Rnum varchar (10)= CAST(CEILING(RAND()* 100) AS varchar);
declare @jzlx varchar(10)='310';
if(@mdtrt_cert_type='02')
begin
set @jzlx='4' --身份证
end
if(@mdtrt_cert_type='01') --电子凭证
begin
set @jzlx='3'
end
if(@mdtrt_cert_type='03') --社保卡
begin
set @jzlx='2'
end
select cd.grbh psn_no,--人员编号  
'390' insutype,--险种类型 
  --  cd.xzlx insutype,--险种类型  
    a.lxr coner_name,--联系人姓名  
    a.lxrdh tel,--联系电话  
    convert(varchar(20),a.ryrq,120) begntime,--开始时间  
    @mdtrt_cert_type  mdtrt_cert_type,--就诊凭证类型  
    @mdtrt_cert_no mdtrt_cert_no,--就诊凭证编号  
    @med_type med_type,--医疗类别  
    @hisId+isnull(@hisIdNum,'')  ipt_no,--住院号  
    a.blh medrcdno,--病历号  
    c.gjybdm atddr_no,--主治医生编码  
    c.name chfpdr_name,--主诊医师姓名  
    d.zdmc adm_diag_dscr,--入院诊断描述  
    dept.ybksbm adm_dept_codg,--入院科室编码  
    dept.name adm_dept_name,--入院科室名称  
    a.cw adm_bed,--入院床位  
    d.icd10 dscg_maindiag_code,--住院主诊断代码  
    d.zdmc dscg_maindiag_name, --住院主诊断名称  
    '' main_cond_dscr,--主要病情描述  
    isnull(bzbm,'') dise_codg, --病种编码  
    isnull(bzmc,'') dise_name,--病种名称  
    ''  oprn_oprt_code,--手术操作代码  
    '' oprn_oprt_name,--手术操作名称  
    a.syfwzh fpsc_no,--计划生育服务证号  
    case rytj when '52' then '1' else '' end matn_type,--生育类别  
    '' birctrl_type,--计划生育手术类别  
    case rytj when '52' then '0' else '' end latechb_flag,--晚育标志  
    0 geso_val, --孕周数  
    0 fetts,--胎次  
    0 fetus_cnt,--胎儿数  
    case rytj when '52' then '0' else '' end pret_flag,--早产标志  
    case  when syrq is  null then convert(varchar(10),GETDATE(),120) else convert(varchar(10),syrq,120) end  birctrl_matn_date,--计划生育手术或生育日期  
    '' dise_type_code--病种类型  
from NewtouchHIS_Sett..zy_brjbxx a  
LEFT JOIN [NewtouchHIS_Sett].[dbo].[xt_brjbxx] b ON b.patid = a.patid  
                AND b.OrganizeId = a.OrganizeId  
                AND b.zt = '1'  
LEFT JOIN [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff] c ON c.OrganizeId = a.OrganizeId  
                  AND c.gh = a.doctor  
                  AND c.zt = '1'  
left join [NewtouchHIS_Sett].[dbo].[zy_rydzd] d on d.zyh=a.zyh and d.OrganizeId=a.OrganizeId and d.zt='1' and d.zdpx='1'  
left join  NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.code=a.ks and dept.OrganizeId=a.OrganizeId and dept.zt='1'  
LEFT JOIN xt_card (NOLOCK) cd ON cd.OrganizeId = a.OrganizeId and cd.CardType=@jzlx and cd.patid=a.patid
                                                              --AND cd.CardNo = a.kh
                                                              AND cd.zt = '1'
where a.zyh=@hisId and a.OrganizeId=@orgId and a.zt='1' and  a.zybz<>'9'  
end  
  
else if @type='3'  
begin  
SELECT  jzdj.jylsh mdtrt_id,--就诊 ID  
  cd.grbh psn_no,--人员编号  
     cd.xzlx insutype,--险种类型  
     convert(varchar(20),ciszyxx.cqrq,120)  endtime,--结束时间 
	 --convert(varchar(20),zybrxx.cyrq,120)  endtime, 
  isnull(bzbm,'') dise_codg, --病种编码  
   isnull(bzmc,'') dise_name,--病种名称   
  '' oprn_oprt_code,--手术操作代码  
  '' oprn_oprt_name,--手术操作名称  
  zybrxx.syfwzh fpsc_no,--计划生育服务证号  
  '' matn_type,--生育类别  
  '' birctrl_type,--计划生育手术类别  
  '' latechb_flag,--晚育标志  
  0 geso_val,--孕周数  
  0 fetts,--胎次  
  0 fetus_cnt,--胎儿数  
  '' pret_flag,--早产标志  
  case  when syrq is  null then convert(varchar(10),GETDATE(),120) else convert(varchar(10),syrq,120) end birctrl_matn_date,--计划生育手术或生育日期  
  '' cop_flag,--伴有并发症标志  
         dept.ybksbm dscg_dept_codg,--ciszyxx.DeptCode cyks ,出院科室编码  
   dept.Name dscg_dept_name,--出院科室名称  
   zybrxx.cw dscg_bed,--出院床位  
  --case cyzd.cyqk when '6' then '2' when '4' then '5' when '5' then '9' else '1' end dscg_way,--离院方式  
  --'' die_date --死亡日期  
  case ciszyxx.cyfs when '3' then '2' when '4' then '5' else '1' end dscg_way,--离院方式  2转院  5死亡 1医嘱离院
  case ciszyxx.cyfs when '4' then convert(varchar(10),cyrq,121) else  '' end die_date --死亡日期 
FROM    zy_brjbxx zybrxx  
        INNER JOIN Newtouch_CIS..zy_brxxk ciszyxx ON ciszyxx.OrganizeId = zybrxx.OrganizeId  
                                                     AND ciszyxx.zyh = zybrxx.zyh  
                                                     AND ciszyxx.zt = '1'  
  left join [Newtouch_CIS].[dbo].[zy_PatDxInfo] cyzd on cyzd.zyh=zybrxx.zyh and cyzd.OrganizeId=zybrxx.OrganizeId and cyzd.zt='1'  
             and cyzd.zdlx='0' and zdlb='2'  
  LEFT JOIN [NewtouchHIS_Sett].[dbo].[xt_brjbxx] b ON b.patid = zybrxx.patid  
                AND b.OrganizeId = zybrxx.OrganizeId  
                AND b.zt = '1'  
  left join [NewtouchHIS_Sett]..[cqyb_OutPut02] jzdj on jzdj.zymzh=zybrxx.zyh and jzdj.OrganizeId=zybrxx.OrganizeId and jzdj.zt='1'  
        LEFT JOIN [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff] staff ON staff.OrganizeId = ciszyxx.OrganizeId  
                                                              AND staff.gh = ciszyxx.ysgh  
                                                              AND staff.zt = '1'  
        left join  NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.code=ciszyxx.DeptCode and dept.OrganizeId=ciszyxx.OrganizeId and dept.zt='1'  
LEFT JOIN xt_card (NOLOCK) cd ON cd.OrganizeId = zybrxx.OrganizeId
                                                              AND cd.CardNo = zybrxx.kh
                                                              AND cd.zt = '1'
WHERE   zybrxx.zt = '1'  
        AND zybrxx.zyh = @hisId  
        AND zybrxx.OrganizeId = @orgId  
  
end  
  
else  
begin  
  
select   
    jzdj.jylsh,--就诊 ID  
    cd.grbh psn_no,--人员编号  
    a.lxr coner_name,--联系人姓名  
    a.lxrdh tel,--联系电话  
    convert(varchar(20),a.ryrq,120) begntime,--开始时间  
    convert(varchar(20),getdate(),120) endtime,--结束时间  
    @mdtrt_cert_type  mdtrt_cert_type,--就诊凭证类型  
    @med_type med_type,--医疗类别  
    (a.zyh+isnull(jzh,'')) ipt_otp_no,--住院号  
    a.blh medrcdno,--病历号  
    c.gjybdm atddr_no,--主治医生编码  
    c.name chfpdr_name,--主诊医师姓名  
    isnull(d.zdmc,ryzd.icd10) adm_diag_dscr,--入院诊断描述  
    dept.ybksbm adm_dept_codg,--入院科室编码  
    dept.name adm_dept_name,--入院科室名称  
    a.cw adm_bed,--入院床位  
    isnull(d.zddm,ryzd.icd10) dscg_maindiag_code,--住院主诊断代码  
    isnull(d.zdmc,ryzd.zdmc) dscg_maindiag_name, --住院主诊断名称  
    '' main_cond_dscr,--主要病情描述  
    isnull(bzbm,'') dise_codg, --病种编码  
    isnull(bzmc,'') dise_name,--病种名称   
    ''  oprn_oprt_code,--手术操作代码  
    '' oprn_oprt_name,--手术操作名称  
    a.syfwzh fpsc_no,--计划生育服务证号  
    case rytj when '52' then '1' else '' end matn_type,--生育类别  
    '' birctrl_type,--计划生育手术类别  
     case rytj when '52' then '0' else '' end latechb_flag,--晚育标志  
    0 geso_val, --孕周数  
    0 fetts,--胎次  
    0 fetus_cnt,--胎儿数  
    case rytj when '52' then '0' else '' end pret_flag,--早产标志  
    case  when syrq is  null then convert(varchar(10),'2021-12-31 23:53:53',120) else convert(varchar(10),syrq,120) end birctrl_matn_date,--计划生育手术或生育日期  
    '' dise_type_code--病种类型  
from NewtouchHIS_Sett..zy_brjbxx a  
LEFT JOIN [NewtouchHIS_Sett].[dbo].[xt_brjbxx] b ON b.patid = a.patid  
                AND b.OrganizeId = a.OrganizeId  
                AND b.zt = '1'  
left join [NewtouchHIS_Sett]..[cqyb_OutPut02] jzdj on jzdj.zymzh=a.zyh and jzdj.OrganizeId=a.OrganizeId and jzdj.zt='1'  
LEFT JOIN [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff] c ON c.OrganizeId = a.OrganizeId  
                  AND c.gh = a.doctor  
                  AND c.zt = '1'  
LEFT JOIN [Newtouch_CIS].[dbo].[zy_PatDxInfo] d ON d.zyh = a.zyh  
                  AND d.OrganizeId = a.OrganizeId  
                  AND d.zt = '1' and d.zdlb='1' and d.zdlx='0'
LEFT JOIN [NewtouchHIS_Sett].[dbo].zy_rydzd ryzd ON ryzd.zyh = a.zyh  
                  AND ryzd.OrganizeId = a.OrganizeId  
                  AND ryzd.zt = '1' and ryzd.zdpx='1' 
left join  NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.code=a.ks and dept.OrganizeId=a.OrganizeId and dept.zt='1'  
LEFT JOIN xt_card (NOLOCK) cd ON cd.OrganizeId = a.OrganizeId
                                                              AND cd.CardNo = a.kh
                                                              AND cd.zt = '1'
where a.zyh=@hisId and a.OrganizeId=@orgId and a.zt='1' and  a.zybz<>'9'  
  
end  
  
  

















