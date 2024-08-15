
/*
author:mohaijiang
time:2024年8月15日
des: 入院登记表
*/
CREATE PROCEDURE [NewtouchHis_Base].[dbo].[RPT_SETT_入院登记表]
(
    @hospitalCode varchar(50),
    @patid varchar(50)
)
AS

BEGIN
SELECT  c.patid ,
        c.CardNo AS kh ,
        zy.zyh ,
        zy.zyh AS zyh2 ,
        jb.xm ,
        jb.zjh ,
        jb.py,
        CONVERT(VARCHAR(100), jb.csny, 23) csny ,
        case jb.xb when 1 then '男' else '女' end as xb,
        CAST( (CASE  WHEN zy.zyh IS NULL THEN FLOOR(datediff(DY,jb.csny,getdate())/365.25) ELSE zy.nl END) as SMALLINT) nl,
        jb.brly ,
        jb.blh ,
        jb.dh ,
        jb.hf ,
        staff2.staffName jkjlmc,
        zy.cw ,
        ( SELECT    zdmc
          FROM      NewtouchHIS_Sett..zy_rydzd
          WHERE     zdpx = 1   AND zt=1
            AND zyh = zy.zyh AND zy_rydzd.OrganizeId= @hospitalCode
        ) zdmc1 ,
        ( SELECT    zdmc
          FROM      NewtouchHIS_Sett..zy_rydzd
          WHERE     zdpx = 2   AND zt=1
            AND zyh = zy.zyh AND zy_rydzd.OrganizeId= @hospitalCode
        ) zdmc2 ,
        ( SELECT    zdmc
          FROM      NewtouchHIS_Sett..zy_rydzd
          WHERE     zdpx = 3   AND zt=1
            AND zyh = zy.zyh AND zy_rydzd.OrganizeId= @hospitalCode
        ) zdmc3 ,
        FORMAT(ryrq, 'yyyy-MM-dd') as ryrq ,
        CONVERT(VARCHAR(100), cyrq, 23) cyrq ,
        ks.Name ksmc ,
        ks.Code ks,
        bq.bqmc ,
        ISNULL(zy.gms, jb.gms) gms,
        case zy.rybq  when 1 then '一般' when 2 then '病重' when 3  then '病危' else '' end as rybq,
        ISNULL(zy.zy,jb.zy) zy ,
        ISNULL(zy.cs_sheng, jb.cs_sheng) cs_sheng ,
        ISNULL(zy.cs_shi, jb.cs_shi) cs_shi ,
        ISNULL(zy.cs_xian, jb.cs_xian) cs_xian ,
        ISNULL(zy.cs_dz, jb.cs_dz) cs_dz ,
        ISNULL(zy.xian_dz, jb.xian_dz) xian_dz ,
        ISNULL(zy.xian_sheng, jb.xian_sheng) xian_sheng ,
        ISNULL(zy.xian_shi, jb.xian_shi) xian_shi ,
        ISNULL(zy.xian_xian, jb.xian_xian) xian_xian ,
        ISNULL(zy.hu_dz, jb.hu_dz) hu_dz ,
        ISNULL(zy.hu_sheng, jb.hu_sheng) hu_sheng ,
        ISNULL(zy.hu_shi, jb.hu_shi) hu_shi ,
        ISNULL(zy.hu_xian, jb.hu_xian) hu_xian ,
        ISNULL(ISNULL(zy.cs_sheng, jb.cs_sheng),'') +  isnull(ISNULL(zy.cs_shi, jb.cs_shi),'' )+ ISNULL(ISNULL(zy.cs_xian, jb.cs_xian),'') + ISNULL(ISNULL(zy.cs_dz, jb.cs_dz),'') as birthAddress,
        ISNULL(ISNULL(zy.xian_sheng, jb.xian_sheng),'') +  isnull(ISNULL(zy.xian_shi, jb.xian_shi),'' )+  ISNULL(ISNULL(zy.xian_xian, jb.xian_xian),'') + ISNULL(ISNULL(zy.xian_dz, jb.xian_dz),'') as xianAddress,
        ISNULL(ISNULL(zy.hu_sheng, jb.hu_sheng),'') + ISNULL(ISNULL(zy.hu_shi, jb.hu_shi),'') + ISNULL(ISNULL(zy.hu_xian, jb.hu_xian),'') + ISNULL(ISNULL(zy.hu_dz, jb.hu_dz),'') as huAddress,
        ISNULL(zy.jjlxr_sheng, jb.jjlxr_sheng) jjlxr_sheng ,
        ISNULL(zy.jjlxr_shi, jb.jjlxr_shi) jjlxr_shi ,
        ISNULL(zy.jjlxr_xian, jb.jjlxr_xian) jjlxr_xian ,
        ISNULL(zy.jjlxr_dz, jb.jjlxr_dz) jjlxr_dz ,
        ISNULL(ISNULL(zy.jjlxr_sheng, jb.jjlxr_sheng),'') + ISNULL(ISNULL(zy.jjlxr_shi, jb.jjlxr_shi),'') + ISNULL(ISNULL(zy.jjlxr_xian, jb.jjlxr_xian),'') +         ISNULL(ISNULL(zy.jjlxr_dz, jb.jjlxr_dz),'') as jjlxrAddress,
        case ( ISNULL(zy.hy, jb.hf) ) when 0 then '未婚' when '1' then '已婚' when 9 then '不详' else '' end as  hy ,
        gj.gjmc ,
        gj.gjCode ,
        mz.mzmc ,
        mz.mzCode ,
        ( ISNULL(zy.bje, 0) ) bje ,
        ISNULL(zy.lxr,jb.jjllr) lxr ,
        relaItem.Name as lxrRelaName,
        zy.lxr2 ,
        jb.dybh,
        (ISNULL(zy.lxrdh,jb.jjlldh)) lxrdh ,
        zy.lxryddh2 ,
        zy.lxrdz ,
        zy.lxrdz2 ,
        zy.lxrEmail ,
        zy.lxrEmail2 ,
        ( ISNULL(zy.lxrgx,jb.jjllrgx) ) lxrgx ,
        ( ISNULL(zy.lxrgx2, '') ) lxrgx2 ,
        zy.lxrjtdh ,
        zy.lxrjtdh2 ,
        zy.lxrWebchat ,
        zy.lxrWebchat2 ,
        case zy.rytj when '9' then '其他' when '21' then '普通住院' when '23' then '转外诊治住院' when '24' then '急诊转住院' when '52' then '生育住院' when '9902' then '重大疾病住院' when '9904' then '儿童两病住院' when '9907' then '耐多药结核住院' when '9921' then '新生儿随母住院' when '9922' then '120' when '9925' then '转入住院' else '' end as rytj,
        case when zy.zyh is not null then zyxz.brxzmc else jbxz.brxzmc end brxzmc,
        case when zy.zyh is not null then zyxz.brxzbh else jbxz.brxzbh end brxzbh,
        case when zy.zyh is not null then zyxz.brxz else jbxz.brxz end brxz,
        staff2.staffGh jkjl ,
        staff.staffgh doctor ,
        staff.staffName doctormc ,
        ( SELECT    CAST(zdCode AS VARCHAR(10)) + '-'
                        + CAST(zdmc AS VARCHAR(30))
          FROM      NewtouchHIS_Sett..zy_rydzd
          WHERE     zdpx = 1   AND zt=1
            AND zyh = zy.zyh AND zy_rydzd.OrganizeId= @hospitalCode
        ) ryzd1 ,
        ( SELECT    CAST(zdCode AS VARCHAR(10)) + '-'
                        + CAST(zdmc AS VARCHAR(30))
          FROM      NewtouchHIS_Sett..zy_rydzd
          WHERE     zdpx = 2   AND zt=1
            AND zyh = zy.zyh AND zy_rydzd.OrganizeId= @hospitalCode
        ) ryzd2 ,
        ( SELECT    CAST(zdCode AS VARCHAR(10)) + '-'
                        + CAST(zdmc AS VARCHAR(30))
          FROM      NewtouchHIS_Sett..zy_rydzd
          WHERE     zdpx = 3   AND zt=1
            AND zyh = zy.zyh AND zy_rydzd.OrganizeId= @hospitalCode
        ) ryzd3 ,
        CAST(bq.bqId AS VARCHAR(10)) + '-' + CAST(bq.bqCode AS VARCHAR(10)) bq ,
        jb.zjlx ,
        zy.ys,
        jb.phone,jb.jjlldh,jb.jjllr,jb.bz,jb.dwdz dz,jb.dwmc,zy.zcyy ,c.cblb,
        zy.ssczdm,zy.ssczmc,zy.syfwzh,zy.sylb,zy.sysslb,zy.wybz,zy.yzs,zy.tc,zy.tes,zy.zcbz,zy.syrq
