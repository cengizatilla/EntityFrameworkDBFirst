using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTU.ADO20.Preview.Entities;
using YTU.ADO20.Preview.NTires;

namespace YTU.ADO20.Preview
{
    class Program
    {
        static void Main(string[] args)
        {
            // UI => Windows Form | Web Form 
            BusinessLogicLayer bll = new BusinessLogicLayer();
            List<Product> theRecordList = bll.getAllProducts();
            if (theRecordList != null && theRecordList.Count > 0)
            {
                foreach (var item in theRecordList)
                {
                    Console.WriteLine($"{item.Name} - {item.ProductNumber}");
                }
            }
            else
            {
                Console.WriteLine("Product listesine ulaşılamadı");
            }
        }
    }
}
