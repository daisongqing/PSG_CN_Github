using MinimalistUI.CustomEventArgs.TextboxControls;
using MinimalistUI.EnumModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MinimalistUI.Components.TextboxControls
{
    public partial class MinimalistTextbox : Control
    {
        #region Fields
        #region Visual Correlation 视觉相关
        /// <summary>
        /// 背景色
        /// </summary>
        private Color _backColor = DefaultBackColor;
        /// <summary>
        /// 失焦的线框框线颜色
        /// </summary>
        private Color _wireframeColorLoseFocus = Color.FromArgb(220,223,230);
        /// <summary>
        /// 获焦的线框框线颜色
        /// </summary>
        private Color _wireframeColorOnFocus = Color.FromArgb(0, 200, 171);
        /// <summary>
        /// 线框框线的圆角半径
        /// </summary>
        private float _radius = 18;
        /// <summary>
        /// 失焦时文本框显示名颜色
        /// </summary>
        private Color _textBoxDisplayNameColorLoseFocus = Color.FromArgb(220, 223, 230);
        /// <summary>
        /// 获焦时文本框显示名颜色
        /// </summary>
        private Color _textBoxDisplayNameColorOnFocus = Color.FromArgb(0, 200, 171);
        /// <summary>
        /// 文本框显示名的字体
        /// </summary>
        private Font _textBoxDisplayNameFont = DefaultFont;
        /// <summary>
        /// 失焦时错误信息文本颜色(获取焦点时不显示错误信息)
        /// </summary>
        private Color _errorMessageTextColorLoseFocus = Color.FromArgb(255, 135, 135);
        /// <summary>
        /// 错误信息文本字体
        /// </summary>
        private Font _errorMessageTextFont = DefaultFont;
        /// <summary>
        /// 提示文字的颜色
        /// </summary>
        private Color _cueTextColor = Color.FromArgb(144, 147, 153);
        /// <summary>
        /// 提示文字的字体
        /// </summary>
        private Font _cueTextFont = DefaultFont;
        /// <summary>
        /// 输入文字的颜色
        /// </summary>
        private Color _inputTextColor = Color.Black;
        /// <summary>
        /// 输入文字的字体
        /// </summary>
        private Font _inputTextFont = DefaultFont;
        /// <summary>
        /// 文本框的字符
        /// </summary>
        private Char _textBoxChar = '\0';

        private const int SPACE = 5;
        private const int SPACE_COEFFICIENT = 10;
        private const int ERROR_MSG_REGION_HEIGHT = 30;
        private const int TEXTBOX_NAME_DISPLAY_HEIGHT = 16;
        private const int PICTUREBOX_SQUARE_LENGTH = 26;
        private const int PICTUREBOX_MARGIN = SPACE * SPACE_COEFFICIENT - PICTUREBOX_SQUARE_LENGTH;
        #endregion

        #region External Input Data Params 外部输入数据参数
        private Image _leftIcon;
        private Dictionary<ControlStatus, Image> _rightIcons;
        private StringBuilder _cueText = new StringBuilder();
        private StringBuilder _errorMessage = new StringBuilder();
        private StringBuilder _textBoxDisplayName = new StringBuilder();
        private bool _readonly = false;
        #endregion

        #region Internal Data Params 内部数据参数
        private bool _enabled = false;
        private Color _textBoxDisplayNameColor = Color.Empty;
        private ControlStatus _status = ControlStatus.LoseFocusAndSuccess;
        private readonly CustomTextBox _textbox = new CustomTextBox();
        private readonly PictureBox _leftPictureBox = new PictureBox();
        private readonly PictureBox _rightPictureBox = new PictureBox();
        private Color _wireframeColor = Color.Empty;
        private Rectangle _textBoxNameDisplayRegion = Rectangle.Empty;
        private Rectangle _errorMsgRegion = Rectangle.Empty;
        private Rectangle _wireframeRegion = Rectangle.Empty;
        private string _textBoxText = string.Empty;
        private Font _textBoxFont = DefaultFont;
        private Color _textBoxForeColor = Color.Black;
        #endregion

        #region 事件字段
        private event EventHandler<TextBoxLoseFocusEventArgs> _textBoxLoseFocus;

        private event EventHandler<TextBoxEnterEditEventArgs> _textBoxEnterEdit;
        #endregion
        #endregion

        #region Properties

        #region 自定义视觉选项
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("失焦的线框框线颜色")]
        public Color WireframeColorLoseFocus
        {
            get => _wireframeColorLoseFocus;
            set
            {
                _wireframeColorLoseFocus = value;
                _wireframeColor = this.Focused ? _wireframeColorOnFocus : _wireframeColorLoseFocus;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("获焦的线框框线颜色")]
        public Color WireframeColorOnFocus
        {
            get => _wireframeColorOnFocus;
            set
            {
                _wireframeColorOnFocus = value;
                _wireframeColor = this.Focused ? _wireframeColorOnFocus : _wireframeColorLoseFocus;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("线框框线圆角半径")]
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
        [Description("失焦时文本框显示名颜色")]
        public Color TextBoxDisplayNameColorLoseFocus
        {
            get => _textBoxDisplayNameColorLoseFocus;
            set
            {
                _textBoxDisplayNameColorLoseFocus = value;
                _textBoxDisplayNameColor = this.Focused ? _textBoxDisplayNameColorOnFocus : _textBoxDisplayNameColorLoseFocus;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("获焦时文本框显示名颜色")]
        public Color TextBoxDisplayNameColorOnFocus
        {
            get => _textBoxDisplayNameColorOnFocus;
            set
            {
                _textBoxDisplayNameColorOnFocus = value;
                _textBoxDisplayNameColor =  this.Focused ? _textBoxDisplayNameColorOnFocus : _textBoxDisplayNameColorLoseFocus;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("文本框显示名的字体")]
        public Font TextBoxDisplayNameFont
        {
            get => _textBoxDisplayNameFont;
            set
            {
                _textBoxDisplayNameFont = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("失焦时错误信息文本颜色(获取焦点时不显示错误信息)")]
        public Color ErrorMessageTextColorLoseFocus
        {
            get => _errorMessageTextColorLoseFocus;
            set
            {
                _errorMessageTextColorLoseFocus = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("错误信息文本字体")]
        public Font ErrorMessageTextFont
        {
            get => _errorMessageTextFont;
            set
            {
                _errorMessageTextFont = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("提示文字的颜色")]
        public Color CueTextColor
        {
            get => _cueTextColor;
            set
            {
                _cueTextColor = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("提示文字的字体")]
        public Font CueTextFont
        {
            get => _cueTextFont;
            set
            {
                _cueTextFont = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("输入文字的颜色")]
        public Color InputTextColor
        {
            get => _inputTextColor;
            set
            {
                _inputTextColor = value;
                _textbox.ForeColor = _inputTextColor;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("输入文字的字体")]
        public Font InputTextFont
        {
            get => _inputTextFont;
            set
            {
                _inputTextFont = value;
                _textbox.Font = _inputTextFont;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义视觉选项")]
        [Description("文本框字符")]
        public Char TextBoxChar
        {
            get => _textBoxChar;
            set
            {
                _textBoxChar = value;
                _textbox.PasswordChar = value;
            }
        }
        #endregion

        #region 外部输入数据参数
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("外部输入数据参数")]
        [Description("左侧图标")]
        public Image LeftIcon
        {
            get => LeftImage;
            set => LeftImage = value;
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("外部输入数据参数")]
        [Description("右侧图标")]
        public Dictionary<ControlStatus, Image> RightIcon
        {
            get => RightImages;
            set => RightImages = value;
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("外部输入数据参数")]
        [Description("提示输入信息")]
        public string CueText
        {
            get => _cueText.ToString();
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;
                if (_cueText.ToString() == value)
                    return;
                _cueText.Clear();
                _cueText.Append(value);
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("外部输入数据参数")]
        [Description("错误信息")]
        public string ErrorMessage
        {
            get => _errorMessage.ToString();
            set
            {
                if (_errorMessage.ToString() == value)
                    return;
                _errorMessage.Clear();
                _errorMessage.Append(value);
                if(_status == ControlStatus.LoseFocusAndSuccess)
                    _status = string.IsNullOrWhiteSpace(_errorMessage.ToString()) ? ControlStatus.LoseFocusAndSuccess : ControlStatus.LoseFocusAndError;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("外部输入数据参数")]
        [Description("文本框显示名")]
        public string TextBoxDisplayName
        {
            get => _textBoxDisplayName.ToString();
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;
                if (_textBoxDisplayName.ToString() == value)
                    return;
                _textBoxDisplayName.Clear();
                _textBoxDisplayName.Append(value);

                AllocationSizeAndLocation();
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("外部输入数据参数")]
        [Description("文本框只读")]
        public bool ReadOnly
        {
            get => _readonly;
            set
            {
                if (value == _readonly)
                    return;
                _readonly = value;
                _textbox.ReadOnly = _readonly;
            }
        }
        #endregion

        #region 私有属性
        private string TextBoxText
        {
            get => _textBoxText;
            set
            {
                if (value == _textBoxText)
                    return;

                _textBoxText = value;

                if (_textbox.Text == _textBoxText)
                    return;

                _textbox.Text = _textBoxText;
            }
        }

        private Font TextBoxFont
        {
            get => _textBoxFont;
            set
            {
                if (value == _textBoxFont)
                    return;

                _textBoxFont = value;

                if (_textbox.Font == _textBoxFont)
                    return;

                _textbox.Font = _textBoxFont;
            }
        }

        private Color TextBoxForeColor
        {
            get => _textBoxForeColor;
            set
            {
                if (value == _textBoxForeColor)
                    return;

                _textBoxForeColor = value;

                if (_textbox.ForeColor.A == _textBoxForeColor.A &&
                    _textbox.ForeColor.B == _textBoxForeColor.B &&
                    _textbox.ForeColor.G == _textBoxForeColor.G &&
                    _textbox.ForeColor.R == _textBoxForeColor.R)
                    return;

                _textbox.ForeColor = _textBoxForeColor;
            }
        }

        private Image LeftImage
        {
            get => _leftIcon;
            set
            {
                _leftIcon = value;

                if (_leftIcon == null)
                {
                    _leftPictureBox.Visible = false;
                    return;
                }

                _leftPictureBox.BackgroundImage = _leftIcon;
                _leftPictureBox.Visible = true;
            }
        }

        private Dictionary<ControlStatus, Image> RightImages
        {
            get => _rightIcons;
            set
            {
                _rightIcons = value;

                if (_rightIcons == null ||
                    _rightIcons.Count == 0)
                {
                    _rightPictureBox.Visible = false;
                    return;
                }

                _rightPictureBox.BackgroundImage = _rightIcons[_status];
                _rightPictureBox.Visible = true;
            }
        }

        private bool TextBoxEnabled
        {
            get => _enabled;
            set
            {
                if (_enabled == value)
                    return;

                _textbox.Enabled = _enabled = value;
            }
        }

        #endregion

        #region 自定义事件选项
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义事件选项")]
        [Description("文本框失去焦点时发生")]
        public event EventHandler<TextBoxLoseFocusEventArgs> TextBoxLoseFocus
        {
            add
            {
                if (value == null)
                    return;

                if (_textBoxLoseFocus == null ||
                   _textBoxLoseFocus.GetInvocationList().Length == 0)
                {
                    _textBoxLoseFocus = value;
                }
                else
                {
                    foreach (var item in _textBoxLoseFocus.GetInvocationList())
                    {
                        if (value == item)
                            return;
                    }
                    _textBoxLoseFocus += value;
                }
            }
            remove
            {
                if (value == null)
                    return;

                foreach (var item in _textBoxLoseFocus.GetInvocationList())
                {
                    if (value == item)
                        _textBoxLoseFocus -= value;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义事件选项")]
        [Description("文本框进入输入模式时发生")]
        public event EventHandler<TextBoxEnterEditEventArgs> TextBoxEnterEdit
        {
            add
            {
                if (value == null)
                    return;

                if (_textBoxEnterEdit == null ||
                   _textBoxEnterEdit.GetInvocationList().Length == 0)
                {
                    _textBoxEnterEdit = value;
                }
                else
                {
                    foreach (var item in _textBoxEnterEdit.GetInvocationList())
                    {
                        if (value == item)
                            return;
                    }
                    _textBoxEnterEdit += value;
                }
            }
            remove
            {
                if (value == null)
                    return;

                foreach (var item in _textBoxLoseFocus.GetInvocationList())
                {
                    if (value == item)
                        _textBoxEnterEdit -= value;
                }
            }
        }
        #endregion

        #endregion

        public MinimalistTextbox()
        {
            InitializeComponent();
            init();
            _textbox.AcceptsReturn = false;
            _textbox.Multiline = false;
            _textbox.GotFocus += _textbox_GotFocus;
            //_textbox.LostFocus += _textbox_LostFocus;
            _textbox.KeyDown += _textbox_KeyDown;
            _textbox.TextChanged += _textbox_TextChanged;
            _textbox.LocationChanged += _textbox_LocationChanged;
            _leftPictureBox.MouseClick += LeftPictureBox_MouseClick;
            _leftPictureBox.LocationChanged += _leftPictureBox_LocationChanged;
            _rightPictureBox.MouseClick += RightPictureBox_MouseClick;
            _rightPictureBox.LocationChanged += _rightPictureBox_LocationChanged;
        }

        private void _textbox_TextChanged(object sender, EventArgs e)
        {
            Text = _textbox.Text;
        }

        private void _textbox_KeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        private void _textbox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (Text == _cueText.ToString())
                _textbox.Text = string.Empty;
        }

        private void _textbox_LostFocus(object sender, EventArgs e)
        {
            base.OnLostFocus(e);
            
            _wireframeColor = _wireframeColorLoseFocus;
            _textBoxDisplayNameColor = _textBoxDisplayNameColorLoseFocus;
            _status = string.IsNullOrWhiteSpace(_errorMessage.ToString()) ? ControlStatus.LoseFocusAndSuccess : ControlStatus.LoseFocusAndError;
            Text = _textbox.Text;
            Invalidate();

            var args = new TextBoxLoseFocusEventArgs();
            if (_textBoxLoseFocus != null)
                _textBoxLoseFocus(this, args);
        }

        private void _textbox_GotFocus(object sender, EventArgs e)
        {
            base.OnGotFocus(e);
            _wireframeColor = _wireframeColorOnFocus;
            _textBoxDisplayNameColor = _textBoxDisplayNameColorOnFocus;
            _status = ControlStatus.Focus;
            //_textbox.Text = _textbox.Text == _cueText.ToString() ? string.Empty : _textbox.Text;
            //TextBoxText = string.IsNullOrEmpty(Text) ? _cueText.ToString() : Text;
            Invalidate();
            var args = new TextBoxEnterEditEventArgs();
            if (_textBoxEnterEdit != null)
            {
                _textBoxEnterEdit(this,args);
            }
        }

        private void _rightPictureBox_LocationChanged(object sender, EventArgs e)
        {
            if (_wireframeRegion == Rectangle.Empty)
                return;

            if (_rightPictureBox.Location.X == _wireframeRegion.X + _wireframeRegion.Width - PICTUREBOX_MARGIN / 2 - _rightPictureBox.Width &&
                _rightPictureBox.Location.Y == _textbox.Location.Y)
                return;

            _rightPictureBox.Location = new Point(_wireframeRegion.X + _wireframeRegion.Width - PICTUREBOX_MARGIN / 2 - _rightPictureBox.Width,
                                                 _textbox.Location.Y);
        }

        private void _leftPictureBox_LocationChanged(object sender, EventArgs e)
        {
            if (_wireframeRegion == Rectangle.Empty)
                return;

            if (_leftPictureBox.Location.X == _wireframeRegion.X + PICTUREBOX_MARGIN / 2 &&
               _leftPictureBox.Location.Y == _textbox.Location.Y)
                return;

            _leftPictureBox.Location = new Point(_wireframeRegion.X + PICTUREBOX_MARGIN / 2,
                                                _textbox.Location.Y);
        }

        private void _textbox_LocationChanged(object sender, EventArgs e)
        {
            int internalWidthDiff = PICTUREBOX_SQUARE_LENGTH * 2 + SPACE * 2 + PICTUREBOX_MARGIN;

            if (string.IsNullOrWhiteSpace(_textBoxDisplayName.ToString()) &&
               _wireframeRegion == Rectangle.Empty &&
               _textbox.Location.X == _wireframeRegion.X + internalWidthDiff / 2 &&
               _textbox.Location.Y == _wireframeRegion.Y + _wireframeRegion.Height / 2 - _textbox.Height / 2)
                return;

            if ((!string.IsNullOrWhiteSpace(_textBoxDisplayName.ToString())) &&
                _textBoxNameDisplayRegion == Rectangle.Empty &&
                _textbox.Location.X == _textBoxNameDisplayRegion.X &&
                _textbox.Location.Y == _textBoxNameDisplayRegion.Y + _textBoxNameDisplayRegion.Height + SPACE)
                return;


            _textbox.Location = string.IsNullOrWhiteSpace(_textBoxDisplayName.ToString()) ?
                                new Point(_wireframeRegion.X + internalWidthDiff / 2,
                                              _wireframeRegion.Y + _wireframeRegion.Height / 2 - _textbox.Height / 2)
                                : new Point(_textBoxNameDisplayRegion.X,
                                              _textBoxNameDisplayRegion.Y + _textBoxNameDisplayRegion.Height + SPACE);
        }

        private void init()
        {
            _cueText.Append(string.Empty);
            LoadTextBoxTextDisplay();
            _status = string.IsNullOrWhiteSpace(_errorMessage.ToString()) ? ControlStatus.LoseFocusAndSuccess : ControlStatus.LoseFocusAndError;
            _textBoxDisplayNameColor = _textBoxDisplayNameColorLoseFocus;
            _wireframeColor = _wireframeColorLoseFocus;
            _textbox.BackColor = _backColor;
            _textbox.ReadOnly = _readonly;
            _leftPictureBox.Height = _leftPictureBox.Width = PICTUREBOX_SQUARE_LENGTH;
            _rightPictureBox.Height = _rightPictureBox.Width = PICTUREBOX_SQUARE_LENGTH;
            _leftPictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            _rightPictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            TextBoxEnabled = Enabled;
            RightImages = null;
            LeftImage = null;
            AllocationSizeAndLocation();
            Controls.AddRange(new Control[3] { _textbox, _leftPictureBox, _rightPictureBox });
        }

        private void RightPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
        }

        private void LeftPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
        }

        #region 内置对象的事件绑定
        private void Textbox_TextChanged(object sender, EventArgs e)
        {
            if (_textbox.Text == null)
                return;
            if (_textbox.Text != string.Empty)
                return;

            Text = _textbox.Text;
        }
        #endregion

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            TextBoxEnabled = Enabled;

        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            LoadTextBoxTextDisplay();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            _status = ControlStatus.Hover;
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            _textbox.BackColor = _backColor = BackColor;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) 
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;

            //绘制圆角框
            GraphicsPath graphicsPath = DrawUtils.GetRoundedFrame(_wireframeRegion, SPACE, _radius);
            using (Pen pen = new Pen(_wireframeColor))
                g.DrawPath(pen, graphicsPath);
            using (Brush brush = new SolidBrush(_backColor))
                g.FillPath(brush, graphicsPath);

            //若失去焦点, 绘制错误提示信息
            if(_status == ControlStatus.LoseFocusAndError &&
                (!string.IsNullOrWhiteSpace(_errorMessage.ToString())))
            {
                Point errMsgTextLocation = DrawUtils.GetAlignmentStrPosInRect(_errorMsgRegion,
                                                                              g.MeasureString(_errorMessage.ToString(), _errorMessageTextFont),
                                                                              new TextAlignment[2] {TextAlignment.HorizontalLeft, TextAlignment.VerticalTop });
                using (Brush brush = new SolidBrush(_errorMessageTextColorLoseFocus))
                    g.DrawString(_errorMessage.ToString(),
                                 _errorMessageTextFont,
                                 brush,
                                 errMsgTextLocation.X,
                                 errMsgTextLocation.Y);
            }

            //绘制文本框标题名称
            Point textBoxDisplayNameLocation = DrawUtils.GetAlignmentStrPosInRect(_textBoxNameDisplayRegion,
                                                                                  g.MeasureString(_textBoxDisplayName.ToString(), _textBoxDisplayNameFont),
                                                                                  new TextAlignment[2] { TextAlignment.HorizontalLeft, TextAlignment.VerticalCenter });
            using (Brush brush = new SolidBrush(_textBoxDisplayNameColor))
                g.DrawString(_textBoxDisplayName.ToString(),
                             _textBoxDisplayNameFont,
                             brush,
                             textBoxDisplayNameLocation.X,
                             textBoxDisplayNameLocation.Y);    
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            AllocationSizeAndLocation();
            Invalidate();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            AllocationSizeAndLocation();
            Invalidate();
        }

        /// <summary>
        /// 分配尺寸位置
        /// </summary>
        private void AllocationSizeAndLocation()
        {
            _wireframeRegion = new Rectangle(ClientRectangle.X,
                                             ClientRectangle.Y,
                                             ClientRectangle.Width,
                                             ClientRectangle.Height - ERROR_MSG_REGION_HEIGHT);
            
            int internalWidthDiff = PICTUREBOX_SQUARE_LENGTH * 2 + SPACE * 2 + PICTUREBOX_MARGIN;

             _errorMsgRegion = new Rectangle(_wireframeRegion.X + internalWidthDiff / 2,
                                             ClientRectangle.Y + ClientRectangle.Height - ERROR_MSG_REGION_HEIGHT,
                                             _wireframeRegion.Width - internalWidthDiff,
                                             ERROR_MSG_REGION_HEIGHT);

            if (string.IsNullOrWhiteSpace(_textBoxDisplayName.ToString()))
            {
                _textBoxNameDisplayRegion = Rectangle.Empty;

                _textbox.Width = _wireframeRegion.Width - internalWidthDiff;
                _textbox.Height = PICTUREBOX_SQUARE_LENGTH;
                _textbox.Location = new Point(_wireframeRegion.X + internalWidthDiff / 2,
                                              _wireframeRegion.Y + _wireframeRegion.Height / 2 - _textbox.Height / 2);

                //_leftPictureBox.Location = new Point(_wireframeRegion.X + PICTUREBOX_MARGIN / 2,
                //                                     (int)(_wireframeRegion.Y + _wireframeRegion.Height / 2 - _leftPictureBox.Width / 2));
                //_rightPictureBox.Location = new Point(_wireframeRegion.X + _wireframeRegion.Width - PICTUREBOX_MARGIN / 2 - _rightPictureBox.Width,
                //                                     (int)(_wireframeRegion.Y + _wireframeRegion.Height / 2 - _rightPictureBox.Width / 2));
            }
            else
            {
                _textBoxNameDisplayRegion = new Rectangle((int)(_wireframeRegion.X + internalWidthDiff / 2),
                                                            _wireframeRegion.Y + SPACE * 4,
                                                            _wireframeRegion.Width - internalWidthDiff,
                                                            TEXTBOX_NAME_DISPLAY_HEIGHT);

                _textbox.Width = _wireframeRegion.Width - internalWidthDiff;
                _textbox.Height = _wireframeRegion.Height - _textBoxNameDisplayRegion.Height - SPACE * 8;
                _textbox.Location = new Point(_textBoxNameDisplayRegion.X,
                                              _textBoxNameDisplayRegion.Y + _textBoxNameDisplayRegion.Height + SPACE);

                //_leftPictureBox.Location = new Point(_wireframeRegion.X + PICTUREBOX_MARGIN / 2,
                //                                     (int)(_wireframeRegion.Y + _wireframeRegion.Height / 2 - _leftPictureBox.Width / 2));
                //_rightPictureBox.Location = new Point(_wireframeRegion.X + _wireframeRegion.Width - PICTUREBOX_MARGIN / 2 - _rightPictureBox.Width,
                //                                     (int)(_wireframeRegion.Y + _wireframeRegion.Height / 2 - _rightPictureBox.Width / 2));
            }

            _leftPictureBox.Location = new Point(_wireframeRegion.X + PICTUREBOX_MARGIN / 2,
                                                     _textbox.Location.Y);
            _rightPictureBox.Location = new Point(_wireframeRegion.X + _wireframeRegion.Width - PICTUREBOX_MARGIN / 2 - _rightPictureBox.Width,
                                                 _textbox.Location.Y);

        }

        private void LoadTextBoxTextDisplay()
        {
            TextBoxText = string.IsNullOrEmpty(Text) ? _cueText.ToString() : Text;
            TextBoxFont = string.IsNullOrEmpty(Text) ?
                           _cueTextFont : _inputTextFont;
            TextBoxForeColor = string.IsNullOrEmpty(Text) ?
                                _cueTextColor : _inputTextColor;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            _textbox.Focus();
        }
    }

}
