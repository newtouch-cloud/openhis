-- pds 库开始

alter table dbo.xt_yp_crkmx
    add zsm varchar(1000)
    go

exec sp_addextendedproperty 'MS_Description', N'追溯码', 'SCHEMA', 'dbo', 'TABLE', 'xt_yp_crkmx', 'COLUMN', 'zsm'
go

alter table dbo.xt_yp_crkmx
    add sfcl int
    go

exec sp_addextendedproperty 'MS_Description', N'是否拆零， 1：是，2：否', 'SCHEMA', 'dbo', 'TABLE', 'xt_yp_crkmx', 'COLUMN',
     'sfcl'
go

-- pds 库结束