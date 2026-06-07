using MinimalistUI.EnumModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalistUI
{
    public class DrawUtils
    {
        /// <summary>
        /// 绘制圆角框
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="space"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static GraphicsPath GetRoundedFrame(Rectangle rect,
                                                    float space,
                                                    float radius)
        {
            rect = new Rectangle((int)(rect.X  + space),
                                 (int)(rect.Y + space),
                                 (int)(rect.Width - space * 2),
                                 (int)(rect.Height - space * 2));

            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.StartFigure();
            graphicsPath.AddArc(rect.X, rect.Y, 2 * radius, 2 * radius, 180, 90);    //左上角圆弧
            graphicsPath.AddLine(rect.X + radius, rect.Y, rect.X + rect.Width - radius, rect.Y);   //上边界
            graphicsPath.AddArc(rect.X + rect.Width - 2 * radius, rect.Y, 2 * radius, 2 * radius, 270, 90);   //右上角圆弧
            graphicsPath.AddLine(rect.X + rect.Width, rect.Y + radius, rect.X + rect.Width, rect.Y + rect.Height - radius);      //右边界
            graphicsPath.AddArc(rect.X + rect.Width - 2 * radius, rect.Y + rect.Height - 2 * radius, 2 * radius, 2 * radius, 0, 90); //右下角圆弧
            graphicsPath.AddLine(rect.X + rect.Width - radius, rect.Y + rect.Height, rect.X + radius, rect.Y + rect.Height);     //下边界
            graphicsPath.AddArc(rect.X, rect.Y + rect.Height - 2 * radius, 2 * radius, 2 * radius, 90, 90);       //左下角圆弧
            graphicsPath.AddLine(rect.X, rect.Y + rect.Height - radius, rect.X, rect.Y + radius);                           //左边界
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        /// <summary>
        /// 绘制在矩形中水平垂直居中的字符串
        /// </summary>
        /// <returns></returns>
        public static Point GetAlignmentStrPosInRect(Rectangle rect,
                                                    SizeF strSize,
                                                    TextAlignment[] textAlignments)
        {
            //空指针及数组个数不为2, 报参数异常
            if (textAlignments == null ||
               textAlignments.Length != 2)
                throw new ArgumentOutOfRangeException();


            //如果非垂直与水平组合的, 报参数异常
            if (((int)textAlignments[0] + (int)textAlignments[1]) < 1 ||
                ((int)textAlignments[0] + (int)textAlignments[1]) > 5)
                throw new ArgumentOutOfRangeException();

            TextAlignment horizontalOption = textAlignments[0] > 0 ? textAlignments[0] : textAlignments[1];
            TextAlignment verticalOption = textAlignments[0] > 0 ? textAlignments[1] : textAlignments[0];

            int x = -1;
            int y = -1;
            switch (horizontalOption)
            {
                case TextAlignment.HorizontalLeft:
                    x = rect.X;
                    break;
                case TextAlignment.HorizontalCenter:
                    x = (int)(rect.X + rect.Width / 2 - strSize.Width / 2);
                    break;
                case TextAlignment.HorizontalRight:
                    x = (int)(rect.X + rect.Width - strSize.Width);
                    break;
            }
            switch (verticalOption)
            {
                case TextAlignment.VerticalTop:
                    y = rect.Y;
                    break;
                case TextAlignment.VerticalCenter:
                    y = (int)(rect.Y + rect.Height / 2 - strSize.Height / 2);
                    break;
                case TextAlignment.VerticalBottom:
                    y = (int)(rect.Y + rect.Height - strSize.Height);
                    break;
            }

            return new Point(x,y);
        }
    }
}
