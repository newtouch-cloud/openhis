using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.DrugStorage
{
    /// <summary>
    /// 查询药品库存返回报文
    /// </summary>
    public class StockQueryResponseDTO
    {
        /// <summary>
        /// 药品库存信息
        /// </summary>
        public List<DrugStockInfo> drugStockInfos { get; set; }
    }

    /// <summary>
    /// 库存药品信息
    /// </summary>
    public class DrugStockInfo
    {

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 领药药房
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// 领药药房名称
        /// </summary>
        public string lyyfmc { get; set; }

        /// <summary>
        /// 做小单位库存数量
        /// </summary>
        public int kscl { get; set; }

        /// <summary>
        /// 做小单位冻结数量
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// 做小单位可用数量
        /// </summary>
        public int kysl { get; set; }
    }
}
