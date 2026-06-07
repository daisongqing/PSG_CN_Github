using System;
using System.Globalization;
using System.IO;
using System.Management;
using System.Text;

namespace pSystem.UI.Register
{
    /// <summary>
    /// 加密内容含括模式
    /// 0、硬盘；1、CPU；2、两者皆可
    /// </summary>
    public enum emVerifyMode
    {
        /// <summary>
        /// 仅包含硬盘序列号
        /// </summary>
        HardDisk = 0,
        /// <summary>
        /// 仅包含cpu序列号
        /// </summary>
        CPU,
        /// <summary>
        /// 如果都能获取到，则两者都包含，否则自适应
        /// </summary>
        Both,
        /// <summary>
        /// 无
        /// </summary>
        NoAll
    }
    public enum RegDataType
    {
        /// <summary>
        /// 验证信息
        /// </summary>
        Crack = 1,
        /// <summary>
        /// 最后使用日期
        /// </summary>
        LastDate = 2,
        /// <summary>
        /// 注册码
        /// </summary>
        RegInfo = 3,
        /// <summary>
        /// 注册时间
        /// </summary>
        RegDateTime = 4
    }
    /// <summary>
    /// 获取CPU序列号、对字符串进行加密等
    /// </summary>
    public class Reg
    {
        string encryptKey;//加密解密Key
        emVerifyMode verifyMode = emVerifyMode.HardDisk;
        private string m_regPath = "";
        private string m_fileName = "RegCNPhysio.xml";
        private UserConfigXML m_xmlCreat = null;
        //记录注册时间的注册表位置
        private string[] RegDatePath = new string[] { "SOFTWARE", "CNTenLaterA", "RDT" };
        //记录上次使用时间的注册表位置
        private string[] LastDatePath = new string[] { "SOFTWARE", "CNTenLaterA", "LDT" };
        //记录破解项的注册表位置
        private string[] CrackPath = new string[] { "SOFTWARE", "CNTenLaterA", "CRK" };
        //记录注册码的注册表位置
        private string[] RegPath = new string[] { "SOFTWARE", "CNTenLaterA", "REG" };
        #region 公有成员
        /// <summary>
        /// 设置主节点注册名
        /// </summary>
        /// <param name="KeyName"></param>
        public void SetRegistKey(string KeyName)
        {
            if (KeyName == "CNTenLater")///临时应付用，为了纠正以前的版本
            {
                RegTableHelper.SetReplaceKey("CNPhysio");
            }
            RegDatePath[1] = LastDatePath[1] = CrackPath[1] = RegPath[1] = KeyName;
        }
        //100F未注册,101F失败,20LL成功（低权限）,20HL成功（高权限）,102F试用期结束,新增只写功能
        public string[] RegRlts = new string[] { "100F", "101F", "20LL", "20HL", "102F", "20WL" };
        //破解项状态
        public string[] CrackState = new string[] { "safe", "warn", "over" };
        /// <summary>
        /// 信息注册路径
        /// </summary>
        public string RegisterPath
        {
            set
            {
                m_regPath = value.Trim();
                if (m_regPath != "")
                {
                    m_regPath = Path.Combine(m_regPath, m_fileName);
                    m_xmlCreat = new UserConfigXML(m_regPath);
                }
            }
            get
            {
                return m_regPath.Substring(0, m_regPath.LastIndexOf('\\'));
            }
        }
        /// <summary>
        /// 加密解密key
        /// </summary>
        public string EncryptKey
        {
            get
            {
                return encryptKey;
            }
            set
            {
                if (encryptKey == value)
                    return;
                if (value.Length < 16)
                {
                    value = value.PadRight(16, '0');
                }
                else if (value.Length > 16)
                {
                    value = value.Substring(0, 16);
                }
                encryptKey = value;
            }
        }
        /// <summary>
        /// 如果当前计算机不支持当前模式，会对其进行修改，最后应该返回回去进行回写
        /// 软件默认读硬盘，读不了再去读CPU；
        /// </summary>
        public emVerifyMode VerifyMode
        {
            get
            {
                return verifyMode;
            }
            set
            {
                if (verifyMode == value)
                    return;
                verifyMode = value;
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">秘钥 必须大于等于8个字符长度</param>
        /// <param name="IsSaveToRegTable">false时需要指定RegisterPath的值即自定义注册信息保存路径</param>
        public Reg(string key, bool IsSaveToRegTable = true)
        {
            EncryptKey = key;
            m_IsSaveToRegTab = IsSaveToRegTable;
        }
        /// <summary>
        /// 获取机器码
        /// </summary>
        /// <returns></returns>
        public string getMachineID()
        {
            switch (verifyMode)
            {
                case emVerifyMode.HardDisk:
                    return GetDiskID();
                case emVerifyMode.CPU:
                    return GetCpuID();
                case emVerifyMode.Both:
                    return GetDiskID() + GetCpuID();
                default:
                    return GetCpuID();
            }
        }
        /// <summary>
        /// 获取主板的ID
        /// </summary>
        /// <returns></returns>
        private string getBoardID()
        {
            ManagementClass mc = new ManagementClass("Win32_BaseBoard");
            ManagementObjectCollection moc = mc.GetInstances();
            string strID = "";
            foreach (ManagementObject mo in moc)
            {
                strID = mo.Properties["SerialNumber"].Value.ToString();
                break;
            }
            return strID;
        }
        /// <summary>
        /// 获得CPU的序列号
        /// </summary>
        /// <returns></returns>
        private string GetCpuID()
        {
            try
            {
                string szCpu = null;
                ManagementClass myCpu = new ManagementClass("win32_Processor");
                ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
                foreach (ManagementObject myObject in myCpuConnection)
                {
                    szCpu = myObject.Properties["Processorid"].Value.ToString().Trim();
                    break;
                }
                return szCpu;
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// 取得设备硬盘的卷标号
        /// </summary>
        /// <returns></returns>
        private string GetDiskID()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString().Trim();
        }
        /// <summary>
        /// 进行注册——关键
        /// </summary>
        /// <param name="reg">加密后的注册信息</param>
        /// <returns>注册是否成功</returns>
        public bool WriteReg(string reg)
        {
            if (!m_IsSaveToRegTab)
            {
                if (m_regPath == "")
                    return false;
                else
                {
                    using (StreamWriter sw = new StreamWriter(m_regPath))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n  <{0}>\r\n<{1} Value=\"\" />\r\n  <{2} Value=\"\" />\r\n  <{3} Value=\"\" />\r\n  <{4} Value=\"\" />\r\n</{0}>",
                            RegPath[1], RegDataType.RegInfo.ToString(), RegDataType.RegDateTime.ToString(), RegDataType.LastDate.ToString(), RegDataType.Crack.ToString());
                        sw.WriteLine(sb);
                        sw.Close();
                    }
                }
            }
            //解析注册码
            string szDecrypt = EncryptDecrypt.AESDecrypt(reg, encryptKey);
            string szRegID = szDecrypt.Substring(2, szDecrypt.Length - 10);
            string szDays = szDecrypt.Substring(szDecrypt.Length - 8, 6);//使用的天数
            string szLevel = szDecrypt.Substring(0, 2) + szDecrypt.Substring(szDecrypt.Length - 3, 2);
            //读取注册表
            string szRegTbl = GetRegTbl();
            bool add = false;
            string oldReg = "";
            string[] strValues = szRegID.Split('/');
            szRegID = strValues[0];
            if (szRegTbl != "")
            {
                oldReg = EncryptDecrypt.AESDecrypt(szRegTbl, encryptKey);
                if (oldReg != "")
                {
                    if (strValues.Length > 1)
                    {
                        string oldRegID = oldReg.Substring(2, oldReg.Length - 10);
                        string[] oldregs = oldRegID.Split('/');
                        if (oldregs.Length > 1)
                        {
                            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                            dtFormat.ShortDatePattern = "yyyyMMddHHmmss";

                            //todo
                            //DateTime dateTime;
                            DateTime newtime = DateTime.Now;
                            DateTime oldtime = DateTime.Now;
                            try
                            {
                                newtime = DateTime.ParseExact(strValues[1], "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                                oldtime = DateTime.ParseExact(oldregs[1], "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            catch (Exception ee)
                            {
                                Console.WriteLine(ee.Message);
                                return false;
                            }
                            //if (!DateTime.TryParse(oldregs[1], out dateTime))
                            //    return false;
                            // if (Convert.ToDateTime(strValues[1], dtFormat) <= Convert.ToDateTime(oldregs[1], dtFormat))
                            if (newtime <= oldtime)
                            {
                                return false;
                            }
                            else
                            {
                                add = true;
                            }
                        }
                        else
                            add = true;
                    }
                    else
                    {
                        if (szRegTbl != "")
                        {
                            if (reg == szRegTbl)//试用注册码已经使用
                            {
                                return false;
                            }
                            else
                            {
                                int currentdays = int.Parse(oldReg.Substring(oldReg.Length - 8, 6));//使用的天数
                                if (int.Parse(szDays) < currentdays)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    //对高手删除注册表中的相应注册项而进行破解暂时不考虑——关键
                    if (reg == szRegTbl)//试用注册码已经使用
                    {
                        return false;
                    }
                }
            }
            bool bIsSuccess = false;
            switch (verifyMode)
            {
                case emVerifyMode.HardDisk:
                    if (szRegID == GetDiskID())
                    {
                        bIsSuccess = true;
                    }
                    break;
                case emVerifyMode.CPU:
                    if (szRegID == GetCpuID())
                    {
                        bIsSuccess = true;
                    }
                    break;
                case emVerifyMode.Both:
                    {
                        Reg rg = new Reg(encryptKey);
                        string szHardDisk = rg.GetDiskID().Trim();
                        string szMachCode = "";
                        if (szHardDisk.Length <= 0)//不支持disk c读取
                        {
                            verifyMode = emVerifyMode.CPU;
                            goto case emVerifyMode.CPU;
                        }
                        else
                        {
                            szMachCode = szHardDisk;
                        }

                        string szCpuID = rg.GetCpuID().Trim();
                        if (szCpuID.Length <= 0)
                        {
                            verifyMode = emVerifyMode.HardDisk;
                        }
                        else
                        {
                            szMachCode += szCpuID;

                        }
                        if (szRegID == szMachCode)
                        {
                            bIsSuccess = true;
                        }
                        else { bIsSuccess = false; }
                    }
                    break;
            }
            if (bIsSuccess)
            {
                if (add)
                {
                    string newreg = CreateRegInfo(szRegID, 2, int.Parse(szDays) + int.Parse(oldReg.Substring(oldReg.Length - 8, 6)),true);
                    reg = newreg;
                    //reg = EncryptDecrypt.AESEncrypt(CreateRegInfo(szRegID, 2, int.Parse(szDays) + int.Parse(oldReg.Substring(oldReg.Length - 8, 6)), true), encryptKey);
                }
                else
                {
                    WriteRegDate();
                    WriteLastDate();
                    WriteCrack(CrackState[0]);
                }
                WriteRegTbl(reg);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 最大使用天数
        /// </summary>
        public int UseDays { get; private set; }
        /// <summary>
        /// 验证注册码信息 并获得权限信息——关键
        /// </summary>
        /// <returns>100F未注册,101F失败,20LL成功（低权限）,20HL成功（高权限）,102F试用期结束</returns>
        public string VerifyReg()
        {
            if (!m_IsSaveToRegTab)
            {
                if (m_regPath == "")
                    return RegRlts[0];
                else if (!File.Exists(m_regPath))
                {
                    return RegRlts[0];
                }
            }
            string szRegTbl = GetRegTbl();
            if (szRegTbl.Length <= 0)
            {
                return RegRlts[0];
            }
            else//说明已经注册过
            {
                //解析注册码
                string szDecrypt = EncryptDecrypt.AESDecrypt(szRegTbl, encryptKey);
                if(string.IsNullOrEmpty( szDecrypt))
                    return RegRlts[0];
                string szRegID = szDecrypt.Substring(2, szDecrypt.Length - 10).Split('/')[0];
                string szDays = szDecrypt.Substring(szDecrypt.Length - 8, 6).Trim();//使用的天数
                string szLevel = szDecrypt.Substring(0, 2) + szDecrypt.Substring(szDecrypt.Length - 2, 2);
                int szDaysOK;
                if (!int.TryParse(szDays, out szDaysOK))
                    return RegRlts[1];
                UseDays = int.Parse(szDays);
                //
                //读取注册表
                string szTmp1 = GetRegDate();
                string szTmp2 = GetLastDate();
                string szTmp3 = GetCrack();
                string szRegDate = string.Empty;
                string szLastDate = string.Empty;
                string szCreak = string.Empty;
                if (szTmp1.Length <= 0 || szTmp2.Length <= 0 || szTmp3.Length <= 0)
                {
                    WriteCrack(EncryptDecrypt.AESEncrypt(CrackState[1], encryptKey));
                    return RegRlts[1];
                }
                szRegDate = szTmp1;
                szLastDate = szTmp2;
                szCreak = szTmp3;
                DateTime nowDate = DateTime.Today;//时间格式默认
                DateTime regDate = Convert.ToDateTime(szRegDate);
                DateTime lastDate = Convert.ToDateTime(szLastDate);
                if (UseDays <= 30)//试用版
                {
                    if (szCreak == CrackState[1] || szCreak == CrackState[2])//是否为warn或over
                    {
                        return RegRlts[1];
                    }
                    if (DateTime.Compare(nowDate, lastDate) < 0)//当前时间小于上次使用时间，已经被改动时间，有破解嫌疑
                    {
                        WriteCrack(EncryptDecrypt.AESEncrypt(CrackState[1], encryptKey));//warn
                        return RegRlts[1];
                    }
                }
                TimeSpan spanDays = nowDate - regDate;
                int nSpanDays = spanDays.Days;
                if (nSpanDays > UseDays)
                {
                    WriteCrack(EncryptDecrypt.AESEncrypt(CrackState[2], encryptKey));//over
                    return RegRlts[1];
                }
                //检查注册文件，返回注册权限
                switch (verifyMode)
                {
                    case emVerifyMode.HardDisk:
                        if (szRegID == GetDiskID())
                        {
                            WriteLastDate();
                            return szLevel;
                        }
                        else
                        {
                            WriteCrack(EncryptDecrypt.AESEncrypt(CrackState[1], encryptKey));//warn
                            return RegRlts[1];
                        }
                    case emVerifyMode.CPU:
                        if (szRegID == GetCpuID())
                        {
                            WriteLastDate();
                            return szLevel;
                        }
                        else
                        {
                            WriteCrack(EncryptDecrypt.AESEncrypt(CrackState[1], encryptKey));//warn
                            return RegRlts[1];
                        }
                    case emVerifyMode.Both:
                        if (szRegID == GetDiskID() + GetCpuID())
                        {
                            WriteLastDate();
                            return szLevel;
                        }
                        else
                        {
                            WriteCrack(EncryptDecrypt.AESEncrypt(CrackState[1], encryptKey));//warn
                            return RegRlts[1];
                        }
                    default:
                        return RegRlts[1];
                }
            }
        }
        /// <summary>
        /// 公司使用——用来生成注册码
        /// </summary>
        /// <param name="machineCode">机器码</param>
        /// <param name="level">权限码</param>
        /// <param name="days">使用天数999999</param>
        /// <returns>成功返回注册码，失败返回空</returns>
        public string CreateRegInfo(string machineCode, int level, int days, bool along = false)
        {
            if (days <= 0 || days > 999999)
            {
                return "";
            }
            string szDays = days.ToString().PadLeft(6, '0');
            string[] szLevels;
            switch (level)
            {
                case 0://只读
                    szLevels = new string[] { RegRlts[2].Substring(0, 2), RegRlts[2].Substring(2, 2) };
                    break;
                case 1://读写
                    szLevels = new string[] { RegRlts[3].Substring(0, 2), RegRlts[3].Substring(2, 2) };
                    break;
                case 2://只写
                    szLevels = new string[] { RegRlts[5].Substring(0, 2), RegRlts[5].Substring(2, 2) };
                    break;
                default:
                    return "";
            }
            string szReg = EncryptDecrypt.AESEncrypt(string.Format("{0}{1}{4}{2}{3}", szLevels[0], machineCode, szDays, szLevels[1], along ? string.Format("/{0:yyyyMMddHHmmss}", DateTime.Now) : ""), encryptKey);
            return szReg;
        }
        #endregion
        #region 私有成员
        private bool m_IsSaveToRegTab = true;
        /// <summary>
        /// 从自定义文件中读出相应键值
        /// </summary>
        /// <param name="typ"></param>
        /// <returns></returns>
        private string Read(RegDataType typ)
        {
            return m_xmlCreat.Get(typ.ToString());
        }
        /// <summary>
        /// 把相应信息写入自定义文件
        /// </summary>
        /// <param name="typ"></param>
        /// <param name="value"></param>
        private void Write(RegDataType typ, string value)
        {
            m_xmlCreat.Set(typ.ToString(), value);
        }
        /// <summary>
        /// 写注册日期
        /// </summary>
        private void WriteRegDate()
        {
            string szDate = DateTime.Today.ToString("G");
            string szEncrypt = EncryptDecrypt.AESEncrypt(szDate, encryptKey);
            if (m_IsSaveToRegTab)
                RegTableHelper.WTRegedit(RegDatePath, szEncrypt);
            else
            {
                if (m_regPath != "")
                {
                    Write(RegDataType.RegDateTime, szEncrypt);
                }
            }
        }
        /// <summary>
        /// 获取注册日期
        /// </summary>
        /// <returns>返回注册日期（解密后），如果为空，说明有问题，注册表被删，极有可能客户在尝试破解</returns>
        private string GetRegDate()
        {
            string szEncrypt = "";
            if (m_IsSaveToRegTab)
            {
                if (!RegTableHelper.IsRegeditExit(RegDatePath))
                {
                    return "";
                }
                else
                {
                    szEncrypt = RegTableHelper.GetRegistData(RegDatePath);
                }
            }
            else
            {
                if (m_regPath != "")
                {
                    szEncrypt = Read(RegDataType.RegDateTime);
                }
                if (szEncrypt == "")
                    return "";
            }
            return EncryptDecrypt.AESDecrypt(szEncrypt, encryptKey);
        }
        /// <summary>
        /// 写本次使用日期，为下次验证使用
        /// </summary>
        private void WriteLastDate()
        {
            string szDate = DateTime.Today.ToString("yyyy-MM-dd");
            string szEncrypt = EncryptDecrypt.AESEncrypt(szDate, encryptKey);
            if (m_IsSaveToRegTab)
                RegTableHelper.WTRegedit(LastDatePath, szEncrypt);
            else
            {
                if (m_regPath != "")
                {
                    Write(RegDataType.LastDate, szEncrypt);
                }
            }
        }
        /// <summary>
        /// 获取上次使用日期
        /// </summary>
        /// <returns>返回上次使用日期（解密后），如果为空，说明有问题，注册表被删，极有可能客户在尝试破解</returns>
        private string GetLastDate()
        {
            string szEncrypt = "";
            if (m_IsSaveToRegTab)
            {
                if (!RegTableHelper.IsRegeditExit(LastDatePath))
                {
                    return "";
                }
                else
                {
                    szEncrypt = RegTableHelper.GetRegistData(LastDatePath);
                }
            }
            else
            {
                if (m_regPath != "")
                {
                    szEncrypt = Read(RegDataType.LastDate);
                }
                if (szEncrypt == "")
                    return "";
            }
            return EncryptDecrypt.AESDecrypt(szEncrypt, encryptKey);
        }
        /// <summary>
        /// 写Crack数据,验证使用
        /// </summary>
        /// <param name="reg">使用标记safe、安全；warn、破解嫌疑；over、试用超期</param>
        private void WriteCrack(string flag)
        {
            string szEncrypt = EncryptDecrypt.AESEncrypt(flag, encryptKey);
            if (m_IsSaveToRegTab)
                RegTableHelper.WTRegedit(CrackPath, szEncrypt);
            else
            {
                if (m_regPath != "")
                {
                    Write(RegDataType.Crack, szEncrypt);
                }
            }
        }
        /// <summary>
        /// 获取Crack数据
        /// </summary>
        /// <returns>返回标记safe、安全；warn、破解嫌疑；over、试用超期；如果为空，说明有问题，注册表被删，极有可能客户在尝试破解</returns>
        private string GetCrack()
        {
            string szEncrypt = "";
            if (m_IsSaveToRegTab)
            {
                if (!RegTableHelper.IsRegeditExit(CrackPath))
                {
                    return "";
                }
                else
                {
                    szEncrypt = RegTableHelper.GetRegistData(CrackPath);
                }
            }
            else
            {
                if (m_regPath != "")
                {
                    szEncrypt = Read(RegDataType.Crack);
                }
                if (szEncrypt == "")
                    return "";
            }
            return EncryptDecrypt.AESDecrypt(szEncrypt, encryptKey);
        }
        /// <summary>
        /// 写reg数据
        /// </summary>
        /// <param name="reg">已经加密的注册码</param>
        private void WriteRegTbl(string reg)
        {
            if (m_IsSaveToRegTab)
                RegTableHelper.WTRegedit(RegPath, reg);
            else
            {
                if (m_regPath != "")
                {
                    Write(RegDataType.RegInfo, reg);
                }
            }
        }
        /// <summary>
        /// 获取reg数据
        /// </summary>
        /// <returns>加密后的reg数据</returns>
        private string GetRegTbl()
        {
            string szEncrypt = "";
            if (m_IsSaveToRegTab)
            {
                if (!RegTableHelper.IsRegeditExit(RegPath))
                {
                    return "";
                }
                else
                {
                    szEncrypt = RegTableHelper.GetRegistData(RegPath);
                }
            }
            else
            {
                if (m_regPath != "")
                {
                    szEncrypt = Read(RegDataType.RegInfo);
                }
                if (szEncrypt == "")
                    return "";
            }
            return szEncrypt;
        }
        #endregion
    }
}
