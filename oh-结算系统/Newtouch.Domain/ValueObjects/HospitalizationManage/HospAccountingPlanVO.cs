using Newtouch.HIS.Domain.BusinessObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 住院记账计划
    /// </summary>
    public class HospAccountingPlanVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 医嘱性质
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate { get; set; }

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

        ///// <summary>
        ///// 
        ///// </summary>
        //public decimal? zje { get; set; }

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
        /// 执行状态
        /// </summary>
        public int zxzt { get; set; }

        /// <summary>
        /// 最后执行时间
        /// </summary>
        public DateTime? LastEexcutionTime { get; set; }

        /// <summary>
        /// 医嘱类型 1药品 2项目
        /// </summary>
        public string yzlx { get; set; }

        /// <summary>
        /// 康复类别
        /// </summary>
        public string kflb { get; set; }
        public int? zll { get; set; }
        /// <summary>
        ///单位计量数
        /// </summary>
        public int? dwjls { get; set; }
        /// <summary>
        /// 计价策略
        /// </summary>
        public int? jjcl { get; set; }
        public string zxks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zxksmc { get; set; }
    }
}
