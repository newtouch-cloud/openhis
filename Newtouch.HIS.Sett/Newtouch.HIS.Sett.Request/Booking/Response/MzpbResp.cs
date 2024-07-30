using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class MzpbResponse
    {
        public DateTime OutDate { get; set; }
        public string OutDept { get; set; }
        public string OutDeptName { get; set; }
        /// <summary>
        /// mjzbz 门诊类型
        /// </summary>
        public string RegType { get; set; }
        /// <summary>
        /// 排班描述
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 号源总数
        /// </summary>
        public int? TotalNum { get; set; }
        /// <summary>
        /// 剩余号源
        /// </summary>
        public int? BookNum { get; set; }
    }
}
