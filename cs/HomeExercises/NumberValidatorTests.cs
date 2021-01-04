using System;
using FluentAssertions;
using NUnit.Framework;

namespace HomeExercises
{
	public class NumberValidatorTests
	{
		[Test]
		[TestCase(17, 2, true, "0.0", TestName = "WhenNumberWithZeroFraction")]
		[TestCase(17, 2, true, "0", TestName = "WhenWithoutFraction")]
		[TestCase(4, 2, true, "+1.23", TestName = "WhenPositiveWithFraction")]
		[TestCase(4, 2, false, "-1.23", TestName = "WhenNegativeWithFraction")]
		public void NumberValidator_ShouldCorrectlyWork_OnValidNumbers(
			int precision, int scale, bool onlyPositive, string value)
		{
			var validator = new NumberValidator(precision, scale, onlyPositive);
			validator.IsValidNumber(value).Should().BeTrue($"{validator} failed on input {value}");
		}

		[Test]
		[TestCase(3, 2, true, "+1.23", TestName = "DigitCountGreaterThanPrecision")]
		[TestCase(17, 2, true, "0.000", TestName = "FractionGreaterThanScale")]
		[TestCase(4, 2, true, "-1.23", TestName = "NegativeValueWhenOnlyPositive")]
		[TestCase(3, 2, true, "wrong.data", TestName = "WrongData")]
		[TestCase(3, 2, true, "", TestName = "EmptyValue")]
		[TestCase(3, 2, true, null, TestName = "NullValue")]
		
		public void NumberValidator_ShouldCorrectlyWork_OnInvalidNumbers(
			int precision, int scale, bool onlyPositive, string value)
		{
			var validator = new NumberValidator(precision, scale, onlyPositive);
			validator.IsValidNumber(value).Should().BeFalse($"{validator} failed on input {value}");
		}
		

		[Test]
		[TestCase(-1, 2, TestName = "WhenNegativePrecision")]
		[TestCase(0, 2, TestName = "WhenZeroPrecision")]
		[TestCase(2, 4, TestName = "WhenScaleGraterThanPrecision")]
		[TestCase(2, 2, TestName = "WhenPrecisionEqualScale")]
		[TestCase(2,-2, TestName = "WhenScaleNegative")]
		public void NumberValidator_ShouldThrowExceptionOnInvalidArguments(int precision, int scale)
		{
			Assert.Throws<ArgumentException>(() => new NumberValidator(precision, scale));
		}
	}
}