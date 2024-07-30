using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class TemperatureGraphData
    {
        /// <summary>
        /// Id
        /// </summary>
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
        public DateTime rq { get; set; }
        /// <summary>
        /// 时间点  
        /// </summary>
        /// <returns></returns>
        public int sj { get; set; }
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
        /// 身高
        /// </summary>
        /// <returns></returns>
        public string sg { get; set; }
        /// <summary>
        /// 疼痛评分
        /// </summary>
        /// <returns></returns>
        public string ttpf { get; set; }
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
        /// 护理意识
        /// </summary>
        public string hlys { get; set; }
        /// <summary>
        /// 病人饮食
        /// </summary>
        public string brfood { get; set; }
        /// <summary>
        /// 皮肤情况
        /// </summary>
        public string pfqk { get; set; }
        /// <summary>
        /// 管道护理
        /// </summary>
        public string gdhl { get; set; }

        /// <summary>
        /// 病情观察及措施
        /// </summary>
        public string bqgcjcs { get; set; }
        /// <summary>
        /// 饮食量途径
        /// </summary>
        public string ysltj { get; set; }
        /// <summary>
        /// 呕吐量颜色
        /// </summary>
        public string otlys { get; set; }
        /// <summary>
        /// 尿量颜色
        /// </summary>
        public string nlys { get; set; }
        /// <summary>
        /// 护士签名
        /// </summary>
        public string hsqm { get; set; }
        /// <summary>
        /// 输入液体量
        /// </summary>
        public int? srytl { get; set; }
        /// <summary>
        /// wljw
        /// </summary>
        public decimal? wljw { get; set; }

        #region 预加工
        /// <summary>
        /// 显示 日期
        /// </summary>
        public string showrq { get; set; }
        /// <summary>
        /// 日期排号 
        /// </summary>
        public int num { get; set; }
        public int nowpage { get; set; }

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
        /// 手术后天数
        /// </summary>
        public string sshts { get; set; }
        #endregion
        public string ps { get; set; }
    }
}
