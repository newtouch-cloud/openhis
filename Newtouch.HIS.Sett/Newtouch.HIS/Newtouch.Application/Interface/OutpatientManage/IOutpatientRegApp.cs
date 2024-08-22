using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutpatientRegApp
    {
        /// <summary>
        /// 挂号排班List
        /// </summary>
        /// <param name="keyword">科室/医生</param>
        /// <param name="mzlx">门诊类型 普通门诊/急症/专家门诊</param>
        /// <returns></returns>
        List<RegScheduleVO> GetRegScheduleList(string keyword, string mzlx);

        string GetInvoice();

        void CheckInvoice(string inputFph);

        /// <summary>
        /// 门诊挂号保存
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="kh"></param>
        /// <param name="ghly"></param>
        /// <param name="mjzbz"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <param name="ksmc"></param>
        /// <param name="ysmc"></param>
        /// <param name="ghxm"></param>
        /// <param name="zlxm"></param>
        /// <param name="fph"></param>
        /// <param name="sfrq"></param>
        /// <param name="isCkf"></param>
        /// <param name="isGbf"></param>
        /// <param name="ghpbId"></param>
        /// <param name="feeRelated"></param>
        /// <param name="brxz">病人性质由页面传过来，因为医保患者可以挂自费</param>
        /// <param name="notSett">true暂时不结（门诊收费时一起结），否则结</param>
        /// <param name="ybjsh">医保端结算号</param>
        /// <param name="qzjzxh">前置就诊序号</param>
        /// <param name="qzmzh">前置门诊号</param>
        /// <param name="mzyyghId">门诊预约挂号ID 对应Newtouch_CIS.dbo.mz_yygh.Id</param>
        /// <returns></returns>
        void Save(int patid, string kh, string ghly, string mjzbz,
            string ks, string ys, string ksmc, string ysmc, string ghxm, string zlxm
            , string fph, DateTime? sfrq, bool isCkf, bool isGbf, int ghpbId
            , OutpatientSettFeeRelatedDTO feeRelated, string brxz
            , string ybjsh, string mzyyghId, ref short? qzjzxh, ref string qzmzh
            , string jzyy,string jzid, string jzlx, string bzbm, string bzmc, out object newJszbInfo);

        long Book(OutpatientRegistEntity gh, decimal ssk, string fph, bool isCkf, bool isGbf, string updateBrxz);

        void CheckBrxz(string kh, string updateBrxz);

        /// <summary>
        /// 预约挂号
        /// </summary>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <param name="blh"></param>
        /// <param name="ksCode"></param>
        /// <param name="mzlx"></param>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        string BespeakRegister(int? zjlx, string zjh, string blh, string ksCode, int mzlx, string ysgh);
    }
}
