using System;
using System.Collections.Generic;
using System.IO;
using System.Management;

namespace AwareTec.Polysmith.Util
{
    /// <summary>
    /// 磁盘信息 单例模式
    /// </summary>
    public class HardDiskMation
    {
        private static HardDiskMation m_Default = null;
        /// <summary>
        /// 单例模式
        /// </summary>
        public static HardDiskMation Default
        {
            get
            {
                return m_Default ?? (m_Default = new HardDiskMation());
            }
        }

        /// 
        /// 盘符信息
        /// 
        public class HardDiskPartition
        {
            #region Data
            private string _PartitionName;
            private double _FreeSpace;
            private double _SumSpace;
            #endregion //Data

            #region Properties
            /// <summary>
            /// 空余大小
            /// </summary>
            public double FreeSpace
            {
                get { return _FreeSpace; }
                set { this._FreeSpace = value; }
            }
            /// <summary>
            ///            使用空间
            /// </summary>
            public double UseSpace
            {
                get { return _SumSpace - _FreeSpace; }
            }
            /// <summary>
            ///  总空间
            /// </summary>
            public double SumSpace
            {
                get { return _SumSpace; }
                set { this._SumSpace = value; }
            }
            /// <summary>
            /// 分区名称
            /// </summary>
            public string PartitionName
            {
                get { return _PartitionName; }
                set { this._PartitionName = value; }
            }
            /// <summary>
            /// 是否主分区
            /// </summary>
            public bool IsPrimary
            {
                get
                {
                    //判断是否为系统安装分区
                    if (System.Environment.GetEnvironmentVariable("windir").Remove(2) == this._PartitionName)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            #endregion //Properties
        }
        /// <summary>
        /// 处理Double值，精确到小数点后几位
        /// </summary>
        /// <param name="_value">值</param>
        /// <param name="Length">精确到小数点后几位</param>
        /// <returns>返回值</returns>
        private double ManagerDoubleValue(double _value, int Length)
        {
            if (Length < 0)
            {
                Length = 0;
            }
            return System.Math.Round(_value, Length);
        }
        /// <summary>
        /// 获取硬盘上所有的盘符空间信息列表
        /// </summary>
        /// <returns></returns>
        private List<HardDiskPartition> GetDiskListInfo()
        {
            List<HardDiskPartition> list = null;
            //指定分区的容量信息
            try
            {
                SelectQuery selectQuery = new SelectQuery("select * from win32_logicaldisk");

                ManagementObjectSearcher searcher = new ManagementObjectSearcher(selectQuery);

                ManagementObjectCollection diskcollection = searcher.Get();
                if (diskcollection != null && diskcollection.Count > 0)
                {
                    list = new List<HardDiskPartition>();
                    HardDiskPartition harddisk = null;
                    foreach (ManagementObject disk in searcher.Get())
                    {
                        int nType = Convert.ToInt32(disk["DriveType"]);
                        if (nType != Convert.ToInt32(DriveType.Fixed))
                        {
                            continue;
                        }
                        else
                        {
                            harddisk = new HardDiskPartition();
                            harddisk.FreeSpace = Convert.ToDouble(disk["FreeSpace"]) / (1024 * 1024 * 1024);
                            harddisk.SumSpace = Convert.ToDouble(disk["Size"]) / (1024 * 1024 * 1024);
                            harddisk.PartitionName = disk["DeviceID"].ToString();
                            list.Add(harddisk);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return list;
        }
        /// <summary>
        /// 获取系统推荐路径
        /// </summary>
        /// <returns></returns>
        public string getAutoDirectory()
        {
            List<HardDiskPartition> ret = GetDiskListInfo();
            if (ret.Count == 1)
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
            else
            {
                string ID = "";
                double max = 0;
                for (int i = 0; i < ret.Count; i++)
                {
                    if (!ret[i].IsPrimary)
                    {
                        if (ret[i].FreeSpace > max)
                        {
                            max = ret[i].FreeSpace;
                            ID = ret[i].PartitionName;
                        }
                    }
                }
                //菲诗奥医疗  AwareSleep
                //string path = string.Format("{0}\\菲诗奥数据\\", ID);
                string path = string.Format("{0}\\AwareSleep\\", ID);
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                //return string.Format("{0}\\菲诗奥数据\\", ID);
                return string.Format("{0}\\AwareSleep\\", ID);
            }
        }

    }
}
