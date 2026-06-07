using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace AwareTec.Polysmith.DataMangement
{
    /// <summary>
    /// 病人信息处理单元
    /// </summary>
    public class PatientUnit
    {
        private static PatientUnit m_default = null;
        /// <summary>
        /// 单例模式
        /// </summary>
        public static PatientUnit Default
        {
            get
            {
                return m_default ?? (m_default = new PatientUnit());
            }
        }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName = "patient.pat";
        /// <summary>
        /// 存储
        /// </summary>
        /// <param name="Directory"></param>
        /// <param name="patient"></param>
        /// <returns></returns>
        public bool Write(string Directory, DataBaseCom.Doc_PatientInfo patient)
        {
            string patientPath = string.Format("{0}\\{1}", Directory, FileName);
            try
            {
                using (FileStream fs = new FileStream(patientPath, FileMode.Create, FileAccess.Write))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8))
                    {
                        string value = JsonConvert.SerializeObject(patient);
                        bw.Write(System.Text.Encoding.UTF8.GetBytes(value));
                        bw.Flush();
                        bw.Close();
                    }
                    fs.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="Directory"></param>
        /// <returns></returns>
        public DataBaseCom.Doc_PatientInfo Read(string Directory)
        {
            DataBaseCom.Doc_PatientInfo ret = null;
            string patientPath = string.Format("{0}\\{1}", Directory, FileName);
            if (File.Exists(patientPath))
            {
                using (FileStream fs = new FileStream(patientPath, FileMode.Open, FileAccess.Read))
                {
                    byte[] rangeBytes = new byte[fs.Length];
                    using (BinaryReader reader = new BinaryReader(fs, Encoding.UTF8))
                    {
                        byte[] buffer = new byte[1024];
                        int offset = 0;
                        while (true)
                        {
                            int readcount = reader.Read(buffer, 0, buffer.Length);
                            if (readcount > 0)
                            {
                                Array.Copy(buffer, 0, rangeBytes, offset, readcount);
                                offset += readcount;
                                if (offset == rangeBytes.Length)
                                {
                                    break;
                                }
                            }
                            else
                                break;
                        }
                        if (offset > 0)
                        {
                            try
                            {
                                ret = JsonConvert.DeserializeObject<DataBaseCom.Doc_PatientInfo>(System.Text.Encoding.UTF8.GetString(rangeBytes));
                            }
                            catch { }
                        }
                    }
                }
                File.Delete(patientPath);
            }
            return ret;
        }
    }

    /// <summary>
    /// 病人信息处理单元
    /// </summary>
    public class DoctorUnit
    {
        private static DoctorUnit m_default = null;
        /// <summary>
        /// 单例模式
        /// </summary>
        public static DoctorUnit Default
        {
            get
            {
                return m_default ?? (m_default = new DoctorUnit());
            }
        }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName = "doctor.dot";
        /// <summary>
        /// 存储
        /// </summary>
        /// <param name="Directory"></param>
        /// <param name="doctor"></param>
        /// <returns></returns>
        public bool Write(string Directory, DataBaseCom.Doc_Doctor doctor)
        {
            string patientPath = string.Format("{0}\\{1}", Directory, FileName);
            try
            {
                using (FileStream fs = new FileStream(patientPath, FileMode.Create, FileAccess.Write))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8))
                    {
                        string value = JsonConvert.SerializeObject(doctor);
                        bw.Write(System.Text.Encoding.UTF8.GetBytes(value));
                        bw.Flush();
                        bw.Close();
                    }
                    fs.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="Directory"></param>
        /// <returns></returns>
        public DataBaseCom.Doc_Doctor Read(string Directory)
        {
            DataBaseCom.Doc_Doctor ret = null;
            string patientPath = string.Format("{0}\\{1}", Directory, FileName);
            if (File.Exists(patientPath))
            {
                using (FileStream fs = new FileStream(patientPath, FileMode.Open, FileAccess.Read))
                {
                    byte[] rangeBytes = new byte[fs.Length];
                    using (BinaryReader reader = new BinaryReader(fs, Encoding.UTF8))
                    {
                        byte[] buffer = new byte[1024];
                        int offset = 0;
                        while (true)
                        {
                            int readcount = reader.Read(buffer, 0, buffer.Length);
                            if (readcount > 0)
                            {
                                Array.Copy(buffer, 0, rangeBytes, offset, readcount);
                                offset += readcount;
                                if (offset == rangeBytes.Length)
                                {
                                    break;
                                }
                            }
                            else
                                break;
                        }
                        if (offset > 0)
                        {
                            try
                            {
                                ret = JsonConvert.DeserializeObject<DataBaseCom.Doc_Doctor>(System.Text.Encoding.UTF8.GetString(rangeBytes));
                            }
                            catch { }
                        }
                    }
                }
                File.Delete(patientPath);
            }
            return ret;
        }
    }
}
