-- NewtouchHIS_Base.dbo.xt_ypsx definition

-- Drop table 药品属性

CREATE TABLE NewtouchHIS_Base.dbo.xt_ypsx_base 
(
	ypsxId int IDENTITY(1,1) NOT NULL,
	ypId int NOT NULL,
	OrganizeId varchar(50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	ypCode varchar(30) COLLATE Chinese_PRC_CI_AS NOT NULL,
	shbz char(1) COLLATE Chinese_PRC_CI_AS NULL,
	tsbz char(1) COLLATE Chinese_PRC_CI_AS NULL,
	jsbz char(1) COLLATE Chinese_PRC_CI_AS NULL,
	gzy char(1) COLLATE Chinese_PRC_CI_AS NULL,
	mzy char(1) COLLATE Chinese_PRC_CI_AS NULL,
	yljsy char(1) COLLATE Chinese_PRC_CI_AS NULL,
	zbbz char(20) COLLATE Chinese_PRC_CI_AS NULL,
	zlff char(2) COLLATE Chinese_PRC_CI_AS NULL,
	sjap char(2) COLLATE Chinese_PRC_CI_AS NULL,
	yl numeric(6,2) NULL,
	yldw varchar(10) COLLATE Chinese_PRC_CI_AS NULL,
	ypgg varchar(300) COLLATE Chinese_PRC_CI_AS NULL,
	ybdm varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	syts int NULL,
	dczdjl numeric(9,4) NULL,
	dczdsl numeric(9,4) NULL,
	ljzdjl numeric(9,4) NULL,
	ljzdsl numeric(9,4) NULL,
	pzwh varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	yptssx varchar(10) COLLATE Chinese_PRC_CI_AS NULL,
	ypflCode varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	jzlx char(1) COLLATE Chinese_PRC_CI_AS NULL,
	mrbzq int NULL,
	zjtzsj datetime NULL,
	xglx char(10) COLLATE Chinese_PRC_CI_AS NULL,
	ghdw varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	ypcd int NULL,
	CreatorCode varchar(50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	CreateTime datetime NOT NULL,
	LastModifyTime datetime NULL,
	LastModifierCode varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	zt char(1) COLLATE Chinese_PRC_CI_AS NOT NULL,
	px int NULL,
	xzyy bit NULL,
	xzyysm varchar(256) COLLATE Chinese_PRC_CI_AS NULL,
	LastYBUploadTime datetime NULL,
	mrjl decimal(9,2) NULL,
	mrpc varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	ybbz char(1) COLLATE Chinese_PRC_CI_AS DEFAULT '1' NOT NULL,
	xnhybdm varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	gjybdm varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	ybmlscrq datetime NULL,
	gjybmc varchar(100) COLLATE Chinese_PRC_CI_AS NULL,
	xjbs varchar(1) COLLATE Chinese_PRC_CI_AS NULL,
	dcxl decimal(9,2) NULL,
	mbxl decimal(9,2) NULL,
	mryf varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	ybgg varchar(300) COLLATE Chinese_PRC_CI_AS NULL,
	ypzsm varchar(100) COLLATE Chinese_PRC_CI_AS NULL,
	CONSTRAINT PK_xt_ypsx_3 PRIMARY KEY (ypsxId),
	VER varchar(255) COLLATE Chinese_PRC_CI_AS NULL
);

CREATE NONCLUSTERED INDEX idx_org_ypcode ON NewtouchHIS_Base.dbo.xt_ypsx_base (  OrganizeId ASC  , ypCode ASC  , zt ASC  )  
	 INCLUDE ( ypgg ) 
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;

CREATE NONCLUSTERED INDEX ix_xt_ypsx_ypId ON NewtouchHIS_Base.dbo.xt_ypsx_base (  ypId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- NewtouchHIS_Base.dbo.xt_yp_base definition

-- Drop table 系统药品

CREATE TABLE NewtouchHIS_Base.dbo.xt_yp_base
(
	ypId int NOT NULL,
	ypCode varchar(30) COLLATE Chinese_PRC_CI_AS NOT NULL,
	ypmc varchar(256) COLLATE Chinese_PRC_CI_AS NOT NULL,
	OrganizeId varchar(50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	ypqz varchar(10) COLLATE Chinese_PRC_CI_AS NULL,
	yphz varchar(10) COLLATE Chinese_PRC_CI_AS NULL,
	spm varchar(150) COLLATE Chinese_PRC_CI_AS NULL,
	py varchar(70) COLLATE Chinese_PRC_CI_AS NOT NULL,
	cfl numeric(9,4) NULL,
	cfdw varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	jl numeric(9,4) NULL,
	jldw varchar(10) COLLATE Chinese_PRC_CI_AS NULL,
	bzs numeric(19,2) NOT NULL,
	bzdw varchar(20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	mzcls numeric(19,4) NULL,
	mzcldw varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	zycls numeric(9,4) NULL,
	zycldw varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	zxdw varchar(20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	djdw varchar(20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	lsj numeric(11,6) NOT NULL,
	pfj numeric(11,6) NULL,
	zfbl numeric(9,2) NOT NULL,
	zfxz char(1) COLLATE Chinese_PRC_CI_AS NOT NULL,
	dlCode varchar(20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	jx varchar(60) COLLATE Chinese_PRC_CI_AS NULL,
	ycmc varchar(160) COLLATE Chinese_PRC_CI_AS NOT NULL,
	medid int NULL,
	medextid int NULL,
	ypbzdm varchar(50) COLLATE Chinese_PRC_CI_AS DEFAULT '3' NULL,
	nbdl varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	mzzybz char(1) COLLATE Chinese_PRC_CI_AS NOT NULL,
	CreatorCode varchar(50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	CreateTime datetime NOT NULL,
	LastModifyTime datetime NULL,
	LastModifierCode varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	zt char(1) COLLATE Chinese_PRC_CI_AS NOT NULL,
	px int NULL,
	lsbz bit NULL,
	mjzbz int NULL,
	yfCode varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	isKss char(1) COLLATE Chinese_PRC_CI_AS DEFAULT '0' NOT NULL,
	kssId varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	jybz varchar(20) COLLATE Chinese_PRC_CI_AS DEFAULT '1' NOT NULL,
	bz varchar(500) COLLATE Chinese_PRC_CI_AS NULL,
	cxjje numeric(9,4) NULL,
	tsypbz varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	VER varchar(255) COLLATE Chinese_PRC_CI_AS NULL
);

CREATE NONCLUSTERED INDEX ypCode_index ON NewtouchHIS_Base.dbo.xt_yp_base (  ypCode ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;

CREATE NONCLUSTERED INDEX ypmc_index ON NewtouchHIS_Base.dbo.xt_yp_base (  ypmc ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;

-- NewtouchHIS_Base.dbo.xt_sfdl_base definition

-- Drop table

CREATE TABLE NewtouchHIS_Base.dbo.xt_sfdl_base
(
	dlId int IDENTITY(1,1) NOT NULL,
	dlCode varchar(20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	dlmc varchar(50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	ParentId int NULL,
	OrganizeId varchar(50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	mzzybz char(1) COLLATE Chinese_PRC_CI_AS NOT NULL,
	mzprintreportcode varchar(20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	mzprintbillcode varchar(20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	py varchar(50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	CreatorCode varchar(50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	CreateTime datetime NOT NULL,
	LastModifyTime datetime NULL,
	LastModifierCode varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	zt char(1) COLLATE Chinese_PRC_CI_AS NOT NULL,
	px int NULL,
	reportdlcode varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	dllb int NULL,
	fjCode varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	fjmc varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	sn money NULL,
	dzfplbcode varchar(30) COLLATE Chinese_PRC_CI_AS NULL,
	dzfplbmc varchar(30) COLLATE Chinese_PRC_CI_AS NULL,
	zydzfplbcode varchar(30) COLLATE Chinese_PRC_CI_AS NULL,
	zydzfplbmc varchar(30) COLLATE Chinese_PRC_CI_AS NULL,
	CONSTRAINT PK_xt_sfdl_base1 PRIMARY KEY (dlId)
);

CREATE NONCLUSTERED INDEX ix_xt_sfdl_base_dlCodeOrgId ON NewtouchHIS_Base.dbo.xt_sfdl_base (  dlCode ASC  , OrganizeId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;		



-- NewtouchHIS_Base.dbo.xt_sfxm definition

-- Drop table

CREATE TABLE NewtouchHIS_Base.dbo.xt_sfxm_base (
	sfxmId int IDENTITY(1,1) NOT NULL,
	sfxmCode varchar(20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	sfxmmc varchar(256) COLLATE Chinese_PRC_CI_AS NOT NULL,
	sfdlCode varchar(200) COLLATE Chinese_PRC_CI_AS NOT NULL,
	badlCode varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	nbdlCode varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	OrganizeId varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	duration int NULL,
	py varchar(250) COLLATE Chinese_PRC_CI_AS NULL,
	dw varchar(220) COLLATE Chinese_PRC_CI_AS NULL,
	dj numeric(9,2) NULL,
	flCode varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	zfbl numeric(9,2) NULL,
	zfxz char(1) COLLATE Chinese_PRC_CI_AS NULL,
	mzzybz char(1) COLLATE Chinese_PRC_CI_AS NULL,
	ssbz char(1) COLLATE Chinese_PRC_CI_AS NULL,
	tsbz char(1) COLLATE Chinese_PRC_CI_AS NULL,
	sfbz char(1) COLLATE Chinese_PRC_CI_AS NULL,
	ybdm varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	wjdm varchar(40) COLLATE Chinese_PRC_CI_AS NULL,
	bz varchar(1000) COLLATE Chinese_PRC_CI_AS NULL,
	CreatorCode varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	CreateTime datetime NULL,
	LastModifyTime datetime NULL,
	LastModifierCode varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	px int NULL,
	zt char(1) COLLATE Chinese_PRC_CI_AS NULL,
	dwjls int NULL,
	jjcl int NULL,
	zxks varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	gg varchar(100) COLLATE Chinese_PRC_CI_AS NULL,
	LastYBUploadTime datetime NULL,
	ssdj varchar(2) COLLATE Chinese_PRC_CI_AS NULL,
	sqlx varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	ybbz char(1) COLLATE Chinese_PRC_CI_AS DEFAULT '1' NULL,
	xnhybdm varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	gjybdm varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	cxjje numeric(11,4) NULL,
	pzwh varchar(100) COLLATE Chinese_PRC_CI_AS NULL,
	sccj varchar(100) COLLATE Chinese_PRC_CI_AS NULL,
	gjybmc varchar(200) COLLATE Chinese_PRC_CI_AS NULL,
	wz_Code varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	wz_ConsumablesID varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	wz_SerialNumber varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	wz_Packing varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	wz_IsHight bit DEFAULT 0 NULL,
	wz_MaterialCode varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
	xjbs varchar(1) COLLATE Chinese_PRC_CI_AS NULL,
	ssicd9bm varchar(20) COLLATE Chinese_PRC_CI_AS NULL,
	iswzsame varchar(2) COLLATE Chinese_PRC_CI_AS NULL,
	CONSTRAINT PK_XT_SFXM_BASE PRIMARY KEY (sfxmId)
);

CREATE NONCLUSTERED INDEX Basesfxm_sfxmCode_OrganizeId_sfxmmc ON NewtouchHIS_Base.dbo.xt_sfxm_base (  sfxmCode ASC  , OrganizeId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
CREATE NONCLUSTERED INDEX sfxmCode_index ON NewtouchHIS_Base.dbo.xt_sfxm_base (  sfxmCode ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
CREATE NONCLUSTERED INDEX sfxmmc_index ON NewtouchHIS_Base.dbo.xt_sfxm_base (  sfxmmc ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;