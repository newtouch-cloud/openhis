using HIS.SSO.Domain.Entity.SysManage;
using HIS.SSO.Domain.IDomainServices;
using HIS.SSO.Domain.Model.SysManage;
using HIS.SSO.Domain.ValueObjects.SysManage;
using Mapster;
using Microsoft.Data.SqlClient;
using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Model;
using System.Data.Common;

namespace HIS.SSO.DomainService
{
    public class ShortcutMenuDmnService : BaseDmnService<SysShortcutMenuEntity>, IShortcutMenuDmnService
    {
        /// <summary>
        /// 快捷菜单query
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
		public async Task<List<SysShortcutMenuEntity>> GetShortcutMenuList(string userCode, string orgId)
        {
            var result = await GetByWhereWithAttr<SysShortcutMenuEntity>(p => p.zt == "1" && p.OrganizeId == orgId && p.CreatorCode == userCode);
            return result;
        }
        /// <summary>
        /// 功能快捷菜单add
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
		public async Task<BusResult<string>> SaveShortcutMenu(ShorcutMenuModel request)
        {
            var exists = await GetByWhere(p => p.MenuName == request.MenuName && p.CreatorCode==request.CreatorCode && p.zt == "1");
            if (exists != null && exists.Count > 0)
            {
                return new BusResult<string> { code = ResponseResultCode.FAILOfExists, msg = "功能菜单已存在" };
            }
            var newEty = request.Adapt<SysShortcutMenuEntity>();
            newEty.NewEntity(request.OrganizeId, request.CreatorCode);
            newEty.Id = Guid.NewGuid().ToString();
            var result = await Add(newEty);
            if (result > 0)
            {
                return new BusResult<string> { code = ResponseResultCode.SUCCESS, Data = newEty.Id };
            }
            return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "操作失败，请刷新重试" };
        }

        public async Task<List<HomeDataTotalVo>> GetDutyTotalList(string dutycode, DateTime tjrq, string usercode, string orgId, bool isAdmin)
        {//暂用sql语句
            string dbstr = "";
            string sql = "";
            switch (dutycode)
            {
                case "tollman":
                    dbstr = DBEnum.SettDb.ToString();
                    sql = @"select b.brxzmc brxz,count(distinct(case when jslx ='0' then  ghnm  end)) ghrc,
count(distinct(case when jslx ='0' and jszt='2'  then (ghnm)  end)) thrc,
sum(zje) jsje,
sum(case  when jszt='2'  then (zje)  end) tfje
from mz_js a with(nolock) ,xt_brxz b with(nolock)
where a.brxz=b.brxz and a.OrganizeId =b.OrganizeId and  a.zt='1' and  a.OrganizeId=@orgId and a.CreateTime>=@tjrq ";
                    if (isAdmin)
                    {
                        sql += " and a.CreatorCode=@user ";
                    }
                    sql += " group by b.brxzmc;";
                    break;

                case "Doctor":
                    dbstr = DBEnum.CisDb.ToString();
                    sql = @"select b.brxzmc brxz,count(distinct a.jzId) jzrc,
sum(zje) kfje,
sum(case when sfbz='1' then zje end) jsje
from xt_cf a with(nolock) ,xt_jz b  with(nolock)
where  a.jzId=b.jzId and a.zt='1' and  a.OrganizeId=@orgId and a.CreateTime>=@tjrq ";
                    if (isAdmin)
                    {
                        sql += " and a.CreatorCode=@user ";
                    }
                    sql += " group by b.brxzmc;";
                    break;
                case "Nurse":
                    dbstr = DBEnum.SettDb.ToString();
                    sql = @" select b.brxzmc brxz, 
(select count(case when zybz=1 then zyh  end  )  from zy_brjbxx where convert(varchar(10),CreateTime,121)=@tjrq and zt='1' and zyxx.brxz=brxz ) yrqrc,
(select count(case when zybz=0 then zyh  end)  from zy_brjbxx where convert(varchar(10),CreateTime,121)=@tjrq and zt='1' and zyxx.brxz=brxz) wrqrc,
sum(case when zybz  in ('0','1','7') then 1 end) jrwcqrc,
sum(case when convert(varchar(10),cyrq,121)=@tjrq then 1 else 0 end) jrcqrc,
(select count(1)  from NewtouchHIS_Base.[dbo].[V_S_xt_cw] where zt='1') zcw,
(select sum( case when sfzy=1 then 0 else 1 end) sycw from NewtouchHIS_Base.[dbo].[V_S_xt_cw] where zt='1') sycw
from zy_brjbxx zyxx with(nolock)  ,xt_brxz b with(nolock)
where zyxx.brxz=b.brxz and zyxx.OrganizeId=b.OrganizeId and zyxx.zt='1' and zyxx.OrganizeId=@orgId 
group by b.brxzmc,zyxx.brxz;";
                    break;
            }
            var result = await GetListBySqlQuery<HomeDataTotalVo>(dbstr, sql, new List<DbParameter>() {
                new SqlParameter("@orgId",orgId) ,
                new SqlParameter("@tjrq",tjrq.ToString("yyyy-MM-dd")),
                new SqlParameter("@user",usercode??"")
            });

            return result;

        }

    }
}
