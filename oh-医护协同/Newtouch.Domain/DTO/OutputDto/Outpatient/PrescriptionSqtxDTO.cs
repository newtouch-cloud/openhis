using Newtouch.Domain.ValueObjects;
using newtouchyibao.Models;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    public class PrescriptionSqtxDTO
    {

        public int CFLX { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string JZLX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MZH { get; set; }

        /// <summary>
        /// 枚举 1 西药处方 2 中药处方 3 康复处方 4 检验处方 5 检查处方 6 输液处方
        /// </summary>
        public string CFH { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string YYKSBM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string YYKSMC { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string YYYSGH { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string YSXM { get; set; }

        /// <summary>
        /// 处方明细
        /// </summary>
        public List<SqtxCFYP> ypcfmxList { get; set; }

        public List<SqtxCFXM> xmcfmxList { get; set; }
    }
}
