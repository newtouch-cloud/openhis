namespace Newtouch.DomainServices.TSQL
{
    /// <summary>
    /// 住院医嘱
    /// </summary>
    public class InpatientYz
    {
        /// <summary>
        /// 医嘱执行 包括文字医嘱
        /// </summary>
        public const string zyOrderExecutionWithWzYz = @"
BEGIN TRAN
BEGIN TRY
	DECLARE @yzxhTab TABLE (
		yzxh VARCHAR(50)
	)
	INSERT INTO @yzxhTab ( yzxh )
	SELECT * FROM dbo.f_split(@yzxhlist,',')
	IF @@error <> 0 
	BEGIN      
		SELECT 'F|获取医嘱序号传值失败'  
		ROLLBACK TRAN
		RETURN    
	END 

	----创建医嘱表，保存医嘱信息
	DECLARE @OrderTab TABLE
	(
		Id VARCHAR(50),
		OrganizeId VARCHAR(50),
		zyh VARCHAR(20),
		zh INT,
		WardCode VARCHAR(20),
		DeptCode VARCHAR(20),
		ysgh VARCHAR(20),
		pcCode VARCHAR(20),
		zxcs INT,
		xmdm VARCHAR(50),
		xmmc NVARCHAR(50),
		dw VARCHAR(50),
		zbbz INT,
		sl INT,
		yzlx INT,
		zxsj DATETIME,
		zxr VARCHAR(50),
		ypjl NUMERIC(14,3),
		ypgg VARCHAR(200),
		zxksdm NVARCHAR(200),
		yzxz INT,
        isjf INT
	)
	---根据医嘱id将医嘱信息插入表中
	INSERT INTO @OrderTab
	( 
		Id ,
		OrganizeId ,
		zyh ,
		zh ,
		WardCode ,
		DeptCode ,
		ysgh,
		pcCode ,
		zxcs ,
		xmdm ,
		xmmc ,
		dw ,
		zbbz,
		sl ,
		yzlx ,
		zxsj ,
		zxr ,
		ypjl ,
		ypgg ,
		zxksdm,
		yzxz,
        isjf
	)
	SELECT Id,OrganizeId,zyh,zh,WardCode,DeptCode,ysgh,pccode,ISNULL(zxcs,1) ,xmdm,xmmc,dw,zbbz,sl,yzlx,zxsj,zxr,ypjl,ypgg,zxksdm,2 AS yzxz,isjf 
	FROM dbo.zy_cqyz a
	WHERE Id IN (SELECT yzxh FROM @yzxhTab) --AND yzlx IN (2,4)
	UNION ALL 
	SELECT Id,OrganizeId,zyh,zh,WardCode,DeptCode,ysgh,pccode,ISNULL(zxcs,1),xmdm,xmmc,dw,zbbz,sl,yzlx,zxsj,zxr,ypjl,ypgg,zxksdm,1 AS yzxz ,isjf
	FROM dbo.zy_lsyz b
	WHERE Id IN (SELECT yzxh FROM @yzxhTab) --AND yzlx IN (2,4)
	IF @@error <> 0 
	BEGIN      
		SELECT 'F|获取医嘱信息失败'  
		ROLLBACK TRAN
		RETURN    
	END 

	-----插入费用明细库
	INSERT INTO dbo.zy_fymxk
	( 
		Id ,OrganizeId ,zyh ,yzxh ,	zxrq ,qqrq ,xmdm ,xmmc ,gg ,dw ,sl ,dj , zje ,yzxz ,ybdm ,jzks ,gdzxbz ,yzlb ,
		WardCode ,DeptCode ,cwdm ,czyh ,ysgh ,ysksdm ,zxksdm ,CreateTime ,CreatorCode ,zt,isjf
	)
	SELECT NEWID(),a.OrganizeId,a.zyh,a.Id,@zxrq,GETDATE(),a.xmdm,a.xmmc,a.ypgg,b.zycldw,a.sl,b.lsj/b.bzs*b.zycls AS dj,a.sl*b.lsj/b.bzs*b.zycls AS zje,a.yzxz,c.ybdm,a.DeptCode,0,0,a.WardCode,a.DeptCode,d.BedCode,@czyh,a.ysgh,a.DeptCode,a.zxksdm,GETDATE(),@czyh,'1',isjf 
	FROM @OrderTab a
	LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_yp b ON a.xmdm=b.ypCode AND a.OrganizeId=b.OrganizeId AND b.zt='1'
	LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx c ON b.ypId=c.ypId  AND b.OrganizeId=c.OrganizeId AND c.zt='1'
	LEFT JOIN dbo.zy_brxxk d ON a.zyh =d.zyh AND a.OrganizeId=d.OrganizeId AND d.zt='1'
	WHERE  a.yzlx IN (2,4,10)
	UNION ALL
	SELECT NEWID(),a.OrganizeId,a.zyh,a.Id,@zxrq,GETDATE(),a.xmdm,a.xmmc,a.ypgg,b.dw,a.sl,b.dj AS dj,a.sl*b.dj AS zje,a.yzxz,b.ybdm,a.DeptCode,0,1,a.WardCode,a.DeptCode,d.BedCode,@czyh,a.ysgh,a.DeptCode,a.zxksdm,GETDATE(),@czyh,'1' ,isjf
	FROM @OrderTab a
	LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfxm b ON a.xmdm=b.sfxmCode AND a.OrganizeId=b.OrganizeId AND b.zt='1'
	LEFT JOIN dbo.zy_brxxk d ON a.zyh =d.zyh AND a.OrganizeId=d.OrganizeId AND d.zt='1'
	WHERE a.yzlx NOT IN (2,3,4,10,11)
	IF @@error <> 0 
	BEGIN      
		SELECT 'F|插入费用明细库失败'   
		ROLLBACK TRAN
		RETURN    
	END 

	-----药品医嘱插入发药请求库
	INSERT INTO dbo.zy_fyqqk
	( 
		Id ,OrganizeId ,lyxh ,zyh ,hzxm ,yzxh ,fzxh ,yfdm ,WardCode ,DeptCode ,ysgh ,zxrq ,qqrq ,ypdm ,ypmc ,
		ypsl ,ypgg ,ypdw ,ypdj ,zxcs ,tybz ,yzxz ,zbbz ,mcsl ,CreateTime ,CreatorCode ,zt
	)
	SELECT NEWID(),a.OrganizeId, @lyxh, a.zyh,d.xm,a.Id,a.zh,a.zxksdm,a.WardCode,a.DeptCode,a.ysgh,@zxrq,GETDATE(),a.xmdm,a.xmmc,a.sl,a.ypgg,b.zycldw AS dw,b.lsj/b.bzs*b.zycls AS ypdj,
	a.zxcs,1 AS tybz,a.yzxz,a.zbbz,a.ypjl ,GETDATE(),@czyh,'1' FROM @OrderTab a
	LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_yp b ON a.xmdm=b.ypCode AND a.OrganizeId=b.OrganizeId AND b.zt='1'
	LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx c ON b.ypId=c.ypId AND b.OrganizeId=c.OrganizeId AND c.zt='1'
	LEFT JOIN dbo.zy_brxxk d ON a.zyh =d.zyh AND a.OrganizeId=d.OrganizeId AND d.zt='1'
	WHERE  a.yzlx IN (2,4,10) and a.isjf<>'0'
	IF @@error <> 0 
	BEGIN      
		SELECT 'F|插入发药请求库失败'    
		ROLLBACK TRAN
		RETURN    
	END 

	-----项目医嘱插入通用膳食请求库
	INSERT INTO dbo.zy_tyssqqk( 
				Id , OrganizeId ,lyxh , zyh ,hzxm,yzxh,fzxh ,yfdm ,   WardCode ,DeptCode ,  ysgh ,zxrq ,qqrq ,     xmdm , xmmc ,  sl , dw ,  dj ,                  zxcs, zyxz ,  zbbz,  mcsl,  yzlx,CreateTime,CreatorCode ,zt)
	SELECT NEWID(),a.OrganizeId,@lyxh,a.zyh,d.xm,a.Id,a.zh,a.zxksdm,a.WardCode,a.DeptCode,a.ysgh,@zxrq,GETDATE(),a.xmdm,a.xmmc,a.sl,b.dw ,ISNULL(b.dj,0) AS ypdj,a.zxcs,a.yzxz,a.zbbz,a.ypjl,a.yzlx,GETDATE(), @czyh,       '1' 
	FROM @OrderTab a
	LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfxm b ON a.xmdm=b.sfxmCode AND a.OrganizeId=b.OrganizeId AND b.zt='1'
	LEFT JOIN dbo.zy_brxxk d ON a.zyh =d.zyh AND a.OrganizeId=d.OrganizeId AND d.zt='1'
	WHERE  a.yzlx NOT IN (2,4,10)
	IF @@error <> 0 
	BEGIN      
		SELECT 'F|插入通用膳食请求库失败'   
		ROLLBACK TRAN
		RETURN    
	END 

	-----更新医嘱状态
	UPDATE dbo.zy_cqyz SET yzzt=2, zxsj=@zxrq,zxr=@czyh,LastModifierCode=@czyh,LastModifyTime=GETDATE() WHERE Id IN (SELECT yzxh FROM @yzxhTab)
	IF @@error <> 0 
	BEGIN      
		SELECT 'F|更新医嘱信息失败'  
		ROLLBACK TRAN
		RETURN    
	END 
	UPDATE dbo.zy_lsyz SET yzzt=2, zxsj=@zxrq,zxr=@czyh,LastModifierCode=@czyh,LastModifyTime=GETDATE() WHERE Id IN (SELECT yzxh FROM @yzxhTab)
	IF @@error <> 0 
	BEGIN      
		SELECT 'F|更新医嘱信息失败'  
		ROLLBACK TRAN
		RETURN    
	END 
	SELECT 'T|执行成功'
	COMMIT TRAN
END TRY
BEGIN CATCH
	SELECT 'F|'+ERROR_MESSAGE()
	ROLLBACK TRAN
END CATCH
";
    }
}