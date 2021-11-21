using System;

namespace Yabe.Application.Extensions
{
    public static class StringFormattingExtensions
    {
        public static string ToFriendlyBytesString(this int bytes)
        {
            return ToFriendlyBytesString((long)bytes);
        }

        public static string ToFriendlyBytesString(this long bytes)
        {
            if (bytes < 0)
                return $"-{ToFriendlyBytesString(-bytes)}";

            if (bytes < 1024)
                return $"{bytes} B";

            string[] suffix = { "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

            var exponent = (int)(Math.Log(bytes) / Math.Log(1024));
            var scaled = bytes / Math.Pow(1024, exponent);

            return $"{scaled:F2} {suffix[exponent - 1]}";
        }
    }
}
