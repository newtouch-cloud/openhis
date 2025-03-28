using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    public class QHDSmartCheckInput
    {
        public string jsondata { get; set; }
        public string jydm { get; set; }
    }
    public class YBRequestBase<T>
    {
        public YBRequest<T> REQUESTDATA { get; set; }
    }

    public class YBRequest<T>
    {
        /// <summary>
        /// 交易代码
        /// </summary>
        public string MSGNO { get; set; }
        /// <summary>
        /// 定点医疗机构编码
        /// </summary>
        public string AKB020 { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OPERID { get; set; }
        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string OPERNAME { get; set; }
        /// <summary>
        /// 业务周期号 操作员签到时，中心返回给 his 的业务周期号
        /// </summary>
        public string PERIODNO { get; set; }
        /// <summary>
        /// 发送方交易流水号 由 HIS 填入，采用定点医疗机构编码+时间 yyyymmddhh24miss+序 列
        /// </summary>
        public string MSGID { get; set; }
        /// <summary>
        /// 东软提供(暂时为空)
        /// </summary>
        public string GRANTID { get; set; }
    }

    public class YBRequestSingle<T> : YBRequest<T>
    {
        public T INPUT{ get; set; }
    }

    public class YBRequestRows<T> : YBRequest<T>
    {
        public List<T> INPUT { get; set; }
    }

    public class ROWS<T>
    {
        public T INPUTROW { get; set; }
    }
}
