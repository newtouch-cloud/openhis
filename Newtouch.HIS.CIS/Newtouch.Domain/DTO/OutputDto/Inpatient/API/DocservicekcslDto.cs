using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto.Inpatient.API
{

    public class DocservicekcslRequestDto {
        public string ypCode { get; set; }
        public string lyyf { get; set; }
    }
    public class DocservicekcslResultDto
    {
        public string ResultCode { get; set; }
        public string ResultMsg { get; set; }
        public bool IsSucceed { get; set; }
        public StockQueryResponseDTO Data { get; set; }
    }

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
