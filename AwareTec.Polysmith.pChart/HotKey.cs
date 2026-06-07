using System;
using System.Windows.Forms;

namespace AwareTec.Polysmith.pChart
{
    /// <summary>
    /// 热键单元
    /// </summary>
    public class HotKey
    {
        /// <summary>
        /// 当前事件的最小时间阈值
        /// </summary>
        public float MinLimitRange = 1;

        public string NewMarkName = "";

        public bool ControlEnable = false;

        public bool AltEnable = false;

        public bool ShiftEnable = false;

        public string KeyCode = "";

        public IMarker.MarkType MarkTyp = IMarker.MarkType.None;

        private Keys keydata = Keys.None;
        public Keys KeyData
        {
            get
            {
                if (keydata == Keys.None)
                {
                    if (KeyCode != "")
                        keydata = (Keys)Enum.Parse(typeof(Keys), KeyCode);
                    if (ControlEnable)
                        keydata |= Keys.Control;
                    if (AltEnable)
                        keydata |= Keys.Alt;
                    if (ShiftEnable)
                        keydata |= Keys.Shift;
                }
                return keydata;
            }
        }
        /// <summary>
        /// 获取快捷组合字符串
        /// </summary>
        /// <returns></returns>
        public new string ToStrig()
        {
            string text = KeyCode;
            if (ControlEnable)
                text = string.Format("Ctrl + {0}", text);
            if (AltEnable)
                text = string.Format("Alt + {0}", text);
            if (ShiftEnable)
                text = string.Format("Shift + {0}", text);
            return text;
        }
    }
}
