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
using PhoneBook.UserManager;
using PhoneBook.Display;

namespace PHONE
{
    class Program
    {
         internal static int userId = -1;
        static void Main(string[] args)
        {
            MessageBox.Show("Welcome :))))))","Hi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            UserManager.login();
            Display.ActMenu();
        }

    }
}

