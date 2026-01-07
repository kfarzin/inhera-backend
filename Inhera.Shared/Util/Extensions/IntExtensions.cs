namespace Inhera.Shared.Util.Extensions
{
    public static class IntExtensions
    {
        public static string ToBase36(this int entry)
        {
            try
            {
                bool negative = entry < 0;
                entry = Math.Abs(entry);
                string encoded = string.Empty;
                do
                    encoded = StringExtensions.Digits[(int)(entry % StringExtensions.Digits.Length)] + encoded;
                while ((entry /= StringExtensions.Digits.Length) != 0);
                return negative ? "-" + encoded : encoded;
            }
            catch
            {
                return "";
            }
        }
    }
}
