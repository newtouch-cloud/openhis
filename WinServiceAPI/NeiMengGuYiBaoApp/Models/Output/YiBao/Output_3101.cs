using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_3101 : OutputBase
    {
        //输出-违规信息（节点标识：result）
        public List<Outputrs3101> result { get; set; }
        //输出-违规明细信息（节点标识：judge_result_detail_dtos）
        public List<OutputJRDD3101> judge_result_detail_dtos { get; set; }
    }
    //输出-违规信息（节点标识：result）
    public class Outputrs3101
    {
        //	违规标识	字符型	50	计算结果记录唯一ID
        public string jr_id { get; set; }
        //	规则ID	字符型	50	例如：R01
        public string rule_id { get; set; }
        //	规则名称	字符型	200	例如：配伍禁忌
        public string rule_name { get; set; }
        //	违规内容	字符型	500	例如：患者处方中存在配伍禁忌的药品【A药】、【B药】。
        public string vola_cont { get; set; }
        //	参保人ID	字符型	50	（注意：当是多患者导致违规时，这里是其中一个患者ID）
        public string patn_id { get; set; }
        //	就诊ID	字符型	50	（注意：当是多就诊导致违规时，这里是其中一个就诊ID）
        public string mdtrt_id { get; set; }
        //	违规明细	违规明细集合		
        //public string judge_result_detail_dtos { get; set; }
        public List<OutputJRDD3101> judge_result_detail_dtos { get; set; }
        //	违规金额	数值型	16,2	
        public string vola_amt { get; set; }
        //	违规金额计算状态	字符型	3	参考字典表
        public string vola_amt_stas { get; set; }
        //	严重程度	字符型	3	参考字典表
        public string sev_deg { get; set; }
        //	违规依据	字符型	500	
        public string vola_evid { get; set; }
        //	违规行为分类	字符型	3	参考字典表
        public string vola_bhvr_type { get; set; }
        //	任务ID	字符型		
        public string task_id { get; set; }

    }
    //输出-违规明细信息（节点标识：judge_result_detail_dtos）
    public class OutputJRDD3101
    {
        //违规明细标识	字符型	50  违规明细唯一标识
        public string jrd_id { get; set; }
        //参保人标识	字符型	50
        public string patn_id { get; set; }
        //就诊标识	字符型	50
        public string mdtrt_id { get; set; }
        //处方(医嘱)标识	字符型	50
        public string rx_id { get; set; }
        //违规明细类型	字符型	3  1、违规项	2、涉及项
        public string vola_item_type { get; set; }
        //违规金额	数值型	16,2
        public string vola_amt { get; set; }
    }
}
