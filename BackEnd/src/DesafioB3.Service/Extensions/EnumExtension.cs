using System.ComponentModel;

namespace DesafioB3.Service.Extensions;

public static class EnumExtension
{
	public static TEnum IntToEnum<TEnum>(this int value)
	{
		if (Enum.IsDefined(typeof(TEnum), value))
		{
			return (TEnum)Enum.ToObject(typeof(TEnum), value);
		}

		return default;
	}

	public static string GetEnumDescription<TEnum>(this TEnum enumValue) where TEnum : Enum
	{
		var field = typeof(TEnum).GetField(enumValue.ToString());
		var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

		return attribute != null ? attribute.Description : enumValue.ToString();
	}
}