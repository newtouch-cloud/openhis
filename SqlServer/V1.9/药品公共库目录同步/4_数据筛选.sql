SELECT  top 10  row_number() over (order by REG_NAME),
jxId,
    MED_LIST_CODG, 
    REG_NAME, 
    DRUGSTDCODE, 
    DRUG_DOSFORM, 
    DRUG_DOSFORM_NAME, 
    DRUG_TYPE, 
    DRUG_SPEC, 
    REG_DOSFORM, 
    REG_SPEC, 
    EACH_DOS, 
    OTC_FLAG,
    PACMATL, 
    EFCC_ATD, 
    MIN_PAC_CNT, 
    MIN_PACUNT, 
    MIN_PREPUNT, 
    DRUG_EXPY, 
    MIN_PRCUNT, 
    PRODENTP_CODE, 
    PRODENTP_NAME, 
    MKT_STAS, 
    VER,
    CASE VALI_FLAG WHEN '1' THEN 'ÊÇ' WHEN '0' THEN '·ñ' ELSE '' END AS VALI_FLAG,
    VER_NAME 
FROM 
    NewtouchHIS_Base.[dbo].G_yb_wm_tcmpat_info_b b WITH (NOLOCK)
	left join NewtouchHIS_Base.dbo.xt_ypjx d on d.jxmc=b.DRUG_DOSFORM
WHERE 
    NOT EXISTS (
        SELECT 1 
        FROM NewtouchHIS_Base.dbo.xt_yp_base 
        WHERE xt_yp_base.VER = b.VER
    )