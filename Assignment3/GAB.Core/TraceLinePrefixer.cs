namespace GAB.Core
{
    using System;

    public static class TraceLinePrefixer
    {
        public static string GetConsoleLinePrefix()
        {
            return string.Format("{0}[{1}] -> ", FormattingConstants.NewLine, DateTime.Now.ToLongTimeString());
        }
    }
}