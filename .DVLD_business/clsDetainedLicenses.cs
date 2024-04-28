using DVLD_DateAccess;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_business
{
    public class clsDetainedLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int DetainID { set; get; }
        public int LicenseID { set; get; }
        public DateTime DetainDate { set; get; }

        public float FineFees { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo { set; get; }
        public bool IsReleased { set; get; }
        public DateTime ReleaseDate { set; get; }
        public int ReleasedByUserID { set; get; }
        public clsUser ReleasedByUserInfo { set; get; }
        public int ReleaseApplicationID { set; get; }

        public clsDetainedLicense()

        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MaxValue;
            this.ReleasedByUserID = 0;
            this.ReleaseApplicationID = -1;



            Mode = enMode.AddNew;

        }

        public clsDetainedLicense(int DetainID,
            int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID,
            bool IsReleased, DateTime ReleaseDate,
            int ReleasedByUserID, int ReleaseApplicationID)

        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.Find(this.CreatedByUserID);
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.ReleasedByUserInfo = clsUser.FindByPersoenID(this.ReleasedByUserID);
            Mode = enMode.Update;
        }
        private bool _AddNewDetainedLicense()
        {
            //call DataAccess Layer 

            this.DetainID =     clsDetainedLicensesDataAccessLayer.AddNewDetainedLicense(
                this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);

            return (this.DetainID != -1);
        }

        private bool _UpdateDetainedLicense()
        {
            //call DataAccess Layer 

            return         clsDetainedLicensesDataAccessLayer.UpdateDetainedLicense(
                this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
        }

        public static clsDetainedLicense Find(int DetainID)
        {
            int LicenseID = -1; DateTime DetainDate = DateTime.Now;
            float FineFees = 0; int CreatedByUserID = -1;
            bool IsReleased = false; DateTime ReleaseDate = DateTime.MaxValue;
            int ReleasedByUserID = -1; int ReleaseApplicationID = -1;

            if (        clsDetainedLicensesDataAccessLayer.GetDetainedLicenseInfoByID(DetainID,
            ref LicenseID, ref DetainDate,
            ref FineFees, ref CreatedByUserID,
            ref IsReleased, ref ReleaseDate,
            ref ReleasedByUserID, ref ReleaseApplicationID))

                return new clsDetainedLicense(DetainID,
                     LicenseID, DetainDate,
                     FineFees, CreatedByUserID,
                     IsReleased, ReleaseDate,
                     ReleasedByUserID, ReleaseApplicationID);
            else
                return null;

        }

        public static DataTable GetAllDetainedLicenses()
        {
            return         clsDetainedLicensesDataAccessLayer.GetAllDetainedLicenses();

        }

        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1; DateTime DetainDate = DateTime.Now;
            float FineFees = 0; int CreatedByUserID = -1;
            bool IsReleased = false; DateTime ReleaseDate = DateTime.MaxValue;
            int ReleasedByUserID = -1; int ReleaseApplicationID = -1;

            if (        clsDetainedLicensesDataAccessLayer.GetDetainedLicenseInfoByLicenseID(LicenseID,
            ref DetainID, ref DetainDate,
            ref FineFees, ref CreatedByUserID,
            ref IsReleased, ref ReleaseDate,
            ref ReleasedByUserID, ref ReleaseApplicationID))

                return new clsDetainedLicense(DetainID,
                     LicenseID, DetainDate,
                     FineFees, CreatedByUserID,
                     IsReleased, ReleaseDate,
                     ReleasedByUserID, ReleaseApplicationID);
            else
                return null;

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDetainedLicense();

            }

            return false;
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return         clsDetainedLicensesDataAccessLayer.IsLicenseDetained(LicenseID);
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return         clsDetainedLicensesDataAccessLayer.ReleaseDetainedLicense(this.DetainID,
                   ReleasedByUserID, ReleaseApplicationID);
        }
        public static short Count()
        {
            return clsDetainedLicensesDataAccessLayer.CountDetainedLicenses();
        }

        ////static void Main(string[] args)
        ////{
        ////    //TestAddDetainedLicenses();
        ////// TestFindDetainedLicenses();
        //// TestUpdateDetainedLicenses();
        ////  //TestDeleteDetainedLicenses();
        ////    Console.ReadLine();
        ////}

        //static void TestAddDetainedLicenses()
        //{
        //    clsDetainedLicenses detainedLicenses = new clsDetainedLicenses();
        //    detainedLicenses.LicenseID = 27;
        //    detainedLicenses.DetainDate = DateTime.Parse("2023-10-10 09:23:00");
        //    detainedLicenses.FineFees = 50.00M;
        //    detainedLicenses.CreatedByUserID = 17;
        //    detainedLicenses.IsReleased = false;
        //    detainedLicenses.ReleaseDate = DateTime.Parse("2023-10-10 09:23:00");
        //    detainedLicenses.ReleasedByUserID = 17;
        //    detainedLicenses.ReleaseApplicationID = 131;

        //    if (detainedLicenses.Save())
        //    {
        //        Console.WriteLine("Detained Licenses added successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to add Detained Licenses.");
        //    }
        //}


        //static void TestFindDetainedLicenses()
        //{
        //    int detainedLicensesIdToFind = 13; // Replace with the actual Detained Licenses ID to find

        //    clsDetainedLicenses foundDetainedLicenses = clsDetainedLicenses.Find(detainedLicensesIdToFind);

        //    if (foundDetainedLicenses != null)
        //    {
        //        Console.WriteLine($"Found Detained Licenses: LicenseID={foundDetainedLicenses.LicenseID}, DetainDate={foundDetainedLicenses.DetainDate}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Detained Licenses not found.");
        //    }
        //}

        //static void TestUpdateDetainedLicenses()
        //{
        //    int detainedLicensesIdToUpdate = 12;

        //    clsDetainedLicenses detainedLicenses = clsDetainedLicenses.Find(detainedLicensesIdToUpdate);

        //    if (detainedLicenses != null)
        //    {
        //        Console.WriteLine(detainedLicenses.DetainID);
        //        Console.WriteLine(detainedLicenses.CreatedByUserID);

        //        detainedLicenses.LicenseID = 27;
        //        detainedLicenses.DetainDate = DateTime.Parse("2023-10-10 09:23:00");
        //        detainedLicenses.FineFees = 50.00M;
        //        detainedLicenses.CreatedByUserID = 17;
        //        detainedLicenses.IsReleased = false;
        //        detainedLicenses.ReleaseDate = DateTime.Parse("2023-10-10 09:23:00");
        //        detainedLicenses.ReleasedByUserID = 17;
        //        detainedLicenses.ReleaseApplicationID = 131;

        //        // Assign DateTime values for DetainDate and ReleaseDate
        //        detainedLicenses.DetainDate = DateTime.Parse("2023-10-10 09:23:00");
        //        detainedLicenses.ReleaseDate = DateTime.Parse("2023-10-10 09:23:00");

        //        if (detainedLicenses._UpdateDetainedLicenses())
        //        {
        //            Console.WriteLine("Detained Licenses updated successfully!");
        //        }
        //        else
        //        {
        //            Console.WriteLine(detainedLicenses.CreatedByUserID);
        //            Console.WriteLine("Failed to update Detained Licenses.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Detained Licenses not found.");
        //    }
        //}


        //static void TestDeleteDetainedLicenses()
        //{
        //    int detainedLicensesIdToDelete = 15; // Replace with the actual Detained Licenses ID to delete

        //    if (clsDetainedLicenses.DeleteDetainedLicenses(detainedLicensesIdToDelete))
        //    {
        //        Console.WriteLine("Detained Licenses deleted successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to delete Detained Licenses.");
        //    }
        //}

    }
}
