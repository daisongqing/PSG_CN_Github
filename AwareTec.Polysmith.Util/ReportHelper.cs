using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Reporting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Management;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Util
{
    public class ReportHelper
    {
        /// <summary>
        /// 文件保存类型
        /// </summary>
        public enum DocumentFormat
        {
            Doc = 10,
            DocX = 22,
            Pdf = 40,
            Xps = 41,
            Jpeg = 104
        }
        private Aspose.Words.Document m_document = null;
        /// <summary>
        /// 创建模板对象
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        public void CreateNewWordDocument(string templateName)
        {
            m_document = new Aspose.Words.Document(templateName);
            m_document.MailMerge.FieldMergingCallback = new HandleMergeFieldInsertDocument();
        }


        /// <summary> 
        /// 保存报告
        /// </summary> 
        /// <param name="fileName">文件名</param> 
        /// <param name="wDoc">Document对象</param> 
        public bool SaveAs(string fileName, DocumentFormat sformat)
        {
            try
            {
                Assembly asse = Assembly.Load("AwareTec.Polysmith.DataBaseCom");
                var classs = asse.GetTypes();
                List<string> des = new List<string>();
                List<string> values = new List<string>();
                for (int i = 0; i < classs.Length; i++)
                {
                    PropertyInfo[] mi = classs[i].GetProperties();
                    object o = asse.CreateInstance(classs[i].FullName);//加载类型
                    for (int j = 0; j < mi.Length; j++)
                    {
                        des.Add(mi[j].Name);
                        object value = mi[j].GetValue(o, null);
                        values.Add(value != null ? value.ToString() : j.ToString());
                    }
                }
                return SaveAs(des, values, fileName, sformat);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 保存报告
        /// </summary>
        /// <param name="Names">域名队列</param>
        /// <param name="Values">与域名对应的值队列</param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="sformat">保存格式</param>
        /// <returns></returns>
        public bool SaveAs(List<string> Names, List<string> Values, string SavePath, DocumentFormat sformat, Action<bool> callback = null)
        {
            m_document.MailMerge.Execute(Names.ToArray(), Values.ToArray());
            new Task(() =>
            {
                bool ok = false;
                try
                {
                    System.Threading.Thread.Sleep(1000);
                    m_document.Save(SavePath, (SaveFormat)((byte)sformat));
                    ok = true;
                }
                catch
                {

                }
                if (callback != null)
                {
                    callback.Invoke(ok);
                }
            }).Start();
            return true;
        }
        /// <summary>
        /// 打印并预览Word
        /// </summary>
        /// <param name="doc"></param>
        public void PrintWordDcoument(List<string> Names, List<string> Values, Icon icon = null)
        {
            m_document.MailMerge.Execute(Names.ToArray(), Values.ToArray());
            PrintWordDcoument(m_document, icon);
        }
        /// <summary>
        /// 打印并预览Word
        /// </summary>
        /// <param name="doc"></param>
        public void PrintWordDcoument(Aspose.Words.Document doc, Icon icon = null)
        {
            try
            {

                Aspose.Words.Rendering.AsposeWordsPrintDocument printer = new Aspose.Words.Rendering.AsposeWordsPrintDocument(doc);

                PrintPreviewDialog previewDlg = new PrintPreviewDialog();
                if (icon != null)
                    previewDlg.Icon = icon;
                previewDlg.Document = printer;
                previewDlg.ShowInTaskbar = false;
                previewDlg.MinimizeBox = true;
                previewDlg.PrintPreviewControl.Zoom = 1;
                previewDlg.Document.DocumentName = "报告单.doc";
                previewDlg.Document.BeginPrint += Document_BeginPrint;
                previewDlg.WindowState = FormWindowState.Maximized;
                previewDlg.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印失败" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Document_BeginPrint(object sender, PrintEventArgs e)
        {
            var doc = sender as PrintDocument;
            if (e.PrintAction == PrintAction.PrintToPrinter)
            {
                string defaultPrinterName = new PrinterSettings().PrinterName;
                if (!CheckPrinterConnected(defaultPrinterName))
                {
                    MessageBox.Show("打印失败:未能连接至默认的打印机", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                    return;
                }
                else
                {

                    // MessageBox.Show("打印成功", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 检查打印机是否可以被连接
        /// </summary>
        /// <param name="defaultName"></param>
        /// <returns></returns>
        private bool CheckPrinterConnected(string defaultName)
        {
            ManagementScope scope = new ManagementScope(@"\root\cimv2");
            scope.Connect();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

            foreach (ManagementObject printer in searcher.Get())
            {
                string printerName = printer["Name"].ToString().ToLower();
                if (printerName == defaultName.ToLower())
                {
                    if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
                    {
                        // printer is offline by user
                        return false;
                    }
                    else
                    {
                        // printer is not offline
                        return true;
                    }
                }
            }
            return false;
        }
    }
    class HandleMergeFieldInsertDocument : IFieldMergingCallback
    {
        //文本处理在这里，如果写在这一块，则不起作用
        void IFieldMergingCallback.FieldMerging(FieldMergingArgs e)
        {

        }
        //图片处理在这里
        void IFieldMergingCallback.ImageFieldMerging(ImageFieldMergingArgs args)
        {
            if (args.DocumentFieldName.Equals("photo"))
            {
                // 使用DocumentBuilder处理图片的大小
                DocumentBuilder builder = new DocumentBuilder(args.Document);
                builder.MoveToMergeField(args.FieldName);

                Shape shape = builder.InsertImage(args.FieldValue.ToString());

                // 设置x,y坐标和高宽.
                shape.Left = 30;
                shape.Top = 0;
                shape.Width = 600;
                shape.Height = 300;
            }
        }
    }
}
