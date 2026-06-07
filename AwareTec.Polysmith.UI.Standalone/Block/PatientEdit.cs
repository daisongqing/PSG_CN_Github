using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.UI.EnumModel;
using AwareTec.Polysmith.Util.EnumUtils;

namespace AwareTec.Polysmith.UI.Block
{
    public partial class PatientEdit : SkinForm
    {
        #region 全局变量
        /// <summary>
        /// 输入信息提示标签
        /// </summary>
        DotNetCtl.ErrorProviderHelper m_errorProvider;
        const int InputValue_PADDING = -20;

        //const string AGE_LABEL_TEXT = "年龄";
        string AGE_LABEL_TEXT = ML.GetText("PATIENTEDIT_AGE_LABEL_TEXT", "年龄");
        const int TWO_WORD_X = 38;
        const int ADULT_AGE_UPPER_LIMIT = 150;
        const int CHILD_AGE_UPPER_LIMIT = 14;

        //const string BIRTH_DATE_TEXT = "出生年月"; 
        string BIRTH_DATE_TEXT  = ML.GetText("PATIENTEDIT_BIRTH_DATE_TEXT", "出生年月");
        const int FOUR_WORD_X = 17;
        private Doc_PatientInfo m_patientInfo = null;
        bool fill = false;
        private bool m_edit = false;
        private bool allowupdate = false;
        #endregion

        #region 事件与委托的定义
        public delegate bool SaveInfoDelegate(Doc_PatientInfo info);
        /// <summary>
        /// 获取最新
        /// </summary>
        public event SaveInfoDelegate SaveInfoHandle;

        #endregion

        #region 初始化相关
        /// <summary>
        /// 构造函数
        /// </summary>
        public PatientEdit()
        {
            InitializeComponent();

            #region 为防止designer随意更改 解绑事件，因此事件绑定一律放置于逻辑代码的构造函数中
            this.Load += this.PatientEdit_Load;

            m_errorProvider = new DotNetCtl.ErrorProviderHelper(this);


            this.NextStepButton.Click += this.NextStepButton_Click;
            this.AddButton.Click += this.AddButton_Click;
            this.CancelButton.Click += this.CancelButton_Click;
            this.EmergencyContactPersonTelTextBox.KeyPress += this.Check_KeyDown_AlwaysNumber;
            this.MobileNumberTextBox.KeyPress += this.Check_KeyDown_AlwaysNumber;
            this.BodyWeightTextBox.KeyPress += this.BodyWeightTextBox_KeyPress;
            this.HeightTextBox.KeyPress += this.HeightTextBox_KeyPress;
            this.PatientNoTextBox.TextChanged += this.PatientNoTextBox_TextChanged;
            this.PatientNoTextBox.KeyPress += PatientNoTextBox_KeyPress;
            this.PatientNoTextBox.Leave += this.PatientNoTextBox_Leave;
            //this.PatientAgeTextBox.KeyPress += this.Check_KeyDown_AlwaysNumber;
            this.PatientAgeTextBox.KeyPress += PatientAgeTextBox_KeyPress;
            this.PatientAgeTextBox.Leave += PatientAgeTextBox_Leave;
            this.PatientNameTextBox.KeyPress += this.PatientNameTextBox_KeyPress;
            this.TelephoneNumberTextBox.KeyPress += this.Check_KeyDown_AlwaysNumber;
            this.EmergencyContactPersonTextBox.KeyPress+= this.PatientNameTextBox_KeyPress;

            #endregion

            #region 动态显示年龄和出生年月
            bool useBirthdayEnable = Channel.Default.SystemSetting.UseBirthdayEnable;
            //BirthDateTimePicker.Visible = useBirthdayEnable;
            //PatientAgeTextBox.Visible = !useBirthdayEnable;
            //RequiredLabel2.Visible = !useBirthdayEnable;
            if (useBirthdayEnable)
                m_errorProvider.ShowWarning(PatientAgeTextBox as Control, "日期格式为:yyyy-MM-dd");
            this.PatientAgeTextBox.MaxLength = useBirthdayEnable ? 10 : 3;
            AgeAndBirthDateLabel.Text = useBirthdayEnable ? BIRTH_DATE_TEXT : AGE_LABEL_TEXT;
            AgeAndBirthDateLabel.Location = useBirthdayEnable ?
                                            new Point(FOUR_WORD_X, AgeAndBirthDateLabel.Location.Y) :
                                            new Point(TWO_WORD_X, AgeAndBirthDateLabel.Location.Y);
            #endregion
        }


