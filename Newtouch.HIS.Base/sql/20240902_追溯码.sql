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

exec sp_addextendedproperty 'MS_Description', N'门诊处方药品操作记录', 'SCHEMA', 'dbo', 'TABLE', 'mz_cfypczjl'
go

alter table dbo.mz_cfypczjl
    add zsm varchar(1000)
go

exec sp_addextendedproperty 'MS_Description', N'追溯码', 'SCHEMA', 'dbo', 'TABLE', 'mz_cfypczjl', 'COLUMN', 'zsm'
go

alter table dbo.mz_cfypczjl
    add sfcl int
go

exec sp_addextendedproperty 'MS_Description', N'是否拆零，1：是，2否', 'SCHEMA', 'dbo', 'TABLE', 'mz_cfypczjl', 'COLUMN',
     'sfcl'
go

alter table dbo.mz_tfmx
    add zsm varchar(1000)
go

exec sp_addextendedproperty 'MS_Description', N'追溯码', 'SCHEMA', 'dbo', 'TABLE', 'mz_tfmx', 'COLUMN', 'zsm'
go

alter table dbo.mz_tfmx
    add sfcl int
go

exec sp_addextendedproperty 'MS_Description', N'是否拆零 1：是，2否', 'SCHEMA', 'dbo', 'TABLE', 'mz_tfmx', 'COLUMN',
     'sfcl'
go


alter table dbo.zy_ypyzxx
    add zsm varchar(1000)
go

exec sp_addextendedproperty 'MS_Description', N'追溯码', 'SCHEMA', 'dbo', 'TABLE', 'zy_ypyzxx', 'COLUMN', 'zsm'
go

alter table dbo.zy_ypyzxx
    add sfcl int
go

exec sp_addextendedproperty 'MS_Description', N'是否拆零，1：是，2：否', 'SCHEMA', 'dbo', 'TABLE', 'zy_ypyzxx', 'COLUMN',
     'sfcl'
go

alter table dbo.zy_ypyzczjl
    add zsm varchar(1000)
go

exec sp_addextendedproperty 'MS_Description', N'追溯码', 'SCHEMA', 'dbo', 'TABLE', 'zy_ypyzczjl', 'COLUMN', 'zsm'
go

alter table dbo.zy_ypyzczjl
    add sfcl int
go

exec sp_addextendedproperty 'MS_Description', N'是否拆零', 'SCHEMA', 'dbo', 'TABLE', 'zy_ypyzczjl', 'COLUMN', 'sfcl'
go


-- 库存盘点增加追溯码字段
alter table dbo.xt_yp_pdxxmx
    add zsm varchar(1000)
go

exec sp_addextendedproperty 'MS_Description', N'追溯码', 'SCHEMA', 'dbo', 'TABLE', 'xt_yp_pdxxmx', 'COLUMN', 'zsm'
go

alter table dbo.xt_yp_pdxxmx
    add sfcl int
go

exec sp_addextendedproperty 'MS_Description', N'是否拆零，1：是，2：否', 'SCHEMA', 'dbo', 'TABLE', 'xt_yp_pdxxmx', 'COLUMN',
     'sfcl'
go


-- pds 库结束