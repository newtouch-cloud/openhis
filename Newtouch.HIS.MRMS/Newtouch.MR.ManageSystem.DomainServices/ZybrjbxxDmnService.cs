using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.DomainServices
{
    public class ZybrjbxxDmnService : DmnServiceBase, IZybrjbxxDmnService
    {
        public ZybrjbxxDmnService(IDefaultDatabaseFactory databaseFactory)
    : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取病案首页病人基本信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public BabasyVO GetPatBasicInfo_basy(string orgId, string zyh)
        {
            string sql = @"select convert(varchar(10),[syxh]) bah,[OrganizeId],[zyh],[patid],[brxz] fylb,[zybz],[ks] ryks,[bq] rybf,[ryrq],
            [rytj],[rqry],[rqrq],[zy],[mz],[gj],[cs_sheng] csd_s,[cs_shi] csd_qs,[cs_xian] csd_x,[hu_sheng]+[hu_shi] hkdz_s,
            [hu_xian] hkdx,[hu_dz] hkdz,[xian_sheng] xzds,[xian_shi] xzd_qs,[xian_xian] xzdx,[xian_dz] xzdz,convert(varchar(5),[hy]) hyzk
            ,[bje],[lxr],[lxrgx] gx,[lxrdh] lxdh,[lxrdz] lxdz,[cyjdry],[cyjdrq],[cyrq],convert(varchar(2),[cyzd]) cyzd,[cybq],
            [lxrjtdh],[lxrWebchat],[lxrEmail],[lxr2],[lxrgx2],[lxryddh2],[lxrjtdh2],[lxrWebchat2],[lxrEmail2],[lxrdz2],[gms],
            [ys],[doctor] ryys,[kh] jkkh,[CardType],[CardTypeName],[jkjl],[cw],[rybq],ryzd,[xm] brxm,[xb],[blh]
            ,convert(varchar(10),csny,120)[csny],[zjh] sfzhm,[zjlx],convert(varchar(2),[nl]) nl,[brly],[nlshow],dh xzz_lxdh,dwmc,ryzd_jbbm,cyzdmc,
            b.code yljgdm,b.name yljgmc,sjzyts
            from [dbo].[V_ZY_PatList]  a  with(nolock)
            left join  [NewtouchHIS_Base].[dbo].[Sys_Organize] b with(nolock) on a.OrganizeId=b.ID
            where a.OrganizeId=@orgId and zyh=@zyh and a.zt=1 ";

            BabasyVO ety = this.FindList<BabasyVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@zyh", zyh)
            }).FirstOrDefault();

            sql = @"select name zy,name gx from  [NewtouchHIS_Base].[dbo].[Sys_ItemsDetail]  m with(nolock)
                 where exists(select 1 from [NewtouchHIS_Base].[dbo].[Sys_Items] n with(nolock) 
                    where n.code=@code and m.itemid=n.id and n.zt=1) and m.code=@detcode";

            if (ety != null && !string.IsNullOrWhiteSpace(ety.zy))
            {
                BabasyVO itemzy = this.FindList<BabasyVO>(sql, new SqlParameter[] {
                    new SqlParameter("@code", "Profession"),
                    new SqlParameter("@detcode", ety.zy)
                }).FirstOrDefault();

                if (itemzy != null)
                {
                    ety.zy = itemzy.zy;
                }
            }

            if (ety != null && !string.IsNullOrWhiteSpace(ety.gx))
            {
                BabasyVO itemgx = this.FindList<BabasyVO>(sql, new SqlParameter[] {
                    new SqlParameter("@code", "RelativeType"),
                    new SqlParameter("@detcode", ety.gx)
                }).FirstOrDefault();

                if (itemgx != null)
                {
                    ety.gx = itemgx.gx;
                }
            }

            sql = null;
            return ety;
        }
    }
}
