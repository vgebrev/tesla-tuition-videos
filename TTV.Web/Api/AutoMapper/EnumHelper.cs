using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TTV.Web.Api.AutoMapper;

public static class EnumHelper
{
    public static string GetEnumDescription<T>(this T enumerationValue)
    where T : struct
    {
        Type type = enumerationValue.GetType();
        if (!type.IsEnum)
        {
            throw new ArgumentException($"{type.Name} must be an Enum", $"{nameof(enumerationValue)}");
        }

        MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString() ?? string.Empty);
        if (memberInfo != null && memberInfo.Length > 0)
        {
            object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
            {
                return ((DescriptionAttribute)attrs[0]).Description ?? string.Empty;
            }
            else
            {
                object[] displayAttrs = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                if (displayAttrs != null && displayAttrs.Length > 0)
                {
                    return ((DisplayAttribute)displayAttrs[0]).Name ?? string.Empty;
                }
            }
        }

        return enumerationValue.ToString() ?? string.Empty;
    }
}

