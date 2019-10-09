using System.ComponentModel;

namespace VehicalTracking.Common.Exceptions
{
    public enum ErrorCodes
    {
        /// <summary>
        /// User doesn't existed
        /// </summary>
        [Description("User doesn't existed - Email: {0}")]
        EC_User_001,

        /// <summary>
        /// Password is incorrect
        /// </summary>
        [Description("The password you entered is incorrect")]
        EC_User_002,

        /// <summary>
        /// Create user unsuccessful
        /// </summary>
        [Description("Create user unsuccessful. {0}")]
        EC_User_003
    }
}
