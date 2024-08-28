using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_3507: InputBase
    {
        public data3507 data { get; set; }
    }
    public class data3507
    {
        /// <summary>
        /// 定点医药机构批次流水号
        /// </summary>
        public string fixmedins_bchno { get; set; }
        /// <summary>
        /// 进销存数据类型 1-盘存信息；2-库存变更信息；3-采购信息；4-销售信息
        /// </summary>
        public string inv_data_type { get; set; }
    }
}
