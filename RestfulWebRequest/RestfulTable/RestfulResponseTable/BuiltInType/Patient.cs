using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType
{
    public class Patient
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string id;
        /// <summary>
        /// 病例号
        /// </summary>
        public string medicalNo;
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string name;
        /// <summary>
        /// 性别
        /// </summary>
        public bool gender;
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime birthday;
        /// <summary>
        /// 年龄
        /// </summary>
        public int age;
        /// <summary>
        /// 身高
        /// </summary>
        public double height;
        /// <summary>
        /// 体重
        /// </summary>
        public double weight;
        /// <summary>
        /// BMI
        /// </summary>
        public double bmi;
        /// <summary>
        /// 手机号
        /// </summary>
        public string phone;
        /// <summary>
        /// 身份证号
        /// </summary>
        public string idNo;
        /// <summary>
        /// 联系地址
        /// </summary>
        public string address;
        /// <summary>
        /// 医疗中心id
        /// </summary>
        public string hospitalId;
    }
}
