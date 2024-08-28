using CQYiBaoInterface.Models.Input;
using CQYiBaoInterface.Models.Output;
using CQYiBaoInterface.Models.Post;
using CQYiBaoInterface.Models.SQL;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using SqlHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YiBaoInterface;

namespace YiBaoBase
{
    public partial class Frm_Main : XtraForm
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        private string czydm = ConfigurationManager.AppSettings["czydm"];
        private string czyxm = ConfigurationManager.AppSettings["czyxm"];
        private void sb_Find_Click(object sender, EventArgs e)
        {
            if (te_Bbh1.Text == "")
            {
                MessageBox.Show("请选择版本号");
                return;
            }
            gc_Ybfh1.DataSource = null;
            MessageBox.Show("下载时间较长1-5分钟请耐心等待！");

            string bbh = "";//版本号

            DataTable dtRreturnAll = null;// =new DataTable();

            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = cbe_Xznr1.Text.Substring(0, 4);    
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 1;

            int i = 1;
            while (i <= Convert.ToInt32(te_number1.Text.Trim()))
            {
                Input_13 input = new Input_13();
                input.data = new data13();
                if (bbh != "")
                {
                    input.data.ver = bbh;
                }
                else
                {
                    input.data.ver = te_Bbh1.Text;
                }
                string code = "1";
                Output_13 output13 = new Output_13();
                string json = ClassHelper.SaveToInterface1(input, out output13, post, out code);
                me_Ybfh1.Text = json;
                if (code != "0")//如果成功则更新本地信息表 zt=0 
                {
                    MessageBox.Show(json);
                    return;
                }

                code = "1";
                PostBase post1 = new PostBase();
                post1.hisId = "0";
                post1.tradiNumber = "9102";
                //post.sign_no = sign_no;
                post1.insuplc_admdvs = "";
                post1.inModel = 0;
                post1.operatorId = czydm;
                post1.operatorName = czyxm;

                Input_9102 input9102 = new Input_9102();
                input9102.fsDownloadIn = new fsUploadIn9102();
                input9102.fsDownloadIn.filename = output13.filename;
                input9102.fsDownloadIn.fixmedins_code = "plc";// ConfigurationManager.AppSettings["fixmedins_code"];
                input9102.fsDownloadIn.file_qury_no = output13.file_qury_no;

                Output_9102 output9102 = new Output_9102();
                //string jsonOutput = ClassHelper.SaveToInterface1(input9102, out output9102, post1, out code);
                string jybh = cbe_Xznr1.Text.Substring(0, 4);
                string jsonOutput = ClassHelper.Download_9102(input9102, post1, jybh, false);

                me_Ybfh1.Text = jsonOutput;
                if (code != "0")
                {
                    return;
                }
                OutputDownload13 download = new OutputDownload13();


                string filename = jsonOutput + @"\" + output13.filename.Replace(".zip", "");
                DataTable dtRreturn = new DataTable();
                if (jybh == "1301")
                {

                    dtRreturn = Functions.GetOutTxtString(filename, download.data1301);
                }
                else if (jybh == "1302")
                {
                    dtRreturn = Functions.GetOutTxtString(filename, download.data1302);
                }
                else if (jybh == "1302")
                {
                    dtRreturn = Functions.GetOutTxtString(filename, download.data1302);
                }
                else if (jybh == "1303")
                {
                    //download.data1303 = new DataTable();
                    dtRreturn = Functions.GetOutTxtString(filename, download.data1303);
                }
                else if (jybh == "1305")
                {
                    dtRreturn = Functions.GetOutTxtString(filename, download.data1305);
                }
                else if (jybh == "1306")
                {
                    dtRreturn = Functions.GetOutTxtString(filename, download.data1306);
                }
                else if (jybh == "1307")
                {
                    dtRreturn = Functions.GetOutTxtString(filename, download.data1307);
                }
                else if (jybh == "1308")
                {
                    dtRreturn = Functions.GetOutTxtString(filename, download.data1308);
                }
                else if (jybh == "1309")
                {
                    dtRreturn = Functions.GetOutTxtString(filename, download.data1309);
                }
                else if (jybh == "1310")
                {
                    dtRreturn = Functions.GetOutTxtString(filename, download.data1310);
                }
                else if (jybh == "1311")
                {
                    dtRreturn = Functions.GetOutTxtString(filename, download.data1311);
                }
                else if (jybh == "1313")
                {
                    dtRreturn = Functions.GetOutTxtString(filename, download.data1313);
                }
                else if (jybh == "1314")
                {
                    dtRreturn = Functions.GetOutTxtString(filename, download.data1314);
                }
                else if (jybh == "1315")
                {
                    dtRreturn = Functions.GetOutTxtString(filename, download.data1315);
                }
                if (dtRreturn.Rows.Count > 0)
                {
                    bbh = Convert.ToString(dtRreturn.Rows[0]["版本名称"]);
                }


                if (dtRreturnAll == null)
                {
                    dtRreturnAll = dtRreturn.Clone();
                }



                //for (int j = 0; j < dtRreturn.Rows.Count; j++)
                //{
                //    DataRow dr = dtRreturnAll.NewRow();
                //    foreach (DataColumn dc in dtRreturnAll.Columns)
                //    {
                //        dr[dc.ColumnName] = dtRreturn.Rows[j][dc.ColumnName];
                //    }
                //    dtRreturnAll.Rows.Add(dr);
                //}
                dtRreturnAll.Merge(dtRreturn);

                i++;
            }
            gv_Ybfh1.Columns.Clear();

            gc_Ybfh1.DataSource = dtRreturnAll;
            gv_Ybfh1.BestFitColumns();


        }
        private void sb_Cz_Click(object sender, EventArgs e)
        {
            if (te_Jybh2.Text == "")
            {
                te_Jybh2.Focus();
                MessageBox.Show("请输入交易编号");
                return;
            }
            if (te_Rybh2.Text == "")
            {
                te_Rybh2.Focus();
                MessageBox.Show("请输入人员编号");
                return;
            }
            if (te_YfsfbwId2.Text == "")
            {
                te_YfsfbwId2.Focus();
                MessageBox.Show("请输入原发送方报文ID");
                return;
            }
            PostBase post = new PostBase();
            post.hisId = te_Hisid2.Text;
            post.tradiNumber = "2601";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = te_Cbd2.Text;

            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 0;

            Input_2601 input = new Input_2601();
            input.data = new data2601();
            input.data.oinfno = te_Jybh2.Text;
            input.data.psn_no = te_Rybh2.Text;
            input.data.omsgid = te_YfsfbwId2.Text;
            Output_2601 output = new Output_2601();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")
            {
                MessageBox.Show("冲正成功！");
            }
            me_Ybfh2.Text = json;
        }

        private void sb_Output1_Click(object sender, EventArgs e)
        {
            if (gv_Ybfh1.RowCount > 0)
            {
                Functions.ExportGridViewToExcelNew(this, gv_Ybfh1, cbe_Xznr1.Text);
            }
        }
        /// <summary>
        /// 对账查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sb_Find3_Click(object sender, EventArgs e)
        {

            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "3201";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;

            Post_3201 input2301 = new Post_3201();
            input2301.insuplc_admdvs = "";
            input2301.insutype = cbe_Xzlx3.Text.Substring(0, 3);
            input2301.clr_type = Cbe_Qslb3.Text.Substring(0, 2);
            input2301.operatorId = czydm;
            input2301.operatorName = czyxm;
            input2301.setl_optins = ConfigurationManager.AppSettings["fixmedins_code"];
            input2301.stmt_begndate = de_Qrq3.DateTime;
            input2301.stmt_enddate = de_Zrq3.DateTime;

            DataTable dtMain = ClassSqlHelper.QueryReconciliation(input2301, 2); //1汇总  0 明细 2汇总查询
            gc_Ybxx3.DataSource = dtMain;
            gv_Ybxx3.BestFitColumns();

            //Input_3201 input = new Input_3201();
            //input.data = new data3201();

            //List<data3201> list = Functions.ToList<data3201>(dtMain);
            //input.data = list[0];

            //Output_3201 output = new Output_3201();
            //output.stmtinfo = new stmtinfo();
            //string code = "1";
            //string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            //return json;
        }

        private void sb_Dz3_Click(object sender, EventArgs e)
        {
            if (gv_Ybxx3.RowCount < 1)
            {
                MessageBox.Show("请先进行查询");
                return;
            }
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "3201";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;

            Post_3201 input2301 = new Post_3201();
            input2301.insuplc_admdvs = "";
            input2301.insutype = cbe_Xzlx3.Text.Substring(0, 3);
            input2301.clr_type = Cbe_Qslb3.Text.Substring(0, 2);
            input2301.operatorId = czydm;
            input2301.operatorName = czyxm;
            input2301.setl_optins = ConfigurationManager.AppSettings["setl_optins"];
            input2301.stmt_begndate = de_Qrq3.DateTime;
            input2301.stmt_enddate = de_Zrq3.DateTime;

            DataTable dtMain = ClassSqlHelper.QueryReconciliation(input2301, 1);


            Input_3201 input = new Input_3201();
            input.data = new data3201();

            List<data3201> list = Functions.ToList<data3201>(dtMain);
            input.data = list[0];
            input.data.stmt_begndate = de_Qrq3.DateTime.ToString("yyyy-MM-dd");
            input.data.stmt_enddate= de_Zrq3.DateTime.ToString("yyyy-MM-dd");
            Output_3201 output = new Output_3201();
            output.stmtinfo = new stmtinfo();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            me_ybfh3.Text = json;
            if (code == "0")
            {
                MessageBox.Show("对账成功!");
            }
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            DateTime rq = ClassSqlHelper.GetServerTime();
            DateTime qrq = Convert.ToDateTime(ClassSqlHelper.GetServerTime().ToString("yyyy-MM-dd") + " 00:00:01");
            DateTime zrq = Convert.ToDateTime(ClassSqlHelper.GetServerTime().ToString("yyyy-MM-dd") + " 23:59:59");
            
            de_Qrq3.DateTime = qrq;
            de_Zrq3.DateTime = zrq;
            de_Qrq4.DateTime = qrq;
            de_Zrq4.DateTime = zrq;
            de_Gxrq6.DateTime = zrq.AddDays(-180);
            te_Jgmc5.Text = ConfigurationManager.AppSettings["fixmedins_name"];
            de_Qrq7.DateTime = zrq;
            de_Zrq7.DateTime = zrq;
            de_Zyrq8.DateTime = zrq;
            de_Ksrq8.DateTime= zrq;
            de_Jsrq8.DateTime = zrq;
            de_Zyrq8.DateTime= zrq;
            de_Zrqrq8.DateTime = qrq;
            de_Zrzrq8.DateTime = zrq;
            te_Bbh1.Text = "0";
            rg_Yxlb6.SelectedIndex = 1;
            rg_Yxbz5.SelectedIndex = 1;
            de_manu_date9.DateTime = qrq;
            de_expy_end9.DateTime = qrq;
            de_purc_retn_stointime9.DateTime =rq;
            de_hosp_ide_date10.DateTime = rq;
            de_begndate10.DateTime = qrq;
            de_begndate10.DateTime = zrq;
            de_enddate10.DateTime = zrq;

            de_Blqrq10.DateTime = qrq;
            de_Blzrq10.DateTime = zrq;
            te_fixmedins_hilist_name9.Text= ConfigurationManager.AppSettings["fixmedins_name"];
            te_fixmedins_hilist_id9.Text= ConfigurationManager.AppSettings["fixmedins_code"]; 
            te_fixmedins_bchno9.Text = ConfigurationManager.AppSettings["setl_optins"];

            te_ide_fixmedins_no10.Text = ConfigurationManager.AppSettings["fixmedins_code"];
            te_ide_fixmedins_name10.Text = ConfigurationManager.AppSettings["fixmedins_name"];
            te_insu_optins10.Text = ConfigurationManager.AppSettings["setl_optins"];

            te_fixmedins_code11.Text = te_ide_fixmedins_no10.Text;
            te_fixmedins_name11.Text = te_ide_fixmedins_name10.Text;

            de_qrq11.DateTime = qrq;
            de_jsrq11.DateTime = zrq;

            te_diag_dr_codg10.Text = czydm;
            te_diag_dr_name10.Text = czyxm;

            de_Ddqrq11.DateTime= qrq;
            de_Ddzrq11.DateTime = zrq;
            glue_opsp_dise10.Properties.DataSource = ClassSqlHelper.QueryDiseasest();


        }

