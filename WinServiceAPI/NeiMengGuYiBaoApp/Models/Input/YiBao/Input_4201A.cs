using System.Collections.Generic;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    //每次接口调用只上传一位患者的信息
    public class Input_4201A : InputBase
    {
        public List<feedetail_4201> feedetail { get; set; }
    }

}
