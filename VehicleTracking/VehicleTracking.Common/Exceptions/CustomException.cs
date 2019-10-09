using System;
using VehicleTracking.Common.Helpers;

namespace VehicleTracking.Common.Exceptions
{
    public class CustomException : Exception
    {
        public string ErrorCode { get; }

        public string ErrorMessage { get; }

        public CustomException(ErrorCodes error)
        {
            ErrorCode = error.ToString();
            ErrorMessage = error.GetDescriptionFromEnumValue();
        }

        public CustomException(ErrorCodes error, params object[] values)
        {
            ErrorCode = error.ToString();
            ErrorMessage = string.Format(error.GetDescriptionFromEnumValue(), values);
        }
    }
}
