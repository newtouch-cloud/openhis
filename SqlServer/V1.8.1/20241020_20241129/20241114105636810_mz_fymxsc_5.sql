USE [NewtouchHIS_Sett]
GO

-- 事件类型: ALTER_PROCEDURE
-- 变更时间: 11/14/2024 10:56:36


/*
exec mz_fymxsc '0','2304202120016',null,null,null,'false','false','6d5752a7-234a-403e-aa1c-df8b45d3469f','843710463','13030011303013054434','2023420163322_nciro041y',null,'0204','000164',null
*/

  
  
--门诊费用明细上传  
ALTER PROCEDURE [dbo].[mz_fymxsc]   
@type varchar(1),--0挂号  1处方  3 处方退费再传
@mzh varchar(20),  
@cfnm varchar(500), --处方内码
@ghxm varchar(20),  --挂号项目
@zlxm varchar(20),  --诊疗项目
@ckf  varchar(20),  --磁卡费
@gbf varchar(20),  --工本费
@orgId varchar(50),  --组织机构
@jzid varchar(50),  --就诊id
@rybh varchar(50),  --人员编号
@pch varchar(50), --收费批次号
@jsnm varchar(50), --结算内码 
@ksbm varchar(50),--科室编码
@ysbm varchar(50),--医生编码
@dise_codg varchar(50)--病种编码
as  
  
--INSERT xt_lshtb(col) VALUES(1) --新增明细流水号  
if @type='0'  
begin  
declare @ksmc varchar(50);
declare @ysmc varchar(50);
declare @gjysbm varchar(50);
select @ysmc=Name,@gjysbm=gjybdm from NewtouchHIS_Base..V_S_Sys_Staff where gh=@ysbm and organizeid=@orgId and zt=1
select @ksmc=Name from NewtouchHIS_Base..V_S_Sys_Department where Code=@ksbm and organizeid=@orgId and zt=1
--门诊挂号诊疗项目上传  
SELECT cast(CONVERT(bigint, CONVERT(varbinary, CAST(N'0x' + replace(newid(),'-','') AS char),1)) as varchar) feedetl_sn ,--费用明细流水号  
  @jzid mdtrt_id,--就诊 ID  
  @rybh psn_no,--人员编号  
  @pch chrg_bchno,--收费批次号  
  '' dise_codg,--病种编码  
  @mzh rxno,--处方号  
  '0' rx_circ_flag,--外购处方标志  
   convert(varchar(50),GETDATE(),120)   fee_ocur_time, --费用发生时间  
  gjybdm med_list_codg,--医疗目录编码  
  sfxmcode medins_list_codg,--医药机构目录编码  
   Convert(decimal(13,2),dj) det_item_fee_sumamt,--明细项目费用总额  
        CONVERT(NUMERIC(10,2),1) cnt ,--数量  
  dj pric,--单价  
  '' sin_dos_dscr,--单次剂量描述  
  ''used_frqu_dscr,--使用频次描述  
  CONVERT(NUMERIC(4,2),1) prd_days,--周期天数  
  '' medc_way_dscr,--用药途径描述  
  @ksbm bilg_dept_codg,--开单科室编码  
  @ksmc bilg_dept_name,--开单科室名称  
  @gjysbm bilg_dr_codg, --开单医生编码  
  @ysmc bilg_dr_name,--开单医师姓名  
  @ksbm acord_dept_codg,--受单科室编码  
  @ksmc acord_dept_name,--受单科室名称  
  @gjysbm orders_dr_code,--受单医生编码  
  @ysmc orders_dr_name,--受单医生姓名  
  cast('1' as varchar) hosp_appr_flag,--医院审批标志  
  '' tcmdrug_used_way,--中药使用方式  
  '' etip_flag,--外检标志  
  '' etip_hosp_code,--外检医院编码  
  '' dscg_tkdrug_flag,--出院带药标志  
  '' matn_fee_flag--生育费用标志  
