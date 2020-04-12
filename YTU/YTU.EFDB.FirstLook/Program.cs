using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTU.EFDB.FirstLook
{
    class Program
    {
        static void Main(string[] args)
        {

            //getAllCustomers();
            //getAllCustomerWhere1();
            // getAllProducts();
            // getAllProductsWhere1();

            //Customer customerRecord = new Customer();
            //customerRecord.CustomerID = 1001;
            //customerRecord.CustomerCode = "CS123";
            //customerRecord.FirstName = "Cengiz";
            //customerRecord.LastName = "Atilla";

            //addNewCustomer(customerRecord);

            // updateCustomer(77, "Cengiz", "Atilla");
            // deleteCustomer(77);

            //getAllCustomers();
            //addNewCustomer(new Customer()
            //{
            //    CustomerID = 77,
            //    FirstName = "Cengiz",
            //    LastName = "Atilla",
            //    CustomerCode = "CA1001",
            //    CreateDate = DateTime.Now
            //});
            //updateCustomer(77, "Cengiz", "Öztürk");
            //deleteCustomer(77);

            // getAllCustomersWithSP();
            // getCustomerWithIDSP(36);
            // addNewCustomerProductListWithSP(514, 36, 1);
            // removeCustomerProductListWithSP(36);

            //learnIQueryableInterface();
            //learnIEnumerableInterface();

            //learnTransaction(new Customer() { CustomerID = 100 }, new Product()
            //{
            //    ProductID = 700,
            //    ListPrice = 100
            //});


            useTSQLCommandWithEF();

        }

        static void useTSQLCommandWithEF()
        {
            YTUEFEntities context = new YTUEFEntities();
           // List<Customer> theRecordList = context.Customer.SqlQuery("select top 10 * from Customer").ToList();

           int resultInt =  context.Database.ExecuteSqlCommand("insert into Customer values (7000,'CA100','Ahmet','Vatansever',GETDATE())");

        }

        static void learnTransaction(Customer dataCustomer, Product dataProduct)
        {
            YTUEFEntities context = new YTUEFEntities();
            #region Transaction ile kullanımı 
            using (DbContextTransaction trns = context.Database.BeginTransaction())
            {
                try
                {
                    context.CustomerProductLists.Add(new CustomerProductList()
                    {
                        Amount = 1,
                        ListPrice = dataProduct.ListPrice,
                        ProductId = dataProduct.ProductID,
                        CustomerId = dataCustomer.CustomerID,
                        CreateDate = DateTime.Now
                    });
                    context.SaveChanges(); // SQL INSERT

                    //throw new Exception();

                    context.CustomerHistories.Add(new CustomerHistory()
                    {
                        productID = dataProduct.ProductID,
                        amount = 1,
                        stateCode = 100,
                        createDate = DateTime.Now
                    });

                    context.SaveChanges();
 
                    trns.Commit(); // Onaylamak tüm işlemleri onaylıyorsunuz...
                }
                catch (Exception ex)
                {
                    trns.Rollback(); // Geri çağırma işlemi...
                }
            }
            #endregion

            #region Transaction olmadan kullanım 
           
            context.CustomerProductLists.Add(new CustomerProductList()
            {
                Amount = 1,
                ListPrice = dataProduct.ListPrice,
                ProductId = dataProduct.ProductID,
                CustomerId = dataCustomer.CustomerID,
                CreateDate = DateTime.Now
            });
            context.SaveChanges(); // SQL INSERT

            context.CustomerHistories.Add(new CustomerHistory()
            {
                productID = dataProduct.ProductID,
                amount = 1,
                stateCode = 100,
                createDate = DateTime.Now
            });

            context.SaveChanges();
            #endregion
        }

        static void learnIQueryableInterface()
        {
            YTUEFEntities context = new YTUEFEntities();
            IQueryable<Customer> theRecordList = context.Customer.Where(i => i.CustomerID > 0);
            foreach (var item in theRecordList)
            {
                Console.WriteLine(item.FirstName);
            }
        }

        static void learnIEnumerableInterface()
        {
            YTUEFEntities context = new YTUEFEntities();
            IEnumerable<Customer> theRecordList = context.Customer.Where(i => i.CustomerID > 0).ToList();
            foreach (var item in theRecordList)
            {
                Console.WriteLine(item.FirstName);
            }
        }

        static void removeCustomerProductListWithSP(int customerID)
        {
            YTUEFEntities context = new YTUEFEntities();
            context.deleteCustomerProductList(customerID);
        }

        static void addNewCustomerProductListWithSP(int productId, int customerId, int amount)
        {
            YTUEFEntities context = new YTUEFEntities();
            #region ilk kullanım
            //int result = context.addNewCustomerProductList(customerId, productId, amount, 100M, DateTime.Now);
            #endregion
            #region Product ID değerine göre listprice * amount olarak kullanımı 
            var theProduct = context.Products.Find(productId);
            if (theProduct != null)
            {
                context.addNewCustomerProductList(customerId, productId, amount, (theProduct.ListPrice * amount), DateTime.Now);
            }
            #endregion
        }

        static void getCustomerWithIDSP(int customerID)
        {
            YTUEFEntities context = new YTUEFEntities();
            var theRecord = context.getCustomerById(customerID);
            Console.WriteLine("İşlem Tamamlandı");
        }

        static void getAllCustomersWithSP()
        {
            YTUEFEntities context = new YTUEFEntities();
            var theRecordList = context.getAllCustomers();
            Console.WriteLine("İşlem Tamamlandı");
        }

        static void deleteCustomer(int customerID)
        {
            YTUEFEntities context = new YTUEFEntities();
            Customer theRecord = context.Customer.Find(customerID);
            if (theRecord != null)
            {
                context.Customer.Remove(theRecord);
                context.SaveChanges();
            }
        }

        static void updateCustomer(int customerID, string FirstName, string LastName)
        {
            YTUEFEntities context = new YTUEFEntities();
            Customer theRecord = context.Customer.Find(customerID);
            if (theRecord != null)
            {
                theRecord.FirstName = FirstName;
                theRecord.LastName = LastName;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// EFDBFirst ile yeni bir kayıt ekleme işlemi.
        /// </summary>
        /// <param name="data"></param>
        static void addNewCustomer(Customer data)
        {
            YTUEFEntities context = new YTUEFEntities();
            context.Customer.Add(data);
            context.SaveChanges();
        }

        static void getAllProductsWhere1()
        {
            YTUEFEntities context = new YTUEFEntities();
            List<Product> theProductList = context.Products.Where(x => x.SafetyStockLevel > 200 && x.ListPrice > 90).ToList();
            foreach (var item in theProductList)
            {
                Console.WriteLine($"{item.Name} - {item.ProductNumber} - {item.ListPrice}");
            }
        }

        /// <summary>
        /// Product tablosunda bulunan tüm kayıtları getirdik.
        /// </summary>
        static void getAllProducts()
        {
            YTUEFEntities context = new YTUEFEntities();
            List<Product> productList = context.Products.ToList();
            foreach (var item in productList)
            {
                Console.WriteLine($"{item.Name} - {item.ProductNumber} - {item.ListPrice}");
            }
        }


        /// <summary>
        /// Customer tablosundaki kayıtları CustomerID değeri 190 dan büyük olacak şekilde getirelim.
        /// </summary>
        static void getAllCustomerWhere1()
        {
            YTUEFEntities context = new YTUEFEntities();
            var theCustomerList = context.Customer.Where(i => i.CustomerID > 190).ToList();
            Console.WriteLine(theCustomerList.Count);
            Console.ReadLine();
        }

        /// <summary>
        /// Customer tablosundaki kayıtların hepsini getirelim.
        /// </summary>
        static void getAllCustomers()
        {

            YTUEFEntities context = new YTUEFEntities();
            var theCustomerList = context.Customer.ToList();
            Console.WriteLine(theCustomerList.Count);
            Console.ReadLine();

        }
    }
}
