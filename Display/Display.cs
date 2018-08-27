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
//using PhoneBook.ContactManager;

namespace PhoneBook.Display
{
   class Display
    {
        public static void ActMenu()
        {
            if (Program.userId != -1)
            {
                bool isDone = true;
                while (isDone)
                {
                    ShowMenu();
                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1": ContactManager.ContactManager.InsertContact(); break;
                        case "2": ContactManager.ContactManager.ShowContacts(); Console.ReadKey(); break;
                        case "3": ContactManager.ContactManager.SearchContact(); break;
                        case "4": ContactManager.ContactManager.RemoveContacts(); break;
                        case "5": ContactManager.ContactManager.EditContacts(); break;
                        case "6": isDone = false; break;
                        default:
                            Console.WriteLine("Input incorrect...");
                            break;
                    }
                }
            }
        }
        private static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("1- Insert");
            Console.WriteLine("2- ShowList");
            Console.WriteLine("3- Search");
            Console.WriteLine("4- Remove");
            Console.WriteLine("5- Edit");
            Console.WriteLine("6- Exit");
            Console.WriteLine("Please Enter :");
        }
        public static void ShowMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press eny Key to continue... ");
            Console.ReadKey();
        }
    }
}
