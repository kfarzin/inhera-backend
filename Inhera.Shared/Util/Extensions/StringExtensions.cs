using System.Numerics;
using System.Text.Json;

namespace Inhera.Shared.Util.Extensions
{
    public static class StringExtensions
    {
        public const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string ToStringOrEmpty(this string? entry)
        {
            try
            {
                var result = entry != null
                    ? entry
                    : "";
                return result;
            }
            catch
            {
                return "";
            }
        }

        public static bool IsEmptyOrNull(this string? entry)
        {
            try
            {
                if (entry == null || entry == "")
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return true;
            }
        }

        public static bool IsNotEmptyOrNull(this string? entry)
        {
            try
            {
                if (entry == null || entry == "")
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static TimeOnly? ToNullableTimeOnly(this string? entry)
        {
            try
            {
                var result = TimeOnly.Parse(entry!);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public static long FromBase36(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Empty value.");
            value = value.ToUpper();
            bool negative = false;
            if (value[0] == '-')
            {
                negative = true;
                value = value.Substring(1, value.Length - 1);
            }
            if (value.Any(c => !Digits.Contains(c)))
                throw new ArgumentException("Invalid value: \"" + value + "\".");
            var decoded = 0L;
            for (var i = 0; i < value.Length; ++i)
                decoded += Digits.IndexOf(value[i]) * (long)BigInteger.Pow(Digits.Length, value.Length - i - 1);
            return negative ? decoded * -1 : decoded;
        }

        public static string[] ToJsonStringArray(this string? entry)
        {
            try
            {                
                var result = entry.IsNotEmptyOrNull()
                    ? JsonSerializer.Deserialize<string[]>(entry!)
                    : default;
                return result!;
            }
            catch
            {
                return default;
            }
        }

        public static IDictionary<string, object> ToJsonObjectArray(this string? entry)
        {
            try
            {
                var result = entry.IsNotEmptyOrNull()
                    ? JsonSerializer.Deserialize<IDictionary<string, object>>(entry!)
                    : default;
                return result!;
            }
            catch
            {
                return default;
            }
        }
    }
}