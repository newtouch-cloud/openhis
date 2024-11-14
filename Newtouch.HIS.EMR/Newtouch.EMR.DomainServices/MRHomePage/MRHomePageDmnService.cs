using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.EMR.Domain.BusinessObjects;
using Newtouch.EMR.Domain.DTO.InputDto;
using Newtouch.EMR.Domain.DTO.OutputDto.MRHomePage;
using Newtouch.EMR.Domain.DTO.OutputDto.MRUpload;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects.MRHomePage;
using Newtouch.EMR.Infrastructure;
using Newtouch.EMR.Infrastructure.EnumMR;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.DomainServices.MRHomePage
{
    public class MRHomePageDmnService : DmnServiceBase, IMRHomePageDmnService
    {
        private readonly IBlmblbRepo _blmblbRepo;
        private readonly IMrbasyzdRepo _MrbasyzdRepo;
        private readonly IMrbasyssRepo _MrbasyssRepo;
        private readonly IMrbasyRepo _MrbasyRepo;
        private readonly IMrbasyrelcodeRepo _MrbasyrelcodeRepo;
        private readonly ICommonDmnService _CommonDmnService;
        private readonly IZymeddocsrelationRepo _ZymeddocsrelationRepo;

        public MRHomePageDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<BasyVO> PatMainList(Pagination pagination, string orgId, string cyksrq, string cyjsrq, string bazt, string bq, string keyword, string cyts)
        {
            var mrlist = _MrbasyRepo.FindList(p => p.OrganizeId == orgId && p.zt == "1", pagination);
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));

            string sql = @"select a.Id,b.ItemName YLFKFS,a.JKKH,a.ZYCS,a.BAH,a.ZYH,a.XM,a.XB,a.NL,a.RYTJ,convert(varchar(20),a.RYSJ,120)RYSJ,convert(varchar(20),a.CYSJ,120)CYSJ,
c.Name RYKB,d.Name CYKB,a.MZZD,a.bazt,datediff(dd,a.CYSJ,getdate()) cyts
from mr_basy a with(nolock)
left join [dbo].[mr_dic_common] b with(nolock) on a.YLFKFS=ItemCode and b.RlueCode ='YLFKFS' and a.organizeid=b.organizeid and b.zt='1'
left join [NewtouchHIS_Base].[dbo].[V_S_Sys_Department] c with(nolock) on a.RYKB=c.Code and a.organizeid=c.organizeid and c.zt='1'
left join [NewtouchHIS_Base].[dbo].[V_S_Sys_Department] d with(nolock) on a.CYKB=d.Code and a.organizeid=d.organizeid and d.zt='1'
where a.organizeid=@orgId and a.zt='1'
";
            if (!string.IsNullOrWhiteSpace(cyksrq) && !string.IsNullOrWhiteSpace(cyjsrq))
            {
                sql += " and a.CYSJ >= @ksrq and a.CYSJ < @jsrq ";
                string ksrq = Convert.ToDateTime(cyksrq).ToString("yyyy-MM-dd");
                string jsrq = Convert.ToDateTime(cyjsrq).AddDays(1).ToString("yyyy-MM-dd");
                para.Add(new SqlParameter("@ksrq", ksrq));
                para.Add(new SqlParameter("@jsrq", jsrq));
            }
            if (!string.IsNullOrWhiteSpace(bazt))
            {
                sql += " and a.bazt=@bazt ";
                para.Add(new SqlParameter("@bazt", bazt));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (charindex(@keyword,a.XM)>0 or a.ZYH=@keyword or a.BAH=@keyword  )";
                para.Add(new SqlParameter("@keyword", keyword));
            }
            if (!string.IsNullOrWhiteSpace(cyts))
            {
                sql += " and datediff(dd,a.CYSJ,getdate())>=@cyts";
                para.Add(new SqlParameter("@cyts", cyts));
            }
            return this.QueryWithPage<BasyVO>(sql, pagination, para.ToArray());
        }

        public BasyVO GetMainRecord(string orgId, string mainId)
        {
            var mrbasy = _MrbasyRepo.FindEntity(p => p.Id == mainId && p.OrganizeId == orgId && p.zt == "1");
            //每次修改都会自动同步出区日期与住院天数
            string esql = @"update basy 
                               set basy.CYSJ = brjbxx.cqrq,
                                   basy.CYSJS = SUBSTRING(CONVERT(varchar(100),brjbxx.cqrq,21),11,3),
                                   basy.SJZYTS = dbo.get_zyts(brjbxx.ryrq,brjbxx.cqrq)
                              from Newtouch_CIS.dbo.zy_brxxk as brjbxx,Newtouch_EMR.dbo.mr_basy as basy
                             where brjbxx.ZYH = @zyh 
                               and brjbxx.OrganizeId = @orgId
                               and brjbxx.zyh = basy.ZYH
                               and brjbxx.OrganizeId = basy.OrganizeId";
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@zyh", mrbasy.ZYH));
            parameter.Add(new SqlParameter("@orgId", orgId));
            this.ExecuteSqlCommand(esql, parameter.ToArray());

            BasyVO ety = new BasyVO();
            var para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));
            para.Add(new SqlParameter("@mainId", mainId));

            //            string sql = @" SELECT a.[Id],a.[YLFKFS],a.[JKKH],a.[ZYCS],a.[BAH],a.[PATID],a.[ZYH],a.[XM],a.[XB],convert(varchar(10),CSRQ,120)[CSRQ],a.[NL],a.[GJ],a.[BZYZSNL],a.[XSECSTZ],a.[XSERYTZ]
            //,a.[CSD],a.[GG],a.[MZ],a.[SFZH],a.[ZY],a.[HY],a.[XZZ],a.[DH],a.[XZZYB],a.[HKDZ],a.[HKDYB],a.[GZDWJDZ],a.[DWDH],a.[DWYB],a.[LXRXM],a.[GX],a.[LXRDZ]
            //,a.[LXRDH],a.[RYTJ],convert(varchar(20),RYSJ,120)[RYSJ],a.[RYSJS],a.[RYKB],a.[RYBF],a.[ZKKB],convert(varchar(20),CYSJ,120)[CYSJ],a.[CYSJS],a.[CYKB],a.[CYBF],a.[SJZYTS],a.[MZZD],a.[MZZDDM]
            //,a.[BWHBZ],a.[RYZD],a.[RYZDDM],a.[SSLCLJ]
            //,a.[QJCS],a.[QJCGCS],a.[QZRQ],a.[ZQSS],a.[BRLY],a.[WBYY],a.[H23],a.[BLZD],a.[BLZDDM],a.[BLH],a.[YWGM],a.[GMYW],a.[SWHZSJ],a.[XX],a.[RH],a.[KZR]
            //,a.[ZRYS],a.[ZZYS],isnull(a.[ZYYS],staff.name)ZYYS,a.[ZRHS],a.[JXYS],a.[SXYS],a.[BMY],a.[BAZL],a.[ZKYS],a.[ZKHS],convert(varchar(10),ZKRQ,120)[ZKRQ],a.[LYFS],a.[YZZY_YLJG],a.[WSY_YLJG],a.[SFZZYJH]
            //,a.[MD],a.[RYQ_T],a.[RYQ_XS],a.[RYQ_F],a.[RYH_T],a.[RYH_XS],a.[RYH_F],a.[zt],a.[CreateTime],a.[CreatorCode],a.[LastModifyTime],a.[LastModifierCode]
            //,a.[LCLJGL],a.[LCBZGL],a.[BZGLFL],a.[BQFX],a.[OrganizeId],a.[CSD_SN],a.[CSD_SI],a.[CSD_QX],a.[CSD_JD],a.[XZZ_SN],a.[XZZ_SI],a.[XZZ_QX],a.[XZZ_JD]
            //,a.[HKDZ_SN],a.[HKDZ_SI],a.[HKDZ_QX],a.[HKDZ_JD],a.[LXRDZ_SN],a.[LXRDZ_SI],a.[LXRDZ_QX],a.[LXRDZ_JD],a.[RYCH],a.[CYCH],a.[bazt]
            //,a.[ZFY],a.[ZFJE],a.[QTZF],a.[YLFUF],a.[BZLZF],a.[ZYBLZHZF],a.[ZLCZF],a.[HLF],a.[QTFY],a.[BLZDF],a.[SYSZDF],a.[YXXZDF],a.[LCZDXMF],a.[FSSZLXMF],a.[WLZLF]
            //,a.[SSZLF],a.[MAF],a.[SSF],a.[KFF],a.[ZYZLF],a.[ZYZL],a.[ZYWZ],a.[ZYGS],a.[ZCYJF],a.[ZYTNZL],a.[ZYGCZL],a.[ZYTSZL],a.[ZYQT],a.[ZYTSTPJG],a.[BZSS]
            //,a.[XYF],a.[KJYWF],a.[ZCYF],a.[ZYZJF],a.[ZCYF1],a.[XF],a.[BDBLZPF],a.[QDBLZPF],a.[NXYZLZPF],a.[XBYZLZPF],a.[HCYYCLF],a.[YYCLF],a.[YCXYYCLF],a.[QTF],
            //b.[YLFKFS] R_YLFKFS,b.[XB] R_XB,b.[GJ] R_GJ,b.[MZ] R_MZ,b.[ZY] R_ZY,b.[HY] R_HY,b.[GX] R_GX,b.[RYTJ] R_RYTJ,b.[RYKB] R_RYKB,b.[RYBF] R_RYBF
            //,b.[ZKKB] R_ZKKB,b.[CYBF] R_CYBF,b.[CYKB] R_CYKB,b.[BLZDDM] R_BLZDDM,b.[BRLY] R_BRLY,b.[YWGM] R_YWGM,b.[XX] R_XX,b.[RH] R_RH,b.[KZR] R_KZR,b.[ZRYS] R_ZRYS,b.[ZZYS] R_ZZYS,isnull(b.[ZYYS],doctor) R_ZYYS,b.[ZRHS] R_ZRHS,b.[JXYS] R_JXYS,b.[SXYS] R_SXYS 
            //,b.[BMY] R_BMY,b.[BAZL] R_BAZL,b.[ZKYS] R_ZKYS,b.[ZKHS] R_ZKHS,b.[LYFS] R_LYFS,b.[SFZZYJH] R_SFZZYJH,b.[BQFX] R_BQFX
            //,a.[HXB],a.[XXB],a.[XJ],a.[QX],a.[ZTXHS],a.[BDB],a.[LCD],a.[QT],a.[SXFY],a.[SZ],a.[SZQXZ],a.[SZQXY],a.[SZQXN],a.[WCLCLJ],a.[TCYY],a.[SFBY],a.[BYYY]
            //,a.[CT],a.[PETCT],a.[SYCT],a.[BC],a.[XP],a.[CSXDT],a.[MRI],a.[TWSJC],a.[SYCXSJ],a.[LHYY]
            //,a.[YYGRQK],a.[YYGRSSXG],a.[YYGRSFQRXG],a.[KJYWSYQK],a.[KJYWMC1],a.[KJYWMC2],a.[KJYWMC3],a.[KJYWMC4],a.[KJYWMC5],a.[KJYWMC6]
            //,a.[SFFSYC],a.[SFZYQJFS],a.[YCFQ],a.[SYFY],a.[YFFYDYW],a.[SYLCBX],a.[ZYSFDDHZC],a.[ZYDDHZCDCD],a.[DDHZCDYY],a.[ZYQJSTYY],a.[LYTXNSDZ],a.[DWFZR],a.[TJFZR],a.[TBR],a.[LXDH],a.[SJ]
            //,convert(varchar(10),a.[BCRQ],25) BCRQ,a.BZYYSNL,a.QTYLJGZR,b.ZZYS1 R_ZZYS1,a.ZZYS1,a.[ZZYS]
            //  FROM [dbo].[mr_basy] a with(nolock)  
            //left join [dbo].[mr_basy_rel_code] b with(nolock) on a.organizeid=b.organizeid and a.id=b.syid and a.bah=b.bah 
            // left join NewtouchHIS_Sett..zy_brjbxx br on a.zyh=br.zyh and br.zt=1 and br.OrganizeId=@orgId
            //left join NewtouchHIS_Base..Sys_Staff staff on br.doctor=staff.gh and staff.OrganizeId=@orgId and staff.zt=1
            //  where a.zt='1' and a.OrganizeId=@orgId and a.id=@mainId ";

            string sql = @"exec usp_getbrxx @orgId=@orgId,@mainId=@mainId ";

            ety = this.FirstOrDefault<BasyVO>(sql, para.ToArray());
            return ety;
        }

        public BasyVO GetMainRecordbybrjbxx(string orgId, string zyh)
        {
            var mrbasy = _MrbasyRepo.FindEntity(p => p.ZYH == zyh && p.OrganizeId == orgId && p.zt == "1");
            if (mrbasy != null)
            {
                string usql = @"update basy set 
                                       basy.ZYCS = (select NewtouchHIS_Sett.dbo.get_zycs(@zyh,'',@orgId))
                                  from Newtouch_EMR.dbo.mr_basy as basy
                                 where basy.ZYH = @zyh
                                   and basy.OrganizeId = @orgId
                                   and basy.zt = '1'";
                var para = new List<SqlParameter>();
                para.Add(new SqlParameter("@zyh", zyh));
                para.Add(new SqlParameter("@orgId", orgId));
                this.ExecuteSqlCommand(usql, para.ToArray());

                return GetMainRecord(orgId, mrbasy.Id);
            }

            string sql = @"SELECT [USERNAME],[YLFKFS],[JKKH],a.[ZYCS],a.[PATID],a.[ZYH],a.[XM],a.[XB],[CSRQ],a.[NL],a.[GJ],[BZYZSNL],a.[MZ],[SFZH]
,a.[ZY],[R_ZY],[R_HY],[XZZ],[DH],[XZZYB],[HKDZ],[GZDWJDZ],[LXRXM],[GX],a.[LXRDZ],a.[LXRDH],a.[RYTJ],[RYSJ],[RYSJS],[RYKB],[RYBF]
,[CYSJ],[CYSJS],[R_CYKB],[R_CYBF],[SJZYTS],[MZZD],[MZZDDM],a.[RYZD],[RYZDDM],a.[OrganizeId],[CSD_SN],[CSD_SI],[CSD_QX],[CSD_JD],[XZZ_SN]
,[XZZ_SI],[XZZ_QX],[XZZ_JD],[HKDZ_SN],[HKDZ_SI],[HKDZ_QX],[HKDZ_JD],[LXRDZ_SN],[LXRDZ_SI],[LXRDZ_QX],[LXRDZ_JD]
,[RYCH],[CYCH],[R_MZ],[R_GJ],a.[HY] ,bq.kzr_gh KZR,zrys.ysmc ZRYS,zz.ysmc ZZYS,zyys.ysmc ZYYS,zrhs.ysmc ZRHS,zzys.ysmc ZZYS1,
bq.kzr_gh R_KZR,zrys.ysmc R_ZRYS,zz.ysmc R_ZZYS,zyys.ysmc R_ZYYS,zrhs.ysmc R_ZRHS,zzys.ysmc R_ZZYS1,a.R_GX,a.[JG] as [GG]
FROM [Newtouch_EMR].[dbo].[V_HIS_InpPatInfoForBA] a 
left join NewtouchHIS_Sett..zy_brjbxx br on a.zyh=br.zyh and br.zt=1 and br.OrganizeId=@orgId
left join NewtouchHIS_Base..xt_bq bq on bq.bqmc=a.RYBF and bq.zt=1 and bq.OrganizeId=@orgId
left join NewtouchHIS_Base..Sys_Staff staff on br.doctor=staff.gh and staff.OrganizeId=@orgId and staff.zt=1
left join Newtouch_CIS..zy_PatDocInfo zrys on a.ZYH=zrys.zyh and zrys.OrganizeId=@orgId and zrys.zt=1 and zrys.TypeName='主任医生'
left join Newtouch_CIS..zy_PatDocInfo zz on a.ZYH=zz.zyh and zz.OrganizeId=@orgId and zrys.zt=1 and zz.TypeName='主诊医生'
left join Newtouch_CIS..zy_PatDocInfo zzys on a.ZYH=zzys.zyh and zzys.OrganizeId=@orgId and zzys.zt=1 and zzys.TypeName='主治医生'
left join Newtouch_CIS..zy_PatDocInfo zrhs on a.ZYH=zrhs.zyh and zrhs.OrganizeId=@orgId and zrhs.zt=1 and zrhs.TypeName='责任护士'
left join Newtouch_CIS..zy_PatDocInfo zyys on a.ZYH=zyys.zyh and zyys.OrganizeId=@orgId and zyys.zt=1 and zyys.TypeName='住院医生'
where a.organizeid=@orgId  and a.zyh=@zyh";

            BasyVO vo = this.FindList<BasyVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@zyh",zyh)
            }).FirstOrDefault();

            if (vo != null)
            {
                vo = GetMainRecordZyFeeDetail(vo, orgId, zyh);
                var transInfo = GetInpPatTransferInfo(zyh, orgId);
                if (transInfo != null && transInfo.Count > 0)
                {
                    foreach (var item in transInfo)
                    {
                        if (item.num == 1)
                        {
                            vo.RYBF = string.IsNullOrWhiteSpace(item.WardName) == true ? vo.RYBF : item.WardName;
                            vo.RYKB = string.IsNullOrWhiteSpace(item.DeptName) == true ? vo.RYKB : item.DeptName;
                            vo.RYCH = string.IsNullOrWhiteSpace(item.BedNo) == true ? vo.RYCH : item.BedNo;

                            vo.R_RYBF = string.IsNullOrWhiteSpace(item.WardCode) == true ? vo.R_RYBF : item.WardCode;
                            vo.R_RYKB = string.IsNullOrWhiteSpace(item.DeptCode) == true ? vo.R_RYKB : item.DeptCode;

                        }
                        else if (item.Status == ((int)EnumCwjlzt.Zq).ToString() && string.IsNullOrWhiteSpace(vo.ZKKB) && !string.IsNullOrWhiteSpace(item.TransDeptCode))
                        {
                            vo.R_ZKKB = string.IsNullOrWhiteSpace(item.TransDeptCode) == true ? vo.R_ZKKB : item.TransDeptCode;
                            var ks = _CommonDmnService.GetDeptList(orgId, vo.R_ZKKB).FirstOrDefault();
                            if (ks != null)
                            {
                                vo.ZKKB = ks.Name;
                            }
                        }
                        //多个转科 处理
                        else if (item.Status == ((int)EnumCwjlzt.Zq).ToString() && !string.IsNullOrWhiteSpace(vo.ZKKB) && !string.IsNullOrWhiteSpace(item.TransDeptCode))
                        {
                            vo.ZKKB = vo.ZKKB + "→" + item.TransDeptName;
                            vo.R_ZKKB = vo.R_ZKKB + "→" + item.TransDeptCode;
                        }

                    }
                }

                if (!string.IsNullOrWhiteSpace(vo.R_CYKB))
                {
                    var cykb = _CommonDmnService.GetDeptList(orgId, vo.R_CYKB).FirstOrDefault();
                    if (cykb != null)
                    {
                        vo.CYKB = cykb.Name;
                        vo.R_CYKB = cykb.Code;
                    }
                }
                if (!string.IsNullOrWhiteSpace(vo.R_CYBF))
                {
                    var cybq = _CommonDmnService.GetWardList(orgId, vo.R_CYBF).FirstOrDefault();
                    if (cybq != null)
                    {
                        vo.CYBF = cybq.Name;
                        vo.R_CYBF = cybq.Code;
                    }
                }
                if (!string.IsNullOrWhiteSpace(vo.R_ZY))
                {
                    var zy = _CommonDmnService.GetSysItemDic(orgId, "Profession", null, vo.R_ZY).FirstOrDefault();
                    if (zy != null)
                    {
                        vo.ZY = zy.Name;
                    }
                }

                if (vo.PATID != null && !string.IsNullOrWhiteSpace(vo.RYSJ))
                {
                    sql =
                        //@"select convert(varchar(10),max(zycs)+1) ZYCS from [V_HIS_InpPatInfoForBA] with(nolock) where [OrganizeId]=@orgId and patid=@patid and zyh<>@zyh and RYSJ<@rysj ";
                        @"select NewtouchHIS_Sett.dbo.get_zycs(@zyh,'',@orgId) [ZYCS]";
                    string zycs = this.FindList<BasyVO>(sql,
                        new SqlParameter[]
                        {
                            new SqlParameter("@orgId", orgId),
                            //new SqlParameter("@patid", vo.PATID),
                            /*new SqlParameter("@zjh",vo.SFZH),*/
                            new SqlParameter("@zyh", zyh)
                            //new SqlParameter("@rysj", vo.RYSJ)
                        }).FirstOrDefault().ZYCS;

                    if (!string.IsNullOrWhiteSpace(zycs))
                    {
                        vo.ZYCS = zycs;
                    }
                }

                vo.BAH = zyh;
                //vo.BAH = GetBAH(orgId);
            }
            else
            {
                throw new FailedException("提取病人信息失败。");
            }

            if (string.IsNullOrWhiteSpace(vo.BAH))
            {
                throw new FailedException("病案号生成失败，请重试。");
            }
            return vo;
        }

        public BasyVO GetMainRecordZyFee(string orgId, string zyh)
        {
            string sql = @"select ZYH,zje ZFY,xjzf ZFJE
from V_HIS_InpPatSett
where OrganizeId=@orgId and zyh=@zyh
and jszt=1 
";
            BasyVO vo = new BasyVO();
            return this.FindList<BasyVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@zyh",zyh)
            }).FirstOrDefault();
        }

        public BasyVO GetMainRecordZyFeeDetail(BasyVO vo, string orgId, string zyh)
        {
            string sql = @"exec usp_sync_patfeefromSett @orgId=@orgId,@zyh=@zyh ";
            var list = this.FindList<DicFeeDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@zyh",zyh)
            });

            Type t = vo.GetType();//获得该类的Type
            foreach (PropertyInfo pi in t.GetProperties())
            {
                var name = pi.Name;//获得属性的名字,后面就可以根据名字判断来进行些自己想要的操作
                var val = list.Find(p => p.FeetypeCode == name);
                if (val != null)
                {
                    pi.SetValue(vo, val.Amount, null);
                }
            }
            if (vo.ZFY == null)   //未结算则取计费表费用
            {
                vo = GetMainRecordZyFeeDetailOld(vo, orgId, zyh);
            }
            return vo;
        }
        public BasyVO GetMainRecordZyFeeDetailOld(BasyVO vo, string orgId, string zyh)
        {
            string sql = @"select FeetypeCode,FeetypeName,isnull(sum(Amount),0)Amount
from V_HIS_InpPatFeeForBA
where OrganizeId=@orgId and  zyh=@zyh
group by FeetypeCode,FeetypeName
union all
select 'ZFY'FeetypeCode,'总费用' FeetypeName,isnull(sum(Amount),0)Amount
from V_HIS_InpPatFeeForBA
where OrganizeId=@orgId and zyh=@zyh
and FeetypeCode not in
(
select b.ShortCode from [Newtouch_MRMS].[dbo].[mr_dic_bafeetype] b where b.zt='1' and b.ParentCode is not null and b.ParentCode in(
select a.Code from [Newtouch_MRMS].[dbo].[mr_dic_bafeetype] a where a.zt='1' and a.ParentCode is not null)
)
";

            var list = this.FindList<DicFeeDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@zyh",zyh)
            });

            Type t = vo.GetType();//获得该类的Type
            foreach (PropertyInfo pi in t.GetProperties())
            {
                var name = pi.Name;//获得属性的名字,后面就可以根据名字判断来进行些自己想要的操作
                var val = list.Find(p => p.FeetypeCode == name);
                if (val != null)
                {
                    pi.SetValue(vo, val.Amount, null);
                }
            }

            return vo;
        }

        public IList<PatZDListVO> GetDiagLsit(string orgId, string bah, string zyh)
        {
            string zdsql = @"SELECT Id,[BAH],[ZYH],ZDLB,CASE WHEN ZDLX IS NULL THEN (CASE WHEN ZDOrder='1' THEN '1' ELSE '2' END) ELSE ZDLX END ZDLX 
,[ZDOrder],[JBDM],[JBMC],[RYBQ],[RYBQMS]
,[CYQK],[CYQKMS]
,[LastModifierCode],[OrganizeId]
  FROM [dbo].[mr_basy_zd] with(nolock)
where zt='1' and OrganizeId=@orgId and BAH=@bah and ZYH=@zyh  
order BY ZDLB, CASE WHEN ZDLX >= 3 THEN 0 ELSE 1 END, ABS(ZDLX),ZDOrder  ";

            var zdlist = FindList<PatZDListVO>(zdsql, new SqlParameter[] {
            new SqlParameter("@orgId",orgId),
            new SqlParameter("@bah",bah ??""),
            new SqlParameter("@zyh",zyh ??"")
        }.ToArray());

            return zdlist;
        }

        public IList<BasyOpDto> GetOPLsit(string orgId, string bah, string zyh)
        {
            string opsql = @"SELECT a.[Id],[BAH],[ZYH],[SSOrder],[SSJCZBM],[SSJCZRQ],[SSJB],[SSJCZMC]
,b.staffname SZMC,[SZ],c.staffname YZMC, [YZ],d.staffname EZMC, [EZ],[QKDJ],[QKYHLB],f.anesname MZFSMS, [MZFS],e.staffname MZYSMC, [MZYS],a.[zt],[CreateTime]
--,[CreatorCode],[LastModifyTime],[LastModifierCode],a.[OrganizeId]
,QKYHDJ,SSLX,SQZBSJT,SSKSSJ,SSJSSJ,SQYFKJGYS,MZKSSJNYR,MZFJ_ASA,QKBW,SSQKGR,SSBFZ,SSBFZMC
  FROM [dbo].[mr_basy_ss] a with(nolock)
  left join  [NewtouchHIS_Base].dbo.[V_C_Sys_StaffDuty] b with(nolock) on a.OrganizeId=b.organizeid and a.SZ=b.StaffGh and b.zt='1' and b.dutycode='Doctor'
  left join  [NewtouchHIS_Base].dbo.[V_C_Sys_StaffDuty] c with(nolock) on a.OrganizeId=c.organizeid and a.[YZ]=c.StaffGh and c.zt='1'  and c.dutycode='Doctor'
  left join  [NewtouchHIS_Base].dbo.[V_C_Sys_StaffDuty] d with(nolock) on a.OrganizeId=d.organizeid and a.[EZ]=d.StaffGh and d.zt='1' and d.dutycode='Doctor'
  left join  [NewtouchHIS_Base].dbo.[V_C_Sys_StaffDuty] e with(nolock) on a.OrganizeId=e.organizeid and a.[MZYS]=e.StaffGh and e.zt='1' and e.dutycode='Doctor'
left join [Newtouch_OR].[dbo].[V_SYS_Anesthesia] f with(nolock) on a.[MZFS]=f.AnesCode and a.OrganizeId=f.OrganizeId
where a.zt='1' and a.OrganizeId=@orgId and BAH=@bah and ZYH=@zyh  
order by SSOrder ";

            var sslist = FindList<BasyOpDto>(opsql, new SqlParameter[] {
            new SqlParameter("@orgId",orgId),
            new SqlParameter("@bah",bah),
            new SqlParameter("@zyh",zyh)
        }.ToArray());

            return sslist;
        }

        /// <summary>
        /// 诊断列表保存
        /// </summary>
        /// <param name="list"></param>
        /// <param name="orgId"></param>
        public void ZdListSubmit(List<BasyZdDto> list, string orgId)
        {
            int i = 1;
            int zycnt = 1;
            int flag = 0;
            if (list != null && list.Count > 0)
            {
                var bah = list.First().BAH;
                var baInfo = _MrbasyRepo.FindEntity(p => p.BAH == bah && p.OrganizeId == orgId && p.zt == "1");
                if (baInfo == null)
                {
                    throw new FailedException("患者基础病案信息未录入，无法保存。");
                }
                foreach (BasyZdDto v in list)
                {
                    MrbasyzdEntity zdety = new MrbasyzdEntity();
                    if (!string.IsNullOrWhiteSpace(v.Id))
                    {
                        zdety = _MrbasyzdRepo.FindEntity(v.Id);
                        if (zdety != null)
                        {
                            if (zdety.OrganizeId == orgId && zdety.zt == "1")
                            {
                                zdety.RYBQ = v.RYBQ;
                                zdety.RYBQMS = v.RYBQMS;
                                zdety.CYQK = v.CYQK;
                                zdety.CYQKMS = v.CYQKMS;
                                zdety.ZDLX = v.ZDLX;
                                zdety.ZDLB = v.ZDLB;
                                zdety.JBDM = v.JBDM;
                                zdety.JBMC = v.JBMC;
                                zdety.zt = v.zt == null ? zdety.zt : v.zt;

                                if (zdety.zt == "1")
                                {
                                    if (v.ZDLB == "TCM")
                                        zdety.ZDOrder = zycnt;
                                    else
                                        zdety.ZDOrder = i;
                                }

                                flag = 1;
                                _MrbasyzdRepo.Update(zdety);
                            }
                            else
                            {
                                throw new FailedException("数据异常，请刷新后重新填写");
                            }
                        }
                    }

                    if (flag == 0 && v.zt != "0")
                    {
                        zdety.BAH = v.BAH;
                        zdety.ZYH = v.ZYH;
                        zdety.OrganizeId = orgId;
                        zdety.RYBQ = v.RYBQ;
                        zdety.RYBQMS = v.RYBQMS;
                        zdety.CYQK = v.CYQK;
                        zdety.CYQKMS = v.CYQKMS;
                        zdety.JBDM = v.JBDM;
                        zdety.JBMC = v.JBMC;
                        zdety.ZDLX = v.ZDLX;
                        zdety.ZDLB = v.ZDLB;
                        zdety.zt = v.zt;

                        if (v.ZDLB == "TCM")
                            zdety.ZDOrder = zycnt;
                        else
                            zdety.ZDOrder = i;
                        zdety.Create(true);
                        _MrbasyzdRepo.Insert(zdety);
                    }

                    flag = 0;
                    if (v.zt != "0")
                    {
                        if (v.ZDLB == "TCM")
                            zycnt++;
                        else
                            i++;
                    }

                }
            }
            else
            {
                throw new FailedException("诊断列表不可为空");
            }
        }

        /// <summary>
        /// 手术列表保存
        /// </summary>
        /// <param name="list"></param>
        /// <param name="orgId"></param>
        public void OpListSubmit(List<BasyOpDto> list, string orgId)
        {
            int i = 1;
            int flag = 0;
            if (list != null && list.Count > 0)
            {
                var bah = list.First().BAH;
                var baInfo = _MrbasyRepo.FindEntity(p => p.BAH == bah && p.OrganizeId == orgId && p.zt == "1");
                if (baInfo == null)
                {
                    throw new FailedException("患者基础病案信息未录入，无法保存。");
                }
                foreach (BasyOpDto v in list)
                {
                    MrbasyssEntity opety = new MrbasyssEntity();
                    if (!string.IsNullOrWhiteSpace(v.Id))
                    {
                        opety = _MrbasyssRepo.FindEntity(v.Id);
                        if (opety != null)
                        {
                            if (opety.OrganizeId == orgId && opety.zt == "1")
                            {
                                opety.SSJCZBM = v.SSJCZBM;
                                opety.SSJCZRQ = Convert.ToDateTime(v.SSJCZRQ);
                                opety.SSJB = v.SSJB;
                                opety.SSJCZMC = v.SSJCZMC;
                                opety.SZ = v.SZ;
                                opety.YZ = v.YZ;
                                opety.EZ = v.EZ;
                                opety.QKDJ = v.QKDJ;
                                opety.QKYHLB = v.QKYHLB;
                                opety.QKYHDJ = v.QKYHDJ;
                                opety.MZFS = v.MZFS;
                                opety.MZYS = v.MZYS;
                                opety.zt = v.zt;
                                opety.SSLX = v.SSLX;
                                opety.SQZBSJT = v.SQZBSJT;
                                opety.SSJSSJ = v.SSJSSJ;
                                opety.SSKSSJ = v.SSKSSJ;
                                opety.SQYFKJGYS = v.SQYFKJGYS;
                                opety.MZKSSJNYR = v.MZKSSJNYR;
                                opety.MZFJ_ASA = v.MZFJ_ASA;
                                opety.QKBW = v.QKBW;
                                opety.SSQKGR = v.SSQKGR;
                                opety.SSBFZ = v.SSBFZ;
                                opety.SSBFZMC = v.SSBFZMC;
                                if (v.zt == "1")
                                {
                                    opety.SSOrder = i;
                                }

                                flag = 1;
                                _MrbasyssRepo.Update(opety);
                            }
                            else
                            {
                                throw new FailedException("数据异常，请刷新后重新填写");
                            }
                        }
                    }

                    if (flag == 0 && v.zt != "0")
                    {
                        opety.SSJCZBM = v.SSJCZBM;
                        opety.SSJCZRQ = Convert.ToDateTime(v.SSJCZRQ);
                        opety.SSJB = v.SSJB;
                        opety.SSJCZMC = v.SSJCZMC;
                        opety.SZ = v.SZ;
                        opety.YZ = v.YZ;
                        opety.EZ = v.EZ;
                        opety.QKDJ = v.QKDJ;
                        opety.QKYHLB = v.QKYHLB;
                        opety.QKYHDJ = v.QKYHDJ;
                        opety.MZFS = v.MZFS;
                        opety.MZYS = v.MZYS;
                        opety.SSOrder = i;
                        opety.ZYH = v.ZYH;
                        opety.BAH = v.BAH;
                        opety.OrganizeId = orgId;
                        opety.SSLX = v.SSLX;
                        opety.SQZBSJT = v.SQZBSJT;
                        opety.SSJSSJ = v.SSJSSJ;
                        opety.SSKSSJ = v.SSKSSJ;
                        opety.SQYFKJGYS = v.SQYFKJGYS;
                        opety.MZKSSJNYR = v.MZKSSJNYR;
                        opety.MZFJ_ASA = v.MZFJ_ASA;
                        opety.QKBW = v.QKBW;
                        opety.SSQKGR = v.SSQKGR;
                        opety.SSBFZ = v.SSBFZ;
                        opety.SSBFZMC = v.SSBFZMC;

                        opety.Create(true);
                        _MrbasyssRepo.Insert(opety);
                    }

                    flag = 0;
                    if (v.zt == "1")
                    {
                        i++;
                    }

                }
            }
            else
            {
                throw new FailedException("手术列表不可为空");
            }

        }

        public string PatBasicCheck(BasyVO vo, BasyRelVO relvo, string orgId, string user)
        {
            string msg = "";
            if (string.IsNullOrWhiteSpace(vo.RYSJ))
            {
                msg += "入院时间不可为空；";
            }
            if (string.IsNullOrWhiteSpace(vo.LYFS))
            {
                msg += "离院方式不可为空；";
            }
            if (string.IsNullOrWhiteSpace(vo.RYTJ))
            {
                msg += "入院途径不可为空；";
            }
            if (string.IsNullOrWhiteSpace(vo.YLFKFS))
            {
                msg += "医疗付款方式不可为空；";
            }
            if (string.IsNullOrWhiteSpace(vo.ZYCS))
            {
                msg += "住院次数不可为空；";
            }
            if (string.IsNullOrWhiteSpace(vo.XB))
            {
                msg += "性别不可为空；";
            }
            if (string.IsNullOrWhiteSpace(vo.CSRQ))
            {
                msg += "出生日期不可为空；";
            }
            if (string.IsNullOrWhiteSpace(vo.ZY))
            {
                msg += "职业不可为空；";
            }
            if (string.IsNullOrWhiteSpace(vo.SFZH))
            {
                msg += "身份证号不可为空；";
            }
            if (string.IsNullOrWhiteSpace(vo.HY))
            {
                msg += "婚姻状况不可为空；";
            }
            if (string.IsNullOrWhiteSpace(vo.MZZD))
            {
                msg += "门（急）诊诊断不可为空；";
            }
            if (string.IsNullOrWhiteSpace(vo.MZZDDM))
            {
                msg += "门（急）诊诊断疾病编码不可为空；";
            }
            if (string.IsNullOrWhiteSpace(vo.RYZD))
            {
                msg += "入院诊断不可为空；";
            }
            if (string.IsNullOrWhiteSpace(vo.RYZDDM))
            {
                msg += "入院诊断疾病编码不可为空；";
            }
            if (string.IsNullOrWhiteSpace(vo.LYFS))
            {
                msg += "离院方式不可为空；";
            }
            return msg;

        }

        /// <summary>
        /// 生成病案号
        /// </summary>
        /// <returns></returns>
        //public string GetBAH(string orgId)
        //{
        //    string year = DateTime.Now.Year.ToString();
        //    return EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("mr_basy.bah."+year, orgId, year+ "{0:D4}", false);
        //}

        public string GetBAH(string orgId)
        {
            string year = DateTime.Now.Year.ToString();
            return EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("mr_basy.bah." + year, orgId, year + "{0:D4}", false);
        }

        /// <summary>
        /// 保存患者基本信息
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="orgId"></param>
        public void PatBasicSubmit(BasyVO vo, BasyRelVO relvo, string blmbId, string orgId, string user, string username = null)
        {
            if (vo != null && !string.IsNullOrWhiteSpace(orgId))
            {
                string err = PatBasicCheck(vo, relvo, orgId, user);
                if (!string.IsNullOrWhiteSpace(err))
                {
                    throw new FailedException(err);
                }
                try
                {
                    MrbasyEntity ety = new MrbasyEntity();
                    MrbasyrelcodeEntity relety = new MrbasyrelcodeEntity();

                    ety = _MrbasyRepo.FindEntity(p => p.ZYH == vo.ZYH && p.OrganizeId == orgId && p.zt == "1");

                    if (ety != null && ety.BAH == vo.BAH)
                    {
                        //更新
                        //ety = _MrbasyRepo.FindEntity(p => p.BAH == vo.BAH && p.zt == "1" && p.OrganizeId == orgId);
                        if (ety != null)
                        {
                            ety = PatBasyInfoSet(ety, vo, orgId);
                            ety.LastModifyTime = DateTime.Now;
                            ety.LastModifierCode = user;
                            _MrbasyRepo.Update(ety);

                            var findrel = _MrbasyrelcodeRepo.FindEntity(p => p.BAH == ety.BAH && p.SYId == ety.Id && p.OrganizeId == orgId);
                            if (findrel != null)
                            {
                                relvo.R_YLFKFS = vo.YLFKFS;
                                relety = PatBasyInfoRelSet(findrel, relvo, vo, orgId);
                                _MrbasyrelcodeRepo.Update(relety);
                            }
                            else
                            {
                                relvo.R_BAH = vo.BAH;
                                relvo.R_YLFKFS = vo.YLFKFS;

                                relety = PatBasyInfoRelSet(relety, relvo, vo, orgId);
                                relety.SYId = ety.Id;
                                relety.Create(true);
                                _MrbasyrelcodeRepo.Insert(relety);
                            }

                            var basyLj = _ZymeddocsrelationRepo.FindEntity(p => p.OrganizeId == orgId && p.zyh == ety.ZYH && p.blId==ety.Id && p.bllx == "5" && p.zt == "0");
                            if (basyLj != null)
                            {
                                DocRelSubmit(blmbId, ety.ZYH, ety.Id, "病案首页", ety.CreatorCode, username, orgId);
                            }
                            else
                            {
                                var Lj = _ZymeddocsrelationRepo.FindEntity(p => p.OrganizeId == orgId && p.zyh == ety.ZYH && p.blId == ety.Id && p.bllx == "5" && p.zt == "1");
                                ZymeddocsrelationEntity entity = new ZymeddocsrelationEntity();
                                entity = Lj;
                                entity.LastModifierCode = ety.LastModifierCode;
                                entity.LastModifyTime = ety.LastModifyTime;
                                _ZymeddocsrelationRepo.Update(entity);
                            }
                        }
                        else
                        {
                            throw new FailedException("未找到该患者病案信息，请刷新重试。");
                        }
                    }
                    else if (ety == null && !string.IsNullOrWhiteSpace(vo.BAH) && !string.IsNullOrWhiteSpace(blmbId))
                    {
                        vo.bazt = ((int)Enumbazt.lrz).ToString();
                        var pat = GetMainRecordbybrjbxx(orgId, vo.ZYH);
                        if (pat != null)
                        {
                            ety = PatBasyInfoSet(ety, vo, orgId);
                            ety.PATID = pat.PATID;
                            ety.bazt = ((int)Enumbazt.lrz).ToString();
                            ety.OrganizeId = orgId;
                            ety.Create(true);
                            _MrbasyRepo.Insert(ety);

                            relvo.R_BAH = vo.BAH;
                            relvo.R_YLFKFS = vo.YLFKFS;
                            relety = PatBasyInfoRelSet(relety, relvo, vo, orgId);
                            relety.SYId = ety.Id;
                            relety.Create(true);
                            _MrbasyrelcodeRepo.Insert(relety);

                            var basy = _MrbasyRepo.FindEntity(p => p.OrganizeId == orgId && p.ZYH == ety.ZYH && p.BAH == ety.BAH && p.zt == "1");
                            if (basy != null)
                            {
                                DocRelSubmit(blmbId, basy.ZYH, basy.Id, "病案首页", basy.CreatorCode, username, orgId);
                            }
                        }
                        else
                        {
                            throw new FailedException("未找到该患者病案信息，请刷新重试。");
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(vo.BAH))
                    {
                        throw new FailedException("病案号生成失败，请重新加载。");
                    }
                    else if (string.IsNullOrWhiteSpace(blmbId))
                    {
                        throw new FailedException("无法加载病案模板，请返回重试。");
                    }
                    else
                    {
                        throw new FailedException("患者已存在病案信息，请勿重复提交。");
                    }
                }
                catch (FailedException ex)
                {
                    var exmsg = ex.Msg == null ? ex.Message : ex.Msg;
                    throw new FailedException(exmsg);
                }

            }
        }


        public MrbasyEntity PatBasyInfoSet(MrbasyEntity ety, BasyVO vo, string orgId)
        {
            if (ety == null)
            {
                ety = new MrbasyEntity();
            }
            ety.BZYYSNL = vo.BZYYSNL;
            ety.QTYLJGZR = vo.QTYLJGZR;
            ety.BAH = vo.BAH;
            ety.YLFKFS = vo.YLFKFS;
            ety.JKKH = vo.JKKH;
            ety.ZYCS = vo.ZYCS;
            ety.XM = vo.XM;
            ety.XB = vo.XB;
            ety.PATID = vo.PATID;
            ety.ZYH = vo.ZYH;
            if (!string.IsNullOrWhiteSpace(vo.CSRQ))
            {
                ety.CSRQ = Convert.ToDateTime(vo.CSRQ);
            }
            ety.NL = vo.NL;
            ety.GJ = vo.GJ;
            ety.BZYZSNL = vo.BZYZSNL;
            ety.XSECSTZ = vo.XSECSTZ;
            ety.XSERYTZ = vo.XSERYTZ;
            ety.CSD = vo.CSD;
            ety.GG = vo.GG;
            ety.MZ = vo.MZ;
            ety.SFZH = vo.SFZH;
            ety.ZY = vo.ZY;
            ety.HY = vo.HY;
            ety.XZZ = vo.XZZ;
            ety.DH = vo.DH;
            ety.XZZYB = vo.XZZYB;
            ety.HKDZ = vo.HKDZ;
            ety.HKDYB = vo.HKDYB;
            ety.GZDWJDZ = vo.GZDWJDZ;
            ety.DWDH = vo.DWDH;
            ety.DWYB = vo.DWYB;
            ety.LXRXM = vo.LXRXM;
            ety.GX = vo.GX;
            ety.LXRDZ = vo.LXRDZ;
            ety.LXRDH = vo.LXRDH;
            ety.RYTJ = vo.RYTJ;
            if (!string.IsNullOrWhiteSpace(vo.RYSJ))
            {
                ety.RYSJ = Convert.ToDateTime(vo.RYSJ);
                ety.RYSJS = Convert.ToDateTime(vo.RYSJ).Hour;
            }

            ety.RYKB = vo.RYKB;
            ety.RYBF = vo.RYBF;
            ety.ZKKB = vo.ZKKB;

            if (!string.IsNullOrWhiteSpace(vo.CYSJ))
            {
                ety.CYSJ = Convert.ToDateTime(vo.CYSJ);
                ety.CYSJS = Convert.ToDateTime(vo.CYSJ).Hour;
            }
            ety.CYKB = vo.CYKB;
            ety.CYBF = vo.CYBF;
            ety.SJZYTS = vo.SJZYTS;
            ety.MZZD = vo.MZZD;
            ety.MZZDDM = vo.MZZDDM;
            ety.BWHBZ = vo.BWHBZ;
            ety.RYZD = vo.RYZD;
            ety.RYZDDM = vo.RYZDDM;
            ety.SSLCLJ = vo.SSLCLJ;
            ety.QJCS = vo.QJCS;
            ety.QJCGCS = vo.QJCGCS;
            if (!string.IsNullOrWhiteSpace(vo.QZRQ))
            {
                ety.QZRQ = Convert.ToDateTime(vo.QZRQ);
            }
            ety.ZQSS = vo.ZQSS;
            ety.BRLY = "1";
            ety.WBYY = vo.WBYY;
            ety.H23 = vo.H23;
            ety.BLZD = vo.BLZD;
            ety.BLZDDM = vo.BLZDDM;
            ety.BLH = vo.BLH;
            ety.YWGM = vo.YWGM == null ? "1" : vo.YWGM;
            ety.GMYW = vo.GMYW;
            ety.SWHZSJ = vo.SWHZSJ;
            ety.RH = vo.RH;
            ety.XX = vo.XX;
            ety.KZR = vo.KZR;
            ety.ZRYS = vo.ZRYS;
            ety.ZZYS = vo.ZZYS;
            ety.ZZYS1 = vo.ZZYS1;

            ety.ZZYS = vo.ZZYS;
            ety.ZYYS = vo.ZYYS;
            ety.ZRHS = vo.ZRHS;
            ety.JXYS = vo.JXYS;
            ety.SXYS = vo.SXYS;
            ety.BMY = vo.BMY;
            ety.BAZL = vo.BAZL == null ? "1" : vo.BAZL;
            ety.ZKYS = vo.ZKYS;
            ety.ZKHS = vo.ZKHS;
            if (!string.IsNullOrWhiteSpace(vo.ZKRQ))
            {
                ety.ZKRQ = Convert.ToDateTime(vo.ZKRQ);
            }
            ety.LYFS = vo.LYFS == null ? "9" : vo.LYFS;
            ety.YZZY_YLJG = vo.YZZY_YLJG;
            ety.WSY_YLJG = vo.WSY_YLJG;
            ety.SFZZYJH = vo.SFZZYJH;
            ety.MD = vo.MD;
            ety.RYQ_T = vo.RYQ_T;
            ety.RYQ_XS = vo.RYQ_XS;
            ety.RYH_T = vo.RYH_T;
            ety.RYQ_F = vo.RYQ_F;
            ety.RYH_XS = vo.RYH_XS;
            ety.RYH_F = vo.RYH_F;

            ety.LCLJGL = vo.LCLJGL;
            ety.LCBZGL = vo.LCBZGL;

            ety.BZGLFL = vo.BZGLFL;
            ety.BQFX = vo.BQFX;
            ety.RYCH = vo.RYCH;
            ety.CYCH = vo.CYCH;
            ety.CSD_SN = vo.CSD_SN;
            ety.CSD_SI = vo.CSD_SI;
            ety.CSD_QX = vo.CSD_QX;
            ety.CSD_JD = vo.CSD_JD;
            ety.XZZ_SN = vo.XZZ_SN;
            ety.XZZ_SI = vo.XZZ_SI;
            ety.XZZ_QX = vo.XZZ_QX;
            ety.XZZ_JD = vo.XZZ_JD;
            ety.HKDZ_SN = vo.HKDZ_SN;
            ety.HKDZ_SI = vo.HKDZ_SI;
            ety.HKDZ_QX = vo.HKDZ_QX;
            ety.HKDZ_JD = vo.HKDZ_JD;
            ety.LXRDZ_SN = vo.LXRDZ_SN;
            ety.LXRDZ_SI = vo.LXRDZ_SI;
            ety.LXRDZ_QX = vo.LXRDZ_QX;
            ety.LXRDZ_JD = vo.LXRDZ_JD;

            ety.ZFY = vo.ZFY;
            ety.ZFJE = vo.ZFJE;
            ety.YLFUF = vo.YLFUF;
            ety.BZLZF = vo.BZLZF;
            ety.ZYBLZHZF = vo.ZYBLZHZF;
            ety.ZLCZF = vo.ZLCZF;
            ety.HLF = vo.HLF;
            ety.QTFY = vo.QTFY;
            ety.BLZDF = vo.BLZDF;
            ety.SYSZDF = vo.SYSZDF;
            ety.YXXZDF = vo.YXXZDF;
            ety.LCZDXMF = vo.LCZDXMF;
            ety.FSSZLXMF = vo.FSSZLXMF;
            ety.WLZLF = vo.WLZLF;
            ety.SSZLF = vo.SSZLF;
            ety.MAF = vo.MAF;
            ety.SSF = vo.SSF;
            ety.KFF = vo.KFF;
            ety.ZYZLF = vo.ZYZLF;
            ety.ZYZL = vo.ZYZL;
            ety.ZYWZ = vo.ZYWZ;
            ety.ZYGS = vo.ZYGS;
            ety.ZCYJF = vo.ZCYJF;
            ety.ZYTNZL = vo.ZYTNZL;
            ety.ZYGCZL = vo.ZYGCZL;
            ety.ZYTSZL = vo.ZYTSZL;
            ety.ZYQT = vo.ZYQT;
            ety.ZYTSTPJG = vo.ZYTSTPJG;
            ety.BZSS = vo.BZSS;
            ety.XYF = vo.XYF;
            ety.KJYWF = vo.KJYWF;
            ety.ZCYF = vo.ZCYF;
            ety.ZYZJF = vo.ZYZJF;
            ety.ZCYF1 = vo.ZCYF1;
            ety.XF = vo.XF;
            ety.BDBLZPF = vo.BDBLZPF;
            ety.QDBLZPF = vo.QDBLZPF;
            ety.NXYZLZPF = vo.NXYZLZPF;
            ety.XBYZLZPF = vo.XBYZLZPF;
            ety.HCYYCLF = vo.HCYYCLF;
            ety.YYCLF = vo.YYCLF;
            ety.YCXYYCLF = vo.YCXYYCLF;
            ety.QTF = vo.QTF;
            /*2022-10-09新增*/
            ety.HXB = vo.HXB;
            ety.XXB = vo.XXB;
            ety.XJ = vo.XJ;
            ety.QX = vo.QX;
            ety.ZTXHS = vo.ZTXHS;
            ety.BDB = vo.BDB;
            ety.LCD = vo.LCD;
            ety.QT = vo.QT;
            ety.SXFY = vo.SXFY;
            /*随诊*/
            ety.SZ = vo.SZ;
            ety.SZQXZ = vo.SZQXZ;
            ety.SZQXY = vo.SZQXY;
            ety.SZQXN = vo.SZQXN;
            /*临床路径*/
            ety.WCLCLJ = vo.WCLCLJ;/*完成临床路径*/
            ety.TCYY = vo.TCYY;/*临床路径退出原因*/
            ety.SFBY = vo.SFBY;/*临床路径是否变异*/
            ety.BYYY = vo.BYYY;/*临床路径变异原因*/
            /*检查情况*/
            ety.CT = vo.CT;
            ety.PETCT = vo.PETCT;
            ety.SYCT = vo.SYCT;
            ety.BC = vo.BC;
            ety.XP = vo.XP;
            ety.CSXDT = vo.CSXDT;
            ety.MRI = vo.MRI;
            ety.TWSJC = vo.TWSJC;

            ety.SYCXSJ = vo.SYCXSJ;
            ety.LHYY = vo.LHYY;

            /*附页*/
            ety.YYGRQK = vo.YYGRQK;
            ety.YYGRSSXG = vo.YYGRSSXG;
            ety.YYGRSFQRXG = vo.YYGRSFQRXG;
            ety.KJYWSYQK = vo.KJYWSYQK;
            ety.KJYWMC1 = vo.KJYWMC1;
            ety.KJYWMC2 = vo.KJYWMC2;
            ety.KJYWMC3 = vo.KJYWMC3;
            ety.KJYWMC4 = vo.KJYWMC4;
            ety.KJYWMC5 = vo.KJYWMC5;
            ety.KJYWMC6 = vo.KJYWMC6;

            ety.SFFSYC = vo.SFFSYC;
            ety.SFZYQJFS = vo.SFZYQJFS;
            ety.YCFQ = vo.YCFQ;
            ety.SYFY = vo.SYFY;
            ety.YFFYDYW = vo.YFFYDYW;
            ety.SYLCBX = vo.SYLCBX;
            ety.ZYSFDDHZC = vo.ZYSFDDHZC;
            ety.ZYDDHZCDCD = vo.ZYDDHZCDCD;
            ety.DDHZCDYY = vo.DDHZCDYY;
            ety.ZYQJSTYY = vo.ZYQJSTYY;
            ety.LYTXNSDZ = vo.LYTXNSDZ;
            ety.DWFZR = vo.DWFZR;
            ety.TJFZR = vo.TJFZR;
            ety.TBR = vo.TBR;
            ety.LXDH = vo.LXDH;
            ety.SJ = vo.SJ;
            ety.BCRQ = vo.BCRQ;

            ety.ZYSSLCLJ = vo.ZYSSLCLJ;
            ety.SYYLJGZYZJ = vo.SYYLJGZYZJ;
            ety.SYZYZLSB = vo.SYZYZLSB;
            ety.SYZYZLJS = vo.SYZYZLJS;
            ety.BZSH = vo.BZSH;
            return ety;
        }

        public MrbasyrelcodeEntity PatBasyInfoRelSet(MrbasyrelcodeEntity relEty, BasyRelVO relvo, BasyVO vo, string orgId)
        {
            relEty.OrganizeId = orgId;
            relEty.BAH = relvo.R_BAH == null ? relEty.BAH : relvo.R_BAH;
            if (!string.IsNullOrWhiteSpace(vo.YLFKFS))
            {
                var ylfk = _CommonDmnService.DicCommonList(orgId, null, "YLFKFS", vo.YLFKFS).FirstOrDefault();
                relEty.YLFKFS = ylfk.Name;
            }

            if (!string.IsNullOrWhiteSpace(vo.CYKB) && string.IsNullOrWhiteSpace(relvo.R_CYKB) && string.IsNullOrWhiteSpace(relEty.CYKB))
            {
                var cybq = _CommonDmnService.GetDeptList(orgId, vo.CYKB).FirstOrDefault();
                if (cybq != null && !string.IsNullOrWhiteSpace(cybq.Code))
                {
                    relEty.CYKB = cybq.Code;
                }
            }
            if (!string.IsNullOrWhiteSpace(vo.RYKB) && string.IsNullOrWhiteSpace(relvo.R_RYKB) && string.IsNullOrWhiteSpace(relEty.RYKB))
            {
                var rybq = _CommonDmnService.GetDeptList(orgId, vo.RYKB).FirstOrDefault();
                if (rybq != null && !string.IsNullOrWhiteSpace(rybq.Code))
                {
                    relEty.RYKB = rybq.Code;
                }
            }
            if (!string.IsNullOrWhiteSpace(vo.RYBF) && string.IsNullOrWhiteSpace(relvo.R_RYBF) && string.IsNullOrWhiteSpace(relEty.RYBF))
            {
                var rybf = _CommonDmnService.GetWardList(orgId, vo.RYBF).FirstOrDefault();
                if (rybf != null && !string.IsNullOrWhiteSpace(rybf.Code))
                {
                    relEty.RYBF = rybf.Code;
                }
            }
            if (!string.IsNullOrWhiteSpace(vo.CYBF) && string.IsNullOrWhiteSpace(relvo.R_RYBF) && string.IsNullOrWhiteSpace(relEty.CYBF))
            {
                var cybf = _CommonDmnService.GetWardList(orgId, vo.CYBF).FirstOrDefault();
                if (cybf != null && !string.IsNullOrWhiteSpace(cybf.Code))
                {
                    relEty.CYBF = cybf.Code;
                }
            }
            if (!string.IsNullOrWhiteSpace(vo.RYTJ))
            {
                Dictionary<string, string> rytjdic = EFDBBaseFuncHelper.GetEnumDescription<EnumRYTJ>();
                EnumRYTJ enumRytj = (EnumRYTJ)Enum.Parse(typeof(EnumRYTJ), vo.RYTJ.ToString());
                relEty.RYTJ = rytjdic[enumRytj.ToString()];
            }
            if (!string.IsNullOrWhiteSpace(vo.XB))
            {
                Dictionary<string, string> xbdic = EFDBBaseFuncHelper.GetEnumDescription<EnumMRSex>();
                EnumMRSex enumSex = (EnumMRSex)Enum.Parse(typeof(EnumMRSex), vo.XB.ToString());
                relEty.XB = xbdic[enumSex.ToString()];
            }
            if (!string.IsNullOrWhiteSpace(vo.BQFX))
            {
                Dictionary<string, string> bqfxdic = EFDBBaseFuncHelper.GetEnumDescription<EnumBqfx>();
                EnumBqfx enumbqfx = (EnumBqfx)Enum.Parse(typeof(EnumBqfx), vo.BQFX.ToString());
                relEty.BQFX = bqfxdic[enumbqfx.ToString()];
            }
            if (!string.IsNullOrWhiteSpace(vo.LYFS))
            {
                Dictionary<string, string> lyfsdic = EFDBBaseFuncHelper.GetEnumDescription<EnumLyfs>();
                EnumLyfs enumlyfs = (EnumLyfs)Enum.Parse(typeof(EnumLyfs), vo.LYFS.ToString());
                relEty.LYFS = lyfsdic[enumlyfs.ToString()];
            }
            if (!string.IsNullOrWhiteSpace(vo.BAZL))
            {
                Dictionary<string, string> bazldic = EFDBBaseFuncHelper.GetEnumDescription<EnumBazl>();
                EnumBazl enumbazl = (EnumBazl)Enum.Parse(typeof(EnumBazl), vo.BAZL.ToString());
                relEty.BAZL = bazldic[enumbazl.ToString()];
            }
            if (!string.IsNullOrWhiteSpace(vo.XX))
            {
                Dictionary<string, string> xxdic = EFDBBaseFuncHelper.GetEnumDescription<EnumBloodType>();
                EnumBloodType enumxx = (EnumBloodType)Enum.Parse(typeof(EnumBloodType), vo.XX.ToString());
                relEty.XX = xxdic[enumxx.ToString()];
            }
            if (!string.IsNullOrWhiteSpace(vo.RH))
            {
                Dictionary<string, string> rhdic = EFDBBaseFuncHelper.GetEnumDescription<EnumBloodTypeRH>();
                EnumBloodTypeRH enumrh = (EnumBloodTypeRH)Enum.Parse(typeof(EnumBloodTypeRH), vo.RH.ToString());
                relEty.RH = rhdic[enumrh.ToString()];
            }
            if (!string.IsNullOrWhiteSpace(vo.LYFS))
            {
                Dictionary<string, string> lyfsdic = EFDBBaseFuncHelper.GetEnumDescription<EnumLyfs>();
                EnumLyfs enumlyfs = (EnumLyfs)Enum.Parse(typeof(EnumLyfs), vo.LYFS.ToString());
                relEty.LYFS = lyfsdic[enumlyfs.ToString()];
            }
            if (!string.IsNullOrWhiteSpace(vo.SXFY))
            {
                Dictionary<string, string> lyfsdic = EFDBBaseFuncHelper.GetEnumDescription<EnumSXFY>();
                EnumSXFY enumSXFY = (EnumSXFY)Enum.Parse(typeof(EnumSXFY), vo.SXFY.ToString());
                relEty.SXFY = lyfsdic[enumSXFY.ToString()];
            }
            if (!string.IsNullOrWhiteSpace(vo.BYYY))
            {
                Dictionary<string, string> lyfsdic = EFDBBaseFuncHelper.GetEnumDescription<EnumBYYY>();
                EnumBYYY enumBYYY = (EnumBYYY)Enum.Parse(typeof(EnumBYYY), vo.BYYY.ToString());
                relEty.BYYY = lyfsdic[enumBYYY.ToString()];
            }

            relEty.BLZDDM = relvo.R_BLZDDM == null ? relEty.BLZDDM : relvo.R_BLZDDM;
            relEty.BMY = relvo.R_BMY == null ? relEty.BMY : relvo.R_BMY;
            relEty.BRLY = relvo.R_BRLY == null ? relEty.BRLY : relvo.R_BRLY;
            relEty.CYBF = relvo.R_CYBF == null ? relEty.CYBF : relvo.R_CYBF;
            relEty.CYKB = relvo.R_CYKB == null ? relEty.CYKB : relvo.R_CYKB;
            relEty.GJ = relvo.R_GJ == null ? relEty.GJ : relvo.R_GJ;
            relEty.GX = relvo.R_GX == null ? relEty.GX : relvo.R_GX;
            relEty.HY = relvo.R_HY == null ? relEty.HY : relvo.R_HY;
            relEty.JXYS = relvo.R_JXYS == null ? relEty.JXYS : relvo.R_JXYS;
            relEty.KZR = relvo.R_KZR == null ? relEty.KZR : relvo.R_KZR;
            relEty.MZ = relvo.R_MZ == null ? relEty.MZ : relvo.R_MZ;
            relEty.RYBF = relvo.R_RYBF == null ? relEty.RYBF : relvo.R_RYBF;
            relEty.RYKB = relvo.R_RYKB == null ? relEty.RYKB : relvo.R_RYKB;
            //relEty.SFZZYJH = relvo.R_SFZZYJH == null ? relEty.HY : relvo.R_HY; 
            relEty.SXYS = relvo.R_SXYS == null ? relEty.SXYS : relvo.R_SXYS;
            //relEty.YWGM = vo.YWGM == null ? relEty.YWGM : vo.YWGM; 
            relEty.ZKHS = relvo.R_ZKHS == null ? relEty.ZKHS : relvo.R_ZKHS;
            relEty.ZKKB = relvo.R_ZKKB == null ? relEty.ZKKB : relvo.R_ZKKB;
            relEty.ZKYS = relvo.R_ZKYS == null ? relEty.ZKYS : relvo.R_ZKYS;
            relEty.ZRHS = relvo.R_ZRHS == null ? relEty.ZRHS : relvo.R_ZRHS;
            relEty.ZRYS = relvo.R_ZRYS == null ? relEty.ZRYS : relvo.R_ZRYS;
            relEty.ZY = relvo.R_ZY == null ? relEty.ZY : relvo.R_ZY;
            relEty.ZYYS = relvo.R_ZYYS == null ? relEty.ZYYS : relvo.R_ZYYS;
            relEty.ZZYS = relvo.R_ZZYS == null ? relEty.ZZYS : relvo.R_ZZYS;
            relEty.ZZYS1 = relvo.R_ZZYS1 == null ? relEty.ZZYS1 : relvo.R_ZZYS1;

            relEty.CSD_SN = relvo.R_CSD_SN == null ? relEty.CSD_SN : relvo.R_CSD_SN;
            relEty.CSD_SI = relvo.R_CSD_SI == null ? relEty.CSD_SI : relvo.R_CSD_SI;
            relEty.CSD_QX = relvo.R_CSD_QX == null ? relEty.CSD_QX : relvo.R_CSD_QX;
            relEty.CSD_JD = relvo.R_CSD_JD == null ? relEty.CSD_JD : relvo.R_CSD_JD;
            relEty.XZZ_SN = relvo.R_XZZ_SN == null ? relEty.XZZ_SN : relvo.R_XZZ_SN;
            relEty.XZZ_SI = relvo.R_XZZ_SI == null ? relEty.XZZ_SI : relvo.R_XZZ_SI;
            relEty.XZZ_QX = relvo.R_XZZ_QX == null ? relEty.XZZ_QX : relvo.R_XZZ_QX;
            relEty.XZZ_JD = relvo.R_XZZ_JD == null ? relEty.XZZ_JD : relvo.R_XZZ_JD;
            relEty.HKDZ_SN = relvo.R_HKDZ_SN == null ? relEty.HKDZ_SN : relvo.R_HKDZ_SN;
            relEty.HKDZ_SI = relvo.R_HKDZ_SI == null ? relEty.HKDZ_SI : relvo.R_HKDZ_SI;
            relEty.HKDZ_QX = relvo.R_HKDZ_QX == null ? relEty.HKDZ_QX : relvo.R_HKDZ_QX;
            relEty.HKDZ_JD = relvo.R_HKDZ_JD == null ? relEty.HKDZ_JD : relvo.R_HKDZ_JD;
            relEty.LXRDZ_SN = relvo.R_LXRDZ_SN == null ? relEty.LXRDZ_SN : relvo.R_LXRDZ_SN;
            relEty.LXRDZ_SI = relvo.R_LXRDZ_SI == null ? relEty.LXRDZ_SI : relvo.R_LXRDZ_SI;
            relEty.LXRDZ_QX = relvo.R_LXRDZ_QX == null ? relEty.LXRDZ_QX : relvo.R_LXRDZ_QX;
            relEty.LXRDZ_JD = relvo.R_LXRDZ_JD == null ? relEty.LXRDZ_JD : relvo.R_LXRDZ_JD;

            return relEty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="zdlb">诊断类别 1入院诊断2出院诊断</param>
        /// <returns></returns>
        public IList<PatZDListVO> GetPatHisZDInfo(string bah, string zyh, string orgId, int? zdlb)
        {
            string sql = @"select @bah BAH,OrganizeId,ZYH,convert(int,zdlx) ZDOrder,JBDM ,JBMC,
convert(varchar(2)," + ((int)EnumRybq.y).ToString() + ") RYBQ,'有' RYBQMS," +
"convert(varchar(2)," + ((int)EnumCyqk.zy).ToString() + @") CYQK,'治愈' CYQKMS
from V_HIS_InpPatDiag with(nolock)
where organizeid=@orgId and zyh=@zyh
";

            if (zdlb != 0)
            {
                sql += "  and zdlb=@zdlb ";
            }

            sql += " order by zyh,zdlx ";
            return this.FindList<PatZDListVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@bah", bah),
                new SqlParameter("@zyh",zyh),
                new SqlParameter("@zdlb",zdlb)});
        }

        public IList<BasyOpDto> GetPatHisOperInfo(string bah, string zyh, string orgId)
        {
            string sql = @" SELECT @bah BAH,@zyh ZYH,[OrganizeId],[ssxh],[SSJCZBM],[SSJCZMC],[SSJCZRQ],[SSJB],[SZ],[YZ],[EZ]
,[MZYS],[SZMC],[YZMC],[EZMC],[MZYSMC],[QKDJ],[MZFS],[MZFSMS]
FROM [dbo].[V_HIS_PatOperation]
where OrganizeId=@orgId and zyh=@zyh
order by  ssxh
";

            return this.FindList<BasyOpDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@zyh",zyh),
                new SqlParameter("@bah", bah)});
        }

        #region EMR关系维护
        public void DocRelSubmit(string blmbId, string zyh, string blId, string blmc, string ysgh, string ysxm, string orgId)
        {
            string bllx = "";
            var mbety = _blmblbRepo.FindEntity(blmbId);
            if (mbety != null)
            {
                bllx = mbety.bllx;
            }
            ZymeddocsrelationEntity entity = new ZymeddocsrelationEntity();
            entity.bllx = bllx ?? "5";//(int)EnumBllx.basy;
            entity.blmc = DateTime.Now.ToString("ddHHmm") + blmc;
            entity.blrq = DateTime.Now;
            entity.blzt = 0;
            entity.blId = blId;
            entity.mbId = blmbId;
            entity.OrganizeId = orgId;
            entity.zyh = zyh;
            entity.IsParent = 0;
            entity.ysgh = ysgh;
            entity.ysxm = ysxm;
            entity.YbUploadFlag = "0";
            entity.Create(true);
            _ZymeddocsrelationRepo.Insert(entity);
        }
        #endregion

        public IList<InpPatTransferInfo> GetInpPatTransferInfo(string zyh, string orgId)
        {
            string sql = @"select convert(int, ROW_NUMBER()over(order by CreateTime ))num ,OrganizeId,zyh,BedCode,BedNo,WardCode,WardName,RoomCode,RoomName,
DeptCode,DeptName,TransBedCode,TransDeptCode,TransWardCode,convert(varchar(2),[Status])Status,zt, TransDeptName,TransWardName
from V_HIS_InpPatTransfer a with(nolock)
where a.organizeid=@orgId and a.zyh=@zyh";
            return this.FindList<InpPatTransferInfo>(sql, new SqlParameter[] {
                new SqlParameter("@zyh",zyh),
                new SqlParameter("@orgId",orgId)
            });
        }

        public IList<YB_7600> GetHomePageforYB(string OrgId, string zyh)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[] {
                            new SqlParameter("@zyh", zyh),
                            new SqlParameter("@OrgId", OrgId)
                };

                string sql = "exec  usp_YB_GetHomePage @OrgId=@OrgId,@zyh=@zyh ";
                return this.FindList<YB_7600>(sql, para);

            }
            catch (Exception ex)
            {
                throw new FailedException("获取病案首页异常，" + ex.Message);
            }
        }
        public IList<YB_7610> GetHomePageDiagforYB(string OrgId, string zyh)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[] {
                            new SqlParameter("@zyh", zyh),
                            new SqlParameter("@OrgId", OrgId)
                };

                string sql = "exec  usp_YB_GetHomePage @OrgId=@OrgId,@zyh=@zyh ";
                return this.FindList<YB_7610>(sql, para);

            }
            catch (Exception ex)
            {
                throw new FailedException("获取病案首页异常，" + ex.Message);
            }
        }

        #region 结算病人不能保存诊断
        public IList<PatZDListVO> SettlementQuery(string zyh, string orgId)
        {
            try
            {
                var sql = "select zyh ZYH,jsjsrq CreateTime from [NewtouchHIS_Sett].[dbo].[zy_js] with(nolock)  " +
                    "where jszt = '1' and zt = '1' and zyh = @zyh and jsxz<>'2' and OrganizeId = @orgId " +
                    "and jsnm not in (select cxjsnm from[NewtouchHIS_Sett].[dbo].[zy_js] where jszt = 2 and zt = 1) ";
                return this.FindList<PatZDListVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@zyh",zyh)});
            }
            catch (Exception ex)
            {

                throw new FailedException("异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 把病案的诊断同步到出区诊断表
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public void SynchronizationZD(string zyh, string orgId)
        {
            try
            {


                var sqldelete = "delete from [Newtouch_CIS].dbo.zy_PatDxInfo where zyh=@zyh and zdlb='2' and OrganizeId = @orgId";
                this.ExecuteSqlCommand(sqldelete, new SqlParameter[] {
                           new SqlParameter("@zyh",zyh),
                           new SqlParameter("@orgId", orgId)
                           });
                var sqlcx = "select count(zyh) zs from [Newtouch_CIS]..zy_PatDxInfo where zyh=@zyh and zt='1' and OrganizeId=@orgId and zdlb='2' ";
                var data = this.FindList<ButtonEnableVO>(sqlcx, new SqlParameter[] {
                new SqlParameter("@zyh",zyh),
                new SqlParameter("@orgId", orgId)});

                var sqlcx2 = "select Id,ZYH,ZDLB,ZDLX,ZDOrder,JBDM,JBMC,CYQK,ZT,CreateTime,CreatorCode,OrganizeId from [Newtouch_EMR].[dbo].[mr_basy_zd]  a  where  " +
                    " a.zyh = @zyh and a.OrganizeId = @orgId and a.jbdm <> '999999999' and a.zt = '1' order by ZDOrder ";
                var data2 = this.FindList<PatZDListVO>(sqlcx2, new SqlParameter[] {
                new SqlParameter("@zyh",zyh),
                new SqlParameter("@orgId", orgId)});

                if (data[0].zs > 0 && data2.Count > 0)
                {
                    var datacount = data[0].zs;
                    for (int i = 0; i < data2.Count; i++)
                    {
                        //i = int.Parse(datacount.ToString());
                        var zdlx = datacount + i;
                        var sql = "insert into [Newtouch_CIS]..zy_PatDxInfo  " +
                            "values(@Id, @OrganizeId, @zyh, @zddl,@zdlb, @zdlx, @zddm, @zdmc, NULL, @CreateTime, @CreatorCode, NULL, NULL, @zt, @cyqk,@px)";

                        this.ExecuteSqlCommand(sql, new SqlParameter[] {
                            new SqlParameter("@Id", data2[i].Id),
                            new SqlParameter("@OrganizeId", data2[i].OrganizeId),
                            new SqlParameter("@zyh", data2[i].ZYH),
                            new SqlParameter("@zddl", data2[i].ZDLB),
                            new SqlParameter("@zdlb", "2"),
                            new SqlParameter("@zdlx", data2[i].ZDLX),
                            new SqlParameter("@zddm", data2[i].JBDM),
                            new SqlParameter("@zdmc", data2[i].JBMC),
                            new SqlParameter("@CreateTime", data2[i].CreateTime),
                            new SqlParameter("@CreatorCode", data2[i].CreatorCode),
                            new SqlParameter("@zt", data2[i].zt),
                            new SqlParameter("@cyqk", data2[i].CYQK),
                            new SqlParameter("@px", data2[i].ZDOrder),
                            });
                    }
                }
                else if (data[0].zs < 1 && data2.Count > 0)
                {
                    var datacount = data[0].zs;
                    for (int i = 0; i < data2.Count; i++)
                    {
                        //i = int.Parse(datacount.ToString());
                        var zdlx = datacount + i;
                        var sql = "insert into [Newtouch_CIS]..zy_PatDxInfo  " +
                            "values(@Id, @OrganizeId, @zyh,@zddl, @zdlb, @zdlx, @zddm, @zdmc, NULL, @CreateTime, @CreatorCode, NULL, NULL, @zt, @cyqk,@px)";

                        this.ExecuteSqlCommand(sql, new SqlParameter[] {
                            new SqlParameter("@Id", data2[i].Id),
                            new SqlParameter("@OrganizeId", data2[i].OrganizeId),
                            new SqlParameter("@zyh", data2[i].ZYH),
                            new SqlParameter("@zddl", data2[i].ZDLB),
                            new SqlParameter("@zdlb", "2"),
                            new SqlParameter("@zdlx", data2[i].ZDLX),
                            new SqlParameter("@zddm", data2[i].JBDM),
                            new SqlParameter("@zdmc", data2[i].JBMC),
                            new SqlParameter("@CreateTime", data2[i].CreateTime),
                            new SqlParameter("@CreatorCode", data2[i].CreatorCode),
                            new SqlParameter("@zt", data2[i].zt),
                            new SqlParameter("@cyqk", data2[i].CYQK),
                            new SqlParameter("@px", data2[i].ZDOrder),
                            });
                    }
                }

            }
            catch (Exception ex)
            {

                throw new FailedException("异常：" + ex.Message);
            }
        }

        public List<ButtonEnableVO> sqlchaxun(string zyh, string orgId)
        {
            try
            {
                var sqlcx = "select count(zyh) zs from [Newtouch_CIS]..zy_PatDxInfo where zyh=@zyh and zt='1' and OrganizeId=@orgId";
                return this.FindList<ButtonEnableVO>(sqlcx, new SqlParameter[] {
                new SqlParameter("@zyh",zyh),
                new SqlParameter("@orgId", orgId)});
            }
            catch (Exception ex)
            {

                throw new FailedException("异常：" + ex.Message);
            }

        }

        public List<ButtonEnableVO> DiagnosticSave(string Code, string orgId)
        {
            try
            {
                var sql = "select Value,CreateTime,LastModifyTime from [Newtouch_EMR].[dbo].[Sys_Config] nolock where Code=@Code and OrganizeId=@orgId ";
                return this.FindList<ButtonEnableVO>(sql, new SqlParameter[] {
                new SqlParameter("@Code",Code),
                new SqlParameter("@orgId", orgId)});
            }
            catch (Exception ex)
            {

                throw new FailedException("异常：" + ex.Message);
            }
        }

        #endregion

        public string GetYbdmByGh(string rygh)
        {
            string sql = @"  select gjybdm from NewtouchHIS_Base..Sys_Staff where gh=@rygh";

            return FirstOrDefault<string>(sql, new[] { new SqlParameter("@rygh", rygh) });
        }
    }


}
