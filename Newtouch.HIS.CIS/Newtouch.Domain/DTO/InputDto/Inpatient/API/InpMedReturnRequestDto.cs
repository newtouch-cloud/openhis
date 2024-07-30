using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InputDto
{
    public class InpMedReturnRequestDto
    {
        /// <summary>
        /// 医嘱号
        /// </summary>
        public string yzId { get; set; }
        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }
       
        /// <summary>
        /// 分组序号
        /// </summary>
        public int? zh { get; set; }
      

        public string zyh { get; set; }

        public int tysl { get; set; }

        public int? zhyz { get; set; }
        /// <summary>
        /// 退药申请人
        /// </summary>
        public string tysqr { get; set; }
        /// <summary>
        /// 执行日期
        /// </summary>
        public DateTime zxrq { get; set; }
        public string ksCode { get; set; }
        public string bqCode { get; set; }
        public string fyyf { get; set; }
        public long lyxh { get; set; }

    }

    public class InMedReturnRequestslDto {
        public string Id { get; set; }
        public int tsl { get; set; }
    }
}
