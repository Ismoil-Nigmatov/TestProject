using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TestProject.ValidateAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class CustomEmailAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "Please enter a valid email address.";
        private readonly string _emailPattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

        public CustomEmailAttribute() : base(DefaultErrorMessage) { }

        public override bool IsValid(object value)
        {
            if (value == null || !(value is string))
            {
                return false;
            }

            string email = (string)value;
            return Regex.IsMatch(email, _emailPattern, RegexOptions.IgnoreCase);
        }
    }
}
