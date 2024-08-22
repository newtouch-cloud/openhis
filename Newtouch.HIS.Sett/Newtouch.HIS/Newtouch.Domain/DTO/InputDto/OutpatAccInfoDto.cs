using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.DTO.InputDto
{
    public class OutpatAccInfoDto
    {
        public int patid { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string blh { get; set; }
        public DateTime? csny { get; set; }
        public string zjlx { get; set; }
        public string zjh { get; set; }
        public string jsr { get; set; }
        public int? nl { get; set; }
        public string phone { get; set; }
        public string brxz { get; set; }
        public string brxzmc { get; set; }
        public int? sycs { get; set; }
        public int? dybh { get; set; }
        public string kh { get; set; }
        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string CardTypeName { get; set; }
        public string mzh { get; set; }
        public int? ghnm { get; set; }
        public DateTime? ghsj { get; set; }
        public string brly { get; set; }
        public string ghrq { get; set; }

        public string lxr { get; set; }
        public string lxrgx { get; set; }
        public string lxrdh { get; set; }
        public string jjllrgx { get; set; }

        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 初复诊
        /// </summary>
        public byte fzbz { get; set; }

        /// <summary>
        /// 大病
        /// </summary>
        public string db { get; set; }

        /// <summary>
        /// 大病诊断
        /// </summary>
        public string dbzd { get; set; }

        /// <summary>
        /// 婚否 枚举 EnumHF
        /// </summary>
        public byte? hf { get; set; }

        /// <summary>
        /// 国籍
        /// </summary>
        public string gjCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gjmc { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string mzCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzmc { get; set; }

        /// <summary>
        /// 挂号状态
        /// </summary>
        public string ghzt { get; set; }

        /// <summary>
        /// 医保结算号
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// 就诊原因
        /// </summary>
        public string jzyy { get; set; }

        /// <summary>
        /// 社保编号
        /// </summary>
        public string sbbh { get; set; }

        /// <summary>
        /// 就诊标志
        /// </summary>
        public string jiuzhenbiaozhi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>一个
        /// 
        /// </summary>
        public string ksmc { get; set; }
		/// <summary>
		/// 参保类别
		/// </summary>
		public string cblb { get; set; }
        public string zyh { get; set; }
        public string mjzbz { get; set; }
        public string CardId { get; set; }
        public string dz { get; set; }
        public string zybz { get; set; }

    }

    public class OptimAccInfoDto
    {
        public string ghrq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string patientName { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfdlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfdlmc { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 单位时长
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// 收费项目当前的单价
        /// </summary>
        public decimal? dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 由 ys ysmc ks ksmc 构造而来
        /// VO引用DTO有问题
        /// </summary>
        public IList<InpatientAccountingPlanItemDoctorDto> ysList
        {
            get
            {
                var list = new List<InpatientAccountingPlanItemDoctorDto>();
                var ysArr = (ys ?? "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                var ysmcArr = (ysmc ?? "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                var ksArr = (ks ?? "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                var ksmcArr = (ksmc ?? "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (ysArr.Length > 0 && ysArr.Length == ysmcArr.Length && ksArr.Length == ksmcArr.Length && ysArr.Length == ksArr.Length)
                {
                    for (var i = 0; i < ysArr.Length; i++)
                    {
                        list.Add(new InpatientAccountingPlanItemDoctorDto()
                        {
                            gh = ysArr[i],
                            Name = ysmcArr[i],
                            ks = ksArr[i],
                            ksmc = ksmcArr[i],
                        });
                    }
                }
                return list;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }


        /// <summary>
        /// 医嘱类型 1药品 2项目
        /// </summary>
        public string yzlx { get; set; }

        /// <summary>
        /// 康复类别
        /// </summary>
        public string kflb { get; set; }

        public DateTime? jzsj { get; set; }
        //public IList<InpatientAccountingPlanItemDoctorDto> ysList { get; set; }
        public int? clzt { get; set; }
        public string newid { get; set; }
        public int? jsmxnm { get; set; }
        public string ttbz { get; set; }
        public string jfbId { get; set; }

        /// <summary>
        /// 单次治疗量
        /// </summary>
        public int? zll { get; set; }
        public int? zxzt { get; set; }
        public string jzjhmxId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string yzxz { get; set; }
        /// <summary>
        /// 单位计量数
        /// </summary>
        public int? dwjls { get; set; }
        /// <summary>
        /// 计价策略
        /// </summary>
        public int? jjcl { get; set; }

        public int? xmnm { get; set; }
        public string zfxz { get; set; }
        public decimal? zfbl { get; set; }
        public string xnhgrbm { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class OutpatOptimAccInfoDto
    {
        /// <summary>
        /// 就诊记录
        /// </summary>
        public OutpatAccInfoDto OutpatAccInfoDto { get; set; }
        
        /// <summary>
        /// 就诊记录集合
        /// </summary>
        public List<OutpatAccInfoDto> OutpatAccListDto { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public IList<OptimAccInfoDto> OptimAccInfoDto { get; set; }
    }
    public class OutpatOptimInfoDto
    {
        public OutpatAccInfoDto OutpatAccInfoDto { get; set; }
        public IList<SyncTreatmentServiceRecordVO> SyncTreatmentServiceRecordVO { get; set; }
    }
}
