using System;
using System.Collections.Generic;

namespace AwareTec.Polysmith.Protocol
{
    public class ProtocolAnalysis
    {
        /// <summary>
        /// CRC16校验码结构
        /// </summary>
        public struct CRC16
        {
            public byte CRC_H;
            public byte CRC_L;
        }
        private DataDefine.FrameFactory m_Frame = null;

        public ProtocolAnalysis()
        {
            m_Frame = new DataDefine.FrameFactory()
            {
                StartByte = System.Text.Encoding.Default.GetBytes("FS"),
                Endbyte = System.Text.Encoding.Default.GetBytes("PS")
            };
        }
        /// <summary>
        /// 数据解析
        /// </summary>
        private void rxByte(byte b)
        {
            if (rxFS)
            {
                switch (rxIdx)
                {
                    case 1:
                        m_devTyp = (char)b;
                        rxIdx++;
                        break;
                    case 2:
                        type = b;
                        rxIdx++;
                        break;
                    case 3:
                        func = b;
                        rxIdx++;
                        break;
                    case 4:
                        len = b;
                        rxIdx++;
                        break;
                    case 5:
                        len += (b << 8);
                        if (len < 7)
                            rxFS = false;
                        else if (len == 7)
                        {
                            rxIdx++;
                        }
                        else
                        {
                            data = new byte[len - 7];
                            len = data.Length;
                        }
                        rxIdx++;
                        break;
                    case 6:
                        data[dataIdx++] = b;
                        if (dataIdx == len)
                        {
                            rxIdx++;
                        }
                        break;
                    case 7:
                        uSum = b;
                        rxIdx++;
                        break;
                    case 8:
                        uSum += (ushort)(b << 8);
                        ///暂不作校验判断
                        //if (uSum != CRCFull)
                        //    rxFS = false;
                        rxIdx++;
                        break;
                    case 9:
                        if (tmpIdx == 0)
                        {
                            if (b == m_Frame.Endbyte[tmpIdx])
                                tmpIdx++;
                            else
                                rxFS = false;
                        }
                        else
                        {
                            if (b == m_Frame.Endbyte[tmpIdx])
                            {
                                if (ReciveDataPikerHandle != null)
                                    ReciveDataPikerHandle.Invoke(new DataDefine.FrameFactory() { StartByte = m_Frame.StartByte, Endbyte = m_Frame.Endbyte, Fun = type, AccessSelections = func, UserData = data, FrameLength = data.Length, DeviceTyp = m_devTyp });
                            }
                            tmpIdx = 0;
                            rxFS = false;
                        }
                        break;
                }
            }
            else
            {
                if (tmpIdx == 0)
                {
                    rxIdx = 0;
                    crcLow = 0xff;
                    crcHi = 0xff;
                    dataIdx = 0;
                    uSum = 0;
                    CRCFull = 0xffff;
                    m_devTyp = ' ';
                    if (b == m_Frame.StartByte[tmpIdx])
                    {
                        tmpIdx++;
                    }
                }
                else
                {
                    if (b == m_Frame.StartByte[tmpIdx])
                    {
                        rxFS = true;
                        rxIdx++;
                    }
                    tmpIdx = 0;
                }
            }
            ///校验注释掉
            //if ((rxIdx > 1) && (rxIdx < 7))
            //{
            //    //UInt16 crcIdx = (UInt16)(crcHi ^ b); // 按位异或，对应的位相异时，结果为1
            //    //crcHi = (byte)(crcLow ^ ar_uch_CRCHi[crcIdx]);
            //    //crcLow = ar_uch_CRCLo[crcIdx];
            //    CRCFull = (ushort)(CRCFull ^ b);
            //    for (int j = 0; j < 8; j++)
            //    {
            //        CRCLSB = (char)(CRCFull & 0x0001);
            //        CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);
            //        if (CRCLSB == 1)
            //            CRCFull = (ushort)(CRCFull ^ 0xA001);
            //    }
            //}
        }
        /// <summary>
        /// 接收到数据包委托
        /// </summary>
        /// <param name="DataPiker"></param>
        public delegate void ReciveDataPiker(DataDefine.FrameFactory DataPiker);
        /// <summary>
        ///  接收到数据包事件
        /// </summary>
        public event ReciveDataPiker ReciveDataPikerHandle;
        /// <summary>
        /// 数据解析
        /// </summary>
        /// <param name="data"></param>
        public void RxBytes(byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                rxByte(data[i]);
            }
        }
        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            rxFS = false;
            tmpIdx = 0;
        }
        /// <summary>
        /// 获取CRC校验码
        /// </summary>
        /// <param name="sendbyte"></param>
        /// <returns></returns>
        public CRC16 s_Crc16Bit(List<byte> p_uch_Data)
        {
            CRC16 _crc = new CRC16();
            byte uch_CRCHi = 0xFF;
            byte uch_CRCLo = 0xFF;
            //UInt16 uin_Index = 0;

            //for (int i = 0; i < p_uch_Data.Count; i++)
            //{
            //    uin_Index = (UInt16)(uch_CRCHi ^ p_uch_Data[i]); // 按位异或，对应的位相异时，结果为1
            //    uch_CRCHi = (byte)(uch_CRCLo ^ ar_uch_CRCHi[uin_Index]);
            //    uch_CRCLo = ar_uch_CRCLo[uin_Index];
            //}
            _crc.CRC_H = uch_CRCHi;
            _crc.CRC_L = uch_CRCLo;
            return _crc;
        }
        /// <summary>
        /// 获取CRC16校验码
        /// </summary>
        private ushort CRC_16(byte[] sendbyte)
        {
            ushort CRCFull = 0xFFFF;
            char CRCLSB;

            for (int i = 0; i < sendbyte.Length; i++)
            {
                CRCFull = (ushort)(CRCFull ^ sendbyte[i]);

                for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);
                    if (CRCLSB == 1)
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                }
            }
            return CRCFull;
        }
        private char m_devTyp = ' ';
        private const int DataLengthLimit = 1024 * 1024;
        private bool rxFS = false;
        private byte tmpIdx = 0;
        private int rxIdx = 0;
        private int dataIdx = 0;
        private byte func = 0x00;
        private byte type = 0x00;
        private int len = 0x00;
        private byte[] data = null;
        private byte crcLow = 0xff;
        private byte crcHi = 0xff;
        private ushort CRCFull = 0xFFFF;
        private char CRCLSB;
        private ushort uSum = 0x00;
        #region CRC16
        private static readonly byte[] ar_uch_CRCHi =
        {
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81,
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,
            0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40
        };
        private static readonly byte[] ar_uch_CRCLo =
        {
            0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7, 0x05, 0xC5, 0xC4,
            0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09,
            0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD,
            0x1D, 0x1C, 0xDC, 0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
            0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32, 0x36, 0xF6, 0xF7,
            0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A,
            0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE,
            0x2E, 0x2F, 0xEF, 0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
            0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1, 0x63, 0xA3, 0xA2,
            0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F,
            0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB,
            0x7B, 0x7A, 0xBA, 0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
            0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0, 0x50, 0x90, 0x91,
            0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C,
            0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88,
            0x48, 0x49, 0x89, 0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
            0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83, 0x41, 0x81, 0x80,
            0x40
        };
        #endregion

    }
}
