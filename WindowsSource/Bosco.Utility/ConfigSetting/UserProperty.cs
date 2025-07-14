/*  Class Name      : UserProperty
 *  Purpose         : Define/access user info for currently logged in user
 *  Author          : CS
 *  Created on      : 8-Jul-2010
 */

using System;
using System.Data;
using System.Collections.Generic;
using Bosco.Utility;
using System.Drawing;

namespace Bosco.Utility.ConfigSetting
{
    public class UserProperty : SettingProperty
    {
        private static DataView dvUser = null;
        private const string UserIdField = "USER_ID";
        private const string UserNameField = "USER_NAME";
        private const string UserFullNameField = "NAME";
        private const string UserRole = "USERROLE";
        private const string UserFirstName = "FIRSTNAME";
        private const string UserLastName = "LASTNAME";
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
        /// Get the User Photo
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Bitmap GetUserPhoto(string name)
        {
            Bitmap UserPhoto = null;

            if (dvUser != null && dvUser.Count > 0)
            {
                if (!string.IsNullOrEmpty(dvUser[0][name].ToString()) && dvUser[0][name].ToString() != "System.Byte[]")
                {
                    UserPhoto = dvUser[0][name].ToString().Equals(string.Empty) ? UserPhoto : ImageProcessing.ByteArrayToImage((byte[])dvUser[0][name]);
                }
                else if(dvUser[0][name].ToString() != "System.Byte[]")
                {
                    UserPhoto = dvUser.ToTable().Rows[0][name].ToString().Equals(string.Empty) ? UserPhoto : ImageProcessing.ByteArrayToImage((byte[])dvUser.ToTable().Rows[0][name]);
                }
            }

            return UserPhoto;
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
        public string LoginUserId
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
        public bool HasLoginUser
        {
            get
            {
                int userId = 0;
                int.TryParse(LoginUserId, out userId);
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
        public string LoginUserName
        {
            get
            {
                return GetUserInfo(UserNameField);
            }
        }

        /// <summary>
        /// Get Full Name of the logged in user
        /// </summary>
        public string LoginUserFullName
        {
            get { return GetUserInfo(UserFullNameField); }
        }

        /// <summary>
        /// Get Address of the logged in user
        /// </summary>
        public string LoginUserAddress
        {
            get { return GetUserInfo(UserAddressField); }
        }

        /// <summary>
        /// Get Mobile No of the logged in user
        /// </summary>
        public string LoginUserMobileNo
        {
            get { return GetUserInfo(UserMobileNoField); }
        }

        /// <summary>
        /// Get logged in user Email Id
        /// </summary>
        public string LoginUserEmailId
        {
            get { return GetUserInfo(UserEmailIdField); }
        }

        public string GetUserRole
        {
            get { return GetUserInfo(UserRole); }
        }

        public string FirstName
        {
            get { return GetUserInfo(UserFirstName); }
        }

        public string LastName
        {
            get { return GetUserInfo(UserLastName); }
        }
        public int LoginUserRoleId
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
        /// On 27/08/2021, To check logged user default admin user
        /// </summary>
        public bool IsLoginUserDefaultAdminUser
        {
            get
            {
                return (NumberSet.ToInteger(LoginUserId) == DEFAULT_ADMIN_USER_ID);
            }
        }

        /// <summary>
        /// On 27/08/2021, To check logged user default auditor user
        /// </summary>
        public bool IsLoginUserDefaultAuditorUser
        {
            get
            {
                return (NumberSet.ToInteger(LoginUserId) == DEFAULT_AUDITOR_USER_ID);
            }
        }

        /// <summary>
        /// On 30/08/2021, To check logged user auditor user
        /// </summary>
        public bool IsLoginUserAuditor
        {
            get
            {
                return (LoginUserRoleId == DEFAULT_AUDITOR_ROLE_ID);
            }
        }

        /// <summary>
        /// On 28/08/2021, To check logged in user, is full rights user like Admin and Auditor
        /// </summary>
        /// <returns></returns>
        public bool IsFullRightsReservedUser
        {
            get
            {
                return (IsLoginUserDefaultAdminUser || IsLoginUserDefaultAuditorUser);
            }
        }


        ////Login User ProjectSelectionId
        //private int loginUserProjectId = 0;
        //public int LoginUserProjectId
        //{
        //    set { loginUserProjectId = value; }
        //    get { return loginUserProjectId; }

        //}

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

        public override void Dispose()
        {
            //GC.Collect();
        }

        #endregion
    }
}
