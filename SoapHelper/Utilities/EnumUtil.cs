using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SoapHelper.Utilities
{
    internal static class EnumUtil
    {
        public static List<string> GetEnumValuesAsArray(Type enumType)
        {
            FieldInfo[] fields = enumType.GetFields();
            List<string> names = new List<string>();

            // 遍历FieldInfo数组，提取每个字段的名称并添加到列表中
            foreach (FieldInfo field in fields)
            {
                string name = field.Name;
                if (!Enum.IsDefined(enumType, name))
                {
                    throw new InvalidOperationException("Unexpected enum value found");
                }
                names.Add(name);
            }

            // 将List转换为数组返回
            return names;
        }

        public static string GetEnumValuesAsString(Type enumType)
        {
            FieldInfo[] fields = enumType.GetFields();
            List<string> names = new List<string>();
            var sb = new StringBuilder();

            // 遍历FieldInfo数组，提取每个字段的名称并添加到列表中
            for (int i = 1; i < fields.Length; i++)
            {
                string name = fields[i].Name;
                //if (!Enum.IsDefined(enumType, name))
                //{
                //    throw new InvalidOperationException("Unexpected enum value found");
                //}
                sb.Append(name);

                if(i == fields.Length - 1)
                {
                    sb.Append(".");
                }
                else
                {
                    sb.Append(",");
                }
            }

            return sb.ToString();
        }
    }
}
