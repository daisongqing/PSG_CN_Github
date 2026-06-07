using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AwareTec.Polysmith.DataMangement
{
    /// <summary>
    /// 单元处理基类
    /// </summary>
    public class IUnit : IDisposable
    {
        #region 私有成员
        private string m_BasePath = "";
        private string m_GUID = "";
        /// <summary>
        /// 队列锁
        /// </summary>
        private object m_lockObj = new object();
        private string m_EntrypKey = "pengdowork";
        private string m_keyname = "Location";
        #endregion
        #region 保护成员
        /// <summary>
        /// 二进制写入流
        /// </summary>
        protected BinaryWriter m_writer = null;
        /// <summary>
        /// 文件存储路径
        /// </summary>
        protected string m_savePath = "";
        /// <summary>
        /// 文件另存为地址
        /// </summary>
        protected string m_saveasPath = "";
        /// <summary>
        /// 创建写入流
        /// </summary>
        /// <returns></returns>
        protected BinaryWriter CreatWriter(string m_savePath)
        {
            BinaryWriter ret = null;
            if (m_savePath == "")
                return ret;
            if (!File.Exists(m_savePath))
            {
                string dir = Path.GetDirectoryName(m_savePath);
                Directory.CreateDirectory(dir);
            }
            ret = new BinaryWriter(new FileStream(m_savePath, FileMode.Create, FileAccess.Write), Encoding.ASCII);
            return ret;
        }
        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="m_savePath"></param>
        /// <returns></returns>
        protected string ReadData()
        {
            if (m_savePath == "")
                return "";
            if (!File.Exists(m_savePath))
            {
                return "";
            }
            using (FileStream fs = new FileStream(m_savePath, FileMode.Open, FileAccess.Read))
            {
                byte[] rangeBytes = new byte[fs.Length];
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    byte[] buffer = new byte[1024];
                    int offset = 0;
                    while (true)
                    {
                        try
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
                        catch
                        {
                            break;
                        }
                    }
                    if (offset > 0)
                    {
                        return System.Text.Encoding.Default.GetString(rangeBytes);
                    }
                    return "";
                }
            }
        }
        //加密
        protected byte[] Encryption(byte[] plaindata)
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = m_EntrypKey;//密匙容器的名称，保持加密解密一致才能解密成功
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                return encryptdata;//将加密后的字节数组转换为字符串
            }
        }

        //解密
        protected byte[] Decrypt(byte[] encryptdata)
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = m_EntrypKey;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                return decryptdata;
            }
        }
        #endregion
        #region 公有成员
        /// <summary>
        /// 数据与结果是否保存路径一致
        /// </summary>
        internal bool DataAnResult
        {
            set
            {
                if (value)
                    m_keyname = "";
            }
        }
        /// <summary>
        /// 设置基础路径
        /// </summary>
        internal string BasePath
        {
            set
            {
                if (value == "")
                    m_BasePath = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, m_keyname);
                else
                    m_BasePath = string.Format("{0}\\{1}", value, m_keyname);
            }
        }
        /// <summary>
        /// 获取或设置GUID
        /// </summary>
        public string GUID
        {
            set
            {
                m_GUID = value;
                if (m_BasePath == "")
                    m_BasePath = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, m_keyname);
                m_savePath = string.Format("{0}\\{1}\\{2}", m_BasePath, m_GUID, FileName);
                m_saveasPath = string.Format("{0}\\{1}\\Back{2}", m_BasePath, m_GUID, FileName);
            }
            get
            {
                return m_GUID;
            }
        }
        /// <summary>
        /// 获取文件名
        /// </summary>
        public string FileName
        {
            protected set;
            get;
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose()
        {
            if (m_writer != null)
            {
                m_writer.Flush();
                m_writer.Close();
                m_writer.Dispose();
                m_writer = null;
            }
        }
        #endregion
    }
}
