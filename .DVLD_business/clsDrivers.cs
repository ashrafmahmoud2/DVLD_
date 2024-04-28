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
    public class clsDrivers:clsPerson
    {
        //fix the proplem of update ;
        // add viruel to save();
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public clsPerson PersoenInfo;

        public clsDrivers() : base()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.PersoenInfo = clsPerson.Find(PersonID);
            this.CreatedDate = DateTime.MinValue;
        }

        private clsDrivers(int PersonID, string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, enGender Gender,
            string Address, string Phone, string Email, int NationalityCountryID,
            string ImagePath, int DriverID, int CreatedByUserID, DateTime CreatedDate)
            : base(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                  DateOfBirth, (byte)Gender, Address, Phone, Email, NationalityCountryID, ImagePath)
        {
            this.DriverID = DriverID;
            this.PersonID=PersonID;
            this.CreatedByUserID = CreatedByUserID; // Use the provided CreatedByUserID parameter
            this.CreatedDate = CreatedDate; // Use the provided CreatedDate parameter
            this.PersoenInfo=clsPerson.Find(PersonID);
         Mode=   enMode.Update;
        }

        public bool _AddNewDrivers()
        {
            this.DriverID = clsDriversDataAccessLayer.AddNewDrivers(this.PersonID, this.CreatedByUserID, this.CreatedDate);

            return (this.DriverID != -1);
        }

        private bool _UpdateDrivers()
        {
            return clsDriversDataAccessLayer.UpdateDrivers(this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
        }

        public bool Save()
        {
            base.Mode = (clsPerson.enMode)Mode;
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDrivers())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDrivers();
            }

            return false;
        }

        private static int _GetPersonIDByDriversID(int DriversID)
        {
            return clsDriversDataAccessLayer._GetPersonIDByDriversID(DriversID);
        }

        public static clsDrivers Find(int DriversID)
        {
            int personID = -1;
            int CreatedByUserID = -1;
            DateTime createdDate = DateTime.MinValue;

            bool isFound = clsDriversDataAccessLayer.GetDriversInfoByID(
                DriversID, ref personID, ref CreatedByUserID, ref createdDate);

            return isFound ? CreateDriversFromPersonID(personID, DriversID, CreatedByUserID, createdDate) : null;
        }
        public static clsDrivers FindByPersoneID(int personID)
        {
            int DriversID = -1;
            int CreatedByUserID = -1;
            DateTime createdDate = DateTime.MinValue;

            bool isFound = clsDriversDataAccessLayer.GetDriversInfoByPersoenID(
               personID , ref DriversID, ref CreatedByUserID, ref createdDate);

            return isFound ? CreateDriversFromPersonID(personID, DriversID, CreatedByUserID, createdDate) : null;
        }

        private static clsDrivers CreateDriversFromPersonID(int personID, int DriversID, int CreatedByUserID, DateTime createdDate)
        {
            clsPerson person = clsPerson.Find(personID);

            if (person != null)
            {
                return new clsDrivers(
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
                    DriversID,
                    CreatedByUserID,
                    createdDate
                );
            }

            return null;
        }

        public static bool DeleteDrivers(int DriversID)
        {
            int PersonID = _GetPersonIDByDriversID(DriversID);

            if (PersonID == -1)
            {
                return false;
            }

            if (!clsDriversDataAccessLayer.DeleteDrivers(DriversID))
            {
                return false;
            }

            //return clsPerson.DeletePerson(PersonID);
            return true;
        }

        public static bool DoesDriversExist(int DriversID)
        {
            return clsDriversDataAccessLayer.DoesDriversExist(DriversID);
        }

        public static DataTable GetAllDriverss()
        {
            return clsDriversDataAccessLayer.GetAllDrivers();
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicense.GetDriverLicenses(DriverID);
        }


        public static DataTable GetLicenses(int DriverID)
        {
            return clsLicense.GetDriverLicenses(DriverID);
        }

        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenses.GetDriverInternationalLicenses(DriverID);
        }


        public static short Count()
        {
            return clsDriversDataAccessLayer.CountDrivers();
        }

        static void Main(string[] args)
        {
            //TestAddDrivers();
             TestFindDrivers();
            //  TestUpdateDrivers();
            /// TestDeleteDrivers();
            Console.ReadLine();
        }

        static void TestAddDrivers()
        {
            clsDrivers Drivers = new clsDrivers();
            Drivers.CreatedByUserID = 1;
            Drivers.CreatedDate = DateTime.Now;
            Drivers.PersonID = 1;

            if (Drivers.Save())
            {
                Console.WriteLine("Drivers added successfully!");
            }
            else
            {
                Console.WriteLine("Failed to add Drivers.");
            }
        }

        static void TestFindDrivers()
        {
            int DriversIdToFind = 1; // Replace with the actual Drivers ID to find

            clsDrivers foundDrivers = clsDrivers.FindByPersoneID(DriversIdToFind);

            if (foundDrivers != null)
            {
                Console.WriteLine($"Found Drivers: CreatedByUserID={foundDrivers.CreatedByUserID}, CreatedDate={foundDrivers.CreatedDate}");
            }
            else
            {
                Console.WriteLine("Drivers not found.");
            }
        }

        static void TestUpdateDrivers()
        {
            int DriversIdToUpdate =11; // Replace with the actual Drivers ID to update

            clsDrivers DriversToUpdate = clsDrivers.Find(DriversIdToUpdate);

            if (DriversToUpdate != null)
            {
                DriversToUpdate.CreatedByUserID = 26;
                DriversToUpdate.CreatedDate = DateTime.Now;
                DriversToUpdate.PersonID = 1030;

                if (DriversToUpdate.Save())
                {
                    Console.WriteLine("Drivers updated successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to update Drivers.");
                }
            }
            else
            {
                Console.WriteLine("Drivers not found.");
            }
        }

        static void TestDeleteDrivers()
        {
            int DriversIdToDelete = 8; // Replace with the actual Drivers ID to delete

            if (clsDrivers.DeleteDrivers(DriversIdToDelete))
            {
                Console.WriteLine("Drivers deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete Drivers.");
            }
        }
    }
}