FROM   NewtouchHIS_Sett..xt_brjbxx jb
           LEFT JOIN NewtouchHIS_Sett.dbo.zy_brjbxx zy ON zy.patid = jb.patid  AND  zy.OrganizeId=@hospitalCode
           LEFT JOIN NewtouchHIS_Sett..xt_card c  ON zy.kh = c.cardno and jb.OrganizeId= @hospitalCode
           LEFT JOIN NewtouchHIS_Sett.dbo.xt_brxz jbxz ON jbxz.brxz = zy.brxz
    AND jbxz.zt = '1' AND jbxz.OrganizeId=@hospitalCode
           LEFT JOIN NewtouchHIS_Sett.dbo.xt_brxz zyxz ON zyxz.brxz = zy.brxz
    AND zyxz.zt = '1' AND zyxz.OrganizeId=@hospitalCode
           LEFT JOIN NewtouchHIS_Sett.dbo.zy_rydzd zd ON zd.zyh = zy.zyh AND zd.OrganizeId=@hospitalCode
           LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_Department ks ON ks.Code = zy.ks
    AND ks.OrganizeId = @hospitalCode
    LEFT JOIN NewtouchHIS_Base..V_S_xt_bq bq ON zy.bq = bq.bqCode AND bq.OrganizeId=@hospitalCode
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_gj gj ON zy.gj = gj.gjCode
    LEFT JOIN NewtouchHIS_Base.dbo.V_C_Sys_StaffDuty staff ON staff.StaffGh = zy.doctor  AND staff.DutyCode='Doctor' AND staff.OrganizeId=@hospitalCode
    LEFT JOIN NewtouchHIS_Base.dbo.V_C_Sys_StaffDuty staff2 ON staff2.StaffGh = zy.jkjl AND staff2.DutyCode='RehabDoctor' AND staff2.OrganizeId=@hospitalCode
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_mz mz ON mz.mzCode = zy.mz
    LEFT JOIN NewtouchHIS_Base.dbo.Sys_ItemsDetail relaItem on (relaItem.code = zy.lxrgx  and relaItem.ItemId = '316da863-9ee1-4c09-b326-e43cee24bc61')
where jb.patid=@patid




END





go