        private void de_Qrq3_EditValueChanged(object sender, EventArgs e)
        {
            gc_Ybxx3.DataSource = null;
        }

        private void cbe_Xzlx3_SelectedIndexChanged(object sender, EventArgs e)
        {
            gc_Ybxx3.DataSource = null;
        }

        private void Cbe_Qslb3_SelectedIndexChanged(object sender, EventArgs e)
        {
            gc_Ybxx3.DataSource = null;
        }

        private void de_Zrq3_EditValueChanged(object sender, EventArgs e)
        {
            gc_Ybxx3.DataSource = null;
        }


        private void sb_Find4_Click(object sender, EventArgs e)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "3202";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 1;

            //上传对账明细
            Post_9101 post9101 = new Post_9101();
            post9101.operatorId = czydm;
            post9101.operatorName = czyxm;
            post9101.insuplc_admdvs = "";
            post9101.fixmedins_code = ConfigurationManager.AppSettings["fixmedins_code"];// input3202.clr_type;
            post9101.fileName = "scmxdz"; //input3202.fileName;


            Post_3201 input2301 = new Post_3201();
            input2301.insuplc_admdvs = "";
            input2301.insutype = cbe_Xzlx4.Text.Substring(0, 3);
            input2301.clr_type = cbe_Qslb4.Text.Substring(0, 2);
            input2301.operatorId = czydm;
            input2301.operatorName = czyxm;
            input2301.setl_optins = ConfigurationManager.AppSettings["fixmedins_code"];
            input2301.stmt_begndate = de_Qrq3.DateTime;
            input2301.stmt_enddate = de_Zrq3.DateTime;

            post9101.dtIn = ClassSqlHelper.QueryReconciliation(input2301, 0);
            gc_Scyb4.DataSource = post9101.dtIn;
            gv_Scyb4.BestFitColumns();


        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="input9101"></param>
        /// <returns></returns>
        public string FsUploadIn_9101(Post_9101 input9101, out string code)
        {
            //签到
            //GetSigin(input9101.operatorId, input9101.operatorName);
            code = "1";
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "9101";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = input9101.insuplc_admdvs;
            post.operatorId = input9101.operatorId;
            post.operatorName = input9101.operatorName;
            post.inModel = 0;
            string txtpackage = System.Environment.CurrentDirectory + @"\" + input9101.fileName;



            if (!Directory.Exists(txtpackage))
            {
                Directory.CreateDirectory(txtpackage);
            }
            string txt = txtpackage + @"\" + input9101.fileName;
            if (File.Exists(txt))
            {
                File.Delete(txt);
            }

            if (File.Exists(txtpackage + ".zip"))
            {
                File.Delete(txtpackage + ".zip");
            }
            Functions.WriteTXT(txt + ".txt", input9101.dtIn);

            System.IO.Compression.ZipFile.CreateFromDirectory(txtpackage, txtpackage + ".zip"); //压缩
            //System.IO.Directory.Delete(txtpackage);
            byte[] buffer = File.ReadAllBytes(txtpackage + ".zip");
            //System.IO.Compression.ZipFile.ExtractToDirectory(@"e:\test\test.zip", @"e:\test"); //解压
            Input_9101 input = new Input_9101();

            input.fsUploadIn = new fsUploadIn();
            input.fsUploadIn.filename = input9101.fileName;
            input.fsUploadIn.fixmedins_code = input9101.fixmedins_code;
            input.fsUploadIn.@in = Convert.ToBase64String(buffer);

            Output_9101 output = new Output_9101();

            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            me_Ybfh4.Text = json;
            return json;
        }
        private void sb_Dz_Click(object sender, EventArgs e)
        {
            if (gv_Scyb4.RowCount < 1)
            {
                MessageBox.Show("请先进行查询");
                return;
            }

            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "3202";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 1;

            //上传对账明细
            Post_9101 post9101 = new Post_9101();
            post9101.operatorId = czydm;
            post9101.operatorName = czyxm;
            post9101.insuplc_admdvs = "";
            post9101.fixmedins_code = ConfigurationManager.AppSettings["setl_optins"];// input3202.clr_type;
            post9101.fileName = "scmxdz"; //input3202.fileName;

            Post_3201 input2301 = new Post_3201();
            input2301.insuplc_admdvs = "";
            input2301.insutype = cbe_Xzlx4.Text.Substring(0, 3);
            input2301.clr_type = cbe_Qslb4.Text.Substring(0, 2);
            input2301.operatorId = czydm;
            input2301.operatorName = czyxm;
            input2301.setl_optins = ConfigurationManager.AppSettings["setl_optins"];
            input2301.stmt_begndate = de_Qrq3.DateTime;
            input2301.stmt_enddate = de_Zrq3.DateTime;

            post9101.dtIn = ClassSqlHelper.QueryReconciliation(input2301, 0);
            gc_Scyb4.DataSource = post9101.dtIn;
            gv_Scyb4.BestFitColumns();



            string file = FsUploadIn_9101(post9101, out string code);
            if (code != "0")
            {
                MessageBox.Show("上传医保明细失败");
                return;
            }
            Output_9101 OutModlem = new Output_9101();//JsonConvert.DeserializeObject
            OutModlem = JsonConvert.DeserializeObject<Output_9101>(Convert.ToString(file));
            /*
            /@insutype varchar(30),--1  insutype 险种  字符型  6  Y Y
            //@clr_type varchar(30),--2  clr_type 清算类别  字符型  6  Y Y
            //@setl_optins varchar(40),--3  setl_optins 结算经办机构  字符型  6  Y
            //@stmt_begndate datetime,--4  stmt_begndate 对账开始日期  日期型 Y
            //@stmt_enddate datetime--5  stmt_enddate 对账结束日期  日期型 Y
            // @lb int ,--1汇总  0 明细
             */

            Post_3201 input3202 = new Post_3201();
            input3202.insutype = cbe_Xzlx4.Text.Substring(0, 3);
            input3202.clr_type = cbe_Qslb4.Text.Substring(0, 2);
            input3202.setl_optins = ConfigurationManager.AppSettings["setl_optins"];
            input3202.stmt_begndate = de_Qrq4.DateTime;
            input3202.stmt_enddate = de_Zrq4.DateTime;

            Input_3202 input = new Input_3202();
            DataTable dtMain = ClassSqlHelper.QueryReconciliation(input3202, 1);
            input.data = new data3202();
            input.data.setl_optins = input3202.setl_optins;

            input.data.file_qury_no = OutModlem.file_qury_no;
            input.data.stmt_begndate = Convert.ToDateTime(dtMain.Rows[0]["stmt_begndate"]).ToString("yyyy-MM-dd");
            input.data.stmt_enddate = Convert.ToDateTime(dtMain.Rows[0]["stmt_enddate"]).ToString("yyyy-MM-dd");
            input.data.fund_pay_sumamt = Convert.ToDecimal(dtMain.Rows[0]["medfee_sumamt"]);
            input.data.fund_pay_sumamt = Convert.ToDecimal(dtMain.Rows[0]["fund_pay_sumamt"]);
            input.data.cash_payamt = Convert.ToDecimal(dtMain.Rows[0]["cash_payamt"]);
            input.data.fixmedins_setl_cnt = Convert.ToInt32(dtMain.Rows[0]["fixmedins_setl_cnt"]);
            Output_3202 output = new Output_3202();
            output.fileinfo = new fileinfo();
            code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            me_Ybfh4.Text = json;

            if (code != "0")
            {
                MessageBox.Show("对账失败！");
                return;
            }

            PostBase post1 = new PostBase();
            post1.hisId = "0";
            post1.tradiNumber = "9102";
            //post.sign_no = sign_no;
            post1.insuplc_admdvs = "";
            post1.inModel = 1;
            post1.operatorId = czydm;
            post1.operatorName = czyxm;

            Input_9102 input9102 = new Input_9102();
            input9102.fsDownloadIn = new fsUploadIn9102();
            input9102.fsDownloadIn.filename = post9101.fileName;
            input9102.fsDownloadIn.fixmedins_code = ConfigurationManager.AppSettings["fixmedins_code"];
            input9102.fsDownloadIn.file_qury_no = output.fileinfo.file_qury_no;

            Output_9102 output9102 = new Output_9102();
            code = "1";
            string json9102 = ClassHelper.SaveToInterface1(input, out output9102, post, out code);
            me_Ybfh4.Text = json9102;
            if (code != "0")
            {
                MessageBox.Show("下载明细失败！");
                return;
            }


            OutputDownload13 download = new OutputDownload13();

            string jybh = cbe_Xznr1.Text.Substring(0, 3);
            string filename = json9102 + @"\" + post9101.fileName.Replace(".zip", "");
            DataTable dtRreturn = Functions.GetOutTxtString(filename, download.data3202);
            gc_Ybfh4.DataSource = dtRreturn;
            gv_Ybfh4.BestFitColumns();



        }

        private void sb_Xgmm5_Click(object sender, EventArgs e)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "1163";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 0;

