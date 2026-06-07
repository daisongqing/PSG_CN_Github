using RestfulWebRequest.EnumModels.EnumModels4Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType
{
    public class OrderItem : IRestfulTable
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string no;
        /// <summary>
        /// 患者名称
        /// </summary>
        public string patientName;
        /// <summary>
        /// 病例号
        /// </summary>
        public string medicalNo;
        /// <summary>
        /// 预约检测时间
        /// </summary>
        public DateTime examTime;
        /// <summary>
        /// 监测类型
        /// </summary>
        public ExamType examType;
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatus status;
        /// <summary>
        /// 医生名称
        /// </summary>
        public string doctor;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime creationTime;
        /// <summary>
        /// 模式
        /// </summary>
        public ModeType mode;
        /// <summary>
        /// 是否为定时任务
        /// </summary>
        public bool isTimedTask;
        /// <summary>
        /// 患者
        /// </summary>
        public Patient patient;
        /// <summary>
        /// 审核状态
        /// </summary>
        public ExamineStatus? examineStatus;
        /// <summary>
        /// 订单唯一标识符
        /// </summary>
        public string id;
        /// <summary>
        /// 开始监测时间
        /// </summary>
        public DateTime? ActualExamTime;
    }
}
