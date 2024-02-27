namespace DesafioB3.Service.Extensions;

public static class StringExtension
{
	public static string OnlyNumber(this string value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return "";
		}

		var result = string.Concat(value.Where(char.IsDigit));
		return result;
	}
}