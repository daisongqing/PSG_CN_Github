using AwareTec.Polysmith.Cloud.UI.Forms.Tips;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Forms
{
    public partial class PdfViewDialog : CloudSkinForm
    {
        private string m_sourcePath = Application.StartupPath + "\\help.pdf";
        private string bak_pdfPath = Application.StartupPath + "\\TemPlate\\bak_help.pdf";
        public PdfViewDialog()
        {
            InitializeComponent();
            this.Load += Form5_Load;
            this.FormClosed += Form5_FormClosed;
            this.Text = "睡眠分析系统说明书";
            this.WindowState = FormWindowState.Maximized;
        }
        /// <summary>
        /// 设置待预览文件是否加密标志
        /// </summary>
        public bool Encryption = true;
        /// <summary>
        /// 设置文件路径
        /// </summary>
        public string PdfPath
        {
            set
            {
                bak_pdfPath = value;
            }
        }
        /// <summary>
        /// 设置加密文件源路径 当Encryption标志true时必须要进行设置
        /// </summary>
        public string EncryptionPdfPath
        {
            set
            {
                m_sourcePath = value;
            }
        }
       private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            File.Delete(bak_pdfPath);
            this.Dispose();
        }

       private void MyOpenFileDialog()
       {
           try
           {
               AxAcroPDFLib.AxAcroPDF axAcroPDF = new AxAcroPDFLib.AxAcroPDF();
               axAcroPDF.BeginInit();
               ((System.ComponentModel.ISupportInitialize)(axAcroPDF)).BeginInit();
               axAcroPDF.Location = new System.Drawing.Point(0, 0);
               axAcroPDF.Dock = DockStyle.Fill;
               this.Controls.Add(axAcroPDF);
               axAcroPDF.EndInit();
               ((System.ComponentModel.ISupportInitialize)(axAcroPDF)).EndInit();
               axAcroPDF.LoadFile(bak_pdfPath);
               axAcroPDF.setView("fit");
               axAcroPDF.setShowScrollbars(true);
               axAcroPDF.setShowToolbar(!Encryption);
               axAcroPDF.setZoom(100);
               this.BringToFront();
           }
           catch (Exception ee)
           {
               MessageForm.Show("系统未安装Acrobat Reader DC版本的PDF阅读器！");
           }
       }

        private void Form5_Load(object sender, EventArgs e)
        {
            bool ready = true;
            if (Encryption)
            {
                try
                {
                    iTextSharp.text.pdf.RandomAccessFileOrArray ra = new iTextSharp.text.pdf.RandomAccessFileOrArray(m_sourcePath);
                    if (ra != null)
                    {
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        byte[] password = System.Text.ASCIIEncoding.ASCII.GetBytes("pps");
                        iTextSharp.text.pdf.PdfReader thepdfReader = new iTextSharp.text.pdf.PdfReader(ra, password);
                        iTextSharp.text.pdf.PdfReader.unethicalreading = true;
                        int pages = thepdfReader.NumberOfPages;
                        iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document();
                        iTextSharp.text.pdf.PdfCopy pdfCopy = new iTextSharp.text.pdf.PdfCopy(pdfDoc, ms);
                        pdfDoc.Open();
                        int i = 0;
                        while (i < pages)
                        {
                            pdfCopy.AddPage(pdfCopy.GetImportedPage(thepdfReader, i + 1));
                            i += 1;
                        }
                        pdfDoc.Close();
                        File.WriteAllBytes(bak_pdfPath, ms.ToArray());
                    }
                    else
                        ready = false;
                }
                catch
                {
                    ready = false;
                    MessageForm.Show("系统未安装Acrobat Reader DC版本的PDF阅读器！");
                }
            }
            if (ready)
                MyOpenFileDialog();
        }

    }
}
