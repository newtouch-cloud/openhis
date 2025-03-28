using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.ValueObjects
{
    public class OperationDicVO
    {
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// ssdm
        /// </summary>
        /// <returns></returns>
        public string ssdm { get; set; }

        /// <summary>
        /// ssmc
        /// </summary>
        /// <returns></returns>
        public string ssmc { get; set; }

        /// <summary>
        /// zjm
        /// </summary>
        /// <returns></returns>
        public string zjm { get; set; }

        /// <summary>
        /// ssjb
        /// </summary>
        /// <returns></returns>
        public string ssjb { get; set; }
    }
}
