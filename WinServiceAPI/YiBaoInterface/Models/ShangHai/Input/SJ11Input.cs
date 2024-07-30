using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Input
{
    public class SJ11Input:InputBase
    {
        /// <summary>
        /// 凭证类别 0：磁卡，1：保障卡，3：电子凭证
        /// </summary>
        public string cardtype { get; set; }
        /// <summary>
        /// 凭证码 字符串63 非空 磁卡：28 位，保障卡：不填 电子凭证：填写令牌
        /// </summary>
        public string carddata { get; set; }
        /// <summary>
        /// 科室编码 字符串 50 非空 见字典表
        /// </summary>
        public string deptid { get; set; }

        /// <summary>
        /// 登记类别 字符串 1 非空 1：家床建床 2：急观入观 3：入院登记 4：大病登记 6：保健对象急观 7：保健对象入院 0：门诊登记
        /// </summary>
        public string djtype { get; set; }
        /// <summary>
        /// 登记号急观：填急观号，住院： 填住院号，家床：填空格，大病：填空格
        /// </summary>

        public string djno { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public string startdate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string enddate { get; set; }
        /// <summary>
        /// 诊断编码循环体开始
        /// </summary>
        public List<Zdnos> zdnos { get; set; }

        /// <summary>
        /// 大病项目 可空
        /// </summary>
        public string dbxm { get; set; }

        /// <summary>
        /// 门诊大病登记 疾病诊断分类
        /// </summary>
        public string zd { get; set; }

        /// <summary>
        /// 大病登记委托 人姓名
        /// </summary>
        public string wtrxm { get; set; }

        /// <summary>
        /// 大病登记委托
        /// </summary>
        public string wtrsfzh { get; set; }

        /// <summary>
        /// 大病登记原因
        /// </summary>
        public string yy { get; set; }

        /// <summary>
        /// 大病登记描述
        /// </summary>
        public string des { get; set; }

        /// <summary>
        /// 大病登记子类
        /// </summary>
        public string dbzl { get; set; }

        /// <summary>
        /// 大病登记医师
        /// </summary>
        public string ysxm { get; set; }

        /// <summary>
        /// 大病登记医师工号
        /// </summary>
        public string ysgh { get; set; }
    }
}
