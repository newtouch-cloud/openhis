USE [NewtouchHIS_Base]
GO

-- 事件类型: ALTER_PROCEDURE
-- 变更时间: 11/07/2024 11:04:38




/*
author:tyz
time:2023年3月13日
des:外部出库
*/   
ALTER PROCEDURE [dbo].[RPT_PDS_药品外部出库]    
(
@hospitalCode varchar(50), 
@yfbmCode   varchar(20),
@crkId varchar(50),
@kssj datetime,
@jssj  datetime,
@gyss varchar(20),
@shzt varchar(10),
@fph varchar(30),
@pdh varchar(30)
)    
AS

-- 切割字符串
DECLARE @t TABLE(col VARCHAR(50));
DECLARE @s VARCHAR(5000);
DECLARE @split VARCHAR(1);
SET @s=ISNULL(@gyss,'');
SET @split=',';
while(charindex(@split,@s)<>0)   
begin   
    INSERT @t(col) VALUES (substring(@s,1,charindex(@split,@s)-1))   
    SET @s = STUFF(@s,1,charindex(@split,@s),'')   
end   
INSERT @t(col) VALUES (@s) 

SELECT d.pdh 单据号, d.operateTime 退货时间, d.dlmc 大类名称, d.ypmc 药品名称, d.ypgg 规格, d.Ph 批号, --CONCAT(d.bmsl,d.ckdw) 数量, 
d.bmsl 数量, d.bzdw 单位, CONCAT(d.jj,'元/',d.bzdw) 进价单价
,CONVERT(NUMERIC(11,2),d.bmsl*d.Ckzhyz/d.bzs*d.jj) 进价总额, d.gysmc 供应商名称,d.ycmc 药厂名称, d.yfbmmc 药房部门名称
FROM (
	SELECT LTRIM(RTRIM(dj.Pdh)) pdh, dj.Cksj operateTime, dl.dlmc, gys.gysmc, yp.ypmc, ypsx.ypgg, RTRIM(LTRIM(mx.Ph)) Ph, mx.Sl bmsl, yp.bzdw
	, yp.bzs, mx.jj, mx.ckdw, mx.Ckzhyz, ckyfbm.yfbmmc
	, dbo.f_changeAuditState(dj.shzt) shzt,yp.ycmc
	, mx.px
	from NewtouchHIS_PDS..xt_yp_crkdj(nolock) dj
	INNER JOIN NewtouchHIS_PDS..xt_yp_crkmx(NOLOCK) mx on mx.crkId = dj.crkId AND mx.zt='1'
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=mx.Ypdm AND yp.OrganizeId=@hospitalCode
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=yp.OrganizeId
	LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_ypgys(nolock) gys on gys.gysCode = dj.Rkbm and gys.OrganizeId = dj.OrganizeId
	LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm(nolock) ckyfbm on ckyfbm.yfbmCode = dj.Ckbm and ckyfbm.OrganizeId = dj.OrganizeId
	LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl(nolock) dl on dl.dlCode = yp.dlCode and dl.OrganizeId = dj.OrganizeId AND dl.zt='1'
	where dj.OrganizeId =@hospitalCode 
	AND (dj.shzt=@shzt OR ''=ISNULL(@shzt,''))
	AND dj.Czsj BETWEEN @kssj AND @jssj
	AND ISNULL(mx.Fph,'') LIKE '%'+ISNULL(@fph,'')+'%'
	AND (dj.Rkbm IN (SELECT * FROM @t) OR ''=ISNULL(@gyss,''))
	AND (dj.crkId =@crkId OR ''=ISNULL(@crkId,''))
	AND dj.Ckbm=@yfbmCode
	AND ISNULL(dj.Pdh,'') LIKE '%'+ISNULL(@pdh,'')+'%'
	AND dj.djlx=2
) d
order by d.px





