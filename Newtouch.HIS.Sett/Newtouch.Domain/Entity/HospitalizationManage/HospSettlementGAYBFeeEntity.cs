using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2019-04-08 14:00
    /// 描 述：住院结算-医保费用-贵安
    /// </summary>
    [Table("zy_js_gaybjyfy")]
    public class HospSettlementGAYBFeeEntity : IEntity<HospSettlementGAYBFeeEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 关联zy_js jsnm
        /// </summary>
        public int jsnm { get; set; }
        /// <summary>
        /// 费用总额（包括不和医保交易的全自费部分）
        /// </summary>
        public decimal his_hisfyze { get; set; }
        /// <summary>
        /// prm_akc190
        /// </summary>
        /// <returns></returns>
        public string prm_akc190 { get; set; }
        /// <summary>
        /// prm_yab003
        /// </summary>
        /// <returns></returns>
        public string prm_yab003 { get; set; }
        /// <summary>
        /// prm_aka130
        /// </summary>
        /// <returns></returns>
        public string prm_aka130 { get; set; }
        /// <summary>
        /// prm_ykb065
        /// </summary>
        /// <returns></returns>
        public string prm_ykb065 { get; set; }
        /// <summary>
        /// prm_hisfyze 需要和医保交易的部分的费用总额
        /// </summary>
        /// <returns></returns>
        public decimal prm_hisfyze { get; set; }
        /// <summary>
        /// prm_yka110
        /// </summary>
        /// <returns></returns>
        public string prm_yka110 { get; set; }
        /// <summary>
        /// prm_aae013
        /// </summary>
        /// <returns></returns>
        public string prm_aae013 { get; set; }
        /// <summary>
        /// prm_aae011
        /// </summary>
        /// <returns></returns>
        public decimal? prm_aae011 { get; set; }
        /// <summary>
        /// prm_ykc141
        /// </summary>
        /// <returns></returns>
        public string prm_ykc141 { get; set; }
        /// <summary>
        /// prm_aac001
        /// </summary>
        /// <returns></returns>
        public string prm_aac001 { get; set; }
        /// <summary>
        /// prm_yka103
        /// </summary>
        /// <returns></returns>
        public string prm_yka103 { get; set; }
        /// <summary>
        /// prm_yka065
        /// </summary>
        /// <returns></returns>
        public decimal? prm_yka065 { get; set; }
        /// <summary>
        /// prm_aae036
        /// </summary>
        /// <returns></returns>
        public DateTime? prm_aae036 { get; set; }
        /// <summary>
        /// prm_yka055
        /// </summary>
        /// <returns></returns>
        public decimal prm_yka055 { get; set; }
        /// <summary>
        /// prm_yka056
        /// </summary>
        /// <returns></returns>
        public decimal prm_yka056 { get; set; }
        /// <summary>
        /// prm_yka057
        /// </summary>
        /// <returns></returns>
        public decimal prm_yka057 { get; set; }
        /// <summary>
        /// prm_yka111
        /// </summary>
        /// <returns></returns>
        public decimal prm_yka111 { get; set; }
        /// <summary>
        /// prm_yka058
        /// </summary>
        /// <returns></returns>
        public decimal? prm_yka058 { get; set; }
        /// <summary>
        /// prm_yka248
        /// </summary>
        /// <returns></returns>
        public decimal? prm_yka248 { get; set; }
        /// <summary>
        /// prm_yka062
        /// </summary>
        /// <returns></returns>
        public decimal? prm_yka062 { get; set; }
        /// <summary>
        /// prm_yke030
        /// </summary>
        /// <returns></returns>
        public decimal prm_yke030 { get; set; }
        /// <summary>
        /// prm_ake032
        /// </summary>
        /// <returns></returns>
        public decimal prm_ake032 { get; set; }
        /// <summary>
        /// prm_ake181
        /// </summary>
        /// <returns></returns>
        public decimal prm_ake181 { get; set; }
        /// <summary>
        /// prm_ake173
        /// </summary>
        /// <returns></returns>
        public decimal prm_ake173 { get; set; }
        /// <summary>
        /// prm_zhyjyjj
        /// </summary>
        /// <returns></returns>
        public decimal? prm_zhyjyjj { get; set; }
        /// <summary>
        /// prm_akc087
        /// </summary>
        /// <returns></returns>
        public decimal? prm_akc087 { get; set; }
        /// <summary>
        /// prm_ykb037
        /// </summary>
        /// <returns></returns>
        public string prm_ykb037 { get; set; }
        /// <summary>
        /// prm_yka316
        /// </summary>
        /// <returns></returns>
        public string prm_yka316 { get; set; }
        /// <summary>
        /// prm_akc021
        /// </summary>
        /// <returns></returns>
        public string prm_akc021 { get; set; }
        /// <summary>
        /// prm_ykc120
        /// </summary>
        /// <returns></returns>
        public string prm_ykc120 { get; set; }
        /// <summary>
        /// prm_yab139
        /// </summary>
        /// <returns></returns>
        public string prm_yab139 { get; set; }
        /// <summary>
        /// prm_aac003
        /// </summary>
        /// <returns></returns>
        public string prm_aac003 { get; set; }
        /// <summary>
        /// prm_aac004
        /// </summary>
        /// <returns></returns>
        public string prm_aac004 { get; set; }
        /// <summary>
        /// prm_aac002
        /// </summary>
        /// <returns></returns>
        public string prm_aac002 { get; set; }
        /// <summary>
        /// prm_aac006
        /// </summary>
        /// <returns></returns>
        public DateTime? prm_aac006 { get; set; }
        /// <summary>
        /// prm_akc023
        /// </summary>
        /// <returns></returns>
        public decimal? prm_akc023 { get; set; }
        /// <summary>
        /// prm_aab001
        /// </summary>
        /// <returns></returns>
        public string prm_aab001 { get; set; }
        /// <summary>
        /// prm_aab004
        /// </summary>
        /// <returns></returns>
        public string prm_aab004 { get; set; }
        /// <summary>
        /// prm_ykc280
        /// </summary>
        /// <returns></returns>
        public string prm_ykc280 { get; set; }
        /// <summary>
        /// prm_ykc281
        /// </summary>
        /// <returns></returns>
        public string prm_ykc281 { get; set; }
        /// <summary>
        /// prm_yka054
        /// </summary>
        /// <returns></returns>
        public string prm_yka054 { get; set; }
        /// <summary>
        /// prm_yae366
        /// </summary>
        /// <returns></returns>
        public string prm_yae366 { get; set; }
        /// <summary>
        /// prm_akc090
        /// </summary>
        /// <returns></returns>
        public decimal? prm_akc090 { get; set; }
        /// <summary>
        /// prm_yka120
        /// </summary>
        /// <returns></returns>
        public decimal? prm_yka120 { get; set; }
        /// <summary>
        /// prm_yka122
        /// </summary>
        /// <returns></returns>
        public decimal? prm_yka122 { get; set; }
        /// <summary>
        /// prm_yka368
        /// </summary>
        /// <returns></returns>
        public decimal? prm_yka368 { get; set; }
        /// <summary>
        /// prm_yka025
        /// </summary>
        /// <returns></returns>
        public decimal? prm_yka025 { get; set; }
        /// <summary>
        /// prm_yka900
        /// </summary>
        /// <returns></returns>
        public decimal? prm_yka900 { get; set; }
        /// <summary>
        /// prm_aae001
        /// </summary>
        /// <returns></returns>
        public decimal? prm_aae001 { get; set; }
        /// <summary>
        /// prm_yka089
        /// </summary>
        /// <returns></returns>
        public string prm_yka089 { get; set; }
        /// <summary>
        /// prm_yka027
        /// </summary>
        /// <returns></returns>
        public string prm_yka027 { get; set; }
        /// <summary>
        /// prm_yka028
        /// </summary>
        /// <returns></returns>
        public decimal? prm_yka028 { get; set; }
        /// <summary>
        /// prm_yka345
        /// </summary>
        /// <returns></returns>
        public decimal? prm_yka345 { get; set; }

        public string prm_ykc299 { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
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
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

    }
}