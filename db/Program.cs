using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    class Program
    {
        static void Main(string[] args)
        {
            listAg("000000000000");

            using(var db = new testmodel())
            {
                foreach(var p in db.Product)
                {
                    Console.WriteLine($"Name: {p.Name} Product number: {p.ProductNumber}");
                }
            }
        }

        public static void listAg(string uin)
        {
            var now = DateTime.Now.ToString("yyy-MM-dd");
            string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=kaspilab;Integrated Security=true;";
            string queryString = "select c.ContractID, c.ContractType, c.StartDate, c.EndDate, a.Balance from Contracts c inner join Accounts a on " +
                "c.UIN = @uin and c.ContractID = a.ContractID";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@uin", uin);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Contract ID: {reader[0]} Contract Type: {reader[1]} Start Date: {reader[2]} Active: {String.Compare(reader[3].ToString(), now) < 0} Balance: {reader[4]}");
                    }
                    reader.Close();
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
