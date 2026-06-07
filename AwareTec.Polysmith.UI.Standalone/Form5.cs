using iTextSharp.text.pdf;
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

namespace AwareTec.Polysmith.UI
{
    public partial class Form5 : Form
    {        private string bak_pdfPath = Application.StartupPath + "\\TemPlate\\bak_help.pdf";
           private string m_sourcePath = Application.StartupPath + "\\help.pdf";
        public Form5()
        {
            InitializeComponent();
            this.Load += Form5_Load;
        }

        void Form5_Load(object sender, EventArgs e)
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
                EditPDF(bak_pdfPath);
            }
        }
        private void EditPDF(string fpath)
        {

            string path = fpath.Replace("\\", "/");
            PdfReader reader = new PdfReader(path);
            MemoryStream ms = new MemoryStream();
            PdfStamper stamper = new PdfStamper(reader, ms);
            stamper.Writer.ViewerPreferences = PdfWriter.HideWindowUI;
            stamper.Writer.SetEncryption(PdfWriter.STRENGTH128BITS, null, null, PdfWriter.AllowPrinting | PdfWriter.AllowFillIn);
            stamper.Writer.CloseStream = false;
            //直接弹出打印不用点击打印按钮
            //PdfAction.JavaScript("myOnMessage();", stamper.Writer);
            //stamper.Writer.AddJavaScript("this.print(true);function myOnMessage(aMessage) {app.alert('Test',2);} var msgHandlerObject = new Object();doc.onWillPrint = myOnMessage;this.hostContainer.messageHandler = msgHandlerObject;");

            //StringBuilder script = new StringBuilder();
            //script.Append("this.print({bUI: false,bSilent: true,bShrinkToFit: true});").Append("\r\nthis.closeDoc();");
            //script.Append("var pp = this.getPrintParams();pp.printerName = '\\\\fpserver\\hp LaserJet 1010'; this.print(pp);");
            //script.Append("this.print(flase);");
            //stamper.Writer.AddJavaScript(script.ToString(),false);
            //PdfContentByte cb = stamper.GetOverContent(1);
            //cb.Circle(250, 250, 50);
            //cb.SetColorFill(iTextSharp.text.Color.RED);
            //cb.SetColorStroke(iTextSharp.text.Color.WHITE);
            //cb.FillStroke();
            stamper.Close();
            ViewPdf(ms);

        }
        private void ViewPdf(Stream fs)
        {
            byte[] buffer = new byte[fs.Length];
            fs.Position = 0;
            fs.Read(buffer, 0, (int)fs.Length);
        }
    }
}
