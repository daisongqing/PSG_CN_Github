using AwareTec.Polysmith.DataBaseCom;
using Newtonsoft.Json;
using pSystem.DataBaseHepler;
using pSystem.Interface.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.UI.DataModel
{
    public class DataBaseHelper
    {
        private static DataBaseHelper m_Default = null;
        private string m_dbName = "AwareTecSmith.db";
        private string m_dbPsw = "123456";
        /// <summary>
        /// 单例模式
        /// </summary>
        public static DataBaseHelper Default
        {
            get
            {
                return m_Default ?? (m_Default = new DataBaseHelper());

            }

        }
        private IDataBaseCommand m_sqlCommand = null;

        private DataBaseHelper()
        {
            ///已经转移到了日志类，这里就可以注释掉了
            //if (!TemplateCheck.Default.Check())
            //{
            //    AhDung.MessageTip.ShowError("缺少必要的启动文件，系统无法正常运行！");
            //    m_Default = null;
            //    return;
            //}
            m_sqlCommand = new SQLiteHepler(string.Format("Data Source={0}{1};Password={2};", AppDomain.CurrentDomain.BaseDirectory, m_dbName, m_dbPsw));
        }

        /// <summary>
        ///  计算指定文件的SHA256值
        /// </summary>
        /// <param name="fileName">指定文件的完全限定名称</param>
        /// <returns>返回值的字符串形式</returns>
        public string ComputeSHA256(String fileName)
        {
            String hashMD5 = String.Empty;
            //检查文件是否存在，如果文件存在则进行计算，否则返回空值
            if (System.IO.File.Exists(fileName))
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    //计算文件的MD5值
                    System.Security.Cryptography.SHA256 calculator = System.Security.Cryptography.SHA256.Create();
                    byte[] buff = new byte[1024 * 10];///只取前10k的数据计算哈希值
                    int len = fs.Read(buff, 0, buff.Length);
                    byte[] readbytes = new byte[len];
                    Array.Copy(buff, 0, readbytes, 0, len);
                    Byte[] buffer = calculator.ComputeHash(readbytes);
                    calculator.Clear();
                    hashMD5 = string.Join("", buffer.Select(t => t.ToString("x2")));
                }//关闭文件流
            }//结束计算
            return hashMD5;
        }
        /// <summary>
        ///  计算指定文件的SHA256值
        /// </summary>
        /// <param name="fileName">指定文件的完全限定名称</param>
        /// <returns>返回值的字符串形式</returns>
        public string ComputeSHA256(byte[] data)
        {
            String hashMD5 = String.Empty;
            //计算文件的MD5值
            System.Security.Cryptography.SHA256 calculator = System.Security.Cryptography.SHA256.Create();
            byte[] buff = new byte[1024 * 10];///只取前10k的数据计算哈希值
            Array.Copy(data, 0, buff, 0, buff.Length);
            Byte[] buffer = calculator.ComputeHash(buff);
            calculator.Clear();
            hashMD5 = string.Join("", buffer.Select(t => t.ToString("x2")));
            return hashMD5;
        }
        /// <summary>
        /// 保存所有指标
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="DoctorID"></param>
        /// <param name="edfPath"></param>
        public bool SaveCompeletResultEx(string guid2, string resultPath)
        {
            if (!string.IsNullOrEmpty(guid2))
            {
                ///更新掉记录状态
                m_sqlCommand.Update(new Doc_MainViewRecord()
                                    {
                                        GUID = guid2
                                    },
                                    new Doc_MainViewRecord()
                                    {
                                        Progress = (int)ProgressState.Compelet,
                                        Reserve5 = resultPath
                                    });
                return true;
            }
            return false;
        }

        /// <summary>
        /// 保存临时指标
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool SaveTemporaryResultEx(AnalysisResult result, bool onlyResult = false)
        {
            if (this.m_sqlCommand == null)
            {
                return false;
            }
            string guid = result.GUID;
            if (!string.IsNullOrEmpty(guid))
            {
                if (!onlyResult)
                {
                    Doc_EventRecords doc_EventRecords = new Doc_EventRecords
                    {
                        GUID = guid
                    };
                    List<Doc_EventRecords> list = this.SelectAll<Doc_EventRecords>(doc_EventRecords);
                    List<Doc_EventRecords> list2 = new List<Doc_EventRecords>();
                    if (list.Count > 0)
                    {
                        this.m_sqlCommand.Delete(doc_EventRecords);
                        list2 = list.FindAll((Doc_EventRecords t) => t.EventType >= 99);
                    }
                    int i;
                    for (i = 0; i < result.EventRecords.Count; i++)
                    {
                        if (result.EventRecords[i].EventType >= 99)
                        {
                            Doc_EventRecords ff = list2.Find((Doc_EventRecords t) => t.EventID == result.EventRecords[i].EventID);
                            if (ff != null)
                            {
                                list2.RemoveAll((Doc_EventRecords t) => t.EventID == ff.EventID);
                            }
                        }
                    }
                    result.EventRecords.AddRange(list2);
                    this.m_sqlCommand.Insert<Doc_EventRecords>(result.EventRecords);
                    Doc_Epochs condition = new Doc_Epochs
                    {
                        GUID = guid
                    };
                    this.m_sqlCommand.Delete(condition);
                    this.m_sqlCommand.Insert<Doc_Epochs>(result.Epochs);
                }
                Doc_DataResult doc_DataResult = new Doc_DataResult();
                if (result.Sleep != null)
                {
                    doc_DataResult.SleepResult = JsonConvert.SerializeObject(result.Sleep);
                }
                if (result.BloodOxygen != null)
                {
                    doc_DataResult.BloodOxygenResult = JsonConvert.SerializeObject(result.BloodOxygen);
                }
                if (result.BreathEvent != null)
                {
                    doc_DataResult.BreathEventResult = JsonConvert.SerializeObject(result.BreathEvent);
                }
                if (result.BodyState != null)
                {
                    doc_DataResult.BodyStateResult = JsonConvert.SerializeObject(result.BodyState);
                }
                if (result.BodyMovement != null)
                {
                    doc_DataResult.BodyMovementResult = JsonConvert.SerializeObject(result.BodyMovement);
                }
                if (result.HeartRate != null)
                {
                    doc_DataResult.HeartRateResult = JsonConvert.SerializeObject(result.HeartRate);
                }
                if (!this.m_sqlCommand.Update(new Doc_DataResult
                {
                    GUID = guid
                }, doc_DataResult))
                {
                    doc_DataResult.GUID = guid;
                    this.m_sqlCommand.Insert(doc_DataResult);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="patientID"></param>
        /// <param name="guid"></param>
        /// <param name="matchKey"></param>
        /// <returns></returns>
        public bool DeletaResultEx(int ID, string patientID, string guid, string matchKey)
        {
            if (this.m_sqlCommand == null)
            {
                return false;
            }
            bool result;
            if (result = this.m_sqlCommand.Delete(new Doc_MainViewRecord
            {
                ID = ID
            }))
            {
                List<ITable> list = new List<ITable>();
                //if (!string.IsNullOrEmpty(guid))
                //{
                //    list.Add(new Doc_DataResult
                //    {
                //        GUID = guid
                //    });
                //    list.Add(new Doc_Epochs
                //    {
                //        GUID = guid
                //    });
                //    list.Add(new Doc_EventRecords
                //    {
                //        GUID = guid
                //    });
                //}
                #region 删除相关数据
                #region 如果记录中不存在该病人id，则删除病人表对应id的数据
                if (this.m_sqlCommand.Select(new Doc_MainViewRecord
                {
                    PatientID = patientID
                }).Rows.Count == 0)
                {
                    list.Add(new Doc_PatientInfo
                    {
                        PatientNo = patientID
                    });
                }
                #endregion
                #region 删除对应的定标数据
                if (!string.IsNullOrEmpty(matchKey))
                {
                    list.Add(new Doc_CalibrationRecord
                    {
                        MatchKey = matchKey
                    });
                }
                #endregion
                this.m_sqlCommand.Delete<ITable>(list);
                #endregion
            }
            return result;
        }

        /// <summary>
        /// 从数据库获取分析结果数据
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public AnalysisResult ReadResultEx(string guid)
        {
            DateTime dt = DateTime.Now;
            AnalysisResult analysisResult = new AnalysisResult();
            if (this.m_sqlCommand == null)
            {
                return analysisResult;
            }
            analysisResult.Epochs = new TableConvert<Doc_Epochs>().ConvertFromTable(this.m_sqlCommand.Select(new Doc_Epochs
            {
                GUID = guid
            }));
            Console.WriteLine(string.Format("*****读取Epochs耗时{0}ms*******", (DateTime.Now - dt).TotalMilliseconds));
            analysisResult.EventRecords = new TableConvert<Doc_EventRecords>().ConvertFromTable(this.m_sqlCommand.Select(new Doc_EventRecords
            {
                GUID = guid
            }));
            Console.WriteLine(string.Format("*****读取Events耗时{0}ms*******", (DateTime.Now - dt).TotalMilliseconds));
            List<Doc_DataResult> list = new TableConvert<Doc_DataResult>().ConvertFromTable(this.m_sqlCommand.Select(new Doc_DataResult
            {
                GUID = guid
            }));
            Console.WriteLine(string.Format("*****读取DataResult耗时{0}ms*******", (DateTime.Now - dt).TotalMilliseconds));
            if (list.Count > 0)
            {
                analysisResult.Sleep = JsonConvert.DeserializeObject<Doc_SleepResult>(list[0].SleepResult);
                analysisResult.BloodOxygen = JsonConvert.DeserializeObject<Doc_BloodOxygenResult>(list[0].BloodOxygenResult);
                analysisResult.BodyMovement = JsonConvert.DeserializeObject<Doc_BodyMovementResult>(list[0].BodyMovementResult);
                analysisResult.BodyState = JsonConvert.DeserializeObject<Doc_BodyStateResult>(list[0].BodyStateResult);
                analysisResult.BreathEvent = JsonConvert.DeserializeObject<Doc_BreathEventResult>(list[0].BreathEventResult);
                analysisResult.HeartRate = JsonConvert.DeserializeObject<Doc_HeartRateReuslt>(list[0].HeartRateResult);
            }
            analysisResult.EventRecords= analysisResult.EventRecords.Distinct(new Doc_EventRecords.Distinct<Doc_EventRecords>()).ToList();
            analysisResult.GUID = guid;
            Console.WriteLine(string.Format("*****加载分析结果耗时{0}ms*******", (DateTime.Now - dt).TotalMilliseconds));
            return analysisResult;
        }

        /// <summary>
        /// 修改指标结果
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool UpdateToTemporaryResultEx(string guid)
        {
            if (!string.IsNullOrEmpty(guid))
            {
                ///更新掉记录状态
                m_sqlCommand.Update(new Doc_MainViewRecord() { GUID = guid }, new Doc_MainViewRecord() { Progress = (int)ProgressState.Temporary });
                return true;
            }
            return true;
        }

        private string getstrMainViewTable()
        {
            return "select MainViewRecord.ID as ID, Patient.MedicalNo as PatientNo,Patient.OrderID as OrderID, Patient.PatientName as PatientName ,Patient.Age as Age,Patient.Gender as Sex,Doctor.Name as DoctorName ,Doctor.UserID as DoctorNo, RecordTime,Progress,GUID,CreatTime,VideoHave,DifferentVersion,Reserve3,EdfPath,Patient.Weight as Weight,Patient.Height as Height,MatchKey,FrameCount,ReviewMontageName,Reserve4,Reserve5 as ResultPath from MainViewRecord";
        }

        #region 首页病人表单的加载

        /// <summary>
        /// 在登录账号及选定模式下
        /// 查询所有的记录表单
        /// </summary>
        /// <remarks>
        /// 所有/不带任何条件
        /// </remarks>
        /// <returns></returns>
        public DataTable LoadMainView()
        {
            if (m_sqlCommand == null)
                return new DataTable();
            string strSelect = string.Format("{0}\r\n{1}\r\n{2}\r\n{3}",
                                            getstrMainViewTable(),
                                            "inner join Patient on MainViewRecord.PatientID=Patient.MedicalNo",
                                            "inner join Doctor on MainViewRecord.DoctorID=Doctor.ID",
                                            string.Format("where LoginID={0} and MainViewRecord.ModeType={1} ORDER BY CreatTime DESC LIMIT 100",
                                                        Channel.Default.Loginer.ID,
                                                        Channel.Default.SystemSetting.ModeType
                                                        )
                                            );
            return m_sqlCommand.Select(strSelect);
        }

        /// <summary>
        /// 在登录账号及选定模式下
        /// 选择性查询记录表单
        /// </summary>
        /// <remarks>
        /// 带条件：特定进度下的
        /// </remarks>
        /// <returns></returns>
        public DataTable LoadMainView(ProgressState state)
        {
            if (m_sqlCommand == null)
                return new DataTable();
            string strSelect = string.Format("{0}\r\n{1}\r\n{2}\r\n{3}",
                                               getstrMainViewTable(),
                                               "inner join Patient on MainViewRecord.PatientID=Patient.MedicalNo",
                                               "inner join Doctor on MainViewRecord.DoctorID=Doctor.ID",
                                               string.Format("where LoginID={0} and Progress={1} and MainViewRecord.ModeType={2} ORDER BY RecordTime DESC ",
                                                            Channel.Default.Loginer.ID,
                                                            (int)state,
                                                            Channel.Default.SystemSetting.ModeType
                                                            )
                                            );
            return m_sqlCommand.Select(strSelect);
        }

        /// <summary>
        /// 在登录账号及选定模式下
        /// 选择性查询记录表单
        /// </summary>
        /// <remarks>
        /// 带条件：起始时间
        /// </remarks>
        /// <param name="startTime">查询范围 开始时间</param>
        /// <param name="endTime">查询范围 截止时间</param>
        /// <returns></returns>
        public DataTable LoadMainView(DateTime startTime, DateTime endTime)
        {
            return LoadMainView("", "", "", startTime, endTime);
        }

        /// <summary>
        /// 在登录账号及选定模式下
        /// 选择性查询记录表单
        /// </summary>
        /// <remarks>
        /// 带条件：病例号、医师编号 、病人姓名、起/始时间
        /// 
        /// 支持对病例号和病人姓名进行模糊查询
        /// </remarks>
        /// <param name="patientID">病例号</param>
        /// <param name="DoctorID">医师编号</param>
        /// <param name="Name">病人姓名</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public DataTable LoadMainView(string patientID, string DoctorID, string Name, DateTime startTime, DateTime endTime)
        {
            if (m_sqlCommand == null)
                return new DataTable();
            string where = "where CreatTime >='{0:yyyy-MM-dd HH:mm:ss.fff}' and CreatTime<='{1:yyyy-MM-dd HH:mm:ss.fff}' and  LoginID={2} and MainViewRecord.ModeType={3} order by CreatTime desc";
            string strSelect = string.Format("{0}\r\n{1}\r\n{2}\r\n{3}",
                                            getstrMainViewTable(),
                                            string.Format("inner join Patient on MainViewRecord.PatientID=Patient.MedicalNo{0}{1}",
                                                            patientID == "" ? "" : string.Format(" and Patient.MedicalNo like '%{0}%'", patientID),
                                                            Name == "" ? "" : string.Format(" and Patient.PatientName like '%{0}%'", Name)),
                                            string.Format("inner join Doctor on MainViewRecord.DoctorID=Doctor.ID{0}",
                                                            DoctorID == "" ? "" : string.Format(" and Doctor.UserID='{0}'", DoctorID)),
                                            string.Format(where, startTime, endTime, Channel.Default.Loginer.ID, Channel.Default.SystemSetting.ModeType));
            return m_sqlCommand.Select(strSelect);
        }

        /// <summary>
        /// 在登录账号及选定模式下
        /// 选择性查询记录表单
        /// </summary>
        /// <remarks>
        /// 带条件：病例号、医师编号 、病人姓名
        /// 
        /// 支持对病例号和病人姓名进行模糊查询
        /// </remarks>
        /// <param name="patientID">病例号</param>
        /// <param name="DoctorID">医师编号</param>
        /// <param name="Name">病人姓名</param>
        /// <returns></returns>
        public DataTable LoadMainView(string patientID, string DoctorID, string Name)
        {
            if (m_sqlCommand == null)
                return new DataTable();
            string strSelect = string.Format("{0}\r\n{1}\r\n{2}\r\n{3}",
                                            getstrMainViewTable(),
                                            string.Format("inner join Patient on MainViewRecord.PatientID=Patient.MedicalNo{0}{1}",
                                                            patientID == "" ? "" : string.Format(" and Patient.MedicalNo like '%{0}%'", patientID),
                                                            Name == "" ? "" : string.Format(" and Patient.PatientName like '%{0}%'", Name)),
                                            string.Format("inner join Doctor on MainViewRecord.DoctorID=Doctor.ID{0}",
                                                            DoctorID == "" ? "" : string.Format(" and Doctor.UserID='{0}'", DoctorID)),
                                            string.Format("where LoginID={0} and MainViewRecord.ModeType={1} order by RecordTime desc",
                                                            Channel.Default.Loginer.ID,
                                                            Channel.Default.SystemSetting.ModeType
                                                            )
                                            );
            return m_sqlCommand.Select(strSelect);
        }

        /// <summary>
        /// 根据唯一标识符查询对应的记录表单
        /// </summary>
        /// <param name="id">MainViewRecord表的唯一标识符</param>
        /// <returns></returns>
        public DataTable LoadMainView(int id)
        {
            if (m_sqlCommand == null)
                return new DataTable();
            string strSelect = string.Format("{0}\r\n{1}\r\n{2}\r\n{3}", getstrMainViewTable(),
                                           "inner join Patient on MainViewRecord.PatientID=Patient.MedicalNo",
                                           "inner join Doctor on MainViewRecord.DoctorID=Doctor.ID",
                                            (string.Format("where LoginID={0} and MainViewRecord.ID={1} order by RecordTime desc",
                                            Channel.Default.Loginer.ID, id)));
            return m_sqlCommand.Select(strSelect);
        }
        #endregion

        /// <summary>
        /// 查询指定的项
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public T Select<T>(T instance) where T : ITable, new()
        {
            if (m_sqlCommand == null)
                return default(T);
            List<T> find = SelectAll(instance);
            if (find.Count > 0)
            {
                return find[0];
            }
            return default(T);
        }
        /// <summary>
        /// 范围内查找
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public List<T> SelectAll<T>(T instance) where T : ITable, new()
        {
            if (m_sqlCommand == null)
                return default(List<T>);
            return new TableConvert<T>().ConvertFromTable(m_sqlCommand.Select(instance));
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> Select<T>() where T : ITable, new()
        {
            if (m_sqlCommand == null)
                return default(List<T>);
            return new TableConvert<T>().ConvertFromTable(m_sqlCommand.Select(new T()));
        }
        /// <summary>
        /// 删除指定的数据
        /// </summary>
        /// <returns></returns>
        public bool Delete(ITable instance)
        {
            if (m_sqlCommand == null)
                return false;
            return m_sqlCommand.Delete(instance);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <returns></returns>
        public bool Delete<T>(List<T> instance) where T : ITable
        {
            if (m_sqlCommand == null)
                return false;
            return m_sqlCommand.Delete(instance);
        }
        /// <summary>
        /// 插入指定的数据
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public bool Insert(ITable instance)
        {
            if (m_sqlCommand == null)
                return false;
            return m_sqlCommand.Insert(instance);
        }
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public bool Insert<T>(List<T> instance) where T : ITable
        {
            if (m_sqlCommand == null)
                return false;
            return m_sqlCommand.Insert(instance);
        }
        /// <summary>
        /// 更新指定的数据
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public bool Update(ITable oldValue, ITable newValue)
        {
            if (m_sqlCommand == null)
                return false;
            return m_sqlCommand.Update(oldValue, newValue);
        }
        /// <summary>
        /// 更新指定的数据
        /// </summary>
        /// <param name="tables"></param>
        /// <returns></returns>
        public bool Update(Dictionary<ITable, ITable> tables)
        {
            if (m_sqlCommand == null)
                return false;
            return m_sqlCommand.Update(tables);
        }
        /// <summary>
        /// 判断指定数据是否存在
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public bool Exsit(ITable instance)
        {
            if (m_sqlCommand == null)
                return false;
            return m_sqlCommand.Exist(instance);
        }
    }
}
