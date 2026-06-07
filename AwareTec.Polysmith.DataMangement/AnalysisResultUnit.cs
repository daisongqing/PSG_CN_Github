using AwareTec.Polysmith.DataBaseCom;
using Newtonsoft.Json;
using System;

namespace AwareTec.Polysmith.DataMangement
{
    /// <summary>
    /// 结果保存
    /// </summary>
    public class AnalysisResultUnit : IUnit
    {
        #region 私有成员

        #endregion
        #region 公有成员
        internal AnalysisResultUnit()
        {
            FileName = "result.rlt";
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="result"></param>
        public void WriteResult(Doc_DataResult result)
        {
            if (m_writer == null)
                m_writer = CreatWriter(m_savePath);
            m_writer.BaseStream.SetLength(0);
            m_writer.Write(System.Text.Encoding.Default.GetBytes(ToString(result)));
        }

        /// <summary>
        /// 读取结果
        /// </summary>
        public Doc_DataResult ReadResult()
        {
            return ToValue(base.ReadData());
        }

        /// <summary>
        /// 转成字符串
        /// </summary>
        /// <returns></returns>
        public string ToString(Doc_DataResult result)
        {
            return JsonConvert.SerializeObject(result);
        }
        /// <summary>
        /// 转成值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Doc_DataResult ToValue(string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<Doc_DataResult>(value);
            }
            catch(Exception ee)
            {
                //todo 这里有时候结果的转换会有问题，暂时不处理，客户端不影响结果。
            }
            return new Doc_DataResult();
        }
        #endregion
    }
}
