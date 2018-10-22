using System.Text.RegularExpressions;

namespace TimeTracker.Utils
{
    public interface ICaseConverter
    {
        string ToKebabCase(string value, string separator = "-");
    }

    public class CaseConverter : ICaseConverter
    {
        public string ToKebabCase(string value, string separator = "-")
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return Regex.Replace(
                    value,
                    "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
                    $"{separator}$1",
                    RegexOptions.Compiled)
                .Trim()
                .ToLower();
        }
    }
}