FROM    NewtouchHIS_Base..V_S_xt_sfxm  
WHERE  isnull(zfxz,'1')!='1' and OrganizeId =@orgId AND zt='1'  
        AND (sfxmCode IN ( @ghxm ) or 
        sfxmCode IN ( select zlxm from mz_gh_zlxmzh with(nolock) where zt='1'   
and OrganizeId=@orgId and zhcode=@zlxm ) or sfxmCode IN ( @ckf ) or sfxmCode IN (  @gbf ))  
end  
else if  @type='3'
begin
     SELECT    'yp'+cast(mx.cfmxid as varchar) feedetl_sn ,--费用明细流水号  
  gh.jzid mdtrt_id,--就诊 ID  
  cd.grbh psn_no,--人员编号  
  @pch chrg_bchno,--dbo.f_NextBH() chrg_bchno,--收费批次号  
  @dise_codg dise_codg,--病种编码  
  cf.cfh rxno,--处方号  
  '0' rx_circ_flag,--外购处方标志  
   convert(varchar(50),mx.CreateTime,120) fee_ocur_time, --费用发生时间  
  yp.gjybdm med_list_codg,--医疗目录编码  
  yp.ypCode medins_list_codg,--医药机构目录编码  
   Convert(decimal(13,2),ISNULL(mx.je, 0.00)) det_item_fee_sumamt,--明细项目费用总额  
        ISNULL(mx.sl, 0.00) cnt ,--数量  
  ISNULL(mx.dj, 0.00) pric,--单价  
  cast(mx.jl as varchar) sin_dos_dscr,--单次剂量描述  
  ''used_frqu_dscr,--使用频次描述  
  CONVERT(NUMERIC(4,2),1) prd_days,--周期天数  
  '' medc_way_dscr,--用药途径描述  
  ks.ybksbm bilg_dept_codg,--开单科室编码  
  ks.Name bilg_dept_name,--开单科室名称 
  isnull(staff.gjybdm,staff.zjh) bilg_dr_codg, --开单医生编码  
  --isnull(staff.zjh,cf.ys) bilg_dr_codg, --开单医生编码  
  cf.ysmc bilg_dr_name,--开单医师姓名  
  ks.ybksbm acord_dept_codg,--受单科室编码  
  ks.Name acord_dept_name,--受单科室名称  
  isnull(staff.gjybdm,staff.zjh) orders_dr_code,--受单医生编码  
  --isnull(staff.zjh,cf.ys) orders_dr_code,--受单医生编码 
  cf.ysmc orders_dr_name,--受单医生姓名  
  case isnull(zzfbz,'0') when '1' then '2' else '1' end hosp_appr_flag,--医院审批标志  
  '' tcmdrug_used_way,--中药使用方式  
  '' etip_flag,--外检标志  
  '' etip_hosp_code,--外检医院编码  
  '' dscg_tkdrug_flag,--出院带药标志  
  case gh.mjzbz when  '9' then '1' else '' end matn_fee_flag, --生育费用标志 
  a.jsmxnm
          FROM      [NewtouchHIS_Sett].[dbo].[mz_jsmx] (NOLOCK) a
                    LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_cfmx] (NOLOCK) mx ON a.cf_mxnm = mx.cfmxId
                                                              AND mx.OrganizeId = a.OrganizeId
                                                              AND mx.zt = '1'
                    LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_cf] (NOLOCK) cf ON cf.cfnm = mx.cfnm
                                                              AND cf.OrganizeId = mx.OrganizeId
                                                              AND cf.zt = '1'
                    INNER JOIN dbo.mz_gh (NOLOCK) gh ON gh.ghnm = cf.ghnm  
                                                    AND gh.zt = '1'  
                                                    AND gh.OrganizeId = cf.OrganizeId  
                INNER JOIN dbo.xt_brjbxx (NOLOCK) xtxx on xtxx.patid=gh.patid and xtxx.OrganizeId=gh.OrganizeId and xtxx.zt=1 
                    INNER JOIN NewtouchHIS_Base..V_C_xt_yp yp ON yp.ypCode = mx.yp
                                                              AND yp.OrganizeId = mx.OrganizeId
                                                              AND yp.zt = '1'
                                                              AND  isnull(yp.zfxz,'1')!='1'  
                    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode = yp.dlCode
                                                              AND sfdl.zt = '1'
                                                              AND sfdl.OrganizeId = a.OrganizeId
                    LEFT JOIN NewtouchHIS_Base..Sys_Department (NOLOCK) ks ON ks.OrganizeId = cf.OrganizeId
                                                              AND ks.Code = cf.ks
                                                              AND ks.zt = '1'
                    LEFT JOIN NewtouchHIS_Base..Sys_Staff (NOLOCK) staff ON staff.OrganizeId = cf.OrganizeId
                                                              AND staff.gh = cf.ys
                                                              AND staff.zt = '1'
                    LEFT JOIN xt_card (NOLOCK) cd ON cd.OrganizeId = gh.OrganizeId and cd.CardTypeName=gh.CardTypeName
                                                              AND cd.CardNo = gh.kh
                                                              AND cd.zt = '1'
                   
          WHERE     a.jsnm = @jsnm
                    AND ISNULL(a.sl,0)>0
                    AND ISNULL(a.cf_mxnm, 0) != 0
                    AND a.OrganizeId = @orgId
                    AND a.zt = '1'
          UNION ALL
          SELECT    'xm'+cast(xm.xmnm as varchar) feedetl_sn ,--费用明细流水号  
  gh.jzid mdtrt_id,--就诊 ID  
  cd.grbh psn_no,--人员编号  
  @pch chrg_bchno,--dbo.f_NextBH() chrg_bchno,--收费批次号  
  @dise_codg dise_codg,--病种编码  
  cf.cfh rxno,--处方号  
  '0' rx_circ_flag,--外购处方标志  
   convert(varchar(50),a.CreateTime,120) fee_ocur_time, --费用发生时间  
  sfxm.gjybdm med_list_codg,--医疗目录编码  
  sfxm.sfxmCode medins_list_codg,--医药机构目录编码  
   Convert(decimal(13,2),ISNULL(xm.je, 0.00)) det_item_fee_sumamt,--明细项目费用总额  
        ISNULL(xm.sl, 0.00) cnt ,--数量  
  ISNULL(xm.dj, 0.00) pric,--单价  
  null sin_dos_dscr,--单次剂量描述  
  ''used_frqu_dscr,--使用频次描述  
  CONVERT(NUMERIC(4,2),1) prd_days,--周期天数  
  '' medc_way_dscr,--用药途径描述  
  ks.ybksbm bilg_dept_codg,--开单科室编码  
  ks.Name bilg_dept_name,--开单科室名称  
  isnull(staff.gjybdm,staff.zjh) bilg_dr_codg, --开单医生编码  
  --isnull(staff.zjh,cf.ys) bilg_dr_codg, --开单医生编码  
  cf.ysmc bilg_dr_name,--开单医师姓名  
  ks.ybksbm acord_dept_codg,--受单科室编码  
  ks.Name acord_dept_name,--受单科室名称  
  isnull(staff.gjybdm,staff.zjh) orders_dr_code,--受单医生编码  
  --isnull(staff.zjh,cf.ys) orders_dr_code,--受单医生编码 
  cf.ysmc orders_dr_name,--受单医生姓名    
  case isnull(zzfbz,'0') when '1' then '2' else '1' end hosp_appr_flag,--医院审批标志  
  '' tcmdrug_used_way,--中药使用方式  
  '' etip_flag,--外检标志  
  '' etip_hosp_code,--外检医院编码  
  '' dscg_tkdrug_flag,--出院带药标志  
  case gh.mjzbz when  '9' then '1' else '' end matn_fee_flag, --生育费用标志  
  a.jsmxnm
          FROM      [NewtouchHIS_Sett].[dbo].[mz_jsmx] (NOLOCK) a
                    LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_xm] (NOLOCK) xm ON a.mxnm = xm.xmnm
                                                              AND xm.OrganizeId = a.OrganizeId
                                                              AND xm.zt = '1'
                    LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_cf] (NOLOCK) cf ON cf.cfnm = xm.cfnm
                                                              AND cf.OrganizeId = xm.OrganizeId
                                                              AND cf.zt = '1'
                    INNER JOIN dbo.mz_gh (NOLOCK) gh ON gh.ghnm = cf.ghnm  
                                                    AND gh.zt = '1'  
                                                    AND gh.OrganizeId = cf.OrganizeId  
                INNER JOIN dbo.xt_brjbxx (NOLOCK) xtxx on xtxx.patid=gh.patid and xtxx.OrganizeId=gh.OrganizeId and xtxx.zt=1 
                    INNER JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm ON sfxm.sfxmCode = xm.sfxm
                                                              AND sfxm.OrganizeId = xm.OrganizeId
                                                              AND sfxm.zt = '1'
                                                              AND isnull(sfxm.zfxz,'1')!='1' 
                    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode = sfxm.sfdlCode
                                                              AND sfdl.zt = '1'
                                                              AND sfdl.OrganizeId = a.OrganizeId
                    LEFT JOIN NewtouchHIS_Base..Sys_Department (NOLOCK) ks ON ks.OrganizeId = cf.OrganizeId
                                                              AND ks.Code = cf.ks
                                                              AND ks.zt = '1'
                    LEFT JOIN NewtouchHIS_Base..Sys_Staff (NOLOCK) staff ON staff.OrganizeId = cf.OrganizeId
                                                              AND staff.gh = cf.ys
                                                              AND staff.zt = '1'
