USE [NewtouchHIS_Sett]
GO

-- 事件类型: ALTER_PROCEDURE
-- 变更时间: 11/22/2024 17:41:42




  
  
/*-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  

修改人：邓烽
修改时间：2023年9月21日11:12:35
修改内容：
1、4101A 交易修改入院科室编码 adm_dept_code 为adm_dept_codg，
出院科室编码 dscg_dept_code 为dscg_dept_codg
2、4101A 交易入参增加新生儿天龄 age_days
exec usp_Inp_ybupload_cyjs_setlinfoV2 @OrgId='6d5752a7-234a-403e-aa1c-df8b45d3469f',@zyh='03333' 

修改人：邓烽
修改时间：2024年2月7日11:44:45
修改内容：只获取最后一次的结算信息
-- =============================================*/  
ALTER proc [dbo].[usp_Inp_ybupload_cyjs_setlinfoV2]  
(
 @orgId varchar(50),    
 @zyh varchar(20)    
)
as    


 
declare @age_days varchar(10)/*新人儿年龄天*/
select @age_days=nlshow from NewtouchHIS_Sett.dbo.zy_brjbxx with(nolock) where OrganizeId=@orgId and zyh=@zyh and zt=1 and zybz<>'9'

if(@age_days not like'%岁%' and @age_days not like'%月%')
begin
	select @age_days=replace(@age_days,'日','')
	select @age_days=replace(@age_days,'天','')
	select @age_days=ltrim(rtrim(@age_days))
end
else
begin
	select @age_days=null
end



 create table #yb_gx  
(  
ba_code varchar(10),  
ba_name varchar(50),  
yb_code varchar(10),  
yb_name varchar(50),  
)  
  
