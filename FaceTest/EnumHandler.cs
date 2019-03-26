using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FaceTest
{
    /// <summary>
    /// https://blog.csdn.net/u011400752/article/details/83818832
    /// string descprition = Plate.HS300.GetDescription(); //=沪深300
    /// string value = Plate.HS300.ToString(); // =HS300
    /// Plate plate1 = "HS300".GetValue<Plate>(); //=Plate.沪深300
    /// Plate plate = "沪深300".GetValueByDescription<Plate>();//=Plate.沪深300
    /// </summary>
    public static class EnumHandler
    {
        /// <summary>
        /// 获取枚举值的Description
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T value) where T : struct
        {
            string result = value.ToString();
            Type type = typeof(T);
            FieldInfo info = type.GetField(value.ToString());
            var attributes = info.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attributes != null && attributes.FirstOrDefault() != null)
            {
                result = (attributes.First() as DescriptionAttribute).Description;
            }

            return result;
        }

        /// <summary>
        /// 根据Description获取枚举值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetValueByDescription<T>(this string description) where T : struct
        {
            Type type = typeof(T);
            foreach (var field in type.GetFields())
            {
                if (field.Name == description)
                {
                    return (T)field.GetValue(null);
                }

                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attributes != null && attributes.FirstOrDefault() != null)
                {
                    if (attributes.First().Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            throw new ArgumentException(string.Format("{0} 未能找到对应的枚举.", description), "Description");
        }

        /// <summary>
        /// 获取string获取枚举值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetValue<T>(this string value) where T : struct
        {
            T result;
            if (Enum.TryParse(value, true, out result))
            {
                return result;
            }

            throw new ArgumentException(string.Format("{0} 未能找到对应的枚举.", value), "Value");
        }

    }
}
