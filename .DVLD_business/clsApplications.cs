using DVLD_DateAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_business.clsPerson;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_business
{
    
    public class clsApplication
    {
        
        public enum enMode { AddNew = 0, Update = 1 };
        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, 
            ReleaseDetainedDrivingLicsense = 5,
            NewInternationalLicense = 6, RetakeTest = 7
        };

        public enMode Mode = enMode.AddNew;
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

        public int ApplicationID { set; get; }
        public int ApplicantPersonID { set; get; }
        public clsPerson PersoneInfo { set; get; }
        public string ApplicantFullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }
        }
        public DateTime ApplicationDate { set; get; }
        public int ApplicationTypeID { set; get; }
        public clsApplicationType ApplicationTypeInfo;
        public enApplicationStatus ApplicationStatus { set; get; }
        public string StatusText
        {
            get
            { 

                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }

        }
        public DateTime LastStatusDate { set; get; }
        public float PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo;

        public clsApplication()

        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }

        private clsApplication(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID)

        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.PersoneInfo = clsPerson.Find(ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.Find(CreatedByUserID);
            Mode = enMode.Update;
        }
        

        public bool _AddNewApplication()
        {
            //call DataAccess Layer 
            this.ApplicationID = clsApplicationsDataAccessLayer.AddNewApplications(
                this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            //call DataAccess Layer 

            return clsApplicationsDataAccessLayer.UpdateApplications(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

        }

        public static clsApplication FindBaseApplication(int applicationID)
        {
            int applicantPersonID = -1;
            DateTime applicationDate = DateTime.Now;
            int applicationTypeID = -1;
            byte applicationStatus = 1;
            DateTime lastStatusDate = DateTime.Now;
            float paidFees = 0;
            int createdByUserID = -1;

            bool isFound = clsApplicationsDataAccessLayer.GetApplicationsInfoByID(
                applicationID, ref applicantPersonID, ref applicationDate, ref applicationTypeID,
                ref applicationStatus, ref lastStatusDate, ref paidFees, ref createdByUserID);

            return isFound ? new clsApplication(
                applicationID, applicantPersonID, applicationDate, applicationTypeID,
                (enApplicationStatus)applicationStatus, lastStatusDate, paidFees, createdByUserID)
                : null;
        }


        public bool Cancel()

        {
            return clsApplicationsDataAccessLayer.UpdateStatus(ApplicationID, 2);
        }

        public bool SetComplete()

        {
            return clsApplicationsDataAccessLayer.UpdateStatus(ApplicationID, 3);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplication();

            }

            return false;
        }

        public bool Delete()
        {
            return clsApplicationsDataAccessLayer.DeleteApplications(this.ApplicationID);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationsDataAccessLayer.DoesApplicationsExist(ApplicationID);
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationsDataAccessLayer.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationsDataAccessLayer.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationsDataAccessLayer.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }

        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }

        //static void Main(string[] args)
        //{
        //    TestFindApplications();
        //    Console.ReadLine();
        //}

        //static void TestAddApplications()
        //{
        //clsApplication Applications = new clsApplication();
        //    Applications.ApplicantPersonID = 1;
        //    Applications.ApplicationDate = DateTime.Now;
        //    Applications.ApplicationTypeID = 1;
        //    Applications.ApplicationStatus = enApplicationStatus.New;
        //    Applications.LastStatusDate = DateTime.Now;
        //    Applications.PaidFees = 88;
        //    Applications.CreatedByUserID = 1;

        //    if (Applications.Save())
        //    {
        //        Console.WriteLine("Applications added successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to add Applications.");
        //    }
        //}

        static void TestFindApplications()
        {
            int ApplicationIdToFind = 111;

            clsApplication foundApplication = clsApplication.FindBaseApplication(ApplicationIdToFind);

            if (foundApplication != null)
            {
                Console.WriteLine($"Found Application: ApplicationID={foundApplication.ApplicationID}, ApplicationDate={foundApplication.ApplicationDate}");
            }
            else
            {
                Console.WriteLine("Application not found.");
            }
        }

        ////static void TestUpdateApplications()
        ////{
        ////    int ApplicationsIdToUpdate = 111;

        ////clsApplication Applications = clsApplication.Find(111);

        ////    if (Applications != null)
        ////    {
        ////        Console.WriteLine(Applications.ApplicationID);
        ////        Console.WriteLine(Applications.CreatedByUserID);




        ////        Applications.ApplicantPersonID = 1023;
        ////        Applications.ApplicationDate = DateTime.Now;
        ////        Applications.ApplicationTypeID = 1;
        ////        Applications.ApplicationStatus = 2;
        ////        Applications.LastStatusDate = DateTime.Now;
        ////        Applications.PaidFees = 100.00M;
        ////        Applications.CreatedByUserID = 26;





        ////        if (Applications._UpdateApplications())
        ////        {
        ////            //stop in fix update;

        ////            Console.WriteLine("Applications updated successfully!");
        ////        }
        ////        else
        ////        {
        ////            Console.WriteLine(Applications.CreatedByUserID);
        ////            Console.WriteLine("Failed to update Applications.");
        ////        }
        ////    }
        ////    else
        ////    {
        ////        Console.WriteLine("Applications not found.");
        ////    }
        ////}

        ////static void TestDeleteApplications()
        ////{
        ////    int ApplicationsIdToDelete = 117; // Replace with the actual Applications ID to delete

        ////    if (clsApplication.DeleteApplications(ApplicationsIdToDelete))
        ////    {
        ////        Console.WriteLine("Applications deleted successfully!");
        ////    }
        ////    else
        ////    {
        ////        Console.WriteLine("Failed to delete Applications.");
        ////    }
        ////}




    }
}
