using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_01
{
    /// <summary>
    /// 推送患者信息请求体
    /// </summary>
    [XmlRoot("Request")]
    public class PushPatientRequestEntity : RequestBase
    {
        /// <summary>
        /// 患者信息体
        /// </summary>
        public Patient Patient { get; set; }
    }

    /// <summary>
    /// 患者信息体
    /// </summary>
    public class Patient
    {

        private string _id = "";

        /// <summary>
        ///患者编号（一个患者对应一个编号，且与身份证号码一一对应）
        /// 必填
        /// </summary>
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }


        private string _name = "";
        /// <summary>
        /// 患者姓名
        /// 必填
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _genderCode = "";
        /// <summary>
        /// 患者性别编码
        /// 必填
        /// </summary>
        public string genderCode
        {
            get { return _genderCode; }
            set { _genderCode = value; }
        }

        private string _gender = "";
        /// <summary>
        /// 患者性别
        /// 必填
        /// </summary>
        public string gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        private string _birthday = "";
        /// <summary>
        /// 出生日期 YYYYMMDD
        /// 必填
        /// </summary>
        public string birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        private string _cardTypeCode = "";
        /// <summary>
        /// 证件类型编码
        /// 必填
        /// </summary>
        public string cardTypeCode
        {
            get { return _cardTypeCode; }
            set { _cardTypeCode = value; }
        }

        private string _cardType = "";
        /// <summary>
        /// 证件类型（一般指居民身份证号）
        /// 必填
        /// </summary>
        public string cardType
        {
            get { return _cardType; }
            set { _cardType = value; }
        }

        private string _cardNo = "";
        /// <summary>
        /// 证件号码
        /// 必填
        /// </summary>
        public string cardNo
        {
            get { return _cardNo; }
            set { _cardNo = value; }
        }

        private string _occupationCode = "";
        /// <summary>
        /// 职业编码
        /// </summary>
        public string occupationCode
        {
            get { return _occupationCode; }
            set { _occupationCode = value; }
        }

        private string _occupation = "";
        /// <summary>
        /// 职业
        /// </summary>
        public string occupation
        {
            get { return _occupation; }
            set { _occupation = value; }
        }

        private string _marriedCode = "";
        /// <summary>
        /// 婚姻状况编码
        /// </summary>
        public string marriedCode
        {
            get { return _marriedCode; }
            set { _marriedCode = value; }
        }

        private string _married = "";
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string married
        {
            get { return _married; }
            set { _married = value; }
        }

        private string _countryCode = "";
        /// <summary>
        /// 国籍编码
        /// </summary>
        public string countryCode
        {
            get { return _countryCode; }
            set { _countryCode = value; }
        }

        private string _country = "";
        /// <summary>
        /// 国籍
        /// </summary>
        public string country
        {
            get { return _country; }
            set { _country = value; }
        }

        private string _nationalityCode = "";
        /// <summary>
        /// 民族编码
        /// </summary>
        public string nationalityCode
        {
            get { return _nationalityCode; }
            set { _nationalityCode = value; }
        }

        private string _nationality = "";
        /// <summary>
        /// 民族
        /// </summary>
        public string nationality
        {
            get { return _nationality; }
            set { _nationality = value; }
        }

        private string _provinceCode = "";
        /// <summary>
        /// 家庭住址省份编码
        /// 必填
        /// </summary>
        public string provinceCode
        {
            get { return _provinceCode; }
            set { _provinceCode = value; }
        }

        private string _cityCode = "";
        /// <summary>
        /// 家庭住址市级编码
        /// 必填
        /// </summary>
        public string cityCode
        {
            get { return _cityCode; }
            set { _cityCode = value; }
        }

        private string _areaCode = "";
        /// <summary>
        /// 家庭住址区县编码（保持与云平台的行政区划编码一致）
        /// 必填
        /// </summary>
        public string areaCode
        {
            get { return _areaCode; }
            set { _areaCode = value; }
        }

        private string _province = "";
        /// <summary>
        /// 家庭住址省份
        /// </summary>
        public string province
        {
            get { return _province; }
            set { _province = value; }
        }

        private string _city = "";
        /// <summary>
        /// 家庭住址市级
        /// </summary>
        public string city
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _area = "";
        /// <summary>
        /// 家庭住址区县
        /// </summary>
        public string area
        {
            get { return _area; }
            set { _area = value; }
        }

        private string _streetInfo = "";
        /// <summary>
        /// 街道信息
        /// </summary>
        public string streetInfo
        {
            get { return _streetInfo; }
            set { _streetInfo = value; }
        }

        private string _postcode = "";
        /// <summary>
        /// 邮编
        /// </summary>
        public string postcode
        {
            get { return _postcode; }
            set { _postcode = value; }
        }

        private string _ctName = "";
        /// <summary>
        /// 联系人名称
        /// </summary>
        public string ctName
        {
            get { return _ctName; }
            set { _ctName = value; }
        }

        private string _ctRoleId = "";
        /// <summary>
        /// 与联系人关系
        /// </summary>
        public string ctRoleId
        {
            get { return _ctRoleId; }
            set { _ctRoleId = value; }
        }

        private string _ctAddr = "";
        /// <summary>
        /// 联系人地址
        /// </summary>
        public string ctAddr
        {
            get { return _ctAddr; }
            set { _ctAddr = value; }
        }

        private string _ctTelephone = "";
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ctTelephone
        {
            get { return _ctTelephone; }
            set { _ctTelephone = value; }
        }

        private string _patiTypeCode = "";
        /// <summary>
        /// 患者类型编码
        /// </summary>
        public string patiTypeCode
        {
            get { return _patiTypeCode; }
            set { _patiTypeCode = value; }
        }

        private string _patiType = "";
        /// <summary>
        /// 患者类型
        /// </summary>
        public string patiType
        {
            get { return _patiType; }
            set { _patiType = value; }
        }

        private string _telephone = "";
        /// <summary>
        /// 患者电话
        /// </summary>
        public string telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }

        private string _outpatientNo = "";
        /// <summary>
        /// 门诊号, 即就诊流水号
        /// 必填
        /// </summary>
        public string outpatientNo
        {
            get { return _outpatientNo; }
            set { _outpatientNo = value; }
        }

        private string _orgCode = "";
        /// <summary>
        /// 机构编码
        /// 必填
        /// </summary>
        public string orgCode
        {
            get { return _orgCode; }
            set { _orgCode = value; }
        }
    }

    /// <summary>
    /// 行政机构配置
    /// </summary>
    public class ConfigurationInfo
    {
        /// <summary>
        /// 乡镇
        /// </summary>
        public string xz { get; set; }

        /// <summary>
        /// 行政编码
        /// </summary>
        public string xzbm { get; set; }

        /// <summary>
        /// 机构代码
        /// </summary>
        public string jgdm { get; set; }

        /// <summary>
        /// 家庭住址区县编码（保持与云平台的行政区划编码一致）
        /// </summary>
        public string areaCode { get; set; }
    }
}