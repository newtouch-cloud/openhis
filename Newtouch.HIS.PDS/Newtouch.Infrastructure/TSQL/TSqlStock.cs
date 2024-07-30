namespace Newtouch.Infrastructure.TSQL
{
    /// <summary>
    /// 库存
    /// </summary>
    public class TSqlStock
    {
        /// <summary>
        /// 批量直接出库
        /// </summary>
        public const string direct_delivery_batch_search = @"
SELECT s.ypdm, s.ypmc, s.dlmc, s.gg, s.ycmc, dbo.f_getComplexYpSlandDw(s.kysl, s.ckbmzhyz, s.ckbmbmdw, s.zxdw) slStr
,CONVERT(NUMERIC(11,4),s.zxdwlsj*s.ckbmzhyz) lsj, CONVERT(NUMERIC(11,2),s.zxdwlsj*s.kysl) lsze, s.ckbmbmdw dw
,CONCAT(CONVERT(NUMERIC(11,4),s.zxdwlsj*s.ckbmzhyz),'元/', s.ckbmbmdw) lsjdjdw
FROM (
	SELECT kcxx.ypdm, yp.ypmc, sfdl.dlmc, yp.ypgg gg, yp.ycmc, SUM(kcxx.kcsl-kcxx.djsl) kysl
    ,dbo.f_getyfbmZhyz(@yfbmCode, kcxx.ypdm, kcxx.OrganizeId) ckbmzhyz, dbo.f_getyfbmDw(@yfbmCode, kcxx.ypdm, kcxx.OrganizeId) ckbmbmdw
	,yp.lsj/yp.bzs zxdwlsj, yp.zxdw
	FROM dbo.xt_yp_kcxx(NOLOCK) kcxx
	INNER JOIN NewtouchHIS_Base.dbo.V_C_xt_yp yp ON yp.ypCode=kcxx.ypdm AND yp.OrganizeId=kcxx.OrganizeId AND yp.zt='1'
	INNER JOIN dbo.xt_yp_bmypxx(NOLOCK) bmyp ON bmyp.yfbmCode=kcxx.yfbmCode AND bmyp.Ypdm=kcxx.ypdm AND bmyp.OrganizeId=kcxx.OrganizeId
	INNER JOIN dbo.xt_yp_bmypxx(NOLOCK) rkbmyp ON rkbmyp.yfbmCode=@rkbm AND rkbmyp.Ypdm=kcxx.ypdm AND rkbmyp.OrganizeId=kcxx.OrganizeId 
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode = yp.dlCode AND sfdl.OrganizeId=kcxx.OrganizeId AND sfdl.zt = '1'
	WHERE kcxx.yfbmCode=@yfbmCode
	AND kcxx.zt='1'
	AND kcxx.OrganizeId=@Organizeid
	AND kcxx.kcsl-kcxx.djsl>0
	AND bmyp.zt='1'
	GROUP BY kcxx.ypdm, yp.ypmc, sfdl.dlmc, yp.ypgg, yp.ycmc, kcxx.OrganizeId, yp.lsj, yp.bzs, yp.zxdw
) s
";

        /// <summary>
        /// 直接批量出库
        /// </summary>
        public const string direct_delivery_batch_submit = @"
BEGIN TRANSACTION
BEGIN TRY 
	IF OBJECT_ID(N'tempdb..#djmx', N'U') IS NOT NULL
	DROP TABLE #djmx

	--获取明细
	SELECT NEWID() crkmxId,s.ypdm,s.ph,s.pc,s.yxq,s.cd,s.ypmc,s.ykjj
		,CONVERT(NUMERIC(11,4),s.ykpfj/s.bzs*s.Ckzhyz) pfj,CONVERT(NUMERIC(11,4),s.yklsj/s.bzs*s.Ckzhyz) lsj,s.ykpfj,s.yklsj,CONVERT(NUMERIC(11,2),s.zxdwjj*s.kysl) zje
		,CONVERT(INT,s.kysl/s.Ckzhyz) sl,s.Rkzhyz,s.kcsl Ckbmkc,s.kysl kykcsl,s.Ckzhyz,s.ckdw,CONVERT(NUMERIC(11,4),s.zxdwjj*s.Ckzhyz) jj --出库部门单位进价
	INTO #djmx
	FROM (
		SELECT kcxx.ypdm,kcxx.ph,kcxx.pc,kcxx.yxq,yp.pfj ykpfj,yp.lsj yklsj,yp.ypmc
		,dbo.f_getyfbmZhyz(@yfbmCode,kcxx.ypdm,@Organizeid) Ckzhyz,dbo.f_getyfbmDw(@yfbmCode, kcxx.ypdm, @OrganizeId) ckdw, dbo.f_getyfbmZhyz(@rkbm,kcxx.ypdm,@Organizeid) Rkzhyz
		,kcxx.jj/yp.bzs zxdwjj,kcxx.jj ykjj,kcxx.cd,SUM(kcxx.kcsl-kcxx.djsl) kysl,SUM(kcxx.kcsl) kcsl,yp.bzs
		FROM dbo.xt_yp_kcxx kcxx
		INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=kcxx.ypdm AND yp.OrganizeId=kcxx.OrganizeId
		INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=kcxx.OrganizeId
		INNER JOIN dbo.xt_yp_bmypxx(NOLOCK) bmypxx ON bmypxx.yfbmCode=kcxx.yfbmCode AND bmypxx.Ypdm=kcxx.ypdm AND bmypxx.OrganizeId=kcxx.OrganizeId AND bmypxx.zt='1'
		INNER JOIN dbo.xt_yp_bmypxx(NOLOCK) rkbmypxx ON rkbmypxx.yfbmCode=@rkbm AND rkbmypxx.Ypdm=kcxx.ypdm AND rkbmypxx.OrganizeId=kcxx.OrganizeId AND rkbmypxx.zt='1'
		WHERE kcxx.OrganizeId=@Organizeid
		AND kcxx.zt='1'
		AND kcxx.yfbmCode=@yfbmCode
		AND kcxx.kcsl-kcxx.djsl>0 
		AND kcxx.ypdm IN (SELECT * FROM dbo.f_split(@ypCodes,','))
		GROUP BY kcxx.ypdm,kcxx.ph,kcxx.pc,kcxx.yxq,yp.pfj,yp.lsj,kcxx.jj,kcxx.cd,yp.bzs,yp.ypmc
	) s
	IF NOT EXISTS(SELECT 1 FROM #djmx)
	BEGIN
		SELECT '没有可出库的药品';
		ROLLBACK TRANSACTION
		RETURN;
	END 
	
	--保存出入库单据主信息
	INSERT dbo.xt_yp_crkdj( crkId ,OrganizeId ,djlx ,Pdh ,Rkbm ,Ckbm ,Rksj ,Cksj ,Rkczy ,Ckczy ,Crkfsdm ,Czsj ,Sqsj ,Shczy ,shzt ,zt ,px ,CreatorCode ,CreateTime ,LastModifyTime ,LastModifierCode)
	VALUES  ( @crkId , -- crkId - varchar(50)
			  @Organizeid , -- OrganizeId - varchar(50)
			  @djlx , -- djlx - int
			  @djh , -- Pdh - char(30)
			  @rkbm , -- Rkbm - varchar(100)
			  @yfbmCode , -- Ckbm - varchar(100)
			  GETDATE() , -- Rksj - datetime
			  GETDATE() , -- Cksj - datetime
			  @userCode , -- Rkczy - varchar(10)
			  @userCode , -- Ckczy - varchar(10)
			  @crkfs , -- Crkfsdm - char(10)
			  GETDATE() , -- Czsj - datetime
			  GETDATE() , -- Sqsj - datetime
			  @userCode , -- Shczy - varchar(10)
			  @shzt , -- shzt - char(1)
			  '1' , -- zt - char(1)
			  0 , -- px - int
			  @userCode , -- CreatorCode - varchar(50)
			  GETDATE() , -- CreateTime - datetime
			  GETDATE() , -- LastModifyTime - datetime
			  ''  -- LastModifierCode - varchar(50)
			);
	IF @@ROWCOUNT<=0
	BEGIN 
		SELECT '保存出入库单据失败';
		ROLLBACK TRANSACTION
		RETURN;
	END

	DECLARE @crkmxId VARCHAR(50);
	DECLARE @ypdm VARCHAR(50);
	DECLARE @ph VARCHAR(50);
	DECLARE @pc VARCHAR(50);
	DECLARE @yxq DATETIME;
	DECLARE @cd INT;
	DECLARE @pfj NUMERIC(11,4);
	DECLARE @lsj NUMERIC(11,4);
	DECLARE @jj NUMERIC(11,4);
	DECLARE @ykjj NUMERIC(11,4);
	DECLARE @ykpfj NUMERIC(11,4);
	DECLARE @yklsj NUMERIC(11,4);
	DECLARE @zje NUMERIC(11,2);
	DECLARE @sl INT;
	DECLARE @Ckzhyz INT;
	DECLARE @ckdw VARCHAR(20);
	DECLARE @Rkzhyz INT;
	DECLARE @Ckbmkc INT;
	DECLARE @kykcsl INT;
	DECLARE @ypmc VARCHAR(100);
	DECLARE @tmpkcsl INT;
	WHILE EXISTS(SELECT 1 FROM #djmx)
	BEGIN
		SELECT TOP 1 @crkmxId=crkmxId,@ypdm=ypdm,@ph=ph,@pc=pc,@yxq=yxq,@cd=cd
		,@pfj=pfj,@lsj=lsj,@jj=jj,@ykjj=ykjj,@ykpfj=ykpfj,@yklsj=yklsj,@zje=zje,@sl=sl
		,@Ckzhyz=Ckzhyz,@ckdw=ckdw,@Rkzhyz=Rkzhyz,@Ckbmkc=Ckbmkc,@kykcsl=kykcsl,@ypmc=ypmc FROM #djmx;

		SELECT @tmpkcsl=(kcsl-djsl) 
		FROM dbo.xt_yp_kcxx 
		WHERE ypdm=@ypdm 
		AND ISNULL(LTRIM(RTRIM(pc)),'')=ISNULL(LTRIM(RTRIM(@pc)),'') 
		AND ISNULL(LTRIM(RTRIM(ph)),'')=ISNULL(LTRIM(RTRIM(@ph)),'') 
		AND ISNULL(LTRIM(RTRIM(yxq)),'')=ISNULL(LTRIM(RTRIM(@yxq)),'') 
		AND zt='1' AND (kcsl-djsl)>0 AND yfbmCode=@yfbmCode AND OrganizeId=@Organizeid

		--判断库存是否足够出库
		IF @tmpkcsl<@kykcsl
		BEGIN
			SELECT '【'+@ypmc+'】库存不足';
			ROLLBACK TRANSACTION
			RETURN;
		END 

		UPDATE dbo.xt_yp_kcxx SET kcsl=kcsl-@kykcsl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
		WHERE ypdm=@ypdm 
		AND ISNULL(LTRIM(RTRIM(pc)),'')=ISNULL(LTRIM(RTRIM(@pc)),'') 
		AND ISNULL(LTRIM(RTRIM(ph)),'')=ISNULL(LTRIM(RTRIM(@ph)),'') 
		AND ISNULL(LTRIM(RTRIM(yxq)),'')=ISNULL(LTRIM(RTRIM(@yxq)),'') 
		AND zt='1' AND (kcsl-djsl)>0 AND yfbmCode=@yfbmCode AND OrganizeId=@Organizeid
		IF @@ROWCOUNT<=0
		BEGIN
			SELECT '【'+@ypmc+'】扣库存失败';
			ROLLBACK TRANSACTION
			BREAK;
		END 

		IF EXISTS(SELECT 1 FROM dbo.xt_yp_kcxx 
		WHERE ypdm=@ypdm 
		AND ISNULL(LTRIM(RTRIM(pc)),'')=ISNULL(LTRIM(RTRIM(@pc)),'') 
		AND ISNULL(LTRIM(RTRIM(ph)),'')=ISNULL(LTRIM(RTRIM(@ph)),'') 
		AND ISNULL(LTRIM(RTRIM(yxq)),'')=ISNULL(LTRIM(RTRIM(@yxq)),'') 
		AND zt='1' AND yfbmCode=@rkbm AND OrganizeId=@Organizeid
		)
		BEGIN
			UPDATE dbo.xt_yp_kcxx SET kcsl=kcsl+@kykcsl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
			WHERE ypdm=@ypdm 
			AND ISNULL(LTRIM(RTRIM(pc)),'')=ISNULL(LTRIM(RTRIM(@pc)),'') 
			AND ISNULL(LTRIM(RTRIM(ph)),'')=ISNULL(LTRIM(RTRIM(@ph)),'') 
			AND ISNULL(LTRIM(RTRIM(yxq)),'')=ISNULL(LTRIM(RTRIM(@yxq)),'') 
			AND zt='1' AND yfbmCode=@rkbm AND OrganizeId=@Organizeid
			IF @@ROWCOUNT<=0
			BEGIN
				SELECT '【'+@ypmc+'】入库失败';
				ROLLBACK TRANSACTION
				BREAK;
			END 
		END 
		ELSE
        BEGIN
			INSERT INTO dbo.xt_yp_kcxx( kcId ,OrganizeId ,yfbmCode ,ypdm ,ph ,yxq ,kcsl ,ypkw ,kzbz ,djsl ,tybz ,crkmxId ,jj ,zhyz ,cd ,pc ,zt ,px ,CreatorCode ,CreateTime ,LastModifyTime ,LastModifierCode ,Updating ,locked)
			VALUES  ( NEWID() , -- kcId - varchar(50)
			          @Organizeid , -- OrganizeId - varchar(50)
			          @rkbm , -- yfbmCode - varchar(100)
			          @ypdm , -- ypdm - varchar(20)
			          @ph , -- ph - varchar(30)
			          @yxq , -- yxq - datetime
			          @kykcsl , -- kcsl - int
			          '' , -- ypkw - char(10)
			          0 , -- kzbz - smallint
			          0 , -- djsl - int
			          0 , -- tybz - smallint
			          '' , -- crkmxId - varchar(50)
			          @ykjj, -- jj - decimal
			          @Rkzhyz , -- zhyz - int
			          @cd , -- cd - int
			          @pc , -- pc - varchar(30)
			          '1' , -- zt - char(1)
			          0 , -- px - int
			          @userCode , -- CreatorCode - varchar(50)
			          GETDATE() , -- CreateTime - datetime
			          NULL , -- LastModifyTime - datetime
			          '' , -- LastModifierCode - varchar(50)
			          0 , -- Updating - smallint
			          ''  -- locked - char(1)
			        )
			IF @@ROWCOUNT<=0
			BEGIN
				SELECT '【'+@ypmc+'】入库失败';
				ROLLBACK TRANSACTION
				BREAK;
			END 
		END 
		
		--插入出入库明细
		INSERT INTO dbo.xt_yp_crkmx( crkmxId ,crkId ,Ypdm ,Ph ,Yxq ,Pfj ,Lsj ,Ykpfj ,Yklsj ,Zje ,Sl ,Rkzhyz ,Rkbmkc ,Ckzhyz ,Ckbmkc, ckdw ,jj ,cd ,pc ,zt ,px ,CreatorCode ,CreateTime ,LastModifyTime ,LastModifierCode)
		VALUES  ( @crkmxId , -- crkmxId - varchar(50)
				  @crkId , -- crkId - varchar(50)
				  @ypdm , -- Ypdm - varchar(20)
				  @ph , -- Ph - char(30)
				  @yxq , -- Yxq - datetime
				  @pfj , -- Pfj - decimal
				  @lsj , -- Lsj - decimal
				  @ykpfj , -- Ykpfj - decimal
				  @yklsj , -- Yklsj - decimal
				  @zje , -- Zje - decimal
				  @sl , -- Sl - int
				  @Rkzhyz , -- Rkzhyz - int
				  0 , -- Rkbmkc - int
				  @Ckzhyz , -- Ckzhyz - int
				  @Ckbmkc , -- Ckbmkc - int
				  @ckdw , -- ckdw - varchar(20)
				  @jj , -- jj - decimal
				  @cd , -- cd - int
				  @pc , -- pc - varchar(30)
				  '1' , -- zt - char(1)
				  0 , -- px - int
				  @userCode , -- CreatorCode - varchar(50)
				  GETDATE() , -- CreateTime - datetime
				  NULL , -- LastModifyTime - datetime
				  ''  -- LastModifierCode - varchar(50)
				)
		IF @@ROWCOUNT<=0
		BEGIN
			SELECT '【'+@ypmc+'】保存出入库单据明细失败';
			ROLLBACK TRANSACTION
			BREAK;
		END 

		DELETE FROM #djmx WHERE crkmxId=@crkmxId;
        select @crkId
	END 
END TRY
BEGIN CATCH
    SELECT Error_message() as ErrorMessage  --错误的具体信息
    ROLLBACK TRANSACTION  ---由于出错，这里回滚到开始，第一条语句也没有插入成功。
	RETURN 
END CATCH
SELECT '';
COMMIT TRANSACTION
";

        /// <summary>
        /// 冻结库存（减库存使用  内部发药退回）
        /// </summary>
        public const string frozen_stock_Reduce_use_by_returninward = @"
BEGIN TRANSACTION
BEGIN TRY
	DECLARE @kykc INT;
	DECLARE @ypmc VARCHAR(100);
	SELECT @kykc=SUM(kcsl-djsl),@ypmc=yp.ypmc 
	FROM dbo.xt_yp_kcxx kcxx
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=kcxx.ypdm AND yp.OrganizeId=kcxx.OrganizeId
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=kcxx.OrganizeId
	WHERE kcxx.ypdm=@ypdm AND kcxx.pc=@pc AND kcxx.ph=@ph AND kcxx.OrganizeId=@Organizeid AND kcxx.yfbmCode=@yfbmCode AND kcxx.zt='1'
	GROUP BY yp.ypmc
	IF @kykc<@sl
	BEGIN
		SELECT '【'+@ypmc+'】'+ '库存不足';
		ROLLBACK TRANSACTION
		RETURN;
	END 

	IF NOT EXISTS(SELECT 1 FROM dbo.xt_yp_kcxx WHERE ypdm=@ypdm AND ypdm=@ypdm AND pc=@pc AND ph=@ph AND OrganizeId=@Organizeid AND yfbmCode=@rkbm AND zt='1')
	BEGIN
		SELECT '入库部门不存批次为【'+@pc+'】的药品【'+@ypmc+'】';
		ROLLBACK TRANSACTION;
		RETURN ;
	END

	IF OBJECT_ID(N'tempdb..#tmpkcxx', N'U') IS NOT NULL
		DROP TABLE #tmpkcxx
	
	SELECT kcId, (kcsl-djsl) kykc INTO #tmpkcxx FROM dbo.xt_yp_kcxx WHERE ypdm=@ypdm AND pc=@pc AND ph=@ph AND OrganizeId=@Organizeid AND yfbmCode=@yfbmCode AND zt='1'

	DECLARE @sykc INT; -- 剩余库存
	DECLARE @kcid VARCHAR(50);
	DECLARE @curkykc INT;
	SET @sykc=@sl;
	WHILE @sykc>0 AND EXISTS(SELECT 1 FROM #tmpkcxx)
	BEGIN
		SELECT TOP 1 @kcid=kcId,@curkykc=kykc FROM #tmpkcxx
		IF @curkykc>=@sykc
		BEGIN
			UPDATE dbo.xt_yp_kcxx SET djsl=djsl+@sykc WHERE kcId=@kcid AND pc=@pc AND ph=@ph AND OrganizeId=@Organizeid AND yfbmCode=@yfbmCode AND zt='1'
			SET @sykc=0;
		END 
		ELSE
		BEGIN
			UPDATE dbo.xt_yp_kcxx SET djsl=djsl+@curkykc WHERE kcId=@kcid AND pc=@pc AND ph=@ph AND OrganizeId=@Organizeid AND yfbmCode=@yfbmCode AND zt='1'
			SET @sykc=@sykc-@curkykc;
		END
        
		DELETE FROM #tmpkcxx WHERE kcId=@kcid
	END 
	COMMIT TRANSACTION
	SELECT '';
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE();
	ROLLBACK TRANSACTION
	RETURN
END CATCH
";

        /// <summary>
        /// 冻结库存（减库存使用  直接出库）
        /// </summary>
        public const string frozen_stock_Reduce_use_by_deliveryDirect = @"
BEGIN TRANSACTION
BEGIN TRY
	DECLARE @kykc INT;
	DECLARE @ypmc VARCHAR(100);
	SELECT @kykc=SUM(kcsl-djsl),@ypmc=yp.ypmc 
	FROM dbo.xt_yp_kcxx kcxx
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=kcxx.ypdm AND yp.OrganizeId=kcxx.OrganizeId
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=kcxx.OrganizeId
	WHERE kcxx.ypdm=@ypdm AND kcxx.pc=@pc AND kcxx.ph=@ph AND kcxx.OrganizeId=@Organizeid AND kcxx.yfbmCode=@yfbmCode AND kcxx.zt='1'
	GROUP BY yp.ypmc
	IF @kykc<@sl
	BEGIN
		SELECT '【'+@ypmc+'】'+ '库存不足';
		ROLLBACK TRANSACTION
		RETURN;
	END 

	IF OBJECT_ID(N'tempdb..#tmpkcxx', N'U') IS NOT NULL
		DROP TABLE #tmpkcxx
	
	SELECT kcId, (kcsl-djsl) kykc INTO #tmpkcxx FROM dbo.xt_yp_kcxx WHERE ypdm=@ypdm AND pc=@pc AND ph=@ph AND OrganizeId=@Organizeid AND yfbmCode=@yfbmCode AND zt='1'

	DECLARE @sykc INT; -- 剩余库存
	DECLARE @kcid VARCHAR(50);
	DECLARE @curkykc INT;
	SET @sykc=@sl;
	WHILE @sykc>0 AND EXISTS(SELECT 1 FROM #tmpkcxx)
	BEGIN
		SELECT TOP 1 @kcid=kcId,@curkykc=kykc FROM #tmpkcxx
		IF @curkykc>=@sykc
		BEGIN
			UPDATE dbo.xt_yp_kcxx SET djsl=djsl+@sykc WHERE kcId=@kcid AND pc=@pc AND ph=@ph AND OrganizeId=@Organizeid AND yfbmCode=@yfbmCode AND zt='1'
			SET @sykc=0;
		END 
		ELSE
		BEGIN
			UPDATE dbo.xt_yp_kcxx SET djsl=djsl+@curkykc WHERE kcId=@kcid AND pc=@pc AND ph=@ph AND OrganizeId=@Organizeid AND yfbmCode=@yfbmCode AND zt='1'
			SET @sykc=@sykc-@curkykc;
		END
        
		DELETE FROM #tmpkcxx WHERE kcId=@kcid
	END 
	COMMIT TRANSACTION
	SELECT '';
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE();
	ROLLBACK TRANSACTION
	RETURN
END CATCH
";

        /// <summary>
        /// 生成盘点信息
        /// </summary>
        public const string generate_inventoryInfo = @"
BEGIN TRANSACTION;
BEGIN TRY
	DECLARE @PdId varchar(50);
	DECLARE @pdkssj DATETIME;
	SET @PdId=newId();
	SET @pdkssj=(SELECT TOP 1 pdxx.Jssj jssj 
				FROM dbo.xt_yp_pdxx pdxx 
				WHERE zt='1' AND OrganizeId=@OrganizeId 
				ORDER BY pdxx.Jssj DESC)
	IF @pdkssj IS NULL OR @pdkssj=''
	BEGIN
		SET @pdkssj=GETDATE();
	END 
	INSERT INTO xt_yp_pdxx(PdId,OrganizeId,yfbmCode,Kssj,	   zt,CreatorCode,CreateTime)
				VALUES (@PdId,@OrganizeId,@yfbmCode,@pdkssj,'1',@CreatorCode,GETDATE())

	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmpSourcepdmx') and type='U')
	BEGIN
		DROP TABLE #tmpSourcepdmx;
	END

	SELECT ROW_NUMBER() OVER (ORDER BY a.ypCode) AS Id , ypCode, Ph, pc, Yxq, Llsl, Sjsl, Pfj, Lsj, Ykpfj, Yklsj, Zhyz, KcId 
	INTO #tmpSourcepdmx
	FROM (
		SELECT J.ypCode, C.Ph, C.pc, C.Yxq,J.pfj Ykpfj, J.lsj Yklsj,C.KcId
		,ISNULL(SUM(C.Kcsl),0) Llsl, ISNULL(SUM(C.Kcsl),0) Sjsl,
		(J.pfj/J.bzs) * (CASE Kf.mzzybz WHEN '0' THEN J.bzs WHEN '1' THEN J.mzcls WHEN '2' THEN J.zycls WHEN '3' THEN J.mzcls END) Pfj,
		(J.lsj/J.bzs) * (CASE Kf.mzzybz WHEN '0' THEN J.bzs WHEN '1' THEN J.mzcls WHEN '2' THEN J.zycls WHEN '3' THEN J.mzcls END)  Lsj,		
		(CASE Kf.mzzybz WHEN '0' THEN J.bzs WHEN '1' THEN J.mzcls WHEN '2' THEN J.zycls WHEN '3' THEN J.mzcls END) Zhyz
		FROM xt_yp_bmypxx(NOLOCK) B 
		INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp(NOLOCK) J ON J.ypCode = B.Ypdm AND J.OrganizeId=b.OrganizeId
		INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=j.ypId AND ypsx.OrganizeId=b.OrganizeId
		INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm(NOLOCK) Kf ON Kf.yfbmCode = B.yfbmCode AND kf.yfbmCode=@yfbmCode AND Kf.OrganizeId=B.OrganizeId
		LEFT JOIN xt_yp_kcxx(NOLOCK) C ON C.Ypdm = B.Ypdm AND C.yfbmCode = B.yfbmCode AND C.OrganizeId=B.OrganizeId
		WHERE B.yfbmCode = @yfbmCode 
		AND B.OrganizeId=@OrganizeId 
		GROUP BY J.ypCode,C.Ph,C.pc,C.Yxq,J.pfj,J.lsj,C.Zhyz,Kf.mzzybz,J.mzcls,J.zycls,C.KcId,J.bzs
	) a
		
	IF EXISTS(SELECT 1 FROM #tmpSourcepdmx)
	BEGIN
		IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmpUserfulpdmx') and type='U')
		BEGIN
			DROP TABLE #tmpUserfulpdmx;
		END

		SELECT * INTO #tmpUserfulpdmx FROM #tmpSourcepdmx WHERE KcId IS NOT NULL AND KcId<>'' 
		
		DECLARE @Ids TABLE(
			Id BIGINT
		);

		INSERT INTO @Ids SELECT TOP 200 Id FROM #tmpUserfulpdmx;
		CREATE INDEX tmpUserfulpdmx_index ON #tmpUserfulpdmx(Id);
			
		WHILE EXISTS(SELECT 1 FROM @Ids)
		BEGIN
			INSERT INTO xt_yp_pdxxmx(PdmxId, PdId,	Ypdm,  Ph, pc, Yxq, Llsl, Sjsl, Pfj, Lsj, Ykpfj, Yklsj, Zhyz, zt,  CreatorCode, CreateTime)
							SELECT NEWID(), @PdId, ypCode, ph, pc, yxq, llsl, sjsl, pfj, lsj, ykpfj, yklsj, zhyz, '1', @CreatorCode, getDate() 
							FROM #tmpUserfulpdmx WHERE Id IN (SELECT Id FROM @Ids);
				
			DELETE FROM #tmpUserfulpdmx WHERE Id IN (SELECT Id FROM @Ids);
			DELETE FROM @Ids
			INSERT INTO @Ids SELECT TOP 200 Id FROM #tmpUserfulpdmx;
		END

		IF EXISTS(SELECT 1 FROM #tmpSourcepdmx WHERE KcId IS NULL OR KcId='')
		BEGIN
			IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmpUseless') and type='U')
			BEGIN
				DROP TABLE #tmpUseless;
			END

			SELECT * 
			INTO #tmpUseless 
			FROM #tmpSourcepdmx 
			WHERE KcId IS NULL OR KcId='';
			
			DECLARE @rCount BIGINT,@currentId BIGINT,@newjj NUMERIC(11,4),@newbzs NUMERIC(4,0),@ypCode VARCHAR(20),@zhyz NUMERIC,@kcxh VARCHAR(50), @ph VARCHAR(30),@pc VARCHAR(30),@yxq DATETIME,@llsl INT,@sjsl INT;

			SELECT @rCount=COUNT(0) FROM #tmpUseless;
			WHILE @rCount>0
			BEGIN
				SELECT TOP 1 @currentId=Id, @ypCode=ypCode, @zhyz=Zhyz, @newjj=Ykpfj FROM #tmpUseless;
				
				SET @pc = CONVERT(VARCHAR(8),GETDATE(),112) + REPLACE( CONVERT(VARCHAR(12),GETDATE(),14),':','');
				SET @ph='CSHPDSC';
				SET @yxq=DATEADD(YEAR,1,GETDATE());
				SET @llsl=0;
				SET @sjsl=0;

				INSERT INTO dbo.xt_yp_kcxx 
						( KcId , OrganizeId, yfbmCode,Ypdm,	Ph ,Yxq ,Kcsl,Ypkw, Kzbz,Djsl,Tybz,CrkmxId,jj,zhyz,cd,pc,zt,CreatorCode,CreateTime)
				VALUES ( NEWID(),@OrganizeId,@yfbmCode,@ypCode,@ph ,@yxq ,0 ,'',0 ,	   0,	0 ,	-1 ,@newjj,@zhyz,0,@pc,'1',@OrganizeId,GETDATE());

				INSERT INTO xt_yp_pdxxmx(PdmxId, PdId,	Ypdm,  Ph, pc, Yxq, Llsl, Sjsl, Pfj, Lsj, Ykpfj, Yklsj, Zhyz, zt,  CreatorCode, CreateTime)
								SELECT NEWID(), @PdId, @ypCode,@ph,@pc,@yxq,@llsl,@sjsl, pfj, lsj, ykpfj, yklsj, zhyz, '1', @CreatorCode, getDate() 
							FROM #tmpUseless WHERE Id=@currentId;

				DELETE FROM #tmpUseless WHERE Id=@currentId;
				SELECT @rCount=COUNT(0) FROM #tmpUseless;
			END
		END
	END
	SELECT '' AS Jgxx;
	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE() AS Jgxx;
	ROLLBACK TRANSACTION;
END CATCH
";

        /// <summary>
        /// 结束盘点
        /// </summary>
        public const string end_inventoryInfo = @"
IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmpypjspd') and type='U')
BEGIN
	DROP TABLE #tmpypjspd;
END

SELECT ROW_NUMBER() OVER (ORDER BY M.pdmxId) AS Id, P.yfbmCode, M.Ypdm, ISNULL(M.Ph,'') Ph, Isnull(M.pc,'') pc,Isnull(M.Yxq,'1899-01-01') Yxq, (M.Sjsl - M.Llsl) Sysl, M.Pfj, M.Lsj, M.Ykpfj, M.Yklsj, M.Zhyz
INTO #tmpypjspd
From xt_yp_pdxx P WITH(NOLOCK)
INNER JOIN xt_yp_pdxxmx M WITH(NOLOCK) ON P.pdId=M.pdId AND M.Sjsl-M.Llsl<>0
Where P.pdId = @pdId
AND P.OrganizeId=@OrganizeId

SET XACT_ABORT ON 
BEGIN TRANSACTION;
BEGIN TRY
	IF EXISTS(SELECT 1 FROM #tmpypjspd)
	BEGIN
		CREATE INDEX tmpypjspd_index_id ON #tmpypjspd(Id);
		DECLARE @id BIGINT;
		DECLARE @Kcsl INT;
		DECLARE @CrkmxId VARCHAR(50);
		DECLARE @yfbmCode VARCHAR(50); 
		DECLARE @Ypdm VARCHAR(50);
		DECLARE @Ph VARCHAR(30);
		DECLARE @pc VARCHAR(50);
		DECLARE @Yxq DATETIME;
		DECLARE @Sysl INT;
		DECLARE @djh VARCHAR(50);--输出参数
		DECLARE @Sykc INT;--剩余库存
		DECLARE @Pfj MONEY;--批发价
		DECLARE @Lsj MONEY;--零售价
		DECLARE @Ykpfj MONEY;
		DECLARE @Yklsj MONEY; 
		DECLARE @Zhyz INT;
		DECLARE @Jj MONEY;

		SELECT TOP 1 @id=Id, @yfbmCode=yfbmCode, @Ypdm=Ypdm, @Ph=Ph, @pc=pc, @Yxq=Yxq, @Sysl=Sysl, @Pfj=Pfj, @Lsj=Lsj, @Ykpfj=Ykpfj, @Yklsj=Yklsj, @Zhyz=Zhyz FROM #tmpypjspd;
		WHILE EXISTS(SELECT 1 FROM #tmpypjspd)
		BEGIN
			SELECT @Kcsl = Sum(Isnull(Kcsl,0)) From xt_yp_kcxx WITH(NOLOCK)	Where yfbmCode = @yfbmCode And Ypdm = @Ypdm And Isnull(Ph,'') = @Ph AND Isnull(pc,'') = @pc And Isnull(Yxq,'1899-01-01') = Isnull(@Yxq,'1899-01-01') AND OrganizeId=@OrganizeId
			SELECT @CrkmxId = CrkmxId From xt_yp_kcxx WITH(NOLOCK)	WHERE yfbmCode = @yfbmCode And Ypdm = @Ypdm And Isnull(Ph,'') = @Ph AND Isnull(pc,'') = @pc And Isnull(Yxq,'1899-01-01') = Isnull(@Yxq,'1899-01-01')  AND OrganizeId=@OrganizeId
			SELECT @Jj=(CASE WHEN sum(Kcsl)=0 THEN SUM(jj)/COUNT(1) ELSE ISNULL(sum(Jj*Kcsl)/sum(Kcsl),0) END) From xt_yp_kcxx WITH(NOLOCK) Where yfbmCode = @yfbmCode And Ypdm = @Ypdm  AND OrganizeId=@OrganizeId
			
			If Isnull(@Kcsl,0) + @Sysl < 0
			BEGIN
				--ROLLBACK TRANSACTION
				--SELECT Rtrim(Convert(Varchar(12),@Ypdm)) + '库存不足！' ;
				--RETURN;
				SET @Sysl=-1*@Kcsl;	
			END		
			
			--生成报损报溢单号
			EXEC sp_yp_Getypdjh '报损报溢单', @OrganizeId, @yfbmCode, @djh output
			UPDATE xt_yp_kcxx Set Kcsl = Kcsl + @Sysl, LastModifierCode=@CreatorCode, LastModifyTime=GETDATE() Where yfbmCode = @yfbmCode And Ypdm = @Ypdm And Isnull(Ph,'') = @Ph AND Isnull(pc,'') = @pc And Isnull(Yxq,'1899-01-01') = Isnull(@Yxq,'1899-01-01') AND OrganizeId=@OrganizeId

			SELECT @Sykc = ISNULL(@Kcsl,0) + @Sysl
			IF @Sysl > 0
			BEGIN
				INSERT INTO xt_yp_pdmx(pdId,OrganizeId, yfbmCode,Ypdm,Yxq, Ph, pc,	Bgsj,	Pdsl,		Pfj,  Lsj, Ykpfj, Yklsj, Zhyz,	Pdyy,								Pdkc,	Jj, zt, CreatorCode, CreateTime, zrr, djh)
							VALUES(NEWID(),@OrganizeId,@yfbmCode,@Ypdm,@Yxq,@Ph,@pc,GETDATE(),@Sysl,@Pfj,@Lsj,@Ykpfj,@Yklsj, @Zhyz,'AF753115-B94B-4BAD-AA45-5EE786D0F2DF',@Sykc,@Jj, '1',@CreatorCode,GETDATE(),@CreatorCode,@djh)
			END
			ELSE
			BEGIN
				INSERT INTO xt_yp_pdmx(pdId,OrganizeId, yfbmCode,Ypdm,Yxq, Ph, pc,	Bgsj,	Pdsl,		Pfj,  Lsj, Ykpfj, Yklsj, Zhyz,	Pdyy,								Pdkc,	Jj, zt, CreatorCode, CreateTime, zrr, djh)
							VALUES(NEWID(),@OrganizeId,@yfbmCode,@Ypdm,@Yxq,@Ph,@pc,GETDATE(),@Sysl,@Pfj,@Lsj,@Ykpfj,@Yklsj,@Zhyz,'1B30DBC0-EF98-47D2-9B18-D9A4EFBA9776',@Sykc,	ISNULL(@Jj,0),'1',@CreatorCode,GETDATE(),@CreatorCode,@djh)
			END
			DELETE #tmpypjspd WHERE Id=@id;
			SELECT TOP 1 @id=Id, @yfbmCode=yfbmCode, @Ypdm=Ypdm, @Ph=Ph, @pc=pc, @Yxq=Yxq, @Sysl=Sysl, @Pfj=Pfj, @Lsj=Lsj, @Ykpfj=Ykpfj, @Yklsj=Yklsj, @Zhyz=Zhyz FROM #tmpypjspd;
		END
	END
	
	UPDATE xt_yp_pdxx SET Jssj = GETDATE(), LastModifierCode=@CreatorCode, LastModifyTime=GETDATE()	WHERE pdId = @pdId
	
	COMMIT TRANSACTION
	SELECT '';
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE();
	ROLLBACK TRANSACTION;
	RETURN;
END CATCH
";

        /// <summary>
        /// 作废  减库存
        /// </summary>
        public const string cancel_subtract_kcsl = @"
BEGIN TRY 
	IF @sl<=0
	BEGIN
		SELECT '需作废的库存数量必须大于零';
		RETURN;
	END 
	DECLARE @totalDjsl INT;
	DECLARE @totalKcsl INT;
	SELECT @totalKcsl=ISNULL(SUM(kcsl),0), @totalDjsl=ISNULL(SUM(djsl),0) FROM dbo.xt_yp_kcxx 
	WHERE pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND yfbmCode=@yfbmCode AND ypdm=@ypdm AND zt=@zt
	IF @totalDjsl>0  
	BEGIN
		-- 有冻结说明正在被使用，作废可能导致后期扣库存库存不足
		SELECT '该批次存在冻结库存，不能作废';
		RETURN;
	END
	IF @totalKcsl<@sl
	BEGIN
		SELECT '库存不足，不能作废';
		RETURN;
	END

	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#allkcxx') and type='U')
	BEGIN
		DROP TABLE #allkcxx;
	END
	SELECT kcId, kcsl INTO #allkcxx FROM dbo.xt_yp_kcxx 
	WHERE ypdm=@ypdm AND yfbmCode=@yfbmCode AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt=@zt

	DECLARE @kcsl INT; --当前可用库存
	DECLARE @kcId VARCHAR(50); --当前库存ID
	DECLARE @sykc INT; --剩余库存
	SET @sykc=@sl;
	WHILE(EXISTS(SELECT 1 FROM #allkcxx) AND @sykc>0)
	BEGIN
		SELECT TOP 1 @kcId=kcId,@kcsl=kcsl FROM #allkcxx
		IF @kcsl>=@sykc
		BEGIN
			UPDATE dbo.xt_yp_kcxx SET kcsl=kcsl-@sykc, LastModifyTime=GETDATE(),LastModifierCode=@userCode
			WHERE kcId=@kcId AND ypdm=@ypdm AND yfbmCode=@yfbmCode AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt=@zt
			SET @sykc=0;
		END 
		ELSE 
		BEGIN
			UPDATE dbo.xt_yp_kcxx SET kcsl=0,LastModifyTime=GETDATE(),LastModifierCode=@userCode
			WHERE kcId=@kcId AND ypdm=@ypdm AND yfbmCode=@yfbmCode AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt=@zt
			SET @sykc=@sykc-@kcsl;
		END 
		DELETE FROM #allkcxx WHERE kcId=@kcId;
	END 
	SELECT '';
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE();
	RETURN
END CATCH
";

        /// <summary>
        /// 冻结库存并返回冻结信息
        /// </summary>
        public const string frozen_stork_and_return_frozenInfo = @"
BEGIN TRANSACTION
BEGIN TRY
	IF OBJECT_ID(N'tempdb..#returnData', N'U') IS NOT NULL	
	DROP TABLE #returnData

	--返回表类型
	CREATE TABLE #returnData
	(
		ypdm VARCHAR(50) ,
		pc VARCHAR(50) ,
		ph VARCHAR(50) ,
		jj DECIMAL(11,4),
		djsl INT,
		yxq DATETIME NULL,
		errorMsg VARCHAR(200)
	)
	IF @djsl<0
	BEGIN
		INSERT INTO #returnData( ypdm, pc, ph, jj, djsl, yxq, errorMsg )
		VALUES  ( '', -- ypdm - varchar(50)
		          '', -- pc - varchar(50)
		          '', -- ph - varchar(50)
				  0, --jj -DECIMAL(11,4) 药库单位进价
		          0, -- djsl - int
				  GETDATE(),
		          '目标数量必须大于等于零'  -- errorMsg - varchar(200)
		          );
		SELECT * FROM #returnData;
		RETURN;
	END 
	
	DECLARE @kykc INT;
	DECLARE @ypmc VARCHAR(100);
	SELECT @kykc=SUM(kcxx.kcsl-kcxx.djsl),@ypmc=yp.ypmc
	FROM dbo.xt_yp_kcxx kcxx
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=kcxx.ypdm AND yp.OrganizeId=kcxx.OrganizeId 
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=kcxx.OrganizeId
	WHERE kcxx.ypdm=@ypdm AND kcxx.OrganizeId=@OrganizeId AND kcxx.yfbmCode=@yfbmCode AND kcxx.zt='1' AND kcxx.yxq>GETDATE() AND (kcxx.kcsl-kcxx.djsl)>0 
	GROUP BY yp.ypmc
	IF @kykc<@djsl
	BEGIN
		INSERT INTO #returnData( ypdm, pc, ph, jj, djsl, yxq, errorMsg )
		VALUES  ( '', -- ypdm - varchar(50)
		          '', -- pc - varchar(50)
		          '', -- ph - varchar(50)
				  0, --jj -DECIMAL(11,4) 药库单位进价
		          0, -- djsl - int
				  GETDATE(),
		          '【'+@ypmc+'】'+'库存不足'  -- errorMsg - varchar(200)
		          );
		SELECT * FROM #returnData;
		RETURN;
	END 
	--PRINT CONCAT('kykc-',@kykc,' ypmc-',@ypmc,' djsl-',@djsl);
	
	IF OBJECT_ID(N'tempdb..#allBatches', N'U') IS NOT NULL
	DROP TABLE #allBatches

	SELECT kcId,ph,pc,jj,kcsl,djsl,yxq 
	INTO #allBatches 
	FROM dbo.xt_yp_kcxx(ROWLOCK) 
	WHERE ypdm=@ypdm AND yfbmCode=@yfbmCode AND OrganizeId=@OrganizeId AND zt='1' AND yxq>GETDATE() AND (kcsl-djsl)>0 
	--SELECT * FROM #allBatches ORDER BY yxq DESC

	DECLARE @tkcid VARCHAR(50),@tph VARCHAR(50),@tpc VARCHAR(50),@tjj DECIMAL(11,4),@tkcsl INT,@tdjsl INT,@tkykc INT,@tyxq DATETIME,@sysl INT;
	SET @sysl=@djsl;
    WHILE EXISTS(SELECT 1 FROM #allBatches) AND @sysl>0
	BEGIN
		SELECT TOP 1 @tkcid=kcId,@tph=ph,@tpc=pc,@tjj=jj,@tkcsl=kcsl,@tdjsl=djsl,@tkykc=(kcsl-djsl),@tyxq=yxq FROM #allBatches ORDER BY yxq ASC 
        IF @tkykc>@sysl
		BEGIN
			UPDATE dbo.xt_yp_kcxx SET djsl=djsl+@sysl,LastModifyTime=GETDATE(),LastModifierCode=@userCode WHERE kcId=@tkcid 
			INSERT INTO #returnData( ypdm, pc, ph, djsl, jj, yxq, errorMsg )
			SELECT @ypdm,@tpc,@tph,@sysl,@tjj,@tyxq,''
			SET @sysl=0;
		END 
		ELSE
        BEGIN
			UPDATE dbo.xt_yp_kcxx SET djsl=djsl+@tkykc,LastModifyTime=GETDATE(),LastModifierCode=@userCode WHERE kcId=@tkcid
			INSERT INTO #returnData( ypdm, pc, ph, djsl,jj,yxq, errorMsg )
			SELECT @ypdm,@tpc,@tph,@tkykc,@tjj,@tyxq,''
			SET @sysl=@sysl-@tkykc;
		END
		DELETE FROM #allBatches WHERE kcId=@tkcid 
	END
COMMIT TRANSACTION
SELECT * FROM #returnData;
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
	DELETE FROM #returnData;
	INSERT INTO #returnData( ypdm, pc, ph, jj, djsl, yxq, errorMsg )
	VALUES  ( '', -- ypdm - varchar(50)
			  '', -- pc - varchar(50)
			  '', -- ph - varchar(50)
			  0, --jj -DECIMAL(11,4) 药库单位进价
			  0, -- djsl - int
			  GETDATE(),
			  ERROR_MESSAGE()  -- errorMsg - varchar(200)
			);
	SELECT * FROM #returnData;
END CATCH
";

        /// <summary>
        /// 库存结转
        /// </summary>
        public const string yp_xt_kcjz_v2 = @"
BEGIN TRY
	BEGIN TRANSACTION
	IF OBJECT_ID(N'tempdb.dbo.#NeedCarryDownMedicine', N'U') IS NOT NULL
	BEGIN
		DROP TABLE #NeedCarryDownMedicine;
	END
	
	SELECT NEWID() Id,
		kcxx.ypdm ,
		kcxx.ph ,
		kcxx.pc ,
		kcxx.yxq ,
		kcxx.kcsl ,
		yp.pfj ykpfj ,
		yp.lsj yklsj , 
		kcxx.jj ,
		kcxx.zhyz 
	INTO #NeedCarryDownMedicine
	FROM dbo.xt_yp_kcxx(NOLOCK) kcxx 
	INNER JOIN newtouchhis_base.dbo.V_S_xt_yp yp on kcxx.ypdm = yp.ypCode AND yp.OrganizeId=kcxx.OrganizeId
	INNER JOIN newtouchhis_base.dbo.V_S_xt_ypsx ypsx on yp.ypId = ypsx.ypId AND ypsx.OrganizeId=kcxx.OrganizeId
	INNER JOIN xt_yp_bmypxx(NOLOCK) bmyp on bmyp.ypdm = kcxx.ypdm and bmyp.yfbmCode = kcxx.yfbmCode and bmyp.OrganizeId=kcxx.OrganizeId
	where kcxx.yfbmCode=@YfbmCode AND kcxx.OrganizeId=@OrganizeId
	
	WHILE EXISTS(SELECT 1 FROM #NeedCarryDownMedicine)
	BEGIN
		IF OBJECT_ID(N'tempdb..#tmpData', N'U') IS NOT NULL
		BEGIN
			DROP TABLE #tmpData;
		END
		SELECT TOP 50 * INTO #tmpData FROM #NeedCarryDownMedicine;
		INSERT INTO dbo.xt_yp_kcjzk( OrganizeId ,yfbmCode ,Ypdm ,Ph ,Yxq ,Kcsl ,Ykpfj ,Yklsj ,Jj ,Zhyz ,Jzsj ,pc ,zt ,px ,CreatorCode ,CreateTime ,LastModifyTime ,LastModifierCode)
							SELECT @OrganizeId, @yfbmCode, ypdm, ph, yxq, Kcsl, ykpfj, yklsj, ISNULL(jj,0), zhyz, @jzsj,pc, '1', 0, @CreatorCode, GETDATE(), NULL, '' FROM #tmpData

		DELETE FROM #NeedCarryDownMedicine WHERE Id IN (SELECT Id FROM #tmpData);
	END

	COMMIT TRANSACTION
	SELECT '';
	RETURN;
END TRY 
BEGIN CATCH
	SELECT	ERROR_MESSAGE();
	ROLLBACK TRANSACTION;
	RETURN;
END CATCH 
";

        /// <summary>
        /// 库存结转
        /// </summary>
        public const string yp_xt_kcjz = @"
BEGIN TRY
	BEGIN TRANSACTION
	PRINT '1.1'
	IF OBJECT_ID(N'tempdb.dbo.#NeedCarryDownMedicine', N'U') IS NOT NULL
	BEGIN
		DROP TABLE #NeedCarryDownMedicine;
	END
	CREATE TABLE #NeedCarryDownMedicine
	(
		Id VARCHAR(50) NOT NULL,
		ypdm VARCHAR(50) NULL,
		ph VARCHAR(30) NULL,
		pc VARCHAR(30) NULL,
		yxq DATETIME NULL,
		Kcsl INT NULL,
		ykpfj DECIMAL(11,4) NULL,
		yklsj DECIMAL(11,4) NULL,
		jj DECIMAL(11,4) NULL,
		zhyz INT NULL
	);
		
	INSERT INTO #NeedCarryDownMedicine( Id ,ypdm ,ph ,pc ,yxq ,Kcsl ,ykpfj ,yklsj ,jj ,zhyz)
							SELECT NEWID(), ypdm, ph, pc, yxq, Kcsl, ykpfj, yklsj, jj, zhyz FROM @jzyp;


	WHILE EXISTS(SELECT 1 FROM #NeedCarryDownMedicine)
	BEGIN
		IF OBJECT_ID(N'tempdb..#tmpData', N'U') IS NOT NULL
		BEGIN
			DROP TABLE #tmpData;
		END
		SELECT TOP 50 * INTO #tmpData FROM #NeedCarryDownMedicine;
		INSERT INTO dbo.xt_yp_kcjzk( OrganizeId ,yfbmCode ,Ypdm ,Ph ,Yxq ,Kcsl ,Ykpfj ,Yklsj ,Jj ,Zhyz ,Jzsj ,pc ,zt ,px ,CreatorCode ,CreateTime ,LastModifyTime ,LastModifierCode)
							SELECT @OrganizeId, @yfbmCode, ypdm, ph, yxq, Kcsl, ykpfj, yklsj, jj, zhyz, @jzsj,pc, '1', 0, @CreatorCode, GETDATE(), NULL, '' FROM #tmpData

		DELETE FROM #NeedCarryDownMedicine WHERE Id IN (SELECT Id FROM #tmpData);
	END

	COMMIT TRANSACTION
	SELECT '';
	RETURN;
END TRY 
BEGIN CATCH
	SELECT	ERROR_MESSAGE();
	ROLLBACK TRANSACTION;
	RETURN;
END CATCH 
";

        /// <summary>
        /// 扣库存
        /// </summary>
        public const string SubtractStock = @"
DECLARE @kykc INT, @ypmc VARCHAR(200);
SELECT @kykc= SUM(kcsl-djsl), @ypmc=yp.ypmc
FROM dbo.xt_yp_kcxx kcxx 
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=kcxx.ypdm AND yp.OrganizeId=kcxx.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=kcxx.OrganizeId 
WHERE kcxx.ypdm=@ypdm AND kcxx.ph=@ph AND kcxx.pc=@pc AND kcxx.kcsl>0 AND kcxx.yfbmCode=@yfbmCode AND kcxx.OrganizeId=@Organizeid AND kcxx.zt='1'
GROUP BY yp.ypmc
PRINT @kykc

IF @sl<=0
BEGIN
	SELECT CONCAT('【',@ypmc,'】数量必须为自然数');
	RETURN;
END
IF(@kykc<@sl)
BEGIN
	SELECT CONCAT('【',@ypmc,'】库存不足');
	RETURN;
END

IF OBJECT_ID(N'tempdb..#kcmx', N'U') IS NOT NULL
DROP TABLE #kcmx

SELECT kcId, kcsl 
INTO #kcmx 
FROM dbo.xt_yp_kcxx kcxx 
WHERE kcxx.ypdm=@ypdm AND kcxx.ph=@ph AND kcxx.pc=@pc AND kcxx.kcsl>0 AND kcxx.yfbmCode=@yfbmCode AND kcxx.OrganizeId=@Organizeid AND kcxx.zt='1'
DECLARE @kcId VARCHAR(50);
DECLARE @sysl INT,@curKcsl INT;
SET @sysl=@sl;

BEGIN TRY
	BEGIN TRANSACTION
	WHILE EXISTS(SELECT 1 FROM #kcmx) AND @sysl>0
	BEGIN
		SELECT TOP 1 @kcId=kcId, @curKcsl=kcsl FROM #kcmx
		IF(@curKcsl>=@sysl)
		BEGIN
			UPDATE dbo.xt_yp_kcxx SET kcsl=kcsl-@sysl WHERE kcId=@kcId
			SET @sysl=0;
		END
		ELSE
        BEGIN
			UPDATE dbo.xt_yp_kcxx SET kcsl=0 WHERE kcId=@kcId;
			SET @sysl=@sysl-@curKcsl;
		END 
		DELETE FROM #kcmx WHERE kcId=@kcId
	END
	COMMIT TRANSACTION
	SELECT '';
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
	SELECT ERROR_MESSAGE();
	RETURN;
END CATCH
";

        /// <summary>
        /// 科室备药发药
        /// </summary>
        public const string KsbyStock = @"IF EXISTS(SELECT 1 FROM tempdb..sysobjects WHERE id=OBJECT_ID(N'tempdb..#ypkc') AND type='U')
BEGIN
	DROP TABLE #ypkc;
END
DECLARE @resultMsg VARCHAR(500);
SET @resultMsg=''
select kcxx.kcId,ksby.Id byId,ksby.OrganizeId,ksby.djh,ksby.ksbm,ksby.bqbm,bymx.ypdm,kcxx.pc,kcxx.ph,
yp.ypmc,yp.ypgg,kcxx.yfbmCode,(kcxx.kcsl-kcxx.djsl) kykc ,
kcxx.zhyz,yp.zxdw,yp.bzdw,
bymx.sl,bymx.pfj,bymx.lsj,kcxx.jj,kcxx.yxq,yp.ycmc
into #ypkc
from [xt_bqksby] (nolock) ksby
join [xt_bqksbymx]  (nolock) bymx on ksby.Id=bymx.byId and ksby.OrganizeId=bymx.OrganizeId and bymx.zt=1
join NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=bymx.Ypdm AND yp.OrganizeId=bymx.OrganizeId 
join dbo.xt_yp_kcxx(NOLOCK) kcxx ON kcxx.ypdm=bymx.ypdm  AND kcxx.OrganizeId=bymx.OrganizeId AND kcxx.zt='1'
and bymx.pc=kcxx.pc and bymx.ph=kcxx.ph and kcxx.yfbmCode=@yfbmCode
where ksby.zt=1  and ksby.OrganizeId=@orgId and shzt=1
 and yfbm=@yfbmCode and djh in ( select col from f_split(@sqdArr,','))

 IF EXISTS(SELECT 1 FROM #ypkc where kykc<sl)
	BEGIN
	    select top 1 @resultMsg=djh+' 药品代码:'+ypdm from #ypkc where kykc<sl
		SET @resultMsg=CONCAT('【单据号:',@resultMsg,'】的库存不足')
        select @resultMsg;
		RETURN;
	END 
BEGIN TRY
 BEGIN TRANSACTION
DECLARE @djh VARCHAR(50); 
DECLARE @kcId VARCHAR(50); 
DECLARE @byId VARCHAR(50); 
DECLARE @sqsl Decimal(12,2); 
DECLARE @ypdm VARCHAR(20); 
DECLARE @pc VARCHAR(50); 
DECLARE @ph VARCHAR(50);
DECLARE @ksbm VARCHAR(50);  
DECLARE @bqbm VARCHAR(50); 
WHILE EXISTS(SELECT 1 FROM #ypkc)
BEGIN
SELECT top 1 @djh=djh,@sqsl=sl,@kcId=kcId,@byId=byId,@ypdm=ypdm,@pc=pc,@ph=ph,@ksbm=ksbm,@bqbm=bqbm FROM #ypkc
if EXISTS(select * from Newtouch_CIS.[dbo].[xt_ksby_kcxx] 
where ypdm=@ypdm and ph=@ph and pc=@pc and yfbmCode=@yfbmCode and ksbm=@ksbm and bqbm=@bqbm and zt=1)
begin
	update Newtouch_CIS.[dbo].[xt_ksby_kcxx] set kcsl=kcsl+@sqsl,lastmodifiercode=@userCode ,lastmodifytime=getdate()
	where OrganizeId=@orgId and ypdm=@ypdm and ph=@ph and pc=@pc and yfbmCode=@yfbmCode and ksbm=@ksbm and bqbm=@bqbm and zt=1 
end
else
begin
insert into Newtouch_CIS.[dbo].[xt_ksby_kcxx](kcId,OrganizeId,yfbmCode,ypdm,ph,yxq,kcsl,djsl,tybz,jj,zhyz,cd,pc,px,creatorCode,CreateTime,zt,ksbm,bqbm)
select NEWID(),
	   OrganizeId,
	   yfbmCode,
	   ypdm,
	   ph,
	   yxq,
	   sl,
	   0,
	   0,
	   jj,
	   zhyz,
	   NULL,
	   pc,
	   0,
	   @userCode,
	   GETDATE(),
	   1,
	   ksbm,
	   bqbm
 from #ypkc WHERE djh=@djh
end

 update [xt_yp_kcxx] set kcsl=kcsl-@sqsl,lastmodifiercode=@userCode ,lastmodifytime=getdate() where kcId=@kcId and OrganizeId=@orgId

 update [xt_bqksby] set shzt=3,lastmodifiercode=@userCode ,lastmodifytime=getdate()  where Id=@byId and OrganizeId=@orgId
 update Newtouch_CIS.[dbo].zy_bqksby set shzt=3,lastmodifiercode=@userCode ,lastmodifytime=getdate() where djh=@djh and OrganizeId=@orgId

 IF @@ERROR <> 0
	BEGIN
		SET @resultMsg= CONCAT('【单据号:',@resultMsg,'】插入科室备药库存表失败') 
		BREAK;
	END
DELETE FROM #ypkc WHERE djh=@djh
END

 select @resultMsg;
IF(@resultMsg='')
BEGIN
	COMMIT TRANSACTION;
END
ELSE
BEGIN
	ROLLBACK TRANSACTION;
END 
END TRY
BEGIN CATCH
	SET @resultMsg=ERROR_MESSAGE();
	ROLLBACK TRANSACTION;
END CATCH
 select @resultMsg resultMsg";

        public const string KsbyTslStock = @"
DECLARE @resultMsg VARCHAR(500);
SET @resultMsg=''
DECLARE @djh VARCHAR(50); 
DECLARE @kcId VARCHAR(50); 
DECLARE @byId VARCHAR(50); 
DECLARE @tsl Decimal(12,2); 
DECLARE @ypdm VARCHAR(20); 
DECLARE @pc VARCHAR(50); 
DECLARE @ph VARCHAR(50);
DECLARE @ksbm VARCHAR(50);  
DECLARE @bqbm VARCHAR(50); 

select @djh=djh,@ph=ph,@pc=pc,@ypdm=ypdm,@tsl=tsl from [xt_ksbyth] (nolock) ksby
join [xt_ksbythmx]  (nolock) bymx on ksby.Id=bymx.byId and ksby.OrganizeId=bymx.OrganizeId and bymx.zt=1
where ksby.zt=1  and ksby.OrganizeId=@orgId
 and yfbm=@yfbmCode and djh =@sqd
if(@djh='')
begin
	SET @resultMsg=CONCAT('【单据号:',@djh,'】不存在!')
	SELECT @resultMsg;
	RETURN;
end

if NOT EXISTS(select 1 from Newtouch_CIS.[dbo].[zy_ksbyth] (nolock) where OrganizeId=@orgId
and djh =@sqd and thzt=2 and zt=1)
begin
	SET @resultMsg=CONCAT('【医生站单据号:',@djh,'】不为已申请状态或单据号不存在,不能退药!')
	SELECT @resultMsg;
	RETURN;
end

if EXISTS(select 1 from Newtouch_CIS.[dbo].[xt_ksby_kcxx] (nolock) where OrganizeId=@orgId
 and yfbmCode=@yfbmCode and ypdm=@ypdm and pc=@pc and ph=@ph and zt=1 and (kcsl-djsl)<@tsl)
 begin
 SET @resultMsg=CONCAT('药品：',@ypdm,'在科室备药库存信息中有效库存数量小于退数量,不能退药!')
	SELECT @resultMsg;
	RETURN;
 end

select top 1 @kcId=kcId from xt_yp_kcxx(NOLOCK) kcxx  
 where OrganizeId=@orgId and yfbmCode=@yfbmCode and ypdm=@ypdm and pc=@pc and ph=@ph and zt=1
 if(@kcId='')
 begin
    SET @resultMsg=CONCAT('【药房库存不存在药品代码:',@ypdm,'】的库存信息!')
	SELECT @resultMsg;
	RETURN;
 end

 update xt_yp_kcxx set kcsl=kcsl+@tsl,lastmodifiercode=@userCode ,lastmodifytime=getdate()
 where OrganizeId=@orgId and ypdm=@ypdm and ph=@ph and pc=@pc and yfbmCode=@yfbmCode and zt=1 

 update [xt_ksbyth] set thzt=3,lastmodifiercode=@userCode ,lastmodifytime=getdate()  where djh=@djh and OrganizeId=@orgId
 update Newtouch_CIS.[dbo].zy_bqksby set shzt=3,lastmodifiercode=@userCode ,lastmodifytime=getdate() where djh=@djh and OrganizeId=@orgId

 select @resultMsg;
";
    }
}
