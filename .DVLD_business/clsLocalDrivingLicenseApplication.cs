﻿
using DVLD_DateAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_business
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { set; get; }
        public int LicenseClassID { set; get; }
        public clsLicenseClasses LicenseClassInfo;
        
        public string PersonFullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }

        }

        public clsLocalDrivingLicenseApplication()

        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;

            Mode = enMode.AddNew;

        }

        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID, int LicenseClassID)


        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID; ;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = (int)ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClasses.Find(LicenseClassID);
            Mode = enMode.Update;
        }

        public bool _AddNewLocalDrivingLicenseApplication()
        {
            //call DataAccess Layer 
            
            this.LocalDrivingLicenseApplicationID =
                clsLocalDrivingLicenseApplicationsDataAccessLayer.
                AddNewLocalDrivingLicenseApplications
                (
                this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            //call DataAccess Layer 

            return clsLocalDrivingLicenseApplicationsDataAccessLayer.
                UpdateLocalDrivingLicenseApplications
                (
                this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);

        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingAppLicenseID(int LocalDrivingLicenseApplicationID)
        {
            // 
            int ApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationsDataAccessLayer.
                GetLocalDrivingLicenseApplicationsInfoByID
                (LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID);


            if (IsFound)
            {
                //now we find the base application
            clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                //we return new object of that person with the right data
                return new clsLocalDrivingLicenseApplication(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;


        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            // 
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationsDataAccessLayer.
                GetLocalDrivingLicenseApplicationsInfoByApplicationID
                (ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID);


            if (IsFound)
            {
                //now we find the base application
            clsApplication Application = 
                clsApplication.FindBaseApplication(ApplicationID);

                //we return new object of that person with the right data
                return new clsLocalDrivingLicenseApplication(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;


        }

        public bool Save()
        {

            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;


            //After we save the main application now we save the sub application.
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLocalDrivingLicenseApplication();

            }

            return false;
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationsDataAccessLayer.GetAllLocalDrivingLicenseApplications();
        }

        public bool Delete()
        {
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;
            //First we delete the Local Driving License Application
            IsLocalDrivingApplicationDeleted = clsLocalDrivingLicenseApplicationsDataAccessLayer.
                DeleteLocalDrivingLicenseApplications(this.LocalDrivingLicenseApplicationID);

            if (!IsLocalDrivingApplicationDeleted)
                return false;
            //Then we delete the base Application
            IsBaseApplicationDeleted = base.Delete();
            return IsBaseApplicationDeleted;

        }

        public bool DoesPassTestType(clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationsDataAccessLayer.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesPassPreviousTest(clsTestType.enTestType CurrentTestType)
        {

            switch (CurrentTestType)
            {
                case clsTestType.enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    return true;

                case clsTestType.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.

                    return this.DoesPassTestType(clsTestType.enTestType.VisionTest);


                case clsTestType.enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    return this.DoesPassTestType(clsTestType.enTestType.WrittenTest);

                default:
                    return false;
            }
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationsDataAccessLayer.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTestType(clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationsDataAccessLayer.
                DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public byte TotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccessLayer.
                TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationsDataAccessLayer.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationsDataAccessLayer.
                TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public bool AttendedTest(clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationsDataAccessLayer.TotalTrialsPerTest
                (this.LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public static bool IsThereAnActiveScheduledTest(
            int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {

            return clsLocalDrivingLicenseApplicationsDataAccessLayer.
                IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType TestTypeID)

        {

            return clsLocalDrivingLicenseApplicationsDataAccessLayer.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public clsTest GetLastTestPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestTypeID);
        }

        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }

        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;


            clsDrivers Driver = clsDrivers.FindByPersoneID(this.ApplicantPersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new clsDrivers();

                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            //now we diver is there, so we add new licesnse

            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClass = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = (float)this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License._AddNewLicense())
            {
                //now we should set the application status to complete.
                this.SetComplete();

                return License.LicenseID;
            }

            else
                return -1;
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }

        public int GetActiveLicenseID()
        {//this will get the license id that belongs to this application
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }








        ////static void Main(string[] args)
        ////{
        ////    //TestAddLocalDrivingLicenseApplications();
        ////    TestFindLocalDrivingLicenseApplications();
        ////    TestUpdateLocalDrivingLicenseApplications();
        ////    TestDeleteLocalDrivingLicenseApplications();
        ////    Console.ReadLine();
        ////}

        //// ... Existing code ...

        //static void TestAddLocalDrivingLicenseApplications()
        //{
        //    clsLocalDrivingLicenseApplications localDrivingLicenseApplications = new clsLocalDrivingLicenseApplications();
        //    localDrivingLicenseApplications.ApplicationID = 112;
        //    localDrivingLicenseApplications.LicenseClassID = 1; // Assuming a valid LicenseClassID
        //                                                        // Set other properties if needed

        //    if (localDrivingLicenseApplications.Save())
        //    {
        //        Console.WriteLine("LocalDrivingLicenseApplications added successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to add LocalDrivingLicenseApplications.");
        //    }
        //}

        //static void TestFindLocalDrivingLicenseApplications()
        //{
        //    int localDrivingLicenseApplicationsIdToFind = 39; // Replace with the actual LocalDrivingLicenseApplications ID to find

        //    clsLocalDrivingLicenseApplications foundLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplications.Find(localDrivingLicenseApplicationsIdToFind);

        //    if (foundLocalDrivingLicenseApplications != null)
        //    {
        //        Console.WriteLine($"Found LocalDrivingLicenseApplications: LocalDrivingLicenseApplicationID={foundLocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID}, ApplicationID={foundLocalDrivingLicenseApplications.ApplicationID}, LicenseClassID={foundLocalDrivingLicenseApplications.LicenseClassID}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("LocalDrivingLicenseApplications not found.");
        //    }
        //}

        //static void TestUpdateLocalDrivingLicenseApplications()
        //{
        //    int localDrivingLicenseApplicationsIdToUpdate = 39;

        //    clsLocalDrivingLicenseApplications localDrivingLicenseApplications = clsLocalDrivingLicenseApplications.Find(localDrivingLicenseApplicationsIdToUpdate);

        //    if (localDrivingLicenseApplications != null)
        //    {
        //        Console.WriteLine(localDrivingLicenseApplications.LocalDrivingLicenseApplicationID);
        //        // Console.WriteLine(localDrivingLicenseApplications.CreatedByUserID); // Property not present in the class

        //        localDrivingLicenseApplications.ApplicationID = 112;
        //        localDrivingLicenseApplications.LicenseClassID = 2; // Assuming a different LicenseClassID
        //                                                            // Set other properties if needed

        //        if (localDrivingLicenseApplications.Save())
        //        {
        //            Console.WriteLine("LocalDrivingLicenseApplications updated successfully!");
        //        }
        //        else
        //        {
        //            // Console.WriteLine(localDrivingLicenseApplications.CreatedByUserID); // Property not present in the class
        //            Console.WriteLine("Failed to update LocalDrivingLicenseApplications.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("LocalDrivingLicenseApplications not found.");
        //    }
        //}

        //static void TestDeleteLocalDrivingLicenseApplications()
        //{
        //    int localDrivingLicenseApplicationsIdToDelete = 39; // Replace with the actual LocalDrivingLicenseApplications ID to delete

        //    if (clsLocalDrivingLicenseApplications.DeleteLocalDrivingLicenseApplications(localDrivingLicenseApplicationsIdToDelete))
        //    {
        //        Console.WriteLine("LocalDrivingLicenseApplications deleted successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to delete LocalDrivingLicenseApplications.");
        //    }
        //}


    }
}
