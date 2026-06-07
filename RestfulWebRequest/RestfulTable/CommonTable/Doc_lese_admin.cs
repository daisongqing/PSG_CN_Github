using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RestfulWebRequest.RestfulTable.CommonTable
{
    /// <summary>
    /// 账户管理
    /// </summary>
    public class Doc_lese_admin : IRestfulTable
    {
        #region 公有成员
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public DateTime LastLoginLtime { get; set; }
        public int Status { get; set; }
        public int RoleId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime DeleteTime { get; set; }
        public DateTime PsgLastLoginTime { get; set; }
        public string Pic { get; set; }
        #endregion
    }
}
