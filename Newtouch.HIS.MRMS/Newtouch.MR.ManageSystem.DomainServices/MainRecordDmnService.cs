using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.MR.ManageSystem.Domain.DTO.InputDto;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.Core.Common.Exceptions;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Infrastructure;
using FrameworkBase.MultiOrg.Domain.IRepository;
using System.Reflection;
using Newtouch.MR.ManageSystem.Domain.DTO;
using System.IO;
using System.Data;
using System.Collections;
using System.Web;
using Newtouch.Core.Common.Utils;

namespace Newtouch.MR.ManageSystem.DomainServices
{
    public class MainRecordDmnService: DmnServiceBase, IMainRecordDmnService
    {
#pragma warning disable CS0649 // 从未对字段“MainRecordDmnService._MrbasyzdRepo”赋值，字段将一直保持其默认值 null
        private readonly IMrbasyzdRepo _MrbasyzdRepo;
#pragma warning restore CS0649 // 从未对字段“MainRecordDmnService._MrbasyzdRepo”赋值，字段将一直保持其默认值 null
#pragma warning disable CS0649 // 从未对字段“MainRecordDmnService._MrbasyssRepo”赋值，字段将一直保持其默认值 null
        private readonly IMrbasyssRepo _MrbasyssRepo;
#pragma warning restore CS0649 // 从未对字段“MainRecordDmnService._MrbasyssRepo”赋值，字段将一直保持其默认值 null
#pragma warning disable CS0649 // 从未对字段“MainRecordDmnService._MrbasyRepo”赋值，字段将一直保持其默认值 null
        private readonly IMrbasyRepo _MrbasyRepo;
#pragma warning restore CS0649 // 从未对字段“MainRecordDmnService._MrbasyRepo”赋值，字段将一直保持其默认值 null
#pragma warning disable CS0169 // 从不使用字段“MainRecordDmnService._EMRDmnService”
        private readonly IEMRDmnService _EMRDmnService;
#pragma warning restore CS0169 // 从不使用字段“MainRecordDmnService._EMRDmnService”
#pragma warning disable CS0649 // 从未对字段“MainRecordDmnService._MrbasyrelcodeRepo”赋值，字段将一直保持其默认值 null
        private readonly IMrbasyrelcodeRepo _MrbasyrelcodeRepo;
#pragma warning restore CS0649 // 从未对字段“MainRecordDmnService._MrbasyrelcodeRepo”赋值，字段将一直保持其默认值 null
        private readonly ICommonDmnService _CommonDmnService;

        public MainRecordDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<BasyVO>PatMainList(Pagination pagination,string orgId, string cyksrq,string cyjsrq,string bazt,string bq,string keyword,string sfzh,string cykb)
        {
            var mrlist = _MrbasyRepo.FindList(p => p.OrganizeId == orgId && p.zt == "1",pagination);
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));

            string sql = @"select a.Id,patId,b.ItemName YLFKFS,a.JKKH,a.ZYCS,a.BAH,a.ZYH,a.XM,a.XB,a.NL,a.RYTJ,convert(varchar(20),a.RYSJ,120)RYSJ,convert(varchar(20),a.CYSJ,120)CYSJ,
RYKB,CYKB,a.MZZD,a.bazt,datediff(dd,a.CYSJ,getdate()) cyts,CYCH,RYCH,GDRQ,c.RecordStu,CONVERT(varchar(20), c.CommitTime, 23) CommitTime
from mr_basy a with(nolock)
left join [dbo].[mr_dic_common] b with(nolock) on a.YLFKFS=ItemCode and b.RlueCode ='YLFKFS' and a.organizeid=b.organizeid and b.zt='1'
left join [Newtouch_EMR].[dbo].[zy_brjbxx] c
on a.ZYH=c.zyh and c.zt='1' and a.OrganizeId=c.OrganizeId
where a.organizeid=@orgId and a.zt='1'
";
            if(!string.IsNullOrWhiteSpace(cyksrq)&&!string.IsNullOrWhiteSpace(cyjsrq))
            {
                sql += " and a.CYSJ >= @ksrq and a.CYSJ < @jsrq ";
                string ksrq = Convert.ToDateTime(cyksrq).ToString("yyyy-MM-dd");
                string jsrq = Convert.ToDateTime(cyjsrq).AddDays(1).ToString("yyyy-MM-dd");
                para.Add(new SqlParameter("@ksrq", ksrq));
                para.Add(new SqlParameter("@jsrq", jsrq));
            }
            if(!string.IsNullOrWhiteSpace(bazt))
            {
                sql += " and a.bazt=@bazt ";
                para.Add(new SqlParameter("@bazt", bazt));
            }
            if(!string.IsNullOrWhiteSpace(keyword))
            {
                //sql += " and (charindex(@keyword,a.XM)>0 or a.ZYH=@keyword or a.BAH=@keyword  )";
                sql += " and (charindex(@keyword,a.XM)>0 or a.ZYH=@keyword or a.BAH=@keyword or  a.XM IN (select XM from  [NewtouchHIS_Sett].[dbo].[xt_brjbxx] where py like  ('%" + keyword + "%')) )";
				para.Add(new SqlParameter("@keyword", keyword));
            }
            if (!string.IsNullOrWhiteSpace(sfzh))
            {
	            sql += " and c.sfzh like @sfzh ";
	            para.Add(new SqlParameter("@sfzh", "%"+sfzh+"%"));
            }
            if (!string.IsNullOrWhiteSpace(cykb))
            {
	            sql += " and cykb like @cykb ";
	            para.Add(new SqlParameter("@cykb", "%" + cykb + "%"));
            }
			//if(!string.IsNullOrWhiteSpace(cyts))
			//{
			//    sql += " and datediff(dd,a.CYSJ,getdate())>=@cyts";
			//    para.Add(new SqlParameter("@cyts", cyts));
			//}
			return this.QueryWithPage<BasyVO>(sql,pagination,para.ToArray());
        }

        public BasyVO GetMainRecord(string orgId, string mainId)
        {
            BasyVO ety = new BasyVO();

            //mainId = "65B971B2-8017-4F88-8DB3-A84D009F538D";
            var para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));
            para.Add(new SqlParameter("@mainId", mainId));

            string sql = @" SELECT a.[Id],a.[YLFKFS],a.[JKKH],a.[ZYCS],a.[BAH],a.[PATID],a.[ZYH],a.[XM],a.[XB],convert(varchar(10),CSRQ,120)[CSRQ],a.[NL],a.[GJ],a.[BZYZSNL],a.[XSECSTZ],a.[XSERYTZ]
