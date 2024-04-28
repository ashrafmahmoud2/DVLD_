using DVLD_DateAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_business
{
    public class clsLicenseClasses
    {
        //stop in fix find ;


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public int MinimumAllowedAge { get; set; }
        public int DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }

        public clsLicenseClasses()
        {
            this.LicenseClassID = -1;
            this.ClassName = string.Empty;
            this.ClassDescription = string.Empty;
            this.MinimumAllowedAge = 0;
            this.DefaultValidityLength = 0;
            this.ClassFees = 0.0M;
        }

        private clsLicenseClasses(int licenseClassID, string className, string classDescription,
            int minimumAllowedAge, int defaultValidityLength, decimal classFees)
        {
            this.LicenseClassID = licenseClassID;
            this.ClassName = className;
            this.ClassDescription = classDescription;
            this.MinimumAllowedAge = minimumAllowedAge;
            this.DefaultValidityLength = defaultValidityLength;
            this.ClassFees = classFees;
        }

        private bool _AddNewLicenseClasses()
        {
            this.LicenseClassID = clsLicenseClassesDataAccessLayer.AddNewLicenseClasses(
                this.ClassName, this.ClassDescription, this.MinimumAllowedAge,
                this.DefaultValidityLength, this.ClassFees);

            return (this.LicenseClassID != -1);
        }

        private bool _UpdateLicenseClasses()
        {
            return clsLicenseClassesDataAccessLayer.UpdateLicenseClasses(
                this.LicenseClassID, this.ClassName, this.ClassDescription,
                this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClasses())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLicenseClasses();
            }

            return false;
        }

        public static clsLicenseClasses Find(int LicenseClassID)
        {
            string className = string.Empty;
            string classDescription = string.Empty;
            int minimumAllowedAge = 0;
            int defaultValidityLength = 0;
            decimal classFees = 0;

            if (clsLicenseClassesDataAccessLayer.GetLicenseClassesInfoByID(
                LicenseClassID, ref className, ref classDescription,
                ref minimumAllowedAge, ref defaultValidityLength, ref classFees))
            {
                return new clsLicenseClasses
                {
                    LicenseClassID = LicenseClassID,
                    ClassName = className,
                    ClassDescription = classDescription,
                    MinimumAllowedAge = minimumAllowedAge,
                    DefaultValidityLength = defaultValidityLength,
                    ClassFees = classFees
                };
            }
            else
            {
                return null;
            }
        }
        public static clsLicenseClasses Find(string LicenseClassName)
        {
            int licenseClassID = 0; // Added licenseClassID
            string className = string.Empty;
            string classDescription = string.Empty;
            int minimumAllowedAge = 0;
            int defaultValidityLength = 0;
            decimal classFees = 0;

            if (clsLicenseClassesDataAccessLayer.GetLicenseClassesInfoByName(
                LicenseClassName, ref licenseClassID, ref className, ref classDescription,
                ref minimumAllowedAge, ref defaultValidityLength, ref classFees))
            {
                return new clsLicenseClasses
                {
                    LicenseClassID = licenseClassID, // Populate LicenseClassID
                    ClassName = className,
                    ClassDescription = classDescription,
                    MinimumAllowedAge = minimumAllowedAge,
                    DefaultValidityLength = defaultValidityLength,
                    ClassFees = classFees
                };
            }
            else
            {
                return null;
            }
        }


        public static bool DeleteLicenseClasses(int LicenseID)
        {
            return clsLicenseClassesDataAccessLayer.DeleteLicenseClasses(LicenseID);
        }

        public static bool DoesLicenseClassesExist(int LicenseID)
        {
            return clsLicenseClassesDataAccessLayer.DoesLicenseClassesExist(LicenseID);
        }

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassesDataAccessLayer.GetAllLicenseClasses();
        }

        public static short Count()
        {
            return clsLicenseClassesDataAccessLayer.CountLicenseClasses();
        }


        //static void Main(string[] args)
        //{
        //    // TestAddLicenseClasses();
        //    //  TestFindLicenseClasses();
        //    //  TestUpdateLicenseClasses();
        //    // TestDeleteLicenseClasses();
        //    Console.WriteLine("LicenseClasses");
        //    Console.ReadLine();
        //}



        static void TestAddLicenseClasses()
        {
            clsLicenseClasses licenseClass = new clsLicenseClasses
            {
                ClassName = "Test Class",
                ClassDescription = "Test Description",
                MinimumAllowedAge = 18,
                DefaultValidityLength = 365,
                ClassFees = 100.0M
            };

            // Set the mode to AddNew
            licenseClass.Mode = clsLicenseClasses.enMode.AddNew;

            if (licenseClass.Save())
            {
                Console.WriteLine("License Class added successfully!");
                Console.WriteLine($"LicenseClassID: {licenseClass.LicenseClassID}");
            }
            else
            {
                Console.WriteLine("Failed to add License Class.");
            }
        }

        static void TestFindLicenseClasses()
        {
            int licenseIDToFind = 2;


            clsLicenseClasses foundLicense = clsLicenseClasses.Find(licenseIDToFind);

            if (foundLicense != null)
            {
                Console.WriteLine("License Found:");
                Console.WriteLine($"LicenseClassID: {foundLicense.LicenseClassID}");
                Console.WriteLine($"ClassName: {foundLicense.ClassName}");
                Console.WriteLine($"ClassDescription: {foundLicense.ClassDescription}");
                Console.WriteLine($"MinimumAllowedAge: {foundLicense.MinimumAllowedAge}");
                Console.WriteLine($"DefaultValidityLength: {foundLicense.DefaultValidityLength}");
                Console.WriteLine($"ClassFees: {foundLicense.ClassFees}");
            }
            else
            {
                Console.WriteLine("License not found.");
            }
        }


        static void TestUpdateLicenseClasses()
        {
            // Assuming there is an existing license ID for testing
            int licenseIDToUpdate = 2;

            clsLicenseClasses licenseClass = clsLicenseClasses.Find(licenseIDToUpdate);

            if (licenseClass != null)
            {
                // Modify the properties you want to update
                licenseClass.ClassName = "Updated Class Name";
                licenseClass.ClassDescription = "Updated Description";
                licenseClass.MinimumAllowedAge = 21;
                licenseClass.DefaultValidityLength = 730;
                licenseClass.ClassFees = 150.0M;

                // Set the mode to Update
                licenseClass.Mode = clsLicenseClasses.enMode.Update;

                if (licenseClass.Save())
                {
                    Console.WriteLine("License Class updated successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to update License Class.");
                }
            }
            else
            {
                Console.WriteLine("License not found.");
            }
        }

        static void TestDeleteLicenseClasses()
        {
            // Assuming there is an existing license ID for testing
            int licenseIDToDelete = 8;

            bool isDeleted = clsLicenseClasses.DeleteLicenseClasses(licenseIDToDelete);

            if (isDeleted)
            {
                Console.WriteLine("License Class deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete License Class.");
            }
        }

    }
}
