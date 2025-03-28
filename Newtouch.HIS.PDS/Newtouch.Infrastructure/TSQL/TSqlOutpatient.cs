namespace Newtouch.Infrastructure.TSQL
{
    /// <summary>
    /// T-Sql 门诊
    /// </summary>
    public class TSqlOutpatient
    {
        #region 发药

        /// <summary>
        /// 发药
        /// </summary>
        public const string sp_yp_commit_mz = @"
DECLARE @result VARCHAR(100);
SET @result='';
IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#items') and type='U')
BEGIN
	DROP TABLE #items;
END

SELECT NEWID() Id, cf.Id cfId, cfmx.Id cfmxId, mxph.cfmxphId, mxph.pc, mxph.ph, mxph.sl, cfmx.ypCode
INTO #items
FROM dbo.mz_cfmx(NOLOCK) cfmx
INNER JOIN dbo.mz_cfmxph(NOLOCK) mxph ON cfmx.cfh=mxph.cfh AND cfmx.ypCode=mxph.yp AND cfmx.OrganizeId=mxph.OrganizeId AND mxph.zt='1' AND mxph.sl>0 AND mxph.gjzt='0'
INNER JOIN dbo.mz_cf(NOLOCK) cf ON cf.cfh=cfmx.cfh AND cf.OrganizeId=cfmx.OrganizeId AND cf.lyyf=@Yfbm
INNER JOIN NewtouchHIS_Base.dbo.xt_yp(NOLOCK) yp ON yp.ypCode=cfmx.ypCode AND yp.OrganizeId=cfmx.OrganizeId AND yp.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=yp.OrganizeId AND ypsx.zt='1'
WHERE cfmx.cfh=@Cfh AND cfmx.OrganizeId=@OrganizeId AND cf.fybz='1' and cf.zt = '1' AND cf.jsnm>0 AND cf.sfsj<=GETDATE() ;

IF(EXISTS(SELECT 1 FROM #items))
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
		DECLARE @cfId BIGINT, @cfmxId BIGINT;
		DECLARE @cfmxphId VARCHAR(50), @Id VARCHAR(50), @pc VARCHAR(30), @ph VARCHAR(30), @ypCode VARCHAR(50);
		DECLARE @sl NUMERIC(6,2);
		WHILE(EXISTS(SELECT 1 FROM #items))
		BEGIN
			SELECT TOP 1 @Id=Id, @cfId=cfId, @cfmxId=cfmxId, @cfmxphId=cfmxphId, @pc=pc, @ph=ph, @sl=sl, @ypCode=ypCode FROM #items;
			UPDATE	dbo.xt_yp_kcxx
			SET		kcsl=kcsl-@sl,
					djsl=djsl-@sl,
					LastModifyTime=GETDATE(),
					LastModifierCode=@CreatorCode
			WHERE	pc=@pc
					AND ph=@ph
					AND ypdm=@ypCode
					AND yfbmCode=@Yfbm
					AND OrganizeId=@OrganizeId


			INSERT INTO dbo.mz_cfypczjl( mzcfmxId ,operateType ,ypCode ,cfh ,sl ,bz ,CreateTime ,CreatorCode ,LastModifyTime ,LastModifierCode)
			VALUES  ( @cfmxId , -- mzcfmxId - bigint
			          '1' , -- operateType - char(1)
			          @ypCode , -- ypCode - varchar(20)
			          @Cfh , -- cfh - varchar(50)
			          @sl , -- sl - int
			          '' , -- bz - varchar(200)
			          GETDATE() , -- CreateTime - datetime
			          @CreatorCode , -- CreatorCode - varchar(50)
			          NULL , -- LastModifyTime - datetime
			          ''  -- LastModifierCode - varchar(50)
			        )

			DELETE FROM #items WHERE Id=@Id;
		END
			
		UPDATE	dbo.mz_cf
		SET		fybz='2',
				LastModiFierCode=@CreatorCode,
				LastModifyTime=GETDATE()
		WHERE	Id=@cfId

		COMMIT TRANSACTION
	END TRY 
	BEGIN CATCH 
		SET	@result= ERROR_MESSAGE();
		ROLLBACK TRANSACTION
	END CATCH 
END
ELSE
BEGIN 
	SET	@result= '未查询到符合发药的处方';
END
SELECT @result;
";

        #endregion

        #region 门诊发药V2.0

        /// <summary>
        /// 门诊发药V2.0
        /// </summary>
        public const string mz_fy = @"
DECLARE @result VARCHAR(100);
SET @result='';
IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#items') and type='U')
BEGIN
	DROP TABLE #items;
END

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

SELECT NEWID() Id, cf.Id cfId, cfmx.Id cfmxId, mxph.cfmxphId, mxph.pc, mxph.ph, mxph.sl, cfmx.ypCode
INTO #items
FROM dbo.mz_cfmxph(NOLOCK) mxph
INNER JOIN dbo.mz_cfmx(NOLOCK) cfmx ON cfmx.cfh=mxph.cfh AND cfmx.ypCode=mxph.yp AND cfmx.OrganizeId=mxph.OrganizeId AND cfmx.zt='1' 
INNER JOIN dbo.mz_cf(NOLOCK) cf ON cf.cfh=mxph.cfh AND cf.jsnm>0 AND cf.fybz='1' AND cf.OrganizeId=mxph.OrganizeId AND cf.lyyf=mxph.fyyf AND cf.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp(NOLOCK) yp ON yp.ypCode=cfmx.ypCode AND yp.OrganizeId=cfmx.OrganizeId AND yp.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=yp.OrganizeId AND ypsx.zt='1'
WHERE mxph.cfh=@Cfh AND mxph.OrganizeId=@OrganizeId AND mxph.gjzt='0' AND mxph.zt='1' AND mxph.fyyf=@Yfbm AND mxph.yp=@ypCode
	
IF(EXISTS(SELECT 1 FROM #items))
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

		DECLARE @cfId BIGINT, @cfmxId BIGINT;
		DECLARE @cfmxphId VARCHAR(50), @Id VARCHAR(50), @pc VARCHAR(30), @ph VARCHAR(30);
		DECLARE @sl NUMERIC(6,2), @kcId VARCHAR(50);
				
		WHILE(EXISTS(SELECT 1 FROM #items))
		BEGIN
			SELECT TOP 1 @Id=Id, @cfId=cfId, @cfmxId=cfmxId, @cfmxphId=cfmxphId, @pc=pc, @ph=ph, @sl=sl FROM #items;
						
			IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmpkcxx') and type='U')
			BEGIN
				DROP TABLE #tmpkcxx;
			END

			SELECT kcId, kcsl, djsl 
			INTO #tmpkcxx FROM dbo.xt_yp_kcxx WITH (XLOCK, ROWLOCK) 
			WHERE ypdm=@ypCode AND ph=@ph AND pc=@pc AND zt='1' AND yfbmCode=@Yfbm AND OrganizeId=@OrganizeId
			
			DECLARE @curKcxxDjsl INT;
			DECLARE @curKcxxKcId VARCHAR(50);
			DECLARE @sumOfDjsl INT;
			SELECT @sumOfDjsl=SUM(djsl) FROM #tmpkcxx;
			IF @sumOfDjsl<=@sl
			BEGIN
				ROLLBACK TRANSACTION;
				SET	@result= '冻结数量不足';
				RETURN;
			END 
			DECLARE @sysl INT;
			SET @sysl=@sl;

			WHILE EXISTS(SELECT 1 FROM #tmpkcxx) AND @sl>0
			BEGIN
				SELECT TOP 1 @curKcxxKcId=kcId, @curKcxxDjsl=djsl FROM #tmpkcxx
				IF @curKcxxDjsl>=@sysl
				BEGIN
					UPDATE dbo.xt_yp_kcxx SET djsl-=@sysl, kcsl-=@sysl, LastModifyTime=GETDATE(), LastModifierCode=@userCode WHERE kcId=@kcId AND zt='1'
					SET @sysl=0;
				END
                ELSE
                BEGIN
					UPDATE dbo.xt_yp_kcxx SET djsl=0, kcsl-=@curKcxxDjsl, LastModifyTime=GETDATE(), LastModifierCode=@userCode WHERE kcId=@kcId AND zt='1'
					SET @sysl-=@curKcxxDjsl;
				END 
				DELETE FROM #tmpkcxx WHERE kcId=@curKcxxKcId;
			END 
			INSERT INTO dbo.mz_cfypczjl( mzcfmxId ,operateType ,ypCode ,cfh ,sl ,bz ,CreateTime ,CreatorCode ,LastModifyTime ,LastModifierCode)
			VALUES  ( @cfmxId , -- mzcfmxId - bigint
			          '1' , -- operateType - char(1)
			          @ypCode , -- ypCode - varchar(20)
			          @Cfh , -- cfh - varchar(50)
			          @sl , -- sl - int
			          '' , -- bz - varchar(200)
			          GETDATE() , -- CreateTime - datetime
			          @userCode , -- CreatorCode - varchar(50)
			          NULL , -- LastModifyTime - datetime
			          ''  -- LastModifierCode - varchar(50)
			        )

			DELETE FROM #items WHERE Id=@Id;
		END
			
		UPDATE	dbo.mz_cf
		SET		fybz='2',
				LastModiFierCode=@userCode,
				LastModifyTime=GETDATE()
		WHERE	Id=@cfId

		COMMIT TRANSACTION
	END TRY 
	BEGIN CATCH 
		ROLLBACK TRANSACTION
		SET	@result= ERROR_MESSAGE();
	END CATCH 
END
ELSE
BEGIN 
	SET	@result= '未查询到符合发药的药品';
END
SELECT @result;
";

        /// <summary>
        /// 门诊扣库存 发药使用
        /// </summary>
        public const string mz_reduce_stock = @"
DECLARE @result VARCHAR(100);
SET @result='';
IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#items') and type='U')
BEGIN
	DROP TABLE #items;
END

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

SELECT NEWID() Id, cf.Id cfId, cfmx.Id cfmxId, mxph.cfmxphId, mxph.pc, mxph.ph, CONVERT(INT,mxph.sl) sl, cfmx.ypCode
INTO #items
FROM dbo.mz_cfmxph(NOLOCK) mxph
INNER JOIN dbo.mz_cfmx(NOLOCK) cfmx ON cfmx.cfh=mxph.cfh AND cfmx.ypCode=mxph.yp AND cfmx.OrganizeId=mxph.OrganizeId AND cfmx.zt='1' 
INNER JOIN dbo.mz_cf(NOLOCK) cf ON cf.cfh=mxph.cfh AND cf.jsnm>0 AND cf.fybz='1' AND cf.OrganizeId=mxph.OrganizeId AND cf.lyyf=mxph.fyyf AND cf.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp(NOLOCK) yp ON yp.ypCode=cfmx.ypCode AND yp.OrganizeId=cfmx.OrganizeId AND yp.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=yp.OrganizeId AND ypsx.zt='1'
WHERE mxph.cfh=@cfh AND mxph.OrganizeId=@OrganizeId AND mxph.gjzt='0' AND mxph.zt='1' AND mxph.fyyf=@yfbmCode AND mxph.yp=@ypCode
	
IF(EXISTS(SELECT 1 FROM #items))
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

		DECLARE @cfId BIGINT, @cfmxId BIGINT;
		DECLARE @cfmxphId VARCHAR(50), @Id VARCHAR(50), @pc VARCHAR(30), @ph VARCHAR(30);
		DECLARE @sl NUMERIC(6,2), @kcId VARCHAR(50);
				
		WHILE(EXISTS(SELECT 1 FROM #items))
		BEGIN
			SELECT TOP 1 @Id=Id, @cfId=cfId, @cfmxId=cfmxId, @cfmxphId=cfmxphId, @pc=pc, @ph=ph, @sl=sl FROM #items;
						
			IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmpkcxx') and type='U')
			BEGIN
				DROP TABLE #tmpkcxx;
			END

			SELECT kcId, kcsl, djsl 
			INTO #tmpkcxx FROM dbo.xt_yp_kcxx WITH (XLOCK, ROWLOCK) 
			WHERE ypdm=@ypCode AND ph=@ph AND pc=@pc AND zt='1' AND yfbmCode=@yfbmCode AND OrganizeId=@OrganizeId
			
			DECLARE @curKcxxDjsl INT;
			DECLARE @curKcxxKcId VARCHAR(50);
			DECLARE @sumOfDjsl INT;
			SELECT @sumOfDjsl=SUM(djsl) FROM #tmpkcxx ;
			IF @sumOfDjsl<@sl
			BEGIN
				ROLLBACK TRANSACTION;
				SELECT '冻结数量不足';
				RETURN;
			END 
			DECLARE @sysl INT;
			SET @sysl=@sl;

			WHILE EXISTS(SELECT 1 FROM #tmpkcxx) AND @sl>0
			BEGIN
				SELECT TOP 1 @curKcxxKcId=kcId, @curKcxxDjsl=djsl FROM #tmpkcxx
				IF @curKcxxDjsl>=@sysl
				BEGIN
					UPDATE dbo.xt_yp_kcxx SET djsl-=@sysl, kcsl-=@sysl, LastModifyTime=GETDATE(), LastModifierCode=@userCode WHERE kcId=@curKcxxKcId AND zt='1'
					SET @sysl=0;
				END
                ELSE
                BEGIN
					UPDATE dbo.xt_yp_kcxx SET djsl=0, kcsl-=@curKcxxDjsl, LastModifyTime=GETDATE(), LastModifierCode=@userCode WHERE kcId=@curKcxxKcId AND zt='1'
					SET @sysl-=@curKcxxDjsl;
				END 
				DELETE FROM #tmpkcxx WHERE kcId=@curKcxxKcId;
			END 

			DELETE FROM #items WHERE Id=@Id;
		END

		COMMIT TRANSACTION
		SELECT '';
	END TRY 
	BEGIN CATCH 
		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE();
	END CATCH 
END
ELSE
BEGIN 
	SELECT '未查询到符合发药的药品';
END
";

        #endregion

        #region 门诊发药统计
        
        /// <summary>
        /// 门诊发药统计
        /// </summary>
        public const string drug_deliver_statistics = @"
SELECT s.xm,s.nl,s.CardNo,s.cfh,s.fph,s.fyrq,s.ypmc,s.ycmc
,dbo.f_getComplexYpSlandDw(s.zxdwsl,s.zhyz,s.bmdw,s.zxdw) sl
,CONVERT(NUMERIC(11,4),s.zxdwjj*s.zhyz) jj, CONVERT(NUMERIC(11,2),s.zxdwjj*s.zxdwsl) jjze
,CONVERT(NUMERIC(11,4),s.zxdwlsj*s.zhyz) lsj, CONVERT(NUMERIC(11,2),s.zxdwlsj*s.zxdwsl) lsze
,s.bmdw
FROM ( 
	SELECT DISTINCT cf.xm,cf.nl,cf.CardNo,cf.cfh,cf.Fph fph,czjl.CreateTime fyrq,yp.ypmc,yp.ycmc,cfmx.sl*cfmx.zhyz zxdwsl,kcxx.jj/yp.bzs zxdwjj,yp.lsj/yp.bzs zxdwlsj,dbo.f_getyfbmZhyz(cf.lyyf, yp.ypCode, cf.OrganizeId) zhyz,yp.zxdw,dbo.f_getyfbmDw(cf.lyyf,yp.ypCode,yp.OrganizeId) bmdw
	FROM dbo.mz_cfmx(NOLOCK) cfmx  
	INNER JOIN dbo.mz_cf(NOLOCK) cf ON cf.cfh=cfmx.cfh AND cf.OrganizeId=cfmx.OrganizeId AND cf.zt='1'
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=cfmx.ypCode AND yp.OrganizeId=cf.OrganizeId
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=cf.OrganizeId
	INNER JOIN dbo.mz_cfmxph(NOLOCK) mxph ON mxph.cfh=cfmx.cfh
	INNER JOIN dbo.xt_yp_kcxx(NOLOCK) kcxx ON kcxx.pc=mxph.pc AND kcxx.yfbmCode=cf.lyyf
	INNER JOIN dbo.mz_cfypczjl(NOLOCK) czjl ON czjl.ypCode=cfmx.ypCode AND czjl.operateType='1' AND czjl.mzcfmxId=cfmx.Id
	WHERE cfmx.OrganizeId=@Organizeid
	AND czjl.CreateTime BETWEEN @startTime AND @endTime
    AND cf.lyyf=@yfbmCode
	AND (cf.xm LIKE '%'+@srm+'%' OR cf.CardNo LIKE '%'+@srm+'%' OR cf.Fph LIKE '%'+@srm+'%')
) s
";

        #endregion

        #region 门诊处方退药

        /// <summary>
        /// 门诊处方退药
        /// </summary>
        public const string mz_rp_reture_drug = @"
DECLARE @ktsl INT;
SELECT @ktsl=CONVERT(INT,ISNULL(SUM(a.sl),0)) 
FROM (
	SELECT mxph.sl
	FROM dbo.mz_cfmx(NOLOCK) cfmx
	INNER JOIN dbo.mz_cfmxph(NOLOCK) mxph ON mxph.yp=cfmx.ypCode AND mxph.cfh = cfmx.cfh AND mxph.OrganizeId = cfmx.OrganizeId AND mxph.zt='1' AND mxph.gjzt='0'
	WHERE cfmx.ypCode=@ypCode AND cfmx.zt='1' AND cfmx.OrganizeId=@OrganizeId
	AND mxph.fyyf=@yfbmCode AND mxph.ph=@ph AND mxph.pc=@pc
	UNION ALL
	SELECT -1*tymx.sl sl
	FROM dbo.mz_tfmx(NOLOCK) tymx
	WHERE tymx.ph=@ph AND tymx.pc=@pc
	AND tymx.OrganizeId=@OrganizeId AND tymx.ypCode=@ypCode
	AND tymx.cfh=@cfh AND tymx.zt='1'
) a

IF @ktsl<@tysl
BEGIN
	SELECT '退药数超出可退数量';
	RETURN ;
END 

SET NOCOUNT ON 
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

BEGIN TRY
	BEGIN TRANSACTION

	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#kcxx') and type='U')
	BEGIN
		DROP TABLE #kcxx;
	END
	
	DECLARE @curKcId VARCHAR(50);

	SELECT TOP 1 @curKcId= kcId 
	FROM dbo.xt_yp_kcxx WITH (XLOCK, ROWLOCK) 
	WHERE ypdm=@ypCode AND pc=@pc AND ph=@ph AND zt='1' AND yfbmCode=@yfbmCode

	IF ISNULL(@curKcId, '')<>''
	BEGIN
		UPDATE dbo.xt_yp_kcxx SET kcsl+=@tysl, LastModifyTime=GETDATE(), LastModifierCode=@userCode WHERE kcId=@curKcId
		
		UPDATE dbo.mz_cf SET fybz='3', LastModiFierCode=@userCode, LastModifyTime=GETDATE() 
		WHERE cfh=@cfh AND zt='1' AND OrganizeId=@OrganizeId AND lyyf=@yfbmCode 

		INSERT INTO dbo.mz_tfmx( returnDrugBillNo, OrganizeId ,ypCode ,cfh ,ph ,pc ,sl ,Remark ,zt ,CreateTime ,CreatorCode ,LastModifyTime ,LastModifierCode,zsm,sfcl)
		VALUES  ( @returnDrugBillNo, -- returnDrugBillNo - varchar(50)
				  @OrganizeId , -- OrganizeId - varchar(50)
		          @ypCode , -- ypCode - varchar(50)
		          @cfh , -- cfh - varchar(100)
		          @ph , -- ph - varchar(30)
		          @pc , -- pc - varchar(30)
		          @tysl , -- sl - numeric
		          '' , -- Remark - varchar(200)
		          '1' , -- zt - varchar(1)
		          GETDATE() , -- CreateTime - datetime
		          @userCode , -- CreatorCode - varchar(50)
		          NULL , -- LastModifyTime - datetime
		          '',  -- LastModifierCode - varchar(50)
				  @zsm,
				  @sfcl
		        )

		INSERT INTO dbo.mz_cfypczjl( mzcfmxId ,operateType ,ypCode ,cfh ,sl ,bz ,CreateTime ,CreatorCode ,LastModifyTime ,LastModifierCode,zsm,sfcl)
		SELECT TOP 1 Id,'2', ypCode, cfh, @tysl, '', GETDATE(), @userCode, NULL, NULL ,@zsm,@sfcl
		FROM dbo.mz_cfmx(NOLOCK) 
		WHERE cfh=@cfh AND ypCode=@ypCode AND zt='1' AND OrganizeId=@OrganizeId
	END	
	ELSE
    BEGIN
		SELECT '库存中不存在该批次药品'
		COMMIT TRANSACTION;
		RETURN;
	END 

	COMMIT TRANSACTION
	SELECT ''
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE();
END CATCH
";

        #endregion
    }
}