,a.[CSD],a.[GG],a.[MZ],a.[SFZH],a.[ZY],a.[HY],a.[XZZ],a.[DH],a.[XZZYB],a.[HKDZ],a.[HKDYB],a.[GZDWJDZ],a.[DWDH],a.[DWYB],a.[LXRXM],a.[GX],a.[LXRDZ]
,a.[LXRDH],a.[RYTJ],convert(varchar(20),RYSJ,120)[RYSJ],a.[RYSJS],a.[RYKB],a.[RYBF],a.[ZKKB],convert(varchar(20),CYSJ,120)[CYSJ],a.[CYSJS],a.[CYKB],a.[CYBF],a.[SJZYTS],a.[MZZD],a.[MZZDDM],a.[SSLCLJ]
,a.[QJCS],a.[QJCGCS],a.[QZRQ],a.[ZQSS],a.[BRLY],a.[WBYY],a.[H23],a.[BLZD],a.[BLZDDM],a.[BLH],a.[YWGM],a.[GMYW],a.[SWHZSJ],a.[XX],a.[RH],a.[KZR]
,a.[ZRYS],a.[ZZYS],a.[ZYYS],a.[ZRHS],a.[JXYS],a.[SXYS],a.[BMY],a.[BAZL],a.[ZKYS],a.[ZKHS],convert(varchar(10),ZKRQ,120)[ZKRQ],a.[LYFS],a.[YZZY_YLJG],a.[WSY_YLJG],a.[SFZZYJH]
,a.[MD],a.[RYQ_T],a.[RYQ_XS],a.[RYQ_F],a.[RYH_T],a.[RYH_XS],a.[RYH_F],a.[zt],a.[CreateTime],a.[CreatorCode],a.[LastModifyTime],a.[LastModifierCode]
,a.[LCLJGL],a.[LCBZGL],a.[BZGLFL],a.[BQFX],a.[OrganizeId],a.[CSD_SN],a.[CSD_SI],a.[CSD_QX],a.[CSD_JD],a.[XZZ_SN],a.[XZZ_SI],a.[XZZ_QX],a.[XZZ_JD]
,a.[HKDZ_SN],a.[HKDZ_SI],a.[HKDZ_QX],a.[HKDZ_JD],a.[LXRDZ_SN],a.[LXRDZ_SI],a.[LXRDZ_QX],a.[LXRDZ_JD],a.[RYCH],a.[CYCH],a.[bazt]
,a.[ZFY],a.[ZFJE],a.[YLFUF],a.[BZLZF],a.[ZYBLZHZF],a.[ZLCZF],a.[HLF],a.[QTFY],a.[BLZDF],a.[SYSZDF],a.[YXXZDF],a.[LCZDXMF],a.[FSSZLXMF],a.[WLZLF]
,a.[SSZLF],a.[MAF],a.[SSF],a.[KFF],a.[ZYZLF],a.[ZYZL],a.[ZYWZ],a.[ZYGS],a.[ZCYJF],a.[ZYTNZL],a.[ZYGCZL],a.[ZYTSZL],a.[ZYQT],a.[ZYTSTPJG],a.[BZSS]
,a.[XYF],a.[KJYWF],a.[ZCYF],a.[ZYZJF],a.[ZCYF1],a.[XF],a.[BDBLZPF],a.[QDBLZPF],a.[NXYZLZPF],a.[XBYZLZPF],a.[HCYYCLF],a.[YYCLF],a.[YCXYYCLF],a.[QTF],
b.[YLFKFS] R_YLFKFS,b.[XB] R_XB,b.[GJ] R_GJ,b.[MZ] R_MZ,b.[ZY] R_ZY,b.[HY] R_HY,b.[GX] R_GX,b.[RYTJ] R_RYTJ,b.[RYKB] R_RYKB,b.[RYBF] R_RYBF
,b.[ZKKB] R_ZKKB,b.[CYBF] R_CYBF,b.[CYKB] R_CYKB,b.[BLZDDM] R_BLZDDM,b.[BRLY] R_BRLY,b.[YWGM] R_YWGM,b.[XX] R_XX,b.[RH] R_RH,b.[KZR] R_KZR,b.[ZRYS] R_ZRYS,b.[ZZYS] R_ZZYS,b.[ZYYS] R_ZYYS,b.[ZRHS] R_ZRHS,b.[JXYS] R_JXYS,b.[SXYS] R_SXYS 
,b.[BMY] R_BMY,b.[BAZL] R_BAZL,b.[ZKYS] R_ZKYS,b.[ZKHS] R_ZKHS,b.[LYFS] R_LYFS,b.[SFZZYJH] R_SFZZYJH,b.[BQFX] R_BQFX
  FROM [dbo].[mr_basy] a with(nolock)  
