

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
    public class clsTestType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public clsTestType.enTestType ID { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public float Fees { set; get; }
        public clsTestType()

        {
            this.ID = clsTestType.enTestType.VisionTest;
            this.Title = "";
            this.Description = "";
            this.Fees = 0;

        }

        public clsTestType(clsTestType.enTestType ID, string TestTypeTitel,
            string Description, float TestTypeFees)

        {
            this.ID = ID;
            this.Title = TestTypeTitel;
            this.Description = Description;

            this.Fees = TestTypeFees;
            Mode = enMode.Update;
        }

        private bool _AddNewTestType()
        {
            //call DataAccess Layer 
            
            this.ID = (clsTestType.enTestType)clsTestTypeDataAccessLayer.AddNewTestType(this.Title, this.Description, this.Fees);

            return (this.Title != "");
        }

        private bool _UpdateTestType()
        {
            //call DataAccess Layer 

            return clsTestTypeDataAccessLayer.UpdateTestType((int)this.ID, this.Title, this.Description, this.Fees);
        }

        public static clsTestType Find(clsTestType.enTestType TestTypeID)
        {
            string Title = "", Description = ""; float Fees = 0;

            if (clsTestTypeDataAccessLayer.GetTestTypeInfoByID((int)TestTypeID, ref Title, ref Description, ref Fees))

                return new clsTestType(TestTypeID, Title, Description, Fees);
            else
                return null;

        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeDataAccessLayer.GetAllTestTypes();

        }

        public static clsTestType.enTestType GetTestType(int LocalDrivingLicenseApplicationID)
        {
            int passedTestCount =
                clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);

            switch (passedTestCount)
            {
                case 0:
                    return clsTestType.enTestType.VisionTest;
                case 1:
                    return clsTestType.enTestType.WrittenTest;
                case 2:
                    return clsTestType.enTestType.StreetTest;
                default:
                    return clsTestType.enTestType.VisionTest;
            }
        }




        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestType();

            }

            return false;
        }

        public static short Count()
        {
            return clsTestTypeDataAccessLayer.CountTestTypes();
        }

        // static void TestAddTestTypes()
        //{
        //    clsTestType testTypes = new clsTestType();
        //    testTypes.Title = "Driving Test";
        //    testTypes.Description = "Test for driving skills";
        //    testTypes.Fees = 150;

        //    if (testTypes.Save())
        //    {
        //        Console.WriteLine("TestTypes added successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to add TestTypes.");
        //    }
        //}

        //public static void TestFindTestTypes()
        //{
        //    int testTypesIdToFind = 4.4; // Replace with the actual TestTypes ID to find

        //    clsTestType foundTestTypes = clsTestType.Find(testTypesIdToFind);

        //    if (foundTestTypes != null)
        //    {
        //        Console.WriteLine($"Found TestTypes: TestTypeID={foundTestTypes.TestTypeID}, TestTypeTitle={foundTestTypes.TestTypeTitle}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("TestTypes not found.");
        //    }
        //}

        //public static void TestUpdateTestTypes()
        //{
        //    int testTypesIdToUpdate = 4;

        //    clsTestType testTypes = clsTestType.Find(testTypesIdToUpdate);

        //    if (testTypes != null)
        //    {
        //        Console.WriteLine(testTypes.TestTypeID);
        //        Console.WriteLine(testTypes.TestTypeTitle);

        //        testTypes.TestTypeTitle = "Updated Test";
        //        testTypes.TestTypeDescription = "Updated description";
        //        testTypes.TestTypeFees = 200.00M;

        //        if (testTypes.Save())
        //        {
        //            Console.WriteLine("TestTypes updated successfully!");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Failed to update TestTypes.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("TestTypes not found.");
        //    }
        //}

        //public static void TestDeleteTestTypes()
        //{
        //    int testTypesIdToDelete = 6; // Replace with the actual TestTypes ID to delete

        //    if (clsTestType.DeleteTestTypes(testTypesIdToDelete))
        //    {
        //        Console.WriteLine("TestTypes deleted successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to delete TestTypes.");
        //    }
        //}

        //static void Main(string[] args)
        //{
        //    // Uncomment and run the desired test method
        //    // TestAddTestTypes();
        //     //TestFindTestTypes();
        //     //TestUpdateTestTypes();
        //     TestDeleteTestTypes();

        //    Console.ReadLine();
        //}

    }
}