        /// <summary>
        /// 检查年龄/出生年月输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientAgeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control control = sender as Control;
            if (control.ForeColor == Color.Red)
            {
                m_errorProvider.Clear(control);
                control.ForeColor = Color.DimGray;
            }
            if (Channel.Default.SystemSetting.UseBirthdayEnable)
                DataModel.InputTextCheck.datetimevalue_KeyPress(sender, e);
            else
                DataModel.InputTextCheck.intvalue_KeyPress(sender, e);
        }
        /// <summary>
        /// 判断年龄/出生年月输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientAgeTextBox_Leave(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if(string.IsNullOrEmpty(control.Text))
            {
                m_errorProvider.ShowError(control,Program.Language=="EN"? "The input value cannot be empty!" : "输入值不允许为空！", InputValue_PADDING);
                control.ForeColor = Color.Red;
                return;
            }
            if (Channel.Default.SystemSetting.UseBirthdayEnable)
            {
                DateTime datetime;
                bool isdatetime= DateTime.TryParse(PatientAgeTextBox.Text, out datetime);
                if (!isdatetime)
                {
                    m_errorProvider.ShowError(control, string.Format(Program.Language == "EN" ? "Please enter the correct date format (yyyy MM dd)" : "请输入正确的日期格式(yyyy-MM-dd）"), InputValue_PADDING);
                    control.ForeColor = Color.Red;
                }
            }
            else
            {
                int res;
                bool isage = int.TryParse(PatientAgeTextBox.Text, out res);
                if (!isage)
                {
                    m_errorProvider.ShowError(control, string.Format(Program.Language == "EN" ? "Age must be an integer" : "年龄必须为整数"), InputValue_PADDING);
                    control.ForeColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientEdit_Load(object sender, EventArgs e)
        {
            //如果从新监测里面 点击更多 查看病例信息 不允许更改
            if ((this.Owner as Block.NewDevice)!=null)
            {
                PatientNoTextBox.Enabled = false;
                PatientNameTextBox.Enabled = false;
                BirthDateTimePicker.Enabled = false;
                PatientAgeTextBox.Enabled = false;
                HeightTextBox.Enabled = false;
                BodyWeightTextBox.Enabled = false;
                genderMaleRadioButton.Enabled = false;
                genderFemaleRadioButton.Enabled = false;
            }
            if (SaveInfoHandle == null)
            {
                CancelButton.Visible = AddButton.Visible = false;
                this.ControlBox = true;
            }
        }

        /// <summary>
        /// 初始化信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="edit"></param>
        public void Initialize(Doc_PatientInfo info, bool edit)
        {
            if (info != null)
            {
                PatientNoTextBox.Text = info.PatientNo;
                fillData(info);
                if (!edit)
                {
                    PatientNoTextBox.ReadOnly = false;
                    this.AddButton.Text = Program.Language=="EN"?"Add":"新增";
                }
                else
                    this.AddButton.Text = Program.Language == "EN" ? "Save" : "保存";
            }
            m_patientInfo = info;
            NextStepButton.Visible = false;
            m_edit = edit;
        }

        /// <summary>
        /// 初始化信息
        /// </summary>
        /// <param name="info"></param>
        public void Initialize(Doc_PatientInfo info)
        {
            if (info != null)
            {
                PatientNoTextBox.Text = info.PatientNo;
                fillData(info);
                PatientNoTextBox.ReadOnly = false;
                //如果病例号已经在数据库中存在，则不允许添加相同的病例号
                Doc_PatientInfo patient = DataModel.DataBaseHelper.Default.Select(new Doc_PatientInfo() { PatientNo = info.PatientNo });
                if (patient != null)
                    AddButton.Enabled = false;  //新增按钮禁用
            }
            m_patientInfo = info;
            allowupdate = true;
        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="patient"></param>
        private void fillData(Doc_PatientInfo patient)
        {
            //出生年月或年龄
            if (Channel.Default.SystemSetting.UseBirthdayEnable)
                PatientAgeTextBox.Text = patient.BirthDate.ToString("yyyy/MM/dd");
            //BirthDateTimePicker.Value = patient.BirthDate;
            else
                PatientAgeTextBox.Text = patient.Age.ToString();

            //性别
            genderMaleRadioButton.Checked = patient.Gender.Equals(Program.Language == "EN" ? "Male" : "男");
            genderFemaleRadioButton.Checked = patient.Gender.Equals(Program.Language == "EN" ? "Female" : "女");

            #region 体重 身高 固定电话 手机 紧急联系人 紧急联系人电话 家庭地址
            PatientNameTextBox.Text = patient.PatientName;                      //姓名
            BodyWeightTextBox.Text = patient.Weight.ToString();                 //体重
            HeightTextBox.Text = patient.Height.ToString();                     //身高
            TelephoneNumberTextBox.Text = patient.Telephone;                    //电话
            MobileNumberTextBox.Text = patient.MobilePhone;                     //手机
            EmergencyContactPersonTextBox.Text = patient.EmergencyPeople;       //紧急联系人
            EmergencyContactPersonTelTextBox.Text = patient.EmergencyPhone;     //紧急联系人电话
            FamilyAddressTextBox.Text = patient.Address;                        //家庭地址
            PatientNoTextBox.ReadOnly = true;
            #endregion
            fill = true;
        }

        /// <summary>
        /// 检查
        /// </summary>
        /// <returns></returns>
        private bool Check()
        {
            #region 检查病例号
            string no = PatientNoTextBox.Text.Trim();
            if (no == "")
            {
                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Case number cannot be empty" : "病例号不能为空");
                return false;
            }
            #endregion

            #region 检查姓名
            string name = PatientNameTextBox.Text.Trim();
            if (name == "")
            {
                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Name cannot be empty" : "姓名不能为空");
                return false;
            }
            #endregion

            #region 检查年龄

            DateTime birthDate = new DateTime();
            int age = 0;

            // 使用年龄 进行病例编辑
            if (!Channel.Default.SystemSetting.UseBirthdayEnable)
            {
                int.TryParse(PatientAgeTextBox.Text, out age);
                if (age > DateTime.Now.Year)
                {
                    AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Age input has exceeded the limit" : "年龄输入已超出限度");
                    return false;
                }
                birthDate = DateTime.Parse(string.Format("{0}-01-01", DateTime.Now.Year - age));
            }
            // 使用出生日期 进行病例编辑
            else
            {
                birthDate = Convert.ToDateTime(PatientAgeTextBox.Text);
                if (birthDate.Year > DateTime.Now.Year)
                {
                    AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "The date of birth cannot be later than the present" : "出生日期不得大于现在");
                    return false;
                }
                age = (DateTime.Now.Year - birthDate.Year);
            }


            //根据模式检查年龄
            int upperLimit = 0;
            switch((ModeType)Channel.Default.SystemSetting.ModeType)
            {
                case ModeType.Adult:
                    upperLimit = ADULT_AGE_UPPER_LIMIT;
                    break;
                case ModeType.Child:
                    upperLimit = CHILD_AGE_UPPER_LIMIT;
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }

            if (age < 1 || age > upperLimit)
            {
                string msg = string.Format(Program.Language == "EN" ? "{0}: Age must not be less than 1 year old or greater than {1} years old" : "{ 0}:年龄不得小于1岁或大于{1}岁",
                                            EnumHelper.GetDescription((ModeType)Channel.Default.SystemSetting.ModeType),
                                            upperLimit);
                AhDung.MessageTip.ShowWarning(msg);
                return false;
            }
            #endregion

            #region 检查身高和体重
            float h = 0;
            float.TryParse(HeightTextBox.Text, out h);
            float w = 0;
            float.TryParse(BodyWeightTextBox.Text, out w);
            if (!(h > 0 && h < 300))
            {
                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Height input error, maximum not exceeding 300cm" : "身高输入有误,最大不超过300cm");
                return false;
            }
            if (!(w > 0 && w < 400))
            {
                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Weight input error, maximum not exceeding 400kg" : "体重输入有误,最大不超过400kg");
                return false;
            }
            #endregion

            if (m_patientInfo == null)
            {
                m_edit = true;
                m_patientInfo = new Doc_PatientInfo();
                m_patientInfo.ModeType = Channel.Default.SystemSetting.ModeType;
            }
            if (m_edit || allowupdate)
            {
                m_patientInfo.Age = age;
                m_patientInfo.BirthDate = birthDate;
                m_patientInfo.Gender = genderMaleRadioButton.Checked ? (Program.Language == "EN" ? "Male" : "男") : (Program.Language == "EN" ? "Female" : "女");
                m_patientInfo.Weight = w;
                m_patientInfo.Height = h;
                m_patientInfo.PatientNo = no;
                m_patientInfo.PatientName = name;

                m_patientInfo.MobilePhone = MobileNumberTextBox.Text.Trim();
                m_patientInfo.Telephone = TelephoneNumberTextBox.Text;
                m_patientInfo.EmergencyPeople = EmergencyContactPersonTextBox.Text;
                m_patientInfo.EmergencyPhone = EmergencyContactPersonTelTextBox.Text;
                m_patientInfo.Address = FamilyAddressTextBox.Text;
            }
            DataModel.LogInstance.Default.AddLog(string.Format("病人信息为：病例号 {0}, 病人姓名 {1}, 年龄 {2}, 性别 {3}, 身高 {4}, 体重 {5}", m_patientInfo.PatientNo, m_patientInfo.PatientName, m_patientInfo.Age, m_patientInfo.Gender, m_patientInfo.Height, m_patientInfo.Weight),pSystem.LogManagement.LogLevel.DEBUG);
            return true;
        }
        #endregion

        #region 所有控件的绑定事件
        /// <summary>
        /// 当病例号的值改变时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientNoTextBox_TextChanged(object sender, EventArgs e)
        {
            if (fill)
            {
                fill = false;
                PatientNameTextBox.Text = string.Empty;

                //if (!Channel.Default.SystemSetting.UseBirthdayEnable)
                PatientAgeTextBox.Text = string.Empty;

                BodyWeightTextBox.Text = string.Empty;
                HeightTextBox.Text = string.Empty;
                genderMaleRadioButton.Checked = true;
                genderFemaleRadioButton.Checked = false;
                MobileNumberTextBox.Text = string.Empty;
                TelephoneNumberTextBox.Text = string.Empty;
                EmergencyContactPersonTextBox.Text = string.Empty;
                EmergencyContactPersonTelTextBox.Text = string.Empty;
                FamilyAddressTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// 当离开病例号输入框时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientNoTextBox_Leave(object sender, EventArgs e)
        {
            string patientno = PatientNoTextBox.Text.Trim();
            if (patientno != "")
            {
                Doc_PatientInfo patient = DataModel.DataBaseHelper.Default.Select(new Doc_PatientInfo() { PatientNo = patientno });
                if (patient != null)
                {
                    fillData(patient);
                    if (!m_edit)
                    {
                        AddButton.Enabled = false;
                        PatientNoTextBox.ReadOnly = false;
                    }
                }
                else
                {
                    if (!m_edit)
                        AddButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 取消按钮点击触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
            DataModel.LogInstance.Default.AddLog("点击 病人信息-取消 按钮");
        }

        /// <summary>
        /// 新增按钮点击触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("点击 病人信息-新增 按钮");
            if (SaveInfoHandle != null)
            {
                if (Check())
                {
                    if (SaveInfoHandle.Invoke(m_patientInfo))
                        this.Close();
                    DataModel.LogInstance.Default.AddLog("新增病例成功", pSystem.LogManagement.LogLevel.WARN);
                }
            }
        }

        /// <summary>
        /// 下一步按钮点击触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextStepButton_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                this.DialogResult = DialogResult.Cancel;
                Channel.Default.NextStep(m_patientInfo, true);
                DataModel.LogInstance.Default.AddLog("点击 病人信息-下一步 按钮");
            }
        }
        /// <summary>
        /// 病例号特殊符号限制输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataModel.InputTextCheck.filename_KeyPress(sender, e);
        }
        /// <summary>
        /// 姓名被键盘输入时，检查姓名的基本输入是否有误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataModel.InputTextCheck.name_KeyPress(sender, e);
        }

        /// <summary>
        /// 身高被键盘输入时，检查身高的基本输入是否有误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeightTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataModel.InputTextCheck.floatvalue_KeyPress(sender, e);
        }

        /// <summary>
        /// 体重被键盘输入时，检查体重的基本输入是否有误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BodyWeightTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataModel.InputTextCheck.floatvalue_KeyPress(sender, e);
        }

        /// <summary>
        /// 检查键盘输入是否一直都是数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check_KeyDown_AlwaysNumber(object sender, KeyPressEventArgs e)
        {
            DataModel.InputTextCheck.phone_KeyPress(sender, e);
        }
        #endregion
    }
}
