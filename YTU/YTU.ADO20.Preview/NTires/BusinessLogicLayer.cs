using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTU.ADO20.Preview.Entities;

namespace YTU.ADO20.Preview.NTires
{
    public class BusinessLogicLayer
    {
        // UI almış olduğu değerleri bir kontrol den geçirip datanın temiz olmasını sağlar ve veriyi database logic layer a iletir. 
        DatabaseLogicLayer databaseLogic;
        public BusinessLogicLayer()
        {
            databaseLogic = new DatabaseLogicLayer();
        }

        public List<Product> getAllProducts()
        {
            List<Product> theRecords = new List<Product>();

            SqlDataReader reader = databaseLogic.getAllProduct();
            while (reader.Read())
            {
                Product theRecord = new Product();
                theRecord.ProductID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                theRecord.Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                theRecord.ProductNumber = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                theRecord.Color = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);

                theRecords.Add(theRecord);
            }
            reader.Close();
            databaseLogic.connection.Close(); // public BLL içerisinden erişmek ve bağlantıyı BLL katmanında kapatmak . 
            return theRecords;
        }
    }
}
