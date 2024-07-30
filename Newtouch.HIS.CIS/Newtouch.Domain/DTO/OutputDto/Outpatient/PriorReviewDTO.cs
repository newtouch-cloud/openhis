using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Domain.DTO.OutputDto
{
    [XmlRoot("INFO")]
    public class PriorReviewDTO
    {
        public MESSAGE MESSAGE { get; set; }
        public DATA DATA { get; set; }
    }
    public class MESSAGE
    {
        public string VERSION { get; set; }
    }
    public class DATA
    {
        public BEAN BEAN { get; set; }
    }
    public class BEAN
    {
        public string CURRENT_STATUS { get; set; }
        public string IS_ADD { get; set; }
        public string BILL_TYPE { get; set; }
        public string HIS_BILL_ID { get; set; }
        public string END_TIME { get; set; }
        public string HOSPITAL_CODE { get; set; }
        public string HOSPITAL_NAME { get; set; }
        public string PATIENT_CODE { get; set; }
        public string PATIENT_NAME { get; set; }
        public string PATIENT_SEX_CODE { get; set; }
        public string PATIENT_BIRTH { get; set; }
        public string PERSON_TYPE_CODE { get; set; }
        public string BENEFIT_TYPE_CODE { get; set; }
        public string SEE_DOCTOR_TYPE_CODE { get; set; }
        public string IS_WITHOUT_PLACE { get; set; }
        public string WITHOUT_PLACE_PERSON_TYPE { get; set; }
        public string WITHOUT_PLACE_UNIT_CODE { get; set; }
        public string IN_HOSPITAL_ICD_CODE { get; set; }
        public string IN_HOSPITAL_ICD_NAME { get; set; }
        public string OUT_HOSPITAL_ICD_CODE { get; set; }
        public string OUT_HOSPITAL_ICD_NAME { get; set; }
        public string OUT_HOSPITAL_REASON { get; set; }
        public string ICD1_CODE { get; set; }
        public string ICD1_NAME { get; set; }
        public string ICD2_CODE { get; set; }
        public string ICD2_NAME { get; set; }
        public string ICD3_CODE { get; set; }
        public string ICD3_NAME { get; set; }
        public string ICD4_CODE { get; set; }
        public string ICD4_NAME { get; set; }
        public string ICD5_CODE { get; set; }
        public string ICD5_NAME { get; set; }
        public string ICD6_CODE { get; set; }
        public string ICD6_NAME { get; set; }
        public string ICD7_CODE { get; set; }
        public string ICD7_NAME { get; set; }
        public string ICD8_CODE { get; set; }
        public string ICD8_NAME { get; set; }
        public string ICD9_CODE { get; set; }
        public string ICD9_NAME { get; set; }
        public string ICD10_CODE { get; set; }
        public string ICD10_NAME { get; set; }
        public string ICD11_CODE { get; set; }
        public string ICD11_NAME { get; set; }
        public string ICD12_CODE { get; set; }
        public string ICD12_NAME { get; set; }
        public string ICD13_CODE { get; set; }
        public string ICD13_NAME { get; set; }
        public string ICD14_CODE { get; set; }
        public string ICD14_NAME { get; set; }
        public string ICD15_CODE { get; set; }
        public string ICD15_NAME { get; set; }
        public string ICD16_CODE { get; set; }
        public string ICD16_NAME { get; set; }
        public string ICD17_CODE { get; set; }
        public string ICD17_NAME { get; set; }
        public string ICD18_CODE { get; set; }
        public string ICD18_NAME { get; set; }
        public string ICD19_CODE { get; set; }
        public string ICD19_NAME { get; set; }
        public string ICD20_CODE { get; set; }
        public string ICD20_NAME { get; set; }
        public string INPATIENT_NO { get; set; }
        public string OUT_HOSPITAL_OFFICE { get; set; }
        public string IN_HOSPITAL_TIME { get; set; }
        public string OUT_HOSPITAL_TIME { get; set; }
        public string SEE_DOCTOR_TIME { get; set; }
        public decimal? HEIGHT { get; set; }
        public decimal? WEIGHT { get; set; }
        public string IS_TRAN_IN_HOSPITAL { get; set; }
        public string IS_PREGNANCY { get; set; }
        public string IS_SUCKLING { get; set; }
        public string HIS_BILL_NO { get; set; }
        public string HARD_ILL_FLAG { get; set; }
        public string HARD_ILL_CODE { get; set; }
        public decimal BILL_TOTAL_MONEY { get; set; }
        public decimal IM_TOTAL_MONEY { get; set; }
        public string COL1 { get; set; }
        public string COL2 { get; set; }
        public string REMARK { get; set; }
        public List<ROW> ROWS { get; set; }
    }
    public class ROW
    {
        public string HIS_BILL_ID { get; set; }
        public string RECIPE_ID { get; set; }
        public string HIS_BILL_DETAIL_ID { get; set; }
        public string ITEM_TYPE { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public decimal NUMBER { get; set; }
        public decimal PRICE { get; set; }
        public decimal TOTAL_MONEY { get; set; }
        public string HOSPITAL_CODE { get; set; }
        public string HOSPITAL_NAME { get; set; }
        public string OFFICE_CODE { get; set; }
        public string OFFICE_NAME { get; set; }
        public string DOCTOR_CODE { get; set; }
        public string DOCTOR_NAME { get; set; }
        public string USE_AMOUNT { get; set; }
        public string PACKAGE_UNIT { get; set; }
        public string STD { get; set; }
        public string FREQUENCY { get; set; }
        public string USAGE { get; set; }
        public int USE_MEDI_DAYS { get; set; }
        public string GIVE_MEDI_WAY { get; set; }
        public decimal IM_MONEY { get; set; }
        public string REAL_NUMBER { get; set; }
        public string REAL_MONEY { get; set; }
        public string DOCTOR_LEVEL_CODE { get; set; }
        public string DOCTOR_DUTY_CODE { get; set; }
        public string COL1 { get; set; }
        public string COL2 { get; set; }
        public string REMARK { get; set; }
    }
}
