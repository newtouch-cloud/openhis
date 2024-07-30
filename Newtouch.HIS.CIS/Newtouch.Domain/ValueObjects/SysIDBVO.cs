using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// 中间库同步到本地
    /// </summary>
    public class SysIDBVO
    {

        public string OrganizeId { get; set; }

        public string jzId { get; set; }


        /// <summary>
        /// 冗余挂号信息  枚举 1 普通门诊 2 急诊 3专家
        /// </summary>
        public int mjzbz { get; set; }

        /// <summary>
        /// 对应billing接口中的病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 关联billing系统的挂号表
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// 冗余患者信息字段
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 冗余患者信息字段
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// 冗余患者信息字段
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// 冗余患者信息字段
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// 冗余患者信息字段 枚举
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// 冗余患者信息字段
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// 冗余挂号表字段 科室名称
        /// </summary>
        public string ghksmc { get; set; }

        /// <summary>
        /// 冗余挂号表字段 医生名称
        /// </summary>
        public string ghys { get; set; }

        /// <summary>
        /// 冗余挂号表字段
        /// </summary>
        public DateTime ghsj { get; set; }

        public DateTime ghczsj { get; set; }

        public int jzzt { get; set; }
        public string brxzCode { get; set; }


    }
}
