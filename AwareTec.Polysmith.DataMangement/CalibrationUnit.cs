using AwareTec.Polysmith.DataBaseCom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AwareTec.Polysmith.DataMangement
{
    public class CalibrationUnit : IUnit
    {
        #region 私有成员

        #endregion
        #region 公有成员
        /// <summary>
        /// 构造函数
        /// </summary>
        internal CalibrationUnit()
        {
            FileName = "Calibration.calt";
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="result"></param>List<float> values
        public void WriteResult(List<float> values)
        {
            if (m_writer == null)
                m_writer = CreatWriter(m_savePath);
            m_writer.Write(System.Text.Encoding.Default.GetBytes(ToString(values)));
            m_writer.Flush();
            m_writer.Close();
            m_writer.Dispose();
            m_writer = null;
        }

        /// <summary>
        /// 读取结果
        /// </summary>
        public List<float> ReadResult()
        {
            return ToValue(base.ReadData());
        }

        /// <summary>
        /// 转成字符串
        /// </summary>
        /// <returns></returns>
        public string ToString(List<float> values)
        {
            return string.Format("{0}#{1}", GUID, string.Join(",", values));
        }
        /// <summary>
        /// 转成值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<float> ToValue(string value)
        {
            List<float> m_CalibrationValues = new List<float>();
            string[] values = value.Split('#');
            if (values.Length == 2)
            {
                if (GUID == values[0])
                {
                    string[] strValues = values[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        m_CalibrationValues = strValues.Select(t => float.Parse(t)).ToList();
                    }
                    catch { }
                }
            }
            return m_CalibrationValues;
        }
        #endregion
    }
}
