using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.UI.DataModel;
using AwareTec.Polysmith.UI.EnumModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.UI.CompatibleDbManager.EventDefineTable
{
    /// <summary>
    /// 兼容旧版本数据库，由于业务新增 需要调整数据库数据
    /// </summary>
    /// <remarks>
    /// 用于事件定义表的模式字段新增
    /// 
    /// 用法注释：
    /// 外部调用此函数(单条复制)： <see cref = "UpdateModeTypeInDb(ModeType, Doc_EventsDefine)" />
    /// 外部调用此函数(多条复制)： <see cref="BatchUpdateModeTypeInDb(ModeType, List{Doc_EventsDefine})"/>
    /// </remarks>
    public class ModeTypeCompatibleDbManager
    {
        /// <summary>
        /// 默认被复制的原数据 关键字段
        /// </summary>
        private static ModeType _DefaultModeType = ModeType.Adult;

        #region 公有函数
        /// <summary>
        /// 在数据库中批量复制
        /// </summary>
        /// <param name="mode">复制后新增字段的新值</param>
        /// <param name="eventDefines">被复制的原数据组</param>
        /// <returns>更新成功或不需要更新 都返回true</returns>
        public static bool BatchUpdateModeTypeInDb(ModeType mode, List<Doc_EventsDefine> originalDatas)
        {
            //检查输入
            if (originalDatas?.Count == 0)
                return false;

            //判断整组数据是否都已复制
            if (DatasHaveBeenCopied(mode, originalDatas))
                return true;

            List<Doc_EventsDefine> copiedDatas = new List<Doc_EventsDefine>();
            foreach(var item in originalDatas)
            {
                var newItem = GenerateNewData(mode, item);
                copiedDatas.Add(newItem);
            }
            return DataBaseHelper.Default.Insert(copiedDatas);
        }

        /// <summary>
        /// 在数据库中单条复制
        /// </summary>
        /// <param name="mode">复制后新增字段的新值</param>
        /// <param name="eventDefine">被复制的原数据</param>
        /// <returns>更新成功或不需要更新 都返回true</returns>
        public static bool UpdateModeTypeInDb(ModeType mode, Doc_EventsDefine originalData)
        {
            //检查输入
            if (originalData == null)
                return false;

            //判断该数据是否已复制
            if(DataHaveBeenCopied(mode, originalData))
                return true;

            var newData = GenerateNewData(mode, originalData); 
            return DataBaseHelper.Default.Insert(newData);
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 旧数据复制一份，调整新增字段的数据进行新数据的生成
        /// </summary>
        /// <returns>返回新数据</returns>
        private static Doc_EventsDefine GenerateNewData(ModeType mode, Doc_EventsDefine originalData)
        {
            var newData = originalData.Clone() as Doc_EventsDefine;

            newData.ModeType = (int)mode;

            return newData;
        }

        /// <summary>
        /// 该数据是否已被复制并存在数据库
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="originalData"></param>
        /// <returns></returns>
        private static bool DataHaveBeenCopied(ModeType mode, Doc_EventsDefine originalData)
        {
            Doc_EventsDefine searchData = new Doc_EventsDefine{
                ModeType = (int)mode,
                Name = originalData.Name
            };

            var result = DataBaseHelper.Default.Exsit(searchData);
            return result;
        }

        /// <summary>
        /// 判断整组数据是否都已复制并存在数据库
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="eventDefines"></param>
        /// <returns></returns>
        private static bool DatasHaveBeenCopied(ModeType mode, List<Doc_EventsDefine> originalDatas)
        {
            if (!FieldValueHaveBeenExist(mode))
                return false;//在整个表都不存在，说明这组数据也从没复制过

            foreach(var item in originalDatas)
            {
                if(!DataHaveBeenCopied(mode, item))//只要有其中一条数据是未复制的
                    return false;   //即刻返回false
            }

            return true;
        }

        /// <summary>
        /// 该字段的值是否存在于表中的某条数据
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        private static bool FieldValueHaveBeenExist(ModeType mode)
        {
            var result = DataBaseHelper.Default.Exsit(new Doc_EventsDefine()
            {
                ModeType = (int)mode
            });

            return result;
        }

        #endregion
    }
}
