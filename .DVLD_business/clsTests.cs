//public int TestID { get; set; }
//public int TestAppointmentID { get; set; }
//public bool TestResult { get; set; }
//public string Notes { get; set; }
//public int CreatedByUserID { get; set; }
using DVLD_DateAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_business
{
    public class clsTest
    {
        
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestID { set; get; }
        public int TestAppointmentID { set; get; }
        public clsTestAppointment TestAppointmentInfo { set; get; }
        public bool TestResult { set; get; }
        public string Notes { set; get; }
        public int CreatedByUserID { set; get; }

        public clsTest()

        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }

        public clsTest(int TestID, int TestAppointmentID,
            bool TestResult, string Notes, int CreatedByUserID)

        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }

        public bool _AddNewTest()
        {
            //call DataAccess Layer 
            this.TestID = clsTestDataAccessLayer.AddNewTest(this.TestAppointmentID,
                this.TestResult, this.Notes, this.CreatedByUserID);


            return (this.TestID != -1);
        }

        private bool _UpdateTest()
        {
            //call DataAccess Layer 

            return clsTestDataAccessLayer.UpdateTest(this.TestID, this.TestAppointmentID,
                this.TestResult, this.Notes, this.CreatedByUserID);
        }

        public static clsTest Find(int TestID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestDataAccessLayer.GetTestInfoByID(TestID,
            ref TestAppointmentID, ref TestResult,
            ref Notes, ref CreatedByUserID))

                return new clsTest(TestID,
                        TestAppointmentID, TestResult,
                        Notes, CreatedByUserID);
            else
                return null;

        }

        public static clsTest FindLastTestPerPersonAndLicenseClass
            (int PersonID, int LicenseClassID, clsTestType.enTestType TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestDataAccessLayer.GetLastTestByPersonAndTestTypeAndLicenseClass
                (PersonID, LicenseClassID, (int)TestTypeID, ref TestID,
            ref TestAppointmentID, ref TestResult,
            ref Notes, ref CreatedByUserID))

                return new clsTest(TestID,
                        TestAppointmentID, TestResult,
                        Notes, CreatedByUserID);
            else
                return null;

        }

        public static DataTable GetAllTests()
        {
            return clsTestDataAccessLayer.GetAllTests();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTest();

            }

            return false;
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestDataAccessLayer.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
        public static int GetPassedTestCountbyint(int LocalDrivingLicenseApplicationID)
        {
            return clsTestDataAccessLayer.GetPassedTestCountByint(LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }

        //static void Main(string[] args)
        //{
        //    TestAddTests();
        //    //   TestFindTests();
        //   // TestUpdateTests();
        //    //  TestDeleteTests();
        //    Console.ReadLine();
        //}

        ////    // ... Existing code ...

        //static void TestAddTests()
        //{
        //    clsTest tests = new clsTest();
        //    tests.TestAppointmentID = 2128;
        //    tests.TestResult = true;
        //    tests.Notes = "Test notes";
        //    tests.CreatedByUserID = 1;

        //    if (tests._AddNewTest())
        //    {
        //        Console.WriteLine("Tests added successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to add Tests.");
        //    }
        //}

        //static void TestFindTests()
        //{
        //    int testsIdToFind = 66; // Replace with the actual Tests ID to find

        //    clsTest foundTests = clsTest.Find(testsIdToFind);

        //    if (foundTests != null)
        //    {
        //        Console.WriteLine($"Found Tests: TestID={foundTests.TestID}, TestAppointmentID={foundTests.TestAppointmentID}, TestResult={foundTests.TestResult}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Tests not found.");
        //    }
        //}

        //static void TestUpdateTests()
        //{
        //    int testsIdToUpdate = 66;

        //    clsTest tests = clsTest.Find(testsIdToUpdate);

        //    if (tests != null)
        //    {
        //        Console.WriteLine(tests.TestID);
        //        Console.WriteLine(tests.CreatedByUserID);

        //        tests.TestAppointmentID = 112;
        //        tests.TestResult = false;
        //        tests.Notes = "Updated test notes";
        //        tests.CreatedByUserID = 26;

        //        if (tests.Save())
        //        {
        //            Console.WriteLine("Tests updated successfully!");
        //        }
        //        else
        //        {
        //            Console.WriteLine(tests.CreatedByUserID);
        //            Console.WriteLine("Failed to update Tests.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Tests not found.");
        //    }
        //}

        //static void TestDeleteTests()
        //{
        //    int testsIdToDelete = 112; // Replace with the actual Tests ID to delete

        //    if (clsTest.DeleteTests(testsIdToDelete))
        //    {
        //        Console.WriteLine("Tests deleted successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to delete Tests.");
        //    }
        //}

        //// ... Rest of the code ...



    }
}
