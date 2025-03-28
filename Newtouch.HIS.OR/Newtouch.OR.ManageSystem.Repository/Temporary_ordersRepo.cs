using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Repository
{
   public class Temporary_ordersRepo : RepositoryBase<Temporary_ordersEntity>, ITemporary_ordersERepo
    {
        public Temporary_ordersRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int Submitlsyz(Temporary_ordersEntity entity, string keyVaue)
        {

            string sql = string.Format(@"
insert into [Newtouch_CIS].[dbo].[zy_lsyz] 
(Id, OrganizeId, zyh, zh, WardCode, DeptCode, ysgh, pcCode, zxcs, zxzq, 
zxzqdw, zdm, xmdm, xmmc, yzzt, dw, zbbz, sl, dwlb, yzlx, zfysgh, zfsj, 
zfr, shsj, shr, sssj, ssr, kssj, zxsj, zxr, ypjl, ypgg, ztnr, yznr, ypyfdm, 
zxksdm, printyznr, CreateTime, CreatorCode, LastModifyTime, LastModifierCode, 
zt, hzxm, bw, zxsjd, nlmddm, kssReason, ztId, ztmc, sqlx, bwff, sqdh, ds, yzh, 
lcyx, sqbz, sqdzt, djbz, cydybz, zzfbz, syncStatus, yztag, isjf)
values(
newid(),'"+ entity.OrganizeId + "', '" + entity.zyh + "', NULL, '" + entity.WardCode + "','" + entity.DeptCode + "', '" + entity.ysgh + "', '" + entity.pcCode + "', '" + entity.zxcs + "', '" + entity.zxzq + "'," +
"'"+entity.zxzqdw+ "',NULL, '" + entity.xmdm+"', '"+entity.xmmc+"', '"+entity.yzzt+ "', NULL, '" + entity.zbbz+"', '"+entity.sl+"', '"+entity.dwlb+"', '"+entity.yzlx+ "',NULL,NULL,"+
"'"+entity.zfr+"',NULL, '"+entity.shr+"', '"+entity.sssj+"', '"+entity.ssr+"', '"+entity.kssj+"', NULL,NULL, '" + entity.ypjl+ "', NULL, '" + entity.ztnr+"', '"+entity.yznr+ "',NULL," +
"NULL,NULL, '" + entity.CreateTime+"', '"+entity.CreatorCode+"', '"+entity.LastModifyTime+"', '"+entity.LastModifierCode+"',"+
"'"+entity.zt+"', '"+entity.hzxm+"', '"+entity.bw+ "', NULL,NULL,NULL, NULL, NULL,NULL, NULL, '"+entity.sqdh+"', NULL, '" + entity.yzh+"',"+
"NULL,NULL, NULL, NULL,NULL,NULL, NULL, NULL,'0')");

           var count= ExecuteSqlCommand(sql);
            

            return count;
        }
    }
}
