using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.IRepository.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.OutpatientManage
{
    public class OutBookDateRepo : RepositoryBase<OutBookDateEntity>, IOutBookDateRepo
    {
        public OutBookDateRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        //排班时间 新增/更新
        public int UpdateDate(OutBookArrangeVO entity, int ghpbId, string weekdd, string orgId, string User, DateTime Time,int ghpbIdNew)
        {
            var period = 0;var totalNum = 0;var weekddName = "";var totalNum1 = 0;
            if (weekdd == "1") {
                period = entity.zyi;
                totalNum = entity.hy1;
                totalNum1 = entity.hy21;
                weekddName = "星期一";
            }
            else if (weekdd == "2")
            {
                period = entity.zer;
                totalNum = entity.hy2;
                totalNum1 = entity.hy22;
                weekddName = "星期二";
            }
            else if (weekdd == "3")
            {
                period = entity.zsan;
                totalNum = entity.hy3;
                totalNum1 = entity.hy23;
                weekddName = "星期三";
            }
            else if (weekdd == "4")
            {
                period = entity.zsi;
                totalNum = entity.hy4;
                totalNum1 = entity.hy24;
                weekddName = "星期四";
            }
            else if (weekdd == "5")
            {
                period = entity.zwu;
                totalNum = entity.hy5;
                totalNum1 = entity.hy25;
                weekddName = "星期五";
            }
            else if (weekdd == "6")
            {
                period = entity.zliu;
                totalNum = entity.hy6;
                totalNum1 = entity.hy26;
                weekddName = "星期六";
            }
            else if (weekdd == "7")
            {
                period = entity.zri;
                totalNum = entity.hy7;
                totalNum1 = entity.hy27;
                weekddName = "星期日";
            }
            if (ghpbId == 0)
            {//新增
                //获取排班编号
                if (entity.sjd != null && entity.sjd != "")
                {
                    var dbEntity = new OutBookDateEntity();
                    dbEntity.OrganizeId = entity.OrganizeId;
                    dbEntity.ghpbId = ghpbId;
                    dbEntity.Weekdd = weekdd;
                    dbEntity.Weekddname = weekddName;
                    dbEntity.Period = 2;
                    dbEntity.TotalNum = totalNum;
                    dbEntity.IsBook = entity.isBook;
                    dbEntity.zt = entity.zt;
                    dbEntity.CreatorCode = User;
                    dbEntity.CreateTime = Convert.ToDateTime(Time);
                    dbEntity.LastModifyTime = null;
                    dbEntity.LastModifierCode = null;
                    dbEntity.px = 1;
                    dbEntity.timeslot = entity.sjd;
                    return Insert(dbEntity);
                }
                else { 
                var dbEntity = new OutBookDateEntity();
                dbEntity.OrganizeId = entity.OrganizeId;
                dbEntity.ghpbId = ghpbIdNew;
                dbEntity.Weekdd = weekdd;
                dbEntity.Weekddname = weekddName;
                dbEntity.Period = 0;
                dbEntity.TotalNum = totalNum;
                dbEntity.IsBook = entity.isBook;
                dbEntity.zt = "1";
                dbEntity.CreatorCode = User;
                dbEntity.CreateTime = Convert.ToDateTime(Time);
                dbEntity.LastModifyTime = null;
                dbEntity.LastModifierCode = null;
                dbEntity.px = 1;
                Insert(dbEntity);
                var dbEntitylast = new OutBookDateEntity();
                dbEntitylast.OrganizeId = entity.OrganizeId;
                dbEntitylast.ghpbId = ghpbIdNew;
                dbEntitylast.Weekdd = weekdd;
                dbEntitylast.Weekddname = weekddName;
                dbEntitylast.Period = 1;
                dbEntitylast.TotalNum = totalNum1;
                dbEntitylast.IsBook = entity.isBook;
                dbEntitylast.zt = "1";
                dbEntitylast.CreatorCode = User;
                dbEntitylast.CreateTime = Convert.ToDateTime(Time);
                dbEntitylast.LastModifyTime = null;
                dbEntitylast.LastModifierCode = null;
                dbEntitylast.px = 1;
                return Insert(dbEntitylast);
                }
            }
            else
            {//修改时根据星期是否存在执行更新/新增
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.ghpbId == ghpbId && p.Weekdd == weekdd))
                {//更新
                    //获取排班编号
                    if (entity.sjd != null && entity.sjd != "")
                    {
                        var dbEntity = new OutBookDateEntity();
                        dbEntity.OrganizeId = entity.OrganizeId;
                        dbEntity.ghpbId = ghpbId;
                        dbEntity.Weekdd = weekdd;
                        dbEntity.Weekddname = weekddName;
                        dbEntity.Period = 2;
                        dbEntity.TotalNum = totalNum;
                        dbEntity.IsBook = entity.isBook;
                        dbEntity.zt = entity.zt;
                        dbEntity.CreatorCode = User;
                        dbEntity.CreateTime = Convert.ToDateTime(Time);
                        dbEntity.LastModifyTime = null;
                        dbEntity.LastModifierCode = null;
                        dbEntity.px = 1;
                        dbEntity.timeslot = entity.sjd;
                        return Insert(dbEntity);
                    }
                    else
                    {
                        var dbEntity = new OutBookDateEntity();
                        dbEntity.OrganizeId = entity.OrganizeId;
                        dbEntity.ghpbId = ghpbId;
                        dbEntity.Weekdd = weekdd;
                        dbEntity.Weekddname = weekddName;
                        dbEntity.Period = period;
                        dbEntity.TotalNum = totalNum;
                        dbEntity.IsBook = entity.isBook;
                        dbEntity.zt = "1";
                        dbEntity.CreatorCode = User;
                        dbEntity.CreateTime = Convert.ToDateTime(Time);
                        dbEntity.LastModifyTime = null;
                        dbEntity.LastModifierCode = null;
                        dbEntity.px = 1;
                        Insert(dbEntity);
                        var dbEntitylast = new OutBookDateEntity();
                        dbEntitylast.OrganizeId = entity.OrganizeId;
                        dbEntitylast.ghpbId = ghpbId;
                        dbEntitylast.Weekdd = weekdd;
                        dbEntitylast.Weekddname = weekddName;
                        dbEntitylast.Period = 1;
                        dbEntitylast.TotalNum = totalNum1;
                        dbEntitylast.IsBook = entity.isBook;
                        dbEntitylast.zt = "1";
                        dbEntitylast.CreatorCode = User;
                        dbEntitylast.CreateTime = Convert.ToDateTime(Time);
                        dbEntitylast.LastModifyTime = null;
                        dbEntitylast.LastModifierCode = null;
                        dbEntitylast.px = 1;
                        return Insert(dbEntitylast);
                    }
                }
                else
                {//新增
                    if (entity.sjd!=null&& entity.sjd!="")
                    {
                        var dbEntity = new OutBookDateEntity();
                        dbEntity.OrganizeId = entity.OrganizeId;
                        dbEntity.ghpbId = ghpbId;
                        dbEntity.Weekdd = weekdd;
                        dbEntity.Weekddname = weekddName;
                        dbEntity.Period = 2;
                        dbEntity.TotalNum = totalNum;
                        dbEntity.IsBook = entity.isBook;
                        dbEntity.zt = entity.zt;
                        dbEntity.CreatorCode = User;
                        dbEntity.CreateTime = Convert.ToDateTime(Time);
                        dbEntity.LastModifyTime = null;
                        dbEntity.LastModifierCode = null;
                        dbEntity.px = 1;
                        dbEntity.timeslot = entity.sjd;
                        return Insert(dbEntity);
                    }
                    else { 
                    var dbEntity = new OutBookDateEntity();
                    dbEntity.OrganizeId = entity.OrganizeId;
                    dbEntity.ghpbId = ghpbId;
                    dbEntity.Weekdd = weekdd;
                    dbEntity.Weekddname = weekddName;
                    dbEntity.Period = 0;
                    dbEntity.TotalNum = totalNum;
                    dbEntity.IsBook = entity.isBook;
                    dbEntity.zt = entity.zt;
                    dbEntity.CreatorCode = User;
                    dbEntity.CreateTime = Convert.ToDateTime(Time);
                    dbEntity.LastModifyTime = null;
                    dbEntity.LastModifierCode = null;
                    dbEntity.px = 1;
                    Insert(dbEntity);
                    var dbEntitylast = new OutBookDateEntity();
                    dbEntitylast.OrganizeId = entity.OrganizeId;
                    dbEntitylast.ghpbId = ghpbId;
                    dbEntitylast.Weekdd = weekdd;
                    dbEntitylast.Weekddname = weekddName;
                    dbEntitylast.Period = 1;
                    dbEntitylast.TotalNum = totalNum1;
                    dbEntitylast.IsBook = entity.isBook;
                    dbEntitylast.zt = "1";
                    dbEntitylast.CreatorCode = User;
                    dbEntitylast.CreateTime = Convert.ToDateTime(Time);
                    dbEntitylast.LastModifyTime = null;
                    dbEntitylast.LastModifierCode = null;
                    dbEntitylast.px = 1;
                    return Insert(dbEntitylast);
                    }
                }
            }
        }
    }
}
