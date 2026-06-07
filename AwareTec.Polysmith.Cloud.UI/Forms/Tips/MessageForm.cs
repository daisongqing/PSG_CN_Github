using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Forms.Tips
{
    public partial class MessageForm: Form
    {
        public static MessageForm _Default;

        #region  属性

        #region 私有属性
        /// <summary>
        /// 图标类型与实际图片的对应关系字典映射
        /// </summary>
        private Dictionary<MessageBoxIcon, Bitmap> MessageBoxIconMappingImg = new Dictionary<MessageBoxIcon, Bitmap>();

        /// <summary>
        /// 提示文字
        /// </summary>
        private string _tipText = string.Empty;

        /// <summary>
        /// 标题文字
        /// </summary>
        private string _captionText = CAPTION_TEXT_DEFAULT;

        /// <summary>
        /// 图标类型
        /// </summary>
        private MessageBoxIcon _messageBoxIconType = MessageBoxIcon.Warning;

        private bool _showCaption = false;

        private bool _showButton = true;

        private MessageBoxButtons _buttons = MessageBoxButtons.YesNo;

        const MessageBoxButtons BUTTONS_TYPE_DEFAULT = MessageBoxButtons.YesNo;

        const string CAPTION_TEXT_DEFAULT = "";

        const MessageBoxIcon ICON_DEFAULT = MessageBoxIcon.Warning;

        #region 视觉参数

        const int CAPTION_HEIGHT = 33;

        const int PIC_ANCHOR_TOP_MARGIN = 30;
        const int BUTTON_ANCHOR_BOTTOM_MARGIN = 35;
        const int BUTTON_ANCHOR_RIGHT = 40;
        const int BUTTON_ANCHOR_LEFT = BUTTON_ANCHOR_RIGHT;
        const int LABEL_ANCHOR_TOP = PIC_ANCHOR_TOP_MARGIN + PIC_VDIFF_LABEL;

        const int BUTTON_DIFF_LABEL = 30;

        const int PIC_HDIFF_LABEL = 10;          //picturebox离label的水平距离
        const int PIC_VDIFF_LABEL = 3;          //picturebox离label的垂直距离

        const int WIDTH_PER_WORD = 18;
        const int MAX_WORD_PER_LINE = 10;
        const int WORD_WIDTH_MARGIN = 2;
        #endregion

        #endregion

        #region 受保护的属性
        public bool ShowCaption
        {
            get => _showCaption;
        }

        /// <summary>
        /// 提示文字
        /// </summary>
        public string TipText
        {
            get => _tipText;
            set
            {
                _tipText = value;
                AttentionTipsLabel.Text = _tipText;
                AttentionTipsLabel.Size = CalculateTextBoxSize(_tipText, AttentionTipsLabel.Font);
                ResizeForm();
            }
        }

        /// <summary>
        /// 显示按钮
        /// </summary>
        public bool ShowButton
        {
            get => _showButton;
            set
            {
                _showButton = value;
                ResizeForm();
            }
        }

        /// <summary>
        /// 标题文字
        /// </summary>
        public string CaptionText
        {
            get => _captionText;
        }

        /// <summary>
        /// 图标类型
        /// </summary>
        public MessageBoxIcon MessageBoxIconType
        {
            get => _messageBoxIconType;
            set
            {
                if (!MessageBoxIconMappingImg.ContainsKey(value))
                    _messageBoxIconType = ICON_DEFAULT;
                else
                    _messageBoxIconType = value;
                AttentionIconPictureBox.BackgroundImage = MessageBoxIconMappingImg[_messageBoxIconType];
            } 
        }

        public MessageBoxButtons Buttons
        {
            get => _buttons;
            set
            {
                //暂不支持三种按钮的
                if (value == MessageBoxButtons.AbortRetryIgnore ||
                    value == MessageBoxButtons.YesNoCancel)
                    _buttons = BUTTONS_TYPE_DEFAULT;
                else
                    _buttons = value;
            }
            
        }

        /// <summary>
        /// 获取单例
        /// </summary>
        public static MessageForm Default
        {
            get => _Default ?? (_Default = new MessageForm());
        }

        #endregion
        #endregion

        private event EventHandler<CaptionVisibleChangedEventArgs> _messageFormCaptionVisibleChanged;

        #region 事件定义

        /// <summary>
        /// 标题栏可见性改变时
        /// </summary>
        public event EventHandler<CaptionVisibleChangedEventArgs> MessageFormCaptionVisibleChanged
        {
            add => this._messageFormCaptionVisibleChanged += value;
            remove => this._messageFormCaptionVisibleChanged -= value;
        }
        #endregion


        #region 受保护的方法

        protected void ShowCaptionText(bool showCaption, string captionText = "")
        {
            if(showCaption != _showCaption)
            {
                _showCaption = showCaption;
                CaptionVisibleChangedEventArgs args = new CaptionVisibleChangedEventArgs()
                { visible = showCaption };
                _messageFormCaptionVisibleChanged?.Invoke(this, args);
            }

            if (showCaption)
            {
                _captionText = captionText;
                Text = _captionText;
            }
            else
            {
                _captionText = CAPTION_TEXT_DEFAULT;
                Text = _captionText;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private MessageForm()
        {
            MessageBoxIconMappingImg.Add(MessageBoxIcon.Warning, Properties.Resources.attention);
            //this.MessageFormCaptionVisibleChanged += MessageForm_MessageFormCaptionVisibleChanged;
            InitializeComponent();
        }

        /// <summary>
        /// 根据字符串计算适宜的文本框尺寸
        /// </summary>
        private Size CalculateTextBoxSize(string text,Font font)
        {
            int line = text.Length % MAX_WORD_PER_LINE == 0 ? text.Length / MAX_WORD_PER_LINE : text.Length / MAX_WORD_PER_LINE + 1;

            int sizeW = 0, sizeH = 0;

            using (Graphics g = this.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(text, font);
                sizeW = (int)Math.Ceiling(textSize.Width);
                sizeH = (int)Math.Ceiling(textSize.Height);
            }
            
            int width = line == 1 ? sizeW + WORD_WIDTH_MARGIN : MAX_WORD_PER_LINE * WIDTH_PER_WORD;
            int height = line * sizeH;
            return new Size(width,height);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            switch(_buttons)
            {
                case MessageBoxButtons.YesNo:
                    DialogResult = DialogResult.No;
                    break;
                case MessageBoxButtons.OKCancel:
                case MessageBoxButtons.RetryCancel:
                    DialogResult = DialogResult.Cancel;
                    break;
                default:
                    throw new NotSupportedException("枚举类型超出处理范围");
            }

            this.Opacity = 0;
            //this.Hide();
        }

        private void MessageForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Enter:
                    ConfirmButton_Click(ConfirmButton,e);
                    break;
                default:
                    break;
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            switch(_buttons)
            {
                case MessageBoxButtons.OK:
                case MessageBoxButtons.OKCancel:
                    DialogResult = DialogResult.OK;
                    break;
                case MessageBoxButtons.YesNo:
                    DialogResult = DialogResult.Yes;
                    break;
                case MessageBoxButtons.RetryCancel:
                    DialogResult = DialogResult.Retry;
                    break;
                default:
                    throw new NotSupportedException("枚举类型超出处理范围");
            }

            this.Opacity = 0;
            //this.Hide();
        }

        /// <summary>
        /// 标题栏可见性改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageForm_MessageFormCaptionVisibleChanged(object sender, CaptionVisibleChangedEventArgs e)
        {
            //if (e.visible)
            //    this.Size = new Size(this.Width, this.Height + CAPTION_HEIGHT);
        }

        private void ResizeForm()
        {
            int w, h;
            if(_showButton)
            {
                w = AttentionTipsLabel.Width + CancelButton.Width / 2 + BUTTON_ANCHOR_RIGHT + PIC_HDIFF_LABEL
                    + BUTTON_ANCHOR_LEFT + CancelButton.Width/2 - AttentionIconPictureBox.Width/2 + AttentionIconPictureBox.Width;

                h = AttentionTipsLabel.Height + BUTTON_DIFF_LABEL + CancelButton.Height + BUTTON_ANCHOR_BOTTOM_MARGIN 
                    + LABEL_ANCHOR_TOP;
            }
            else
            {
                w = AttentionTipsLabel.Width + CancelButton.Width / 2 + BUTTON_ANCHOR_RIGHT + PIC_HDIFF_LABEL
                    + BUTTON_ANCHOR_LEFT + AttentionIconPictureBox.Width;

                h = AttentionTipsLabel.Height + BUTTON_ANCHOR_BOTTOM_MARGIN
                    + LABEL_ANCHOR_TOP;
            }
            this.ClientSize = new Size(w, h);
            ChangeLayout();
        }

        /// <summary>
        /// 改变所有按钮的可见性和文本
        /// </summary>
        /// <param name="buttons"></param>
        private void ChangeButtonVisible(MessageBoxButtons buttons)
        {
            const string OK_TEXT = "确定";
            const string CANCEL_TEXT = "取消";
            const string YES_TEXT = "是";
            const string NO_TEXT = "否";
            const string RETRY_TEXT = "重试";

            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    ConfirmButton.Text = OK_TEXT;
                    ConfirmButton.Visible = true;
                    CancelButton.Visible = false;
                    break;
                case MessageBoxButtons.OKCancel:
                    ConfirmButton.Text = OK_TEXT;
                    ConfirmButton.Visible = true;
                    CancelButton.Text = CANCEL_TEXT;
                    CancelButton.Visible = true;
                    break;
                case MessageBoxButtons.YesNo:
                    ConfirmButton.Text = YES_TEXT;
                    ConfirmButton.Visible = true;
                    CancelButton.Text = NO_TEXT;
                    CancelButton.Visible = true;
                    break;
                case MessageBoxButtons.RetryCancel:
                    ConfirmButton.Text = RETRY_TEXT;
                    ConfirmButton.Visible = true;
                    CancelButton.Text = CANCEL_TEXT;
                    CancelButton.Visible = true;
                    break;
                default:
                    throw new NotSupportedException("枚举类型超出处理范围");
            }
        }

        /// <summary>
        /// 当整体窗口尺寸改变时，改变窗口所有控件的布局
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageForm_SizeChanged(object sender, EventArgs e)
        {
            //ChangeLayout();
        }

        /// <summary>
        /// 改变整个窗体的布局
        /// </summary>
        private void ChangeLayout()
        {
            if (!_showButton)
            {
                CancelButton.Visible = false;
                ConfirmButton.Visible = false;
                this.ControlBox = true;
                this.MinimizeBox = false;
                this.MaximizeBox = false;
            }
            else
            {
                this.ControlBox = false;
                CancelButton.Visible = ConfirmButton.Visible = false;
                ChangeButtonVisible(_buttons);
            }

            //PictureBox位置也需要变化
            AttentionIconPictureBox.Location = new Point(BUTTON_ANCHOR_LEFT + CancelButton.Width/2 - AttentionIconPictureBox.Width/2,
                                                         PIC_ANCHOR_TOP_MARGIN);

            //Label位置也需要变化
            AttentionTipsLabel.Location = new Point(AttentionIconPictureBox.Location.X + AttentionIconPictureBox.Width + PIC_HDIFF_LABEL,
                                                AttentionIconPictureBox.Location.Y + PIC_VDIFF_LABEL);
            Point labelRightTopPoint = new Point(AttentionTipsLabel.Location.X + AttentionTipsLabel.Width, AttentionTipsLabel.Location.Y);
            
            if(_showButton)
            {
                //取消按钮位置也需要变化
                CancelButton.Location = new Point(BUTTON_ANCHOR_LEFT,
                                                 this.AttentionTipsLabel.Location.Y + this.AttentionTipsLabel.Height + BUTTON_DIFF_LABEL);

                //确定按钮位置也需要变化
                ConfirmButton.Location = new Point(labelRightTopPoint.X - ConfirmButton.Width / 2,
                                                   CancelButton.Location.Y);
            }
        }

        #endregion

        /// <summary>
        /// 显示带标题栏的消息框窗口
        /// </summary>
        /// <param name="text">提示信息</param>
        /// <param name="caption">标题栏</param>
        /// <param name="icon">图标类型</param>
        /// <returns></returns>
        public static DialogResult Show(string tipText, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if(string.IsNullOrWhiteSpace(tipText))
                throw new ParameterErrorException(typeof(MessageForm), "提示信息不为空串或伪空串");

            if (string.IsNullOrWhiteSpace(caption))
                throw new ParameterErrorException(typeof(MessageForm), "显示标题栏必须：标题栏文字不为空串或伪空串");
            
            _Default = new MessageForm();
            Default.ShowButton = true;
            Default.Buttons = buttons;
            Default.TipText = tipText ?? Default.TipText;
            Default.MessageBoxIconType = icon;
            Default.ShowCaptionText(true, caption);

            Default.Opacity = 1;
            return Default.ShowDialog();
        }

        /// <summary>
        /// 显示无标题栏的消息框窗口
        /// </summary>
        /// <param name="text"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static DialogResult Show(string tipText, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (string.IsNullOrWhiteSpace(tipText))
                throw new ParameterErrorException(typeof(MessageForm), "提示信息不为空串或伪空串");

            _Default = new MessageForm();
            Default.ShowButton = true;
            Default.Buttons = buttons;
            Default.TipText = tipText ?? Default.TipText;
            Default.MessageBoxIconType = icon;
            Default.ShowCaptionText(false);

            Default.Opacity = 1;
            return Default.ShowDialog();
        }

        /// <summary>
        /// 显示无标题栏且无按钮的消息框窗口
        /// </summary>
        /// <param name="tipText"></param>
        public static void Show(string tipText)
        {
            if (string.IsNullOrWhiteSpace(tipText))
                throw new ParameterErrorException(typeof(MessageForm), "提示信息不为空串或伪空串");

            _Default = new MessageForm();
            Default.ShowButton = false;
            Default.TipText = tipText ?? Default.TipText;
            Default.MessageBoxIconType = ICON_DEFAULT;
            Default.ShowCaptionText(false);

            Default.Opacity = 1;
            Default.ShowDialog();
        }


        #region 内部类

        #region 自定义异常类

        public class UIException : Exception
        {
            static string _msg4TypeTips = "在{0}的UI组件或窗体中有着这样的错误:{1}\n理由是:{2}";

            public UIException(Type type,string details,string reason):base(string.Format(_msg4TypeTips,type,details,reason)){}
        }
        public class ParameterErrorException : UIException
        {
            static string _msg = "参数错误-";
            public ParameterErrorException(Type type, string reason) : base(type, _msg, reason){}
        }
        #endregion

        #region 自定义事件消息类
        public class CaptionVisibleChangedEventArgs : EventArgs
        {
            public bool visible { get; set; }
        }

        #endregion

        #endregion
    }
}
