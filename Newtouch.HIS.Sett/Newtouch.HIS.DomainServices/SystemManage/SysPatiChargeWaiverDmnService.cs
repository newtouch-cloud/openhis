using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools.DB;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Newtouch.HIS.DomainServices
{
    public class SysPatiChargeWaiverDmnService
    {
        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        //public IList<SysPatiChargeWaiverVo> GetSysPatiChargeAddVoList(int keyValue)
        //{
        //    StringBuilder strSql = new StringBuilder();

        //    strSql.Append(@"select
        //                j.brsfjmbh,x.brxzmc,d.dlmc,m.sfxmmc,j.jmbl,j.zt,j.jdry,j.jdrq,j.bgbz,j.bgrq
        //                from xt_brsfjm j
        //                left join (select * from xt_brxz where zt = '1' and bgbz = 0) as x on j.brxz = x.brxz
        //                left join (select * from xt_sfdl where zt = '1' and bgbz = 0) as d on j.dl = d.dl
        //                left join (select * from xt_sfxm where zt = '1' and bgbz = 0) as m on j.sfxm = m.sfxm
        //                where 1 = 1  
        //                      and  j.zt='1'

        //                ");
        //    if (keyValue>0)
        //    {
        //        strSql.Append(@"  and x.brxzmc=@keyValue'");
        //        DbParameter[] param =
        //        {
        //            new SqlParameter("@keyValue",keyValue)
        //        };
        //    }

        //    DataTable dt = DbHelper.ExecuteSqlCommand2(strSql.ToString());

        //    return null;
        //}
    }
}

