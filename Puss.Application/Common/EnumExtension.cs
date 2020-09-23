using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Puss.Application.Common
{
    public static class EnumExtension
    {
        /// <summary>
        /// 根据EnumName获取Value
        /// </summary>
        /// <param name="enumType">枚举</param>
        /// <param name="enumName">备注</param>
        /// <returns></returns>
        public static int GetEnumValue(Type enumType, string enumName)
        {
            try
            {
                if (!enumType.IsEnum)
                    throw new ArgumentException("enumType必须是枚举类型");
                var values = Enum.GetValues(enumType);
                var ht = new Hashtable();
                foreach (var val in values)
                {
                    ht.Add(Enum.GetName(enumType, val), val);
                }
                return (int)ht[enumName];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public class EnumRemarkAttribute : Attribute
        {
            public string Remark { get; set; }
            public EnumRemarkAttribute(string remark)
            {
                this.Remark = remark;
            }
        }

        /// <summary>
        /// 获取当前枚举描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum enumValue)
        {
            try
            {
                Type type = enumValue.GetType();
                MemberInfo[] memInfo = type.GetMember(enumValue.ToString());
                if (null != memInfo && memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (null != attrs && attrs.Length > 0)
                        return ((DescriptionAttribute)attrs[0]).Description;
                }
                return enumValue.ToString();
            }
            catch (Exception)
            {
                return "Unknown";
            }
        }
    }
}
