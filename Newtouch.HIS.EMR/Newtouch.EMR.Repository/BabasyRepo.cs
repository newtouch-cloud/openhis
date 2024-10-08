﻿using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.EMR.Domain.BusinessObjects;
using System;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// 创 建：hyj
    /// 日 期：2018-09-20 18:23
    /// 描 述：选项明细表
    /// </summary>
    public class BabasyRepo : RepositoryBase<BabasyEntity>, IBabasyRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public BabasyRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(BabasyVO entity, string keyValue)
        {
            var dbEntity = new BabasyEntity();
            int flag = 0;
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                dbEntity = this.FindEntity(keyValue);
                if (dbEntity != null)
                {
                    flag = 1;
                }
            }

            if (flag==1)
            {
                dbEntity = this.FindEntity(keyValue);
                //properties
                #region 病案首页更新
                dbEntity.zid = entity.zid;
                dbEntity.tmh = entity.tmh;
                dbEntity.bah = entity.bah;
                dbEntity.zyh = entity.zyh;
                dbEntity.fylb = entity.fylb;
                dbEntity.brxm = entity.brxm;
                dbEntity.byzg = entity.byzg;
                dbEntity.xb = entity.xb;
                if (!string.IsNullOrWhiteSpace(entity.csny))
                {
                    dbEntity.csny = Convert.ToDateTime(entity.csny);
                }               
                dbEntity.nl = entity.nl;
                dbEntity.zy = entity.zy;
                dbEntity.hyzk = entity.hyzk;
                dbEntity.csdz = entity.csdz;
                dbEntity.mz = entity.mz;
                dbEntity.gj = entity.gj;
                dbEntity.sfzhm = entity.sfzhm;
                dbEntity.dwmc = entity.dwmc;
                dbEntity.dwdh = entity.dwdh;
                dbEntity.dwyb = entity.dwyb;
                dbEntity.brfb = entity.brfb;
                dbEntity.hkdz = entity.hkdz;
                dbEntity.hkyb = entity.hkyb;
                dbEntity.lxr = entity.lxr;
                dbEntity.gx = entity.gx;
                dbEntity.lxdz = entity.lxdz;
                dbEntity.lxdh = entity.lxdh;
                dbEntity.ryks = entity.ryks;
                dbEntity.ryrq = entity.ryrq;
                dbEntity.rysj = entity.rysj;
                dbEntity.rytj = entity.rytj;
                dbEntity.rybq = entity.rybq;
                dbEntity.cyks = entity.cyks;
                dbEntity.cyrq = entity.cyrq;
                dbEntity.cysj = entity.cysj;
                dbEntity.mzzd = entity.mzzd;
                dbEntity.mzys = entity.mzys;
                dbEntity.ryzd = entity.ryzd;
                dbEntity.ryys = entity.ryys;
                if (!string.IsNullOrWhiteSpace(entity.qzrq))
                {
                    dbEntity.qzrq = Convert.ToDateTime(entity.qzrq);
                }
                dbEntity.cydz = entity.cydz;
                dbEntity.cyqk = entity.cyqk;
                dbEntity.cyzd = entity.cyzd;
                dbEntity.lx = entity.lx;
                dbEntity.mebm = entity.mebm;
                dbEntity.bfz = entity.bfz;
                dbEntity.yngr = entity.yngr;
                dbEntity.blzd = entity.blzd;
                dbEntity.blh = entity.blh;
                dbEntity.gmyw = entity.gmyw;
                dbEntity.szqx = entity.szqx;
                dbEntity.xx = entity.xx;
                dbEntity.sj = entity.sj;
                dbEntity.sx = entity.sx;
                dbEntity.mzzl = entity.mzzl;
                dbEntity.rcf = entity.rcf;
                dbEntity.zllx = entity.zllx;
                dbEntity.lcblf = entity.lcblf;
                dbEntity.lcsjf = entity.lcsjf;
                dbEntity.qjcs = entity.qjcs;
                dbEntity.cgcs = entity.cgcs;
                dbEntity.qjff = entity.qjff;
                dbEntity.tjh = entity.tjh;
                dbEntity.sy = entity.sy;
                dbEntity.sjba = entity.sjba;
                dbEntity.kyba = entity.kyba;
                dbEntity.bazl = entity.bazl;
                dbEntity.swsj = entity.swsj;
                dbEntity.eflh = entity.eflh;
                dbEntity.kzr = entity.kzr;
                dbEntity.zzys = entity.zzys;
                dbEntity.sxys = entity.sxys;
                dbEntity.bazlr = entity.bazlr;
                dbEntity.bmy = entity.bmy;
                dbEntity.ssrq = entity.ssrq;
                dbEntity.ssbm = entity.ssbm;
                dbEntity.sslb = entity.sslb;
                dbEntity.mzfs = entity.mzfs;
                dbEntity.mzysr = entity.mzysr;
                dbEntity.qklb = entity.qklb;
                dbEntity.qkyh = entity.qkyh;
                dbEntity.ssys = entity.ssys;
                dbEntity.sqhf = entity.sqhf;
                dbEntity.zyf = entity.zyf;
                dbEntity.xyf = entity.xyf;
                dbEntity.zcyf = entity.zcyf;
                dbEntity.zcayf = entity.zcayf;
                dbEntity.jyf = entity.jyf;
                dbEntity.zlf = entity.zlf;
                dbEntity.clf = entity.clf;
                dbEntity.fsf = entity.fsf;
                dbEntity.ssf = entity.ssf;
                dbEntity.hyf = entity.hyf;
                dbEntity.sxf = entity.sxf;
                dbEntity.syf = entity.syf;
                dbEntity.jsf = entity.jsf;
                dbEntity.bbf = entity.bbf;
                dbEntity.qtf = entity.qtf;
                dbEntity.zfy = entity.zfy;
                dbEntity.srrq = entity.srrq;
                dbEntity.czydm = entity.czydm;
                dbEntity.bj = entity.bj;
                dbEntity.gdbj = entity.gdbj;
                dbEntity.year = entity.year;
                dbEntity.month = entity.month;
                dbEntity.id = entity.id;
                dbEntity.grbw = entity.grbw;
                dbEntity.lcyfs = entity.lcyfs;
                dbEntity.rycs = entity.rycs;
                dbEntity.hbsag = entity.hbsag;
                dbEntity.hcv = entity.hcv;
                dbEntity.hiv = entity.hiv;
                dbEntity.zrfys = entity.zrfys;
                dbEntity.jxys = entity.jxys;
                dbEntity.yjssxys = entity.yjssxys;
                dbEntity.zkys = entity.zkys;
                dbEntity.zkhs = entity.zkhs;
                dbEntity.barq = entity.barq;
                dbEntity.dyl = entity.dyl;
                dbEntity.sz = entity.sz;
                dbEntity.rh = entity.rh;
                dbEntity.sxfy = entity.sxfy;
                dbEntity.sxpz = entity.sxpz;
                dbEntity.sxsl = entity.sxsl;
                dbEntity.hlf = entity.hlf;
                dbEntity.mzf = entity.mzf;
                dbEntity.yef = entity.yef;
                dbEntity.ssys1 = entity.ssys1;
                dbEntity.ssys2 = entity.ssys2;
                dbEntity.sxdw = entity.sxdw;
                dbEntity.grbyjc = entity.grbyjc;
                dbEntity.grbyzd = entity.grbyzd;
                dbEntity.sx9jc = entity.sx9jc;
                dbEntity.kss1 = entity.kss1;
                dbEntity.kss2 = entity.kss2;
                dbEntity.kss3 = entity.kss3;
                dbEntity.tsjc_ct = entity.tsjc_ct;
                dbEntity.tsjc_mri = entity.tsjc_mri;
                dbEntity.tsjc_dpl = entity.tsjc_dpl;
                dbEntity.tsjc4 = entity.tsjc4;
                dbEntity.tsjc5 = entity.tsjc5;
                dbEntity.tsjc6 = entity.tsjc6;
                dbEntity.ba_jy = entity.ba_jy;
                dbEntity.sx_hxb = entity.sx_hxb;
                dbEntity.sx_xxb = entity.sx_xxb;
                dbEntity.sx_xj = entity.sx_xj;
                dbEntity.sx_qx = entity.sx_qx;
                dbEntity.sx_qt = entity.sx_qt;
                dbEntity.dzbl_id = entity.dzbl_id;
                dbEntity.kzrmc = entity.kzrmc;
                dbEntity.zyysmc = entity.zyysmc;
                dbEntity.zzysmc = entity.zzysmc;
                dbEntity.zrysmc = entity.zrysmc;
                dbEntity.ryzdmc = entity.ryzdmc;
                dbEntity.cyzdmc = entity.cyzdmc;
                dbEntity.csd_s = entity.csd_s;
                dbEntity.csd_x = entity.csd_x;
                dbEntity.sfbl = entity.sfbl;
                dbEntity.szqxz = entity.szqxz;
                dbEntity.szqxy = entity.szqxy;
                dbEntity.szqxn = entity.szqxn;
                dbEntity.qtzd = entity.qtzd;
                dbEntity.wbys = entity.wbys;
                dbEntity.ssmc = entity.ssmc;
                dbEntity.rybs = entity.rybs;
                dbEntity.cybs = entity.cybs;
                dbEntity.zkks = entity.zkks;
                dbEntity.nldw = entity.nldw;
                dbEntity.bwcs = entity.bwcs;
                dbEntity.bzcs = entity.bzcs;
                dbEntity.pbcs = entity.pbcs;
                dbEntity.czy = entity.czy;
                dbEntity.zyzt = entity.zyzt;
                dbEntity.ryyy = entity.ryyy;
                dbEntity.IDC = entity.IDC;
                dbEntity.IDC1 = entity.IDC1;
                dbEntity.ztjs = entity.ztjs;
                dbEntity.cyzt = entity.cyzt;
                dbEntity.qtzd1 = entity.qtzd1;
                dbEntity.qtzd2 = entity.qtzd2;
                dbEntity.qtzd3 = entity.qtzd3;
                dbEntity.qtzd4 = entity.qtzd4;
                dbEntity.jszlsq = entity.jszlsq;
                dbEntity.hblx = entity.hblx;
                dbEntity.qfyy = entity.qfyy;
                dbEntity.mzycy = entity.mzycy;
                dbEntity.lcybl = entity.lcybl;
                dbEntity.fsybl = entity.fsybl;
                dbEntity.dbr = entity.dbr;
                dbEntity.dbrdw = entity.dbrdw;
                dbEntity.lxdhdbr = entity.lxdhdbr;
                dbEntity.hljb = entity.hljb;
                dbEntity.dqzt = entity.dqzt;
                dbEntity.zdmc1 = entity.zdmc1;
                dbEntity.zdmc2 = entity.zdmc2;
                dbEntity.zdmc3 = entity.zdmc3;
                dbEntity.zdmc4 = entity.zdmc4;
                dbEntity.zdmc5 = entity.zdmc5;
                dbEntity.zydj_id = entity.zydj_id;
                dbEntity.zrys = entity.zrys;
                dbEntity.tz_xsecs = entity.tz_xsecs;
                dbEntity.tz_xsery = entity.tz_xsery;
                dbEntity.cs_t = entity.cs_t;
                dbEntity.zzjgdm = entity.zzjgdm;
                dbEntity.xzdz = entity.xzdz;
                dbEntity.zzyb = entity.zzyb;
                dbEntity.mzzd_icd = entity.mzzd_icd;
                dbEntity.blzd_icd = entity.blzd_icd;
                dbEntity.wbyy = entity.wbyy;
                dbEntity.bch_ry = entity.bch_ry;
                dbEntity.bch_cy = entity.bch_cy;
                dbEntity.zrhsmc = entity.zrhsmc;
                dbEntity.zkrq = entity.zkrq;
                dbEntity.ssjb = entity.ssjb;
                dbEntity.dj_qkyh = entity.dj_qkyh;
                dbEntity.jsjg_yl = entity.jsjg_yl;
                dbEntity.jsjg_wsy = entity.jsjg_wsy;
                dbEntity.zczymd = entity.zczymd;
                dbEntity.zfje = entity.zfje;
                dbEntity.zczy_jh = entity.zczy_jh;
                dbEntity.sm = entity.sm;
                dbEntity.hmsj_ryq = entity.hmsj_ryq;
                dbEntity.hmsj_ryh = entity.hmsj_ryh;
                dbEntity.lyfs = entity.lyfs;
                dbEntity.zrhs = entity.zrhs;
                dbEntity.wbyy_icd = entity.wbyy_icd;
                dbEntity.jkkh = entity.jkkh;
                dbEntity.zyjh = entity.zyjh;
                dbEntity.mu = entity.mu;
                dbEntity.hkdt = entity.hkdt;
                dbEntity.hkdx = entity.hkdx;
                dbEntity.xzdt = entity.xzdt;
                dbEntity.xzdx = entity.xzdx;
                dbEntity.xzz_lxdh = entity.xzz_lxdh;
                dbEntity.xzz_yb = entity.xzz_yb;
                dbEntity.hmsj_ryq_t = entity.hmsj_ryq_t;
                dbEntity.hmsj_ryq_x = entity.hmsj_ryq_x;
                dbEntity.hmsj_ryq_f = entity.hmsj_ryq_f;
                dbEntity.hmsj_ryh_t = entity.hmsj_ryh_t;
                dbEntity.hmsj_ryh_x = entity.hmsj_ryh_x;
                dbEntity.hmsj_ryh_f = entity.hmsj_ryh_f;
                dbEntity.kjyf_sybz = entity.kjyf_sybz;
                dbEntity.kjyf_sycxsj = entity.kjyf_sycxsj;
                dbEntity.lhyy = entity.lhyy;
                dbEntity.byxtzdry = entity.byxtzdry;
                dbEntity.byjscrysj = entity.byjscrysj;
                dbEntity.zrmc = entity.zrmc;
                dbEntity.lclj_jrbz = entity.lclj_jrbz;
                dbEntity.lclj_wcbz = entity.lclj_wcbz;
                dbEntity.lclj_tcyy = entity.lclj_tcyy;
                dbEntity.lclj_bybz = entity.lclj_bybz;
                dbEntity.lclj_byyy = entity.lclj_byyy;
                dbEntity.lclj_mc = entity.lclj_mc;
                dbEntity.bwbz_gzbz = entity.bwbz_gzbz;
                dbEntity.zzlys = entity.zzlys;
                dbEntity.tnmfq = entity.tnmfq;
                dbEntity.jg_s = entity.jg_s;
                dbEntity.jg_qs = entity.jg_qs;
                dbEntity.zy_zygz = entity.zy_zygz;
                dbEntity.zy_rzzzbf = entity.zy_rzzzbf;
                dbEntity.zy_syzcy = entity.zy_syzcy;
                dbEntity.zy_rsmdsc = entity.zy_rsmdsc;
                dbEntity.zy_rscx = entity.zy_rscx;
                dbEntity.zy_cxl = entity.zy_cxl;
                dbEntity.zy_xsr_ch = entity.zy_xsr_ch;
                dbEntity.zy_xsr_pku = entity.zy_xsr_pku;
                dbEntity.zy_xsr_cah = entity.zy_xsr_cah;
                dbEntity.zy_xsr_g6pd = entity.zy_xsr_g6pd;
                dbEntity.zy_xsr_tl = entity.zy_xsr_tl;
                dbEntity.zy_yygrqk = entity.zy_yygrqk;
                dbEntity.zy_grbw = entity.zy_grbw;
                dbEntity.zy_grmc = entity.zy_grmc;
                dbEntity.basy_bb = entity.basy_bb;
                dbEntity.xzds = entity.xzds;
                dbEntity.zyzdzyhzqk = entity.zyzdzyhzqk;
                dbEntity.zdfhqk_mzycy = entity.zdfhqk_mzycy;
                dbEntity.zdfhqk_ryycy = entity.zdfhqk_ryycy;
                dbEntity.zdfhqk_sqysh = entity.zdfhqk_sqysh;
                dbEntity.zdfhqk_lcybl = entity.zdfhqk_lcybl;
                dbEntity.zdfhqk_fsybl = entity.zdfhqk_fsybl;
                dbEntity.lclj_gl = entity.lclj_gl;
                dbEntity.nl_y = entity.nl_y;
                dbEntity.tyzbzry = entity.tyzbzry;
                dbEntity.jgts = entity.jgts;
                dbEntity.qtzf = entity.qtzf;
                dbEntity.ybylfwf = entity.ybylfwf;
                dbEntity.ybzlczf = entity.ybzlczf;
                dbEntity.blzdf = entity.blzdf;
                dbEntity.syszdf = entity.syszdf;
                dbEntity.yxxzdf = entity.yxxzdf;
                dbEntity.lczdxmf = entity.lczdxmf;
                dbEntity.fsszlxmf = entity.fsszlxmf;
                dbEntity.lcwlzlf = entity.lcwlzlf;
                dbEntity.sszlf = entity.sszlf;
                dbEntity.kff = entity.kff;
                dbEntity.zyzlf = entity.zyzlf;
                dbEntity.kjywfy = entity.kjywfy;
                dbEntity.zcyf_zcy = entity.zcyf_zcy;
                dbEntity.xf = entity.xf;
                dbEntity.bdblzpf = entity.bdblzpf;
                dbEntity.qdblzpf = entity.qdblzpf;
                dbEntity.nxyzlzpf = entity.nxyzlzpf;
                dbEntity.xbyzlzpf = entity.xbyzlzpf;
                dbEntity.jcyycxyyclf = entity.jcyycxyyclf;
                dbEntity.zlyycxyyclf = entity.zlyycxyyclf;
                dbEntity.ssyycxyyclf = entity.ssyycxyyclf;
                dbEntity.qj_cgcs = entity.qj_cgcs;
                dbEntity.zzys_ys = entity.zzys_ys;
                dbEntity.ynbl_bz = entity.ynbl_bz;
                dbEntity.jg_zyzj_sybz = entity.jg_zyzj_sybz;
                dbEntity.bzsh = entity.bzsh;
                dbEntity.zyzlsb_sybz = entity.zyzlsb_sybz;
                dbEntity.zyzljs_sybz = entity.zyzljs_sybz;
                dbEntity.kjyw_syqk = entity.kjyw_syqk;
                dbEntity.yf_fyyw = entity.yf_fyyw;
                dbEntity.lcbx = entity.lcbx;
                dbEntity.qtyljgmc = entity.qtyljgmc;
                dbEntity.yymc = entity.yymc;
                dbEntity.ylfffs = entity.ylfffs;
                dbEntity.hkdz_s = entity.hkdz_s;
                dbEntity.rybf = entity.rybf;
                dbEntity.cybf = entity.cybf;
                dbEntity.sjzyts = entity.sjzyts;
                dbEntity.mzzd_jbmc_zy = entity.mzzd_jbmc_zy;
                dbEntity.mzzd_jbbm_zy = entity.mzzd_jbbm_zy;
                dbEntity.wbyybm = entity.wbyybm;
                dbEntity.ywgm = entity.ywgm;
                dbEntity.bzlzhzf_zy = entity.bzlzhzf_zy;
                dbEntity.zyzd_zyfw = entity.zyzd_zyfw;
                dbEntity.zyzl_zyfw = entity.zyzl_zyfw;
                dbEntity.zywz_zyfw = entity.zywz_zyfw;
                dbEntity.zygs_zyfw = entity.zygs_zyfw;
                dbEntity.zcyjf_zyfw = entity.zcyjf_zyfw;
                dbEntity.tnzl_zyfw = entity.tnzl_zyfw;
                dbEntity.gczl_zyfw = entity.gczl_zyfw;
                dbEntity.tszl_zyfw = entity.tszl_zyfw;
                dbEntity.zyqt_zyfw = entity.zyqt_zyfw;
                dbEntity.zytstpjg_zyfw = entity.zytstpjg_zyfw;
                dbEntity.bzss_zyfw = entity.bzss_zyfw;
                dbEntity.yljgzyzjf_zyf = entity.yljgzyzjf_zyf;
                dbEntity.bzlzf_zy = entity.bzlzf_zy;
                dbEntity.ryzd_jbbm = entity.ryzd_jbbm;
                dbEntity.qtfy = entity.qtfy;
                dbEntity.shcd = entity.shcd;
                dbEntity.ddzc_yy = entity.ddzc_yy;
                dbEntity.zdfhqk_fsylc = entity.zdfhqk_fsylc;
                dbEntity.zdfhqk_bcylc = entity.zdfhqk_bcylc;
                dbEntity.xzzxx = entity.xzzxx;
                dbEntity.hkdxx = entity.hkdxx;
                dbEntity.qtyljgzrmc = entity.qtyljgzrmc;
                #endregion
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                #region 病案首页更新
                dbEntity.zid = entity.zid;
                dbEntity.tmh = entity.tmh;
                dbEntity.bah = entity.bah;
                dbEntity.zyh = entity.zyh;
                dbEntity.fylb = entity.fylb;
                dbEntity.brxm = entity.brxm;
                dbEntity.byzg = entity.byzg;
                dbEntity.xb = entity.xb;
                if (!string.IsNullOrWhiteSpace(entity.csny))
                {
                    dbEntity.csny = Convert.ToDateTime(entity.csny);
                }
                dbEntity.nl = entity.nl;
                dbEntity.zy = entity.zy;
                dbEntity.hyzk = entity.hyzk;
                dbEntity.csdz = entity.csdz;
                dbEntity.mz = entity.mz;
                dbEntity.gj = entity.gj;
                dbEntity.sfzhm = entity.sfzhm;
                dbEntity.dwmc = entity.dwmc;
                dbEntity.dwdh = entity.dwdh;
                dbEntity.dwyb = entity.dwyb;
                dbEntity.brfb = entity.brfb;
                dbEntity.hkdz = entity.hkdz;
                dbEntity.hkyb = entity.hkyb;
                dbEntity.lxr = entity.lxr;
                dbEntity.gx = entity.gx;
                dbEntity.lxdz = entity.lxdz;
                dbEntity.lxdh = entity.lxdh;
                dbEntity.ryks = entity.ryks;
                dbEntity.ryrq = entity.ryrq;
                dbEntity.rysj = entity.rysj;
                dbEntity.rytj = entity.rytj;
                dbEntity.rybq = entity.rybq;
                dbEntity.cyks = entity.cyks;
                dbEntity.cyrq = entity.cyrq;
                dbEntity.cysj = entity.cysj;
                dbEntity.mzzd = entity.mzzd;
                dbEntity.mzys = entity.mzys;
                dbEntity.ryzd = entity.ryzd;
                dbEntity.ryys = entity.ryys;
                if (!string.IsNullOrWhiteSpace(entity.qzrq))
                {
                    dbEntity.qzrq = Convert.ToDateTime(entity.qzrq);
                }
                dbEntity.cydz = entity.cydz;
                dbEntity.cyqk = entity.cyqk;
                dbEntity.cyzd = entity.cyzd;
                dbEntity.lx = entity.lx;
                dbEntity.mebm = entity.mebm;
                dbEntity.bfz = entity.bfz;
                dbEntity.yngr = entity.yngr;
                dbEntity.blzd = entity.blzd;
                dbEntity.blh = entity.blh;
                dbEntity.gmyw = entity.gmyw;
                dbEntity.szqx = entity.szqx;
                dbEntity.xx = entity.xx;
                dbEntity.sj = entity.sj;
                dbEntity.sx = entity.sx;
                dbEntity.mzzl = entity.mzzl;
                dbEntity.rcf = entity.rcf;
                dbEntity.zllx = entity.zllx;
                dbEntity.lcblf = entity.lcblf;
                dbEntity.lcsjf = entity.lcsjf;
                dbEntity.qjcs = entity.qjcs;
                dbEntity.cgcs = entity.cgcs;
                dbEntity.qjff = entity.qjff;
                dbEntity.tjh = entity.tjh;
                dbEntity.sy = entity.sy;
                dbEntity.sjba = entity.sjba;
                dbEntity.kyba = entity.kyba;
                dbEntity.bazl = entity.bazl;
                dbEntity.swsj = entity.swsj;
                dbEntity.eflh = entity.eflh;
                dbEntity.kzr = entity.kzr;
                dbEntity.zzys = entity.zzys;
                dbEntity.sxys = entity.sxys;
                dbEntity.bazlr = entity.bazlr;
                dbEntity.bmy = entity.bmy;
                dbEntity.ssrq = entity.ssrq;
                dbEntity.ssbm = entity.ssbm;
                dbEntity.sslb = entity.sslb;
                dbEntity.mzfs = entity.mzfs;
                dbEntity.mzysr = entity.mzysr;
                dbEntity.qklb = entity.qklb;
                dbEntity.qkyh = entity.qkyh;
                dbEntity.ssys = entity.ssys;
                dbEntity.sqhf = entity.sqhf;
                dbEntity.zyf = entity.zyf;
                dbEntity.xyf = entity.xyf;
                dbEntity.zcyf = entity.zcyf;
                dbEntity.zcayf = entity.zcayf;
                dbEntity.jyf = entity.jyf;
                dbEntity.zlf = entity.zlf;
                dbEntity.clf = entity.clf;
                dbEntity.fsf = entity.fsf;
                dbEntity.ssf = entity.ssf;
                dbEntity.hyf = entity.hyf;
                dbEntity.sxf = entity.sxf;
                dbEntity.syf = entity.syf;
                dbEntity.jsf = entity.jsf;
                dbEntity.bbf = entity.bbf;
                dbEntity.qtf = entity.qtf;
                dbEntity.zfy = entity.zfy;
                dbEntity.srrq = entity.srrq;
                dbEntity.czydm = entity.czydm;
                dbEntity.bj = entity.bj;
                dbEntity.gdbj = entity.gdbj;
                dbEntity.year = entity.year;
                dbEntity.month = entity.month;
                dbEntity.id = entity.id;
                dbEntity.grbw = entity.grbw;
                dbEntity.lcyfs = entity.lcyfs;
                dbEntity.rycs = entity.rycs;
                dbEntity.hbsag = entity.hbsag;
                dbEntity.hcv = entity.hcv;
                dbEntity.hiv = entity.hiv;
                dbEntity.zrfys = entity.zrfys;
                dbEntity.jxys = entity.jxys;
                dbEntity.yjssxys = entity.yjssxys;
                dbEntity.zkys = entity.zkys;
                dbEntity.zkhs = entity.zkhs;
                dbEntity.barq = entity.barq;
                dbEntity.dyl = entity.dyl;
                dbEntity.sz = entity.sz;
                dbEntity.rh = entity.rh;
                dbEntity.sxfy = entity.sxfy;
                dbEntity.sxpz = entity.sxpz;
                dbEntity.sxsl = entity.sxsl;
                dbEntity.hlf = entity.hlf;
                dbEntity.mzf = entity.mzf;
                dbEntity.yef = entity.yef;
                dbEntity.ssys1 = entity.ssys1;
                dbEntity.ssys2 = entity.ssys2;
                dbEntity.sxdw = entity.sxdw;
                dbEntity.grbyjc = entity.grbyjc;
                dbEntity.grbyzd = entity.grbyzd;
                dbEntity.sx9jc = entity.sx9jc;
                dbEntity.kss1 = entity.kss1;
                dbEntity.kss2 = entity.kss2;
                dbEntity.kss3 = entity.kss3;
                dbEntity.tsjc_ct = entity.tsjc_ct;
                dbEntity.tsjc_mri = entity.tsjc_mri;
                dbEntity.tsjc_dpl = entity.tsjc_dpl;
                dbEntity.tsjc4 = entity.tsjc4;
                dbEntity.tsjc5 = entity.tsjc5;
                dbEntity.tsjc6 = entity.tsjc6;
                dbEntity.ba_jy = entity.ba_jy;
                dbEntity.sx_hxb = entity.sx_hxb;
                dbEntity.sx_xxb = entity.sx_xxb;
                dbEntity.sx_xj = entity.sx_xj;
                dbEntity.sx_qx = entity.sx_qx;
                dbEntity.sx_qt = entity.sx_qt;
                dbEntity.dzbl_id = entity.dzbl_id;
                dbEntity.kzrmc = entity.kzrmc;
                dbEntity.zyysmc = entity.zyysmc;
                dbEntity.zzysmc = entity.zzysmc;
                dbEntity.zrysmc = entity.zrysmc;
                dbEntity.ryzdmc = entity.ryzdmc;
                dbEntity.cyzdmc = entity.cyzdmc;
                dbEntity.csd_s = entity.csd_s;
                dbEntity.csd_x = entity.csd_x;
                dbEntity.sfbl = entity.sfbl;
                dbEntity.szqxz = entity.szqxz;
                dbEntity.szqxy = entity.szqxy;
                dbEntity.szqxn = entity.szqxn;
                dbEntity.qtzd = entity.qtzd;
                dbEntity.wbys = entity.wbys;
                dbEntity.ssmc = entity.ssmc;
                dbEntity.rybs = entity.rybs;
                dbEntity.cybs = entity.cybs;
                dbEntity.zkks = entity.zkks;
                dbEntity.nldw = entity.nldw;
                dbEntity.bwcs = entity.bwcs;
                dbEntity.bzcs = entity.bzcs;
                dbEntity.pbcs = entity.pbcs;
                dbEntity.czy = entity.czy;
                dbEntity.zyzt = entity.zyzt;
                dbEntity.ryyy = entity.ryyy;
                dbEntity.IDC = entity.IDC;
                dbEntity.IDC1 = entity.IDC1;
                dbEntity.ztjs = entity.ztjs;
                dbEntity.cyzt = entity.cyzt;
                dbEntity.qtzd1 = entity.qtzd1;
                dbEntity.qtzd2 = entity.qtzd2;
                dbEntity.qtzd3 = entity.qtzd3;
                dbEntity.qtzd4 = entity.qtzd4;
                dbEntity.jszlsq = entity.jszlsq;
                dbEntity.hblx = entity.hblx;
                dbEntity.qfyy = entity.qfyy;
                dbEntity.mzycy = entity.mzycy;
                dbEntity.lcybl = entity.lcybl;
                dbEntity.fsybl = entity.fsybl;
                dbEntity.dbr = entity.dbr;
                dbEntity.dbrdw = entity.dbrdw;
                dbEntity.lxdhdbr = entity.lxdhdbr;
                dbEntity.hljb = entity.hljb;
                dbEntity.dqzt = entity.dqzt;
                dbEntity.zdmc1 = entity.zdmc1;
                dbEntity.zdmc2 = entity.zdmc2;
                dbEntity.zdmc3 = entity.zdmc3;
                dbEntity.zdmc4 = entity.zdmc4;
                dbEntity.zdmc5 = entity.zdmc5;
                dbEntity.zydj_id = entity.zydj_id;
                dbEntity.zrys = entity.zrys;
                dbEntity.tz_xsecs = entity.tz_xsecs;
                dbEntity.tz_xsery = entity.tz_xsery;
                dbEntity.cs_t = entity.cs_t;
                dbEntity.zzjgdm = entity.zzjgdm;
                dbEntity.xzdz = entity.xzdz;
                dbEntity.zzyb = entity.zzyb;
                dbEntity.mzzd_icd = entity.mzzd_icd;
                dbEntity.blzd_icd = entity.blzd_icd;
                dbEntity.wbyy = entity.wbyy;
                dbEntity.bch_ry = entity.bch_ry;
                dbEntity.bch_cy = entity.bch_cy;
                dbEntity.zrhsmc = entity.zrhsmc;
                dbEntity.zkrq = entity.zkrq;
                dbEntity.ssjb = entity.ssjb;
                dbEntity.dj_qkyh = entity.dj_qkyh;
                dbEntity.jsjg_yl = entity.jsjg_yl;
                dbEntity.jsjg_wsy = entity.jsjg_wsy;
                dbEntity.zczymd = entity.zczymd;
                dbEntity.zfje = entity.zfje;
                dbEntity.zczy_jh = entity.zczy_jh;
                dbEntity.sm = entity.sm;
                dbEntity.hmsj_ryq = entity.hmsj_ryq;
                dbEntity.hmsj_ryh = entity.hmsj_ryh;
                dbEntity.lyfs = entity.lyfs;
                dbEntity.zrhs = entity.zrhs;
                dbEntity.wbyy_icd = entity.wbyy_icd;
                dbEntity.jkkh = entity.jkkh;
                dbEntity.zyjh = entity.zyjh;
                dbEntity.mu = entity.mu;
                dbEntity.hkdt = entity.hkdt;
                dbEntity.hkdx = entity.hkdx;
                dbEntity.xzdt = entity.xzdt;
                dbEntity.xzdx = entity.xzdx;
                dbEntity.xzz_lxdh = entity.xzz_lxdh;
                dbEntity.xzz_yb = entity.xzz_yb;
                dbEntity.hmsj_ryq_t = entity.hmsj_ryq_t;
                dbEntity.hmsj_ryq_x = entity.hmsj_ryq_x;
                dbEntity.hmsj_ryq_f = entity.hmsj_ryq_f;
                dbEntity.hmsj_ryh_t = entity.hmsj_ryh_t;
                dbEntity.hmsj_ryh_x = entity.hmsj_ryh_x;
                dbEntity.hmsj_ryh_f = entity.hmsj_ryh_f;
                dbEntity.kjyf_sybz = entity.kjyf_sybz;
                dbEntity.kjyf_sycxsj = entity.kjyf_sycxsj;
                dbEntity.lhyy = entity.lhyy;
                dbEntity.byxtzdry = entity.byxtzdry;
                dbEntity.byjscrysj = entity.byjscrysj;
                dbEntity.zrmc = entity.zrmc;
                dbEntity.lclj_jrbz = entity.lclj_jrbz;
                dbEntity.lclj_wcbz = entity.lclj_wcbz;
                dbEntity.lclj_tcyy = entity.lclj_tcyy;
                dbEntity.lclj_bybz = entity.lclj_bybz;
                dbEntity.lclj_byyy = entity.lclj_byyy;
                dbEntity.lclj_mc = entity.lclj_mc;
                dbEntity.bwbz_gzbz = entity.bwbz_gzbz;
                dbEntity.zzlys = entity.zzlys;
                dbEntity.tnmfq = entity.tnmfq;
                dbEntity.jg_s = entity.jg_s;
                dbEntity.jg_qs = entity.jg_qs;
                dbEntity.zy_zygz = entity.zy_zygz;
                dbEntity.zy_rzzzbf = entity.zy_rzzzbf;
                dbEntity.zy_syzcy = entity.zy_syzcy;
                dbEntity.zy_rsmdsc = entity.zy_rsmdsc;
                dbEntity.zy_rscx = entity.zy_rscx;
                dbEntity.zy_cxl = entity.zy_cxl;
                dbEntity.zy_xsr_ch = entity.zy_xsr_ch;
                dbEntity.zy_xsr_pku = entity.zy_xsr_pku;
                dbEntity.zy_xsr_cah = entity.zy_xsr_cah;
                dbEntity.zy_xsr_g6pd = entity.zy_xsr_g6pd;
                dbEntity.zy_xsr_tl = entity.zy_xsr_tl;
                dbEntity.zy_yygrqk = entity.zy_yygrqk;
                dbEntity.zy_grbw = entity.zy_grbw;
                dbEntity.zy_grmc = entity.zy_grmc;
                dbEntity.basy_bb = entity.basy_bb;
                dbEntity.xzds = entity.xzds;
                dbEntity.zyzdzyhzqk = entity.zyzdzyhzqk;
                dbEntity.zdfhqk_mzycy = entity.zdfhqk_mzycy;
                dbEntity.zdfhqk_ryycy = entity.zdfhqk_ryycy;
                dbEntity.zdfhqk_sqysh = entity.zdfhqk_sqysh;
                dbEntity.zdfhqk_lcybl = entity.zdfhqk_lcybl;
                dbEntity.zdfhqk_fsybl = entity.zdfhqk_fsybl;
                dbEntity.lclj_gl = entity.lclj_gl;
                dbEntity.nl_y = entity.nl_y;
                dbEntity.tyzbzry = entity.tyzbzry;
                dbEntity.jgts = entity.jgts;
                dbEntity.qtzf = entity.qtzf;
                dbEntity.ybylfwf = entity.ybylfwf;
                dbEntity.ybzlczf = entity.ybzlczf;
                dbEntity.blzdf = entity.blzdf;
                dbEntity.syszdf = entity.syszdf;
                dbEntity.yxxzdf = entity.yxxzdf;
                dbEntity.lczdxmf = entity.lczdxmf;
                dbEntity.fsszlxmf = entity.fsszlxmf;
                dbEntity.lcwlzlf = entity.lcwlzlf;
                dbEntity.sszlf = entity.sszlf;
                dbEntity.kff = entity.kff;
                dbEntity.zyzlf = entity.zyzlf;
                dbEntity.kjywfy = entity.kjywfy;
                dbEntity.zcyf_zcy = entity.zcyf_zcy;
                dbEntity.xf = entity.xf;
                dbEntity.bdblzpf = entity.bdblzpf;
                dbEntity.qdblzpf = entity.qdblzpf;
                dbEntity.nxyzlzpf = entity.nxyzlzpf;
                dbEntity.xbyzlzpf = entity.xbyzlzpf;
                dbEntity.jcyycxyyclf = entity.jcyycxyyclf;
                dbEntity.zlyycxyyclf = entity.zlyycxyyclf;
                dbEntity.ssyycxyyclf = entity.ssyycxyyclf;
                dbEntity.qj_cgcs = entity.qj_cgcs;
                dbEntity.zzys_ys = entity.zzys_ys;
                dbEntity.ynbl_bz = entity.ynbl_bz;
                dbEntity.jg_zyzj_sybz = entity.jg_zyzj_sybz;
                dbEntity.bzsh = entity.bzsh;
                dbEntity.zyzlsb_sybz = entity.zyzlsb_sybz;
                dbEntity.zyzljs_sybz = entity.zyzljs_sybz;
                dbEntity.kjyw_syqk = entity.kjyw_syqk;
                dbEntity.yf_fyyw = entity.yf_fyyw;
                dbEntity.lcbx = entity.lcbx;
                dbEntity.qtyljgmc = entity.qtyljgmc;
                dbEntity.yymc = entity.yymc;
                dbEntity.ylfffs = entity.ylfffs;
                dbEntity.hkdz_s = entity.hkdz_s;
                dbEntity.rybf = entity.rybf;
                dbEntity.cybf = entity.cybf;
                dbEntity.sjzyts = entity.sjzyts;
                dbEntity.mzzd_jbmc_zy = entity.mzzd_jbmc_zy;
                dbEntity.mzzd_jbbm_zy = entity.mzzd_jbbm_zy;
                dbEntity.wbyybm = entity.wbyybm;
                dbEntity.ywgm = entity.ywgm;
                dbEntity.bzlzhzf_zy = entity.bzlzhzf_zy;
                dbEntity.zyzd_zyfw = entity.zyzd_zyfw;
                dbEntity.zyzl_zyfw = entity.zyzl_zyfw;
                dbEntity.zywz_zyfw = entity.zywz_zyfw;
                dbEntity.zygs_zyfw = entity.zygs_zyfw;
                dbEntity.zcyjf_zyfw = entity.zcyjf_zyfw;
                dbEntity.tnzl_zyfw = entity.tnzl_zyfw;
                dbEntity.gczl_zyfw = entity.gczl_zyfw;
                dbEntity.tszl_zyfw = entity.tszl_zyfw;
                dbEntity.zyqt_zyfw = entity.zyqt_zyfw;
                dbEntity.zytstpjg_zyfw = entity.zytstpjg_zyfw;
                dbEntity.bzss_zyfw = entity.bzss_zyfw;
                dbEntity.yljgzyzjf_zyf = entity.yljgzyzjf_zyf;
                dbEntity.bzlzf_zy = entity.bzlzf_zy;
                dbEntity.ryzd_jbbm = entity.ryzd_jbbm;
                dbEntity.qtfy = entity.qtfy;
                dbEntity.shcd = entity.shcd;
                dbEntity.ddzc_yy = entity.ddzc_yy;
                dbEntity.zdfhqk_fsylc = entity.zdfhqk_fsylc;
                dbEntity.zdfhqk_bcylc = entity.zdfhqk_bcylc;
                dbEntity.xzzxx = entity.xzzxx;
                dbEntity.hkdxx = entity.hkdxx;
                dbEntity.qtyljgzrmc = entity.qtyljgzrmc;
                dbEntity.OrganizeId = entity.OrganizeId;
                dbEntity.CreatorCode = entity.CreatorCode;
                dbEntity.zt = "1";
                dbEntity.CreateTime = DateTime.Now;
                #endregion
                dbEntity.Create();
                this.Insert(dbEntity);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.zid == keyValue);
        }

    }
}