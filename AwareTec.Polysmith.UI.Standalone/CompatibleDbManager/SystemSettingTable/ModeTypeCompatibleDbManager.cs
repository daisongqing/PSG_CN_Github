using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.UI.EnumModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.UI.CompatibleDbManager.SystemSettingTable
{

    /// <summary>
    /// 兼容旧版本数据库，由于业务新增 需要调整数据库数据
    /// </summary>
    /// <remarks>
    /// 用于系统设置表的模式字段新增
    /// 
    /// 用法注释：
    /// 外部调用此函数： <see cref = "UpdateModeTypeInDb(ModeType, Doc_UsersInfo)" />
    /// </remarks>
    public class ModeTypeCompatibleDbManager
    {
        /// <summary>
        /// 默认被复制的原数据 关键字段
        /// </summary>
        private static ModeType _DefaultModeType = ModeType.Adult;

        /// <summary>
        /// 在数据库中更新
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="user"></param>
        /// <returns>更新成功或不需要更新 都返回true</returns>
        public static bool UpdateModeTypeInDb(ModeType mode, Doc_UsersInfo user)
        {
            var systemSetting = DataModel.DataBaseHelper.Default.SelectAll(new Doc_SystemSetting()
            {
                UserID = user.ID,
                ModeType = (int)mode
            });

            if (systemSetting.Count > 0)
                return true;

            var copiedTable = DataModel.DataBaseHelper.Default.Select(new Doc_SystemSetting()
                            {
                                UserID = user.ID,
                                ModeType = (int)_DefaultModeType
                            });
            return InsertNewData(mode, copiedTable);
        }

        /// <summary>
        /// 旧数据复制一份，调整新增字段的数据再将其插入
        /// </summary>
        /// <returns>插入成功返回true</returns>
        private static bool InsertNewData(ModeType mode, Doc_SystemSetting copiedTable)
        {
            var newTable = copiedTable.Clone() as Doc_SystemSetting;
            var oldTable = DataModel.DataBaseHelper.Default.SelectAll(new Doc_SystemSetting(){});

            newTable.ModeType = (int)mode;

            return DataModel.DataBaseHelper.Default.Insert(newTable);
        }
    }
}
