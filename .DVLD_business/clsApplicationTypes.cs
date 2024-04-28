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
    public class clsApplicationType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public float ApplicationFees { get; set; }

        public clsApplicationType()
        {
            this.ApplicationTypeID = -1;
            this.ApplicationTypeTitle = string.Empty;
            this.ApplicationFees = 0;
        }

        private clsApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, float
            
            ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;

            Mode = enMode.Update;
        }

        private bool _AddNewApplicationTypes()
        {
            this.ApplicationTypeID = clsApplicationTypeDataAccessLayer.
                AddNewApplicationTypes(this.ApplicationTypeTitle, this.ApplicationFees);

            return (this.ApplicationTypeID != -1);
        }

        public bool _UpdateApplicationTypes()
        {
            return clsApplicationTypeDataAccessLayer.
                UpdateApplicationTypes(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationTypes())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateApplicationTypes();
            }

            return false;
        }

        public static clsApplicationType Find(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = string.Empty;
            float ApplicationFees = 0;

            bool isFound = clsApplicationTypeDataAccessLayer.GetApplicationTypeInfoById(
                ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees);

            if (isFound)
            {
                return new clsApplicationType
                {
                    ApplicationTypeID = ApplicationTypeID,
                    ApplicationTypeTitle = ApplicationTypeTitle,
                    ApplicationFees = ApplicationFees
                };
            }
            else
            {
                return null;
            }
        }

        public static bool DeleteApplicationTypes(int ApplicationTypeID)
        {
            return clsApplicationTypeDataAccessLayer.DeleteApplicationTypes(ApplicationTypeID);
        }

        public static bool DoesApplicationTypesExist(int ApplicationTypeID)
        {
            return clsApplicationTypeDataAccessLayer.DoesApplicationTypesExist(ApplicationTypeID);
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeDataAccessLayer.GetAllApplicationTypes();
        }

        public static short Count()
        {
            return clsApplicationTypeDataAccessLayer.CountApplicationTypes();
        }

        //Additional methods...

        //static void Main(string[] args)
        //{
        //    //   TestAddApplicationTypes();
        //     TestFindApplicationTypes();
        //  //  TestUpdateApplicationTypes();
        //    //  TestDeleteApplicationTypes();
        //    Console.ReadLine();
        //}

        //static void TestAddApplicationTypes()
        //{
        //    clsApplicationType ApplicationTypes = new clsApplicationType();
        //    ApplicationTypes.ApplicationTypeTitle = "New Application Type";
        //    ApplicationTypes.ApplicationFees = 50.0M;

        //    if (ApplicationTypes.Save())
        //    {
        //        Console.WriteLine("ApplicationTypes added successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to add ApplicationTypes.");
        //    }
        //}

        static void TestFindApplicationTypes()
        {
            int ApplicationTypesIdToFind = 5; // Replace with the actual ApplicationTypes ID to find

            clsApplicationType foundApplicationTypes = clsApplicationType.Find(ApplicationTypesIdToFind);

            if (foundApplicationTypes != null)
            {
                Console.WriteLine($"Found ApplicationTypes: ApplicationTypeID={foundApplicationTypes.ApplicationTypeID}, ApplicationTypeTitle={foundApplicationTypes.ApplicationTypeTitle}");
            }
            else
            {
                Console.WriteLine("ApplicationTypes not found.");
            }
        }

        //static void TestUpdateApplicationTypes()
        //{
        //    int ApplicationTypesIdToUpdate = 10; // Replace with the actual ApplicationTypes ID to update

        //    clsApplicationType ApplicationTypes = clsApplicationType.Find(8);

        //    if (ApplicationTypes != null)
        //    {
        //        Console.WriteLine($"Current ApplicationTypeTitle:" +
        //            $" {ApplicationTypes.ApplicationTypeTitle}");

        //        Modify the properties
        //        ApplicationTypes.ApplicationTypeTitle = "  Type";
        //        ApplicationTypes.ApplicationFees = 75.0M;

        //        if (ApplicationTypes._UpdateApplicationTypes())
        //        {
        //            Console.WriteLine("ApplicationTypes updated successfully!");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Failed to update ApplicationTypes.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("ApplicationTypes not found.");
        //    }
        //}

        //static void TestDeleteApplicationTypes()
        //{
        //    int ApplicationTypesIdToDelete = 9; // Replace with the actual ApplicationTypes ID to delete

        //    if (clsApplicationType.DeleteApplicationTypes(ApplicationTypesIdToDelete))
        //    {
        //        Console.WriteLine("ApplicationTypes deleted successfully!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to delete ApplicationTypes.");
        //    }
        //}


    }
}
