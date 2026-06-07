using System.Runtime.InteropServices;
using System.Text;

namespace AwareTec.Polysmith.DataCenter
{
    /// <summary>
    /// 
    /// </summary>
    public class iniFile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section">要读取的段落名</param>
        /// <param name="key">要读取的键</param>
        /// <param name="defVal">读取异常的情况下的缺省值</param>
        /// <param name="retVal">key所对应的值，如果该key不存在则返回空值</param>
        /// <param name="size">值允许的大小</param>
        /// <param name="filePath">INI文件的完整路径和文件名</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="section">要写入的段落名</param>
        /// <param name="key">要写入的键，如果该key存在则覆盖写入</param>
        /// <param name="val">key所对应的值</param>
        /// <param name="filePath">INI文件的完整路径和文件名</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

    }
}
