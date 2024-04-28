
//public int TestAppointmentID { get; set; }
//public int TestTypeID { get; set; }
//public int LocalDrivingLicenseApplicationID { get; set; }
//public DateTime AppointmentDate { get; set; }
//public decimal PaidFees { get; set; }
//public int CreatedByUserID { get; set; }
//public bool IsLocked { get; set; }
using DVLD_DateAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_business
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { set; get; }
        public clsTestType.enTestType TestTypeID { set; get; }
        public int LocalDrivingLicenseApplicationID { set; get; }
        public DateTime AppointmentDate { set; get; }
        public float PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsLocked { set; get; }
        public int RetakeTestApplicationID { set; get; }
        public clsApplication RetakeTestAppInfo { set; get; }

        public int TestID
        {
            get { return _GetTestID(); }

        }

        public clsTestAppointment()

        {
            this.TestAppointmentID = -1;
            this.TestTypeID = clsTestType.enTestType.VisionTest;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.RetakeTestApplicationID = -1;
            Mode = enMode.AddNew;

        }

        public clsTestAppointment(int TestAppointmentID, clsTestType.enTestType TestTypeID,
           int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees,
           int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)

        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestAppInfo = clsApplication.FindBaseApplication(RetakeTestApplicationID);
            Mode = enMode.Update;
        }

        private bool _AddNewTestAppointment()
        {
            //call DataAccess Layer 

            this.TestAppointmentID = clsTestAppointmentDataAccessLayer.AddNewTestAppointment((int)this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {
            //call DataAccess Layer 

            return clsTestAppointmentDataAccessLayer.UpdateTestAppointment(this.TestAppointmentID, (int)this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = 1; int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now; float PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; int RetakeTestApplicationID = -1;

            if (clsTestAppointmentDataAccessLayer.GetTestAppointmentInfoByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,
            ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTestType)TestTypeID, LocalDrivingLicenseApplicationID,
             AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;

        }
        public static clsTestAppointment FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID  )
        {
            int TestTypeID = 1; int TestAppointmentID = -1;
            DateTime AppointmentDate = DateTime.Now; float PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; int RetakeTestApplicationID = -1;

            if (clsTestAppointmentDataAccessLayer.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID , ref TestTypeID, ref TestAppointmentID,
            ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTestType)TestTypeID, LocalDrivingLicenseApplicationID,
             AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;

        }


        public static clsTestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            int TestAppointmentID = -1;
            DateTime AppointmentDate = DateTime.Now; float PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; int RetakeTestApplicationID = -1;

            if (clsTestAppointmentDataAccessLayer.GetLastTestAppointment(LocalDrivingLicenseApplicationID, (int)TestTypeID,
                ref TestAppointmentID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
             AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;

        }

        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentDataAccessLayer.GetAllTestAppointments();

        }

        public DataTable GetApplicationTestAppointmentsPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentDataAccessLayer.GetApplicationTestAppointmentsPerTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentDataAccessLayer.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestAppointment();

            }

            return false;
        }

        private int _GetTestID()
        {
            return clsTestAppointmentDataAccessLayer.GetTestID(TestAppointmentID);
        }


        public static bool DoesPersonHaveTestAppointmentsExist(string NationalNo, ref int TestAppointmentID)
        {
            return clsTestAppointmentDataAccessLayer.DoesPersonHaveTestAppointmentsExist(NationalNo, ref TestAppointmentID);
        }


        //public static void TestAddTestAppointments()
        //{
        //    clsTestAppointment testAppointments = new clsTestAppointment();
        //    testAppointments.TestTypeID = 1; // Replace with the actual TestTypeID
        //    testAppointments.LocalDrivingLicenseApplicationID = 41;//Replace with the actual ApplicationID
        //    testAppointments.AppointmentDate = DateTime.Now;
        //    testAppointments.PaidFees = 100.00M;
        //    testAppointments.CreatedByUserID = 1; // Replace with the actual UserID
        //    testAppointments.IsLocked = false;

        //    if (testAppointments.Save())
        //    {
        //        Console.WriteLine("TestAppointments added successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to add TestAppointments.");
        //    }
        //}

        //public static void TestFindTestAppointments()
        //{
        //    int testAppointmentsIdToFind = 38; // Replace with the actual TestAppointments ID to find

        //    clsTestAppointment foundTestAppointments = clsTestAppointment.FindbyLocalDrivingLicenseApplicationID(testAppointmentsIdToFind);

        //    if (foundTestAppointments != null)
        //    {
        //        Console.WriteLine($"Found TestAppointments: TestAppointmentID={foundTestAppointments.TestAppointmentID}, AppointmentDate={foundTestAppointments.AppointmentDate}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("TestAppointments not found.");
        //    }
        //}

        //public static void TestUpdateTestAppointments()
        //{
        //    int testAppointmentsIdToUpdate = 109; // Replace with the actual TestAppointments ID to update

        //    clsTestAppointment testAppointments = clsTestAppointment.Find(testAppointmentsIdToUpdate);

        //    if (testAppointments != null)
        //    {
        //        Console.WriteLine(testAppointments.TestAppointmentID);
        //        Console.WriteLine(testAppointments.AppointmentDate);

        //        testAppointments.AppointmentDate = DateTime.Now; // Update other properties accordingly

        //        if (testAppointments.Save())
        //        {
        //            Console.WriteLine("TestAppointments updated successfully!");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Failed to update TestAppointments.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("TestAppointments not found.");
        //    }
        //}

        //public static void TestDeleteTestAppointments()
        //{
        //    int testAppointmentsIdToDelete = 112; // Replace with the actual TestAppointments ID to delete

        //    if (clsTestAppointment.DeleteTestAppointments(testAppointmentsIdToDelete))
        //    {
        //        Console.WriteLine("TestAppointments deleted successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to delete TestAppointments.");
        //    }
        //}


        //static void Main(string[] args)
        //{
        //    Console.WriteLine("TEST APPOINTMET");
        //    // Uncomment and run the desired test method
        //    //TestAddTestAppointments();
        //   // TestFindTestAppointments();
        //    //TestUpdateTestAppointments();
        //    // TestDeleteTestAppointments();

        //    Console.ReadLine();
        //}
    }
}
