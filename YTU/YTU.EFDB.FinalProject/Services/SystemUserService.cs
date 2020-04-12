using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTU.EFDB.FinalProject.Services
{
    public class SystemUserService
    {
        YTUProjectEntities context;
        public SystemUserService()
        {
            context = new YTUProjectEntities();
        }

        public void addNewItem(systemUser data)
        {
            if (data != null)
            {
                context.systemUsers.Add(data);
                context.SaveChanges();
            }
        }

        public void updateItem(systemUser data)
        {
            if (data != null)
            {
                systemUser theDbRecord = context.systemUsers.Find(data.ID);
                if (theDbRecord != null)
                {
                    theDbRecord.Firstname = data.Firstname;
                    theDbRecord.Lastname = data.Lastname;
                    theDbRecord.Passwords = data.Passwords;
                    context.SaveChanges();
                }
            }
        }

        public void deleteItem(int systemUserId)
        {
            if (systemUserId != 0)
            {
                systemUser theDbRecord = context.systemUsers.Find(systemUserId);
                if (theDbRecord != null)
                {
                    context.systemUsers.Remove(theDbRecord);
                    context.SaveChanges();
                }
            }
        }

        public systemUser checkSystemUserInformation(string userName, string password)
        {
            systemUser theDatabaseUser = null;
            if(!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                int systemUserCheck = context.systemUsers.Where(i => i.Username == userName && i.Passwords == password).Count();
                if(systemUserCheck>0)
                {
                    theDatabaseUser = context.systemUsers.Where(i => i.Username == userName && i.Passwords == password).FirstOrDefault();
                }
            }
            return theDatabaseUser;
        }


    }
}
