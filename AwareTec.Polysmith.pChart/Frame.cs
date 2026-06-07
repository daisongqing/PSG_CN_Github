using System;
using System.Collections.Generic;

namespace AwareTec.Polysmith.pChart
{
    /// <summary>
    ///  通道数据
    /// </summary>
    public class Frame
    {
        /// <summary>
        /// 数据单元
        /// </summary>
        public struct Uion
        {
            /// <summary>
            /// 单元索引号
            /// </summary>
            public int Index;
            /// <summary>
            /// 是否已完成
            /// </summary>
            public bool Ready;
            /// <summary>
            /// 最后一个数据所在索引号
            /// </summary>
            private int m_dataIdx;
            /// <summary>
            /// 最终存储的数据组（1s内数据）
            /// </summary>
            public float[] Value;
            private int m_MaxLen;
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="lenght"></param>
            public Uion(int lenght)
            {
                Index = 0;
                Ready = false;
                m_dataIdx = 0;
                m_MaxLen = lenght;
                Value = new float[m_MaxLen];
            }
            /// <summary>
            /// 添加数据
            /// </summary>
            /// <param name="data"></param>
            public void Add(float data)
            {
                Value[m_dataIdx++] = data;
                if (m_dataIdx == m_MaxLen)
                    Ready = true;
            }
            /// <summary>
            /// 获取最新数据索引号
            /// </summary>
            /// <returns></returns>
            public int getLastIndex()
            {
                return m_dataIdx;
            }
            /// <summary>
            /// 克隆数据
            /// </summary>
            /// <returns></returns>
            public Uion Clone()
            {
                Uion ret = new Uion(this.m_MaxLen);
                ret.Index = this.Index;
                Array.Copy(this.Value, ret.Value, m_MaxLen);
                ret.Ready = this.Ready;
                return ret;
            }
        }
        private int DataLength = 0;
        private Uion m_CurrentFrame;
        private Uion[] m_ValueList;
        private int m_DataIndex = 0;
        /// <summary>
        /// 放大倍数
        /// </summary>
        public int ZoomRate = 1;
        /// <summary>
        /// 帧结构
        /// </summary>
        /// <param name="sampleTimeSpan"></param>
        public Frame(int sampleTimeSpan)
        {
            DataLength = 1000 / sampleTimeSpan;
            m_CurrentFrame = new Uion(DataLength);
            m_ValueList = new Uion[30000];
            BaseTimeSpan = 30;
        }
        private Dictionary<int, int[]> m_DataMap = new Dictionary<int, int[]>(1000);
        /// <summary>
        /// 多少秒为一帧
        /// </summary>
        public int BaseTimeSpan { set; get; }
        /// <summary>
        /// 平均值
        /// </summary>
        public float Average { set; get; }
        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue { set; get; }
        /// <summary>
        /// 最小值
        /// </summary>
        public float MinValue { set; get; }
        private int m_dataCount = 0;
        /// <summary>
        /// 数据长度
        /// </summary>
        public int Count { get { return m_dataCount; } }
        /// <summary>
        /// 添加单个数据
        /// </summary>
        /// <param name="data"></param>
        public void Add(float data)
        {
            if (m_CurrentFrame.Index == 0 && m_CurrentFrame.getLastIndex() == 0)
            {
                MaxValue = MinValue = data;
            }
            else
            {
                if (data > MaxValue)
                {
                    MaxValue = data;
                }
                if (data < MinValue)
                {
                    MinValue = data;
                }
            }
            float value = ZoomRate == 1 ? data : data / ZoomRate;
            m_CurrentFrame.Add(value);
            Average = Average / 2 + value / 2;
            if (m_CurrentFrame.Ready)
            {
                m_ValueList[m_DataIndex++] = m_CurrentFrame.Clone();
                m_CurrentFrame.Ready = false;
                m_CurrentFrame.Index++;
            }
            m_dataCount++;
        }
        /// <summary>
        /// 添加集合数据
        /// </summary>
        /// <param name="data"></param>
        public void AddRange(IEnumerable<float> data)
        {
            foreach (float value in data)
            {
                Add(value);
            }
        }
        /// <summary>
        /// 获取当前需要实时显示的数据段
        /// </summary>
        /// <param name="frameNo">当前帧序号</param>
        /// <param name="timeLine">当前时基（s）</param>
        /// <param name="offset">偏移数（s）</param>
        /// <returns></returns>
        public float[] getViewValues(int frameNo, int timeLine, int offset)
        {
            int cnt = timeLine / BaseTimeSpan;
            int offset1 = 0, offset2 = 0;
            offset1 = BaseTimeSpan - offset;
            if (offset1 < timeLine)
            {
                cnt += 1;
                offset2 = timeLine - offset1;
            }
            float[] ret = new float[timeLine * DataLength];
            int tmpIdx = 0;
            for (int s = 0; s <= cnt; s++)
            {
                int[] idxs = m_DataMap[frameNo + s];
                for (int i = offset1 - 1; i < idxs.Length; i++)
                {
                    Array.Copy(m_ValueList[idxs[i]].Value, 0, ret, DataLength * tmpIdx++, DataLength);
                }
            }
            return ret;
        }
        /// <summary>
        /// 获取当前需要实时显示的数据段
        /// </summary>
        /// <param name="timeLine"></param>
        /// <returns></returns>
        public float[] getViewValues(int timeLine)
        {
            float[] ret = new float[timeLine * DataLength];
            int lastdataIdx = m_CurrentFrame.getLastIndex();
            int currentUionIndex = m_CurrentFrame.Index;
            int offset = DataLength - lastdataIdx;
            Array.Copy(m_ValueList[currentUionIndex - timeLine + 1].Value, lastdataIdx, ret, 0, offset);
            for (int i = timeLine - 2; i > 0; i--)
            {
                Array.Copy(m_ValueList[currentUionIndex - i].Value, 0, ret, offset, DataLength);
                offset += DataLength;
            }
            Array.Copy(m_ValueList[currentUionIndex - timeLine + 1].Value, 0, ret, offset, lastdataIdx);
            return ret;
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public float[] getAllValues()
        {
            float[] ret = new float[Count];
            for (int i = 0; i < m_ValueList.Length; i++)
            {
                if (!m_ValueList[i].Ready)
                    break;
                else
                {
                    Array.Copy(m_ValueList[i].Value, 0, ret, i * DataLength, DataLength);
                }
            }
            return ret;
        }
        /// <summary>
        /// 获取范围内数据点
        /// </summary>
        /// <param name="RecordTimeStart">开始记录时间</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public float[] getRectangleValues(DateTime RecordTimeStart, DateTime startTime, DateTime endTime)
        {
            float[] ret = new float[Count];

            return ret;
        }

        /// <summary>
        /// 获取范围内数据点
        /// </summary>
        /// <param name="endIndex">结束索引</param>
        /// <param name="startIndex">开始索引</param>
        /// <returns></returns>
        public float[] getRectangleValues(int startIndex, int endIndex)
        {
            float[] ret = new float[Count];

            return ret;
        }
    }
}