LEFT JOIN xt_card (NOLOCK) cd ON cd.OrganizeId = gh.OrganizeId 
                                                              AND cd.CardNo = gh.kh and cd.CardTypeName=gh.CardTypeName
                                                              AND cd.zt = '1'
          WHERE     a.jsnm = @jsnm
                    AND ISNULL(a.sl,0)>0
                    AND ISNULL(a.mxnm, 0) != 0
                    AND a.OrganizeId = @orgId
                    AND a.zt = '1'
     
end
else
begin   
--门诊处方明细上传    
SELECT  'yp'+cast(mx.cfmxid as varchar) feedetl_sn ,--费用明细流水号  
  gh.jzid mdtrt_id,--就诊 ID  
  cd.grbh psn_no,--人员编号  
  @pch chrg_bchno,--dbo.f_NextBH() chrg_bchno,--收费批次号  
  @dise_codg dise_codg,--病种编码  
  cf.cfh rxno,--处方号  
  '0' rx_circ_flag,--外购处方标志  
     convert(varchar(50),mx.CreateTime,120) fee_ocur_time, --费用发生时间  
  yp.gjybdm med_list_codg,--医疗目录编码  
  yp.ypCode medins_list_codg,--医药机构目录编码  
   Convert(decimal(13,2),ISNULL(mx.je, 0.00)) det_item_fee_sumamt,--明细项目费用总额  
        ISNULL(mx.sl, 0.00) cnt ,--数量  
  ISNULL(mx.dj, 0.00) pric,--单价  
  cast(mx.jl as varchar) sin_dos_dscr,--单次剂量描述  
  ''used_frqu_dscr,--使用频次描述  
  CONVERT(NUMERIC(4,2),1) prd_days,--周期天数  
  '' medc_way_dscr,--用药途径描述  
  ks.ybksbm bilg_dept_codg,--开单科室编码  
  ks.Name bilg_dept_name,--开单科室名称  
  isnull(staff.gjybdm,cf.ys) bilg_dr_codg, --开单医生编码  
  cf.ysmc bilg_dr_name,--开单医师姓名  
  ks.ybksbm acord_dept_codg,--受单科室编码  
  ks.Name acord_dept_name,--受单科室名称  
  isnull(staff.gjybdm,cf.ys) orders_dr_code,--受单医生编码   
  cf.ysmc orders_dr_name,--受单医生姓名  
  case isnull(zzfbz,'0') when '1' then '2' else '1' end hosp_appr_flag,--医院审批标志  
  '' tcmdrug_used_way,--中药使用方式  
  '' etip_flag,--外检标志  
  '' etip_hosp_code,--外检医院编码  
  '' dscg_tkdrug_flag,--出院带药标志  
  case gh.mjzbz when  '9' then '1' else '' end matn_fee_flag--生育费用标志  
        FROM      mz_cf (NOLOCK) cf  
                INNER JOIN mz_cfmx (NOLOCK) mx ON cf.cfnm = mx.cfnm  
                                                    AND mx.OrganizeId = cf.OrganizeId  
                                                    AND mx.zt = '1'  
                INNER JOIN dbo.mz_gh (NOLOCK) gh ON gh.ghnm = cf.ghnm  
                                                    AND gh.zt = '1'  
                                                    AND gh.OrganizeId = cf.OrganizeId  
                INNER JOIN dbo.xt_brjbxx (NOLOCK) xtxx on xtxx.patid=gh.patid and xtxx.OrganizeId=gh.OrganizeId and xtxx.zt=1  
                INNER JOIN NewtouchHIS_Base.dbo.V_C_xt_yp yp ON yp.ypCode = mx.yp  
                                                            AND yp.OrganizeId = cf.OrganizeId  
                                                            AND yp.zt = '1'  
                                                           AND isnull(yp.zfxz,'1')!='1'
                LEFT JOIN NewtouchHIS_Base.dbo.xt_sfdl (NOLOCK) dl ON dl.dlCode = mx.dl  
                                                            AND dl.OrganizeId = cf.OrganizeId  
                          AND dl.zt = '1'  
                LEFT JOIN NewtouchHIS_Base..Sys_Department (NOLOCK) ks ON ks.OrganizeId = cf.OrganizeId  
                                                            AND ks.Code = cf.ks  
                                                            AND ks.zt = '1'  
                LEFT JOIN NewtouchHIS_Base..Sys_Staff (NOLOCK) staff ON staff.OrganizeId = cf.OrganizeId  
                                                            AND staff.gh = cf.ys  
                                                            AND staff.zt = '1'  
