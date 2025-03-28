using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.Entity
{
    
    public class Temporary_ordersEntity :IEntity<Temporary_ordersEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string zyh { get; set; }
        public int? zh { get; set; }
        public string WardCode { get; set; }
        public string DeptCode { get; set; }
        public string ysgh { get; set; }
        public string pcCode { get; set; }
        public int zxcs { get; set; }
        public int zxzq { get; set; }
        public int zxzqdw { get; set; }
        public string zdm { get; set; }
        public string xmdm { get; set; }
        public string xmmc { get; set; }
        public int yzzt { get; set; }
        public string dw { get; set; }
        public int zbbz { get; set; }
        public int sl { get; set; }
        public int dwlb { get; set; }
        public int yzlx { get; set; }
        public string zfysgh { get; set; }
        public DateTime ?  zfsj { get; set; }
        public string zfr { get; set; }
        public DateTime ? shsj { get; set; }
        public string shr { get; set; }
        public DateTime ? sssj { get; set; }
        public string ssr { get; set; }
        public DateTime ? kssj { get; set; }
        public DateTime ? zxsj { get; set; }
        public string zxr { get; set; }
        public System.Decimal ypjl { get; set; }
        public string ypgg { get; set; }
        public string ztnr { get; set; }
        public string yznr { get; set; }
        public string ypyfdm { get; set; }
        public string zxksdm { get; set; }
        public string printyznr { get; set; }
        public DateTime ? CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime  ? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public string hzxm { get; set; }
        public string bw { get; set; }
        public string zxsjd { get; set; }
        public string nlmddm { get; set; }
        public string kssReason { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }
        public string sqlx { get; set; }
        public string bwff { get; set; }
        public string sqdh { get; set; }
        public int ds { get; set; }
        public string yzh { get; set; }
        public string lcyx { get; set; }
        public string sqbz { get; set; }
        public int sqdzt { get; set; }
        public int djbz { get; set; }
        public int cydybz { get; set; }
        public int zzfbz { get; set; }
        public int syncStatus { get; set; }
        public string yztag { get; set; }
        public int isjf { get; set; }

    }
}
