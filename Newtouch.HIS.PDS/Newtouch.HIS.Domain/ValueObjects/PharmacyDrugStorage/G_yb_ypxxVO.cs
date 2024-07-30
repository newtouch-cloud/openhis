using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    /// <summary>
    /// 医保药品目录表
    /// </summary>
    public class G_yb_ypxxVO
    {
        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypdm { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        public string sjly { get; set; }
        /// <summary>
        /// 注册名称
        /// </summary>
        public string zcmc { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string spmc { get; set; }
        /// <summary>
        /// 注册剂型
        /// </summary>
        public string zcjx { get; set; }
        /// <summary>
        /// 实际剂型
        /// </summary>
        public string sjjx { get; set; }
        /// <summary>
        /// 注册规格
        /// </summary>
        public string zcgg { get; set; }
        /// <summary>
        /// 实际规格
        /// </summary>
        public string sjgg { get; set; }
        /// <summary>
        /// 包装材质
        /// </summary>
        public string bzcz { get; set; }
        /// <summary>
        /// 最小包装数量
        /// </summary>
        public string zxbzsl { get; set; }
        /// <summary>
        /// 最小制剂单位
        /// </summary>
        public string zxzjdw { get; set; }
        /// <summary>
        /// 最小包装单位
        /// </summary>
        public string zxbzdw { get; set; }
        /// <summary>
        /// 药品企业
        /// </summary>
        public string ypqy { get; set; }
        /// <summary>
        /// 上市药品持有人
        /// </summary>
        public string shypcyr { get; set; }
        /// <summary>
        /// 批准文号
        /// </summary>
        public string pzwh { get; set; }
        /// <summary>
        /// 原批准文号
        /// </summary>
        public string ypzwh { get; set; }
        /// <summary>
        /// 药品本位码
        /// </summary>
        public string ypbwm { get; set; }
        /// <summary>
        /// 分包装企业名称
        /// </summary>
        public string fbzqymc { get; set; }
        /// <summary>
        /// 生产企业
        /// </summary>
        public string scqy { get; set; }
        /// <summary>
        /// 市场状态
        /// </summary>
        public string sczt { get; set; }
        /// <summary>
        /// 医保药品名称
        /// </summary>
        public string ybypmc { get; set; }
        /// <summary>
        /// 2021版甲乙类
        /// </summary>
        public string ybjyl { get; set; }
        /// <summary>
        /// 医保剂型
        /// </summary>
        public string ybjx { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string bh { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 甲乙类
        /// </summary>
        public string jyl { get; set; }
        /// <summary>
        /// 备注1
        /// </summary>
        public string bz1 { get; set; }
        /// <summary>
        /// 备注（特殊属性标识）
        /// </summary>
        public string bztssxbs { get; set; }
        /// <summary>
        /// 医保支付标准
        /// </summary>
        public string ybzfbz { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        public string pym { get; set; }
    }


    public class ypxxlist
    {
        public string ypid { get; set; }
    }
}
