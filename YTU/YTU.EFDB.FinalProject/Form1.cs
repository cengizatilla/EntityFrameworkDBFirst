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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Services.SystemUserService executeService = new Services.SystemUserService();
            systemUser theRecord = executeService.checkSystemUserInformation(txtUserName.Text, txtPassword.Text);

            if (theRecord == null)
            {
                MessageBox.Show("Girmiş olduğunu değerler hatalı", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            mainForm mainFormApp = new mainForm(theRecord); // yapıcı metot kullanılarak değer aktarımı gerçekleştireceğiz. 
            mainFormApp.Show();
            this.Hide();

        }
    }
}
