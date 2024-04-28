using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DateAccess
{
    public class clsApplicationTypeDataAccessLayer
    {
        


        public static int AddNewApplicationTypes(string ApplicationTypeTitle, float ApplicationFees)
        {
            int ApplicationTypeID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"insert into ApplicationTypes (ApplicationTypeTitle, ApplicationFees)
                            values (@ApplicationTypeTitle, @ApplicationFees)
                            select scope_identity()";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertID))
                {
                    ApplicationTypeID = InsertID;
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

            return ApplicationTypeID;
        }

        public static bool UpdateApplicationTypes(int ApplicationTypeID,
            string ApplicationTypeTitle, float ApplicationFees)
        {
            int RowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[ApplicationTypes]
                            SET [ApplicationTypeTitle] = @ApplicationTypeTitle,
                                [ApplicationFees] = @ApplicationFees
                            WHERE [ApplicationTypeID] = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);

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

        public static bool DeleteApplicationTypes(int ApplicationTypeID)
        {
            int RowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"delete ApplicationTypes where ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

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

        public static bool DoesApplicationTypesExist(int ApplicationTypeID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select found = 1 from ApplicationTypes where ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                IsFound = (result != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from ApplicationTypes";

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
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static short CountApplicationTypes()
        {
            short Count = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select count(*) from ApplicationTypes";

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
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return Count;
        }


        public static bool GetApplicationTypeInfoById(int applicationTypeId, 
            ref string applicationTypeTitle, ref float applicationFees)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationTypeID", applicationTypeId);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                applicationTypeTitle = reader["ApplicationTypeTitle"].ToString();
                                Console.WriteLine("Record found.");

                                if (!reader.IsDBNull(reader.GetOrdinal("ApplicationFees")))
                                {
                                    // Assuming ApplicationFees is of type float
                                    applicationFees = (float)(decimal)reader["ApplicationFees"];
                                }
                                else
                                {
                                    Console.WriteLine("ApplicationFees is DBNull. Setting a default value.");
                                    // applicationFees = someDefaultValue;
                                }

                            }
                            else
                            {
                                Console.WriteLine("Record not found.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Consider using a logging framework for better error handling and reporting
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }

            return isFound;
        }

        public static void TestFindApplicationTypeInfo()
        {
            int applicationTypeId = 1;
            string applicationTypeTitle = string.Empty;
            float applicationFees = 0;

            bool isFound = GetApplicationTypeInfoById(applicationTypeId, ref applicationTypeTitle, ref applicationFees);

            if (isFound)
            {
                Console.WriteLine($"Application Type ID: {applicationTypeId}");
                Console.WriteLine($"Application Type Title: {applicationTypeTitle}");
                Console.WriteLine($"Application Fees: {applicationFees}");
            }
            else
            {
                Console.WriteLine($"Application Type with ID {applicationTypeId} not found.");
            }
        }

        static void Main(string[] args)
        {
            TestFindApplicationTypeInfo();
            Console.WriteLine("in ApplicationTYPE");
            Console.ReadLine();
        }

    }
}
