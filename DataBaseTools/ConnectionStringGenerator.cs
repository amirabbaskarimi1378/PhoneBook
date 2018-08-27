using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace PhoneBook.DataBaseTools
{
    public class ConnectionStringGenerator
    {
        private static SqlConnection connection;
        private ConnectionStringGenerator()
        {

        }
        public static SqlConnection Generate()
        {
           
                if (connection == null)
                {
                    connection = new SqlConnection("Data Source=(local);Initial Catalog=PhoneContactDb;"
               + "Integrated Security=true");
                }
                return connection;    
        }
    }
}
