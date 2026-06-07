using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsUI.Docking;
namespace AwareTec.Polysmith.UI
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            //panel1.Paint += panel1_Paint;
            this.Load += Form4_Load;
        }

        void Form4_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                Form3 ff = new Form3();
                ff.Text = "你好你得是在你的数据玩带你的";
                ff.Show(dockPanel1);
            }
        }
        int m_typ = 5;
        void panel1_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics gra = e.Graphics)
            {
                gra.DrawImage(CreatImage(m_typ,0,0, Width,Height), 0, 0, Width, Height);
            }
        }

        private Image CreatImage(int typ,int x,int y, int width,int height)
        {
            Bitmap map = new Bitmap(width, height);
            map.MakeTransparent();
            ///"L", "P", "R", "S", "UP" 
            string strValue = typ == 1 ? "左侧" : typ == 2 ? "俯卧" : typ == 3 ? "右侧" : typ == 4 ? "坐立" : "仰卧";
            int rota = typ == 1 ? 90 : typ == 2 ? 180 : typ == 3 ? 270 : typ == 4 ? 0 : 0;
            using (Graphics g = Graphics.FromImage(map))
            {
                using (Pen p = new Pen(Brushes.LightGray, 2))
                {
                    int w = 80, h = 100;
                    Rectangle rect = new Rectangle(x + width / 2 - w / 2, y + height / 2 - h / 2, w, h);
                    g.DrawEllipse(p, rect);
                    g.DrawEllipse(p, rect.X - 8, rect.Y + h / 2, 8, 20);
                    g.DrawEllipse(p, rect.X + w, rect.Y + h / 2, 8, 20);
                    if (typ != 4)
                    {
                        Point o = new Point(rect.X + w / 2, rect.Y - 10);
                        g.DrawCurve(p, new Point[] { o, new Point(o.X - 7, o.Y + 10) });
                        g.DrawCurve(p, new Point[] { o, new Point(o.X + 7, o.Y + 10) });
                    }
                    using (Font f = new Font("宋体", 9))
                    {
                        float waterWide = g.MeasureString(strValue, f).Width;
                        g.DrawString(strValue, f, p.Brush, new PointF(rect.X + rect.Width / 2 - waterWide / 2, rect.Y + rect.Height / 2 - f.Height / 2));
                    }
                }
            }
            return RotateImg(map, rota);
        }
        /// <summary>
        /// 以逆时针为方向对图像进行旋转
        /// </summary>
        /// <param name="b">位图流</param>
        /// <param name="angle">旋转角度[0,360](前台给的)</param>
        /// <returns></returns>
        public Image RotateImg(Image b, int angle)
        {
            angle = angle % 360;

            //弧度转换
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);

            //原图的宽和高
            int w = b.Width;
            int h = b.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));

            //目标位图
            Bitmap dsImage = new Bitmap(W, H);
            dsImage.MakeTransparent();
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //计算偏移量
            Point Offset = new Point((W - w) / 2, (H - h) / 2);

            //构造图像显示区域：让图像的中心与窗口的中心点一致
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(360 - angle);

            //恢复图像在水平和垂直方向的平移
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(b, rect);

            //重至绘图的所有变换
            g.ResetTransform();

            g.Save();
            g.Dispose();
            //保存旋转后的图片
            b.Dispose();
            return dsImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
