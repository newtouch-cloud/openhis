using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBaoScheduling.Common;
using YiBaoScheduling.Model;
using YiBaoScheduling.Proxy;

namespace YiBaoScheduling.QuartzJobs
{
    public sealed class YBUpdateCW:JobBase<YBUpdateCW>
    {
        public override void Body()
        {
            #region 自动判断床号更新入院登记
            Console.WriteLine("1.1.1.查询是否有差异记录Start...");
            var queryCWData = DBHelper.QueryCWCY();
            AppLogger.Info("入院登记床号修改【2403】开始····");
            foreach (var item in queryCWData)
            {
                var result = RequestHelper.GetInstance().BedModification(item);
            }
            AppLogger.Info("入院登记床号修改【2403】结束");
            #endregion
        }
    }
}
