using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.UI.FunctionControls
{
    public partial class DeviceInfo : SkinForm
    {
        public DeviceInfo()
        {
            InitializeComponent();
            this.Load += Info_Load;
        }

        void Info_Load(object sender, EventArgs e)
        {
            if (PortClient != null)
            {
                label12.Text += string.Format("：{0}",PortClient.DeviceName);//连接端口名
                label1.Text += string.Format("：{0}", PortClient.UserData.FirmwareVesion); //固件版本
                label2.Text += string.Format("：{0}", PortClient.UserData.DeviceSNCode); //设备序列号
                DeviceType.Text = string.Format("{0}", PortClient.UserData.DeviceType.ToString());
                label5.Text += string.Format("：{0}", PortClient.UserData.BlueToothName);//设 备 名 称

                if (PortClient.UserData.FirmwareVesion.Length >12)
                {
                    //iRem_B_V1_1_20220400
                    //iRem-A-V1.0-20200905
                    //this.Text += PortClient.UserData.FirmwareVesion.Substring(12, 8);
                    //this.Text += PortClient.UserData.FirmwareVesion.Substring(10, 1);
                    if (PortClient.UserData.FirmwareVesion.Substring(10, 1) == "0")
                    {
                        HSButton.Visible = false;
                    }
                }


                //label3.Text = "最后校准时间  " + PortClient.UserData.LastCastTime;
                label4.Text += string.Format("： \n {0}", PortClient.UserData.SoftWareVesion);//运 行 记 录
                label4.Text = label4.Text.Replace("\r\n\r\n", "\r\n");
                //label4.Text = label4.Text.Replace("Run_Time", "运行时长");
                //label4.Text = label4.Text.Replace("Record_Time", "记录时长");
                //label4.Text = label4.Text.Replace("Device_Sta", "设备状态");
                //label4.Text = label4.Text.Replace("\n\n", "\n");

                LastCheckTimeTextBox.Text = PortClient.UserData.LastCastTime;
            }
        }
        /// <summary>
        /// 设备
        /// </summary>
        public Protocol.ProtocolServer PortClient { set; get; }

        private void CheckTimeButton_Click(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Height = 380;
           // this.Width = 750;
        }


        //private void CheckTimeButton_Click(object sender, EventArgs e)
        //{
        //    Task.Factory.StartNew(() =>
        //    {
        //        if (PortClient != null)
        //        {
        //            DateTime dt = DateTime.Now;
        //            if (PortClient.BoradCastTime())
        //            {
        //                AhDung.MessageTip.ShowOk("校时成功");
        //                DataModel.LogInstance.Default.AddLog(string.Format("用户点击 设备信息-校时按钮，校时成功，最后一次校准时间为 {0}", dt.ToString("yyyy-MM-dd HH:mm:ss")));
        //                this.Invoke(new MethodInvoker(() =>
        //                {
        //                    LastCheckTimeTextBox.Text = dt.ToString("yyyy-MM-dd HH:mm:ss");
        //                }));
        //            }
        //            else
        //            {
        //                AhDung.MessageTip.ShowError("校时失败，请检查设备连接状态或重试");
        //                DataModel.LogInstance.Default.AddLog("用户点击 设备信息-校时按钮，校时失败", pSystem.LogManagement.LogLevel.ERROR);
        //            }
        //        }
        //    });
        //}
    }
}
