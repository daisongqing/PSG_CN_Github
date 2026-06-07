using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AwareTec.Polysmith.Util
{
    public class ExcelTemplate
    {
        public string mFilename;
        public Application app;
        public Workbooks wbs;
        public Workbook wb;
        public Worksheets wss;
        public Worksheet ws;

        public void Create()
        {
            this.app = (Application)new ApplicationClass();
            this.wbs = this.app.Workbooks;
            this.wb = this.wbs.Add((object)true);
        }

        public void Open(string FileName)
        {
            try
            {
                Type wpsAppName;
                string progID = "Excel.Application";
                wpsAppName = Type.GetTypeFromProgID(progID);
                this.app = (Application)Activator.CreateInstance(wpsAppName);
                //this.app = new Application();// (Application)new ApplicationClass();
                this.app.Visible = false;
                this.app.DisplayAlerts = false;
                this.wbs = this.app.Workbooks;
                this.wb = this.wbs.Add((object)FileName);
                this.mFilename = FileName;
            }
            catch (Exception ee) { throw ee; }
        }

        public Worksheet SetNameValue(string SheetName)
        {
            return (Worksheet)this.wb.Worksheets[(object)SheetName];
        }

        public void SetValues(List<string> names, List<string> values)
        {
            List<string> list = new List<string>();
            foreach (Name one in app.Names)
            {
                list.Add(one.Name);
            }
            for (int index = 0; index < names.Count; ++index)
            {
                if (list.Contains(names[index]))
                {
                    try
                    {
                        this.app.Goto((object)names[index], (object)Missing.Value);

                        if (names[index].Contains("Photo"))
                        {
                            float w = Convert.ToSingle(app.ActiveCell.MergeArea.Width);
                            ((Worksheet)wb.Worksheets[1]).Shapes.AddPicture(values[index], Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, Convert.ToSingle(app.ActiveCell.Left), Convert.ToSingle(app.ActiveCell.Top), w, Convert.ToSingle(app.ActiveCell.Height));
                        }
                        else
                            this.app.ActiveCell.FormulaR1C1 = (object)values[index];
                    }
                    catch
                    {
                    }
                    list.Remove(names[index]);
                }
            }
            try
            {
                ((Worksheet)wb.Worksheets[1]).Activate();
            }
            catch { }
        }

        public void InsertPictures(string Filename, Worksheet ws)
        {
            ws.Shapes.AddPicture(Filename, MsoTriState.msoFalse, MsoTriState.msoTrue, 10f, 10f, 150f, 150f);
        }

        public void InsertActiveChart(Microsoft.Office.Interop.Excel.XlChartType ChartType, Worksheet ws, int DataSourcesX1, int DataSourcesY1, int DataSourcesX2, int DataSourcesY2, Microsoft.Office.Interop.Excel.XlRowCol ChartDataType)
        {
            ChartDataType = Microsoft.Office.Interop.Excel.XlRowCol.xlColumns;
            this.wb.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            this.wb.ActiveChart.ChartType = ChartType;
            this.wb.ActiveChart.SetSourceData(ws.get_Range(ws.Cells[(object)DataSourcesX1, (object)DataSourcesY1], ws.Cells[(object)DataSourcesX2, (object)DataSourcesY2]), (object)ChartDataType);
            this.wb.ActiveChart.Location(XlChartLocation.xlLocationAsObject, (object)ws);
        }

        public bool SaveAs(object FileName, System.Action<bool> callback = null)
        {
            try
            {
                string directoryName = Path.GetDirectoryName(FileName.ToString());
                if (!Directory.Exists(directoryName))
                    Directory.CreateDirectory(directoryName);
                this.wb.SaveAs(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                if (callback != null)
                    callback.BeginInvoke(true, (AsyncCallback)null, (object)null);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Close()
        {
            this.wb.Close(Type.Missing, Type.Missing, Type.Missing);
            this.wbs.Close();
            this.app.Quit();
            this.wb = (Workbook)null;
            this.wbs = (Workbooks)null;
            this.app = (Application)null;
            GC.Collect();
        }
    }
}