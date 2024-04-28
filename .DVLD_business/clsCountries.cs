using DVLD_DateAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_business.clsCountries;

namespace DVLD_business
{
    public class clsCountries
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

       

        private clsCountries(int CountryID,string CountryName)
        {
            this.CountryName = CountryName;
            this.CountryID = CountryID;

          
        }

        public clsCountries()
        {
            this.CountryName = string.Empty;
            this.CountryID = -1;


        }





        public static clsCountries Find(int countriesID)
        {
            string countryName = string.Empty;

            bool isFound = clsCountriesDataAccessLayer.GetCountriesInfoByID(countriesID, ref countryName);

            return isFound ? new clsCountries(countriesID, countryName) : null;
        }
        public static clsCountries Find(string countryName)
        {
            int countriesID = -1;

            bool isFound = clsCountriesDataAccessLayer.GetCountriesInfoByName(countryName, ref countriesID);

            return isFound ? new clsCountries(countriesID, countryName) : null;
        }


        public static DataTable GetAllCountries()
        {
            return clsCountriesDataAccessLayer.GetAllCountries();
        }


    }
}
