using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace DVLD_DateAccess
{
    public class clsLicenseClassesDataAccessLayer
    {

        public static bool GetLicenseClassesInfoByName(string licenseClassName,
        ref int licenseClassID, ref string className, ref string classDescription,
        ref int minimumAllowedAge, ref int defaultValidityLength, ref decimal classFees)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT * FROM LicenseClasses WHERE ClassName = @ClassName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClassName", licenseClassName);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                licenseClassID = (int)reader["LicenseClassID"];
                                className = reader["ClassName"].ToString();
                                classDescription = reader["ClassDescription"].ToString();
                                minimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                                defaultValidityLength = (int)reader["DefaultValidityLength"];

                                // Use GetValueOrDefault to handle DBNull.Value
                                classFees = reader["ClassFees"] as decimal? ?? default(decimal);
                            }
                            // The record was not found, isFound remains false
                        }
                    }
                    catch (Exception ex)
                    {
                        isFound = false;
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }

            return isFound;
        }


        public static bool GetLicenseClassesInfoByID(int licenseClassID,
      ref string className, ref string classDescription, ref int minimumAllowedAge,
      ref int defaultValidityLength, ref decimal classFees)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT * FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseClassID", licenseClassID);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                className = reader["ClassName"].ToString();
                                classDescription = reader["ClassDescription"].ToString();
                                minimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                                defaultValidityLength = (int)reader["DefaultValidityLength"];

                                // Check for DBNull before assigning to classFees
                                if (reader["ClassFees"] != DBNull.Value)
                                {
                                    classFees = (decimal)reader["ClassFees"];
                                }
                                else
                                {
                                    classFees = 0;
                                }

                            }
                            // The record was not found, isFound remains false
                        }
                    }
                    catch (Exception ex)
                    {
                        isFound = false;
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }

            return isFound;
        }



        public static int AddNewLicenseClasses(string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, decimal ClassFees)
        {
            int LicenseClassID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"insert into LicenseClasses (ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees)
                            values (@ClassName, @ClassDescription, @MinimumAllowedAge, @DefaultValidityLength, @ClassFees)
                            select scope_identity()";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertID))
                {
                    LicenseClassID = InsertID;
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

            return LicenseClassID;
        }

        public static bool UpdateLicenseClasses(int LicenseClassID, string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, decimal ClassFees)
        {
            int RowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[LicenseClasses]
                            SET [ClassName] = @ClassName,
                                [ClassDescription] = @ClassDescription,
                                [MinimumAllowedAge] = @MinimumAllowedAge,
                                [DefaultValidityLength] = @DefaultValidityLength,
                                [ClassFees] = @ClassFees
                            WHERE [LicenseClassID] = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);

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

        public static bool DeleteLicenseClasses(int LicenseClassID)
        {
            int RowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"delete LicenseClasses where LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        public static bool DoesLicenseClassesExist(int LicenseClassID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select found = 1 from LicenseClasses where LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from LicenseClasses";

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

        public static short CountLicenseClasses()
        {
            short Count = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select count(*) from LicenseClasses";

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
    }
}
