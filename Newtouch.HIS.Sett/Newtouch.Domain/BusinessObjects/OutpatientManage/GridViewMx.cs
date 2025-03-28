using System;
using System.Data;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class GridViewMx
    {
        public int id { get; set; }
        public string InfoSet { get; set; }
        public string DL { get; set; }
        public string CHUFANGHAO { get; set; }
        public string DLMC { get; set; }
        public string YPMC { get; set; }
        public string DANWEI { get; set; }
        public string DJ { get; set; }
        public string ZFBL { get; set; }
        public string SL { get; set; }

        public string JE { get; set; }
        public Decimal RETURNS_SL { get; set; }
        public string IS_RETURN { get; set; }
        public string SFXM { get; set; }
        public string czh { get; set; }
        public int CF_MXNM { get; set; }
        public string FWFDJ { get; set; }
        public int XMNM { get; set; }
        public string ZFXZ { get; set; }
        public string NBDL { get; set; }
        public int GHNM { get; set; }
        // public string pt_cfnm { get; set; }
        public DataRow tag { get; set; }

        #region GRS

        public DateTime? jzsj { get; set; }
        public string bz { get; set; }
        public int? duration { get; set; }
        public int? jsnm { get; set; }
        public string ysmc { get; set; }

        //public static implicit operator DataRow(GridViewMx v)
        //{
        //    DataTable dt = new DataTable();
        //    //dt.Columns.AddRange(v.id);
        //    var dr = dt.NewRow();
        //    dr[0] = v.id;
        //    dr[1] = v.InfoSet;
        //    dr[2] = v.DL;
        //    dr[3] = v.CHUFANGHAO;
        //    dr[4] = v.DLMC;
        //    dr[5] = v.YPMC;
        //    dr[6] = v.DANWEI;
        //    dr[7] = v.DJ;
        //    dr[8] = v.ZFBL;
        //    dr[9] = v.SL;
        //    dr[10] = v.JE;
        //    dr[11] = v.RETURNS_SL;
        //    dr[12] = v.IS_RETURN;
        //    dr[13] = v.SFXM;
        //    dr[14] = v.czh;
        //    dr[15] = v.CF_MXNM;
        //    dr[16] = v.FWFDJ;
        //    dr[17] = v.XMNM;
        //    dr[18] = v.ZFXZ;
        //    dr[18] = v.NBDL;
        //    dr[19] = v.GHNM;
        //    dr[20] = v.tag;
        //    return dr;
        //}
        #endregion
    }
}
