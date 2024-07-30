using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YiBaoBase
{
  public static  class Functions
    {
        public static System.Data.DataTable GetOutTxtString(string txtDocPath, System.Data.DataTable dt_OutTrade)
        {
            try
            {
                if (!File.Exists(txtDocPath))
                {
                    MessageBox.Show("该路径下没有找到文件");
                    return dt_OutTrade;
                }
                if (Path.GetExtension(txtDocPath) != ".txt")
                {
                    return dt_OutTrade;
                }
                StreamReader reader = new StreamReader(txtDocPath, Encoding.GetEncoding("utf-8"));
                while (true)
                {
                    string all = reader.ReadLine();
                    if (all == null) break;
                    string[] str = all.Split(new string[1] { "\t" }, StringSplitOptions.None);
                    if (dt_OutTrade.Columns.Count < str.Length)
                    {
                        int moreColumn = str.Length - dt_OutTrade.Columns.Count;
                        for (int i = 0; i < moreColumn; i++)
                        {
                            dt_OutTrade.Columns.Add("by-" + i);
                        }
                    }
                    dt_OutTrade.Rows.Add(str);
                }
                reader.Close();
                //  File.Delete(txtDocPath);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return dt_OutTrade;
        }

        /// <param name="filename"></param>
        public static void ExportGridViewToExcelNew(XtraForm FormName, GridView gridView, string filename)
        {
            //System.Data.DataTable dt = (System.Data.DataTable)gridView.DataSource;

            SaveFileDialog sfd = new SaveFileDialog();
            filename += DateTime.Now.ToString("yyyyMMdd") + "-" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
            sfd.FileName = filename;
            sfd.Filter = "Excel文件(*.xls) | *.xls";
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog(FormName) == DialogResult.OK && sfd.FileName.Trim() != null)
            {
                int rowIndex = 1;
                int colIndex = 0;
                int colNum = gridView.Columns.Count;
                System.Reflection.Missing miss = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
                xlapp.Visible = true;
                Microsoft.Office.Interop.Excel.Workbooks mBooks = (Microsoft.Office.Interop.Excel.Workbooks)xlapp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook mBook = (Microsoft.Office.Interop.Excel.Workbook)mBooks.Add(miss);
                Microsoft.Office.Interop.Excel.Worksheet mSheet = (Microsoft.Office.Interop.Excel.Worksheet)mBook.ActiveSheet;
                Microsoft.Office.Interop.Excel.Range mRange = mSheet.get_Range((object)"A1", System.Reflection.Missing.Value);


                //设置对齐方式
                mSheet.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                //设置文字自动换行 
                // mSheet.Cells.WrapText = true;

                //设置第一行高度，即标题栏
                ((Microsoft.Office.Interop.Excel.Range)mSheet.Rows["1:1", System.Type.Missing]).RowHeight = 20;
                //设置字体大小（10号字体）
                mSheet.Range[mSheet.Cells[1, 1], mSheet.Cells[gridView.RowCount + 1, gridView.Columns.Count]].Font.Size = 10;
                //设置单元格边框
                Microsoft.Office.Interop.Excel.Range range1 = mSheet.Range[mSheet.Cells[1, 1], mSheet.Cells[gridView.RowCount + 1, gridView.Columns.Count]];
                range1.Borders.LineStyle = 1;
                //mSheet.Columns.EntireColumn.AutoFit();
                //写标题
                for (int row = 1; row <= gridView.Columns.Count; row++)
                {
                    if (gridView.Columns[row - 1].Visible == true)
                    {
                        mSheet.Cells[1, row] = gridView.Columns[row - 1].GetTextCaption();
                        Range rngA = (Range)mSheet.Columns[row, Type.Missing];//设置单元格格式
                        rngA.NumberFormatLocal = "@";//字符型格式
                        // rngA.AutoFit();
                    }

                }
                //mSheet.Columns.EntireColumn.AutoFit();
                try
                {
                    for (int i = 0; i < gridView.RowCount; i++)
                    {
                        rowIndex++;
                        colIndex = 0;
                        if (gridView.GetRowCellValue(i, gridView.Columns[0]).ToString().Trim() != "")
                        {
                            for (int j = 0; j < gridView.Columns.Count; j++)
                            {
                                colIndex++;
                                if (gridView.Columns[j].Visible == true)
                                {
                                    //以下代码为保留原来字段类型，为NULL的转换为空格
                                    if (gridView.GetRowCellValue(i, gridView.Columns[j]) == null)
                                    {
                                        mSheet.Cells[rowIndex, colIndex] = "";
                                    }
                                    else
                                    {
                                        mSheet.Cells[rowIndex, colIndex] = gridView.GetRowCellValue(i, gridView.Columns[j]);
                                    }

                                    //以下代码为把字段转换为字符，为NULL的转换为空格
                                    string value = gridView.GetRowCellValue(i, gridView.Columns[j]) == null ? "" : gridView.GetRowCellValue(i, gridView.Columns[j]).ToString();
                                    if (gridView.Columns[j].DisplayFormat.FormatType == DevExpress.Utils.FormatType.Numeric)
                                    {
                                        value = Convert.ToDecimal(value).ToString("f2");
                                        decimal value1 = Convert.ToDecimal(value);
                                        mSheet.Cells[rowIndex, colIndex] = value1;
                                    }
                                    else
                                    {
                                        mSheet.Cells[rowIndex, colIndex] = value;
                                    }
                                }
                            }
                        }
                    }

                    mBook.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel7, miss, miss, miss, miss, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                                 miss, miss, miss, miss, miss);
                }

                catch (Exception ex)
                {
                    MessageBox.Show("EXCEL导出异常!"+ex.ToString());
                }
            }
        }

        public static List<T> ToList<T>(this System.Data.DataTable table)
        {
            try
            {
                List<T> list = new List<T>();
                if (!table.HasData()) return list;

                foreach (DataRow row in table.Rows)
                {
                    T t = Activator.CreateInstance<T>();
                    PropertyInfo[] pis = t.GetType().GetProperties();
                    foreach (PropertyInfo pi in pis)
                    {
                        if (pi.CanWrite == false) continue;
                        if (table.Columns.Contains(pi.Name))
                        {
                            object value = row[pi.Name];
                            if (pi.PropertyType.Name.ToLower().Contains("int"))
                            {
                                pi.SetValue(t, value == DBNull.Value ? 0 : Convert.ToInt32(value), null);
                            }
                            else if (pi.PropertyType.Name.ToLower().Contains("string"))
                            {
                                pi.SetValue(t, value == DBNull.Value ? "" : Convert.ToString(value), null);
                            }
                            else
                            {
                                pi.SetValue(t, value == DBNull.Value ? null : value, null);
                            }
                        }
                    }
                    list.Add(t);
                }

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 是否包含有表
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool HasData(this System.Data.DataTable table)
        {
            return !(table == null || table.Rows.Count == 0);
        }

        /// <summary>
        ///将table  保存记事本文件  中
        /// </summary>
        /// <param name="dirTXT">文件名称</param>
        /// <param name="dt">table</param>
        public static void WriteTXT(string dirTXT, System.Data.DataTable dt)
        {
            FileStream fs = new FileStream(dirTXT, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                //开始写入
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sw.WriteLine(dt.Rows[i][0].ToString() + " " + dt.Rows[i][1].ToString() + " " + dt.Rows[i][2].ToString() + " " + dt.Rows[i][3].ToString() + " " + dt.Rows[i][4].ToString() + " ");
                }
                //清空缓冲区
                sw.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭流
                sw.Close();
                fs.Close();
            }
        }

        public static System.Data.DataTable JsonToDataTable(string json)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            //JsonStr为Json字符串
            JArray array = JsonConvert.DeserializeObject(json) as JArray;//反序列化为数组
            if (array.Count > 0)
            {
                StringBuilder columns = new StringBuilder();

                JObject objColumns = array[0] as JObject;
                //构造表头
                foreach (JToken jkon in objColumns.AsEnumerable<JToken>())
                {
                    string name = ((JProperty)(jkon)).Name;
                    columns.Append(name + ",");
                    table.Columns.Add(name);
                }
                //向表中添加数据
                for (int i = 0; i < array.Count; i++)
                {
                    DataRow row = table.NewRow();
                    JObject obj = array[i] as JObject;
                    foreach (JToken jkon in obj.AsEnumerable<JToken>())
                    {

                        string name = ((JProperty)(jkon)).Name;
                        string value = ((JProperty)(jkon)).Value.ToString();
                        row[name] = value;
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        /// <summary>
        /// R代表目标实体   T代表数据源实体
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static R Mapping<R, T>(T model)
        {

            R result = Activator.CreateInstance<R>();
            foreach (PropertyInfo info in typeof(R).GetProperties())
            {
                PropertyInfo pro = typeof(T).GetProperty(info.Name);
                if (pro != null)
                    info.SetValue(result, pro.GetValue(model));
            }
            return result;
        }
    }
}
