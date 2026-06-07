using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.UI.FunctionControls.tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.UI.Block
{
    public partial class DoctorEdit : SkinForm
    {
        #region 全局变量
        private string _DoctorPhotoPath = AppDomain.CurrentDomain.BaseDirectory + "Photo\\Doctor";
        bool doctorChange = true;
        private IEnumerable<DataBaseCom.Doc_Doctor> m_DoctorList = null;
        private string m_ImgPath = "";
        bool m_scanimg = false;
        #endregion

        #region 初始化相关

        /// <summary>
        /// 构造函数
        /// </summary>
        public DoctorEdit()
        {
            InitializeComponent();

            #region 为防止designer随意更改 解绑事件，因此事件绑定一律放置于逻辑代码的构造函数中
            this.Load += DoctorEdit_Load;

            this.LicenseNoTextBox.Leave += this.LicenseNoTextBox_Leave;
            this.LicenseNoTextBox.TextChanged += this.LicenseNoTextBox_TextChanged;
            this.DoctorNameComboBox.Leave += this.DoctorNameComboBox_Leave;
            this.DoctorNameComboBox.SelectedValueChanged += DoctorNameComboBox_SelectedValueChanged;
            this.UploadPhotosPictureBox.Click += this.UploadPhotos;
            this.UploadPhotosLabel.Click += this.UploadPhotos;
            this.LicenseNoTextBox.KeyPress += this.LicenseNoTextBox_KeyPress;
            this.DoctorNameComboBox.KeyPress += this.DoctorNameComboBox_KeyPress;
            this.ContactWayTextBox.KeyPress += this.ContactWayTextBox_KeyPress;
            this.DeleteButton.Click += this.DeleteButton_Click;
            this.CancelButton.Click += this.CancelButton_Click;
            this.AddAndSaveButton.Click += this.AddAndSaveButton_Click;
            #endregion


        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoctorEdit_Load(object sender, EventArgs e)
        {
            UpdateDoctors();
            DoctorNameComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            this.DoctorNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.DoctorNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            if (!Channel.Default.AllowShowDoctor)
            {
                DeleteButton.Visible = true;
                AddAndSaveButton.Text = Program.Language == "EN" ? "Save" : "新增";
                JobTitleComboBox.SelectedIndex = 0;
                BirthDateTimePicker.Value = DateTime.Now.AddYears(-20);
            }
            else
            {
                DeleteButton.Visible = false;
                DataBaseCom.Doc_Doctor _doctor = Channel.Default.Doctor;
                if (_doctor != null)
                {
                    LicenseNoTextBox.Text = _doctor.UserID;
                    DoctorNameComboBox.Text = _doctor.Name;
                    genderMaleRadioButton.Checked = _doctor.Gender.Equals(Program.Language == "EN" ? "Male" : "男");
                    genderFemaleRadioButton.Checked = _doctor.Gender.Equals(Program.Language == "EN" ? "Female" : "女");
                    BirthDateTimePicker.Value = DateTime.Parse(_doctor.Age);
                    JobTitleComboBox.Text = _doctor.Post;
                    ContactWayTextBox.Text = _doctor.Phone;
                    RemarksRichTextBox.Text = _doctor.Comments;
                    UnlockPasswordTextBox.Text = ConfirmPasswordTextBox.Text = _doctor.LockPsw;
                    if (!string.IsNullOrEmpty(_doctor.Photo) && File.Exists(_doctor.Photo))
                    {
                        UploadPhotosPictureBox.BackgroundImage = Image.FromFile(_doctor.Photo);
                        UploadPhotosLabel.Visible = false;
                    }
                    AddAndSaveButton.Text = Program.Language == "EN" ? "Save" : "保 存";
                    AddAndSaveButton.Tag = _doctor;
                }
                else
                {
                    JobTitleComboBox.SelectedIndex = 0;
                }
            }
        }

        #endregion

        #region 内部方法

        /// <summary>
        /// 根据医师姓名填充数据
        /// </summary>
        private void FillDataByName()
        {
            string value2 = DoctorNameComboBox.Text;
            string[] value = value2.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            if (value.Length > 1)
            {
                DoctorNameComboBox.Text = value[0];
                DataBaseCom.Doc_Doctor _doctor = DataModel.DataBaseHelper.Default.Select(new DataBaseCom.Doc_Doctor() { UserID = value[1] });
                if (_doctor != null)
                {
                    LicenseNoTextBox.Text = _doctor.UserID;
                    DoctorNameComboBox.Text = _doctor.Name;
                    genderMaleRadioButton.Checked = _doctor.Gender.Equals(Program.Language == "EN" ? "Male" : "男");
                    genderFemaleRadioButton.Checked = _doctor.Gender.Equals(Program.Language == "EN" ? "Female" : "女");
                    BirthDateTimePicker.Value = DateTime.Parse(_doctor.Age);
                    JobTitleComboBox.Text = _doctor.Post;
                    ContactWayTextBox.Text = _doctor.Phone;
                    RemarksRichTextBox.Text = _doctor.Comments;
                    UnlockPasswordTextBox.Text = ConfirmPasswordTextBox.Text = _doctor.LockPsw;
                    
                    if (!string.IsNullOrEmpty(_doctor.Photo) && File.Exists(_doctor.Photo))
                    {
                        UploadPhotosPictureBox.BackgroundImage = Image.FromFile(_doctor.Photo);
                        UploadPhotosLabel.Visible = false;
                    }
                    else
                    {
                        UploadPhotosPictureBox.BackgroundImage = null;
                        UploadPhotosLabel.Visible = true;
                    }

                    AddAndSaveButton.Text = Program.Language == "EN" ? "Save" : "保 存";
                    AddAndSaveButton.Tag = _doctor;
                }
            }
            doctorChange = false;
        }

        /// <summary>
        /// 更新所有医师，填充姓名这一下拉控件
        /// </summary>
        private void UpdateDoctors()
        {
            m_DoctorList = DataModel.DataBaseHelper.Default.Select<Doc_Doctor>().Where(t => t.ID != 1);
            string[] source = m_DoctorList.Select(t => string.Format("{0}({1})", t.Name, t.UserID)).ToArray();
            AutoCompleteStringCollection AutoCollection = new AutoCompleteStringCollection();
            AutoCollection.AddRange(source);
            DoctorNameComboBox.AutoCompleteCustomSource = AutoCollection;
            DoctorNameComboBox.Items.Clear();
            DoctorNameComboBox.Items.AddRange(source);
        }
        #endregion

        #region 所有控件的绑定事件

        /// <summary>
        /// 离开证件号控件时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LicenseNoTextBox_Leave(object sender, EventArgs e)
        {
            if (!Channel.Default.AllowShowDoctor)
            {
                string no = LicenseNoTextBox.Text.Trim();
                if (no != "" && doctorChange)
                {
                    DataBaseCom.Doc_Doctor _doctor = DataModel.DataBaseHelper.Default.Select(new DataBaseCom.Doc_Doctor() { UserID = no });
                    if (_doctor != null)
                    {
                        DoctorNameComboBox.Text = _doctor.Name;
                        genderMaleRadioButton.Checked = _doctor.Gender.Equals(Program.Language == "EN" ? "Male" : "男");
                        genderFemaleRadioButton.Checked = _doctor.Gender.Equals(Program.Language == "EN" ? "Female" : "女");
                        BirthDateTimePicker.Value = DateTime.Parse(_doctor.Age);
                        JobTitleComboBox.Text = _doctor.Post;
                        ContactWayTextBox.Text = _doctor.Phone;
                        RemarksRichTextBox.Text = _doctor.Comments;
                        UnlockPasswordTextBox.Text = ConfirmPasswordTextBox.Text = _doctor.LockPsw;
                        if (!string.IsNullOrEmpty(_doctor.Photo) && File.Exists(_doctor.Photo))
                        {
                            UploadPhotosPictureBox.BackgroundImage = Image.FromFile(_doctor.Photo);
                            UploadPhotosLabel.Visible = false;
                        }
                        else
                        {
                            UploadPhotosPictureBox.BackgroundImage = null;
                            UploadPhotosLabel.Visible = true;
                        }
                            
                        AddAndSaveButton.Text = Program.Language=="EN"?"Save":"保 存";
                        AddAndSaveButton.Tag = _doctor;
                    }
                    doctorChange = false;
                }
            }
        }

        /// <summary>
        /// 证件号控件的内容改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LicenseNoTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!doctorChange)
                doctorChange = true;
        }

        /// <summary>
        /// 离开医师姓名控件时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoctorNameComboBox_Leave(object sender, EventArgs e)
        {
            //FillDataByName();
            //矫正医师下拉框的Text
            string value2 = DoctorNameComboBox.Text;
            string[] value = value2.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            if (value.Length < 2)
                return;
            DoctorNameComboBox.Text = value[0];
        }

        /// <summary>
        /// 医师姓名控件的内容改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoctorNameComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            FillDataByName();
        }

        /// <summary>
        /// 取消按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("用户点击 医生信息-取消按钮");
            this.Close();
        }

        /// <summary>
        /// 保存或新增的按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAndSaveButton_Click(object sender, EventArgs e)
        {
            #region 检查用户输入
            string no = LicenseNoTextBox.Text.Trim();
            string name = DoctorNameComboBox.Text.Trim();
            string psw = UnlockPasswordTextBox.Text.Trim();
            string psw2 = ConfirmPasswordTextBox.Text.Trim();
            if (no == "")
            {
                AhDung.MessageTip.ShowWarning("证件号不能为空或者空格！");
                return;
            }
            if (name == "")
            {
                AhDung.MessageTip.ShowWarning("姓名不能为空或者空格！");
                return;
            }
            if (psw == "")
            {
                AhDung.MessageTip.ShowWarning("解锁密码不能为空或者空格！");
                return;
            }
            if (psw.Length < 4)
            {
                AhDung.MessageTip.ShowWarning("密码长度限定4-20个字符！");
                return;
            }
            if (psw != psw2)
            {
                AhDung.MessageTip.ShowError("两次输入的解锁密码不一致");
                return;
            }
            int age = (int)(DateTime.Now - BirthDateTimePicker.Value).TotalDays / 365;
            if (age < 1 || age > 90)
            {
                AhDung.MessageTip.ShowError("出生年月输入有误！");
                return;
            }
            #endregion

            DataBaseCom.Doc_Doctor _doctor = new DataBaseCom.Doc_Doctor()
            {
                Name = name,
                Gender = genderMaleRadioButton.Checked ? (Program.Language == "EN" ? "Male" : "男") : (Program.Language=="EN"?"Female": "女"),
                Age = BirthDateTimePicker.Value.ToString("yyyy-MM-dd"),
                Post = JobTitleComboBox.Text,
                Phone = ContactWayTextBox.Text,
                Comments = RemarksRichTextBox.Text,
                UserID = no,
                LockPsw = psw
            };
            if (AddAndSaveButton.Text == (Program.Language == "EN" ? "Add" : "新增"))
            {
                DataModel.LogInstance.Default.AddLog("用户点击 医生信息-新增按钮");
                if (m_ImgPath != "")
                {
                    _doctor.Photo = Path.Combine(_DoctorPhotoPath, string.Format("{0}{1}.{2}", name, no, Path.GetExtension(m_ImgPath)));
                    if (!Directory.Exists(_DoctorPhotoPath))
                        Directory.CreateDirectory(_DoctorPhotoPath);
                    File.Copy(m_ImgPath, _doctor.Photo);
                }
                if (DataModel.DataBaseHelper.Default.Insert(_doctor))
                {
                    AhDung.MessageTip.ShowOk("评分者新增成功");
                    DataModel.LogInstance.Default.AddLog(string.Format("评分者信息新增成功 新增评分人的证件号为 {0}，姓名为 {1}，密码为 {2}", _doctor.UserID, _doctor.Name, _doctor.LockPsw), pSystem.LogManagement.LogLevel.WARN);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    AhDung.MessageTip.ShowError("评分者新增失败");
                }
            }
            else
            {
                DataModel.LogInstance.Default.AddLog("用户点击 医生信息-保存按钮");
                DataBaseCom.Doc_Doctor doctor = AddAndSaveButton.Tag as DataBaseCom.Doc_Doctor;
                LockDialog lockdialog = new LockDialog();
                lockdialog.StartPosition = FormStartPosition.Manual;
                lockdialog.Location = Cursor.Position;
                lockdialog.PSW = doctor.LockPsw;
                if (lockdialog.ShowDialog() == DialogResult.OK)
                {
                    if (m_ImgPath != "")
                    {
                        _doctor.Photo = Path.Combine(_DoctorPhotoPath, string.Format("{0}{1}.{2}", name, no, Path.GetExtension(m_ImgPath)));
                        if (!Directory.Exists(_DoctorPhotoPath))
                            Directory.CreateDirectory(_DoctorPhotoPath);
                        File.Copy(m_ImgPath, _doctor.Photo, true);
                    }
                    if (DataModel.DataBaseHelper.Default.Update(new DataBaseCom.Doc_Doctor() { UserID = doctor.UserID }, _doctor))
                    {
                        Channel.Default.Doctor = _doctor;
                        AhDung.MessageTip.ShowOk("评分者信息修改成功");
                        DataModel.LogInstance.Default.AddLog(string.Format("评分者信息修改成功 修改评分人，姓名为 {0}，密码为 {1}", _doctor.Name, _doctor.LockPsw), pSystem.LogManagement.LogLevel.WARN);
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        AhDung.MessageTip.ShowError("评分者信息修改失败");
                    }
                }
            }
            UpdateDoctors();
        }

        /// <summary>
        /// 上传照片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UploadPhotos(object sender, EventArgs e)
        {
            if (!m_scanimg)
            {
                m_scanimg = true;
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "JPG文件|*.jpg|PNG文件|*.png|位图文件|*.bmp";
                open.RestoreDirectory = true;
                open.FilterIndex = 1;
                if (open.ShowDialog() == DialogResult.OK)
                {
                    UploadPhotosPictureBox.BackgroundImage = Image.FromFile(open.FileName);
                    m_ImgPath = open.FileName;
                    UploadPhotosLabel.Visible = false;
                }
                m_scanimg = false;
            }
        }

        /// <summary>
        /// 删除按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("用户点击 医生信息-删除按钮");
            if (Channel.Default.AllowShowDoctor)
            {
                AhDung.MessageTip.ShowWarning("处于工作模式的评分人不允许被删除！");
                return;
            }
            DataBaseCom.Doc_Doctor doctor = AddAndSaveButton.Tag as DataBaseCom.Doc_Doctor;
            if (doctor == null)
            {
                AhDung.MessageTip.ShowWarning("无评分人可供删除！");
                return;
            }
            if (doctor.ID == 1)
            {
                AhDung.MessageTip.ShowWarning("系统默认评分人不能被删除！");
                return;
            }
            if (DataModel.DataBaseHelper.Default.Exsit(new Doc_MainViewRecord() { DoctorID = doctor.ID.ToString() }))
            {
                AhDung.MessageTip.ShowWarning("评分人名下有评分记录不能被删除！");
                return;
            }
            LockDialog lockdialog = new LockDialog();
            lockdialog.StartPosition = FormStartPosition.Manual;
            lockdialog.Location = Cursor.Position;
            lockdialog.PSW = doctor.LockPsw;
            if (lockdialog.ShowDialog() == DialogResult.OK)
            {
                if (DataModel.DataBaseHelper.Default.Delete(new DataBaseCom.Doc_Doctor() { UserID = doctor.UserID }))
                {
                    LicenseNoTextBox.Text = DoctorNameComboBox.Text = string.Empty;
                    BirthDateTimePicker.Value = DateTime.Now;
                    JobTitleComboBox.Text = ContactWayTextBox.Text = RemarksRichTextBox.Text = UnlockPasswordTextBox.Text = ConfirmPasswordTextBox.Text = string.Empty;
                    AddAndSaveButton.Tag = null;
                    AddAndSaveButton.Text = Program.Language == "EN" ? "Save" : "新增";
                    UpdateDoctors();
                    AhDung.MessageTip.ShowOk("删除成功");
                    DataModel.LogInstance.Default.AddLog(string.Format("删除成功 删除的医生证件号为 {0}，姓名为 {1}", LicenseNoTextBox.Text, DoctorNameComboBox.Text), pSystem.LogManagement.LogLevel.WARN);
                    DialogResult = DialogResult.OK;
                }
            }
        }

        /// <summary>
        /// 姓名被键盘输入时，检查姓名的基本输入是否有误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoctorNameComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataModel.InputTextCheck.name_KeyPress(sender, e);
        }

        /// <summary>
        /// 联系方式被键盘输入时，检查联系方式的基本输入是否有误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContactWayTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataModel.InputTextCheck.phone_KeyPress(sender, e);
        }

        /// <summary>
        /// 证件号被键盘输入时，检查证件号的基本输入是否有误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LicenseNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = DataModel.InputTextCheck.CheckChar(e.KeyChar.ToString());
        }

        #endregion

        #region 其他

        /// <summary>
        /// 处理命令键
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    if (DoctorNameComboBox.Focused)
                    {
                        FillDataByName();
                        return true;
                    }
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        private void AddAndSaveButton_Click_1(object sender, EventArgs e)
        {

        }
    }
}
