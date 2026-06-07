using MinimalistUI.CustomEventArgs.SwitchControls;
using MinimalistUI.EnumModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinimalistUI.Components.SwitchControls
{
    public partial class SwitchControl : Control
    {
        #region Fields
        #region 视觉字段 Visual Field
        private Color _openColor = Color.FromArgb(0,200,171);
        private Color _closeColor = Color.FromArgb(239, 255, 253);
        private float _radius = 20;
        private const float SPACE = 5;
        private Font _textFont = DefaultFont;
        private Color _openTextColor = DefaultForeColor;
        private Color _closeTextColor = DefaultForeColor;
        #endregion

        #region 数据字段 Data Field
        private string _leftStr = "左开关文本";
        private string _rightStr = "右开关文本";
        #endregion

        #region 交互字段
        private bool _isLeft = true;
        #endregion

        #region 绘图相关的对象字段
        private Rectangle _leftRect = Rectangle.Empty;
        private Rectangle _rightRect = Rectangle.Empty;
        #endregion

        #region 事件字段
        private event EventHandler<SwitchChangedEventArgs> _switchChanged;
        #endregion
        #endregion

        #region Properties
        #region 自定义视觉选项
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("开关打开时的块颜色")]
        public Color OpenColor
        {
            get => _openColor;
            set
            {
                _openColor = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("开关关闭时的块颜色")]
        public Color CloseColor
        {
            get => _closeColor;
            set
            {
                _closeColor = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("圆角框半径")]
        public float Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("开关打开时的文本颜色")]
        public Color OpenTextColor
        {
            get => _openTextColor;
            set
            {
                _openTextColor = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("开关关闭时的文本颜色")]
        public Color CloseTextColor
        {
            get => _closeTextColor;
            set
            {
                _closeTextColor = value;
                Invalidate();
            }
        }
        #endregion

        #region 外部数据参数
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("外部数据参数")]
        [Description("左开关文本")]
        public string LeftStr
        {
            get => _leftStr;
            set
            {
                _leftStr = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("外部数据参数")]
        [Description("右开关文本")]
        public string RightStr
        {
            get => _rightStr;
            set
            {
                _rightStr = value;
                Invalidate();
            }
        }
        #endregion

        #region 自定义事件选项
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义事件选项")]
        [Description("开关切换时触发的事件")]
        public event EventHandler<SwitchChangedEventArgs> SwitchChanged
        {
            add
            {
                if (value == null)
                    return;

                if(_switchChanged == null ||
                   _switchChanged.GetInvocationList().Length == 0)
                {
                    _switchChanged = value;
                }
                else
                {
                    foreach (var item in _switchChanged.GetInvocationList())
                    {
                        if (value == item)
                            return;
                    }
                    _switchChanged += value;
                }
            }
            remove
            {
                if (value == null)
                    return;

                if (_switchChanged == null ||
                   _switchChanged.GetInvocationList().Length == 0)
                    return;

                foreach (var item in _switchChanged.GetInvocationList())
                {
                    if (value == item)
                        _switchChanged -= value;
                }
            }
        }
        #endregion
        #endregion

        #region Constructors
        public SwitchControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Override Events Handle From Base
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            _textFont = Font;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
           
            GraphicsPath largeGraphicsPath = DrawUtils.GetRoundedFrame(this.ClientRectangle, SPACE, _radius);
            using (Brush brush = new SolidBrush(_closeColor))
                g.FillPath(brush, largeGraphicsPath);

            if (_isLeft)
            {
                GraphicsPath smallLeftGraphicsPath = DrawUtils.GetRoundedFrame(_leftRect, SPACE, _radius/3 * 2);
                using (Brush brush = new SolidBrush(_openColor))
                    g.FillPath(brush, smallLeftGraphicsPath);
            }
            else
            {
                GraphicsPath smallRightGraphicsPath = DrawUtils.GetRoundedFrame(_rightRect, SPACE, _radius/3 * 2);
                using (Brush brush = new SolidBrush(_openColor))
                    g.FillPath(brush, smallRightGraphicsPath);
            }

            //draw text
            Point leftTextLocation = DrawUtils.GetAlignmentStrPosInRect(_leftRect,
                                                                        g.MeasureString(_leftStr, _textFont),
                                                                        new TextAlignment[2] {TextAlignment.HorizontalCenter, TextAlignment.VerticalCenter });
            using (Brush brush = new SolidBrush(_isLeft ? _openTextColor : _closeTextColor)) 
                g.DrawString(_leftStr, _textFont, brush, leftTextLocation.X, leftTextLocation.Y);

            Point rightTextLocation = DrawUtils.GetAlignmentStrPosInRect(_rightRect,
                                                                        g.MeasureString(_rightStr, _textFont),
                                                                        new TextAlignment[2] { TextAlignment.HorizontalCenter, TextAlignment.VerticalCenter });
            using (Brush brush = new SolidBrush(_isLeft ? _closeTextColor : _openTextColor))
                g.DrawString(_rightStr, _textFont, brush, rightTextLocation.X, rightTextLocation.Y);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button != MouseButtons.Left)
                return;

            var leftIsSelected = _leftRect.Contains(e.Location);
            if (_isLeft == leftIsSelected)
                return;

            _isLeft = leftIsSelected;
            Invalidate();
            var args = new SwitchChangedEventArgs(_isLeft ? SwitchResults.Left : SwitchResults.Right,
                                                  _isLeft ? _leftStr : _rightStr);
            if(_switchChanged != null)
                _switchChanged(this, args);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            AdjustSize();
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            AdjustSize();
        }
        #endregion

        #region Private Methods
        private void AdjustSize()
        {
            _leftRect = new Rectangle((int)(this.ClientRectangle.X + SPACE),
                                      (int)(this.ClientRectangle.Y + SPACE),
                                      (int)((this.ClientRectangle.Width - SPACE * 2) / 2),
                                      (int)(this.ClientRectangle.Height - SPACE * 2));
            _rightRect = new Rectangle(_leftRect.X + _leftRect.Width,
                                       _leftRect.Y,
                                       _leftRect.Width,
                                       _leftRect.Height);
        }
        #endregion
    }
}
