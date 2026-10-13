using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MVCPro.BLL.CustomValidate
{
    public class NoDigitsAttribute : ValidationAttribute
    {
        public NoDigitsAttribute()
        {
            ErrorMessage = "The field must not contain digits.";
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return true;
            var s = value.ToString();
            if (string.IsNullOrWhiteSpace(s)) return true;
            return !Regex.IsMatch(s, @"\d");
        }
    }
}
