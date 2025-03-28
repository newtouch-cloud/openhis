using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicinDmnService : DmnServiceBase, ISysMedicinDmnService
    {
        public SysMedicinDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 检索药品（for 下拉）
        /// </summary>
        /// <param name="keywrod"></param>
        /// <returns></returns>
        public IList<SysMedicinSimpleInfoVO> GetYpList(string keywrod)
        {
            //            var sql = @" select  distinct yp1.yp,yp1.ypmc,yp1.py,yp1.zfbl,yp1.zfxz,yp1.dl,sfdl.dlmc,yp1.ycmc 
            //  ,ypsx.ybdm   from  xt_yp as yp1 
            // left join xt_sfdl as sfdl on  yp1.dl=sfdl.dl and sfdl.zt='1' 
            // inner join xt_ypsx as ypsx on  ypsx.ypCode=yp1.ypCode 
            // WHERE 1=1 and yp1.zt='1' and yp1.mzzybz='1' 
            //and (@keywrod = '' or yp1.yp like @searchkeywrod or yp1.ypmc like @searchkeywrod or yp1.py like @upperkeywrod)
            // order by yp1.yp desc";
            //            var paras = new SqlParameter[] {
            //                new SqlParameter("@keywrod", keywrod ?? ""),
            //                new SqlParameter("@searchkeywrod", "%" + (keywrod ?? "") + "%"),
            //                new SqlParameter("@upperkeywrod", (keywrod ?? "").ToUpper()),
            //            };
            //            return this.FindList<SysMedicinSimpleInfoVO>(sql, paras);
            return null;
        }

    }
}
