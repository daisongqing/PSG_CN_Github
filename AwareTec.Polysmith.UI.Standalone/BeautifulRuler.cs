using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Resources;
using System.Windows.Forms;
using AwareTec.Polysmith.UI.FunctionControls.tools;

namespace AwareTec.Polysmith.UI
{
    public class BeautifulRuler : Form
    {
        private ToolTip _toolTip = new ToolTip();
        private Point _offset;
        private Rectangle _mouseDownRect;
        private int _resizeBorderWidth = 5;
        private Point _mouseDownPoint;
        private ResizeRegion _resizeRegion = ResizeRegion.None;
        private ContextMenu _menu = new ContextMenu();
        private MenuItem _verticalMenuItem;
        private MenuItem _toolTipMenuItem;
        private static bool showWidth = true;
        private static bool showWebSite = true;
        private const string SHORT_WEBSITE_STRING = "BrawDraw.Com";
        private const string FULL_WEBSITE_STRING = "http://www.BrawDraw.Com";
        private string _webSiteString;
        private Color _webSiteStringColor;

        #region ResizeRegion enum
        private enum ResizeRegion
        {
            None, N, NE, E, SE, S, SW, W, NW
        }
        #endregion

        public BeautifulRuler()
        {
            SetUpMenu();
            InitializeComponent();

            Text = "Ruler";
            BackColor = Color.White;
            ClientSize = new Size(400, 75);
            FormBorderStyle = FormBorderStyle.None;
            Opacity = 0.75;
            ContextMenu = _menu;
            Font = new Font("Tahoma", 10);

            _webSiteStringColor = Color.Blue;

            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.TopMost = true;
        }

        public string WebSiteString
        {
            get
            {
                if (_webSiteString == null)
                {
                    _webSiteString = FULL_WEBSITE_STRING;
                }
                return _webSiteString;
            }
            set
            {
                value = _webSiteString;
            }
        }

        public Color WebSiteStringColor
        {
            get
            {
                if (_webSiteStringColor == Color.Empty)
                {
                    _webSiteStringColor = Color.Black;
                }
                return _webSiteStringColor;
            }
            set
            {
                value = _webSiteStringColor;
            }
        }

        private bool IsVertical
        {
            get { return _verticalMenuItem.Checked; }
            set { _verticalMenuItem.Checked = value; }
        }

        private bool ShowToolTip
        {
            get { return _toolTipMenuItem.Checked; }
            set
            {
                _toolTipMenuItem.Checked = value;
                if (value)
                {
                    SetToolTip();
                }
            }
        }

        private void SetUpMenu()
        {
            AddMenuItem("保持最顶层");
            _verticalMenuItem = AddMenuItem("竖向尺子");
            _toolTipMenuItem = AddMenuItem("工具提示");

            //默认光标停留时显示尺寸大小
            _toolTipMenuItem.Checked = true;

            MenuItem opacityMenuItem = AddMenuItem("透明度");
            AddMenuItem("-");
            AddMenuItem("退出");

            for (int i = 10; i <= 100; i += 10)
            {
                MenuItem subMenu = new MenuItem(i + "%");
                subMenu.Click += new EventHandler(OpacityMenuHandler);
                opacityMenuItem.MenuItems.Add(subMenu);
            }
        }

        private MenuItem AddMenuItem(string text)
        {
            return AddMenuItem(text, Shortcut.None);
        }

        private MenuItem AddMenuItem(string text, Shortcut shortcut)
        {
            MenuItem mi = new MenuItem(text);
            mi.Click += new EventHandler(MenuHandler);
            mi.Shortcut = shortcut;
            _menu.MenuItems.Add(mi);

            return mi;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _offset = new Point(MousePosition.X - Location.X, MousePosition.Y - Location.Y);
            _mouseDownPoint = MousePosition;
            _mouseDownRect = ClientRectangle;

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _resizeRegion = ResizeRegion.None;
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_resizeRegion != ResizeRegion.None)
            {
                HandleResize();
                return;
            }

            Point clientCursorPos = PointToClient(MousePosition);
            Rectangle resizeInnerRect = ClientRectangle;
            resizeInnerRect.Inflate(-_resizeBorderWidth, -_resizeBorderWidth);

            bool inResizableArea = ClientRectangle.Contains(clientCursorPos) && !resizeInnerRect.Contains(clientCursorPos);