LEFT JOIN xt_card (NOLOCK) cd ON cd.OrganizeId = gh.OrganizeId
                                                              AND cd.CardNo = gh.kh and cd.CardTypeName=gh.CardTypeName
                                                              AND cd.zt = '1'
        WHERE     cf.OrganizeId = @orgId  
                AND cf.zt = '1'  
                AND cf.cfzt = '0' --处方有效且未收费  
                AND gh.mzh = @mzh  
                --AND cf.cfnm = @cfnm
                AND cf.cfnm IN (SELECT * FROM dbo.f_split(@cfnm, ','))
UNION ALL  
       SELECT   'xm'+cast(xm.xmnm as varchar) feedetl_sn ,--费用明细流水号  
  gh.jzid mdtrt_id,--就诊 ID  
  cd.grbh psn_no,--人员编号  
  @pch chrg_bchno,--dbo.f_NextBH() chrg_bchno,--收费批次号  
  @dise_codg dise_codg,--病种编码  
  cf.cfh rxno,--处方号  
  '0' rx_circ_flag,--外购处方标志  
   convert(varchar(50),xm.CreateTime,120) fee_ocur_time, --费用发生时间  
  sfxm.gjybdm med_list_codg,--医疗目录编码  
  sfxm.sfxmCode medins_list_codg,--医药机构目录编码  
   Convert(decimal(13,2),ISNULL(xm.je, 0.00)) det_item_fee_sumamt,--明细项目费用总额  
        ISNULL(xm.sl, 0.00) cnt ,--数量  
  ISNULL(xm.dj, 0.00) pric,--单价  
  null sin_dos_dscr,--单次剂量描述  
  ''used_frqu_dscr,--使用频次描述  
  CONVERT(NUMERIC(4,2),1) prd_days,--周期天数  
  '' medc_way_dscr,--用药途径描述  
  ks.ybksbm bilg_dept_codg,--开单科室编码  
  ks.Name bilg_dept_name,--开单科室名称  
  isnull(staff.gjybdm,cf.ys) bilg_dr_codg, --开单医生编码  
  cf.ysmc bilg_dr_name,--开单医师姓名  
  ks.ybksbm acord_dept_codg,--受单科室编码  
  ks.Name acord_dept_name,--受单科室名称  
  isnull(staff.gjybdm,cf.ys) orders_dr_code,--受单医生编码   
  cf.ysmc orders_dr_name,--受单医生姓名   
  case isnull(zzfbz,'0') when '1' then '2' else '1' end hosp_appr_flag,--医院审批标志  
  '' tcmdrug_used_way,--中药使用方式  
  '' etip_flag,--外检标志  
  '' etip_hosp_code,--外检医院编码  
  '' dscg_tkdrug_flag,--出院带药标志  
  case gh.mjzbz when  '9' then '1' else '' end matn_fee_flag--生育费用标志  
        FROM      dbo.mz_xm (NOLOCK) xm  
                INNER JOIN dbo.mz_gh (NOLOCK) gh ON gh.ghnm = xm.ghnm  
                                                    AND gh.OrganizeId = xm.OrganizeId  
                                                    AND gh.zt = '1'  
                INNER JOIN dbo.xt_brjbxx (NOLOCK) xtxx on xtxx.patid=gh.patid and xtxx.OrganizeId=gh.OrganizeId and xtxx.zt=1  
                LEFT JOIN dbo.mz_cf (NOLOCK) cf ON cf.cfnm = xm.cfnm  
                                                    AND cf.OrganizeId = xm.OrganizeId  
                INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_sfxm sfxm ON sfxm.sfxmCode = xm.sfxm  
                                                            AND sfxm.OrganizeId = xm.OrganizeId  
                                                           AND isnull(sfxm.zfxz,'1')!='1'    
                LEFT JOIN NewtouchHIS_Base.dbo.xt_sfdl (NOLOCK) dl ON dl.dlCode = xm.dl  
                                                            AND dl.OrganizeId = xm.OrganizeId  
                                                            AND dl.zt = '1'  
                LEFT JOIN NewtouchHIS_Base..Sys_Department (NOLOCK) ks ON ks.OrganizeId = cf.OrganizeId  
                                                            AND ks.Code = cf.ks  
                                                            AND ks.zt = '1'  
                LEFT JOIN NewtouchHIS_Base..Sys_Staff (NOLOCK) staff ON staff.OrganizeId = cf.OrganizeId  
                                                            AND staff.gh = cf.ys  
                                                            AND staff.zt = '1'  
LEFT JOIN xt_card (NOLOCK) cd ON cd.OrganizeId = gh.OrganizeId
                                                              AND cd.CardNo = gh.kh and cd.CardTypeName=gh.CardTypeName
                                                              AND cd.zt = '1'
        WHERE     xm.OrganizeId = @orgId  
                AND xm.zt = '1'  
                AND xm.xmzt = '0' --有效且未收费   
                AND ( cf.zt IS NULL  
                        OR ( cf.zt = '1'  
                            AND cf.cfzt = '0'  
                            )  
                    ) --未关联处方 或 处方有效且未收费  
                AND gh.mzh = @mzh  
                --AND cf.cfnm = @cfnm   
                AND cf.cfnm IN (SELECT * FROM dbo.f_split(@cfnm, ','))
  
end  

