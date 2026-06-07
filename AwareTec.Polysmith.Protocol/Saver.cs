using System;
using System.IO;
using System.Text;
namespace AwareTec.Polysmith.Protocol
{
    /// <summary>
    /// 保存数据项
    /// </summary>
    public class Saver
    {
        /// <summary>
        /// 病历ID
        /// </summary>
        public string ID { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        private string m_Name = "";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            set
            {
                m_Name = value;
            }
            get
            {
                return m_Name;
            }
        }
        private int m_TotalCnt = 2000;
        /// <summary>
        /// 累计写入最大数量，到达最大数量后即写入文件
        /// </summary>
        public int TotalCnt
        {
            set
            {
                m_TotalCnt = value;
            }
        }
        /// <summary>
        /// 设置存储数据的基础目录
        /// </summary>
        public string BaseSavePath
        {
            set
            {
                m_basePath = value;
            }
        }
        private string m_basePath = AppDomain.CurrentDomain.BaseDirectory + "\\Data";
        private string m_Path = "";
        private string m_Time = "";
        private StreamWriter sw = null;
        private StringBuilder m_buffer = new StringBuilder();
        private int m_writeCnt = 0;
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="userData"></param>
        public void Save(string data)
        {
            if (string.IsNullOrEmpty(m_Time))
            {
                try
                {
                    m_Time = DateTime.Now.ToString("yyyyMMdd");
                    if (!System.IO.File.Exists(m_basePath))
                    {
                        Directory.CreateDirectory(m_basePath);
                    }
                    m_Path = string.Format("{0}\\{1:yyyy-MM-dd}", m_basePath, DateTime.Now);
                    if (!System.IO.File.Exists(m_Path))
                    {
                        Directory.CreateDirectory(m_Path);
                    }
                    m_Path = string.Format("{0}\\{1}", m_Path, string.Format("{0}_{1}.dat", m_Name, ID));
                    sw = new StreamWriter(m_Path, false, Encoding.UTF8);
                    sw.AutoFlush = true;
                }
                catch (Exception ee)
                {

                }
            }
            m_buffer.Append(data);
            if (m_writeCnt == m_TotalCnt)
            {
                if (sw != null)
                    sw.WriteAsync(m_buffer.ToString());
                m_buffer.Clear();
                m_writeCnt = 0;
            }
            else
                m_writeCnt++;
        }
        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            m_Time = "";
            if (sw != null)
            {
                string str = m_buffer.ToString();
                if (str.Length > 0)
                {
                    try
                    {
                        sw.Write(str);
                    }
                    catch { }
                    m_buffer.Clear();
                }
                m_writeCnt = 0;
                sw.Flush();
                sw.Close();
                sw.Dispose();
                sw = null;
            }
        }
    }
}