            if (inResizableArea)
            {
                ResizeRegion resizeRegion = GetResizeRegion(clientCursorPos);
                SetResizeCursor(resizeRegion);

                if (e.Button == MouseButtons.Left)
                {
                    _resizeRegion = resizeRegion;
                    HandleResize();
                }
            }
            else
            {
                Cursor = Cursors.Default;

                if (e.Button == MouseButtons.Left)
                {
                    Location = new Point(MousePosition.X - _offset.X, MousePosition.Y - _offset.Y);
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnResize(EventArgs e)
        {
            if (ShowToolTip)
            {
                SetToolTip();
            }

            base.OnResize(e);
        }

        private void SetToolTip()
        {
            _toolTip.SetToolTip(this, string.Format("宽度: {0} 像素/n高度: {1} 像素", Width, Height));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    HandleMoveResizeKeystroke(e);
                    break;

                case Keys.Space:
                    ChangeOrientation();
                    break;
            }

            base.OnKeyDown(e);
        }

        private void HandleMoveResizeKeystroke(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (e.Control)
                {
                    if (e.Shift)
                    {
                        Width += 1;
                    }
                    else
                    {
                        Left += 1;
                    }
                }
                else
                {
                    Left += 5;
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (e.Control)
                {
                    if (e.Shift)
                    {
                        Width -= 1;
                    }
                    else
                    {
                        Left -= 1;
                    }
                }
                else
                {
                    Left -= 5;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (e.Control)
                {
                    if (e.Shift)
                    {
                        Height -= 1;
                    }
                    else
                    {
                        Top -= 1;
                    }
                }
                else
                {
                    Top -= 5;
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (e.Control)
                {
                    if (e.Shift)
                    {
                        Height += 1;
                    }
                    else
                    {
                        Top += 1;
                    }
                }
                else
                {
                    Top += 5;
                }
            }
        }

        private void HandleResize()
        {
            int diff = 0;
            switch (_resizeRegion)
            {
                case ResizeRegion.E:
                    diff = MousePosition.X - _mouseDownPoint.X;
                    Width = _mouseDownRect.Width + diff;
                    break;

                case ResizeRegion.S:
                    diff = MousePosition.Y - _mouseDownPoint.Y;
                    Height = _mouseDownRect.Height + diff;
                    break;

                case ResizeRegion.SE:
                    Width = _mouseDownRect.Width + MousePosition.X - _mouseDownPoint.X;
                    Height = _mouseDownRect.Height + MousePosition.Y - _mouseDownPoint.Y;
                    break;

                case ResizeRegion.NW:
                    Width = _mouseDownRect.Width - MousePosition.X + _mouseDownPoint.X;
                    Height = _mouseDownRect.Height - MousePosition.Y + _mouseDownPoint.Y;
                    break;

                default:
                    break;
            }


            int tmp;
            int height = Height;
            int width = Width;
            if (IsVertical)
            {
                tmp = Height;
                height = Width;
                width = tmp;
            }
            //当窗口过小时，保持一定大小
            if (Width < 30) Width = 30;
            if (Height < 20) Height = 20;

            if (height < 30)
            {
                showWidth = false;
            }
            else
            {
                showWidth = true;
            }

            if (height > 66 && (width > 60 && width < 200))
            {
                showWebSite = true;
                _webSiteString = SHORT_WEBSITE_STRING;
            }
            else if (height < 66 || width < 40)
            {
                showWebSite = false;
                _webSiteString = FULL_WEBSITE_STRING;
            }
            else
            {
                showWebSite = true;
                _webSiteString = FULL_WEBSITE_STRING;
            }

            this.Invalidate();
        }

        private void SetResizeCursor(ResizeRegion region)
        {
            switch (region)
            {
                case ResizeRegion.N:
                case ResizeRegion.S:
                    Cursor = Cursors.SizeNS;
                    break;

                case ResizeRegion.E:
                case ResizeRegion.W:
                    Cursor = Cursors.SizeWE;
                    break;

                case ResizeRegion.NW:
                case ResizeRegion.SE:
                    Cursor = Cursors.SizeNWSE;
                    break;

                default:
                    Cursor = Cursors.SizeNESW;
                    break;
            }
        }

        private ResizeRegion GetResizeRegion(Point clientCursorPos)
        {
            if (clientCursorPos.Y <= _resizeBorderWidth)
            {
                if (clientCursorPos.X <= _resizeBorderWidth) return ResizeRegion.NW;
                else if (clientCursorPos.X >= Width - _resizeBorderWidth) return ResizeRegion.NE;
                else return ResizeRegion.N;
            }
            else if (clientCursorPos.Y >= Height - _resizeBorderWidth)
            {
                if (clientCursorPos.X <= _resizeBorderWidth) return ResizeRegion.SW;
                else if (clientCursorPos.X >= Width - _resizeBorderWidth) return ResizeRegion.SE;
                else return ResizeRegion.S;
            }
            else
            {
                if (clientCursorPos.X <= _resizeBorderWidth) return ResizeRegion.W;
                else return ResizeRegion.E;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;

            int height = Height;
            int width = Width;

            if (IsVertical)
            {
                graphics.RotateTransform(90);
                graphics.TranslateTransform(0, -Width + 1);
                height = Width;
                width = Height;
            }

            LinearGradientBrush lgb = new LinearGradientBrush(
             new Point(0, 0),
             new Point(0, height),
             Color.Black,
             Color.White
             );
            //定义参与渐变的色彩
            Color[] colors =
    {
     Color.White,
     Color.White,
     Color.FromArgb(228,232,238),
     Color.FromArgb(202,212,222),
     Color.FromArgb(228,232,238),
     Color.White,
     Color.White
    };
            float[] positions =
    {
     0.0f,
     0.1f,
     0.36f,
     0.5f,
     0.64f,
     0.9f,
     1.0f
    };
            ColorBlend clrBlend = new ColorBlend();
            clrBlend.Colors = colors;
            clrBlend.Positions = positions;
            lgb.InterpolationColors = clrBlend;

            graphics.FillRectangle(lgb, 0, 0, width, height);

            DrawRuler(graphics, width, height);
        }

        private void DrawRuler(Graphics g, int formWidth, int formHeight)
        {
            // 边框
            g.DrawRectangle(Pens.Black, 0, 0, formWidth - 1, formHeight - 1);

            // 宽度
            if (showWidth)
            {
                g.DrawString(formWidth + " 像素", Font, Brushes.Black, 10, (formHeight / 2) - (Font.Height / 2));
            }
            // 显示站点
            if (showWebSite)
            {
                g.DrawString(WebSiteString, Font, new SolidBrush(WebSiteStringColor), 80, (formHeight / 2) - (Font.Height / 2));
            }

            // 刻度
            for (int i = 0; i < formWidth; i++)
            {
                if (i % 2 == 0)
                {
                    int tickHeight;
                    if (i % 100 == 0)
                    {
                        tickHeight = 15;
                        DrawTickLabel(g, i.ToString(), i, formHeight, tickHeight);
                    }
                    else if (i % 10 == 0)
                    {
                        tickHeight = 10;
                    }
                    else
                    {
                        tickHeight = 5;
                    }

                    DrawTick(g, i, formHeight, tickHeight);
                }
            }
        }

        private void DrawTick(Graphics g, int xPos, int formHeight, int tickHeight)
        {
            // 顶部
            g.DrawLine(Pens.Black, xPos, 0, xPos, tickHeight);

            // 底部
            g.DrawLine(Pens.Black, xPos, formHeight, xPos, formHeight - tickHeight);
        }

        private void DrawTickLabel(Graphics g, string text, int xPos, int formHeight, int height)
        {
            // 顶部数字
            g.DrawString(text, Font, Brushes.Black, xPos, height);

            // 底部数字
            g.DrawString(text, Font, Brushes.Black, xPos, formHeight - height - Font.Height);
        }

        private void OpacityMenuHandler(object sender, EventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            Opacity = double.Parse(mi.Text.Replace("%", "")) / 100;
        }

        private void MenuHandler(object sender, EventArgs e)
        {
            MenuItem mi = (MenuItem)sender;

            switch (mi.Text)
            {
                case "退出":
                    Close();
                    break;

                case "工具提示":
                    ShowToolTip = !ShowToolTip;
                    break;

                case "竖向尺子":
                    ChangeOrientation();
                    break;

                case "保持最顶层":
                    mi.Checked = !mi.Checked;
                    TopMost = mi.Checked;
                    break;

                default:
                    MessageForm.Show("未有菜单项目。");
                    break;
            }
        }

        // 改变尺子放置方向（横向/竖向）
        private void ChangeOrientation()
        {
            IsVertical = !IsVertical;
            int width = Width;
            Width = Height;
            Height = width;
            this.Invalidate();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "Form1";
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.ResumeLayout(false);

        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.brawdraw.com");
        }
    }
}