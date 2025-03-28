namespace NewtouchHIS.Domain.InterfaceObjets.Pacs
{
    public class PACSgetPatientReportVO
    {
        public string hosCode { get; set; }
        public string hosName { get; set; }
        public string source { get; set; }
        public int patInfoId { get; set; }
        public string reportTime { get; set; }
        public int eqpTypeCode { get; set; }
        public string eqpTypeName { get; set; }
        public string pid { get; set; }
        public decimal? emergencyTag { get; set; }
        public decimal? chargeTag { get; set; }
        public int eqpCode { get; set; }
        public decimal? isPositive { get; set; }
        public string clinicNo { get; set; }
        public string inhosNo { get; set; }
        public string name { get; set; }
        public string pyCode { get; set; }
        public int age { get; set; }
        public int ageUnit { get; set; }
        public string fullAge { get; set; }
        public int sex { get; set; }
        public string ssid { get; set; }
        public string phone { get; set; }
        public string birthday { get; set; }
        public string address { get; set; }
        public int patTypeCode { get; set; }
        public string patTypeName { get; set; }
        public string dptCode { get; set; }
        public string dptName { get; set; }
        public string dctCode { get; set; }
        public string dctName { get; set; }
        public string bedNum { get; set; }
        public string lczd { get; set; }
        public string applyTime { get; set; }
        public string auditTime { get; set; }
        public string reportUserCode { get; set; }
        public string reportUserName { get; set; }
        public string reportUserSign { get; set; }
        public string auditUserCode { get; set; }
        public string auditUserName { get; set; }
        public string auditUserSign { get; set; }
        public string result { get; set; }
        public string description { get; set; }
        public string interfaceId { get; set; }
        public string sourceCode { get; set; }
        public string accessNember { get; set; }
        public string studysInstanceUid { get; set; }
        public List<imageUrlList> imageUrlList { get; set; }
        public string advice { get; set; }
        public string reportPdfUrl { get; set; }
        public string photoTime { get; set; }
        public string operationUserName { get; set; }
        public string operationUserCode { get; set; }
        public string feeitemCode { get; set; }
        public string feeitemName { get; set; }
    }

    public class imageUrlList
    {
        public int patInfoId { get; set; }
        public string remark { get; set; }
        public string imageUrl { get; set; }
        public int selected { get; set; }
    }
    public class PACSgetPatientReportDTO
    {
        public string hosCode { get; set; }
        public string pid { get; set; }
        public string applyNo { get; set; }
        public string checkTime { get; set; }
        public string ssid { get; set; }

    }
}
