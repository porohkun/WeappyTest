namespace System
{
    public static class StringExtensions
    {
        public static string CropLast(this string value, int count)
        {
            return value.Substring(0, value.Length - count);
        }
    }
}
