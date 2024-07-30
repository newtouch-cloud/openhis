namespace Newtouch.Infrastructure.TSQL
{
    /// <summary>
    /// 药品
    /// </summary>
    public class TSqlDrugs
    {
        /// <summary>
        /// 部门药品授权
        /// </summary>
        public const string bm_yp_xxtb = @"
DECLARE @bmypId VARCHAR(50), @ypmc VARCHAR(256), @result VARCHAR(256);
SET @result='';

SELECT @bmypId=ISNULL(bmypxx.bmypId, ''), @ypmc=yp.ypmc
FROM NewtouchHIS_Base.dbo.V_C_xt_yp yp  
LEFT JOIN dbo.xt_yp_bmypxx(NOLOCK) bmypxx ON bmypxx.Ypdm=yp.ypCode AND bmypxx.OrganizeId=yp.OrganizeId AND bmypxx.yfbmCode=@yfbmCode AND bmypxx.zt='1'
WHERE yp.OrganizeId=@OrganizeId 
AND yp.zt='1'
AND yp.ypId=@ypId

IF @bmypId IS NOT NULL AND @bmypId<>'' 
BEGIN
	IF @operateType=0
	BEGIN
		SET @result= '药品【'+@ypmc+'】已存在';
	END
	ELSE IF @operateType=1
	BEGIN
		DELETE FROM dbo.xt_yp_bmypxx WHERE bmypId=@bmypId;
	END
END
ELSE
BEGIN 
	IF @operateType=0
	BEGIN
		INSERT INTO XT_YP_BMYPXX( bmypId ,OrganizeId ,yfbmCode ,Ypdm ,Ypkw ,Zcxh ,Pxfs1 ,Pxfs2 ,Kcsx ,Kcxx ,Jhd ,Jhl ,Ypsxdm ,zt ,Ylsx ,Sysx,CreatorCode,CreateTime)
		SELECT  NEWID(),
				@OrganizeId ,
				K.yfbmCode ,
				Y.ypCode ,
				'' ,
				'' ,
				'' ,
				'' ,
				'0' ,
				'0' ,
				'0' ,
				'0' ,
				X.yptssx ,
				'1' ,
				'0' ,
				'0',
				'admin',
				GETDATE()
		FROM    [NewtouchHIS_Base].dbo.V_S_xt_yfbm_yp K
				INNER JOIN [NewtouchHIS_Base].dbo.V_S_xt_yp Y ON Y.dlCode = K.dlCode AND Y.OrganizeId = k.OrganizeId
				INNER JOIN [NewtouchHIS_Base].dbo.V_S_xt_ypsx X ON X.ypId = Y.ypId AND X.OrganizeId = k.OrganizeId
				INNER JOIN [newtouchhis_base].dbo.V_S_xt_sfdl sd ON sd.dlCode = Y.dlCode AND sd.OrganizeId=K.OrganizeId AND sd.zt='1'
		WHERE   K.yfbmCode = @yfbmCode
				AND k.OrganizeId = @OrganizeId
				AND Y.ypId=@ypId
		ORDER BY K.yfbmCode 

		IF @@ERROR <> 0
		BEGIN
			SET @result= '新增本部门药品【'+@ypmc+'】失败'
		END
	END
	ELSE IF @operateType=1
	BEGIN
		SET @result= '药品【'+@ypmc+'】不存在该药房，无法删除';
	END
END
    
SELECT @result;
";
    }
}
