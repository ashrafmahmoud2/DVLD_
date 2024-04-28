using DVLD_DateAccess;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_business.clsApplication;

namespace DVLD_business
{
    public class clsInternationalLicenses:clsApplication
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public clsDrivers DriverInfo;
        public int InternationalLicenseID { set; get; }
        public int DriverID { set; get; }
        public int IssuedUsingLocalLicenseID { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public bool IsActive { set; get; }


        public clsInternationalLicenses()

        {
            //here we set the applicaiton type to New International License.
            this.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;

            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;

            this.IsActive = true;


            Mode = enMode.AddNew;

        }

        public clsInternationalLicenses(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID,
             int InternationalLicenseID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive)

        {
            //this is for the base clase
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;

            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = clsDrivers.Find(this.DriverID);

            Mode = enMode.Update;
        }

        private bool _AddNewInternationalLicense()
        {
            //call DataAccess Layer 
            
            this.InternationalLicenseID =
                clsInternationalLicensesDataAccessLayer.AddNewInternationalLicense(
                    this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
               this.IssueDate, this.ExpirationDate,
               this.IsActive, this.CreatedByUserID);


            return (this.InternationalLicenseID != -1);
        }

        private bool _UpdateInternationalLicense()
        {
            //call DataAccess Layer 

            return clsInternationalLicensesDataAccessLayer.UpdateInternationalLicense(
                this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
               this.IssueDate, this.ExpirationDate,
               this.IsActive, this.CreatedByUserID);
        }

        public static clsInternationalLicenses Find(int InternationalLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1; int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true; int CreatedByUserID = 1;

            if (clsInternationalLicensesDataAccessLayer.GetInternationalLicenseInfoByID(InternationalLicenseID, ref ApplicationID, ref DriverID,
                ref IssuedUsingLocalLicenseID,
            ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                //now we find the base application
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);


                return new clsInternationalLicenses(Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID,
                                     InternationalLicenseID, DriverID, IssuedUsingLocalLicenseID,
                                         IssueDate, ExpirationDate, IsActive);

            }

            else
                return null;

        }

        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicensesDataAccessLayer.GetAllInternationalLicenses();

        }

        public bool Save()
        {

            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateInternationalLicense();

            }

            return false;
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {

            return clsInternationalLicensesDataAccessLayer.GetActiveInternationalLicenseIDByDriverID(DriverID);

        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicensesDataAccessLayer.GetDriverInternationalLicenses(DriverID);
        }
        public static short Count()
        {
            return clsInternationalLicensesDataAccessLayer.CountInternationalInternationalLicenses();
        }

       

        //// static void Main(string[] args)
        //// {
        ////TestAddInternationalLicense();
        ////  //// TestFindInternationalLicense();
        ////  //   TestUpdateInternationalLicense();
        ////  //   TestDeleteInternationalLicense();
        ////     Console.ReadLine();
        //// }

        //// ... Existing code ...

        //static void TestAddInternationalLicense()
        //{
        //    clsInternationalLicensess internationalLicense = new clsInternationalLicensess();
        //    internationalLicense.ApplicationID = 112;
        //    internationalLicense.DriverID = 8; // Assuming a valid DriverID
        //    internationalLicense.IssuedUsingLocalLicenseID = 25; // Assuming a valid IssuedUsingLocalLicenseID
        //    internationalLicense.IssueDate = DateTime.Now;
        //    internationalLicense.ExpirationDate = DateTime.Now.AddYears(1); // Assuming a one-year validity
        //    internationalLicense.IsActive = true;
        //    internationalLicense.CreatedByUserID = 1; // Assuming a valid CreatedByUserID

        //    if (internationalLicense.Save())
        //    {
        //        Console.WriteLine("International License added successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to add International License.");
        //    }
        //}

        //static void TestFindInternationalLicense()
        //{
        //    int internationalLicenseIdToFind = 17; // Replace with the actual International License ID to find

        //    clsInternationalLicensess foundInternationalLicense = clsInternationalLicensess.Find(internationalLicenseIdToFind);

        //    if (foundInternationalLicense != null)
        //    {
        //        Console.WriteLine($"Found International License: InternationalLicenseID={foundInternationalLicense.InternationalLicenseID}, ApplicationID={foundInternationalLicense.ApplicationID}, DriverID={foundInternationalLicense.DriverID}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("International License not found.");
        //    }
        //}

        //static void TestUpdateInternationalLicense()
        //{
        //    int internationalLicenseIdToUpdate = 17;

        //    clsInternationalLicensess internationalLicense = clsInternationalLicensess.Find(internationalLicenseIdToUpdate);

        //    if (internationalLicense != null)
        //    {
        //        Console.WriteLine(internationalLicense.InternationalLicenseID);

        //        internationalLicense.ApplicationID = 112;
        //        internationalLicense.DriverID =8; // Assuming a different DriverID
        //        internationalLicense.IssuedUsingLocalLicenseID = 25; // Assuming a different IssuedUsingLocalLicenseID
        //        internationalLicense.IssueDate = DateTime.Now;
        //        internationalLicense.ExpirationDate = DateTime.Now.AddYears(2); // Assuming a two-year validity
        //        internationalLicense.IsActive = false;

        //        if (internationalLicense.Save())
        //        {
        //            Console.WriteLine("International License updated successfully!");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Failed to update International License.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("International License not found.");
        //    }
        //}

        //static void TestDeleteInternationalLicense()
        //{
        //    int internationalLicenseIdToDelete = 25; // Replace with the actual International License ID to delete

        //    if (clsInternationalLicensess.DeleteInternationalLicense(internationalLicenseIdToDelete))
        //    {
        //        Console.WriteLine("International License deleted successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to delete International License.");
        //    }
        //}
    }
}
