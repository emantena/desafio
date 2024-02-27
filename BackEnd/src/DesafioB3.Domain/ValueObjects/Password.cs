using DesafioB3.Domain.Enums;
using BC = BCrypt.Net.BCrypt;

namespace DesafioB3.Domain.ValueObjects;
public static class Password
{
	public static string HashPassword(string plainText)
	{
		return BC.HashPassword(plainText);
	}

	public static bool IsMatch(string password, string hash)
	{
		if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hash))
		{
			return false;
		}

		return BC.Verify(password, hash);
	}

	public static PasswordStrength GetPasswordStrength(string password)
	{
		var score = 0;
		if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(password.Trim()))
		{
			return PasswordStrength.Blank;
		}

		if (HasMinimumLength(password, 5))
		{
			score++;
		}

		if (HasMinimumLength(password, 8))
		{
			score++;
		}

		if (HasUpperCaseLetter(password) && HasLowerCaseLetter(password))
		{
			score++;
		}

		if (HasDigit(password))
		{
			score++;
		}

		if (HasSpecialChar(password))
		{
			score++;
		}

		return (PasswordStrength)score;
	}

	public static bool IsStrongPassword(string password)
	{
		return HasMinimumLength(password, 8)
			&& HasUpperCaseLetter(password)
			&& HasLowerCaseLetter(password)
			&& (HasDigit(password) || HasSpecialChar(password));
	}

	private static bool HasMinimumLength(string password, int minLength)
	{
		return password.Length >= minLength;
	}

	private static bool HasDigit(string password)
	{
		return password.Any(c => char.IsDigit(c));
	}

	private static bool HasSpecialChar(string password)
	{
		return password.IndexOfAny("!@#$%^&*?_~-£().,".ToCharArray()) != -1;
	}

	private static bool HasUpperCaseLetter(string password)
	{
		return password.Any(c => char.IsUpper(c));
	}

	private static bool HasLowerCaseLetter(string password)
	{
		return password.Any(c => char.IsLower(c));
	}
}