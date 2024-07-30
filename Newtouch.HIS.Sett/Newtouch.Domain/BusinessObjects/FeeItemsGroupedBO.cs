using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// 处方信息
    /// （没有给cfh时当做新增处方，处方号自动生成）
    /// </summary>
    public class FeeItemsGroupedBO
    {
        /// <summary>
        /// 处方类型  对应枚举 EnumPrescriptionType
        /// </summary>
        public int? cflx { get; set; }
        /// <summary>
        /// 处方类型细分 EnumCflx
        /// </summary>
        public int? cflxxf { get; set; }

        /// <summary>
        /// 处方类型名称
        /// </summary>
        public string cflxmc { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 需要新处方时 收费日期（前提：这个分组仅作为一个处方）
        /// </summary>
        public DateTime? cfsfrq { get; set; }

        /// <summary>
        /// 领药药房
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// 中药贴数
        /// </summary>
        public int? ts { get; set; }

        /// <summary>
        /// 中药代煎标志 1 需要代煎
        /// </summary>
        public int? djbz { get; set; }

        /// <summary>
        /// 有新项目时，是否为其生成关联处方
        /// </summary>
        public bool xmWithCf { get; set; }

        /// <summary>
        /// 单一挂号可能有不同的医生给开处方
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 单一挂号可能有不同的医生给开处方
        /// </summary>
        public string ks { get; set; }

        private IList<OutpatientPrescriptionDetailEntity> _ypList;
        /// <summary>
        /// 新增 药品明细
        /// sl+jl（数量+剂量）
        /// </summary>
        public IList<OutpatientPrescriptionDetailEntity> ypList
        {
            get
            {
                if (_ypList == null)
                {
                    return new List<OutpatientPrescriptionDetailEntity>();
                }
                return _ypList;
            }
            set
            {
                _ypList = value;
            }
        }

        private IList<OutpatientItemEntity> _xmList;
        /// <summary>
        /// 新增 治疗项目明细
        /// sl 或 dczll+zxcs（数量 或 单次治疗量+执行次数）
        /// </summary>
        public IList<OutpatientItemEntity> xmList
        {
            get
            {
                if (_xmList == null)
                {
                    return new List<OutpatientItemEntity>();
                }
                return _xmList;
            }
            set
            {
                _xmList = value;
            }
        }

        private IList<OutpatientPrescriptionDetailEntity> _ztInvalidYpList;
        /// <summary>
        /// 作废 药品明细
        /// 处方内的作废可以以编码来判断唯一（前提：处方内的明细编码不能重复）
        /// </summary>
        public IList<OutpatientPrescriptionDetailEntity> ztInvalidYpList
        {
            get
            {
                if (_ztInvalidYpList == null)
                {
                    return new List<OutpatientPrescriptionDetailEntity>();
                }
                return _ztInvalidYpList;
            }
            set
            {
                _ztInvalidYpList = value;
            }
        }

        private IList<OutpatientItemEntity> _ztInvalidXmList;
        /// <summary>
        /// 作废 治疗项目明细
        /// 处方内的作废可以以编码来判断唯一（前提：处方内的明细编码不能重复）
        /// </summary>
        public IList<OutpatientItemEntity> ztInvalidXmList
        {
            get
            {
                if (_ztInvalidXmList == null)
                {
                    return new List<OutpatientItemEntity>();
                }
                return _ztInvalidXmList;
            }
            set
            {
                _ztInvalidXmList = value;
            }
        }
    }
}
