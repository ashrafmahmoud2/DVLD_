using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DateAccess
{
    public class clsPaymentDataAccessLayer
    {
        public static bool GetPaymentInfoByID(int PaymentID, ref int PersoneID, ref decimal Amount,
          ref DateTime PaymentDate, ref string Payfor)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from Payments where PaymentID = @PaymentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PaymentID", PaymentID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    IsFound = true;

                    PersoneID = (int)reader["PersoneID"];
                    Amount = (decimal)reader["Amount"];
                    PaymentDate = (DateTime)reader["PaymentDate"];
                    Payfor = reader["Payfor"].ToString();
                }
                else
                {
                    // The record was not found
                    IsFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static int AddNewPayment(int PersoneID, decimal Amount, DateTime PaymentDate, string Payfor)
        {
            int PaymentID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"insert into Payments (PersoneID, Amount, PaymentDate, Payfor)
                            values (@PersoneID, @Amount, @PaymentDate, @Payfor)
                            select scope_identity()";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersoneID", PersoneID);
            command.Parameters.AddWithValue("@Amount", Amount);
            command.Parameters.AddWithValue("@PaymentDate", PaymentDate);
            command.Parameters.AddWithValue("@Payfor", Payfor);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertID))
                {
                    PaymentID = InsertID;
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

            return PaymentID;
        }

        public static bool UpdatePayment(int PaymentID, int PersoneID, decimal Amount, DateTime PaymentDate, string Payfor)
        {
            int RowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[Payments]
                    SET [PersoneID] = @PersoneID,
                        [Amount] = @Amount,
                        [PaymentDate] = @PaymentDate,
                        [Payfor] = @Payfor
                    WHERE [PaymentID] = @PaymentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PaymentID", PaymentID);
            command.Parameters.AddWithValue("@PersoneID", PersoneID);
            command.Parameters.AddWithValue("@Amount", Amount);
            command.Parameters.AddWithValue("@PaymentDate", PaymentDate);
            command.Parameters.AddWithValue("@Payfor", Payfor);

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

        public static bool DeletePayment(int PaymentID)
        {
            int RowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"delete Payments where PaymentID = @PaymentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PaymentID", PaymentID);

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

        public static bool DoesPaymentExist(int PaymentID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select found = 1 from Payments where PaymentID = @PaymentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PaymentID", PaymentID);

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

        public static DataTable GetAllPayments()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"						 select *From Payments
";

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

        public static short CountPayments()
        {
            short Count = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select count(*) from Payments";

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

