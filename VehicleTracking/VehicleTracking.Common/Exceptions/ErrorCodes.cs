using System.ComponentModel;

namespace VehicleTracking.Common.Exceptions
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
        EC_User_003,

        /// <summary>
        /// Vehicle doesn't existed
        /// </summary>
        [Description("This vehicle doesn't existed or doesn't belong to current user - Code: {0}")]
        EC_Vehicle_001,

        /// <summary>
        /// Vehicle is existed
        /// </summary>
        [Description("This vehicle code is existed - Code: {0}")]
        EC_Vehicle_002,
    }
}
