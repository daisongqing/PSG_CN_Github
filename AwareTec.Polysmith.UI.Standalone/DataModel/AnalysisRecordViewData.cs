using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.UI.DataModel
{
    public class AnalysisRecordViewData
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { set; get; }
        /// <summary>
        /// 病历号
        /// </summary>
        public string PatientNo { set; get; }
        /// <summary>
        /// 检查号
        /// </summary>
        public string OrderID { set; get; }
        /// <summary>
        /// 病人名字
        /// </summary>
        public string PatientName { set; get; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { set; get; }

        /// <summary>
        /// 名字
        /// </summary>
        public int Age { set; get; }
        /// <summary>
        /// 身高
        /// </summary>
        public float Height { set; get; }
        /// <summary>
        /// 体重
        /// </summary>
        public float Weight { set; get; }
        /// <summary>
        /// BMI
        /// </summary>
        public float BMI { private set; get; }
        /// <summary>
        /// 医生ID
        /// </summary>
        public string DoctorID { set; get; }
        /// <summary>
        /// 医生名字
        /// </summary>
        public string DoctorName { set; get; }
        /// <summary>
        /// 就诊时间
        /// </summary>
        public DateTime RecordTime { set; get; }
        /// <summary>
        /// edf文件路径
        /// </summary>
        public string EdfPath { set; get; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string MatchKey { set; get; }
        /// <summary>
        /// 帧总数量
        /// </summary>
        public int FrameCount { set; get; }
        /// <summary>
        /// 最后一次选择的导联方案名
        /// </summary>
        public string ReviewMontageName { set; get; }
        /// <summary>
        /// 进度
        /// </summary>
        public ProgressState Progress = ProgressState.None;
        private string m_guid = "";
        /// <summary>
        /// GUID
        /// </summary>
        public string GUID
        {
            set
            {
                m_guid = value;
               /// string[] strT = value.Split('_');

            }
            get
            {
                return m_guid;
            }
        }
        /// <summary>
        /// 获取edf文件的hash值
        /// </summary>
        public string EdfHashCode { private set; get; }
        /// <summary>
        /// 获取或设置存储结果的路径
        /// </summary>
        public string ResultPath { set; get; }
        /// <summary>
        /// 记录建立时间
        /// </summary>
        public DateTime CreatTime { set; get; }
        /// <summary>
        /// 显示记录是否有视频
        /// </summary>
        public bool VideoHave { set; get; }
        /// <summary>
        /// 版本说明 2.0之前的版本全部为0,2.0版本为1
        /// </summary>
        public int DifferentVersion { set; get; }
        /// <summary>
        /// 记录视频存放地址
        /// </summary>
        public string Reserve3 { set; get; }
        /// <summary>
        /// 记录是否是定时任务
        /// </summary>
        public string Reserve4 { set; get; }
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static AnalysisRecordViewData ConvertEntity(DataRow dr)
        {
            AnalysisRecordViewData ret = new AnalysisRecordViewData();
            ret.ID = Convert.ToInt32(dr["ID"]);
            ret.Age = Convert.ToInt32(dr["Age"]);
            ret.DoctorID = dr["DoctorNo"].ToString();
            ret.DoctorName = dr["DoctorName"].ToString();
            ret.GUID = dr["GUID"].ToString();
            ret.PatientName = dr["PatientName"].ToString();
            ret.PatientNo = dr["PatientNo"].ToString();
            ret.Progress = (ProgressState)(int.Parse(dr["Progress"].ToString()));
            ret.RecordTime = Convert.ToDateTime(dr["RecordTime"]);
            ret.Sex = dr["Sex"].ToString();
            ret.EdfPath = dr["EdfPath"].ToString();
            ret.Weight =Convert.ToSingle( dr["Weight"]);
            ret.Height = Convert.ToSingle(dr["Height"]);
            ret.BMI = (float)Math.Round(ret.Weight / Math.Pow(ret.Height / 100, 2), 2);
            ret.MatchKey = dr["MatchKey"].ToString();
            ret.FrameCount = Convert.ToInt32(dr["FrameCount"]);
            ret.ReviewMontageName = dr["ReviewMontageName"].ToString();
            ret.OrderID = dr["OrderID"].ToString();
            ret.ResultPath=dr["ResultPath"].ToString();
            ret.CreatTime = Convert.ToDateTime(dr["CreatTime"]);
            ret.VideoHave = Convert.ToBoolean(dr["VideoHave"]);
            ret.DifferentVersion= Convert.ToInt32(dr["DifferentVersion"]);
            ret.Reserve3 = dr["Reserve3"].ToString();
            ret.Reserve4 = dr["Reserve4"].ToString();
            return ret;
        }
        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<AnalysisRecordViewData> ConvertEntity(DataTable dt)
        {
            List<AnalysisRecordViewData> records = new List<AnalysisRecordViewData>(dt.Rows.Count);
            foreach (DataRow dr in dt.Rows)
            {
                records.Add(ConvertEntity(dr));
            }
            return records;
        }
    }
}
