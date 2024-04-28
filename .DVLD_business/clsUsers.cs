using DVLD_DateAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_business.clsPerson;

namespace DVLD_business
{
    public class clsUser : clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsPerson PersoenInfo;

        public clsUser() : base()
        {
            this.UserID = -1;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.IsActive = false;
        }

        private clsUser(int PersonID, string NationalNo, string FirstName, string SecondName,
     string ThirdName, string LastName, DateTime DateOfBirth, enGender Gender,
     string Address, string Phone, string Email, int NationalityCountryID,
     string ImagePath, int UserID, string UserName, string Password, bool IsActive)
     : base(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
         DateOfBirth, (byte)Gender, Address, Phone, Email, NationalityCountryID, ImagePath)
        {
            this.UserID = UserID;
            this.UserName = UserName; // Use the provided UserName parameter
            this.Password = Password; // Use the provided Password parameter
            this.IsActive = IsActive;

            this.PersoenInfo = clsPerson.Find(PersonID);

            Mode = enMode.Update;
        }





        private bool _AddNewUser()
        {
            this.UserID = clsUsersDataAccessLayer.AddNewUser(this.PersonID, this.UserName,
                this.Password, this.IsActive);

            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUsersDataAccessLayer.UpdateUser(this.UserID, this.PersonID, this.UserName,
                this.Password, this.IsActive);
        }

        public bool Save()
        {
            base.Mode = (clsPerson.enMode)Mode;
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateUser();
            }

            return false;
        }


        public static bool ChangePassword(int UserID,string NewPassword)
        {
            return clsUsersDataAccessLayer.ChangePassword(UserID, NewPassword);
        }

        private static int _GetPersonIDByUserID(int UserID)
        {
            return clsUsersDataAccessLayer.GetPersonIDByUserID(UserID);
        }

        public static clsUser Find(int userID)
        {
            int personID = -1;
            string username = string.Empty;
            string password = string.Empty;
            int permissions = -1;
            bool isActive = false;

            bool isFound = clsUsersDataAccessLayer.GetUserInfoByID(
                userID, ref personID, ref username, ref password, ref isActive);

            return isFound ? CreateUserFromPersonID(personID, userID, username, password, permissions, isActive) : null;
        }
        public static clsUser FindByPersoenID(int personID )
        {
            int userID = -1;
            string username = string.Empty;
            string password = string.Empty;
            int permissions = -1;
            bool isActive = false;

            bool isFound = clsUsersDataAccessLayer.GetUserInfoByPersoneID(personID
                , ref userID, ref username, ref password, ref isActive);

            return isFound ? CreateUserFromPersonID(personID, userID, username, password, permissions, isActive) : null;
        }

        public static clsUser Find(string username)
        {
            int userID = -1;
            int personID = -1;
            string password = string.Empty;
            int permissions = -1;
            bool isActive = false;

            bool isFound = clsUsersDataAccessLayer.GetUserInfoByUsername(
                ref userID, ref personID, username, ref password, ref isActive);

            return isFound ? CreateUserFromPersonID(personID, userID, username, password, permissions, isActive) : null;
        }

        public static clsUser Find(string username, string password)
        {
            int userID = -1;
            int personID = -1;
            int permissions = -1;
            bool isActive = false;

            bool isFound = clsUsersDataAccessLayer.GetUserInfoByUsernameAndPassword(
                ref userID, ref personID, username, password, ref isActive);

            return isFound ? CreateUserFromPersonID(personID, userID, username, password, permissions, isActive) : null;
        }

        private static clsUser CreateUserFromPersonID(int personID, int userID, string username, string password, int permissions, bool isActive)
        {
            clsPerson person = clsPerson.Find(personID);

            if (person != null)
            {
                return new clsUser(
                    person.PersonID,
                    person.NationalNo,
                    person.FirstName,
                    person.SecondName,
                    person.ThirdName,
                    person.LastName,
                    person.DateOfBirth,
                    (enGender)person.Gender,
                    person.Address,
                    person.Phone,
                    person.Email,
                    person.NationalityCountryID,
                    person.ImagePath,
                    userID,
                    username,
                    password,
                    //  permissions,
                    isActive
                );
            }

            return null;
        }



        public static bool DeleteUser(int UserID)
        {
            int PersonID = _GetPersonIDByUserID(UserID);

            if (PersonID == -1)
            {
                return false;
            }

            if (!clsUsersDataAccessLayer.DeleteUser(UserID))
            {
                return false;
            }

            //return clsPerson.DeletePerson(PersonID);
            return true;
        }

        public static bool DoesUserExist(int UserID)
        {
            return clsUsersDataAccessLayer.DoesUserExist(UserID);
        }

        public static bool DoesUserExist(string Username)
        {
            return clsUsersDataAccessLayer.DoesUserExist(Username);
        }

        public static bool DoesUserExist(string Username, string Password)
        {
            return clsUsersDataAccessLayer.DoesUserExist(Username, Password);
        }

        public static DataTable GetAllUsers()
        {
            return clsUsersDataAccessLayer.GetAllUsers();
        }

        public static short Count()
        {
            return clsUsersDataAccessLayer.CountUsers();
        }
        //static void Main(string[] args)
        //{
        //   // TestAddUser();
        //   // TestFindUser();
        //   // TestUpdateUser();
        //   // TestDeleteUser();
        //    Console.ReadLine();
        //}

        static void TestAddUser()
        {
            clsUser user = new clsUser();
            user.UserName = "TestUser";
            user.Password = "TestPassword";
            user.PersonID = 1;
            user.IsActive = true;

            if (user.Save())
            {
                Console.WriteLine("User added successfully!");
            }
            else
            {
                Console.WriteLine("Failed to add user.");
            }
        }

        static void TestFindUser()
        {
            int userIdToFind = 1; // Replace with the actual user ID to find

            clsUser foundUser = clsUser.Find(userIdToFind);

            if (foundUser != null)
            {
                Console.WriteLine($"Found user: {foundUser.UserName}");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        static void TestUpdateUser()
        {
            int userIdToUpdate = 1; // Replace with the actual user ID to update

            clsUser userToUpdate = clsUser.Find(userIdToUpdate);

            if (userToUpdate != null)
            {
                userToUpdate.UserName = "UpdatedUser";
                userToUpdate.Password = "UpdatedPassword";

                if (userToUpdate.Save())
                {
                    Console.WriteLine("User updated successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to update user.");
                }
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        static void TestDeleteUser()
        {
            int userIdToDelete = 15; // Replace with the actual user ID to delete

            if (clsUser.DeleteUser(userIdToDelete))
            {
                Console.WriteLine("User deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete user.");
            }
        }




    }
}
