using Newtouch.Domain.Entity;
using System;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// 住院患者信息
    /// </summary>
    public class InPatientPatientSearchVO
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 病区编码
        /// </summary>
        public string WardCode { get; set; }

        /// <summary>
        /// 病区名称
        /// </summary>
        public string WardName { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? birth { get; set; }

        /// <summary>
        /// 入院日期
        /// </summary>
        public DateTime? ryrq { get; set; }

        /// <summary>
        /// 入区日期
        /// </summary>
        public DateTime? rqrq { get; set; }

    }

    public class InPatientNursingInputVO
    {
        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 病区编码
        /// </summary>
        public string WardCode { get; set; }

        /// <summary>
        /// 病区名称
        /// </summary>
        public string WardName { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? birth { get; set; }

        /// <summary>
        /// 入院日期
        /// </summary>
        public DateTime? ryrq { get; set; }

        /// <summary>
        /// 入区日期
        /// </summary>
        public DateTime? rqrq { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// zyh
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// rq
        /// </summary>
        /// <returns></returns>
        public DateTime? rq { get; set; }
        /// <summary>
        /// 时间点 4 8 12 16 20 24
        /// </summary>
        /// <returns></returns>
        public int? sj { get; set; }
        /// <summary>
        /// 枚举 请假 拒测 等
        /// </summary>
        /// <returns></returns>
        public int? brzt { get; set; }
        /// <summary>
        /// tw
        /// </summary>
        /// <returns></returns>
        public decimal? tw { get; set; }
        /// <summary>
        /// 体温测量说明 口温 腋温 肛温
        /// </summary>
        /// <returns></returns>
        public int? twclfs { get; set; }
        /// <summary>
        /// xl
        /// </summary>
        /// <returns></returns>
        public int? xl { get; set; }
        /// <summary>
        /// mb
        /// </summary>
        /// <returns></returns>
        public int? mb { get; set; }
        /// <summary>
        /// qbq
        /// </summary>
        /// <returns></returns>
        public bool? qbq { get; set; }
        /// <summary>
        /// hx
        /// </summary>
        /// <returns></returns>
        public int? hx { get; set; }
        /// <summary>
        /// hxj
        /// </summary>
        /// <returns></returns>
        public bool? hxj { get; set; }
        /// <summary>
        /// xysz
        /// </summary>
        /// <returns></returns>
        public int? xysz { get; set; }
        /// <summary>
        /// xyxz
        /// </summary>
        /// <returns></returns>
        public int? xyxz { get; set; }
        /// <summary>
        /// tz
        /// </summary>
        /// <returns></returns>
        public string tz { get; set; }
        /// <summary>
        /// 字典不区分Org
        /// </summary>
        /// <returns></returns>
        public string tzclbz { get; set; }
        /// <summary>
        /// syl
        /// </summary>
        /// <returns></returns>
        public int? syl { get; set; }
        /// <summary>
        /// ysl
        /// </summary>
        /// <returns></returns>
        public int? ysl { get; set; }
        /// <summary>
        /// sxl
        /// </summary>
        /// <returns></returns>
        public int? sxl { get; set; }
        /// <summary>
        /// dbcs
        /// </summary>
        /// <returns></returns>
        public int? dbcs { get; set; }
        /// <summary>
        /// 字典不区分Org
        /// </summary>
        /// <returns></returns>
        public string dbcsbz { get; set; }
        /// <summary>
        /// otl
        /// </summary>
        /// <returns></returns>
        public int? otl { get; set; }
        /// <summary>
        /// xbl
        /// </summary>
        /// <returns></returns>
        public int? xbl { get; set; }
        /// <summary>
        /// 超液量
        /// </summary>
        /// <returns></returns>
        public int? cys { get; set; }
        /// <summary>
        /// skyll
        /// </summary>
        /// <returns></returns>
        public int? skyll { get; set; }
        /// <summary>
        /// xyl
        /// </summary>
        /// <returns></returns>
        public int? xyl { get; set; }
        /// <summary>
        /// wyl
        /// </summary>
        /// <returns></returns>
        public int? wyl { get; set; }
        /// <summary>
        /// fxxq
        /// </summary>
        /// <returns></returns>
        public int? fxxq { get; set; }
        /// <summary>
        /// qtsc
        /// </summary>
        /// <returns></returns>
        public int? qtsc { get; set; }
        /// <summary>
        /// xyll
        /// </summary>
        /// <returns></returns>
        public int? xyll { get; set; }
        /// <summary>
        /// 字典不区分Org
        /// </summary>
        /// <returns></returns>
        public string xyfs { get; set; }
        /// <summary>
        /// xybhd
        /// </summary>
        /// <returns></returns>
        public int? xybhd { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// px
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        /// <summary>
        /// 自定义时间点
        /// </summary>
        public string zdysjd { get; set; }

        public string lrflag { get; set; }

        /// <summary>
        /// 病人状态-小时
        /// </summary>
        public string brzt_hh { get; set; }
        /// <summary>
        /// 病人状态-分
        /// </summary>
        public string brzt_mi { get; set; }
        /// <summary>
        /// 引流量
        /// </summary>
        public string yll { get; set; }
        /// <summary>
        /// 总入量
        /// </summary>
        public string zrl { get; set; }
        /// <summary>
        /// 总出量
        /// </summary>
        public string zcl { get; set; }
        /// <summary>
        /// 过敏药物
        /// </summary>
        public string gmyw { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public string sg { get; set; }
        /// <summary>
        /// 疼痛评分
        /// </summary>
        public string ttpf { get; set; }
    }
}