left join [dbo].[mr_basy_rel_code] b with(nolock) on a.organizeid=b.organizeid and a.id=b.syid and a.bah=b.bah 
 
  where a.zt='1' and a.OrganizeId=@orgId and a.id=@mainId ";

            ety = this.FirstOrDefault<BasyVO>(sql, para.ToArray());
            return ety;
        }
        /// <summary>
        /// 数据来源-EMR
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public BasyVO GetMainRecordbybrjbxx(string orgId, string zyh) {
            var mrbasy = _MrbasyRepo.FindEntity(p => p.ZYH == zyh && p.OrganizeId == orgId && p.zt == "1");
            if (mrbasy != null)
            {
                return GetMainRecord(orgId, mrbasy.Id);
            }
            string sql = @"SELECT [Id],[YLFKFS],[JKKH], a.[ZYCS],a.[PATID],[ZYH],[XM],[XB],convert(varchar(10),CSRQ,120)[CSRQ],[NL],[GJ],[BZYZSNL],[XSECSTZ],[XSERYTZ]
,[CSD],[GG],[MZ],[SFZH],[ZY],[HY],[XZZ],[DH],[XZZYB],[HKDZ],[HKDYB],[GZDWJDZ],[DWDH],[DWYB],[LXRXM],[GX],[LXRDZ]
,[LXRDH],[RYTJ],convert(varchar(20),RYSJ,120)[RYSJ],[RYSJS],[RYKB],[RYBF],[ZKKB],convert(varchar(20),CYSJ,120)[CYSJ],[CYSJS],[CYKB],[CYBF],[SJZYTS],[MZZD],[MZZDDM],[SSLCLJ]
,[QJCS],[QJCGCS],[QZRQ],[ZQSS],[BRLY],[WBYY],[H23],[BLZD],[BLZDDM],[BLH],[YWGM],[GMYW],[SWHZSJ],[XX],[RH],[KZR]
,[ZRYS],[ZZYS],[ZYYS],[ZRHS],[JXYS],[SXYS],[BMY],[BAZL],[ZKYS],[ZKHS],convert(varchar(10),ZKRQ,120)[ZKRQ],[LYFS],[YZZY_YLJG],[WSY_YLJG],[SFZZYJH]
,[MD],[RYQ_T],[RYQ_XS],[RYQ_F],[RYH_T],[RYH_XS],[RYH_F],[zt],[CreateTime],[CreatorCode],[LastModifyTime],[LastModifierCode]
,[LCLJGL],[LCBZGL],[BZGLFL],[BQFX],[OrganizeId],[CSD_SN],[CSD_SI],[CSD_QX],[CSD_JD],[XZZ_SN],[XZZ_SI],[XZZ_QX],[XZZ_JD]
,[HKDZ_SN],[HKDZ_SI],[HKDZ_QX],[HKDZ_JD],[LXRDZ_SN],[LXRDZ_SI],[LXRDZ_QX],[LXRDZ_JD],[RYCH],[CYCH],[bazt]
,[ZFY],[ZFJE],[YLFUF],[BZLZF],[ZYBLZHZF],[ZLCZF],[HLF],[QTFY],[BLZDF],[SYSZDF],[YXXZDF],[LCZDXMF],[FSSZLXMF],[WLZLF]
,[SSZLF],[MAF],[SSF],[KFF],[ZYZLF],[ZYZL],[ZYWZ],[ZYGS],[ZCYJF],[ZYTNZL],[ZYGCZL],[ZYTSZL],[ZYQT],[ZYTSTPJG],[BZSS]
,[XYF],[KJYWF],[ZCYF],[ZYZJF],[ZCYF1],[XF],[BDBLZPF],[QDBLZPF],[NXYZLZPF],[XBYZLZPF],[HCYYCLF],[YYCLF],[YCXYYCLF],[QTF],
[R_YLFKFS],[R_XB],[R_GJ],[R_MZ],[R_ZY],[R_HY],[R_GX],[R_RYTJ],[R_RYKB],[R_RYBF],[R_ZKKB],[R_CYBF],[R_CYKB]
,[R_BLZDDM],[R_BRLY],[R_YWGM],[R_XX],[R_RH],[R_KZR],[R_ZRYS],[R_ZZYS],[R_ZYYS],[R_ZRHS],[R_JXYS],[R_SXYS],[R_BMY]
,[R_BAZL],[R_ZKYS],[R_ZKHS],[R_LYFS],[R_SFZZYJH],[R_BQFX],[BAH]
  FROM V_EMR_MrBasy a 
  
where organizeid=@orgId and zyh=@zyh and zt='1' ";

            BasyVO vo = this.FindList<BasyVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@zyh",zyh)
            }).FirstOrDefault();

            vo = GetMainRecordZyFeeDetail(vo, orgId, zyh);
            //if(vo!=null)
            //{
            //    vo.BAH = GetBAH(orgId);
            //}
            //else
            //{
            //    throw new FailedException("未找到患者病历信息，请联系管理员。");
            //}

            if (string.IsNullOrWhiteSpace(vo.BAH))
            {
                throw new FailedException("电子病历病案号异常，请退回。");
            }
            return vo;
        }

        #region 数据来源综合
        public BasyVO GetMainRecordbybrjbxxS(string orgId, string zyh)
        {
            var mrbasy = _MrbasyRepo.FindEntity(p => p.ZYH == zyh && p.OrganizeId == orgId && p.zt == "1");
            if(mrbasy!=null)
            {
                return GetMainRecord(orgId, mrbasy.Id);
            }

            string sql = @"SELECT [USERNAME],[YLFKFS],[JKKH],[ZYCS],[PATID],[ZYH],[XM],[XB],[CSRQ],[NL],[GJ],[BZYZSNL],[MZ]
,[SFZH],[ZY],[HKDZ],[GZDWJDZ],[LXRXM],[GX],[LXRDZ],[LXRDH],[RYTJ],[RYSJ],[RYSJS],[RYKB],[RYBF]
,[CYSJ],[CYSJS],[CYKB],[CYBF],[SJZYTS],[MZZD],[MZZDDM],[OrganizeId],[CSD_SN],[CSD_SI],[CSD_QX],[CSD_JD]
,[XZZ_SN],[XZZ_SI],[XZZ_QX],[XZZ_JD],[HKDZ_SN],[HKDZ_SI],[HKDZ_QX],[HKDZ_JD],[LXRDZ_SN],[LXRDZ_SI]
,[LXRDZ_QX],[LXRDZ_JD],[RYCH],[CYCH]
  FROM [dbo].[V_HIS_InpPatInfo]
where organizeid=@orgId and zyh=@zyh ";

            BasyVO vo= this.FindList<BasyVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@zyh",zyh)
            }).FirstOrDefault();

            if(vo!=null)
            {
                var feevo = GetMainRecordZyFee(orgId, zyh);
                if(feevo!=null)
                {
                    vo.ZFY = feevo.ZFY;
                    vo.ZFJE = feevo.ZFJE;
                }

                vo = GetMainRecordZyFeeDetail(vo,orgId, zyh);

                var ryks = _CommonDmnService.GetDeptList(orgId,vo.RYKB).FirstOrDefault();
                if(ryks!=null)
                {
                    vo.RYKB = ryks.Name;
                }

                if(vo.RYKB!=vo.CYKB)
                {
                    var cyks = _CommonDmnService.GetDeptList(orgId, vo.CYKB).FirstOrDefault();
                    if (cyks != null)
                    {
                        vo.CYKB = cyks.Name;
                    }
                }
                else
                {
                    vo.CYKB = vo.RYKB;
                }
                vo.BAH = zyh;
                //vo.BAH = GetBAH(orgId);
            }

            if(string.IsNullOrWhiteSpace(vo.BAH))
            {
                throw new FailedException("病案号生成失败，请重试。");
            }
            return vo;
        }

        public BasyVO GetMainRecordZyFee(string orgId, string zyh)
        {
            string sql = @"select ZYH,zje ZFY,xjzf ZFJE
from [NewtouchHIS_Sett].[dbo].[zy_js]
where OrganizeId=@orgId and zyh=@zyh
and jszt=1 and zt='1'
";
            BasyVO vo = new BasyVO();
            return this.FindList<BasyVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@zyh",zyh)
            }).FirstOrDefault();
        }

        public BasyVO GetMainRecordZyFeeDetail(BasyVO vo,string orgId,string zyh)
        {
            //同步病案系统费用
           // string sql = @"exec usp_sync_basy_feefromSettbyzyh @orgId=@orgId,@zyh=@zyh ";
            string sql = @"select FeetypeCode,FeetypeName,CONVERT(DECIMAL(13,2),isnull(sum(Amount),0))Amount
from V_HIS_InpPatFee
where OrganizeId=@orgId and  zyh=@zyh
group by FeetypeCode,FeetypeName
union all
select 'ZFY'FeetypeCode,'总费用' FeetypeName,CONVERT(DECIMAL(13,2),isnull(sum(Amount),0))Amount
from V_HIS_InpPatFee
where OrganizeId=@orgId and zyh=@zyh 
and FeetypeCode not in
(
select b.ShortCode from [Newtouch_MRMS].[dbo].[mr_dic_bafeetype] b where b.zt='1' and b.ParentCode is not null and b.ParentCode in(
select a.Code from [Newtouch_MRMS].[dbo].[mr_dic_bafeetype] a where a.zt='1' and a.ParentCode is not null)
)";

            var list = this.FindList<DicFeeDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@zyh",zyh)
            });
            


            Type t = vo.GetType();//获得该类的Type
            foreach (PropertyInfo pi in t.GetProperties())
            {
                var name = pi.Name;//获得属性的名字,后面就可以根据名字判断来进行些自己想要的操作
                var val = list.Find(p => p.FeetypeCode == name);
                if(val!=null)
                {
                    pi.SetValue(vo, val.Amount,null);
                }
            }

            return vo;
        }

        public BasyVO GetMainRecordZyFeeDetailOld(BasyVO vo,string orgId,string zyh)
        {
            string sql = @"select FeetypeCode,FeetypeName,CONVERT(DECIMAL(13,2),isnull(sum(Amount),0))Amount
from V_HIS_InpPatFee
where OrganizeId=@orgId and  zyh=@zyh
group by FeetypeCode,FeetypeName
union all
select 'ZFY'FeetypeCode,'总费用' FeetypeName,CONVERT(DECIMAL(13,2),isnull(sum(Amount),0))Amount
from V_HIS_InpPatFee
where OrganizeId=@orgId and zyh=@zyh ";

            var list = this.FindList<DicFeeDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@zyh",zyh)
            });

            Type t = vo.GetType();//获得该类的Type
            foreach (PropertyInfo pi in t.GetProperties())
            {
                var name = pi.Name;//获得属性的名字,后面就可以根据名字判断来进行些自己想要的操作
                var val = list.Find(p => p.FeetypeCode == name);
                if(val!=null)
                {
                    pi.SetValue(vo, val.Amount,null);
                }
            }

            return vo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="zdlb">诊断类别 1入院诊断2出院诊断</param>
        /// <returns></returns>
        public IList<PatZDListVO> GetPatHisZDInfoS(string bah, string zyh, string orgId, int? zdlb)
        {
            string sql = @"select @bah BAH,OrganizeId,ZYH,convert(int,zdlx) ZDOrder,zddm JBDM ,zdmc JBMC,
convert(varchar(2)," + ((int)EnumRybq.y).ToString() + ") RYBQ,'有' RYBQMS," +
"convert(varchar(2)," + ((int)EnumCyqk.zy).ToString() + @") CYQK,'治愈' CYQKMS
from [Newtouch_CIS].dbo.zy_PatDxInfo with(nolock)
where organizeid=@orgId and zyh=@zyh and zt='1'
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

        public IList<BasyOpDto> GetPatHisOperInfoS(string bah, string zyh, string orgId)
        {
            string sql = @"select @bah BAH,@zyh ZYH, a.OrganizeId,a.ssxh,a.ssdm SSJCZBM,ssmcn SSJCZMC,sskssj SSJCZRQ,e.ssjb SSJB,
max(case when rylb=1 then rygh else null end) SZ,
max(case when rylb=2 and px=1 then rygh else null end)YZ,
max(case when rylb=2 and px=2 then rygh else null end)EZ,
max(case when rylb=5 then rygh else null end)MZYS,
max(case when rylb=1 then ryxm else null end) SZMC,
max(case when rylb=2 and px=1 then ryxm else null end)YZMC,
max(case when rylb=2 and px=2 then ryxm else null end)EZMC,
max(case when rylb=5 then ryxm else null end)MZYSMC,
d.name QKDJ,c.AnesCode MZFS,AnesName MZFSMS 
--select *
from [Newtouch_OR].[dbo].[OR_Registration] a with(nolock)
left join [Newtouch_OR].[dbo].[OR_OpStaffRecord] b with(nolock)on a.organizeid=b.organizeid and a.ssxh=b.ssxh and b.zt='1'
left join [Newtouch_OR].dbo.OR_Anesthesia c with(nolock) on a.organizeid=c.organizeid and a.anescode=c.anescode and c.zt='1'
left join [Newtouch_OR].[dbo].[OR_NotchGrade] d with(nolock) on a.organizeid=d.organizeid and a.qkdj=d.code and d.zt='1'
left join [Newtouch_OR].[dbo].[OR_Operation] e with(nolock) on a.organizeid=e.organizeid and a.ssdm=e.ssdm and e.zt='1'
where a.zt='1' and a.OrganizeId=@orgId and a.zyh=@zyh
group by a.OrganizeId,a.ssxh,a.ssdm,ssmcn,d.name,c.Anescode ,c.AnesName,e.ssjb ,sskssj
order by a.ssxh

";

            return this.FindList<BasyOpDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@zyh",zyh),
                new SqlParameter("@bah", bah)});
        }
        #endregion

        public IList<PatZDListVO> GetDiagLsit(string orgId, string bah, string zyh)
        {
            string zdsql = @"SELECT Id,[BAH],[ZYH],[ZDOrder],[JBDM],[JBMC],[RYBQ],[RYBQMS]
,[CYQK],[CYQKMS]
,[LastModifierCode],[OrganizeId]
  FROM [dbo].[mr_basy_zd] with(nolock)
where zt='1' and OrganizeId=@orgId and BAH=@bah and ZYH=@zyh   and JBDM<>'999999999' 
order by ZDOrder";

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
,QKYHDJ
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
            int flag = 0;
            if(list!=null && list.Count>0)
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
                        if(zdety!=null)
                        {
                            if(zdety.OrganizeId==orgId && zdety.zt=="1")
                            {
                                zdety.RYBQ = v.RYBQ;
                                zdety.RYBQMS = v.RYBQMS;
                                zdety.CYQK = v.CYQK;
                                zdety.CYQKMS = v.CYQKMS;
                                zdety.JBDM = v.JBDM;
                                zdety.JBMC = v.JBMC;
                                zdety.zt = v.zt == null ? zdety.zt : v.zt;

                                if(v.zt=="1")
                                {
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

                    if(flag==0 && v.zt!="0")
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
                        zdety.zt = v.zt;
                        zdety.ZDOrder = i;

                        zdety.Create(true);
                        _MrbasyzdRepo.Insert(zdety);
                    }

                    flag = 0;
                    if(v.zt != "0")
                    {
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

                                if(v.zt=="1")
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

                    if (flag == 0 && v.zt!="0")
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
            if (string.IsNullOrWhiteSpace(vo.PATID))
            {
                msg += "病案号获取失败，请刷新重试；";
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
        public string GetBAH(string orgId)
        {
            string year = DateTime.Now.Year.ToString();
            return EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("mr_basy.bah", orgId, year + "{0:D4}", false);
        }
        public string GetBAHEMR(string orgId)
        {
            string year = DateTime.Now.Year.ToString();
            return EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValueEMR("mr_basy.bah", orgId, year + "{0:D4}", false);
        }


        /// <summary>
        /// 保存患者基本信息
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="orgId"></param>
        public void PatBasicSubmit(BasyVO vo, BasyRelVO relvo, string orgId,string user)
        {
            if(vo!=null && !string.IsNullOrWhiteSpace(orgId))
            {
                string err = PatBasicCheck(vo,relvo,orgId,user);
                if(!string.IsNullOrWhiteSpace(err))
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
                        if(ety!=null)
                        {
                            ety = PatBasyInfoSet(ety, vo, orgId);
                            ety.LastModifyTime = DateTime.Now;
                            ety.LastModifierCode = user;
                            
                            _MrbasyRepo.Update(ety);

                            var findrel = _MrbasyrelcodeRepo.FindEntity(p => p.BAH == ety.BAH && p.SYId == ety.Id && p.OrganizeId == orgId);
                            if(findrel!=null)
                            {
                                relvo.R_YLFKFS = vo.YLFKFS;
                                relety = PatBasyInfoRelSet(findrel, relvo,vo, orgId);
                                _MrbasyrelcodeRepo.Update(relety);

                                string sqlje = @"exec [usp_sync_basy_feefromSettbyzyh] @orgId,@zyh";
                                ExecuteSqlCommand(sqlje, new SqlParameter[] {
                                    new SqlParameter("@orgId",orgId),
                                    new SqlParameter("@zyh",ety.ZYH)
                                });
                            }
                            else
                            {
                                relvo.R_BAH = vo.BAH;
                                relvo.R_YLFKFS = vo.YLFKFS;
                                
                                relety = PatBasyInfoRelSet(relety, relvo,vo, orgId);
                                relety.SYId = ety.Id;
                                relety.Create(true);
                                _MrbasyrelcodeRepo.Insert(relety);
                                
                            }
                        }
                        else
                        {
                            throw new FailedException("未找到该患者病案信息，请刷新重试。");
                        }
                    }
                    else if(ety==null && !string.IsNullOrWhiteSpace(vo.BAH))
                    {
                        vo.bazt = ((int)Enumbazt.lrz).ToString();
                        var pat = GetMainRecordbybrjbxx(orgId, vo.ZYH);
                        if(pat!=null)
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
                            
                            //重写EMR
                            try
                            {
                                string sql = @"update Newtouch_EMR.dbo.mr_basy set BABAH='" + vo.BAH + "',LastModifyTime=getdate(),LastModifierCode='MRMS' where organizeid=@orgId and zyh=@zyh and zt='1'";
                                ExecuteSqlCommand(sql, new SqlParameter[] {
                                    new SqlParameter("@orgId",orgId),
                                    new SqlParameter("@zyh",ety.ZYH)
                                });
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {
                            throw new FailedException("未找到该患者病案信息，请刷新重试。");
                        }
                    }
                    else if(string.IsNullOrWhiteSpace(vo.BAH))
                    {
                        throw new FailedException("病案号生成失败，请重新加载。");
                    }
                    else
                    {
                        throw new FailedException("患者已存在病案信息，请勿重复提交。");
                    }
                }
                catch(FailedException ex)
                {
                    var exmsg = ex.Msg == null ? ex.Message:ex.Msg;
                    throw new FailedException(exmsg);
                }

            }
        }


        public MrbasyEntity PatBasyInfoSet(MrbasyEntity ety,BasyVO vo,string orgId)
        {
            if(ety==null)
            {
                ety = new MrbasyEntity();
            }

            ety.BAH = vo.BAH;
            ety.YLFKFS = vo.YLFKFS;
            ety.JKKH = vo.JKKH;
            ety.ZYCS = vo.ZYCS;
            ety.XM = vo.XM;
            ety.XB = vo.XB;
            ety.PATID = vo.PATID;
            ety.ZYH = vo.ZYH;
            if(!string.IsNullOrWhiteSpace(vo.CSRQ))
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
            return ety;
        }

        public MrbasyrelcodeEntity PatBasyInfoRelSet(MrbasyrelcodeEntity relEty,BasyRelVO relvo,BasyVO vo,string orgId)
        {
            relEty.OrganizeId = orgId;
            relEty.BAH = relvo.R_BAH == null ? relEty.BAH : relvo.R_BAH;
            if (!string.IsNullOrWhiteSpace(vo.YLFKFS))
            {
                var ylfk = _CommonDmnService.DicCommonList(orgId,null, "YLFKFS", vo.YLFKFS).FirstOrDefault();
                relEty.YLFKFS = ylfk.Name;
            }

            if (!string.IsNullOrWhiteSpace(vo.RYTJ))
            {
                Dictionary<string, string> rytjdic = EFDBBaseFuncHelper.GetEnumDescription<EnumRYTJ>();
                EnumRYTJ enumRytj = (EnumRYTJ)Enum.Parse(typeof(EnumRYTJ), vo.RYTJ.ToString());
                relEty.RYTJ =  rytjdic[enumRytj.ToString()];
            }
            if(!string.IsNullOrWhiteSpace(vo.XB))
            {
                Dictionary<string, string> xbdic = EFDBBaseFuncHelper.GetEnumDescription<EnumSex>();
                EnumSex enumSex = (EnumSex)Enum.Parse(typeof(EnumSex), vo.XB.ToString());
                relEty.XB =  xbdic[enumSex.ToString()];
            }
            if (!string.IsNullOrWhiteSpace(vo.BQFX))
            {
                Dictionary<string, string> bqfxdic = EFDBBaseFuncHelper.GetEnumDescription<EnumBqfx>();
                EnumBqfx enumbqfx = (EnumBqfx)Enum.Parse(typeof(EnumBqfx), vo.BQFX.ToString());
                relEty.BQFX = bqfxdic[enumbqfx.ToString()];
            }
            //if (!string.IsNullOrWhiteSpace(vo.LYFS))
            //{
            //    Dictionary<string, string> lyfsdic = EFDBBaseFuncHelper.GetEnumDescription<EnumLyfs>();
            //    EnumLyfs enumlyfs = (EnumLyfs)Enum.Parse(typeof(EnumLyfs), vo.LYFS.ToString());
            //    relEty.LYFS = lyfsdic[enumlyfs.ToString()];
            //}
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
            relEty.BLZDDM = relvo.R_BLZDDM == null ? relEty.BLZDDM : relvo.R_BLZDDM; 
            relEty.BMY = relvo.R_BMY == null ? relEty.BMY : relvo.R_BMY; 
            relEty.BRLY = relvo.R_BRLY == null ? relEty.BRLY : relvo.R_BRLY; 
            relEty.CYBF = relvo.R_CYBF == null ? relEty.CYBF : relvo.R_CYBF; 
            relEty.CYKB = relvo.R_CYKB == null ? relEty.CYKB : relvo.R_CYKB; 
            relEty.GJ = relvo.R_GJ == null ? relEty.GJ: relvo.R_GJ;
            relEty.GX = relvo.R_GX == null ? relEty.GX : relvo.R_GX;
            relEty.HY = relvo.R_HY == null ? relEty.HY : relvo.R_HY;
            relEty.JXYS = relvo.R_JXYS == null ? relEty.JXYS : relvo.R_JXYS; 
            relEty.KZR = relvo.R_KZR == null ? relEty.KZR : relvo.R_KZR; 
            relEty.MZ = relvo.R_MZ==null? relEty.MZ: relvo.R_MZ;
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

            return relEty;
        }

        public IList<PatZDListVO> GetPatHisZDInfo(string bah, string zyh, string orgId, int? zdlb)
        {
            string sql = @"  select @bah BAH,OrganizeId,ZYH,ZDOrder,JBDM ,JBMC,RYBQ,RYBQMS,CYQK,CYQKMS 
  from V_EMR_MrBasyDiag with(nolock) where organizeid=@orgId and zyh=@zyh order by ZDOrder ";
            return this.FindList<PatZDListVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@zyh",zyh),
                new SqlParameter("@bah",bah)});
        }

        public IList<BasyOpDto> GetPatHisOperInfo(string bah, string zyh, string orgId)
        {
            string sql = @"  select @bah BAH,OrganizeId,ZYH,SSOrder,SSJCZBM,SSJCZMC,SSJCZRQ,SSJB,
SZ, SZMC,YZ,YZMC,EZ, EZMC,MZYS, MZYSMC,QKDJ,QKYHLB,MZFS , MZFSMS,QKYHDJ
from  V_EMR_MrBasyOperation with(nolock)
where organizeid=@orgId and zyh=@zyh ";
            return this.FindList<BasyOpDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@zyh",zyh),
                new SqlParameter("@bah",bah)});
        }

        public string MedicalRecordExportQuery(string kssj,string jssj, string orgId)
        {
            try
            {
                var sql = @" exec usp_sync_basy_Exportscv '"+ orgId+"','"+ kssj + "','"+ jssj+"'";
                DataTable dt = this.SqlQueryForDataTatable(sql);
                string TemplatePath = ConfigurationHelper.GetAppConfigValue("MedicalRecordExport");
                string FullFileName = ConfigurationHelper.GetAppConfigValue("MedicalRecordExport") + "N041_武胜瑞康精神病专科医院.csv";
                if (Directory.Exists(TemplatePath))
                {
                    if (File.Exists(FullFileName))
                        File.Delete(FullFileName);
                }
                else
                {
                    Directory.CreateDirectory(TemplatePath);
                }
                FileStream fs = new FileStream(FullFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                string lm = "";
                //列名
                foreach (DataColumn item in dt.Columns)
                {
                    lm += item.ColumnName+",";
                }
                lm = lm.Substring(0, lm.Length - 1);
                sw.WriteLine(lm);
                foreach (DataRow row in dt.Rows)
                {
                    string liststr ="";
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        liststr+= row[i].ToString()+",";
                    }
                    liststr = liststr.Substring(0, liststr.Length - 1);
                    sw.WriteLine(liststr.ToString());
                }
                sw.Close();
                fs.Close();
                return "住院院病案首页表导出成功！";
            }
            catch (Exception ex)
            {
                return "住院院病案首页表导出异常：" + ex.Message;
            }
        }
        public string MedicalRecordExportFYQuery(string kssj, string jssj, string orgId)
        {
            try
            {
                var sql = @" exec usp_sync_basy_Exportscvfy '" + orgId + "','" + kssj + "','" + jssj + "'";
                DataTable dt = this.SqlQueryForDataTatable(sql);
                string TemplatePath = ConfigurationHelper.GetAppConfigValue("MedicalRecordExport");
                string FullFileName = ConfigurationHelper.GetAppConfigValue("MedicalRecordExport")+"N043_武胜瑞康精神病专科医院.csv";
                if (Directory.Exists(TemplatePath))
                {
                    if (File.Exists(FullFileName))
                        File.Delete(FullFileName);
                }
                else
                {
                    Directory.CreateDirectory(TemplatePath);
                }
                FileStream fs = new FileStream(FullFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                string lm = "";
                //列名
                foreach (DataColumn item in dt.Columns)
                {
                    lm += item.ColumnName + ",";
                }
                lm = lm.Substring(0, lm.Length - 1);
                sw.WriteLine(lm);
                foreach (DataRow row in dt.Rows)
                {
                    string liststr = "";
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        liststr += row[i].ToString() + ",";
                    }
                    liststr = liststr.Substring(0, liststr.Length - 1);
                    sw.WriteLine(liststr.ToString());
                }
                sw.Close();
                fs.Close();
                return "医院管理附页表导出成功！";
            }
            catch (Exception ex)
            {
                return "医院管理附页导出异常：" + ex.Message;
            }
        }
        private string StringToBytes(string TheString)
        {
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(TheString);
            string returnstr = Encoding.UTF8.GetString(utf8Bytes);
            return returnstr;
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

        public List<ButtonEnableVO> DiagnosticSave(string Code, string orgId)
        {
            try
            {
                var sql = "select Value,CreateTime,LastModifyTime from [Newtouch_MRMS].[dbo].[Sys_Config] nolock where Code=@Code and OrganizeId=@orgId";
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


    }


}
