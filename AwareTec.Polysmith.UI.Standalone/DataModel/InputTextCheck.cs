using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.UI.DataModel
{
    public class InputTextCheck
    {
        /// <summary>
        /// 值输入限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void floatvalue_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control tt = sender as Control;
            //限制只能输入数字，Backspace键，小数点
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                e.Handled = true;  //非以上键则禁止输入
            }
            if (e.KeyChar == '.' && tt.Text.Trim() == "") e.Handled = true; //禁止第一个字符就输入小数点
            if (e.KeyChar == '.' && tt.Text.Contains(".")) e.Handled = true; //禁止输入多个小数点
        }
        /// <summary>
        /// 整数值输入限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void intvalue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //限制只能输入数字，Backspace键
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;  //非以上键则禁止输入
            }
        }
        /// <summary>
        /// datetime值输入限制yyyy/MM/dd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void datetimevalue_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control tt = sender as Control;
            //限制只能输入数字，Backspace,-键
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '-')
            {
                e.Handled = true;  //非以上键则禁止输入
            }
        }
        /// <summary>
        /// 电话号码输入限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            //限制只能输入数字，Backspace键
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;  //非以上键则禁止输入
            }
        }
        /// <summary>
        /// 名称输入限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void name_KeyPress(object sender, KeyPressEventArgs e)
        {
            //限制只能字符
            if (char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;  //非以上键则禁止输入
            }
            else
            {
                if ("[ \\[ \\] \\^ \\-_*×――(^)$%~!@#$…&%￥—+=<>《》!！??？:：•`·、。，；,.;\"‘’“”-/]【】|'+=".Contains(e.KeyChar))
                {
                    e.Handled = true;  //非以上键则禁止输入
                }
            }
        }
        /// <summary>
        /// 非法字符验证
        /// </summary>
        /// <param name="strvalue"></param>
        /// <returns></returns>
        public static bool CheckChar(string strvalue)
        {
            Regex regExp = new Regex("[ \\[ \\] \\^ \\-_*×――(^)$%~!@#$…&%￥—+=<>《》!！??？:：•`·、。，；,.;\"‘’“”-]");
            return regExp.IsMatch(strvalue);
        }
        /// <summary>
        /// 文件命名规则非法字符验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void filename_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("\\/:*?\"<>|,.，。《》？、；：‘’“”{}【】|·~`[]‘’'';!！+=".Contains(e.KeyChar))
            {
                e.Handled = true;  //非以上键则禁止输入
            }
        }
    }
}
