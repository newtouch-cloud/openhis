using System;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// 患者就诊信息查询返回报文
    /// </summary>
    public class PatientTreatmentInfoQueryResponseDTO
    {
        /// <summary>
        /// 就诊ID
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 关联billing系统的挂号表
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// 冗余挂号信息  枚举 1 普通门诊 2 急诊 3专家
        /// </summary>
        public int mjzbz { get; set; }

        /// <summary>
        /// 对应billing接口中的病历号
        /// </summary>
        public string blh { get; set; }

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

        /// <summary>
        /// 挂号操作时间
        /// </summary>
        public DateTime? ghczsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? tizhong { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? tiwen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? maibo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? xueya { get; set; }

        /// <summary>
        /// 血糖测量方式 餐后 空腹 随机
        /// </summary>
        public string xuetangclfs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? xuetang { get; set; }

        /// <summary>
        /// 针对一个疾病 医生进行判断
        /// </summary>
        public bool fzbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime zlkssj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? zljssj { get; set; }

        /// <summary>
        /// 关联base库 科室表
        /// </summary>
        public string jzks { get; set; }

        /// <summary>
        /// 关联base库 人员表（最后看诊医生）
        /// </summary>
        public string jzys { get; set; }

        /// <summary>
        /// （最后）看诊医生名称
        /// </summary>
        public string jzysmc { get; set; }

        /// <summary>
        /// 枚举 1 未就诊 2 就诊中 3 已就诊 （用来恢复结束就诊的记录）
        /// </summary>
        public int jzzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        public string brxzCode { get; set; }

        /// <summary>
        /// 医保结算号（医保返回）
        /// </summary>
        public string ybjsh { get; set; }
        /// <summary>
        /// 初复诊标志 1复诊
        /// </summary>
        public string cfzbz { get; set; }
        /// <summary>
        /// 社保编号
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// 参保地编码
        /// </summary>
        public string cbdbm { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public decimal? shengao { get; set; }
        /// <summary>
        /// 收缩压
        /// </summary>
        public decimal? shousuoya { get; set; }
        /// <summary>
        /// 舒张压
        /// </summary>
        public decimal? shuzhangya { get; set; }
        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactNum { get; set; }
        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string kh { get; set; }

        public string nlshow { get; set; }
        /// <summary>
        /// 呼吸
        /// </summary>
        public decimal? huxi { get; set; }

        /// <summary>
        /// 西医诊断代码
        /// </summary>
        public string xyzdCode { get; set; }

        /// <summary>
        /// 西医诊断名称
        /// </summary>
        public string xyzdmc { get; set; }

        /// <summary>
        /// 中医诊断代码
        /// </summary>
        public string zyzdCode { get; set; }

        /// <summary>
        /// 中医诊断名称
        /// </summary>
        public string zyzdmc { get; set; }
    }
}