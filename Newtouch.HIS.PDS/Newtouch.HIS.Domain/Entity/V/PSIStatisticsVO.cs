namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// 进销存统计容器
    /// </summary>
    public class PsiStatisticsVo
    {
        /// <summary>
        /// 药品单位
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 药品规格
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 进价
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// 期初数量
        /// </summary>
        public int qcsl { get; set; }

        /// <summary>
        /// 入库数量
        /// </summary>
        public int rksl { get; set; }

        /// <summary>
        /// 出库数量
        /// </summary>
        public int cksl { get; set; }

        /// <summary>
        /// 损益数量
        /// </summary>
        public int sysl { get; set; }

        /// <summary>
        /// 期初数量
        /// </summary>
        public int qmsl { get; set; }

        /// <summary>
        /// 期初批发总额
        /// </summary>
        public decimal qcpfze { get; set; }

        /// <summary>
        /// 入库批发总额
        /// </summary>
        public decimal rkpfze { get; set; }

        /// <summary>
        /// 出库批发总额
        /// </summary>
        public decimal ckpfze { get; set; }

        /// <summary>
        /// 损益批发总额
        /// </summary>
        public decimal sypfze { get; set; }

        /// <summary>
        /// 期末批发总额
        /// </summary>
        public decimal qmpfze { get; set; }

        /// <summary>
        /// 调价损益总额
        /// </summary>
        public decimal tjsyze { get; set; }
        /// <summary>
        /// 盘盈数量
        /// </summary>
        public int pysl { get; set; }
        /// <summary>
        /// 盘亏数量
        /// </summary>
        public int pksl { get; set; }
        /// <summary>
        /// 盘盈总额
        /// </summary>
        public decimal pyze { get; set; }
        /// <summary>
        /// 盘亏总额
        /// </summary>
        public decimal pkze { get; set; }
    }
}
