using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTU.ADO20.Preview.NTires
{
    public class DatabaseLogicLayer
    {
        // SQL ile haberleşme temiz veri üzerinde işlem yapmak 
        
        public SqlConnection connection; // Bağlantı sağlar 
        SqlCommand command; // Sağlanmış olan bağlantı üzerinden TSQL komutlarını gönderir. 
        SqlDataReader reader; // SQL içerisinden gelen datanın ( işlenmemiş ) c# karşılığı olarak bize gelir. 

        public DatabaseLogicLayer()
        {
            string connectionStringText = SqlConnectionBuilder(@"DESKTOP-74LQRA6\SQLEXPRESS", "AdventureWorks2017", "sa", "1");
            connection = new SqlConnection(connectionStringText);
        }

        string SqlConnectionBuilder(string dataSource, string initialCatalog, string userId, string password)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = dataSource;
            builder.InitialCatalog = initialCatalog;
            builder.UserID = userId;
            builder.Password = password;
            return builder.ConnectionString;
        }



        public SqlDataReader getAllProduct()
        {
            command = new SqlCommand(@"
select 
ProductID,
Name,
ProductNumber,
Color
from Production.Product
", connection);
            connection.Open(); // SQL ile aramızdaki bağlantıyı açar 

            reader = command.ExecuteReader();
            return reader;
        }

    }
}
