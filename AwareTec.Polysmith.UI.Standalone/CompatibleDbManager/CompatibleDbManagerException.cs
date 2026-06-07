using pSystem.Interface.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.UI.CompatibleDbManager
{
    public class CompatibleDbManagerException : Exception
    {
        private static string _defaultMsg = @"
                                    {{
                                        数据库兼容出现错误, 兼容管理器为：{0}
                                        兼容的数据库表格为：{1}
                                        兼容的字段为：{2}
                                    }}";

        public CompatibleDbManagerException(Type manager, 
                                            Type table,
                                            Type enumType
                                            ):
                                            base(string.Format(_defaultMsg,
                                                                manager.ToString(),
                                                                table.ToString(),
                                                                enumType.ToString()))
        {}
    }
}