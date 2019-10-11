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

        /// <summary>
        /// Session doesn't existed
        /// </summary>
        [Description("This session doesn't existed - Id: {0}")]
        EC_Session_001,

        /// <summary>
        /// There is no session in progress
        /// </summary>
        [Description("There is no session in progress for this vehicle - Vehicle Id: {0}")]
        EC_Session_002,

        /// <summary>
        /// Can not retrieve current location 
        /// </summary>
        [Description("Can not retrieve current location of this vehicle - Vehicle Id: {0}")]
        EC_Location_001,

        /// <summary>
        /// Can not retrieve journey 
        /// </summary>
        [Description("Can not retrieve journey of this vehicle - Vehicle Id: {0}")]
        EC_Location_002,
    }
}
