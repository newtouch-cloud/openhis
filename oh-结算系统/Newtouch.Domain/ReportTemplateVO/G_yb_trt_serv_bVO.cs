using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// 医疗服务项目表
    /// </summary>
    public class G_yb_trt_serv_bVO
    {
        //	医疗目录编码				
        public string MED_LIST_CODG { get; set; }
        //	计价单位				
        public string PRCUNT { get; set; }
        //	计价单位名称				
        public string PRCUNT_NAME { get; set; }
        
        //	诊疗项目说明				
        public string TRT_ITEM_DSCR { get; set; }
        //	诊疗除外内容				
        public string TRT_EXCT_CONT { get; set; }
        //	诊疗项目内涵				
        public string TRT_ITEM_CONT { get; set; }
        //	有效标志				
        public string VALI_FLAG { get; set; }
        //	备注				
        public string MEMO { get; set; }
        //	服务项目类别				
        public string SERVITEM_TYPE { get; set; }
        //	医疗服务项目名称				
        public string SERVITEM_NAME { get; set; }
        //	项目说明				
        public string ITEM_DSCR { get; set; }
        //	开始日期				
        public DateTime? BEGNDATE { get; set; }
        //	结束日期				
        public DateTime? ENDDATE { get; set; }
        //	数据唯一记录号				
        public string RID { get; set; }
        
        //	版本号				
        public string VER { get; set; }
        //	版本名称				
        public string VER_NAME { get; set; }
       

    }
}
