using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newshore.TechnicalTest.Transverse.Utils
{ 
    public static class LogUtils
    {
        public static void WriteErrorLog(string message, Exception exception, params object?[]? propertyValues)
        {
            string errorMessage = GetErrorMessage(exception);

            Log.Fatal(message + errorMessage, propertyValues);
        }

        private static string GetErrorMessage(Exception exception)
        {
            string errorMessage = string.Empty;

            if (exception != null)
            {
                errorMessage += $" -- [{exception.Message}].";

                if (exception.InnerException != null)
                {
                    errorMessage += GetErrorMessage(exception.InnerException);
                }
            }

            return errorMessage;
        }
    }
}
