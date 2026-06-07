using System;
using System.Collections.Generic;

namespace AwareTec.Polysmith.Util.EnumUtils
{
    /// <summary>
    /// 枚举映射成对象的泛型工具类
    /// </summary>
    public class EnumMappingObject<T>
    {
        #region 私有(仅供内部调用)
        private string _name = string.Empty;

        private string _description = string.Empty;

        private T _value = default(T);

        #endregion

        #region 公有属性(仅供外调用)
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public T Value
        {
            get => _value;
            set => _value = value;
        }
        #endregion

        #region 公有方法
        public static EnumMappingObject<T>[] ParseEnum()
        {
            List<EnumMappingObject<T>> list = new List<EnumMappingObject<T>>();
            if (Enum.GetValues(typeof(T)).Length < 1)
                return list.ToArray();

            foreach (object o in Enum.GetValues(typeof(T)))
            {
                list.Add(new EnumMappingObject<T>
                {
                    _name = Enum.GetName(typeof(T), o),
                    _value = (T)o,
                    _description = EnumHelper.GetDescription(o)
                });
            }

            return list.ToArray();
        }

        #endregion
    }
}
