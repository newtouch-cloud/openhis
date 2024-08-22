using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.HeaSecReadInfo
{
    public class ReadSFZ : OutputBase
    {
        public string Name { get; set; }        //姓名
        public string Gender { get; set; }      //性别
        public string Ethnicity { get; set; }   //民族
        public string BirthDate { get; set; }   //出生日期YYYYMMDD
        public string Address { get; set; }     //地址
        public string IdNumber { get; set; }    //身份证号
        public string IssueDate { get; set; }   //发卡日期YYYYMMDD
        public string ExpiryDate { get; set; }  //有效日期YYYYMMDD
        public string IssuingAuthority { get; set; }    //签发机关
    }
}
