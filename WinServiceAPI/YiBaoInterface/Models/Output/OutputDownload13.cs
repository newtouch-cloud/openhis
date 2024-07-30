using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
    public class OutputDownload13
    {

        public OutputDownload13()
        {
            InitDataTable1301();
            InitDataTable1302();
            InitDataTable1303();
            InitDataTable1305();
            InitDataTable1306();
            InitDataTable1307();
            InitDataTable1308();
            InitDataTable1309();
            InitDataTable1310();
            InitDataTable1311();
            InitDataTable1313();
            InitDataTable1314();
            InitDataTable1315();
            InitDataTable3202();
      
        }
        public DataTable data1301;

        public DataTable data1302;

        public DataTable data1303;

        public DataTable data1305;

        public DataTable data1306;

        public DataTable data1307;

        public DataTable data1308;

        public DataTable data1309;

        public DataTable data1310;

        public DataTable data1311;

        public DataTable data1313;

        public DataTable data1314;

        public DataTable data1315;

        public DataTable data3202;//对账明细返回

        private void InitDataTable3202()
        {
            this.data3202 = new DataTable();
            data3202.Columns.Add("psn_no");
            data3202.Columns.Add("mdtrt_id");
            data3202.Columns.Add("setl_id");
            data3202.Columns.Add("msgid");
            data3202.Columns.Add("refd_setl_flag");
            data3202.Columns.Add("stmt_rslt");
            data3202.Columns.Add("memo");

        }

        private void InitDataTable1301()
        {
            this.data1301 = new DataTable();
            data1301.Columns.Add("医疗目录编码");
            data1301.Columns.Add("药品商品名");
            data1301.Columns.Add("通用名编号");
            data1301.Columns.Add("药品通用名");
            data1301.Columns.Add("化学名称");
            data1301.Columns.Add("别名");
            data1301.Columns.Add("英文名称");
            data1301.Columns.Add("注册名称");
            data1301.Columns.Add("药监本位码");
            data1301.Columns.Add("药品剂型");
            data1301.Columns.Add("药品剂型名称");
            data1301.Columns.Add("药品类别");
            data1301.Columns.Add("药品类别名称");
            data1301.Columns.Add("药品规格");
            data1301.Columns.Add("药品规格代码");
            data1301.Columns.Add("注册剂型");
            data1301.Columns.Add("注册规格");
            data1301.Columns.Add("注册规格代码");
            data1301.Columns.Add("每次用量");
            data1301.Columns.Add("使用频次");
            data1301.Columns.Add("酸根盐基");
            data1301.Columns.Add("国家药品编号");
            data1301.Columns.Add("用法");
            data1301.Columns.Add("中成药标志");
            data1301.Columns.Add("生产地类别");
            data1301.Columns.Add("生产地类别名称");
            data1301.Columns.Add("计价单位类型");
            data1301.Columns.Add("非处方药标志");
            data1301.Columns.Add("非处方药标志名称");
            data1301.Columns.Add("包装材质");
            data1301.Columns.Add("包装材质名称");
            data1301.Columns.Add("包装规格");
            data1301.Columns.Add("包装数量");
            data1301.Columns.Add("功能主治");
            data1301.Columns.Add("给药途径");
            data1301.Columns.Add("说明书");
            data1301.Columns.Add("开始日期");
            data1301.Columns.Add("结束日期");
            data1301.Columns.Add("最小使用单位");
            data1301.Columns.Add("最小销售单位");
            data1301.Columns.Add("最小计量单位");
            data1301.Columns.Add("最小包装数量");
            data1301.Columns.Add("最小包装单位");
            data1301.Columns.Add("最小制剂单位");
            data1301.Columns.Add("最小包装单位名称");
            data1301.Columns.Add("最小制剂单位名称");
            data1301.Columns.Add("转换比");
            data1301.Columns.Add("药品有效期");
            data1301.Columns.Add("最小计价单位");
            data1301.Columns.Add("五笔助记码");
            data1301.Columns.Add("拼音助记码");
            data1301.Columns.Add("分包装厂家");
            data1301.Columns.Add("生产企业编号");
            data1301.Columns.Add("生产企业名称");
            data1301.Columns.Add("特殊限价药品标志");
            data1301.Columns.Add("特殊药品标志");
            data1301.Columns.Add("限制使用范围");
            data1301.Columns.Add("限制使用标志");
            data1301.Columns.Add("药品注册证号");
            data1301.Columns.Add("药品注册证号开始日期");
            data1301.Columns.Add("药品注册证号结束日期");
            data1301.Columns.Add("批准文号");
            data1301.Columns.Add("批准文号开始日期");
            data1301.Columns.Add("批准文号结束日期");
            data1301.Columns.Add("市场状态");
            data1301.Columns.Add("市场状态名称");
            data1301.Columns.Add("药品注册批件电子档案");
            data1301.Columns.Add("药品补充申请批件电子档案");
            data1301.Columns.Add("国家医保药品目录备注");
            data1301.Columns.Add("基本药物标志名称");
            data1301.Columns.Add("基本药物标志");
            data1301.Columns.Add("增值税调整药品标志");
            data1301.Columns.Add("增值税调整药品名称");
            data1301.Columns.Add("上市药品目录集药品");
            data1301.Columns.Add("医保谈判药品标志");
            data1301.Columns.Add("医保谈判药品名称");
            data1301.Columns.Add("卫健委药品编码");
            data1301.Columns.Add("备注");
            data1301.Columns.Add("有效标志");
            data1301.Columns.Add("唯一记录号");
            data1301.Columns.Add("数据创建时间");
            data1301.Columns.Add("数据更新时间");
            data1301.Columns.Add("版本号");
            data1301.Columns.Add("版本名称");
            data1301.Columns.Add("儿童用药");
            data1301.Columns.Add("公司名称");
            data1301.Columns.Add("仿制药一致性评价药品");
            data1301.Columns.Add("经销企业");
            data1301.Columns.Add("经销企业联系人");
            data1301.Columns.Add("经销企业授权书电子档案");
            data1301.Columns.Add("国家医保药品目录剂型");
            data1301.Columns.Add("国家医保药品目录甲乙类标识");
        }

        private void InitDataTable1302()
        {
            this.data1302 = new DataTable();
            data1302.Columns.Add("医疗目录编码");
            data1302.Columns.Add("单味药名称");
            data1302.Columns.Add("单复方标志");
            data1302.Columns.Add("质量等级");
            data1302.Columns.Add("中草药年份");
            data1302.Columns.Add("药用部位");
            data1302.Columns.Add("安全计量");
            data1302.Columns.Add("常规用法");
            data1302.Columns.Add("性味");
            data1302.Columns.Add("归经");
            data1302.Columns.Add("品种");
            data1302.Columns.Add("开始日期");
            data1302.Columns.Add("结束日期");
            data1302.Columns.Add("有效标志");
            data1302.Columns.Add("唯一记录号");
            data1302.Columns.Add("数据创建时间");
            data1302.Columns.Add("数据更新时间");
            data1302.Columns.Add("版本号");
            data1302.Columns.Add("版本名称");
            data1302.Columns.Add("药材名称");
            data1302.Columns.Add("功能主治");
            data1302.Columns.Add("炮制方法");
            data1302.Columns.Add("功效分类");
            data1302.Columns.Add("药材种来源");
            data1302.Columns.Add("国家医保支付政策");
            data1302.Columns.Add("省级医保支付政策");
            data1302.Columns.Add("标准名称");
            data1302.Columns.Add("标准页码");

        }

        private void InitDataTable1303()
        {
            this.data1303 = new DataTable();
            data1303.Columns.Add("医疗目录编码");
            data1303.Columns.Add("药品商品名");
            data1303.Columns.Add("别名");
            data1303.Columns.Add("英文名称");
            data1303.Columns.Add("剂型");
            data1303.Columns.Add("剂型名称");
            data1303.Columns.Add("注册剂型");
            data1303.Columns.Add("成分");
            data1303.Columns.Add("功能主治");
            data1303.Columns.Add("性状");
            data1303.Columns.Add("药品规格");
            data1303.Columns.Add("药品规格代码");
            data1303.Columns.Add("注册规格");
            data1303.Columns.Add("注册规格代码");
            data1303.Columns.Add("给药途径");
            data1303.Columns.Add("贮藏");
            data1303.Columns.Add("使用频次");
            data1303.Columns.Add("每次用量");
            data1303.Columns.Add("药品类别");
            data1303.Columns.Add("药品类别名称");
            data1303.Columns.Add("非处方药标志");
            data1303.Columns.Add("非处方药标志名称");
            data1303.Columns.Add("包装材质");
            data1303.Columns.Add("包装材质名称");
            data1303.Columns.Add("包装规格");
            data1303.Columns.Add("说明书");
            data1303.Columns.Add("包装数量");
            data1303.Columns.Add("最小使用单位");
            data1303.Columns.Add("最小销售单位");
            data1303.Columns.Add("最小计量单位");
            data1303.Columns.Add("最小包装数量");
            data1303.Columns.Add("最小包装单位");
            data1303.Columns.Add("最小制剂单位");
            data1303.Columns.Add("最小制剂单位名称");
            data1303.Columns.Add("药品有效期");
            data1303.Columns.Add("最小计价单位");
            data1303.Columns.Add("不良反应");
            data1303.Columns.Add("注意事项");
            data1303.Columns.Add("禁忌");
            data1303.Columns.Add("生产企业编号");
            data1303.Columns.Add("生产企业名称");
            data1303.Columns.Add("生产企业地址");
            data1303.Columns.Add("特殊限价药品标志");
            data1303.Columns.Add("批准文号");
            data1303.Columns.Add("批准文号开始日期");
            data1303.Columns.Add("批准文号结束日期");
            data1303.Columns.Add("药品注册证号");
            data1303.Columns.Add("药品注册证号开始日期");
            data1303.Columns.Add("药品注册证号结束日期");
            data1303.Columns.Add("转换比");
            data1303.Columns.Add("限制使用范围");
            data1303.Columns.Add("最小包装单位名称");
            data1303.Columns.Add("注册名称");
            data1303.Columns.Add("分包装厂家");
            data1303.Columns.Add("市场状态");
            data1303.Columns.Add("药品注册批件电子档案");
            data1303.Columns.Add("药品补充申请批件电子档案");
            data1303.Columns.Add("国家医保药品目录编号");
            data1303.Columns.Add("国家医保药品目录备注");
            data1303.Columns.Add("增值税调整药品标志");
            data1303.Columns.Add("增值税调整药品名称");
            data1303.Columns.Add("上市药品目录集药品");
            data1303.Columns.Add("卫健委药品编码");
            data1303.Columns.Add("备注");
            data1303.Columns.Add("有效标志");
            data1303.Columns.Add("开始时间");
            data1303.Columns.Add("结束时间");
            data1303.Columns.Add("唯一记录号");
            data1303.Columns.Add("数据创建时间");
            data1303.Columns.Add("数据更新时间");
            data1303.Columns.Add("版本号");
            data1303.Columns.Add("版本名称");
            data1303.Columns.Add("自制剂许可证号");
            data1303.Columns.Add("儿童用药");
            data1303.Columns.Add("老年患者用药");
            data1303.Columns.Add("医疗机构联系人姓名");
            data1303.Columns.Add("医疗机构联系人电话");
            data1303.Columns.Add("自制剂许可证电子档案");
        }

        private void InitDataTable1305()
        {
            this.data1305 = new DataTable();
            data1305.Columns.Add("医疗目录编码");
            data1305.Columns.Add("计价单位");
            data1305.Columns.Add("计价单位名称");
            data1305.Columns.Add("诊疗项目说明");
            data1305.Columns.Add("诊疗除外内容");
            data1305.Columns.Add("诊疗项目内涵");
            data1305.Columns.Add("有效标志");
            data1305.Columns.Add("备注");
            data1305.Columns.Add("服务项目类别");
            data1305.Columns.Add("医疗服务项目名称");
            data1305.Columns.Add("项目说明");
            data1305.Columns.Add("开始日期");
            data1305.Columns.Add("结束日期");
            data1305.Columns.Add("唯一记录号");
            data1305.Columns.Add("版本号");
            data1305.Columns.Add("版本名称");

        }

        private void InitDataTable1306()
        {
            this.data1306 = new DataTable();
            data1306.Columns.Add("医疗目录编码");
            data1306.Columns.Add("耗材名称");
            data1306.Columns.Add("医疗器械唯一标识码");
            data1306.Columns.Add("医保通用名代码");
            data1306.Columns.Add("医保通用名");
            data1306.Columns.Add("产品型号");
            data1306.Columns.Add("规格代码");
            data1306.Columns.Add("规格");
            data1306.Columns.Add("耗材分类");
            data1306.Columns.Add("规格型号");
            data1306.Columns.Add("材质代码");
            data1306.Columns.Add("耗材材质");
            data1306.Columns.Add("包装规格");
            data1306.Columns.Add("包装数量");
            data1306.Columns.Add("产品包装材质");
            data1306.Columns.Add("包装单位");
            data1306.Columns.Add("产品转换比");
            data1306.Columns.Add("最小使用单位");
            data1306.Columns.Add("生产地类别");
            data1306.Columns.Add("生产地类别名称");
            data1306.Columns.Add("产品标准");
            data1306.Columns.Add("产品有效期");
            data1306.Columns.Add("性能结构与组成");
            data1306.Columns.Add("适用范围");
            data1306.Columns.Add("产品使用方法");
            data1306.Columns.Add("产品图片编号");
            data1306.Columns.Add("产品质量标准");
            data1306.Columns.Add("说明书");
            data1306.Columns.Add("其他证明材料");
            data1306.Columns.Add("专机专用标志");
            data1306.Columns.Add("专机名称");
            data1306.Columns.Add("组套名称");
            data1306.Columns.Add("机套标志");
            data1306.Columns.Add("限制使用标志");
            data1306.Columns.Add("医保限用范围");
            data1306.Columns.Add("最小销售单位");
            data1306.Columns.Add("高值耗材标志");
            data1306.Columns.Add("医用材料分类代码");
            data1306.Columns.Add("植入材料和人体器官标志");
            data1306.Columns.Add("灭菌标志");
            data1306.Columns.Add("灭菌标志名称");
            data1306.Columns.Add("植入或介入类标志");
            data1306.Columns.Add("植入或介入类名称");
            data1306.Columns.Add("一次性使用标志");
            data1306.Columns.Add("一次性使用标志名称");
            data1306.Columns.Add("注册备案人名称");
            data1306.Columns.Add("开始日期");
            data1306.Columns.Add("结束日期");
            data1306.Columns.Add("医疗器械管理类别");
            data1306.Columns.Add("医疗器械管理类别名称");
            data1306.Columns.Add("注册备案号");
            data1306.Columns.Add("注册备案产品名称");
            data1306.Columns.Add("结构及组成");
            data1306.Columns.Add("其他内容");
            data1306.Columns.Add("批准日期");
            data1306.Columns.Add("注册备案人住所");
            data1306.Columns.Add("注册证有效期开始时间");
            data1306.Columns.Add("注册证有效期结束时间");
            data1306.Columns.Add("生产企业编号");
            data1306.Columns.Add("生产企业名称");
            data1306.Columns.Add("生产地址");
            data1306.Columns.Add("代理人企业");
            data1306.Columns.Add("代理人企业地址");
            data1306.Columns.Add("生产国或地区");
            data1306.Columns.Add("售后服务机构");
            data1306.Columns.Add("注册或备案证电子档案");
            data1306.Columns.Add("产品影像");
            data1306.Columns.Add("有效标志");
            data1306.Columns.Add("唯一记录号");
            data1306.Columns.Add("版本号");
            data1306.Columns.Add("版本名称");

        }

        private void InitDataTable1307()
        {
            this.data1307 = new DataTable();
            data1307.Columns.Add("西医疾病诊断ID");
            data1307.Columns.Add("章");
            data1307.Columns.Add("章代码范围");
            data1307.Columns.Add("章名称");
            data1307.Columns.Add("节代码范围");
            data1307.Columns.Add("节名称");
            data1307.Columns.Add("类目代码");
            data1307.Columns.Add("类目名称");
            data1307.Columns.Add("亚目代码");
            data1307.Columns.Add("亚目名称");
            data1307.Columns.Add("诊断代码");
            data1307.Columns.Add("诊断名称");
            data1307.Columns.Add("使用标记");
            data1307.Columns.Add("国标版诊断代码");
            data1307.Columns.Add("国标版诊断名称");
            data1307.Columns.Add("临床版诊断代码");
            data1307.Columns.Add("临床版诊断名称");
            data1307.Columns.Add("备注");
            data1307.Columns.Add("有效标志");
            data1307.Columns.Add("唯一记录号");
            data1307.Columns.Add("数据创建时间");
            data1307.Columns.Add("数据更新时间");
            data1307.Columns.Add("版本号");
            data1307.Columns.Add("版本名称");

        }

        private void InitDataTable1308()
        {
            this.data1308 = new DataTable();
            data1308.Columns.Add("手术标准目录 ID");
            data1308.Columns.Add("章");
            data1308.Columns.Add("章代码范围");
            data1308.Columns.Add("章名称");
            data1308.Columns.Add("类目代码");
            data1308.Columns.Add("类目名称");
            data1308.Columns.Add("亚目代码");
            data1308.Columns.Add("亚目名称");
            data1308.Columns.Add("细目代码");
            data1308.Columns.Add("细目名称");
            data1308.Columns.Add("手术操作代码");
            data1308.Columns.Add("手术操作名称");
            data1308.Columns.Add("使用标记");
            data1308.Columns.Add("团标版手术操作代码");
            data1308.Columns.Add("团标版手术操作名称");
            data1308.Columns.Add("临床版手术操作代码");
            data1308.Columns.Add("临床版手术操作名称");
            data1308.Columns.Add("备注");
            data1308.Columns.Add("有效标志");
            data1308.Columns.Add("唯一记录号");
            data1308.Columns.Add("数据创建时间");
            data1308.Columns.Add("数据更新时间");
            data1308.Columns.Add("版本号");
            data1308.Columns.Add("版本名称");

        }

        private void InitDataTable1309()
        {
            this.data1309 = new DataTable();
            data1309.Columns.Add("门慢门特病种目录代码");
            data1309.Columns.Add("门慢门特病种大类名称");
            data1309.Columns.Add("门慢门特病种细分类名称");
            data1309.Columns.Add("医保区划");
            data1309.Columns.Add("备注");
            data1309.Columns.Add("有效标志");
            data1309.Columns.Add("唯一记录号");
            data1309.Columns.Add("数据创建时间");
            data1309.Columns.Add("数据更新时间");
            data1309.Columns.Add("版本号");
            data1309.Columns.Add("病种内涵");
            data1309.Columns.Add("版本名称");
            data1309.Columns.Add("诊疗指南页码");
            data1309.Columns.Add("诊疗指南电子档案");
            data1309.Columns.Add("门慢门特病种名称");
            data1309.Columns.Add("门慢门特病种大类代码");

        }

        private void InitDataTable1310()
        {
            this.data1310 = new DataTable();
            data1310.Columns.Add("病种结算目录 ID");
            data1310.Columns.Add("按病种结算病种目录代码");
            data1310.Columns.Add("按病种结算病种名称");
            data1310.Columns.Add("限定手术操作代码");
            data1310.Columns.Add("限定手术操作名称");
            data1310.Columns.Add("有效标志");
            data1310.Columns.Add("唯一记录号");
            data1310.Columns.Add("数据创建时间");
            data1310.Columns.Add("数据更新时间");
            data1310.Columns.Add("版本号");
            data1310.Columns.Add("病种内涵");
            data1310.Columns.Add("备注");
            data1310.Columns.Add("版本名称");
            data1310.Columns.Add("诊疗指南页码");
            data1310.Columns.Add("诊疗指南电子档案");

        }

        private void InitDataTable1311()
        {
            this.data1311 = new DataTable();
            data1311.Columns.Add("日间手术治疗目录ID");
            data1311.Columns.Add("日间手术病种目录代码");
            data1311.Columns.Add("日间手术病种名称");
            data1311.Columns.Add("有效标志");
            data1311.Columns.Add("唯一记录号");
            data1311.Columns.Add("数据创建时间");
            data1311.Columns.Add("数据更新时间");
            data1311.Columns.Add("版本号");
            data1311.Columns.Add("病种内涵");
            data1311.Columns.Add("备注");
            data1311.Columns.Add("版本名称");
            data1311.Columns.Add("诊疗指南页码");
            data1311.Columns.Add("诊疗指南电子档案");
            data1311.Columns.Add("手术操作名称");
            data1311.Columns.Add("手术操作代码");

        }

        private void InitDataTable1313()
        {
            this.data1313 = new DataTable();
            data1313.Columns.Add("肿瘤形态学 ID");
            data1313.Columns.Add("肿瘤/细胞类型代码");
            data1313.Columns.Add("肿瘤/细胞类型");
            data1313.Columns.Add("形态学分类代码");
            data1313.Columns.Add("形态学分类");
            data1313.Columns.Add("有效标志");
            data1313.Columns.Add("唯一记录号");
            data1313.Columns.Add("数据创建时间");
            data1313.Columns.Add("数据更新时间");
            data1313.Columns.Add("版本号");
            data1313.Columns.Add("版本名称");
        }
        private void InitDataTable1314()
        {
            this.data1314 = new DataTable();
            data1314.Columns.Add("中医疾病诊断 ID");
            data1314.Columns.Add("科别类目代码");
            data1314.Columns.Add("科别类目名称");
            data1314.Columns.Add("专科系统分类目代码");
            data1314.Columns.Add("专科系统分类目名称");
            data1314.Columns.Add("疾病分类代码");
            data1314.Columns.Add("疾病分类名称");
            data1314.Columns.Add("备注");
            data1314.Columns.Add("有效标志");
            data1314.Columns.Add("唯一记录号");
            data1314.Columns.Add("数据创建时间");
            data1314.Columns.Add("数据更新时间");
            data1314.Columns.Add("版本号");
            data1314.Columns.Add("版本名称");
        }

        private void InitDataTable1315()
        {
            this.data1315 = new DataTable();
            data1315.Columns.Add("中医证候 ID");
            data1315.Columns.Add("证候类目代码");
            data1315.Columns.Add("证候类目名称");
            data1315.Columns.Add("证候属性代码");
            data1315.Columns.Add("证候属性");
            data1315.Columns.Add("证候分类代码");
            data1315.Columns.Add("证候分类名称");
            data1315.Columns.Add("备注");
            data1315.Columns.Add("有效标志");
            data1315.Columns.Add("唯一记录号");
            data1315.Columns.Add("数据创建时间");
            data1315.Columns.Add("数据更新时间");
            data1315.Columns.Add("版本号");
            data1315.Columns.Add("版本名称");
        }
    }
}
