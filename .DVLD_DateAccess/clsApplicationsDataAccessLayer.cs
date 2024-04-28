using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DateAccess
{
    public class clsApplicationsDataAccessLayer
    {



        public static bool GetApplicationsInfoByID(int ApplicationID,
     ref int ApplicantPersonID, ref DateTime ApplicationDate,
     ref int ApplicationTypeID, ref byte ApplicationStatus,
     ref DateTime LastStatusDate, ref float PaidFees, ref int CreatedByUserID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"select * from Applications where ApplicationID = @ApplicationID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found
                                IsFound = true;

                                ApplicantPersonID = (int)reader["ApplicantPersonID"];
                                ApplicationDate = (DateTime)reader["ApplicationDate"];
                                ApplicationTypeID = (int)reader["ApplicationTypeID"];
                                ApplicationStatus = (byte)reader["ApplicationStatus"];
                                LastStatusDate = (DateTime)reader["LastStatusDate"];

                                // Handle conversion issues for PaidFees
                                if (reader["PaidFees"] is float)
                                {
                                    PaidFees = (float)reader["PaidFees"];
                                }
                                else if (reader["PaidFees"] is double)
                                {
                                    PaidFees = (float)(double)reader["PaidFees"];
                                }
                                else
                                {
                                    // Handle other data types or conversion issues
                                    PaidFees = 0.0f; // Provide a default value or handle the issue as needed
                                }

                                CreatedByUserID = (int)reader["CreatedByUserID"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        IsFound = false;
                    }
                }
            }

            return IsFound;
        }


        public static int AddNewApplications(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, 
            int CreatedByUserID)
        {
            int ApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"insert into Applications (ApplicantPersonID, ApplicationDate, ApplicationTypeID,
                            ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
                            values (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID,
                                    @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID)
                            select scope_identity()";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertID))
                {
                    ApplicationID = InsertID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return ApplicationID;
        }

        public static bool UpdateApplications(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
     int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int RowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[Applications]
                    SET [ApplicantPersonID] = @ApplicantPersonID,
                        [ApplicationDate] = @ApplicationDate,
                        [ApplicationTypeID] = @ApplicationTypeID,
                        [ApplicationStatus] = @ApplicationStatus,
                        [LastStatusDate] = @LastStatusDate,
                        [PaidFees] = @PaidFees,
                        [CreatedByUserID] = @CreatedByUserID
                    WHERE [ApplicationID] = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                RowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return (RowAffected > 0);
        }


        public static bool DeleteApplications(int ApplicationID)
        {
            int RowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"delete Applications where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                RowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
            finally
            {
                connection.Close();
            }

            return (RowAffected > 0);
        }

        public static bool DoesApplicationsExist(int ApplicationID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select found = 1 from Applications where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                IsFound = (result != null);
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from ApplicationsDetails_view";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static short CountApplications()
        {
            short Count = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select count(*) from Applications";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && short.TryParse(result.ToString(), out short Value))
                {
                    Count = Value;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
            finally
            {
                connection.Close();
            }

            return Count;
        }

        public static int _GetPersonIDByApplicationsID(int ApplicationsID)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select ApplicantPersonID from Applications where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationsID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int Value))
                {
                    PersonID = Value;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
            finally
            {
                connection.Close();
            }

            return PersonID;
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {

            //incase the ActiveApplication ID !=-1 return true.
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != -1);
        }

        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT ActiveApplicationID=ApplicationID FROM Applications WHERE ApplicantPersonID = @ApplicantPersonID and ApplicationTypeID=@ApplicationTypeID and ApplicationStatus=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return ActiveApplicationID;
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID=@ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return ActiveApplicationID;
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }

        public static bool UpdateStatus(int ApplicationID, short NewStatus)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Applications  
                            set 
                                ApplicationStatus = @NewStatus, 
                                LastStatusDate = @LastStatusDate
                            where ApplicationID=@ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@NewStatus", NewStatus);
            command.Parameters.AddWithValue("LastStatusDate", DateTime.Now);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        //public static void TestFindApplications()
        //{
        //    // Arrange
        //    int applicationIDToSearch = 112; // Replace with a valid ApplicationID for testing

        //    int applicantPersonID = 0;
        //    DateTime applicationDate = DateTime.MinValue;
        //    int applicationTypeID = 0;
        //    byte applicationStatus = 0;
        //    DateTime lastStatusDate = DateTime.MinValue;
        //    float paidFees = 0;
        //    int createdByUserID = 0;

        //    // Act
        //    bool isFound = GetApplicationsInfoByID(applicationIDToSearch,
        //        ref applicantPersonID, ref applicationDate, ref applicationTypeID,
        //        ref applicationStatus, ref lastStatusDate, ref paidFees, ref createdByUserID);

        //    // Assert
        //    if (isFound)
        //    {
        //        Console.WriteLine("Application found:");
        //        Console.WriteLine($"Applicant Person ID: {applicantPersonID}");
        //        Console.WriteLine($"Application Date: {applicationDate}");
        //        Console.WriteLine($"Application Type ID: {applicationTypeID}");
        //        Console.WriteLine($"Application Status: {applicationStatus}");
        //        Console.WriteLine($"Last Status Date: {lastStatusDate}");
        //        Console.WriteLine($"Paid Fees: {paidFees}");
        //        Console.WriteLine($"Created By UserID: {createdByUserID}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Application not found.");
        //    }
        //}

        //static void Main(string[] args)
        //{
        //    TestFindApplications();
        //      Console.WriteLine("in Applictions");

        //    Console.ReadLine();
        //}

    }
}
        