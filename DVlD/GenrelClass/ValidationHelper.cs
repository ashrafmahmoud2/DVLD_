using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD.GenrelClass
{
    public class clsValidationHelper
    {
        //MAKE ME WHEN I ADD REMOVE TEH ERRO
        public static bool IsEmailValid(string email)
        {
            return email.Contains("@") ;
        }
        public static TextBox GetGuna2TextBox(object sender)
        {
            // Implement the logic to get Guna2TextBox from sender
            // For example, if sender is Guna2TextBox, you can directly cast it
            return sender as TextBox;
        }
        public static bool ValidateStringInput(TextBox textBox, int numOfWords, bool More = false)
        {
            string input = textBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            string[] words = input.Split(' ');

            foreach (var word in words)
            {
                if (!char.IsLetter(word[0]))
                {
                    return false;
                }
            }

            if (More)
            {
                return words.Length >= numOfWords;
            }
            else
            {
                return words.Length == numOfWords;
            }
        }
        public static bool ValidateNumberInput(TextBox textBox, int numOfNumbers,bool More=false)
        {
            string input = textBox.Text.Trim();
            if (!int.TryParse(input, out int result))
            {
                return false;
            }

            string[] digits = Math.Abs(result).ToString().ToCharArray().Select(c => c.ToString()).ToArray();

          //  return digits.Length == numOfNumbers;


            if (More)
            {
                return digits.Length >= numOfNumbers;
            }
            else
            {
                return digits.Length == numOfNumbers;
            }
        }
        public static bool IsNumberOnly(string input)
        {
            return input.All(char.IsDigit);
        }


    }
}
