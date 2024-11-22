using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum OrderStatusTypeEnums
    {
        Pending,
        Approved,
        Rejected
    }

    public class StringToEnumConverter<TEnum> : IValueConverter<string, TEnum?>
    where TEnum : struct, Enum
    {
        public TEnum? Convert(string sourceMember, ResolutionContext context)
        {
            if (Enum.TryParse<TEnum>(sourceMember, true, out var result))
            {
                return result;
            }
            return null; // Hoặc giá trị mặc định nếu cần
        }
    }
    public class EnumToStringConverter<TEnum> : IValueConverter<TEnum?, string>
where TEnum : struct, Enum
    {
        public string Convert(TEnum? sourceMember, ResolutionContext context)
        {
            return sourceMember?.ToString() ?? string.Empty; // Trả về chuỗi rỗng nếu null
        }
    }
}
