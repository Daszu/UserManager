using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using Secure_Library;

namespace User_Manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            byte[] encodedPassword = new UTF8Encoding().GetBytes(textBox2.Text);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            bool status;

            SecureLib initSecure = new SecureLib();

            try
            {
                if (status = initSecure.login(textBox1.Text, encoded, out Global.uctx))
                {
                    Thread.CurrentPrincipal = Secure_Library.SecureLib.gp;
                    this.Hide();
                    var mainForm = new Form2();
                    mainForm.Closed += (s, args) => this.Close();
                    mainForm.Show();
                }
                else
                    MessageBox.Show("błędne dane logowania!");
                
            }
            catch (SqlException sqle)
            {
                throw sqle;
            }
        }
    }
}
