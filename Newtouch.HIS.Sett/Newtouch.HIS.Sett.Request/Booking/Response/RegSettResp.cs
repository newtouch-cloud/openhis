namespace Newtouch.HIS.Sett.Request.Booking.Response
{
    public class RegSettResp
    {
        public string RegId { get; set; }
        public int QueueNo { get; set; }
        public string Mzh { get; set; }
    }
    public class CancalSettResp
    {
        public string RegId { get; set; }
    }

    public class OrderVo
    {
        public string mzzyh { get; set; }
        public string cardno { get; set; }
        public string outtradeno { get; set; }
        public string tradeno { get; set; }
        public decimal? totalamount { get; set; }
        public decimal? Realitytotalamount { get; set; }
        public string jsnm { get; set; }
    }
}
