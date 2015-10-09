using System;

namespace HousekeepingScheduler.Model
{
    internal static class Guard
    {
        public static void ThrowExceptionIfNull(object param, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void ThrowExceptionIfNullOrEmpty(string param, string paramName)
        {
            if (string.IsNullOrEmpty(param))
            {
                throw new ArgumentException(paramName);
            }
        }
    }
}
