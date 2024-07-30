using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Gzyb_CodeContrast")]
    public class GzybCodeContrastEntity : IEntity<GzybCodeContrastEntity>
    {
        /// <summary>
        /// id
        /// </summary>
        [Key]
        public int id { get; set; }

        /// <summary>
        /// 代码类别
        /// </summary>
        public string aaa100 { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string aaa101 { get; set; }

        /// <summary>
        /// 代码值
        /// </summary>
        public string aaa102 { get; set; }

        /// <summary>
        /// 代码名称
        /// </summary>
        public string aaa103 { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 建档人员
        /// </summary>
        public string jdry { get; set; }

        /// <summary>
        /// 建档日期
        /// </summary>
        public DateTime jdrq { get; set; }
        
    }
}
