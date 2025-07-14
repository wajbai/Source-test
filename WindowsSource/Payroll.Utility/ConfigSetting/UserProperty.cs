/*  Class Name      : UserProperty
 *  Purpose         : Define/access user info for currently logged in user
 *  Author          : CS
 *  Created on      : 8-Jul-2010
 */

using System;
using System.Data;
using System.Collections.Generic;
using Payroll.Utility;

namespace Payroll.Utility.ConfigSetting
{
    public class UserProperty : SettingProperty
    {
        private static DataView dvUser = null;
        private const string UserIdField = "USER_ID";
        private const string UserNameField = "USER_NAME";
        private const string UserFullNameField = "NAME";
        private const string UserAddressField = "ADDRESS";
        private const string RoleIdField = "ROLE_ID";
        private const string UserMobileNoField = "MOBILE_NO";
        private const string UserEmailIdField = "EMAIL_ID";


        private string GetUserInfo(string name)
        {
            string val = "";

            if (dvUser != null && dvUser.Count > 0)
            {
                val = dvUser[0][name].ToString();
            }

            return val;
        }

        /// <summary>
        /// Set User Info as Dataview
        /// </summary>
        public DataView UserInfo
        {
            set
            {
                UserProperty.dvUser = value;
            }
        }

        /// <summary>
        /// Get logged in User Id
        /// </summary>
        public string UserId
        {
            get
            {
                string userId = GetUserInfo(UserIdField);
                if (userId == "") userId = "0";
                return userId;
            }
        }

        /// <summary>
        /// Get User is Logged in
        /// </summary>
        public bool HasUser
        {
            get
            {
                int userId = 0;
                int.TryParse(UserId, out userId);
                return (userId > 0);
            }
        }

        /// <summary>
        /// Get logged in user type is Admin
        /// </summary>
        /*public bool HasAdmin
        {
            get { return (this.UserType == UserType.Admin); }
        }*/

        /// <summary>
        /// Get Name of the logged in user
        /// </summary>
        public string UserName
        {
            get 
            { 
                return GetUserInfo(UserNameField);
            }
        }

        /// <summary>
        /// Get Full Name of the logged in user
        /// </summary>
        public string Name
        {
            get { return GetUserInfo(UserFullNameField); }
        }

        /// <summary>
        /// Get Address of the logged in user
        /// </summary>
        public string Address
        {
            get { return GetUserInfo(UserAddressField); }
        }

        /// <summary>
        /// Get Mobile No of the logged in user
        /// </summary>
        public string MobileNo
        {
            get { return GetUserInfo(UserMobileNoField); }
        }

        /// <summary>
        /// Get logged in user Email Id
        /// </summary>
        public string EmailId
        {
            get { return GetUserInfo(UserEmailIdField); }
        }

        public int RoleId
        {
            get 
            {
                int roleId = 0;
                string roleNum = GetUserInfo(RoleIdField);
                int.TryParse(roleNum, out roleId);
                return roleId; 
            }
        }

        /// <summary>
        /// Get logged in user type
        /// </summary>
        /*public UserType UserType
        {
            get
            {
                UserType userType = UserType.None;
                string user_Type = GetUserInfo(UserTypeField);
                if (user_Type != "") { userType = (UserType)this.EnumSet.GetEnumItemType(typeof(UserType), user_Type); }
                return userType;
            }
        }*/

        #region IDisposable Members

        public virtual void Dispose()
        {
            GC.Collect();
        }

        #endregion
    }
}
