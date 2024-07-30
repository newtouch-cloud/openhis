using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
  public  class OutputReturn
    {
        /// <summary>
		/// infcode 交易状态码  数值型  4  Y 详见下节 0 成功 -1 失败
		/// </summary>
		public string infcode { get; set; }
        /// <summary>
        /// inf_refmsgid 接收方报文 ID 字符型  30  Y接收方返回，接收方医保区划代码(6)+时间(14)+流水号(10)时间格式：yyyyMMddHHmmss
        /// </summary>
        public string inf_refmsgid { get; set; }
        /// <summary>
        /// refmsg_time  接收报文时间 字符型  17  格式：yyyyMMddHHmmssSSS
        /// </summary>
        public string refmsg_time { get; set; }
        /// <summary>
        /// respond_time  响应报文时间 字符型  17  格式：yyyyMMddHHmmssSSS
        /// </summary>
        public string respond_time { get; set; }

        /// <summary>
        /// err_msg  错误信息 字符型 200 交易失败状态下，业务返回的错误信息
        /// </summary>
        public string err_msg { get; set; }

        /// <summary>
        /// output  交易输出 字符型 40000
        /// </summary>
        public object output { get; set; }
    }

    public class ReturnDto
    {
        public int code { get; set; }
        public int? infcode { get; set; }
        public string type { get; set; }
        public string message { get; set; }
        public string err_msg { get; set; }
        public cardecinfo data { get; set; }
    }
    public class cardecinfo
    {
        public string userName { get; set; }
        public string idNo { get; set; }
        public string idType { get; set; }
        public string insuOrg { get; set; }
        public string ecIndexNo { get; set; }
        public string ecToken { get; set; }
        public string gender { get; set; }
        public string birthday { get; set; }
        public string nationality { get; set; }
        public string email { get; set; }
        public string chnlId { get; set; }
        public string ecQrCode { get; set; }
        public string authNo { get; set; }
        public string cardno { get; set; }
    }
}
