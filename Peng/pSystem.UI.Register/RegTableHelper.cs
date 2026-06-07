using Microsoft.Win32;
using System;

namespace pSystem.UI.Register
{
    /// <summary>
    /// 注册表操作类LocalMachine下面的内容
    /// </summary>
    public class RegTableHelper
    {
        private static string m_ReplaceKey = "";
        /// <summary>
        /// 临时兼容
        /// </summary>
        /// <param name="rpKey"></param>
        public static void SetReplaceKey(string rpKey)
        {
            m_ReplaceKey = rpKey;
        }
        /// <summary>
        /// 读取指定名称的注册表的值"以上是读取的注册表中HKEY_LOCAL_MACHINE\SOFTWARE目录下的XXX目录中名称为name的注册表值"
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string GetRegistData(string[] names)
        {
            string registData;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey(names[0], true);//"SOFTWARE"
            RegistryKey aimdir = software.OpenSubKey(names[1], true);//"XXX"
            if (aimdir == null)
            {
                if (m_ReplaceKey == "")
                    return "";
                aimdir = software.OpenSubKey(m_ReplaceKey);
                if (aimdir == null)
                    return "";
            }
            registData = aimdir.GetValue(names[2]).ToString();
            return registData;
        }
        /// <summary>
        /// 向注册表中写数据"以上是在注册表中HKEY_LOCAL_MACHINE\SOFTWARE目录下新建XXX目录并在此目录下创建名称为name值为tovalue的注册表项"
        /// </summary>
        /// <param name="names"></param>
        /// <param name="tovalue"></param>
        public static void WTRegedit(string[] names, string tovalue)
        {
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey software = hklm.OpenSubKey(names[0], true);//"SOFTWARE"
            RegistryKey aimdir = software.OpenSubKey(names[1], true);
            if (aimdir == null)
            {
                if (m_ReplaceKey != "")
                {
                    aimdir = software.OpenSubKey(m_ReplaceKey, true);
                }
                if (aimdir == null)
                    aimdir = software.CreateSubKey(names[1]);//"XXX"
            }
            aimdir.SetValue(names[2], tovalue);
        }
        /// <summary>
        /// 删除注册表中指定的注册表项"以上是在注册表中HKEY_LOCAL_MACHINE\SOFTWARE目录下XXX目录中删除名称为name注册表项"
        /// </summary>
        /// <param name="names"></param>
        public static void DeleteRegist(string[] names)
        {
            string[] aimnames;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey(names[0], true);
            RegistryKey aimdir = software.OpenSubKey(names[1], true);
            if (aimdir == null)
            {
                aimdir = software.OpenSubKey(m_ReplaceKey);
            }
            aimnames = aimdir.GetSubKeyNames();
            foreach (string aimKey in aimnames)
            {
                if (aimKey == names[2])
                    aimdir.DeleteSubKeyTree(names[2]);
            }
        }
        /// <summary>
        /// 判断指定注册表项是否存在
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static bool IsRegeditExit(string[] names)
        {
            bool _exit = false;
            try
            {
                string[] subkeyNames;
                RegistryKey hkml = Registry.LocalMachine;
                RegistryKey software = hkml.OpenSubKey(names[0], true);
                RegistryKey aimdir = software.OpenSubKey(names[1], true);
                if (aimdir == null)
                {
                    aimdir = software.OpenSubKey(m_ReplaceKey);
                    if (aimdir == null)
                    {
                        return _exit;
                    }
                }
                subkeyNames = aimdir.GetValueNames();
                foreach (string keyName in subkeyNames)
                {
                    if (keyName == names[2])
                    {
                        _exit = true;
                        return _exit;
                    }
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            return _exit;
        }

    }
}
