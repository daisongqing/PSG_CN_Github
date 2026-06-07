using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinimalistUI.Components.TextboxControls
{
    internal class CustomTextBox : TextBox
    {
        internal CustomTextBox()
        {
            //SetStyle
            //(
            //    ControlStyles.AllPaintingInWmPaint |
            //    ControlStyles.ResizeRedraw |
            //    ControlStyles.OptimizedDoubleBuffer |
            //    ControlStyles.SupportsTransparentBackColor |
            //    ControlStyles.UserPaint,
            //        true
            //);
            //UpdateStyles();

            BorderStyle = BorderStyle.None;
            Multiline = true;
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    e.Graphics.Clear(Color.Transparent);
        //    base.OnPaint(e);
        //}
    }
}
