using AwareTec.Polysmith.Cloud.UI.Base;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.Base;
using AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.BuiltInType;
using AwareTec.Polysmith.Util.EnumUtils;
using RestfulWebRequest.EnumModels.EnumModels4Table;
using RestfulWebRequest.RestfulTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.FromDb
{
    public class OrderRecordViewModel: IFromDb
    {
        #region Fields
        /// <summary>
        /// 订单号
        /// </summary>
        private ImageAndStringCell _orderNumber;
        /// <summary>
        /// 类别
        /// </summary>
        private string _category;
        /// <summary>
        /// 病例号
        /// </summary>
        private string _medicalRecordNumber = "*************";
        /// <summary>
        /// 病人姓名
        /// </summary>
        private string _patientName;
        /// <summary>
        /// 病人性别
        /// </summary>
        private string _patientGender;
        /// <summary>
        /// 病人年龄
        /// </summary>
        private string _patientAge;
        /// <summary>
        /// 病人BMI
        /// </summary>
        private string _patientBMI;
        /// <summary>
        /// 预约监测时间
        /// </summary>
        private string _appointmentMonitoringTime;
        /// <summary>
        /// 文件传输进度
        /// </summary>
        private string _fileTransmissionProgress = "- -";
        /// <summary>
        /// 进度
        /// </summary>
        private ImageAndStringCell _progressRate;
        /// <summary>
        /// 审核状态
        /// </summary>
        private ImageAndStringCell _approvalStatus;
        /// <summary>
        /// 判图医师
        /// </summary>
        private string _mapInterpnetationDoctorName;
        /// <summary>
        /// 图谱
        /// </summary>
        private string _atlas = "- -";
        /// <summary>
        /// 报告
        /// </summary>
        private string _report = "- -";

        private string _creationTime;
        #endregion

        #region Properties
        [ViewModel("OrderNumberCol")]
        public ImageAndStringCell OrderNumber => _orderNumber;
        [ViewModel("CategoryCol")]
        public string Category => _category;
        [ViewModel("MedicalRecordNumberCol")]
        public string MedicalRecordNumber => _medicalRecordNumber;
        [ViewModel("PatientNameCol")]
        public string PatientName => _patientName;
        [ViewModel("PatientGenderCol")]
        public string PatientGender => _patientGender;
        [ViewModel("PatientAgeCol")]
        public string PatientAge => _patientAge;
        [ViewModel("PatientBMICol")]
        public string PatientBMI => _patientBMI;
        [ViewModel("AppointmentMonitoringTimeCol")]
        public string AppointmentMonitoringTime => _appointmentMonitoringTime;
        [ViewModel("ApprovalStatusCol")] 
        public ImageAndStringCell ApprovalStatus => _approvalStatus;
        [ViewModel("UploadAndDownloadProgressCol")]
        public string FileTransmissionProgress => _fileTransmissionProgress;
        [ViewModel("ProgressRateCol")]
        public ImageAndStringCell ProgressRate => _progressRate;
        [ViewModel("MapInterpnetationDoctorNameCol")]
        public string MapInterpnetationDoctorName => _mapInterpnetationDoctorName;
        [ViewModel("AtlasCol", AlwaysVisible = false)]
        public string Atlas => _atlas;
        [ViewModel("ReportCol")]
        public string Report => _report;
        [ViewModel("OrderCreationTimeCol")]
        public string CreationTime => _creationTime;
        #endregion

        #region RestfulTable对象
        private OrderItem _orderItem;
        public OrderItem OrderItem => _orderItem;
        #endregion

        public void FromDb(IRestfulTable restfulTable, object otherInitParams)
        {
            var data = restfulTable as OrderItem;
            _orderItem = data;

            this._appointmentMonitoringTime = data.examTime.ToString();
            
            _mapInterpnetationDoctorName = data.doctor;
            _approvalStatus = data.examineStatus == null ? null :GetApprovalStatusViewModel(data.examineStatus.Value);
            _category = EnumHelper.GetDescription(data.examType);
            _medicalRecordNumber = data.medicalNo;
            _orderNumber = new ImageAndStringCell()
            {
                DisplayName = data.no,
                Image = data.isTimedTask ? Properties.Resources.SetTimeOK : null
            };
            _patientAge = data.patient.age.ToString();
            _patientBMI = data.patient.bmi.ToString();
            _patientGender = data.patient.gender ? "男" : "女";
            _patientName = data.patientName;
            _progressRate = GetOrderStatusViewCell(data.status);
            if (data.status == OrderStatus.Completed)
            {
                _report = "查看";
                _atlas = "查看";
            }
            _creationTime = data.creationTime.ToString();
        }

        private ImageAndStringCell GetApprovalStatusViewModel(ExamineStatus status)
        {
            Image image = null;
            switch(status)
            {
                case ExamineStatus.NotReviewed:
                    image = Properties.Resources.NotReviewed;
                    break;
                case ExamineStatus.Passed:
                    image = Properties.Resources.AuditPassed;
                    break;
                case ExamineStatus.NoPassed:
                    image = Properties.Resources.AuditNoPassed;
                    break;
                default:
                    throw new NotSupportedException(GlobalReadonlyString.CommonException.EnumOutOfExpectedRange);
            }
            return new ImageAndStringCell()
            {
                DisplayName = EnumHelper.GetDescription(status),
                Image = image
            };
        }

        private ImageAndStringCell GetOrderStatusViewCell(OrderStatus orderStatus)
        {
            Image image = null;
            switch (orderStatus)
            {
                case OrderStatus.ToBeMonitored:
                    image = Properties.Resources.ToBeMonitored;
                    break;
                case OrderStatus.ToBeUpload:
                    image = Properties.Resources.ToBeUpload;
                    break;
                case OrderStatus.ToBeAnalysis:
                    image = Properties.Resources.ToBeAnalysis;
                    break;
                case OrderStatus.ToBeAudit:
                    image = Properties.Resources.ToBeAudit;
                    break;
                case OrderStatus.Completed:
                    image = Properties.Resources.complete2;
                    break;
                case OrderStatus.AppointmentCancellation:
                    break;
                default:
                    throw new NotSupportedException(GlobalReadonlyString.CommonException.EnumOutOfExpectedRange);
            }
            return new ImageAndStringCell()
            {
                DisplayName = EnumHelper.GetDescription(orderStatus),
                Image = image
            };
        }
    }
}
