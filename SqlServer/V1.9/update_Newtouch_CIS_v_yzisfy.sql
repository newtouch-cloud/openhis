USE Newtouch_CIS
GO

ALTER VIEW v_yzisfy AS
SELECT   a.Id, a.OrganizeId, a.lyxh, a.zyh, a.hzxm, a.yzxh, a.fzxh, a.yfdm, a.WardCode, a.DeptCode, a.ysgh, a.zxrq, a.qqrq, 
                a.fyrq, a.ypdm, a.ypmc, CASE WHEN lsyz.yzlx = '10' THEN CAST(ypsl * mcsl AS int) ELSE ypsl END AS ypsl, a.ypgg, 
                a.ypdw, a.dwxs, a.ykxs, a.ypdj, a.zxcs, a.tybz, a.fyczyh, a.yzxz, a.zbbz, a.memo, a.mcsl, a.CreateTime, a.CreatorCode, 
                a.LastModifyTime, a.LastModifierCode, a.zt, a.ytsl, lsyz.yzlx, b.zxId, b.fybz
FROM      dbo.zy_fyqqk AS a WITH (nolock) LEFT OUTER JOIN
                dbo.zy_lsyz AS lsyz WITH (nolock) ON lsyz.Id = a.yzxh AND lsyz.OrganizeId = a.OrganizeId AND 
                lsyz.zt = '1' INNER JOIN
                NewtouchHIS_PDS.dbo.zy_ypyzxx AS b WITH (nolock) ON a.yzxh = b.yzId AND a.zyh = b.zyh AND ISNULL(b.zt, '1') 
                <> 0 AND a.lyxh = b.lyxh AND SUBSTRING(CONVERT(varchar, a.zxrq, 120), 1, 10) = SUBSTRING(CONVERT(varchar, 
                b.zxrq, 120), 1, 10)