insert into #yb_gx(ba_code, ba_name, yb_code, yb_name)  
select 0,'本人或户主',1,'本人'  
union all  
select 1,'配偶',10,'配偶'  
union all  
select 2,'子',20,'子'  
union all  
select 3,'女',30,'女'  
union all  
select 4,'孙子、孙女或外孙子、外孙女',40,'孙子、孙女或外孙子'  
union all  
select 5,'父母',50,'父母'  
union all  
select 6,'祖父母或外祖父母',60,'祖父母或外祖父母'  
union all  
select 7,'兄、弟、姐、妹',70,'兄、弟、姐、妹'  
union all  
select 8,'其他',80,'其他'  
  
  
 select    
 a.psn_no ,    
 a.mdtrt_id,  
 a.setl_id,    
 --'' hi_no,  
 b.BAH medcasno,  
 --'重庆重医附二院宽仁康复医院' fixmedins_name,'H50010302473' fixmedins_code,'3' hi_setl_lv,   
 convert(varchar(50),getdate(),120) dcla_time,    
 isnull(c.GJ,'CHN') ntly, -- 国籍 字符型 6 Y Y   
 isnull(c.zy,'90') prfs, -- 职业 字符型 6 Y Y  
 (case when b.xzz_sn<>'' then isnull(b.XZZ_SN,'')+isnull(b.XZZ_SI,'')+isnull(b.XZZ_QX,'')+isnull(b.XZZ_JD,'') end) curr_addr, -- 现住址 字符型 200   
 b.GZDWJDZ emp_name, -- 单位名称 字符型 200    
 b.GZDWJDZ emp_addr, -- 单位地址 字符型 200    
 b.DWDH emp_tel, -- 单位电话 字符型 50   
 b.DWYB poscode, -- 邮编 字符型 6  
 null coner_name, -- 联系人姓名 字符型 50 Y    
 --b.GX patn_rlts, -- 与患者关系 字符型 6 Y Y    
  isnull(h.yb_code ,1) patn_rlts,  
 (case when b.LXRDZ_SN<>'' then isnull(b.LXRDZ_SN,'')+isnull(b.LXRDZ_SI,'')+isnull(b.LXRDZ_QX,'')+isnull(b.LXRDZ_JD,'') end) coner_addr, -- 联系人地址 字符型 200 Y   
 b.LXRDH coner_tel, -- 联系人电话 字符型 50 Y   
 '' nwb_adm_type, -- 新生儿入院类型 字符型 3 Y  
 b.XSECSTZ nwb_bir_wt, -- 新生儿出生体重 数值型 6,2 精确到 10 克(g)  
 b.XSERYTZ nwb_adm_wt, -- 新生儿入院体重 数值型 6,2 精确到 10 克(g)  
 '' mul_nwb_bir_wt,  
 '' mul_nwb_adm_wt,  
 null opsp_diag_caty, --门诊慢特病诊断科别字符型 50 35    
 null opsp_mdtrt_date, --门诊慢特病就诊日期日期型   
 b.RYTJ adm_way, -- 入院途径 字符型 3 Y   
 '10' trt_type, -- 治疗类别 字符型 3 Y   
 CONVERT(varchar(100),ciszyxx.ryrq, 120) adm_time, -- 入院时间日期时间型  
   ciszyxx.DeptCode adm_dept_codg,--入院科室编码  
  dept.name adm_dept_name,  --入院科室名称  
  '0' traf_dept_flat, --转科室标志  
 c.ZKKB refldept_dept, -- 转科科别 字符型 6 Y参照科室代码（dept），如果超过一次以上的转科，用“→”转接表示  
  CONVERT(varchar(100),ciszyxx.cqrq, 120) dscg_time, --  出院时间日期时间型   
  ciszyxx.DeptCode dscg_dept_codg,--出院科室编码  
  dept.name dscg_dept_name,  --出院科室名称  
 --b.CYKB dscg_caty, --  出院科别 字符型 6 Y Y 参照科室代码（dept）  
 ( select deptcode from DRG_dept where deptname= b.CYKB)dscg_caty,   
 b.MZZD otp_wm_dise, -- 门（急）诊西医诊断字符型 200    
 b.MZZDDM wm_dise_code, -- 西医诊断疾病代码字符型 20    
 '' otp_tcm_dise, -- 门（急）诊中医诊断字符型 200   
 '' tcm_dise_code, -- 中医诊断代码 字符型 20  
 '' vent_used_dura, -- 呼吸机使用时长 字符型 10格式：天数/小时数/分钟数例：1/13/24   
 '' pwcry_bfadm_coma_dura, -- 颅脑损伤患者入院前昏迷时长字符型 10格式：天数/小时数/分钟数例：1/13/24  
 '' pwcry_afadm_coma_dura, --颅脑损伤患者入院后昏迷时长字符型 10格式：天数/小时数/分钟数例：1/13/24    
 0 spga_nurscare_days, --特级护理天数 数值型 3    
 0 lv1_nurscare_days, --一级护理天数 数值型 3    
 0 scd_nurscare_days, --二级护理天数 数值型 3    
 0 lv3_nurscare_days, --三级护理天数 数值型 3  
 b.LYFS dscg_way, -- 离院方式 字符型 3 Y   
 b.YZZY_YLJG acp_medins_name, --拟接收机构名称  
 '' acp_optins_code, --拟接收机构代码  
 zyjs.jsnm bill_code, -- 票据代码 字符型 50 Y  
 zyjs.jsnm bill_no, -- 票据号码 字符型 30 Y  
 a.medins_setl_id biz_sn, -- 业务流水号 字符型 50 Y 业务流水号  
 b.SFZZYJH days_rinp_flag_31, --出院 31 天内再住院计划标志字符型 3 Y    
 b.MD days_rinp_pup_31, --出院 31 天内再住院目的字符型 200  
 --c.ZZYS chfpdr_code, -- 主诊医师代码 字符型 30  
 --case when c.zzys is null then j.gh else c.zzys  end chfpdr_code,  
  --j.gjybdm chfpdr_code,  
 '' chfpdr_code,  
 CONVERT(varchar(100),b.RYSJ, 120) setl_begn_date, --结算开始日期 日期型 Y   
  CONVERT(varchar(100),zyjs.CreateTime, 120) setl_end_date, --结算结束日期 日期型 Y   
 d.DepartmentCode medins_fill_dept, --医疗机构填报部字符型 100 Y   
 a.czydm medins_fill_psn, --医疗机构填报人  
 ''resp_nurs_code, a.clr_way ,
  (case when a.clr_way in(1,2,9) then a.clr_way when a.clr_way=3 then 5 when a.clr_way=4 then 6 else 9 end) hi_paymtd, -- 医保支付方式 字符型 3 Y Y    
  ''stas_type,
  @age_days age_days/*新生儿天龄*/
 --a.psn_name, --人员姓名 字符型 50 Y    
 --a.gend, -- 性别 字符型 6 Y Y    
 --a.brdy, -- 出生日期 日期型 Y    
 --a.age, --     
 --b.BZYZSNL nwb_age, --（年龄不足 1 周岁）年龄数值型 3 小于 1 岁时必填，单位天    
 --a.naty, -- 民族 字符型 3 Y Y    
 --a.psn_cert_type  patn_cert_type, --患者证件类别 字符型 3 Y Y    
 --a.certno, -- 证件号码 字符型 50 Y 患者证件号码    
 --a.insutype hi_type, -- 医保类型 字符型 3 Y Y   insutype > hi_type?    
 --'500103' insuplc, -- 参保地 字符型 6 Y    
 --'' sp_psn_type, -- 特殊人员类型 字符型 6 Y    
 --1 ipt_med_type, -- 住院医疗类型 字符型 3 Y Y     
 --c.RYKB adm_caty, -- 入院科别 字符型 6 Y Y 参照科室代码（dept）      
 --b.SJZYTS act_ipt_days, --  实际住院天数 数值型 3     
 --zdcnt diag_code_cnt, --诊断代码计数 数值型 3    
 --(case when b.MZZDDM=zyzd.JBDM then 1 else 0 end) maindiag_flag, -- 主诊断标志 字符型 3 Y Y    
 --ss.cnt oprn_oprt_code_cnt, -- 手术操作代码计数数值型 3    
 --'' bld_cat, -- 输血品种 字符型 3 Y    
 --0 bld_amt, -- 输血量 数值型 6    
 --'' bld_unt, -- 输血计量单位 字符型 3     
 --b.ZZYS chfpdr_name, -- 主诊医师姓名 字符型 50     
 --a.preselfpay_amt psn_selfpay , --个人自付 数值型 16,2 Y    
 --a.fulamt_ownpay_amt psn_ownpay , --个人自费 数值型 16,2 Y    
 --a.acct_pay , --个人账户支出 数值型 16,2 Y    
 --a.psn_cash_pay psn_cashpay, -- 个人现金支付 数值型 16,2 Y    
 --'65840' hsorg , --医保机构 字符型 100 Y     
 --'HIS1' hsorg_opter , --医保机构经办人 字符型 50    
 --select *    
 from drjk_zyjs_input i with(nolock)     
 left join drjk_zyjs_output a with(nolock)  on i.setl_id=a.setl_id and i.psn_no=a.psn_no and a.zt='1'    
 left join Newtouch_EMR.dbo.mr_basy b with(nolock)  on a.zyh=b.ZYH  and b.zt='1'  and b.organizeid=@orgId  
 left join Newtouch_EMR.dbo.mr_basy_rel_code c with(nolock)  on b.id=c.syid and b.organizeid=c.organizeid   
 left join (select zyjs.* from  zy_js zyjs    
 left join zy_js zyjs1 on  zyjs.jsnm=zyjs1.cxjsnm or zyjs.cxjsnm=zyjs1.jsnm where zyjs.zyh=@zyh  
and zyjs1.jsnm is null) zyjs on i.zyh=zyjs.zyh and zyjs.zt='1' and b.organizeid=zyjs.organizeid   
  left join [drjk_rybl_input] rybl with(nolock) on i.zyh=rybl.zyh and rybl.zt='1'  
  left join Newtouch_CIS..zy_brxxk ciszyxx on i.zyh=ciszyxx.zyh and ciszyxx.zt='1'  and b.organizeid=ciszyxx.organizeid   
 --left join(select organizeid,bah,zyh,count(ssjczbm)cnt     
 --  from Newtouch_EMR.dbo.mr_basy_ss   with(nolock)    
 --  where zt='1'    
 --  group by bah,zyh,organizeid) ss on b.BAH=ss.BAH and b.OrganizeId=ss.OrganizeId and b.ZYH=ss.ZYH    
 --left join(select organizeid,bah,zyh,count(JBDM)zdcnt     
 --  from Newtouch_EMR.dbo.mr_basy_zd  with(nolock)    
 --  where zt='1'    
 --  group by bah,zyh,organizeid) zd on b.BAH=zd.BAH and b.OrganizeId=zd.OrganizeId and b.ZYH=zd.ZYH    
 --left join Newtouch_EMR.dbo.mr_basy_zd zyzd with(nolock) on b.BAH=zyzd.BAH and b.OrganizeId=zyzd.OrganizeId and b.ZYH=zyzd.ZYH and zyzd.ZDOrder=1    
 left join NewtouchHIS_Base.[dbo].[V_S_Sys_Staff] d on a.czydm=d.gh --and OrganizeId=''    
 left join [NewtouchHIS_Base].[dbo].[Sys_Department] dept on ciszyxx.deptCode=dept.Code and ciszyxx.organizeId=dept.organizeId and dept.zt=1   
 left join [NewtouchHIS_Sett].dbo.DRG_dept f on  f.deptName=dept.name   
 left join [Newtouch_MRMS].dbo.[mr_dic_common] g on g.RlueName='关系' and g.itemName=b.GX  
  left join #yb_gx h on h.ba_name=b.GX  
  left join [NewtouchHIS_Base].dbo.sys_staff j on j.name =b.zzys  
 where i.zt='1' and a.zt='1'    
 and  d.OrganizeId=@orgId and     
 a.zyh=@zyh    
 order by a.setl_id desc/*上传最后一次的结算信息*/
    
    
 drop table #yb_gx  
return
  
  
  
  
  
  

