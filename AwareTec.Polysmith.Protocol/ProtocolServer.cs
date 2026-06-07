using pSystem.Communication.Com;
using pSystem.LogManagement;
using System;
using System.Collections.Generic;

namespace AwareTec.Polysmith.Protocol
{
    public class ProtocolServer
    {
        /// <summary>
        /// 协议通讯对象
        /// </summary>
        private ProtocolClient m_ProtocolClient = null;

        private object m_lockRecDatadefine = new object();
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="strValue"></param>
        private void addLog(string strValue)
        {
            LogItem lg = new LogItem();
            lg.Description = strValue;
            lg.Level = LogLevel.INFO;
            lg.LogTyp = LogType.PhysicalLayer;
            lg.PhysicalAddress = m_ProtocolClient.PortClient.MatchKey;
            addLog(lg);
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="lg"></param>
        private void addLog(LogItem lg)
        {
            if (m_AddLog != null)
            {
                m_AddLog.Invoke(lg);
            }
        }
        private ProtocolServer()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ProtocolServer(IOClient client, List<DataDefine> Channels)
        {
            m_ProtocolClient = new ProtocolClient(Channels);
            client.AddLog += addLog;
            m_ProtocolClient.PortClient = client;
            DeviceName = client.MatchKey;
            m_ProtocolClient.RefreshUserDataHandle += m_ProtocolClient_RefreshUserDataHandle;
            m_ProtocolClient.RecImpedanceDataHandle += m_ProtocolClient_RecImpedanceDataHandle;
            m_ProtocolClient.RecDeviceNameEventHandle += m_ProtocolClient_RecDeviceNameEventHandle;
            DataViewTyp = 2;
        }

        public void UpdatePortClient(IOClient client)
        {
            client.AddLog += addLog;
            DeviceName = client.MatchKey;
            m_ProtocolClient.PortClient = client;
        }
        private void m_ProtocolClient_RecDeviceNameEventHandle(string matchKey, string devName)
        {
            if (RecDeviceNameEventHandle != null)
            {
                if (RecDeviceNameEventHandle.Invoke(matchKey, devName))
                {
                    Dispose(false);
                }
            }
        }

        private void m_ProtocolClient_RecImpedanceDataHandle(uint[] UserData)
        {
            if (m_RecImpedanceDataHandle != null)
                m_RecImpedanceDataHandle.BeginInvoke(UserData, null, null);
        }
        /// <summary>
        /// 更新用户数据
        /// </summary>
        /// <param name="userData"></param>
        private void m_ProtocolClient_RefreshUserDataHandle(UserDataDefine userData)
        {
            if (RecUserDataHandle != null && userData.InitReady)
            {
                RecUserDataHandle.BeginInvoke(userData.Clone(), null, null);
            }
            UserData = userData;
        }
        /// <summary>
        /// 查询到设备名时触发委托
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="data"></param>
        public delegate bool RecDeviceNameDelegate(string matchKey, string devName);
        /// <summary>
        ///  查询到设备名时触发事件
        /// </summary>
        public event RecDeviceNameDelegate RecDeviceNameEventHandle;
        /// <summary>
        /// 用户数据更新时触发委托
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="data"></param>
        public delegate void RecUserDataDelegate(UserDataDefine UserData);
        /// <summary>
        /// 用户数据更新时触发事件
        /// </summary>
        public event RecUserDataDelegate RecUserDataHandle;
        public UserDataDefine UserData = new UserDataDefine();
        /// <summary>
        /// 阻抗数据更新时触发委托
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="data"></param>
        public delegate void RecImpedanceDataDelegate(uint[] UserData);
        /// <summary>
        /// 阻抗数据更新时触发事件
        /// </summary>
        private event RecImpedanceDataDelegate m_RecImpedanceDataHandle;
        /// <summary>
        /// 阻抗数据更新时触发事件
        /// </summary>
        public event RecImpedanceDataDelegate RecImpedanceDataHandle
        {
            add
            {
                if (m_RecImpedanceDataHandle != null)
                    m_RecImpedanceDataHandle = null;
                m_RecImpedanceDataHandle += value;
            }
            remove
            {
                m_RecImpedanceDataHandle = null;
            }
        }
        /// <summary>
        /// 终止通讯任务
        /// </summary>
        public bool FnStop
        {
            set
            {
                m_ProtocolClient.fnStop = value;
            }
            get
            {
                return m_ProtocolClient.fnStop;
            }
        }
        private string m_patientNo = "";
        public string PatientNo
        {
            set
            {
                m_patientNo = value;
                m_ProtocolClient.ID = value;
            }
            get
            {
                return m_patientNo;
            }
        }
        /// <summary>
        /// 监听日志信息
        /// </summary>
        private event Action<LogItem> m_AddLog;
        /// <summary>
        /// 监听日志信息
        /// </summary>
        public event Action<LogItem> AddLog
        {
            add
            {
                if (m_AddLog != null)
                    m_AddLog -= value;
                m_AddLog += value;
            }
            remove
            {
                m_AddLog = null;
            }
        }
        /// <summary>
        /// 获取设备名称
        /// </summary>
        public string DeviceName { private set; get; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public bool InitDeviceInfo()
        {
            addLog("发送开启采样指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.GetBatteryCapacityRequest();
            addLog(string.Format("结束开启采样指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 校时
        /// </summary>
        /// <returns></returns>
        public bool BoradCastTime()
        {
            addLog("发送设备校时指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.BoradCastTime();
            addLog(string.Format("结束设备校时指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 开始采样
        /// </summary>
        /// <returns></returns>
        public bool StartSampleRequest()
        {
            addLog("发送开启采样指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.SetStartSampleRequest();
            addLog(string.Format("结束开启采样指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 停止采样
        /// </summary>
        /// <returns></returns>
        public bool StopSampleRequest()
        {
            addLog("发送停止采样指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.SetStopSampleRequest();
            addLog(string.Format("结束停止采样指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 设置病人信息
        /// </summary>
        /// <returns></returns>
        public bool SetPatientIDRequest(string ID)
        {
            addLog("发送设置病人ID指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.SetPatientIDRequest(ID);
            addLog(string.Format("结束设置病人ID指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 读取病人信息
        /// </summary>
        /// <returns></returns>
        public bool GetPatientIDRequest()
        {
            addLog("发送读取病人ID指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.GetPatientIDRequest();
            addLog(string.Format("结束读取病人ID指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 设置设备信息
        /// </summary>
        /// <returns></returns>
        public bool SetDeviceInfoRequest(string ID)
        {
            addLog("发送设置设备信息指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.SetDeviceInfoRequest(ID);
            addLog(string.Format("结束设置设备信息指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <returns></returns>
        public bool GetDeviceInfoRequest()
        {
            addLog("发送读取设备信息指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.GetDeviceInfoRequest();
            addLog(string.Format("结束读取设备信息指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 阻抗测试
        /// </summary>
        /// <returns></returns>
        public bool GetImpedanceDataRequest(bool on)
        {
            addLog("发送开始阻抗测试指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.GetImpedanceDataRequest(on);
            addLog(string.Format("结束开始阻抗测试指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 继续监听
        /// </summary>
        /// <returns></returns>
        public bool SetContinueSampleRequest()
        {
            addLog("发送继续监听指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.SetContinueSampleRequest();
            addLog(string.Format("结束继续监听指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 设置定时开启功能
        /// </summary>
        /// <returns></returns>
        public bool SetAutoRunTime(DateTime startTime, DateTime edTime)
        {
            addLog("发送设置定时开始/停止采集时间指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.SetAutoRunTime(startTime, edTime);
            addLog(string.Format("结束设置定时开始/停止采集时间指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }

        /// <summary>
        /// 设置生物定标
        /// </summary>
        /// <returns></returns>
        public bool SetCalibrationFlag(int CalibrationTyp)
        {
            addLog("发送设置生物定标指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.SetCalibrationFlag(CalibrationTyp);
            addLog(string.Format("结束设置生物定标指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }

        /// <summary>
        /// 获取定时时间
        /// </summary>
        /// <returns></returns>
        public bool GetAutoRunTime()
        {
            addLog("发送获取定时开始/停止采集时间指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.GetAutoRunTime();
            addLog(string.Format("结束获取定时开始/停止采集时间指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 设置设备蓝牙名称
        /// </summary>
        /// <returns></returns>
        public bool SetBlueToothName(string name)
        {
            addLog("发送设置设备蓝牙名称指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.SetBlueToothName(name);
            addLog(string.Format("结束设置设备蓝牙名称指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 获取设备蓝牙名称
        /// </summary>
        /// <returns></returns>
        public bool getBlueToothName()
        {
            addLog("发送获取设备蓝牙名称指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.GetBlueToothName();
            addLog(string.Format("结束获取设备蓝牙名称指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 设置设备SN号
        /// </summary>
        /// <returns></returns>
        public bool SetDeviceSNCode(string name)
        {
            addLog("发送设置设备SN号指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.SetDeviceSNCode(name);
            addLog(string.Format("结束设置设备SN号指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 获取设备SN号
        /// </summary>
        /// <returns></returns>
        public bool GetDeviceSNCode()
        {
            addLog("发送获取设备SN号指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.GetDeviceSNCode();
            addLog(string.Format("结束获取设备SN号指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 获取设备历史日志
        /// </summary>
        /// <returns></returns>
        public bool GetDeviceHS()
        {
            addLog("发送获取设备历史日志");
            DataDefine.eumErrCode ret = m_ProtocolClient.GetDeviceHS();
            addLog(string.Format("结束获取设备历史日志---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 获取设备固件号
        /// </summary>
        /// <returns></returns>
        //public bool GetDeviceFWCode()
        //{
        //    addLog("发送获取设备固件指令");
        //    DataDefine.eumErrCode ret = m_ProtocolClient.GetDeviceFWCode();
        //    addLog(string.Format("结束获取设备固件号指令---[结果] {0}", ret.ToString()));
        //    return ret == DataDefine.eumErrCode.OK;
        //}
        /// <summary>
        /// 设置设备类型
        /// </summary>
        /// <returns></returns>
        public bool SetDeviceType(int deviceType = 1)
        {
            addLog("发送设置设备类型指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.SetDeviceType(deviceType);
            addLog(string.Format("结束设置设备类型指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 设置设备类型
        /// </summary>
        /// <returns></returns>
        public bool GetDeviceType()
        {
            addLog("发送获取设备类型指令");
            DataDefine.eumErrCode ret = m_ProtocolClient.GetDeviceType();
            addLog(string.Format("结束获取设备类型指令---[结果] {0}", ret.ToString()));
            return ret == DataDefine.eumErrCode.OK;
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose(bool allsource = true)
        {
            m_ProtocolClient.Dispose();
            if (allsource)
            {
                if (m_ProtocolClient.PortClient.IsConnected)
                    m_ProtocolClient.PortClient.Close();
            }
        }
        private int m_DataViewTyp = 0;
        /// <summary>
        /// 数据处理 0-仅存储源报文 1-仅存储解析后转换数据 2-存储转换数据并显示数据 3-全部
        /// </summary>
        public int DataViewTyp
        {
            set
            {
                m_ProtocolClient.IsSaver = 1;
                m_ProtocolClient.isShowToView = true;
                if (value == 0)
                {
                    m_ProtocolClient.IsSaver = 0;
                }
                else if (value == 1)
                {
                    m_ProtocolClient.isShowToView = false;
                }
                else if (value == 3)
                    m_ProtocolClient.IsSaver = 2;
                m_DataViewTyp = value;
            }
        }

        /// <summary>
        /// 设置存储数据的基础目录
        /// </summary>
        public string BaseSavePath
        {
            set
            {
                m_ProtocolClient.BaseSavePath = value;
            }
        }
    }
}
