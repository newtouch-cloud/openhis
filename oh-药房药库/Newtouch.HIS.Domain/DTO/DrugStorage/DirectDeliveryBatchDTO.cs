using System;

namespace Newtouch.HIS.Domain.DTO.DrugStorage
{
    /// <summary>
    /// 批量直接出库执行参数
    /// </summary>
    public class DirectDeliveryBatchDTO
    {
        /// <summary>
        /// 组织结构
        /// </summary>
        public string Organizeid { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
        public string djh { get; set; }

        /// <summary>
        /// 出入库单据ID
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// 入库部门代码
        /// </summary>
        public string rkbm { get; set; }

        /// <summary>
        /// 当前药房部门/出库药房部门
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// 出入库方式代码
        /// </summary>
        public string crkfs { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string shzt { get; set; }

        private string _ypCodes = "";
        /// <summary>
        /// 药品代码集合 用','分隔，字符长度不得超过1000
        /// </summary>
        public string ypCodes
        {
            get { return _ypCodes; }
            set
            {
                if (value.Length > 1000)
                {
                    throw new Exception("请控制药品种数，拼接后的药品代码长度不得超过1000");
                }
                _ypCodes = value;
            }
        }
    }
}