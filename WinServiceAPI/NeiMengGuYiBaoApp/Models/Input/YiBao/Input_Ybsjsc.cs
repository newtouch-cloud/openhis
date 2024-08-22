using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_Ybsjsc : InputBase
    {
        public string JYH { get; set; } //	就诊流水号	字符	32	必填	复合主键; 医院HIS系统的唯一编号.
        public string JGDM { get; set; }    //	医保医疗机构代码	字符	11	必填	复合主键;
        public string SBRQ { get; set; }    //	申报日期	字符	8	必填	复合主键;格式：YYYYMMDD
        public List<jylshxx> JYLSHXX { get; set; } //	中心交易流水号(循环体)	字符	16	必填	循环体 医保实时患者填写“中心流水号”，自费及医保非实时患者填写16个0
        public string YBSSJSBZ { get; set; }    //	医保实时结算标志	字符	1	必填	1:医保实时结算  2:非医保实时结算（包含全自费结算）
        public string CYBZ { get; set; }    //	出院结算标志	字符	1	必填	1:出院结算、 2:在院结算
        public string SBLB { get; set; }    //	医保支付方式	字符	1	必填	1:按项目2:单病种3:按病种分值4:疾病诊断相关分组（DRG）5:按床日6:按人头9:其它
        public string ZXYBZ { get; set; }   //	中西医标志	字符	2	必填	西医:01、中医:02
        public string YLFKFS { get; set; }  //	医疗付费方式	字符	3	必填	编码。CV07.10.005
        public string JKKH { get; set; }    //	就诊卡号	字符	100	可选	就诊卡号
        public string ZYCS { get; set; }    //	住院次数	字符	10	必填	
        public string BAH { get; set; } //	病案号	字符	18	必填	
        public string XM { get; set; }  //	姓名	字符	20	必填	加密：个人隐私信息传输
        public string XB { get; set; }  //	性别	字符	1	必填	GB/T 2261.1-2003
        public string CSRQ { get; set; }    //	出生日期	字符	12	必填	格式：YYYYMMDD
        public string NL { get; set; }  //	年龄	数字	10	必填	单位：岁
        public string GJ { get; set; }  //	国籍	字符	3	可选	GB/T 2659-2000
        public string BZYZSNL { get; set; } //	年龄不足一周岁的年龄(月龄)	数字	10	可选	单位：天
        public string XSECSTZ { get; set; } //	新生儿出生体重	数字	12,2	可选	单位：克，新生儿必填
        public string XSERYTZ { get; set; } //	新生儿入院体重	数字	12,2	可选	单位：克，新生儿必填
        public string CSD { get; set; } //	出生地	字符	200	可选	
        public string GG { get; set; }  //	籍贯	字符	200	可选	GB/T2260-2017
        public string MZ { get; set; }  //	民族	字符	2	可选	GB/T3304-1991
        public string ZJLX { get; set; }    //	患者证件类别	字符	2	必填	编码。见证件类型字典
        public string SFZH { get; set; }    //	患者证件号码	字符	100	必填	
        public string ZY { get; set; }  //	职业	字符	2	可选	GB/T 2261.4-2003
        public string HY { get; set; }  //	婚姻	字符	2	可选	GB/T 2261.2-2003
        public string XZZ_S0 { get; set; }  //	现住址（省）	字符	6	可选	GB/T2260
        public string XZZ_S1 { get; set; }  //	现住址（市）	字符	6	可选	GB/T2260
        public string XZZ_X { get; set; }   //	现住址（县）	字符	6	可选	GB/T2260
        public string XZZ_DZ { get; set; }  //	现住址（地址）	字符	254	可选	
        public string DH { get; set; }  //	电话号码	字符	20	可选	
        public string YB1 { get; set; } //	邮编(现住址)	字符	6	可选	
        public string HKDZ { get; set; }    //	户口地址	字符	254	可选	
        public string YB2 { get; set; } //	邮编(户口地址)	字符	6	可选	
        public string GZDWJMC { get; set; } //	工作单位名称	字符	254	可选	
        public string DWDH { get; set; }    //	工作单位电话	字符	20	可选	
        public string YB3 { get; set; } //	工作单位邮编	字符	6	可选	
        public string LXRXM { get; set; }   //	联系人姓名	字符	20	可选	
        public string GX { get; set; }  //	关系(联系人与患者关系)	字符	1	可选	GB/T 4761-2008
        public string LXR_S0 { get; set; }  //	联系人地址（省）	字符	6	可选	GB/T2260
        public string LXR_S1 { get; set; }  //	联系人地址（市）	字符	6	可选	GB/T2260
        public string LXR_X { get; set; }   //	联系人地址（县）	字符	6	可选	GB/T2260
        public string LXR_DZ { get; set; }  //	联系人地址（地址）	字符	254	可选	
        public string DH1 { get; set; } //	联系人电话号码	字符	20	可选	
        public string RYTJ { get; set; }    //	入院途径	字符	1	可选	CV09.00.403
        public string RYSJ { get; set; }    //	入院时间	字符	18	必填	格式：yyyy-MM-dd HH:mm:ss
        public string RYKB { get; set; }    //	入院科别	字符	100	可选	编码。见科别字典
        public string RYBF { get; set; }    //	入院病房	字符	100	可选	
        public string ZKKB { get; set; }    //	转科科别	字符	100	可选	编码。见科别字典　
        public string WSY_YLJG { get; set; }    //	医嘱转社区	字符	200	可选	
        public string YZZY_YLJG { get; set; }   //	医嘱转院	字符	200	可选	
        public string CYSJ { get; set; }    //	出院时间	字符	12	必填	格式：yyyy-MM-dd HH:mm:ss
        public string CYKB { get; set; }    //	出院科别	字符	100	可选	编码。见科别字典
        public string CYBF { get; set; }    //	出院病房	字符	100	可选	
        public string SJZYTS { get; set; }  //	实际住院天数	数字	8,2	必填	
        public string MZZD { get; set; }    //	门(急)诊诊断名称（西医诊断）	字符	200	可选	
        public string JBBM_S { get; set; }  //	门(急)诊诊断名称（西医诊断）疾病代码	字符	20	可选	
        public string MZZD_XYZD { get; set; }   //	门(急)诊诊断名称（中医诊断）	字符	200	可选	中医病案首页
        public string JBBM_S2 { get; set; } //	门(急)诊诊断名称（中医诊断）疾病代码	字符	20	可选	中医病案首页
        public string WBYY { get; set; }    //	损伤、中毒的外部原因名称	字符	100	可选	
        public string JBBM1_S { get; set; } //	损伤、中毒的外部原因	字符	10	可选	
        public string BLZD { get; set; }    //	病理诊断名称	字符	100	可选	
        public string JBBM2_S { get; set; } //	病理诊断	字符	10	可选	
        public string BLH { get; set; } //	病理号	字符	100	可选	病理标本编号
        public string YWGM { get; set; }    //	药物过敏	字符	100	可选	编码。见无有字典
        public string GMYW { get; set; }    //	过敏药物	字符	100	可选	
        public string SJ { get; set; }  //	死亡患者尸检	字符	1	可选	编码。见是否字典
        public string XX { get; set; }  //	血型	字符	1	可选	CV04.50.005
        public string RH { get; set; }  //	RH	字符	1	可选	CV04.50.020
        public string QJCS { get; set; }    //	抢救次数	数字	12	可选	
        public string CGCS { get; set; }    //	成功次数	数字	12	可选	
        public string SXFY { get; set; }    //	输血反应	字符	1	可选	编码。见有无字典
        public string RCMDSC { get; set; }  //	妊娠梅毒筛查	字符	1	可选	编码。见是否字典
        public string XSRJBSC { get; set; } //	新生儿疾病筛查	字符	50	可选	编码。见新生儿疾病筛查说明
        public string CHCX { get; set; }    //	产后出血	字符	1	可选	编码。见是否字典
        public string XSRXB { get; set; }   //	新生儿性别	字符	1	可选	GB/T 2261.1-2003
        public string XSRTZ { get; set; }   //	新生儿体重（g）	数字	12,2	可选	单位：克
        public string XB_SBT { get; set; }  //	新生儿性别(双胞胎)	字符	12	可选	GB/T 2261.1-2003
        public string TZ_SBT { get; set; }  //	新生儿体重(双胞胎)	数字	12,2	可选	单位：克
        public string XB_SABT { get; set; } //	新生儿性别(三胞胎)	字符	12	可选	GB/T 2261.1-2003
        public string TZ_SABT { get; set; } //	新生儿体重(三胞胎)	数字	12,2	可选	单位：克
        public string KZR_DM { get; set; }  //	科主任医保医师代码	字符	10	可选	
        public string KZR { get; set; } //	科主任姓名	字符	100	可选	
        public string ZRYS_DM { get; set; } //	主任（副主任）医保医师代码	字符	15	可选	
        public string ZRYS { get; set; }    //	主任（副主任）医师姓名	字符	100	可选	
        public string ZZYS_DM { get; set; } //	主治医师医保医师代码	字符	15	可选	
        public string ZZYS { get; set; }    //	主治医师姓名	字符	100	可选	
        public string ZYYS_DM { get; set; } //	住院医师医保医师代码	字符	15	可选	
        public string ZYYS { get; set; }    //	住院医师姓名	字符	100	可选	
        public string JXYS_DM { get; set; } //	进修医生医保医师代码	字符	15	可选	
        public string JXYS { get; set; }    //	进修医师姓名	字符	100	可选	
        public string ZRHS { get; set; }    //	责任护士	字符	100	可选	
        public string SXYS { get; set; }    //	实习医师	字符	100	可选	
        public string BMY { get; set; } //	编码员	字符	100	可选	
        public string BAZL { get; set; }    //	病案质量	字符	1	可选	编码。见病案质量字典
        public string ZKYS { get; set; }    //	质控医师	字符	100	可选	
        public string ZKHS { get; set; }    //	质控护士	字符	100	可选	
        public string ZKRQ { get; set; }    //	质控日期	字符	12	可选	格式：YYYYMMDD
        public string LYFS { get; set; }    //	离院方式	字符	1	必填	CV06.00.226
        public string ZZYJH { get; set; }   //	是否有再住院计划	字符	1	可选	1.无  2.有
        public string MD { get; set; }  //	目的	字符	100	可选	
        public string RYQ_T { get; set; }   //	入院前天	数字	10	可选	颅脑损伤患者昏迷时间
        public string RYQ_XS { get; set; }  //	入院前小时	数字	10	可选	颅脑损伤患者昏迷时间
        public string RYQ_F { get; set; }   //	入院前分钟	数字	10	可选	颅脑损伤患者昏迷时间
        public string RYH_T { get; set; }   //	入院后天	数字	10	可选	颅脑损伤患者昏迷时间
        public string RYH_XS { get; set; }  //	入院后小时	数字	10	可选	颅脑损伤患者昏迷时间
        public string RYH_F { get; set; }   //	入院后分钟	数字	10	可选	颅脑损伤患者昏迷时间
        public string ZFY { get; set; } //	总费用	数字	12,2	必填	单位：元；
        public string ZFJE { get; set; }    //	自付金额	数字	12,2	必填	单位：元，医保支付范围内由个人自付金额；
        public string ZFIJE { get; set; }   //	自费金额	数字	12,2	必填	单位：元，医保支付范围外由个人自费金额；
        public string QTZF { get; set; }    //	其他支付	数字	12,2	必填	单位：元
        public string ZLLB { get; set; }    //	治疗类别	字符	3	可选	中医病案首页、CV06.00.225
        public string SSLCLJ2 { get; set; } //	实施临床路径	字符	1	可选	中医病案首页、编码。见实施临床路径字典
        public string ZYYJ { get; set; }    //	使用医疗机构中药制剂	字符	1	可选	中医病案首页
        public string ZYZLSB { get; set; }  //	使用中医诊疗设备	字符	1	可选	中医病案首页
        public string ZYZLJS { get; set; }  //	使用中医诊疗技术	字符	1	可选	中医病案首页
        public string BZSH { get; set; }    //	辩证施护	字符	1	可选	中医病案首页
        public List<zdxx> ZDXX { get; set; }    //	诊断信息			必填	循环体
        public List<ssxx> SSXX { get; set; }    //	手术信息			可选	循环体
        public List<fyxx> FYXX { get; set; }    //	费用信息			必填	循环体
        public List<fpxx> FPXX { get; set; }	//	发票和结算信息			必填	循环体
    }
    public class jylshxx
    {
        public string LSH { get; set; } //中心交易流水号 字符	16	必填	复合主键; 医保实时患者填写“中心流水号”，非医保实时患者填写16个0
        public string LSH_LX { get; set; }//结算类型 字符	2	必填	1:普通 2:高价药 9:其它
    }
    public class zdxx
    {
        public string ZDXH { get; set; } //    诊断序号         字符	2	必填	00：主要诊断、01至14为其它诊断
        public string ZDBM1 { get; set; }   //疾病编码
        public string ZDMC1 { get; set; }   //疾病名称
        public string ZYZDBM { get; set; }  //中医诊断编码  字符	32	可选	中医病案首页必填
        public string ZYZDMC { get; set; }  //中医诊断名称  字符	200	可选	中医病案首页必填
        public string RYBQ { get; set; }    //入院病情  字符	1	可选	CV05.10.019
        public string CYQK { get; set; }	//出院情况  字符	200	可选	编码。见出院情况字典
    }
    public class ssxx
    {
        public string SSXH { get; set; } //	手术序号	字符	2	必填	01至07
        public string SSBM1 { get; set; } //	手术及操作编码	字符	22	必填	
        public string SSJCZMC1 { get; set; } //	手术及操作名称	字符	200	必填	
        public string SSJCZRQ { get; set; } //	手术及操作日期	字符	12	必填	格式：YYYYMMDD
        public string SSJB { get; set; } //	手术级别	字符	1	必填	CV05.10.024
        public string SZ_DM { get; set; } //	手术者医保医师代码	字符	15	必填	
        public string SZ { get; set; } //	手术者姓名	字符	100	必填	
        public string YZ { get; set; } //	Ⅰ助签名	字符	100	可选	
        public string EZ { get; set; } //	Ⅱ助签名	字符	100	可选	
        public string QKYLB { get; set; } //	切口类别	字符	5	可选	CV05.10.023
        public string MZFS { get; set; } //	麻醉方式	字符	100	可选	CV06.00.103
        public string MZYSDM { get; set; } //	麻醉医保医师代码	字符	15	必填	
        public string MZYS { get; set; } //	麻醉医师姓名	字符	100	必填	
    }
    public class fyxx
    {
        public string BAFYLB { get; set; }//病案费用类别 字符	3	必填	见2.1.8病案费用类别填写说明；西医病案首页：总费费用（即：合计）、24个病案费用类别；中医病案首页：总费费用（即：合计）、26个病案费用类别；
        public string JE_0 { get; set; }//金额 数字	12,2	可选

    }
    public class fpxx
    {
        public string FPH { get; set; }//发票号               字符	32	必填	
        public List<lshxx> LSHXX { get; set; }//中心交易流水号 可选	循环体；医保实时交易必填。
        public string FPJE { get; set; }//发票金额 数字	12,2	必填	单位：元
        public string FPRQ { get; set; }//发票日期 字符	12	必填	格式：YYYYMMDD

    }
    public class lshxx
    {
        public string LSHXXXHT { get; set; }//中心交易流水号      字符	16	必填	医保实时结算患者填写“中心流水号”
    }
}
