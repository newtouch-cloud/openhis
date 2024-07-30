using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Lib.Base.Model.PACS
{
    public class ApiPostBase
    {
        public string reqType { get; set; }
        public string reqUrl { get; set; }
        public List<reqBody> reqBody { get; set; }
        public string methodName { get; set; }
        public string namespaceURI { get; set; }
        public string actionType { get; set; }
        public string version { get; set; }
        public string systemType { get; set; }
        public string reqHeaders { get; set; }
    }

    public class reqBody {

        #region 科室

        public string? hosCode { get; set; }
        public string? name { get; set; }
        public string? mobile { get; set; }
        public string? source { get; set; }
        public string? sourceCode { get; set; }
        #endregion

        #region 医生

        public string? deptCode { get; set; }
        public string? deptName { get; set; }
        #endregion


        #region 获取pacs报告结果
        public string pid { get; set; }
        public string applyNo { get; set; }
        public string checkTime { get; set; }
        public string ssid { get; set; }

        #endregion
    }
}
