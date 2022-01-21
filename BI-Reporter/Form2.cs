using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BI_Reporter
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private string Encrypt(string clearText)
        {

            string EncryptionKey = "MAKV4ffPBNI49209";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

            using (Aes encryptor = Aes.Create())
            {
                /* Create the encryption key */
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                    0x49, 0x79, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                /*Write the ClearText to the stream and encrypt */
                using (MemoryStream ms = new MemoryStream())
                {
                        using(CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }

            }
            return clearText;
        }

            

        private void btnRegister_Click(object sender, EventArgs e)
        {

            string password = txtNewPassword.Text;
            

            String connectionString;
            String sqlInsert;

            connectionString = @"Data Source=agem-se1.agem-bisenhs.org.uk;Database=SANDBOX_BISE;User=AHirst;Password=Coniston125";

            SqlConnection cnn = new SqlConnection(connectionString);
            sqlInsert = "Insert into SANDBOX_BISE.[dbo].[UserAccounts] values(@UserName, @password)";

            SqlCommand command = new SqlCommand(sqlInsert, cnn);

            command.Parameters.AddWithValue("@UserName", txtNewUser.Text);
            command.Parameters.AddWithValue("@password", Encrypt(txtNewPassword.Text.Trim()));
            cnn.Open();
            command.ExecuteNonQuery();

            command.Dispose();
            cnn.Close();
            

            if (this.txtNewUser.Text == "")
            {

                var result = MessageBox.Show("No user has been registered, do you want to return ?", "User Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    this.Close();
                }

                return;
            }
            else
            MessageBox.Show("New user registered", "User Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancelReg_Click(object sender, EventArgs e)
        {
            /* https://www.aspsnippets.com/Articles/Encrypt-data-and-store-in-Database-in-ASPNet-using-C-and-VBNet.aspx */

            this.Close();        
            
             
        }
    }
}
