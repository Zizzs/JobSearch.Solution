using System;
using MySql.Data.MySqlClient;
using JobSearch;
 
namespace JobSearch.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}
