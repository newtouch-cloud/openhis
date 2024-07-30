# 新致新源--开源医疗--院版his

## 目录

- [介绍](#介绍)
- [安装](#安装)
- [使用](#使用)
- [贡献](#贡献)
- [许可证](#许可证)
- [作者](#作者)

## 介绍

院版HIS是 一款适用于公立二级以下医院和社区卫生机构的综合性医院信息系统，它包含体检、后台管理、收费结算、医护协同、药房、电子病历等10大功能模块，支持门诊、住院、医技、后勤各项核心业务。

![模块图](https://doc.openhis.org.cn//assets/images/medic-BnmDeIW8-2f6acc1caf5b3380bdf3bba86916ba07.png)

项目目录结构如下：
```
## 目录结构介绍
his-dll-common                      公共ddl包
JSViewer_MVC_Core                   报表模块
Newtouch.HIS.Base                   后台管理模块
Newtouch.HIS.CIS                    医护协同模块
Newtouch.HIS.EMR                    电子病例模块
Newtouch.HIS.HangFire               定时任务模块
Newtouch.HIS.Herp                   医疗资源管理模块
Newtouch.HIS.MRMS                   病案管理模块
Newtouch.HIS.MRQC                   病例质控模块
Newtouch.HIS.OR                     手术管理模块
Newtouch.HIS.PDS                    药房药库管理模块
Newtouch.HIS.Sett                   结算管理模块
Newtouch.HIS.SSO                    联合工作站以及基础API
Newtouch.HIS.Static                 静态资源服务
Newtouch.HIS.WebAPI.Manage          API管理
WinServiceAPI                       医保插件
```

## 安装

使用Visual Studio 2017 或 Visual Studio 2022 打开对应模块的 .sln文件

### 前提条件

- [Visual Studio](https://visualstudio.microsoft.com/)

### 克隆仓库

```bash
git clone https://github.com/newtouch-cloud/openhis.git
cd openhis
```

