using System;
using System.Text.RegularExpressions;

namespace HomeExercises
{
	public class NumberValidator
	{
		private readonly Regex numberRegex = new Regex(@"^([+-]?)(\d+)([.,](\d+))?$",
		                                               RegexOptions.IgnoreCase |
		                                               RegexOptions.Compiled);
		private readonly bool onlyPositive;
		private readonly int precision;
		private readonly int scale;

		public NumberValidator(int precision, int scale = 0, bool onlyPositive = false)
		{
			this.precision = precision;
			this.scale = scale;
			this.onlyPositive = onlyPositive;
			CheckParameters();
		}

		private void CheckParameters()
		{
			if (precision <= 0)
				throw new ArgumentException("precision must be a positive number");
			if (scale < 0 || scale >= precision)
				throw new
					ArgumentException("precision must be a non-negative number less or equal than precision");
		}

		public bool IsValidNumber(string value)
		{
			if (string.IsNullOrEmpty(value))
				return false;

			var match = numberRegex.Match(value);
			if (!match.Success)
				return false;

			var intPart = match.Groups[1].Value.Length + match.Groups[2].Value.Length;
			var fracPart = match.Groups[4].Value.Length;

			if (intPart + fracPart > precision || fracPart > scale)
				return false;

			return !onlyPositive || match.Groups[1].Value != "-";
		}

		public override string ToString()
		{
			return $"Precision: {precision};\nScale: {scale}\nOnlyPositive: {onlyPositive}";
		}
	}
}