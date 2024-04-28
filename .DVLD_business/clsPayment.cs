using DVLD_DateAccess;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_business
{
    public class clsPayment
    {
        public int PaymentID { get; set; }
    public int PersoneID { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Payfor { get; set; }

    public enum enMode { AddNew = 0, Update = 1 };
    public enMode Mode = enMode.AddNew;

    public clsPayment()
    {
        this.PaymentID = -1;
        this.PersoneID = -1;
        this.Amount = 0.0m;
        this.PaymentDate = DateTime.MinValue;
        this.Payfor = string.Empty;
    }

    private clsPayment(int PersoneID, decimal Amount, DateTime PaymentDate, string Payfor)
    {
        this.PaymentID = PaymentID;
        this.PersoneID = PersoneID;
        this.Amount = Amount;
        this.PaymentDate = PaymentDate;
        this.Payfor = Payfor;

        Mode = enMode.Update;
    }

    public bool _AddNewPayment()
    {
        this.PaymentID = clsPaymentDataAccessLayer.AddNewPayment(this.PersoneID, this.Amount, this.PaymentDate, this.Payfor);
        return (this.PaymentID != -1);
    }

    private bool _UpdatePayment()
    {
        return clsPaymentDataAccessLayer.UpdatePayment(this.PaymentID, this.PersoneID, this.Amount, this.PaymentDate, this.Payfor);
    }

    public bool Save()
    {
        switch (Mode)
        {
            case enMode.AddNew:
                if (_AddNewPayment())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }

            case enMode.Update:
                return _UpdatePayment();
        }

        return false;
    }

    public static clsPayment Find(int PaymentID)
    {
        int PersoneID = -1;
        decimal Amount = 0.0m;
        DateTime PaymentDate = DateTime.MinValue;
        string Payfor = string.Empty;

        bool isFound = clsPaymentDataAccessLayer.GetPaymentInfoByID(
            PaymentID, ref PersoneID, ref Amount, ref PaymentDate, ref Payfor);

        if (isFound)
        {
            return new clsPayment
            {
                PaymentID = PaymentID,
                PersoneID = PersoneID,
                Amount = Amount,
                PaymentDate = PaymentDate,
                Payfor = Payfor
            };
        }
        else
        {
            return null;
        }
    }

    public static bool DeletePayment(int PaymentID)
    {
        return clsPaymentDataAccessLayer.DeletePayment(PaymentID);
    }

    public static bool DoesPaymentExist(int PaymentID)
    {
        return clsPaymentDataAccessLayer.DoesPaymentExist(PaymentID);
    }

    public static DataTable GetAllPayments()
    {
        return clsPaymentDataAccessLayer.GetAllPayments();
    }

    public static short Count()
    {
        return clsPaymentDataAccessLayer.CountPayments();
    }


        //static void Main(string[] args)
        //{
        //    TestAddPayment();
        //    TestFindPayment();
        //    TestUpdatePayment();
        //    TestDeletePayment();

        //    Console.ReadLine();
        //}

        static void TestAddPayment()
        {
            clsPayment payment = new clsPayment();
            payment.PersoneID = 1;
            payment.Amount = 50.00M;
            payment.PaymentDate = DateTime.Now;
            payment.Payfor = "Service Fee";

            if (payment.Save())
            {
                Console.WriteLine("Payment added successfully!");
            }
            else
            {
                Console.WriteLine("Failed to add Payment.");
            }
        }

        static void TestFindPayment()
        {
            int paymentIdToFind = 1; // Replace with the actual Payment ID to find

            clsPayment foundPayment = clsPayment.Find(paymentIdToFind);

            if (foundPayment != null)
            {
                Console.WriteLine($"Found Payment: PersoneID={foundPayment.PersoneID}, Amount={foundPayment.Amount}");
            }
            else
            {
                Console.WriteLine("Payment not found.");
            }
        }

        static void TestUpdatePayment()
        {
            int paymentIdToUpdate = 1;

            clsPayment payment = clsPayment.Find(paymentIdToUpdate);

            if (payment != null)
            {
                Console.WriteLine(payment.PaymentID);
                Console.WriteLine(payment.PersoneID);

                payment.PersoneID = 2;
                payment.Amount = 75.00M;
                payment.PaymentDate = DateTime.Now;
                payment.Payfor = "Renewal Fee";

                if (payment._UpdatePayment())
                {
                    Console.WriteLine("Payment updated successfully!");
                }
                else
                {
                    Console.WriteLine(payment.PersoneID);
                    Console.WriteLine("Failed to update Payment.");
                }
            }
            else
            {
                Console.WriteLine("Payment not found.");
            }
        }

        static void TestDeletePayment()
        {
            int paymentIdToDelete = 1;

            if (clsPayment.DeletePayment(paymentIdToDelete))
            {
                Console.WriteLine("Payment deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete Payment.");
            }
        }
    }
}
