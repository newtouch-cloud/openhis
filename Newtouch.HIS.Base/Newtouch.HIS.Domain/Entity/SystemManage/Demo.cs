using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.SystemManage
{
    /// <summary>
    /// 测试demo
    /// </summary>
    [Table("t_demo")]
    public class Demo : IEntity<Demo>
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }
}
