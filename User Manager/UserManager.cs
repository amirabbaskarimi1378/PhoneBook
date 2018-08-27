using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using PHONE;
using PhoneBook.Display;

namespace PhoneBook.UserManager
{
    class UserManager
    {
        public static void login()
        {
            
            Console.Clear();

            Console.WriteLine("Hi, Welcome To This PhoneBook... ");
            Console.WriteLine();
            Console.Write("Please Enter Your username : ");
            string Username = Console.ReadLine();
            Console.Write("Please Enter Your password : ");
            string Password = Console.ReadLine();

            string UsersqueryString = "select * from Users where UserName='" + Username + "'and UserPassword ='" + Password + "'";

            using (SqlConnection Usersconnection =
                            new SqlConnection(Program.ConnectionString))
            {
                SqlCommand Userscommand = new SqlCommand(UsersqueryString, Usersconnection);

                Usersconnection.Open();
                SqlDataReader reader = Userscommand.ExecuteReader();
                if (reader.Read())
                {
                    Program.userId = int.Parse(reader[0].ToString());
                    reader.Close();
                    Usersconnection.Close();


                }
                else
                {
                    Console.Clear();
                    reader.Close();
                    Display.Display.ShowMessage("Your Username Or Password Is Wrong!");
                    login();

                }
            }
        }
    }
}
