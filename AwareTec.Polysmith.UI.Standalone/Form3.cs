using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsUI.Docking;
namespace AwareTec.Polysmith.UI
{
    public partial class Form3 : DockContent
    {
        public Form3()
        {
            InitializeComponent();
            this.Load += Form3_Load;
        }

        void Form3_Load(object sender, EventArgs e)
        {


        }
        private List<float> readLocalData()
        {
            List<float> data = new List<float>();
            string m_pt = Path.Combine(Application.StartupPath, "Data\\test.dat");
            if (File.Exists(m_pt))
            {
                using (StreamReader sr = new StreamReader(m_pt, Encoding.UTF8))
                {
                    string lines = sr.ReadLine();
                    while (!string.IsNullOrEmpty(lines))
                    {
                        string[] dat = lines.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (dat.Length > 0)
                        {
                            data.AddRange(dat.Select(t => float.Parse(t)));
                        }
                        lines = sr.ReadLine();
                    }
                }
            }
            return data;
        }
        public List<byte> ReadFile()
        {
            List<byte> rangData = new List<byte>();
            string m_pt = Path.Combine(Application.StartupPath, "Data\\test.dat");
            if (File.Exists(m_pt))
            {
                //构造读取文件流对象
                using (FileStream fsRead = new FileStream(m_pt, FileMode.Open)) //打开文件，不能创建新的
                {
                    //开辟临时缓存内存
                    byte[] byteArrayRead = new byte[1024 * 1024]; //  1字节*1024 = 1k 1k*1024 = 1M内存
                    //通过死缓存去读文本中的内容
                    while (true)
                    {
                        //readCount  这个是保存真正读取到的字节数
                        int readCount = fsRead.Read(byteArrayRead, 0, byteArrayRead.Length);
                        for (int i = 0; i < readCount; i++)
                            rangData.Add(byteArrayRead[i]);
                        //既然是死循环 那么什么时候我们停止读取文本内容 我们知道文本最后一行的大小肯定是小于缓存内存大小的
                        if (readCount < byteArrayRead.Length)
                        {
                            break;  //结束循环
                        }
                    }
                }
            }
            return rangData;
        }

        private void buttonEx1_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    DateTime st = DateTime.Now;
                    List<float> data = readLocalData();
                    textBoxEx1.Text = (DateTime.Now - st).TotalMilliseconds.ToString();
                    string des = string.Join(" ", data);
                    Console.WriteLine("wancheng");
                }));
            });
        }

        private void buttonEx2_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(()=>{
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    DateTime st = DateTime.Now;
                    List<byte> data = ReadFile();
                    textBoxEx2.Text = (DateTime.Now - st).TotalMilliseconds.ToString();
                    string des = System.Text.Encoding.UTF8.GetString(data.ToArray());
                    Console.WriteLine("wancheng");
                }));
            });
        }
    }
}
