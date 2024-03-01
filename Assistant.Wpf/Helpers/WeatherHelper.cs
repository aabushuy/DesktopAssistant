using System;

namespace Assistant.Wpf.Helpers
{
    internal static class WeatherHelper
    {
        public static string TempToString(this double temp)
        {
            var sign = temp > 0
                ? "+"
                : temp < 0
                    ? "-"
                    : string.Empty;

            return $"{sign}{Math.Abs(Math.Round(temp))}°C";
        }

        public static int HPaToMmHg(this double hPa) => (int)Math.Round(hPa * 0.750062);
    }
}