            Input_null input = new Input_null();
            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            me_Mmybxxfh5.Text = json;

        }

        private void te_jgdm_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void sb_Hq5_Click(object sender, EventArgs e)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "1201";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 0;

            Input_1201 input = new Input_1201();
            input.medinsinfo = new medinsinfo1201();
            input.medinsinfo.fixmedins_type = cbe_Jglx5.Text.Substring(0, 1);
            input.medinsinfo.fixmedins_name = te_Jgmc5.Text;
            input.medinsinfo.fixmedins_code = te_Jgdm5.Text.Trim();

            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            me_Ggbxxfh5.Text = json;
        }

        private void sb_Close1_Click(object sender, EventArgs e)
        {
            //Close();
            string bbh=te_Bbh1.Text;
            string jybh = cbe_Xznr1.Text.Substring(0, 4);
            string filename = me_Ybfh1.Text.Trim().Replace(".zip", "");//jsonOutput + @"\" + output13.filename.Replace(".zip", "");
            DataTable dtRreturn = new DataTable();
            OutputDownload13 download = new OutputDownload13();
            DataTable dtRreturnAll = null;
            if (jybh == "1301")
            {

                dtRreturn = Functions.GetOutTxtString(filename, download.data1301);
            }
            else if (jybh == "1302")
            {
                dtRreturn = Functions.GetOutTxtString(filename, download.data1302);
            }
            else if (jybh == "1302")
            {
                dtRreturn = Functions.GetOutTxtString(filename, download.data1302);
            }
            else if (jybh == "1303")
            {
                //download.data1303 = new DataTable();
                dtRreturn = Functions.GetOutTxtString(filename, download.data1303);
            }
            else if (jybh == "1305")
            {
                dtRreturn = Functions.GetOutTxtString(filename, download.data1305);
            }
            else if (jybh == "1306")
            {
                dtRreturn = Functions.GetOutTxtString(filename, download.data1306);
            }
            else if (jybh == "1307")
            {
                dtRreturn = Functions.GetOutTxtString(filename, download.data1307);
            }
            else if (jybh == "1308")
            {
                dtRreturn = Functions.GetOutTxtString(filename, download.data1308);
            }
            else if (jybh == "1309")
            {
                dtRreturn = Functions.GetOutTxtString(filename, download.data1309);
            }
            else if (jybh == "1310")
            {
                dtRreturn = Functions.GetOutTxtString(filename, download.data1310);
            }
            else if (jybh == "1311")
            {
                dtRreturn = Functions.GetOutTxtString(filename, download.data1311);
            }
            else if (jybh == "1313")
            {
                dtRreturn = Functions.GetOutTxtString(filename, download.data1313);
            }
            else if (jybh == "1314")
            {
                dtRreturn = Functions.GetOutTxtString(filename, download.data1314);
            }
            else if (jybh == "1315")
            {
                dtRreturn = Functions.GetOutTxtString(filename, download.data1315);
            }
            if (dtRreturn.Rows.Count > 0)
            {
                bbh = Convert.ToString(dtRreturn.Rows[0]["版本名称"]);
            }


            if (dtRreturnAll == null)
            {
                dtRreturnAll = dtRreturn.Clone();
            }

            gv_Ybfh1.Columns.Clear();

            gc_Ybfh1.DataSource = dtRreturn;
            gv_Ybfh1.BestFitColumns();
        }

        private void sb_Close2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool IsTrue6()
        {
            if (te_Bysjl6.Text == "")
            {
                MessageBox.Show("本页数据量不能为空");
                te_Bysjl6.Focus();
                return false;
            }

            if (te_Dqys6.Text == "")
            {
                MessageBox.Show("当前页数不能为空");
                te_Dqys6.Focus();
                return false;
            }

            return true;
        }
        /// <summary>
        /// 1316目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sb_Ylybmlppxxcx6_Click(object sender, EventArgs e)
        {
            if (!IsTrue6()) return;
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "1316";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 1;
            Input_1316 input1316 = new Input_1316();
            input1316.data = new data1316();
            input1316.data.begndate = "";
            input1316.data.hilist_code = "";
            input1316.data.insu_admdvs = "";
            input1316.data.list_type = "";
            input1316.data.medins_list_codg = ConfigurationManager.AppSettings["fixmedins_code"];
            input1316.data.page_num = te_Dqys6.Text.Trim();
            input1316.data.page_size = te_Bysjl6.Text.Trim();
            input1316.data.query_date = "";
            input1316.data.updt_time = de_Gxrq6.DateTime.ToString("yyyy-MM-dd");
            input1316.data.vali_flag = rg_Yxlb6.SelectedIndex.ToString();
            string code = "1";
            Output_1316 ouput1316 = new Output_1316();
            ouput1316.data = new List<dataOuput1316>();
            string json = ClassHelper.SaveToInterface1(input1316, out ouput1316, post, out code);
            me_Ggbxxfh5.Text = json;
            if (code != "0") return;

            gv_Ybfh6.Columns.Clear();
            gc_Ybfh6.DataSource = ouput1316.data;
            gv_Ybfh6.Columns["med_list_codg"].Caption = "医疗目录编码";
            gv_Ybfh6.Columns["hilist_code"].Caption = "医保目录编码";
            gv_Ybfh6.Columns["list_type"].Caption = "目录类别";
            gv_Ybfh6.Columns["insu_admdvs"].Caption = "参保机构医保区划";
            gv_Ybfh6.Columns["begndate"].Caption = "开始日期";
            //gv_Ybfh6.Columns["enddate"].Caption = "结束日期";
            gv_Ybfh6.Columns["memo"].Caption = "备注";
            gv_Ybfh6.Columns["vali_flag"].Caption = "有效标志";
            gv_Ybfh6.Columns["rid"].Caption = "唯一记录号";
            gv_Ybfh6.Columns["updt_time"].Caption = "更新时间";
            //gv_Ybfh6.Columns["crter_id"].Caption = "创建人";
            //gv_Ybfh6.Columns["crter_name"].Caption = "创建人姓名";
            //gv_Ybfh6.Columns["crte_time"].Caption = "创建时间";
            //gv_Ybfh6.Columns["crte_optins_no"].Caption = "创建机构";
            //gv_Ybfh6.Columns["opter_id"].Caption = "经办人";
            //gv_Ybfh6.Columns["opter_name"].Caption = "经办人姓名";
            gv_Ybfh6.Columns["opt_time"].Caption = "经办时间";
            //gv_Ybfh6.Columns["optins_no"].Caption = "经办机构";
            //gv_Ybfh6.Columns["poolarea_no"].Caption = "统筹区";
            gv_Ybfh6.BestFitColumns();

        }
        /// <summary>
        /// 1317目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sb_Yljgmlppxxcx6_Click(object sender, EventArgs e)
        {
            if (!IsTrue6()) return;
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "1317";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 1;
            Input_1317 input1317 = new Input_1317();
            input1317.data = new dataInput1317();
            input1317.data.begndate = "";
            input1317.data.fixmedins_code = ConfigurationManager.AppSettings["fixmedins_code"];
            input1317.data.insu_admdvs = "";
            input1317.data.list_type = "";
            input1317.data.medins_list_codg = "";// ConfigurationManager.AppSettings["fixmedins_code"];
            input1317.data.medins_list_name = te_Mlmc6.Text.Trim(); // ConfigurationManager.AppSettings["fixmedins_name"];
            input1317.data.page_num = te_Dqys6.Text.Trim();
            input1317.data.page_size = te_Bysjl6.Text.Trim();
            input1317.data.updt_time = de_Gxrq6.DateTime.ToString("yyyy-MM-dd");
            input1317.data.vali_flag = rg_Yxlb6.SelectedIndex.ToString();
            input1317.data.query_date = "";
            input1317.data.med_list_codg = "";


            string code = "1";
            Output_1317 ouput1317 = new Output_1317();
            ouput1317.data = new List<dataOut1317>(); ;
            string json = ClassHelper.SaveToInterface1(input1317, out ouput1317, post, out code);
            me_Ybfh6.Text = json;
            if (code != "0") return;

            gv_Ybfh6.Columns.Clear(); 
            gc_Ybfh6.DataSource = ouput1317.data;
            //gv_Ybfh6.Columns["fixmedins_code"].Caption = "定点医药机构编号";
            //gv_Ybfh6.Columns["medins_list_codg"].Caption = "定点医药机构目录编号";
            //gv_Ybfh6.Columns["medins_list_name"].Caption = "定点医药机构目录名称";
            gv_Ybfh6.Columns["insu_admdvs"].Caption = "参保机构医保区划";
            gv_Ybfh6.Columns["list_type"].Caption = "目录类别";
            gv_Ybfh6.Columns["med_list_codg"].Caption = "医疗目录编码";
            gv_Ybfh6.Columns["begndate"].Caption = "开始日期";
            //gv_Ybfh6.Columns["enddate"].Caption = "结束日期";
            //gv_Ybfh6.Columns["aprvno"].Caption = "批准文号";
            //gv_Ybfh6.Columns["dosform"].Caption = "剂型";
            //gv_Ybfh6.Columns["exct_cont"].Caption = "除外内容";
            //gv_Ybfh6.Columns["item_cont"].Caption = "项目内涵";
            //gv_Ybfh6.Columns["prcunt"].Caption = "计价单位";
            //gv_Ybfh6.Columns["spec"].Caption = "规格";
            //gv_Ybfh6.Columns["pacspec"].Caption = "包装规格";
            gv_Ybfh6.Columns["memo"].Caption = "备注";
            gv_Ybfh6.Columns["vali_flag"].Caption = "有效标志";
            gv_Ybfh6.Columns["rid"].Caption = "唯一记录号";
            gv_Ybfh6.Columns["updt_time"].Caption = "更新时间";
            gv_Ybfh6.Columns["opt_time"].Caption = "经办时间";
            gv_Ybfh6.BestFitColumns();
        }
        /// <summary>
        /// 1318目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sb_YBmlxjxxcx6_Click(object sender, EventArgs e)
        {
            if (!IsTrue6()) return;
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "1318";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 1;
            Input_1318 input1318 = new Input_1318();
            input1318.data = new dataInput1318();
            input1318.data.query_date = "";
            input1318.data.hilist_code = "";

            input1318.data.overlmt_dspo_way = "";
            input1318.data.insu_admdvs = "";
            input1318.data.begndate = "";
            input1318.data.enddate = "";
            input1318.data.vali_flag = rg_Yxlb6.SelectedIndex.ToString();
            input1318.data.rid = "";
            input1318.data.tabname = "";
            input1318.data.poolarea_no = "";
            input1318.data.page_num = te_Dqys6.Text.Trim();
            input1318.data.page_size = te_Bysjl6.Text.Trim();
            input1318.data.updt_time = de_Gxrq6.DateTime.ToString("yyyy-MM-dd");



            string code = "1";
            Output_1318 ouput1318 = new Output_1318();
            ouput1318.data = new List<dataOuput1318>(); ;
            string json = ClassHelper.SaveToInterface1(input1318, out ouput1318, post, out code);
            me_Ybfh6.Text = json;
            if (code != "0") return;

            gv_Ybfh6.Columns.Clear();
            gc_Ybfh6.DataSource = ouput1318.data;
            //for (int i = 0; i < gv_Ybfh6.Columns.Count; i++)
            //{
                gv_Ybfh6.Columns["hilist_code"].Caption = "医保目录编码";
                gv_Ybfh6.Columns["hilist_lmtpric_type"].Caption = "医保目录限价类型";
                gv_Ybfh6.Columns["overlmt_dspo_way"].Caption = "医保目录超限处理方式";
                gv_Ybfh6.Columns["insu_admdvs"].Caption = "参保机构医保区划";
                gv_Ybfh6.Columns["begndate"].Caption = "开始日期";
                gv_Ybfh6.Columns["enddate"].Caption = "结束日期";
                gv_Ybfh6.Columns["vali_flag"].Caption = "有效标志";
                gv_Ybfh6.Columns["rid"].Caption = "唯一记录号";
                gv_Ybfh6.Columns["updt_time"].Caption = "更新时间";
                gv_Ybfh6.Columns["crter_id"].Caption = "创建人";
                gv_Ybfh6.Columns["crter_name"].Caption = "创建人姓名";
                gv_Ybfh6.Columns["crte_time"].Caption = "创建时间";
                gv_Ybfh6.Columns["crte_optins_no"].Caption = "创建机构";
                gv_Ybfh6.Columns["opter_id"].Caption = "经办人";
            gv_Ybfh6.Columns["opter_name"].Caption = "经办人姓名";
            gv_Ybfh6.Columns["opt_time"].Caption = "经办时间";
            gv_Ybfh6.Columns["optins_no"].Caption = "经办机构";
            gv_Ybfh6.Columns["tabname"].Caption = "表名";
            gv_Ybfh6.Columns["poolarea_no"].Caption = "统筹区";
            gv_Ybfh6.Columns["hilist_pric_uplmt_amt"].Caption = "医保目录定价上限金额";
            //}
            gv_Ybfh6.BestFitColumns();
        }
        /// <summary>
        /// 1319目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sb_Ybmlxzfblxxcx6_Click(object sender, EventArgs e)
        {
            if (!IsTrue6()) return;
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "1319";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 1;
            Input_1319 input1319 = new Input_1319();
            input1319.data = new dataInput1319();

            input1319.data.query_date = "";
            input1319.data.hilist_code = "";
            input1319.data.selfpay_prop_psn_type = "";
            input1319.data.selfpay_prop_type = "";

            input1319.data.insu_admdvs = "";
            input1319.data.begndate = "";
            input1319.data.enddate = "";
            input1319.data.vali_flag = rg_Yxlb6.SelectedIndex.ToString();
            input1319.data.rid = "";
            input1319.data.tabname = "";
            input1319.data.poolarea_no = "";
            input1319.data.page_num = te_Dqys6.Text.Trim();
            input1319.data.page_size = te_Bysjl6.Text.Trim();
            input1319.data.updt_time = de_Gxrq6.DateTime.ToString("yyyy-MM-dd");



            string code = "1";
            Output_1319 ouput1319 = new Output_1319();
            ouput1319.data = new List<dataOuput1319>(); ;
            string json = ClassHelper.SaveToInterface1(input1319, out ouput1319, post, out code);
            me_Ybfh6.Text = json;
            if (code != "0") return;

            gv_Ybfh6.Columns.Clear();
            gc_Ybfh6.DataSource = ouput1319.data;
            gv_Ybfh6.Columns["hilist_code"].Caption = "医保目录编码";
            gv_Ybfh6.Columns["selfpay_prop_psn_type"].Caption = "医保目录自付比例人员类别";
            gv_Ybfh6.Columns["selfpay_prop_type"].Caption = "目录自付比例类别";
            gv_Ybfh6.Columns["insu_admdvs"].Caption = "参保机构医保区划";
            gv_Ybfh6.Columns["begndate"].Caption = "开始日期";
            gv_Ybfh6.Columns["enddate"].Caption = "结束日期";
            gv_Ybfh6.Columns["selfpay_prop"].Caption = "自付比例";
            gv_Ybfh6.Columns["vali_flag"].Caption = "有效标志";
            gv_Ybfh6.Columns["rid"].Caption = "唯一记录号";
            gv_Ybfh6.Columns["updt_time"].Caption = "更新时间";
            gv_Ybfh6.Columns["crter_id"].Caption = "创建人";
            gv_Ybfh6.Columns["crter_name"].Caption = "创建人姓名";
            gv_Ybfh6.Columns["crte_time"].Caption = "创建时间";
            gv_Ybfh6.Columns["crte_optins_no"].Caption = "创建机构";
            gv_Ybfh6.Columns["opter_id"].Caption = "经办人";
            gv_Ybfh6.Columns["opter_name"].Caption = "经办人姓名";
            gv_Ybfh6.Columns["opt_time"].Caption = "经办时间";
            gv_Ybfh6.Columns["optins_no"].Caption = "经办机构";
            gv_Ybfh6.Columns["tabname"].Caption = "表名";
            gv_Ybfh6.Columns["poolarea_no"].Caption = "统筹区";
            gv_Ybfh6.BestFitColumns();
        }
        /// <summary>
        /// 1312目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sb_Ybmlxxcx6_Click(object sender, EventArgs e)
        {
            if (!IsTrue6()) return;
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "1312";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 1;
            Input_1312 input1312 = new Input_1312();
            input1312.data = new dataInput1312();

            input1312.data.query_date = "";
            input1312.data.hilist_code = "";
            input1312.data.insu_admdvs = "";
            input1312.data.begndate = "";
            input1312.data.hilist_name = te_Mlmc6.Text.Trim();
            input1312.data.wubi = "";
            input1312.data.pinyin = "";
            input1312.data.med_chrgitm_type = "";
            input1312.data.chrgitm_lv = "";
            input1312.data.lmt_used_flag = "";
            input1312.data.list_type = "";
            input1312.data.med_use_flag = "";
            input1312.data.hilist_use_type = "";
            input1312.data.lmt_cpnd_type = "";

            input1312.data.insu_admdvs = "";
            input1312.data.begndate = "";

            input1312.data.vali_flag = rg_Yxlb6.SelectedIndex.ToString();
            input1312.data.page_num = te_Dqys6.Text.Trim();
            input1312.data.page_size = te_Bysjl6.Text.Trim();
            input1312.data.updt_time = de_Gxrq6.DateTime.ToString("yyyy-MM-dd");



            string code = "1";
            Output_1312 ouput1319 = new Output_1312();
            ouput1319.data = new List<dataOutput1312>(); ;
            string json = ClassHelper.SaveToInterface1(input1312, out ouput1319, post, out code);
            me_Ybfh6.Text = json;
            if (code != "0") return;

            gv_Ybfh6.Columns.Clear();
            gc_Ybfh6.DataSource = ouput1319.data;
            gv_Ybfh6.Columns["hilist_code"].Caption = "医保目录编码";
            gv_Ybfh6.Columns["hilist_name"].Caption = "医保目录名称";
            gv_Ybfh6.Columns["insu_admdvs"].Caption = "参保机构医保区划";
            gv_Ybfh6.Columns["begndate"].Caption = "开始日期";
            gv_Ybfh6.Columns["enddate"].Caption = "结束日期";
            gv_Ybfh6.Columns["med_chrgitm_type"].Caption = "医疗收费项目类别";
            gv_Ybfh6.Columns["chrgitm_lv"].Caption = "收费项目等级";
            gv_Ybfh6.Columns["lmt_used_flag"].Caption = "限制使用标志";
            gv_Ybfh6.Columns["list_type"].Caption = "目录类别";
            gv_Ybfh6.Columns["med_use_flag"].Caption = "医疗使用标志";
            gv_Ybfh6.Columns["matn_used_flag"].Caption = "生育使用标志";
            gv_Ybfh6.Columns["hilist_use_type"].Caption = "医保目录使用类别";
            gv_Ybfh6.Columns["lmt_cpnd_type"].Caption = "限复方使用类型";
            gv_Ybfh6.Columns["wubi"].Caption = "五笔助记码";
            gv_Ybfh6.Columns["memo"].Caption = "备注";
            gv_Ybfh6.Columns["vali_flag"].Caption = "有效标志";
            gv_Ybfh6.Columns["rid"].Caption = "唯一记录号";
            gv_Ybfh6.Columns["updt_time"].Caption = "更新时间";
            gv_Ybfh6.Columns["crter_id"].Caption = "创建人";
            gv_Ybfh6.Columns["crter_name"].Caption = "创建人姓名";
            gv_Ybfh6.Columns["crte_time"].Caption = "创建时间";
            gv_Ybfh6.Columns["crte_optins_no"].Caption = "创建机构";
            gv_Ybfh6.Columns["opter_id"].Caption = "经办人";
            gv_Ybfh6.Columns["opter_name"].Caption = "经办人姓名";
            gv_Ybfh6.Columns["opt_time"].Caption = "经办时间";
            gv_Ybfh6.Columns["optins_no"].Caption = "经办机构";
            gv_Ybfh6.Columns["poolarea_no"].Caption = "统筹区";
            gv_Ybfh6.BestFitColumns();
        }
        /// <summary>
        /// 1304民族药品目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sb_Mzyymlcx6_Click(object sender, EventArgs e)
        {
            if (!IsTrue6()) return;
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "1304";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 1;
            Input_1304 Input1304 = new Input_1304();
            Input1304.data = new dataInput1314();

            Input1304.data.med_list_codg = "";
            Input1304.data.genname_codg = "";
            Input1304.data.drug_genname = "";
            Input1304.data.drug_prodname = te_Mlmc6.Text.Trim();

            Input1304.data.reg_name = "";
            Input1304.data.tcmherb_name = "";
            Input1304.data.mlms_name = "";

            Input1304.data.rid = "";
            Input1304.data.ver = "";
            Input1304.data.ver_name = "";
            Input1304.data.opt_begn_time = "";
            Input1304.data.opt_end_time = "";
            Input1304.data.opt_end_time = "";

            Input1304.data.vali_flag = rg_Yxlb6.SelectedIndex.ToString();
            Input1304.data.page_num = te_Dqys6.Text.Trim();
            Input1304.data.page_size = te_Bysjl6.Text.Trim();
            Input1304.data.updt_time = de_Gxrq6.DateTime.ToString("yyyy-MM-dd");



            string code = "1";
            Output_1304 ouput1304 = new Output_1304();
            ouput1304.data = new List<dataOuput1304>(); ;
            string json = ClassHelper.SaveToInterface1(Input1304, out ouput1304, post, out code);
            me_Ybfh6.Text = json;
            if (code != "0") return;

            gv_Ybfh6.Columns.Clear();
            gc_Ybfh6.DataSource = ouput1304.data;
            gv_Ybfh6.Columns["med_list_codg"].Caption = "医疗目录编码";
            gv_Ybfh6.Columns["drug_prodname"].Caption = "药品商品名";
            gv_Ybfh6.Columns["genname_codg"].Caption = "通用名编号";
            gv_Ybfh6.Columns["drug_genname"].Caption = "药品通用名";
            gv_Ybfh6.Columns["ethdrug_type"].Caption = "民族药种类";
            gv_Ybfh6.Columns["chemname"].Caption = "化学名称";
            gv_Ybfh6.Columns["alis"].Caption = "别名";
            gv_Ybfh6.Columns["eng_name"].Caption = "英文名称";
            gv_Ybfh6.Columns["dosform"].Caption = "剂型";
            gv_Ybfh6.Columns["each_dos"].Caption = "每次用量";
            gv_Ybfh6.Columns["used_frqu"].Caption = "使用频次";
            gv_Ybfh6.Columns["nat_drug_no"].Caption = "国家药品编号";
            gv_Ybfh6.Columns["used_mtd"].Caption = "用法";
            gv_Ybfh6.Columns["ing"].Caption = "成分";
            gv_Ybfh6.Columns["chrt"].Caption = "性状";
            gv_Ybfh6.Columns["defs"].Caption = "不良反应";
            gv_Ybfh6.Columns["tabo"].Caption = "禁忌";
            gv_Ybfh6.Columns["mnan"].Caption = "注意事项";
            gv_Ybfh6.Columns["stog"].Caption = "贮藏";
            gv_Ybfh6.Columns["drug_spec"].Caption = "药品规格";
            gv_Ybfh6.Columns["prcunt_type"].Caption = "计价单位类型";
            gv_Ybfh6.Columns["otc_flag"].Caption = "非处方药标志";
            gv_Ybfh6.Columns["pacmatl"].Caption = "包装材质";
            gv_Ybfh6.Columns["pacspec"].Caption = "包装规格";
            gv_Ybfh6.Columns["min_useunt"].Caption = "最小使用单位";
            gv_Ybfh6.Columns["min_salunt"].Caption = "最小销售单位";
            gv_Ybfh6.Columns["manl"].Caption = "说明书";
            gv_Ybfh6.Columns["rute"].Caption = "给药途径";
            gv_Ybfh6.Columns["begndate"].Caption = "开始日期";
            gv_Ybfh6.Columns["enddate"].Caption = "结束日期";
            gv_Ybfh6.Columns["pham_type"].Caption = "药理分类";
            gv_Ybfh6.Columns["memo"].Caption = "备注";
            gv_Ybfh6.Columns["pac_cnt"].Caption = "包装数量";
            gv_Ybfh6.Columns["min_unt"].Caption = "最小计量单位";
            gv_Ybfh6.Columns["min_pac_cnt"].Caption = "最小包装数量";
            gv_Ybfh6.Columns["min_pacunt"].Caption = "最小包装单位";
            gv_Ybfh6.Columns["min_prepunt"].Caption = "最小制剂单位";
            gv_Ybfh6.Columns["drug_expy"].Caption = "药品有效期";
            gv_Ybfh6.Columns["efcc_atd"].Caption = "功能主治";
            gv_Ybfh6.Columns["vali_flag"].Caption = "有效标志";
            gv_Ybfh6.Columns["crte_time"].Caption = "数据创建时间";
            gv_Ybfh6.Columns["updt_time"].Caption = "数据更新时间";
            gv_Ybfh6.Columns["crter_id"].Caption = "创建人";
            gv_Ybfh6.Columns["crter_name"].Caption = "创建人姓名";
            gv_Ybfh6.Columns["opter_id"].Caption = "经办人";
            gv_Ybfh6.Columns["opter_name"].Caption = "经办人姓名";
            gv_Ybfh6.Columns["ver"].Caption = "版本号";
            gv_Ybfh6.BestFitColumns();
        }

        private void sb_Xzxx5_Click(object sender, EventArgs e)
        {
            if (te_Zdlx5.Text == "")
            {
                MessageBox.Show("类型不能为空");
                te_Zdlx5.Focus();
                return;
            }

            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "1901";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 1;

            Input_1901 input = new Input_1901();
            input.data = new data1901();
            input.data.type = te_Zdlx5.Text.Trim();
            input.data.parentValue = "";
            input.data.admdvs = ConfigurationManager.AppSettings["mdtrtarea_admvs"]; ;
            input.data.date = DateTime.Now.ToString("yyyy-MM-dd");
            input.data.vali_Flag = rg_Yxbz5.SelectedIndex.ToString();

            string code = "1";
            Output_1901 output = new Output_1901();
            output.list = new List<list1901>();
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            me_Ybfh6.Text = json;
            if (code != "0") return;
            gc_Xzxx5.DataSource = output.list;

            gv_Xzxx5.BestFitColumns();
            me_Yfhxz5.Text = json;
        }

        private void sb_Output5_Click(object sender, EventArgs e)
        {
            if (gv_Xzxx5.RowCount > 0)
            {
                Functions.ExportGridViewToExcelNew(this, gv_Xzxx5, "目录字典明细");
            }
        }

        private void sb_outputHis4_Click(object sender, EventArgs e)
        {
            if (gv_Scyb4.RowCount > 0)
            {
                Functions.ExportGridViewToExcelNew(this, gv_Scyb4, "HIS结算明细");
            }
        }

        private void sb_outputYb4_Click(object sender, EventArgs e)
        {
            if (gv_Ybfh4.RowCount > 0)
            {
                Functions.ExportGridViewToExcelNew(this, gv_Ybfh4, "医保结算返回明细");
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            if (gv_Ybfh6.RowCount > 0)
            {
                Functions.ExportGridViewToExcelNew(this, gv_Ybfh6, "医保返回查询信息");
            }
        }

        private void sb_Cx7_Click(object sender, EventArgs e)
        {
            gc_Cxxx7.DataSource = ClassSqlHelper.QueryTheCode(rg_Lb7.SelectedIndex);
            gv_Cxxx7.BestFitColumns();
        }

        private void sb_Sc7_Click(object sender, EventArgs e)
        {
            if (rg_Lb7.SelectedIndex != 2)
            {
                MessageBox.Show("请先选择未上传的类别");
                return;
            }
            if (gv_Cxxx7.RowCount < 1)
            {
                MessageBox.Show("没有需要上传的请先进行查询");
                return;
            }
            if (gv_Cxxx7.RowCount > 10)
            {
                MessageBox.Show("上传时间较长100条数据需要5分钟，请耐心等待！");
                return;
            }

            if ((int)MessageBox.Show("确认需要上传吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != 1)
            {
                return;
            }

            DateTime getdate = ClassSqlHelper.GetServerTime();
            StringBuilder strEeor = new StringBuilder();

            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "3301";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 0;
            for (int i = 0; i < gv_Cxxx7.RowCount; i++)
            {
                Input_3301 input = new Input_3301();
                input.data = new data3301();
                input.data.fixmedins_hilist_id = Convert.ToString(gv_Cxxx7.GetDataRow(i)["目录编号"]);
                input.data.fixmedins_hilist_name = Convert.ToString(gv_Cxxx7.GetDataRow(i)["目录名称"]);
                input.data.list_type = Convert.ToString(gv_Cxxx7.GetDataRow(i)["目录类别"]);
                input.data.med_list_codg = Convert.ToString(gv_Cxxx7.GetDataRow(i)["医保代码"]);
                input.data.begndate = de_Qrq7.DateTime.ToString("yyyy-MM-dd");
                input.data.enddate = de_Zrq7.DateTime.ToString("yyyy-MM-dd");
                input.data.aprvno = Convert.ToString(gv_Cxxx7.GetDataRow(i)["批准文号"]);
                input.data.dosform = "";
                input.data.exct_cont = Convert.ToString(gv_Cxxx7.GetDataRow(i)["剂型"]);
                input.data.item_cont = "";
                input.data.prcunt = "";
                input.data.spec = Convert.ToString(gv_Cxxx7.GetDataRow(i)["规格"]); ;
                input.data.pacspec = "";
                input.data.memo = Convert.ToString(gv_Cxxx7.GetDataRow(i)["备注"]);

                Output_null output = new Output_null();
                string code = "1";
                string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
                if (code != "0")
                {
                    strEeor.Append(json);
                }
                else//更新本地信息
                {
                    int count = ClassSqlHelper.UpTheCode(Convert.ToString(gv_Cxxx7.GetDataRow(i)["目录编号"]), Convert.ToInt32(gv_Cxxx7.GetDataRow(i)["目录类别"]), getdate);

                    if (count < 0)
                    {
                        strEeor.Append(" 本地更新失败：" + Convert.ToString(gv_Cxxx7.GetDataRow(i)["目录编号"]));
                    }
                }
            }

            if (Convert.ToString(strEeor) == "")
            {
                MessageBox.Show("上传目录信息成功！");
                return;
            }
            me_Ybfh7.Text = Convert.ToString(strEeor);

            //1  fixmedins_hilist_id 定点医药机构目录编号字符型  30  Y 
            //2  fixmedins_hilist_name 定点医药机构目录名称字符型  200  Y 
            //3  list_type 目录类别  字符型  30  Y 
            //4  med_list_codg 医疗目录编码  字符型  30  
            //5  begndate 开始日期  日期型 Y 
            //6  enddate 结束日期  日期型 Y 
            //7  aprvno 批准文号  字符型  30  N 
            //8  dosform 剂型  字符型  200  N 
            //9  exct_cont 除外内容  字符型  2000  N 
            //10  item_cont 项目内涵  字符型  2,000  N 
            //11  prcunt 计价单位  字符型  100  N 
            //12  spec 规格  字符型  200  N 
            //13  pacspec 包装规格  字符型  100  N 
            //14  memo 备注  字符型  500  N
            //目录编号 目录名称    医保代码 国家编码    批准文号 剂型  规格 备注  目录类别 上传日期

        }

        private void rg_Lb7_SelectedIndexChanged(object sender, EventArgs e)
        {
            gc_Cxxx7.DataSource = null;
            gc_Cxxx7.DataSource = ClassSqlHelper.QueryTheCode(rg_Lb7.SelectedIndex);

        }

        private void sb_Cxsc7_Click(object sender, EventArgs e)
        {
            if (rg_Lb7.SelectedIndex != 1)
            {
                MessageBox.Show("请先选择已上传的类别");
                return;
            }
            if (gv_Cxxx7.RowCount < 1)
            {
                MessageBox.Show("没有需要撤销的信息请先进行查询");
                return;
            }
            if ((int)MessageBox.Show("确认需要撤销上传吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != 1)
            {
                return;
            }
            DateTime getdate = Convert.ToDateTime("1900-01-01");
            StringBuilder strEeor = new StringBuilder();
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "3302";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 0;
            string fixmedins_code = ConfigurationManager.AppSettings["fixmedins_code"];

            Input_3302 input = new Input_3302();
            input.data = new catalogcompin3302();
            input.data.fixmedins_hilist_id = Convert.ToString(gv_Cxxx7.GetFocusedRowCellValue("目录编号"));
            input.data.fixmedins_code = fixmedins_code;
            input.data.list_type = Convert.ToString(gv_Cxxx7.GetFocusedRowCellValue("目录类别"));
            input.data.med_list_codg = Convert.ToString(gv_Cxxx7.GetFocusedRowCellValue("医保代码"));

            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            if (code != "0")
            {
                strEeor.Append(json);
            }
            else//更新本地信息
            {
                int count = ClassSqlHelper.UpTheCode(Convert.ToString(gv_Cxxx7.GetFocusedRowCellValue("目录编号")), Convert.ToInt32(gv_Cxxx7.GetFocusedRowCellValue("目录类别")), getdate);

                if (count < 0)
                {
                    strEeor.Append(" 本地更新失败：" + Convert.ToString(gv_Cxxx7.GetFocusedRowCellValue("目录编号")));
                }
            }
            if (Convert.ToString(strEeor) == "")
            {
                MessageBox.Show("撤销上传目录信息成功！");
               
            }
            me_Ybfh7.Text = Convert.ToString(strEeor);

        }

        private void sb_Qbcx7_Click(object sender, EventArgs e)
        {
            if (rg_Lb7.SelectedIndex != 1)
            {
                MessageBox.Show("请先选择已上传的类别");
                return;
            }
            if (gv_Cxxx7.RowCount < 1)
            {
                MessageBox.Show("没有需要撤销的信息请先进行查询");
                return;
            }


            if ((int)MessageBox.Show("确认要全部撤销上传吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != 1)
            {
                return;
            }
            if (gv_Cxxx7.RowCount > 10)
            {
                MessageBox.Show("上传时间较长100条数据需要5分钟，请耐心等待！");
                return;
            }
            DateTime getdate = Convert.ToDateTime("1900-01-01");
            StringBuilder strEeor = new StringBuilder();

            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "3302";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 0;
            string fixmedins_code = ConfigurationManager.AppSettings["fixmedins_code"];
            for (int i = 0; i < gv_Cxxx7.RowCount; i++)
            {
                Input_3302 input = new Input_3302();
                input.data = new catalogcompin3302();
                input.data.fixmedins_hilist_id = Convert.ToString(gv_Cxxx7.GetDataRow(i)["目录编号"]);
                input.data.fixmedins_code = fixmedins_code;
                input.data.list_type = Convert.ToString(gv_Cxxx7.GetDataRow(i)["目录类别"]);
                input.data.med_list_codg = Convert.ToString(gv_Cxxx7.GetDataRow(i)["医保代码"]);

                Output_null output = new Output_null();
                string code = "1";
                string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
                if (code != "0")
                {
                    strEeor.Append(json);
                }
                else//更新本地信息
                {
                    int count = ClassSqlHelper.UpTheCode(Convert.ToString(gv_Cxxx7.GetDataRow(i)["目录编号"]), Convert.ToInt32(gv_Cxxx7.GetDataRow(i)["目录类别"]), getdate);

                    if (count < 0)
                    {
                        strEeor.Append(" 本地更新失败：" + Convert.ToString(gv_Cxxx7.GetDataRow(i)["目录编号"]));
                    }
                }
            }

            if (Convert.ToString(strEeor) == "")
            {
                MessageBox.Show("撤销上传目录信息成功！");
                return;
            }
            me_Ybfh7.Text = Convert.ToString(strEeor);
        }

        private void sb_Cx8_Click(object sender, EventArgs e)
        {
            if (te_Zyh8.Text == "")
            {
                MessageBox.Show("请输入正确的住院号！");
                return;
            }

            DataTable tbInfo = ClassSqlHelper.QueryHospitalInfo(te_Zyh8.Text.Trim());
            if (tbInfo.Rows.Count < 1)
            {
                MessageBox.Show("未查到此住院号的信息，请检查住院号是否正确！");
                return;
            }
            te_Rybh8.Text = Convert.ToString(tbInfo.Rows[0]["psn_no"]);
            te_Xzlx.Text = Convert.ToString(tbInfo.Rows[0]["insutype"]);
            te_Zdmc8.Text = Convert.ToString(tbInfo.Rows[0]["diag_name"]);
            te_Zddm8.Text = Convert.ToString(tbInfo.Rows[0]["diag_code"]);
            te_Lxdh8.Text = Convert.ToString(tbInfo.Rows[0]["tel"]);
            te_Lxdz8.Text = Convert.ToString(tbInfo.Rows[0]["addr"]);
            te_Jyddm8.Text = Convert.ToString(tbInfo.Rows[0]["insuplc_admdvs"]);

            // b.cbdbm insuplc_admdvs,b.grbh psn_no,b.xzlx insutype,phone tel,a.xian_dz addr,cbdbm insu_optins,c.icd10 diag_code,zdmc diag_name,'' dise_cond_dsc
            te_Zwyymc8.Focus();
        }

        private void sb_Hqyyxx8_Click(object sender, EventArgs e)
        {
            if (te_Zwyymc8.Text == "")
            {
                MessageBox.Show("请输入转院医院名称！");
                return;
            }

            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "1201";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 1;

            Input_1201 input = new Input_1201();
            input.medinsinfo = new medinsinfo1201();
            input.medinsinfo.fixmedins_type = "1";
            input.medinsinfo.fixmedins_name = te_Zwyymc8.Text.Trim();
            input.medinsinfo.fixmedins_code = te_Zwddyyjgbh.Text.Trim();

            Output_1201 output = new Output_1201();
            output.medinsinfo = new List<medinsinfoOutput1201>();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0" && output.medinsinfo.Count > 0)
            {
                te_Zwyymc8.Text = output.medinsinfo[0].fixmedins_name;
                te_Zwddyyjgbh.Text = output.medinsinfo[0].fixmedins_code;
            }
            else
            {
                MessageBox.Show("未找到此医院的基本信息！");
            }
            me_Ybfhxx8.Text = json;
        }

        private void sb_Zyba8_Click(object sender, EventArgs e)
        {
            if (te_Rybh8.Text == "")
            {
                MessageBox.Show("请先输入住院号然后进行查询！");
                return;
            }

            if (te_Zwddyyjgbh.Text == "")
            {
                MessageBox.Show("转院医院编码不能为空！");
                sb_Hqyyxx8.Focus();
                return;
            }

            if (te_Zwyymc8.Text == "")
            {
                MessageBox.Show("转院医院名称不能为空！");
                sb_Hqyyxx8.Focus();
                return;
            }


            if (te_Zdmc8.Text == "")
            {
                MessageBox.Show("诊断不能为空！");
                sb_Cx8.Focus();
                return;
            }
            if (te_Zyyy8.Text == "")
            {
                MessageBox.Show("转院原因不能为空！");
                te_Zyyy8.Focus();
                return;
            }
            if (me_Zyyj8.Text == "")
            {
                MessageBox.Show("转院意见不能为空！");
                te_Zyyy8.Focus();
                return;
            }
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "2501A";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 1;

            Input_2501 input = new Input_2501();
            input.refmedin = new refmedin();

            input.refmedin.addr = te_Lxdz8.Text.Trim();
            input.refmedin.begndate = de_Ksrq8.DateTime.ToString("yyyy-MM-dd");
            input.refmedin.diag_code = te_Zddm8.Text.Trim();
            input.refmedin.diag_name = te_Zdmc8.Text.Trim();
            input.refmedin.dise_cond_dscr = me_Jbbqmx8.Text;
            input.refmedin.enddate = de_Jsrq8.DateTime.ToString("yyyy-MM-dd");
            input.refmedin.hosp_agre_refl_flag = ce_Yytyzybz8.Checked == true ? "1" : "0";
            input.refmedin.insutype = te_Xzlx.Text.Trim();
            input.refmedin.insu_optins = ConfigurationManager.AppSettings["mdtrtarea_admvs"]; ;
            input.refmedin.mdtrtarea_admdvs = te_Jyddm8.Text;
            input.refmedin.psn_no = te_Rybh8.Text.Trim();
            input.refmedin.reflin_medins_name = te_Zwyymc8.Text.Trim();
            input.refmedin.reflin_medins_no = te_Zwddyyjgbh.Text.Trim();
            input.refmedin.refl_date = de_Zyrq8.DateTime.ToString("yyyy-MM-dd");
            input.refmedin.refl_opnn = me_Zyyj8.Text.Trim();
            input.refmedin.refl_rea = te_Zyyy8.Text.Trim();
            input.refmedin.refl_type = cb_Zylx8.Text.Substring(0, 1);
            input.refmedin.refl_used_flag = "1";
            input.refmedin.tel = te_Lxdh8.Text.Trim();

            Output_2501 output = new Output_2501();
            output.result = new result2501();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")
            {
                Drjk_zyba_input feedOutput = new Drjk_zyba_input();
                feedOutput = Functions.Mapping<Drjk_zyba_input, refmedin>(input.refmedin);
                feedOutput.zt = 1;
                feedOutput.zyh = te_Zyh8.Text.Trim();
                feedOutput.czrq = ClassSqlHelper.GetServerTime();
                feedOutput.czydm = post.operatorId;
                feedOutput.trt_dcla_detl_sn = output.result.trt_dcla_detl_sn;
                int count = ClassSqlHelper.ExecuteSql(feedOutput.ToAddSql());
                if (count < 0)
                {
                    MessageBox.Show("医保成功保存本地失败");
                    return;
                }
                MessageBox.Show("转院备案成功！");
            }
            me_Ybfhxx8.Text = json;
           

        }

        private void sb_Cxzy8_Click(object sender, EventArgs e)
        {
            gc_Zyxx8.DataSource = ClassSqlHelper.QueryTransfer(de_Zrqrq8.DateTime, de_Zrzrq8.DateTime);
            gv_Zyxx8.BestFitColumns();
        }

        private void sb_Cxbabl_Click(object sender, EventArgs e)
        {
            if (gv_Zyxx8.RowCount < 1)
            {
                MessageBox.Show("没有您撤销的转院信息，请重新查询！");
                return;
            }
            if ((int)MessageBox.Show("确认需要撤销吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != 1)
            {
                return;
            }
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "2502";
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 0;

            Input_2502 input = new Input_2502();
            input.data = new data2502();
            input.data.psn_no = Convert.ToString(gv_Zyxx8.GetFocusedRowCellValue("人员编号"));
            input.data.trt_dcla_detl_sn = Convert.ToString(gv_Zyxx8.GetFocusedRowCellValue("待遇申报明细流水号"));
            input.data.memo = de_Cxyy8.Text.Trim();
            string code = "1";

            Output_null output = new Output_null();
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")
            {

                int count = ClassSqlHelper.UpHospitalInfo(Convert.ToString(gv_Zyxx8.GetFocusedRowCellValue("人员编号")), Convert.ToString(gv_Zyxx8.GetFocusedRowCellValue("待遇申报明细流水号")), czydm);
                if (count < 0)
                {
                    MessageBox.Show("医保成功保存本地失败");
                    return;
                }

            }
            me_Ybfxxs8.Text = json;
            MessageBox.Show("撤销转院备案成功！");
           
        }

        private void sb_Output8_Click(object sender, EventArgs e)
        {
            if (gv_Zyxx8.RowCount > 0)
            {
                Functions.ExportGridViewToExcelNew(this, gv_Zyxx8, "转院备案信息");
            }
        }

        private void sb_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void sb_Close4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void sb_Close5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void sb_Close7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void sb_Closes8_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void sb_Close8_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            if (te_med_list_codg9.Text == "")
            {
                MessageBox.Show("医疗目录编码不能为空！");
                te_med_list_codg9.Focus();
                return;
            }

            else if (te_fixmedins_hilist_id9.Text == "")
            {
                MessageBox.Show("定点医药机构目录编号不能为空！");
                te_fixmedins_hilist_id9.Focus();
                return;
            }

            else if (te_fixmedins_hilist_name9.Text == "")
            {
                MessageBox.Show("定点医药机构目录名称不能为空！");
                te_fixmedins_hilist_name9.Focus();
                return;
            }


            else if (te_fixmedins_bchno9.Text == "")
            {
                MessageBox.Show("定点医药机构批次流水号不能为空！");
                te_fixmedins_bchno9.Focus();
                return;
            }
            else if (te_spler_name9.Text == "")
            {
                MessageBox.Show("供应商名称不能为空！");
                te_spler_name9.Focus();
                return;
            }
            else if (te_aprvno9.Text == "")
            {
                MessageBox.Show("批准文号不能为空！");
                te_aprvno9.Focus();
                return;
            }
            /*
          1  med_list_codg  医疗目录编码  字符型  50  Y
         2  fixmedins_hilist_id 定点医药机构目录编号字符型  30  Y
         3  fixmedins_hilist_name 定点医药机构目录名称字符型  200  Y
         4  dynt_no  随货单号  字符型  50 
         5  fixmedins_bchno 定点医药机构批次流水号字符型  30  Y
         6  spler_name  供应商名称  字符型  200  Y
         7  spler_pmtno  供应商许可证号  字符型  50 
         8  manu_lotnum  生产批号  字符型  30  Y
         9  prdr_name  生产厂家名称  字符型  200  Y
         10  aprvno  批准文号  字符型  100  Y
         11  manu_date  生产日期  日期型  Y yyyy-MM-dd
         12  expy_end  有效期止  日期型  Y yyyy-MM-dd
         13  finl_trns_pric  最终成交单价  数值型  16,6 
         14  purc_retn_cnt  采购/退货数量  数值型  16,4  Y
         15  purc_invo_codg  采购发票编码  字符型  50 
         16  purc_invo_no  采购发票号  字符型  50 
         17  rx_flag  处方药标志  字符型  3  Y
         18 purc_retn_stointime 采购/退货入库时间日期时间型Yyyyy-MM-ddHH:mm:ss
         19 purc_retn_opter_name 采购/退货经办人姓名字符型  50  Y
         20  prod_geay_flag  商品赠送标志  字符型  3  Y 0-否；1-是
         21  memo  备注  字符型  500
          */
            Input_3503 input = new Input_3503();
            input.purcinfo = new purcinfo3503();
            input.purcinfo.med_list_codg = te_med_list_codg9.Text.Trim();
            input.purcinfo.fixmedins_hilist_id = te_fixmedins_hilist_id9.Text.Trim();
            input.purcinfo.fixmedins_hilist_name = te_fixmedins_hilist_name9.Text.Trim();
            input.purcinfo.dynt_no = te_dynt_no9.Text.Trim();
            input.purcinfo.fixmedins_bchno = te_fixmedins_bchno9.Text.Trim();
            input.purcinfo.spler_name = te_spler_name9.Text.Trim();
            input.purcinfo.spler_pmtno = te_spler_pmtno9.Text.Trim();
            input.purcinfo.manu_lotnum = te_manu_lotnum9.Text.Trim();
            input.purcinfo.prodentpName = te_prdr_name9.Text.Trim();
            input.purcinfo.aprvno = te_aprvno9.Text.Trim();
            input.purcinfo.manu_date = de_manu_date9.Text.Trim();
            input.purcinfo.expy_end = de_expy_end9.Text.Trim();
            input.purcinfo.finl_trns_pric = te_finl_trns_pric9.Text.Trim();
            input.purcinfo.purc_retn_cnt = te_purc_retn_cnt9.Text.Trim();
            input.purcinfo.purc_invo_codg = te_purc_invo_codg9.Text.Trim();
            input.purcinfo.purc_invo_no = te_purc_invo_no9.Text.Trim();
            input.purcinfo.rx_flag = ce_rx_flag9.Checked == true ? "1" : "0";
            input.purcinfo.purc_retn_stointime = de_purc_retn_stointime9.Text.Trim();
            input.purcinfo.purc_retn_opter_name = te_purc_retn_opter_name9.Text.Trim();
            input.purcinfo.prod_geay_flag = ce_prod_geay_flag9.Checked == true ? "1" : "0";
            input.purcinfo.memo = te_med_list_codg9.Text.Trim();


            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "3503";
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 0;
            string code = "1";

            Output_null output = new Output_null();
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")
            {

                MessageBox.Show("商品采购成功！");

            }
            me_Ybffxx9.Text = json;

        }

        private void sb_Babc10_Click(object sender, EventArgs e)
        {
            if (te_psn_no10.Text == "")
            {
                MessageBox.Show("人员编码不能为空！");
                te_psn_no10.Focus();
                return;
            }

            else if (glue_opsp_dise10.EditValue == null)
            {
                MessageBox.Show("门慢门特病种不能为空！");
                te_fixmedins_hilist_id9.Focus();
                return;
            }

            else if (te_diag_dr_codg10.Text == "")
            {
                MessageBox.Show("诊断医师代码不能为空！");
                te_diag_dr_codg10.Focus();
                return;
            }


            else if (te_diag_dr_name10.Text == "")
            {
                MessageBox.Show("诊断医师名称不能为空！");
                te_diag_dr_name10.Focus();
                return;
            }
            else if (te_ide_fixmedins_name10.Text == "")
            {
                MessageBox.Show("定点医疗机构不能为空！");
                te_spler_name9.Focus();
                return;
            }
            /*
             1  psn_no  人员编号  字符型  30  Y
            2  insutype  险种类型  字符型  6  Y  Y
            3  opsp_dise_code  门慢门特病种目录代码  字符型  30  Y
            4  opsp_dise_name  门慢门特病种名称  字符型  300  Y
            5  tel  联系电话  字符型  50 
            6  addr  联系地址  字符型  200 
            7  insu_optins  参保机构医保区划  字符型  6  Y
            8  ide_fixmedins_no  鉴定定点医药机构编号  字符型  30  Y
            9  ide_fixmedins_name 鉴定定点医药机构名称  字符型  200  Y
            10  hosp_ide_date  医院鉴定日期  日期型  Y
            11  diag_dr_codg  诊断医师编码  字符型  30  Y
            12  diag_dr_name  诊断医师姓名  字符型  50  Y
            13  begndate  开始日期  日期型  Y
            15  enddate  结束日期  日期型 
             */
            Input_2503 input = new Input_2503();
            input.data = new data2503();
            input.data.psn_no = te_psn_no10.Text.Trim();
            input.data.insutype = cbe_insutype10.Text.Substring(0, 3);
            input.data.opsp_dise_code = glue_opsp_dise10.EditValue.ToString();
            input.data.opsp_dise_name = glue_opsp_dise10.Text;
            input.data.tel = te_tel10.Text.Trim();
            input.data.insu_optins = te_insu_optins10.Text.Trim();
            input.data.ide_fixmedins_no = te_ide_fixmedins_no10.Text.Trim();
            input.data.ide_fixmedins_name = te_ide_fixmedins_name10.Text.Trim();
            input.data.hosp_ide_date = de_hosp_ide_date10.DateTime.ToString("yyyy-MM-dd hh:mm:ss");
            input.data.diag_dr_codg = te_diag_dr_codg10.Text.Trim();
            input.data.diag_dr_name = te_diag_dr_name10.Text.Trim();
            input.data.begndate = de_begndate10.DateTime.ToString("yyyy-MM-dd");
            input.data.enddate=de_enddate10.DateTime.ToString("yyyy-MM-dd");
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "2503";
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 1;
            string code = "1";

            Output_2503 output = new Output_2503();
            output.result = new result2503();

            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")
            {
                Drjk_rymtbba_input drjk = new Drjk_rymtbba_input();

                drjk = Functions.Mapping<Drjk_rymtbba_input, data2503>(input.data);
                drjk.zt = 1;              
                drjk.czrq = ClassSqlHelper.GetServerTime();
                drjk.czydm = post.operatorId;
                drjk.trt_dcla_detl_sn = output.result.trt_dcla_detl_sn;
                int count = ClassSqlHelper.ExecuteSql(drjk.ToAddSql());
                if (count < 0)
                {
                    MessageBox.Show("医保成功保存本地失败");
                    
                }
                MessageBox.Show("人员慢特病备办理成功！");
            }
            me_Ybfhxx10.Text = json;
           
        }

        private void sb_Dk_Click(object sender, EventArgs e)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "1161";
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 0;
            Input_1161 input = new Input_1161();
            Output_1161 output = new Output_1161();
            string code = "1";
            me_Ybfhxx10.Text= ClassHelper.SaveToInterface1(input, out output, post, out code);

            
        }

        private void sb_close10_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void glue_opsp_dise10_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            FilterFieldInfo[] filterFields = GetSelection();
            Delegate method = new MethodInvoker(delegate
            {
                FilterManager.FilterGridLookup(glue_opsp_dise10, filterFields);
            });
            this.BeginInvoke(method);
        }
        public static FilterFieldInfo[] GetSelection()
        {
            FilterFieldInfo fieldDm = new FilterFieldInfo("mtbbzmldm", FilterStrType.Both);
            FilterFieldInfo fieldXm = new FilterFieldInfo("mtbbzdlmc", FilterStrType.Both);
            return new FilterFieldInfo[] { fieldDm, fieldXm };
        }

        private void sb_Cxbl_Click(object sender, EventArgs e)
        {
            if (gv_Bbxx10.RowCount < 1)
            {
                MessageBox.Show("没有您撤销的信息，请重新查询！");
                return;
            }
            if ((int)MessageBox.Show("确认需要撤销吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != 1)
            {
                return;
            }
            if (te_Cxyy10.Text == "")
            {
                MessageBox.Show("请必须输入撤销原因！");
                te_Cxyy10.Focus();
                return;
            }
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "2504";
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 0;

            Input_2504 input = new Input_2504();
            input.data = new data2504();
            input.data.memo = te_Cxyy10.Text.Trim();
            input.data.psn_no = Convert.ToString(gv_Bbxx10.GetFocusedRowCellValue("人员编号"));
            input.data.trt_dcla_detl_sn = Convert.ToString(gv_Bbxx10.GetFocusedRowCellValue("待遇申报明细流水号"));
            StringBuilder strEeor = new StringBuilder();
            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            me_Ybfhxxcx10.Text = json;
            if (code == "0")
            {

                int count = ClassSqlHelper.UpHospitalInfo(Convert.ToString(gv_Bbxx10.GetFocusedRowCellValue("人员编号")), Convert.ToString(gv_Bbxx10.GetFocusedRowCellValue("待遇申报明细流水号")), czydm);
                if (count < 0)
                {
                    MessageBox.Show("医保成功保存本地失败");
                    return;      
                }
                MessageBox.Show("撤销成功！");
            }   
        }

        private void sb_Cxbb10_Click(object sender, EventArgs e)
        {
            gc_Bbxx10.DataSource = ClassSqlHelper.QueryPatientDiseasest(de_Blqrq10.DateTime, de_Blzrq10.DateTime);
            gv_Bbxx10.BestFitColumns();
        }

        private void sb_Output10_Click(object sender, EventArgs e)
        {
            if (gv_Bbxx10.RowCount > 0)
            {
                Functions.ExportGridViewToExcelNew(this, gv_Bbxx10, cbe_Xznr1.Text);
            }
        }

        private void sb_Ddba11_Click(object sender, EventArgs e)
        {
            if (te_psn_no11.Text == "")
            {
                MessageBox.Show("人员编码不能为空！");
                te_psn_no10.Focus();
                return;
            }


            else if (te_fix_srt_no11.Text == "")
            {
                MessageBox.Show("定点排序号不能为空！");
                te_diag_dr_name10.Focus();
                return;
            }

            /*
             1 psn_no  人员编号  字符型  30  Y
2 tel  联系电话  字符型  50 
3 addr  联系地址  字符型  200 
4 biz_appy_type  业务申请类型  字符型  3  Y Y
5 begndate  开始日期  日期型  Y
6 enddate  结束日期  日期型 
7 agnter_name  代办人姓名  字符型  50 
8 agnter_cert_type  代办人证件类型  字符型  6  Y
9 agnter_certno  代办人证件号码  字符型  50 
10 agnter_tel  代办人联系方式  字符型  30 
11 agnter_addr  代办人联系地址  字符型  200 
12 agnter_rlts  代办人关系  字符型  3  Y
13 fix_srt_no  定点排序号  字符型  3  Y
14 fixmedins_code  定点医药机构编号  字符型  12  Y
15 fixmedins_name  定点医药机构名称  字符型  200  Y
16 memo  备注  字符型  500 
             */
            Input_2505 input = new Input_2505();
            input.data = new data2505();
            input.data.psn_no = te_psn_no11.Text.Trim();
            input.data.tel = te_tel11.Text.Trim();
            input.data.biz_appy_type = cbe_biz_appy_type11.Text.Substring(0, 2);         
            input.data.begndate = de_begndate10.DateTime.ToString("yyyy-MM-dd");
            input.data.enddate = de_enddate10.DateTime.ToString("yyyy-MM-dd");
            input.data.agnter_name = te_agnter_name11.Text.Trim();
            input.data.agnter_cert_type = cbe_agnter_cert_type11.Text.Substring(0, 2);
            input.data.agnter_certno = te_agnter_certno11.Text;
            input.data.agnter_tel = te_agnter_tel11.Text.Trim();
            input.data.agnter_addr = te_agnter_addr11.Text.Trim();
            input.data.agnter_rlts = cbe_agnter_rlts11.Text.Substring(0, 1);
            input.data.fix_srt_no = te_fix_srt_no11.Text.Trim();
            input.data.fixmedins_code = te_fixmedins_code11.Text;
            input.data.fixmedins_name = te_fixmedins_name11.Text;
            input.data.memo = me_memo11.Text;
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "2505";
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 1;
            string code = "1";

            Output_2505 output = new Output_2505();
            output.result = new result2505();

            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")
            {
                Drjk_ryddba_input drjk = new Drjk_ryddba_input();

                drjk = Functions.Mapping<Drjk_ryddba_input, data2505>(input.data);
                drjk.zt = 1;
                drjk.czrq = ClassSqlHelper.GetServerTime();
                drjk.czydm = post.operatorId;
                drjk.trt_dcla_detl_sn = output.result.trt_dcla_detl_sn;
                int count = ClassSqlHelper.ExecuteSql(drjk.ToAddSql());
                if (count < 0)
                {
                    MessageBox.Show("医保成功保存本地失败");

                }
                MessageBox.Show("人员定点备办理成功！");
            }
            memoEdit3.Text = json;
        
        }

        private void sb_Ddcx11_Click(object sender, EventArgs e)
        {
            gc_Ddrycx11.DataSource = ClassSqlHelper.QueryFixedPoint(de_Ddqrq11.DateTime, de_Ddzrq11.DateTime);
            gv_Ddrycx11.BestFitColumns();

        }

        private void sb_Output11_Click(object sender, EventArgs e)
        {
            if (gv_Ddrycx11.RowCount > 0)
            {
                Functions.ExportGridViewToExcelNew(this, gv_Ddrycx11, "定点信息");
            }
        }

        private void sb_Ddcxbl11_Click(object sender, EventArgs e)
        {
            if (gv_Ddrycx11.RowCount < 1)
            {
                MessageBox.Show("没有您撤销的信息，请重新查询！");
                return;
            }
            if ((int)MessageBox.Show("确认需要撤销吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != 1)
            {
                return;
            }
            if (te_Cxyy11.Text == "")
            {
                MessageBox.Show("请必须输入撤销原因！");
                te_Cxyy10.Focus();
                return;
            }
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "2506";
            post.insuplc_admdvs = "";
            post.operatorId = czydm;
            post.operatorName = czyxm;
            post.inModel = 0;

            Input_2506 input = new Input_2506();
            input.data = new data2506();
            input.data.memo = te_Cxyy11.Text.Trim();
            input.data.psn_no = Convert.ToString(gv_Ddrycx11.GetFocusedRowCellValue("人员编号"));
            input.data.trt_dcla_detl_sn = Convert.ToString(gv_Ddrycx11.GetFocusedRowCellValue("待遇申报明细流水号"));
            StringBuilder strEeor = new StringBuilder();
            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            me_Yyxxfh11.Text = json;
            if (code == "0")
            {

                int count = ClassSqlHelper.UpFixedPoint(Convert.ToString(gv_Ddrycx11.GetFocusedRowCellValue("人员编号")), Convert.ToString(gv_Ddrycx11.GetFocusedRowCellValue("待遇申报明细流水号")), czydm);
                if (count < 0)
                {
                    MessageBox.Show("医保成功保存本地失败");
                    return;
                }
                MessageBox.Show("撤销成功！");
            }
        }

        private void sb_Qd5_Click(object sender, EventArgs e)
        {

        }

        private void Btn_1101_cs_querynew(object sender, EventArgs e)
        {
            PostBase post = new PostBase();
            //先进行读卡 然后再获取个人基本信息
            post.hisId = "0";
            post.tradiNumber = "1101";
            //post.sign_no = sign_no;
            
            string code = "1";
            post.operatorId = "kfadmin";
            post.operatorName = "管理员";
            post.inModel = 0;

            string kh = string.IsNullOrWhiteSpace(tb_1101_cs_inuptsbk.Text) == true ? tb_1101_cs_input.Text : tb_1101_cs_inuptsbk.Text;
            string klx = string.IsNullOrWhiteSpace(tb_1101_cs_inuptsbk.Text) == true ? "02" : "03";
            string insuplc_admdvs = tb_cs_1101_insuplc_admdvs.Text;
            string name = tb_cs_1101_name.Text.Trim();


            post.insuplc_admdvs = string.IsNullOrWhiteSpace(insuplc_admdvs)==true? "130300" : insuplc_admdvs;

            Input_1101 input1101 = new Input_1101();
            input1101.data = new data1101();
            input1101.data.mdtrt_cert_type = klx;//“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
            input1101.data.mdtrt_cert_no = kh;
            input1101.data.card_sn = "";
            input1101.data.begntime = ClassSqlHelper.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss");
            input1101.data.psn_cert_type ="1";
            input1101.data.certno = kh;
            input1101.data.psn_name = name ?? "";

            Output_1101 Output1101 = new Output_1101();

            string jsonStr = ClassHelper.SaveToInterface1(input1101, out Output1101, post, out code);
            //string jsonStr = "{\"infcode\":\"0\",\"inf_refmsgid\":\"51000020210804174313\",\"refmsg_time\":\"20210804174937472\",\"respond_time\":\"51000020210804174313\",\"err_msg\":\"\",\"output\":{    \"idetinfo\": [],    \"baseinfo\": {      \"certno\": \"511722201904080030\",      \"psn_no\": \"511722201904080030\",      \"gend\": \"1\",      \"brdy\": \"2019 - 04 - 08\",      \"psn_cert_type\": \"\",      \"psn_name\": \"赵恩泽\",      \"age\": 2    },    \"insuinfo\": [      {        \"insuplc_admdvs\": \"511700\",        \"cvlserv_flag\": \"0\",        \"balc\": 0.0,        \"psn_type\": \"1560\",        \"emp_name\": \"宣汉县医疗保障参保库(宣汉财政代缴五类人员)\",        \"insutype\": \"390\"      }    ]  }}";
            this.tb_1101_cs_output.Text = jsonStr;
        }

        private void btn_9102_query_Click(object sender, EventArgs e)
        {
            PostBase post1 = new PostBase();
            post1.hisId = "0";
            post1.tradiNumber = "9102";
            //post.sign_no = sign_no;
            post1.insuplc_admdvs = "";
            post1.inModel = 0;
            post1.operatorId = czydm;
            post1.operatorName = czyxm;

            Input_9102 input9102 = new Input_9102();
            input9102.fsDownloadIn = new fsUploadIn9102();
            input9102.fsDownloadIn.filename = "202104235333186670332774374.txt.zip";
            input9102.fsDownloadIn.fixmedins_code = "plc";// ConfigurationManager.AppSettings["fixmedins_code"];
            input9102.fsDownloadIn.file_qury_no = "fsi/plc/a00a5f4c66cd43568019ea69117510";

            Output_9102 output9102 = new Output_9102();
            string resp=ClassHelper.Download_9102(input9102, post1, tb_9102_code.Text ?? "1301",false);

        }

        private static string downloadfilepath = ConfigurationManager.AppSettings["DownloadFilePath"];

        private void btn_9102_webclient_Click(object sender, EventArgs e)
        {
            PostBase post1 = new PostBase();
            post1.hisId = "0";
            post1.tradiNumber = "9102";
            //post.sign_no = sign_no;
            post1.insuplc_admdvs = "";
            post1.inModel = 0;
            post1.operatorId = czydm;
            post1.operatorName = czyxm;

            Input_9102 input9102 = new Input_9102();
            input9102.fsDownloadIn = new fsUploadIn9102();
            input9102.fsDownloadIn.filename = "202104235333186670332774374.txt.zip";
            input9102.fsDownloadIn.fixmedins_code = "plc";// ConfigurationManager.AppSettings["fixmedins_code"];
            input9102.fsDownloadIn.file_qury_no = "fsi/plc/a00a5f4c66cd43568019ea69117510";

            Output_9102 output9102 = new Output_9102();
            string postData = ClassHelper.Download_9102(input9102, post1, tb_9102_code.Text ?? "1301",true);

            string url = "http://test.inner.getway.ylbzj.hebei.gov.cn/ebus/mbs_fsi_auth/fsi/api/fileupload/download";
            using (WebClient client = new WebClient())
            {
                try
                {
                    string nonce = Guid.NewGuid().ToString();
                    long times = (int)(DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds;
                    string token = "d6Ad0P1B3z6Dsdst0gYAFPlz8YlIvFDx";
                    string paasid = "mbs_fsi_auth";
                    string signature = DigestUtils.Sha256Hex(times + token + nonce + times);
                    client.Headers.Add("Content-Type", "application/json");
                    client.Headers.Add("x-tif-paasid", paasid);
                    client.Headers.Add("x-tif-signature", signature);
                    client.Headers.Add("x-tif-timestamp", times.ToString());
                    client.Headers.Add("x-tif-nonce", nonce);
                    byte[] byteArray= System.Text.Encoding.UTF8.GetBytes(postData);
                    byte[] byteResult = client.UploadData(new Uri(url), "POST", byteArray);
                    MessageBox.Show(System.Text.Encoding.UTF8.GetString(byteResult));
                    client.DownloadFileAsync(new Uri(url), "202104235333186670332774374.txt.zip");
                    client.DownloadProgressChanged += client_DownloadProgressChanged;
                    client.DownloadFileCompleted += client_DownloadFileCompleted;

                }
                catch (Exception ex)
                {
                    tb_9102_resp.Text = ex.Message+ ex.InnerException;
                }
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.label5.Text = string.Format("当前接收到{0}字节，文件大小总共{1}字节", e.BytesReceived, e.TotalBytesToReceive);
            this.progressBar1.Value = e.ProgressPercentage;
        }

        void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("文件下载被取消", "提示", MessageBoxButtons.OKCancel);
            }
            this.progressBar1.Value = 0;
            MessageBox.Show("文件下载成功", "提示");
        }




    }
}
