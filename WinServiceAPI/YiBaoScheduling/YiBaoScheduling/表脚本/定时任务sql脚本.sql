Create table Log_CqYbScheduling 
(
[Id] [bigint] IDENTITY(1,1) NOT NULL,
zyh [varchar](10) NULL,
infno [varchar](10) NULL,
infcode [varchar](10) NULL,
err_msg [varchar](200) NULL,
createtime datetime NULL,
 
CONSTRAINT [PK_Log_CqYbSchedulingLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
