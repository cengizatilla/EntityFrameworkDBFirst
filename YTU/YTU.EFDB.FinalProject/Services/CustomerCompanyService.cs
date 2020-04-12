using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTU.EFDB.FinalProject.Services
{
    public class CustomerCompanyService
    {
        YTUProjectEntities context;
        public CustomerCompanyService()
        {
            context = new YTUProjectEntities();
        }

        public void addNewItem(CustomerCompany dataItem)
        {
            if(dataItem != null)
            {
                context.CustomerCompanies.Add(dataItem);
                context.SaveChanges();
            }
        }

        public void updateItem(CustomerCompany dataItem)
        {
            if(dataItem != null)
            {
                CustomerCompany theDbRecord = context.CustomerCompanies.Find(dataItem.ID);
                if(theDbRecord != null)
                {
                    theDbRecord.Title = dataItem.Title;
                    theDbRecord.Descriptions = dataItem.Descriptions;
                    theDbRecord.TelephoneNumberI = dataItem.TelephoneNumberI;
                    theDbRecord.TelephoneNumberII = dataItem.TelephoneNumberII;
                    theDbRecord.TelephoneNumberIII = dataItem.TelephoneNumberIII;
                    theDbRecord.EmailAddress = dataItem.EmailAddress;
                    context.SaveChanges();
                }
            }
        }

        public void removeItem(int ID)
        {
            if (ID != 0)
            {
                CustomerCompany theDbRecord = context.CustomerCompanies.Find(ID);
                if (theDbRecord != null)
                {
                    context.CustomerCompanies.Remove(theDbRecord);
                }
            }
        }

        public List<CustomerCompany> getAllDataWithSystemUserId(int systemUserID)
        {
            return context.CustomerCompanies.Where(i => i.SystemUserID == systemUserID).ToList();
        }

        public CustomerCompany getDataById(int customerDataID)
        {
            return context.CustomerCompanies.Find(customerDataID);
        }
    }
}
