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
    public sealed class InventoryUpload : JobBase<InventoryUpload>
    {
        public override void Body()
        {
            #region 进销存数据上传
            Console.WriteLine("1.1.1.查询前一天有无进销存的数据Start...");
            var queryCWData = DBHelper.QueryInventory3501();
            AppLogger.Info("盘点信息上传【3501】开始····");
            foreach (var item in queryCWData)
            {
                var result = RequestHelper.GetInstance().InventoryUpload3501(item);
            }
            AppLogger.Info("盘点信息上传【3501】结束");
            #endregion
            #region 库存变更数据上传
            Console.WriteLine("1.1.1.查询库存变更数据Start...");
            var queryUpdataData = DBHelper.QueryInventory3502();
            AppLogger.Info("库存变更信息上传【3502】开始····");
            foreach (var item in queryUpdataData)
            {
                var result = RequestHelper.GetInstance().InventoryUpload3502(item);
            }
            AppLogger.Info("库存变更信息上传【3502】结束");
            #endregion
            #region 商品采购信息上传
            Console.WriteLine("1.1.1.商品采购信息上传Start...");
            var queryPurcinfoData = DBHelper.QueryPurcinfo3503();
            AppLogger.Info("商品采购信息上传【3503】开始····");
            foreach (var item in queryPurcinfoData)
            {
                var result = RequestHelper.GetInstance().InventoryUpload3503(item);
            }
            AppLogger.Info("商品采购信息上传【3503】结束");
            #endregion
            #region 商品采购退货信息上传
            Console.WriteLine("1.1.1.商品采购退货信息上传Start...");
            var queryRetreatData = DBHelper.QueryPurcinfo3504();
            AppLogger.Info("商品采购退货信息上传【3504】开始····");
            foreach (var item in queryRetreatData)
            {
                var result = RequestHelper.GetInstance().InventoryUpload3504(item);
            }
            AppLogger.Info("商品采购退货信息上传【3504】结束");
            #endregion
        }
    }
}
