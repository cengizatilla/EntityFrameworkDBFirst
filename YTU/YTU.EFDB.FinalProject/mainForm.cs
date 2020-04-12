using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTU.EFDB.FinalProject
{
    public partial class mainForm : Form
    {
        systemUser theSystemUser;
        public mainForm(systemUser currentUserData)
        {
            InitializeComponent();
            theSystemUser = currentUserData;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            loadSystemUserData();
        }

        void loadSystemUserData()
        {
            Services.CustomerCompanyService executeService = new Services.CustomerCompanyService();
            List<CustomerCompany> theRecordList = executeService.getAllDataWithSystemUserId(theSystemUser.ID);
            lstCompanyList.DataSource = theRecordList;
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            Services.CustomerCompanyService executeService = new Services.CustomerCompanyService();
            executeService.addNewItem(new CustomerCompany()
            {
                SystemUserID = theSystemUser.ID,
                Title = txtTitle.Text,
                Descriptions = txtDesc.Text,
                TelephoneNumberI = txtTel1.Text,
                TelephoneNumberII = txtTel2.Text,
                TelephoneNumberIII = txtTel3.Text,
                EmailAddress = txtEmail.Text,
                CreateDate = DateTime.Now
            });

            loadSystemUserData();
        }
    }
}
