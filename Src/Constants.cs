using NClicker.Models;
using System;
using System.Runtime.InteropServices;

namespace NClicker
{
    public static class Constants
    {
        public const string PaypalUrl = "https://paypal.me/nakamaa";

        public static readonly bool IsNetCore = RuntimeInformation.FrameworkDescription
            .StartsWith(".NET Core", StringComparison.OrdinalIgnoreCase);

        public static RunConfiguration DefaultConfig => new RunConfiguration
        {
            Seconds = 1,
            Milliseconds = 35,
            RandomSeconds = 0,
            RandomMilliseconds = 35,
            BlockInput = true,
        };

        public static readonly string LiteDbConnectionString = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\NClicker\LocalDB.db";
    }
}