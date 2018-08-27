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

namespace PhoneBook.ContactManager
{
    class ContactManager
    {
        public static void InsertContact()
        {
            Console.Clear();


            Console.Write("PLease Enter your Contact Name : ");
            string name = Console.ReadLine();

            Console.Write("Please Enter PhoneNumber : ");
            string phonenumber = Console.ReadLine();

            if (IsExistContacts(phonenumber, name))
            {
                Display.Display.ShowMessage("This Contact Were Added before !");
            }
            else
            {
                string ContactqueryString = "insert into Contacts(ContactName,ContactPhoneNumber,UserId) values (@Name,@Phonenumber,@Ui)";

                using (SqlConnection Contactconnection =
                new SqlConnection(Program.ConnectionString))
                {
                    SqlCommand Contactcommand = new SqlCommand(ContactqueryString, Contactconnection);

                    Contactconnection.Open();

                    Contactcommand.Parameters.AddWithValue("@Name", name); Contactcommand.Parameters.AddWithValue("@Phonenumber", phonenumber);
                    Contactcommand.Parameters.AddWithValue("@Ui",Program.userId);
                    Contactcommand.ExecuteNonQuery();
                    Contactconnection.Close();
                    Display.Display.ShowMessage("Your Operation was successful ! ");


                }

            }
        }
        public static void SearchContact()
        {
            Console.Write("search your contact name : ");
            string name = Console.ReadLine();

            string SearchqueryString = "select ContactName ,ContactPhoneNumber from Contacts where ContactName like @search and UserId='" +Program.userId + "' ";

            using (SqlConnection Searchconnection =
            new SqlConnection(Program.ConnectionString))
            {
                SqlCommand Searchcommand = new SqlCommand(SearchqueryString, Searchconnection);

                Searchconnection.Open();

                Searchcommand.Parameters.AddWithValue("@search", "%" + name + "%");
                Searchcommand.ExecuteNonQuery();


                SqlDataAdapter adapter = new SqlDataAdapter(Searchcommand);
                DataTable Datatablesearch = new DataTable();
                adapter.Fill(Datatablesearch);

                foreach (DataRow row in Datatablesearch.Rows)
                {
                    Console.WriteLine(row[0] + " - " + row[1]);
                }
                Searchconnection.Close();
                Console.ReadKey();
            }

        }
        public static void ShowContacts()
        {

            string ShowqueryString = "select contactId, ContactName ,ContactPhoneNumber from Contacts where UserId='" +Program.userId + "' order by contactId";

            using (SqlConnection Showconnection =
            new SqlConnection(Program.ConnectionString))
            {
                SqlCommand Showcommand = new SqlCommand(ShowqueryString, Showconnection);

                Showconnection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(Showcommand);
                DataTable ShowDataTable = new DataTable();
                adapter.Fill(ShowDataTable);
                foreach (DataRow row in ShowDataTable.Rows)
                {
                    Console.WriteLine($" {row["ContactId"]}-*{row["ContactName"]} - {row["ContactPhoneNumber"]}");
                }
            }

        }
        public static void EditContacts()
        {
            Console.Clear();
            ShowContacts();
            Console.WriteLine("Please Enter Row Number Of Contact That You Want to Edit : ");
            string contactId = Console.ReadLine();

            if (IsExistContacts(contactId))
            {

                string ShowqueryString = "select ContactName,ContactPhoneNumber from Contacts where ContactId='" + contactId + "'";

                using (SqlConnection Showconnection =
                new SqlConnection(Program.ConnectionString))
                {
                    SqlCommand Showcommand = new SqlCommand(ShowqueryString, Showconnection);

                    Showconnection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(Showcommand);
                    DataTable ShowDataTable = new DataTable();
                    adapter.Fill(ShowDataTable);
                    int i = 1;
                    foreach (DataRow row in ShowDataTable.Rows)
                    {
                        Console.WriteLine($" {row["ContactName"]} - {row["ContactPhoneNumber"]}");
                    }
                    Console.WriteLine("Do You Want To Edit This Contact ? (Y/N)");
                    string answer = Console.ReadLine();
                    if (answer.Trim().ToLower() == "y")
                    {
                        Console.Clear();
                        Console.Write("Please Enter New Name : ");
                        string name = Console.ReadLine();
                        Console.Write("Please Enter New Phone Number :");
                        string PhoneNumber = Console.ReadLine();


                        string EditQueryString = "update Contacts set ContactName = @name , ContactPhoneNumber=@PhoneNumber where ContactId='" + contactId + "' ";

                        using (SqlConnection editConnection =
               new SqlConnection(Program.ConnectionString))
                        {
                            SqlCommand editCommand = new SqlCommand(EditQueryString, editConnection);

                            editConnection.Open();
                            editCommand.Parameters.AddWithValue("@Name", name); editCommand.Parameters.AddWithValue("@Phonenumber", PhoneNumber);
                            editCommand.ExecuteNonQuery();
                            editConnection.Close();
                            Display.Display.ShowMessage("Your Operation was successful ! ");
                        }
                        if (answer.Trim().ToLower() == "n")
                        {
                            Console.Clear();
                            Console.WriteLine("Choose Your Contact Again !");
                        }
                        Console.ReadKey();
                    }
                }

            }
            else
            {
                Display.Display.ShowMessage("This Contact Is Not Exist In Your List ! ");
                EditContacts();
            }

        }
        public static void RemoveContacts()
        {

            Console.Clear();
            ShowContacts();
            Console.WriteLine("Please Enter Row Number Of Contact That You Want to Delete : ");
            string contactId = Console.ReadLine();

            if (IsExistContacts(contactId))
            {
                string ShowqueryString = "select ContactName,ContactPhoneNumber from Contacts where ContactId='" + contactId + "'";

                using (SqlConnection Showconnection =
                new SqlConnection(Program.ConnectionString))
                {
                    SqlCommand Showcommand = new SqlCommand(ShowqueryString, Showconnection);

                    Showconnection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(Showcommand);
                    DataTable ShowDataTable = new DataTable();
                    adapter.Fill(ShowDataTable);
                    foreach (DataRow row in ShowDataTable.Rows)
                    {
                        Console.WriteLine($" {row["ContactName"]} - {row["ContactPhoneNumber"]}");
                    }
                    Console.WriteLine("Do You Want To Delete This Contact ? (Y/N)");
                    string answer = Console.ReadLine();
                    if (answer.Trim().ToLower() == "y")
                    {
                        string deleteQueryString = "delete from Contacts where ContactId='" + contactId + "' ";

                        using (SqlConnection deleteConnection =
               new SqlConnection(Program.ConnectionString))
                        {
                            deleteConnection.Open();
                            SqlCommand deleteCommand = new SqlCommand(deleteQueryString, deleteConnection);
                            deleteCommand.ExecuteNonQuery();
                            deleteConnection.Close();
                            Display.Display.ShowMessage("Your Operation was successful ! ");

                        }
                        if (answer.Trim().ToLower() == "n")
                        {
                            Console.Clear();
                            Console.WriteLine("Choose Your Contact Again !");
                        }
                        Console.ReadKey();
                    }
                }

            }
            else
            {
                Display.Display.ShowMessage("This Contact Is Not Exist In Your List ! ");
                EditContacts();
            }

        }
        private static bool IsExistContacts(string InfContact)
        {
            string getqueryString = "select ContactName,ContactPhoneNumber from Contacts where ContactId='" + InfContact + "'";

            using (SqlConnection getContactConnection =
            new SqlConnection(Program.ConnectionString))
            {
                SqlCommand getContactCommand = new SqlCommand(getqueryString, getContactConnection);

                getContactConnection.Open();
                SqlDataReader reader = getContactCommand.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    return false;
                }

            }
        }
        private static bool IsExistContacts(string phonenumber, string name)
        {

            string getqueryString = "select ContactName,ContactPhoneNumber from Contacts where ContactName='" + name + "'or ContactPhoneNumber ='" + phonenumber + "'";

            using (SqlConnection getContactConnection =
            new SqlConnection(Program.ConnectionString))
            {
                SqlCommand getContactCommand = new SqlCommand(getqueryString, getContactConnection);

                getContactConnection.Open();
                SqlDataReader reader = getContactCommand.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    return false;
                }
            }
        }
    }
}
