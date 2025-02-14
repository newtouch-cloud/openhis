--下载的医保目录语句导入公共库
insert into NewtouchHIS_Base.dbo.xt_yp_base (ypid,[ypCode], [ypmc], [OrganizeId], [ypqz], [yphz], [spm], [py], [cfl], [cfdw], [jl], [jldw], [bzs], [bzdw], [mzcls], [mzcldw], [zycls], [zycldw], [zxdw], [djdw], [lsj], [pfj], [zfbl], [zfxz], [dlCode], [jx], [ycmc], [medid], [medextid], [ypbzdm], [nbdl], [mzzybz], [CreatorCode], [CreateTime], [LastModifyTime], [LastModifierCode], [zt], [px], [lsbz], [mjzbz], [yfCode], [isKss], [kssId], [jybz], [bz], [cxjje], [tsypbz])
SELECT    row_number() over (order by REG_NAME) ypid,
'000000'+convert(varchar,row_number() over (order by REG_NAME) ) [ypCode], REG_NAME [ypmc], '*' [OrganizeId],'' [ypqz], '' [yphz], '' [spm],
NewtouchHIS_Base.[dbo].[f_GetPy](REG_NAME) [py], 
NULL [cfl], '' [cfdw],1.00 [jl],MIN_PREPUNT [jldw], convert(numeric(19,2),isnull(MIN_PAC_CNT,0.0)) [bzs],MIN_PACUNT [bzdw], 
convert(numeric(19,4),MIN_PAC_CNT) [mzcls], MIN_PACUNT [mzcldw], 
1.00 [zycls],MIN_PACUNT [zycldw], MIN_PREPUNT [zxdw],MIN_PACUNT [djdw], 0.00 [lsj],0.00 [pfj], 0.00 [zfbl],'4' [zfxz], '01' [dlCode],
 jxid [jx], PRODENTP_NAME [ycmc],0 [medid], 0 [medextid], '' [ypbzdm],'' [nbdl], '1' [mzzybz],'admin1' [CreatorCode],GETDATE() [CreateTime], 
NULL [LastModifyTime],NULL [LastModifierCode],'1' [zt], NULL [px],NULL [lsbz], NULL [mjzbz], NULL [yfCode],'0' [isKss],NULL [kssId],
'3' [jybz],NULL [bz],NULL [cxjje], NULL [tsypbz]
--'000000'+convert(varchar,row_number() over (order by REG_NAME) ) [ypId], '*' [OrganizeId],'' [ypCode], NULL [shbz], NULL [tsbz],NULL [jsbz],NULL [gzy],NULL [mzy],NULL [yljsy],NULL [zbbz], NULL[zlff],
--NULL [sjap],NULL [yl], NULL [yldw],DRUG_SPEC [ypgg],MED_LIST_CODG [ybdm], NULL [syts],NULL [dczdjl],NULL [dczdsl],NULL [ljzdjl],NULL [ljzdsl], 
--NULL [pzwh],NULL [yptssx],NULL [ypflCode],'1' [jzlx],'0' [mrbzq],NULL [zjtzsj],NULL [xglx],NULL [ghdw],'0' [ypcd], 'admin1'[CreatorCode], 
--GETDATE() [CreateTime],NULL [LastModifyTime],NULL [LastModifierCode], '1' [zt],NULL [px],NULL [xzyy],NULL [xzyysm],NULL [LastYBUploadTime],
--NULL [mrjl], NULL [mrpc],'1' [ybbz],NULL [xnhybdm],MED_LIST_CODG [gjybdm],NULL [ybmlscrq],REG_NAME [gjybmc],NULL [xjbs], NULL[dcxl],NULL [mbxl], 
--NULL [mryf],NULL [ybgg],NULL [ypzsm]


FROM 
    NewtouchHIS_Base.[dbo].G_yb_wm_tcmpat_info_b b WITH (NOLOCK)
	left join NewtouchHIS_Base.[dbo].xt_ypjx d on d.jxmc=b.DRUG_DOSFORM
WHERE 
    NOT EXISTS (
        SELECT 1 
        FROM NewtouchHIS_Base.[dbo].xt_yp_base 
        WHERE xt_yp_base.VER = b.VER
    )

	