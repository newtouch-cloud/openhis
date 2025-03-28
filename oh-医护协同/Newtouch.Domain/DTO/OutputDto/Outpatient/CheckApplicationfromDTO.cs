using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.OutputDto.Outpatient
{
    public class CheckApplicationfromDTO
    {
        public RequestHost RequestHost;
        public Patient Patient;
    }
    public class RequestHost
    {
        //机构编码  Y
        public string MedNo { get; set; }
        //机构名称  Y
        public string MedName { get; set; }

    }
    public class Patient
    {
        //居民姓名 Y
        public string Name { get; set; }
        //性别  男/女 Y
        public string Gender { get; set; }
        //出生日期 Y 
        public DateTime? Birthday { get; set; }
        //身份证号 Y
        public string IdentityID { get; set; }
        // 病区号
        public string WardNo { get; set; }
        //床位号
        public string BedNo { get; set; }
        //病人卡号  Y
        public string CardID { get; set; }

        public Request Request;
    }
    public class Request
    {
        //申请单号  Y
        public string Reqno { get; set; }
        //申请日期  Y
        public DateTime? Indate { get; set; }
        //申请医生工号
        public string DoctorID { get; set; }
        // 申请医生姓名
        public string DoctorName { get; set; }
        // 申请医生身份证
        public string DoctorIdCard { get; set; }
        // 就诊记录编码  Y 门诊、住院、体检号
        public string RecordID { get; set; }
        //来源   Y 门诊 MagQ700001   住院 MagQ700002  体检 MagQ700003
        public string Source { get; set; }
        //影像报告类型
        public string Pic { get; set; }
        //报告明细类型  Y
        public string PicDetail { get; set; }
        //申请单名字
        public string ApplyName { get; set; }
        //部位:方法
        public string BodyPart { get; set; }
        //序号
        public int? WhichNo { get; set; }
        // 有效标志位  N/Y，N 表示取消
        public string IsVaild { get; set; }
        //是否已经收费
        public string IsCharge { get; set; }
        //收费日期
        public DateTime? ChargeDate { get; set; }
        //业务单据号
        public string InvoiceID { get; set; }
        //取消日期
        public DateTime? CancelDate { get; set; }
        //备注
        public string Comment { get; set; }
        //申请科室编码 Y
        public string ApplyDepartmentCode { get; set; }
        //申请科室名字 Y
        public string ApplyDepartmentName { get; set; }
        //临床诊断名字
        public string DiagnoseName { get; set; }

        public List<Order> Order;
    }
    public class Order
    {
        //申请单明细号  Y
        public string Seqno { get; set; }
        //申请单号  Y
        public string Reqno { get; set; }
        //项目编码  Y
        public string ItemID { get; set; }
        //项目名称  Y
        public string ItemName { get; set; }
        //部位 1,部位 2
        public string BodyPart { get; set; }
        //数量
        public string Qty { get; set; }
        //单价
        public string Price { get; set; }
        //序号
        public string SN { get; set; }
    }

    public class CheckfromDTO
    {
        // 就诊记录编码  Y 门诊、住院、体检号
        public string RecordID { get; set; }
        //申请单名字
        public string ApplyName { get; set; }
        //申请单号  Y
        public string Reqno { get; set; }
        //申请日期  Y
        public DateTime? Indate { get; set; }
        //申请医生工号
        public string DoctorID { get; set; }
        // 申请医生姓名
        public string DoctorName { get; set; }
        //申请科室编码 Y
        public string ApplyDepartmentCode { get; set; }
        //申请科室名字 Y
        public string ApplyDepartmentName { get; set; }
        // 病区号
        public string WardNo { get; set; }
        //床位号
        public string BedNo { get; set; }
        //病人卡号  Y
        public string CardID { get; set; }
        //申请单明细号  Y
        public string Seqno { get; set; }

        //项目编码  Y
        public string ItemID { get; set; }
        //项目名称  Y
        public string ItemName { get; set; }
        //数量
        public string Qty { get; set; }
        //单价
        public string Price { get; set; }

		public string PicDetail { get; set; }
		public string xm { get; set; }
        public string xb { get; set; }
        public DateTime? csny { get; set; }
        public string zjh { get; set; }
		

	}
    public class RefJson
    {
        public string Reqno { get; set; }
        public string status { get; set; }
        public string errorCode { get; set; }

    }
    public class retinfo {
        public INFO INFO { get; set; }
    }
    public class INFO
    {
        public MESSAGE MESSAGE { get; set; }
        public DATA DATA { get; set; }
    }
    public class MESSAGE
    {
        public string VERSION { get; set; }
        public string FORMAT { get; set; }
    }
    public class DATA
    {
        public BEAN BEAN { get; set; }
    }
    public class BEAN
    {
        public string BILL_ID { get; set; }
        public string HIS_BILL_ID { get; set; }
        public string IS_ERROR { get; set; }
    }
}
