using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.OutpatientManage
{
    public class OutPatientStaffEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string gh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string py { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string departmengCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string kflb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string mbqx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string zc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string zjlx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string zjh { get; set; }
    }
}
