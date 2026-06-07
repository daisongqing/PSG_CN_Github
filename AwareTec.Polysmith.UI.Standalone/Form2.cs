using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace AwareTec.Polysmith.UI
{
    public partial class Form2 : Form
    {
        protected Steema.TeeChart.TChart tChart1;
        private Steema.TeeChart.Styles.FastLine fastLine1;
        public Form2()
        {
            InitializeComponent();
            LoadChart();
            fastLine1.Add(0,40);
            timer1.Interval = 200;
            timer1.Enabled = true;
        }

        private void LoadChart()
        {
            tChart1 = new Steema.TeeChart.TChart();
            this.fastLine1 = new Steema.TeeChart.Styles.FastLine();
            fastLine1.AutoRepaint = false;

            // Set Ordering to none, to increment speed when adding points
            fastLine1.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.None;
            fastLine1.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.None;

            // Initialize axis scales
            tChart1.Axes.Bottom.SetMinMax(0, 3600000);
            tChart1.Axes.Left.SetMinMax(1, 100);
            this.tChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tChart1.Location = new System.Drawing.Point(0, 0);
            this.tChart1.Name = "tChart1";
            this.tChart1.Size = new System.Drawing.Size(440, 205);
            this.tChart1.TabIndex = 0;
            tChart1.Header.Visible = false;
            tChart1.Legend.Visible = false;
            tChart1.Axes.Left.AxisPen.Width = 1;
            tChart1.Axes.Bottom.AxisPen.Width = 1;
            tChart1.Axes.Bottom.Labels.RoundFirstLabel = false;
            tChart1.Aspect.View3D = false;
            tChart1.Header.Font.Size = 18;
            this.tChart1.Aspect.View3D = false;
            this.tChart1.CurrentTheme = Steema.TeeChart.ThemeType.Opera;
            this.tChart1.Header.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChart1.Header.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChart1.Header.Brush.Gradient.SigmaFocus = 0F;
            this.tChart1.Header.Brush.Gradient.SigmaScale = 0F;
            this.tChart1.Header.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));

            this.tChart1.Header.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.tChart1.Header.Font.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tChart1.Header.Lines = new string[] {
        "Area series gradient & transparency"};
            this.tChart1.Header.Shadow.Height = 0;
            this.tChart1.Header.Shadow.Width = 0;
            this.tChart1.Legend.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChart1.Legend.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChart1.Legend.Brush.Gradient.SigmaFocus = 0F;
            this.tChart1.Legend.Brush.Gradient.SigmaScale = 0F;
            this.tChart1.Legend.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tChart1.Panel.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tChart1.Panel.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tChart1.Panel.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tChart1.Panel.Brush.Gradient.SigmaFocus = 0F;
            this.tChart1.Panel.Brush.Gradient.SigmaScale = 0F;
            this.tChart1.Panel.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tChart1.Series.Add(this.fastLine1);
            this.tChart1.Walls.Back.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChart1.Walls.Bottom.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChart1.Walls.Bottom.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChart1.Walls.Bottom.Brush.Gradient.SigmaFocus = 0F;
            this.tChart1.Walls.Bottom.Brush.Gradient.SigmaScale = 0F;
            this.tChart1.Walls.Bottom.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tChart1.Walls.Left.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChart1.Walls.Left.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChart1.Walls.Left.Brush.Gradient.SigmaFocus = 0F;
            this.tChart1.Walls.Left.Brush.Gradient.SigmaScale = 0F;
            this.tChart1.Walls.Left.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tChart1.Walls.Right.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tChart1.Walls.Right.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChart1.Walls.Right.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChart1.Walls.Right.Brush.Gradient.SigmaFocus = 0F;
            this.tChart1.Walls.Right.Brush.Gradient.SigmaScale = 0F;
            this.tChart1.Walls.Right.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));

            // 
            // fastLine1
            // 
            this.fastLine1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            this.fastLine1.ColorEach = false;
            // 
            // 
            // 
            this.fastLine1.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            this.fastLine1.OriginalCursor = null;
            this.fastLine1.Title = "fastLine1";
            this.fastLine1.TreatNulls = Steema.TeeChart.Styles.TreatNullsStyle.Ignore;
            // 
            // 
            // 
            this.fastLine1.XValues.DataMember = "X";
            this.fastLine1.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine1.YValues.DataMember = "Y";
            this.Controls.Add(tChart1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                Random r = new Random();
                fastLine1.Add(fastLine1.XValues.Last + 1,20+ r.Next(40)-10.5);
            }
        }
    }
}
