using DVLD_DateAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_business
{
    public class clsPerson
    {
        //make a view in perssone && but male or female 

        // the => in Gender
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enGender { Male = 0, Female = 1 };

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {SecondName} {ThirdName} {LastName}"; }
        }

        public DateTime DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }

        private string _ImagePath;
        public string ImagePath { get { return _ImagePath; } set { _ImagePath = value; } }

        public clsCountries CuntryInfo;
        //  public string GenderName => _GetGenderName(this.Gender);

        public clsPerson()
        {
            this.PersonID = -1;
            this.NationalNo = string.Empty;
            this.FirstName = string.Empty;
            this.SecondName = string.Empty;
            this.ThirdName = string.Empty;
            this.LastName = string.Empty;
            this.DateOfBirth = DateTime.Now;
            this.Gender = (byte)enGender.Male;
            this.Address = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;
            this.NationalityCountryID = -1;
            this.ImagePath = string.Empty;

            Mode = enMode.AddNew;
        }

        protected clsPerson(int PersonID, string NationalNo, string FirstName, string SecondName,
          string ThirdName, string LastName, DateTime DateOfBirth, byte Gender,
          string Address, string Phone, string Email, int NationalityCountryID,
          string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            this.CuntryInfo = clsCountries.Find(NationalityCountryID);

            Mode = enMode.Update;
        }



        public bool _AddNewPerson()
        {
            this.PersonID = clsPersonDataAccessLayer.AddNewPerson(this.NationalNo, this.FirstName,
                this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth,
                this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID,
                this.ImagePath);

            return (this.PersonID != -1);
        }

        public bool _UpdatePerson()
        {
            return clsPersonDataAccessLayer.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName,
                this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth,
                this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID,
                this.ImagePath);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePerson();
            }

            return false;
        }

        public static clsPerson Find(int PersonID)
        {
            string NationalNo = string.Empty;
            string FirstName = string.Empty;
            string SecondName = string.Empty;
            string ThirdName = string.Empty;
            string LastName = string.Empty;
            DateTime DateOfBirth = DateTime.Now;
            byte Gender = 0;
            string Address = string.Empty;
            string Phone = string.Empty;
            string Email = string.Empty;
            int NationalityCountryID = 0;
            string ImagePath = string.Empty;

            bool IsFound = clsPersonDataAccessLayer.GetPersonInfoByID(PersonID, ref NationalNo, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth,
                ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID,
                ref ImagePath);

            if (IsFound)
            {
                return new clsPerson(PersonID, NationalNo, FirstName,
                    SecondName, ThirdName, LastName, DateOfBirth,
                    Gender, Address, Phone, Email, NationalityCountryID,
                    ImagePath);
            }
            else
            {
                return null;
            }
        }

        public static clsPerson Find(string nationalNo)
        {
            int personID = 0; // Assuming you need to retrieve PersonID as well
            string firstName = string.Empty;
            string secondName = string.Empty;
            string thirdName = string.Empty;
            string lastName = string.Empty;
            DateTime dateOfBirth = DateTime.Now;
            byte gender = 0;
            string address = string.Empty;
            string phone = string.Empty;
            string email = string.Empty;
            int nationalityCountryID = 0;
            string imagePath = string.Empty;

            bool isFound = clsPersonDataAccessLayer.
                GetPersonInfoByNationalNo(nationalNo, ref firstName,
                ref secondName, ref thirdName, ref lastName, ref dateOfBirth,
                ref gender, ref address, ref phone, ref email, ref nationalityCountryID,
                ref imagePath);

            if (isFound)
            {
                return new clsPerson(personID, nationalNo, firstName,
                    secondName, thirdName, lastName, dateOfBirth,
                    gender, address, phone, email, nationalityCountryID,
                    imagePath);
            }
            else
            {
                return null;
            }
        }

        private string _GetGenderName(enGender gender)
        {
            switch (gender)
            {
                case enGender.Male:
                    return "Male";

                case enGender.Female:
                    return "Female";

                default:
                    return "Unknown";
            }
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPersonDataAccessLayer.DeletePerson(PersonID);
        }

        public virtual bool Delete()
        {
            return DeletePerson(this.PersonID);
        }

        public static int Count()
        {
            return clsPersonDataAccessLayer.Count();
        }

        public static bool DoesPersonExist(int PersonID)
        {
            return clsPersonDataAccessLayer.DoesPersonExist(PersonID);
        }
        public static bool DoesPersonExist(string nationalNo)
        {
            return clsPersonDataAccessLayer.DoesPersonExist(nationalNo);
        }



        public static DataTable GetAllPeople()
        {
            return clsPersonDataAccessLayer.GetAllPeople();
        }
        //static void Main(string[] args)
        //{
        //    // TestAddPerson();
        //    // TestFindPerson();
        //    // TestUpdatePerson();
        //    //  TestDeletePerson();
        //    Console.ReadLine();
        //}

        //    static void TestAddPerson()
        //    {
        //        clsPerson person = new clsPerson();
        //        person.NationalNo = "88";
        //        person.FirstName = "John3";
        //        person.LastName = "Doe";
        //        person.DateOfBirth = new DateTime(1990, 1, 1);
        //        person.Gender = (byte)clsPerson.enGender.Male;
        //        person.Address = "123 Main St";
        //        person.Phone = "555-1234";
        //        person.Email = "john.doe@example.com";
        //        person.NationalityCountryID = 1;
        //        person.ImagePath = "path/to/image.jpg";

        //        if (person.Save())
        //        {
        //            Console.WriteLine("Person added successfully!");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Failed to add person.");
        //        }
        //    }

        //    static void TestFindPerson()
        //    {
        //        int personIdToFind = 1; // Replace with the actual person ID to find

        //        clsPerson foundPerson = clsPerson.Find(personIdToFind);

        //        if (foundPerson != null)
        //        {
        //            Console.WriteLine($"Found person: {foundPerson.FirstName} {foundPerson.LastName}");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Person not found.");
        //        }
        //    }

        //    static void TestUpdatePerson()
        //    {
        //        int personIdToUpdate = 1; // Replace with the actual person ID to update

        //        clsPerson personToUpdate = clsPerson.Find(personIdToUpdate);

        //        if (personToUpdate != null)
        //        {
        //            personToUpdate.FirstName = "UpdatedJohn";
        //            personToUpdate.LastName = "UpdatedDoe";

        //            if (personToUpdate.Save())
        //            {
        //                Console.WriteLine("Person updated successfully!");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Failed to update person.");
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Person not found.");
        //        }
        //    }

        //    static void TestDeletePerson()
        //    {
        //        int personIdToDelete = 1028; // Replace with the actual person ID to delete

        //        if (clsPerson.DeletePerson(personIdToDelete))
        //        {
        //            Console.WriteLine("Person deleted successfully!");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Failed to delete person.");
        //        }
        //    }
        //}

    }
}

