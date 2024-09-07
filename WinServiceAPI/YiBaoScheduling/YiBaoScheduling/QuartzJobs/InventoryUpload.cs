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
            Console.WriteLine("【3501】查询当天有无进销存的数据Start...");
            AppLogger.Info("盘点信息上传【3501】开始····");
            RequestHelper.GetInstance().InventoryUpload3501();
            AppLogger.Info("盘点信息上传【3501】结束");
            Console.WriteLine("【3501】查询当天有无进销存的数据END");
            #endregion

            #region 库存变更数据上传
            Console.WriteLine("【3502】查询库存变更数据Start...");
            AppLogger.Info("库存变更信息上传【3502】开始····");
            RequestHelper.GetInstance().InventoryUpload3502();
            AppLogger.Info("库存变更信息上传【3502】结束");
            Console.WriteLine("【3502】查询库存变更数据END");
            #endregion

            #region 商品采购信息上传
            Console.WriteLine("【3503】商品采购信息上传Start...");
            AppLogger.Info("商品采购信息上传【3503】开始····");
            RequestHelper.GetInstance().InventoryUpload3503();
            AppLogger.Info("商品采购信息上传【3503】结束");
            Console.WriteLine("【3503】商品采购信息上传END");
            #endregion

            #region 商品采购退货信息上传
            Console.WriteLine("【3504】商品采购退货信息上传Start...");
            AppLogger.Info("商品采购退货信息上传【3504】开始····");
            RequestHelper.GetInstance().InventoryUpload3504();
            AppLogger.Info("商品采购退货信息上传【3504】结束");
            Console.WriteLine("【3504】商品采购退货信息上传END");
            #endregion

            #region 商品销售信息上传
            Console.WriteLine("【3505】商品销售信息上传Start...");
            AppLogger.Info("商品销售信息上传【3505】开始····");
            RequestHelper.GetInstance().InventoryUpload3505();
            AppLogger.Info("商品销售信息上传【3505】结束");
            Console.WriteLine("【3505】商品销售信息上传END");
            #endregion

            #region 商品销售退货信息上传
            Console.WriteLine("【3506】商品销售退货信息上传Start...");
            AppLogger.Info("商品销售退货信息上传【3506】开始····");
            RequestHelper.GetInstance().InventoryUpload3506();
            AppLogger.Info("商品销售退货信息上传【3506】结束");
            Console.WriteLine("【3506】商品销售退货信息上传END");
            #endregion      

        }
    }
}
