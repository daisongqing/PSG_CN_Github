using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Vedio
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class MyConfiguration
    {
        public MyConfiguration()
        {
            UrlPath = "192.168.1.64:8000";
            UserName = "admin";
            PassWord = "physio123.";
            FileKeysName = "TimeKey.cfg";
        }
        /// <summary>
        /// 视频监控网络地址
        /// </summary>
        public string UrlPath { set; get; }
        /// <summary>
        /// 登录账户
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string PassWord { set; get; }
        /// <summary>
        /// 匹配码
        /// </summary>
        public string MatchKey { set; get; }
        /// <summary>
        /// 视频监控存储目录
        /// </summary>
        public string SaveDirectory { set; get; }
        /// <summary>
        /// 获取视频文件完整路径
        /// </summary>
        public string FileName
        {
            get
            {
                if (SaveDirectory == "")
                    SaveDirectory = string.Format("{0}Media", AppDomain.CurrentDomain.BaseDirectory);
                return string.Format("{0}\\{1}\\{1}.mp4", SaveDirectory, MatchKey);
            }
        }
        /// <summary>
        /// edf的总时间ms
        /// </summary>
        public int EDFTotalTimes { set; get; }
        /// <summary>
        /// 关键信息的文件名称
        /// </summary>
        public string FileKeysName { set; get; }
        /// <summary>
        /// Tag
        /// </summary>
        public object Tag { set; get; }
        /// <summary>
        /// 当前所有的视频文件对象
        /// </summary>
        public List<VedioUion> Vedios = new List<VedioUion>();
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            Vedios = readVedio();
        }
        /// <summary>
        /// 读取视频单元信息
        /// </summary>
        /// <returns></returns>
        public List<VedioUion> readVedio()
        {
            DateTime dt = DateTime.Now;
            if (SaveDirectory == "")
                SaveDirectory = string.Format("{0}Media", AppDomain.CurrentDomain.BaseDirectory);
            List<VedioUion> ret = new List<VedioUion>();
            string dirpath = string.Format("{0}\\{1}", SaveDirectory, MatchKey);
            string path = string.Format("{0}\\{1}", dirpath, FileKeysName);
            Dictionary<int, string> keys = new Dictionary<int, string>();
            if (Directory.Exists(dirpath))
            {
                string[] files = Directory.GetFiles(dirpath, "*.mp4");
                foreach (string f in files)
                {
                    keys.Add(Path.GetFileName(f).GetHashCode(), f);
                }
            }
            if (!File.Exists(path))
            {
                if (Directory.Exists(dirpath))
                {
                    string values = "";
                    foreach (KeyValuePair<int, string> p in keys)
                    {
                        string fpath = p.Value;
                        DateTime st = default(DateTime);
                        DateTime end = default(DateTime);
                        Vedio.VedioManagement.Default.getStartAndEndTime(fpath, ref st, ref end);
                        VedioUion one = new VedioUion()
                        {
                            Path = fpath,
                            StartTime = st,
                            EndTime = end,
                            FileName = Path.GetFileName(fpath)
                        };
                        ret.Add(one);
                        values = string.Format("{0}{1}#{2}%{3};", values, p.Key, one.StartTime, one.EndTime);
                    }
                    System.IO.File.WriteAllText(path, values, Encoding.UTF8);
                }
            }
            else
            {
                string value = System.IO.File.ReadAllText(path, Encoding.UTF8);
                string[] sss = value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string ss in sss)
                {
                    string[] strValue = ss.Split(new char[] { '#', '%' }, StringSplitOptions.RemoveEmptyEntries);
                    if (strValue.Length == 3)
                    {
                        try
                        {
                            int hashcode = int.Parse(strValue[0]);
                            ret.Add(new VedioUion()
                            {
                                Path = keys[hashcode],
                                StartTime = DateTime.Parse(strValue[1]),
                                EndTime = DateTime.Parse(strValue[2]),
                                FileName = Path.GetFileName(keys[hashcode])
                            });
                        }
                        catch (Exception ee)
                        {

                        }
                    }
                }
            }
            System.Diagnostics.Trace.WriteLine(string.Format("检索视频文件时间键数据耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
            return ret;
        }
        /// <summary>
        /// 根据时间自动匹配
        /// </summary>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        public VedioUion getVedioPlay(DateTime currentTime)
        {
            VedioUion find = Vedios.Find(t => t.StartTime <= currentTime && t.EndTime >= currentTime);
            if (find != null)
            {
                if (find.Running)
                {
                    return find;
                }
            }
            VedioUion run = Vedios.Find(t => t.Running);
            if (run != null)
            {
                DateTime dt = DateTime.Now;
                if (VedioManagement.Default.ReviewStop().IsOK)
                {
                    run.Running = false;
                }
                System.Diagnostics.Trace.WriteLine(string.Format("关闭视频耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
            }
            return find;
        }
        /// <summary>
        /// 复位
        /// </summary>
        public void Reset()
        {
            foreach (VedioUion run in Vedios)
                if (run.Running)
                {
                    run.Running = false;
                }
        }
    }
    /// <summary>
    /// 视频单元
    /// </summary>
    public class VedioUion
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path = "";
        /// <summary>
        /// 记录开始时间
        /// </summary>
        public DateTime StartTime = default(DateTime);
        /// <summary>
        /// 记录结束时间
        /// </summary>
        public DateTime EndTime = default(DateTime);

        public DateTime Now = default(DateTime);

        public int OffsetTimes = 0;
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { set; get; }
        /// <summary>
        /// 获取文件名对应的哈希码
        /// </summary>
        public int HashCode { set; get; }
        /// <summary>
        /// 是否在被运行中
        /// </summary>
        public bool Running = false;
        /// <summary>
        /// 窗口句柄
        /// </summary>
        public IntPtr Hwnd = IntPtr.Zero;
        /// <summary>
        /// 播放器实例
        /// </summary>
        public VedioRePlayFactory VedioEntly { set; get; }
    }
}
