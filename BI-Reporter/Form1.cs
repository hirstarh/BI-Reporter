using System;
using System.IO;
using System.Collections;
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

    public partial class Form1 : Form
    {
        String ID;
        string Name;
        string AddressLine1;
        string AddressLine2;
        string TownCity;
        string County;
        string PostCode;
        string Age;
        string TelNo;




        public Form1()
        {
            InitializeComponent();
            txtID.ReadOnly = true;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bI_Reporter_DataSet.AddressDetails' table. You can move, or remove it, as needed.
            this.addressDetailsTableAdapter.Fill(this.bI_Reporter_DataSet.AddressDetails);

        }

        private string Decrypt(string cipherText)
        {
            PaddingMode Padding = PaddingMode.None;

            string EncryptionKey = "MAKV4ffPBNI49209";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            /*Create key for decryption */
            using (Aes encryptor = Aes.Create())
            {
                encryptor.Padding = Padding;
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

               /* Decrypt and write the text to memory */
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {

                        
                            
                         cs.Write(cipherBytes, 0, cipherBytes.Length); 
                         cs.Close(); 
                    }
                    /* Assign the decrypted password from memory to a string */
                     cipherText = Encoding.Unicode.GetString(ms.ToArray()); 
                    
                    
                    
                }
            }
            
            return cipherText;
        }

        private void DeleteRecord()
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[index];
            ID = row.Cells[0].Value.ToString();
            Name = txtName.Text;
            string message = $"WARNING! - You are Deleting the record for {ID}, do you want to proceed";
            String caption = "Data to be deleted";
            string connectionString = @"Data Source=agem-se1.agem-bisenhs.org.uk;Database=SANDBOX_BISE; User=AHirst;Password=Coniston125";
            string deleteSqlRow = "DELETE FROM SANDBOX_BISE.[dbo].[AddressDetails] WHERE ID = @ID";


            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SqlConnection cnn = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(deleteSqlRow, cnn);
                cnn.Open();
                command.Parameters.AddWithValue("@ID", ID);

                command.ExecuteNonQuery();
                dataGridView1.Update();
                dataGridView1.Refresh();


                MessageBox.Show($"Entry ID record {ID} deleted from the database");

                command.Dispose();
                cnn.Close();
                this.Form1_Load(this, EventArgs.Empty);
                dataGridView1.DataSource = addressDetailsBindingSource;
            }
        }
        /* Method to remove record from the database if Delete button clicked */
        private void clkDataDelete_Click(object sender, EventArgs e)
        {

            DeleteRecord();
            

            }
        

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            string Message = "Do you want to close the application down";
            string caption = "App closure";
            var result = MessageBox.Show(Message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
            
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DeleteRecord();
        }

        /* Method to insert a new record into the database */
        private void btnInsert_Click(object sender, EventArgs e)
        {
            string caption = "Data entered confirmation";
            ID = txtID.Text;
            Name = txtName.Text;
            AddressLine1 = txtAddressLine1.Text;
            AddressLine2 = txtAddressLine2.Text;
            TownCity = txtTownCity.Text;
            County = txtCounty.Text;
            PostCode = txtPostCode.Text;
            Age = txtAge.Text;
            TelNo = txtTelNo.Text;

            string connectionString = @"Data Source=agem-se1.agem-bisenhs.org.uk;Database=SANDBOX_BISE;User=AHirst;Password=Coniston125";
            var sql_Insert = "Insert into SANDBOX_BISE.[dbo].[AddressDetails] (Name, AddressLine1, AddressLine2, TownCity, County, PostCode, Age, TelNo)";
            sql_Insert += "values(@Name, @AddressLine1, @AddressLine2, @TownCity, @County, @PostCode,  @Age, @TelNo)";

            SqlConnection cnn = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(sql_Insert, cnn);
            command.CommandText = sql_Insert;
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@AddressLine1", AddressLine1);
            command.Parameters.AddWithValue("@AddressLine2", AddressLine2);
            command.Parameters.AddWithValue("@TownCity", TownCity);
            command.Parameters.AddWithValue("@County", County);
            command.Parameters.AddWithValue("@PostCode", PostCode);
            command.Parameters.AddWithValue("@Age", Age);
            command.Parameters.AddWithValue("@TelNo", TelNo);
            cnn.Open();
            command.ExecuteNonQuery();

            command.Dispose();
            cnn.Close();
            this.Form1_Load(this, EventArgs.Empty);


            MessageBox.Show($"Data for {Name} Entered", caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /* Method to amend current selected record */
        private void btnAmend_Click(object sender, EventArgs e)
        {
            string caption = "Record entry update";
            ID = txtID.Text;
            string message = $"Data updated for record {ID}";
            Name = txtName.Text;
            AddressLine1 = txtAddressLine1.Text;
            AddressLine2 = txtAddressLine2.Text;
            TownCity = txtTownCity.Text;
            County = txtCounty.Text;
            PostCode = txtPostCode.Text;
            Age = txtAge.Text;
            TelNo = txtTelNo.Text;

            string connectorString = @"Data Source=agem-se1.agem-bisenhs.org.uk;Database=Sandbox_BISE;User=AHirst;Password=Coniston125";
            string sqlUpdate = "UPDATE Sandbox_BISE.[dbo].[AddressDetails] SET Name=@Name, AddressLine1=@AddressLine1, AddressLine2=@AddressLine2, TownCity=@TownCity, County=@County, PostCode=@PostCode, Age=@Age, TelNo=@TelNo WHERE ID = @ID";

            SqlConnection cnn = new SqlConnection(connectorString);
            SqlCommand command = new SqlCommand(sqlUpdate, cnn);

            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@AddressLine1", AddressLine1);
            command.Parameters.AddWithValue("@AddressLine2", AddressLine2);
            command.Parameters.AddWithValue("@TownCity", TownCity);
            command.Parameters.AddWithValue("@County", County);
            command.Parameters.AddWithValue("@PostCode", PostCode);
            command.Parameters.AddWithValue("@Age", Age);
            command.Parameters.AddWithValue("@TelNo", TelNo);
            cnn.Open();
            command.ExecuteNonQuery();
            dataGridView1.Refresh();

            command.Dispose();
            cnn.Close();

            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /* Search method routine */
              
        private void filter_OnSearch(int flag)
        {
            string connectionString = @"Data Source=agem-se1.agem-bisenhs.org.uk;Database=Sandbox_BISE;User=AHirst;Password=Coniston125";
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();

            switch (flag)
            {
                case 1:
                    string sqlCommand = "SELECT * FROM [SANDBOX_BISE].[DBO].[AddressDetails] WHERE (Name LIKE @Search)";
                    SqlCommand command = new SqlCommand(sqlCommand, cnn);
                    command.Parameters.AddWithValue("@Search", "%" + txtSearchName.Text + "%");
                    SqlDataAdapter socket1 = new SqlDataAdapter(command);
                    DataTable dt = new DataTable(); 
                    socket1.Fill(dt);
                    dataGridView1.DataSource = dt;
                    break;

                case 2:
                    string sqlCommand2 = "SELECT * FROM [SANDBOX_BISE].[DBO].[AddressDetails] WHERE (AddressLine1 LIKE @Search)";
                    SqlCommand command2 = new SqlCommand(sqlCommand2, cnn);
                    command2.Parameters.AddWithValue("@Search", "%" + txtSearchAdd1.Text + "%");
                    SqlDataAdapter socket2 = new SqlDataAdapter(command2);
                    DataTable dt2 = new DataTable();
                    socket2.Fill(dt2);
                    dataGridView1.DataSource = dt2;

                    break;
                case 3:
                    string sqlCommand3 = "SELECT * FROM [SANDBOX_BISE].[DBO].[AddressDetails] WHERE (AddressLine2 LIKE @Search)";
                    SqlCommand command3 = new SqlCommand(sqlCommand3, cnn);
                    command3.Parameters.AddWithValue("@Search", "%" + txtSearchAdd2.Text + "%");
                    SqlDataAdapter socket3 = new SqlDataAdapter(command3);
                    DataTable dt3 = new DataTable();
                    socket3.Fill(dt3);
                    dataGridView1.DataSource = dt3;
                    break;

                case 4:
                    string sqlCommand4 = "SELECT * FROM [SANDBOX_BISE].[DBO].[AddressDetails] WHERE (TownCity LIKE @Search)";
                    SqlCommand command4 = new SqlCommand(sqlCommand4, cnn);
                    command4.Parameters.AddWithValue("@Search", "%" + txtSearchTownC.Text + "%");
                    SqlDataAdapter socket4 = new SqlDataAdapter(command4);
                    DataTable dt4 = new DataTable();
                    socket4.Fill(dt4);
                    dataGridView1.DataSource = dt4;
                    break;

                case 5:
                    string sqlCommand5 = "SELECT * FROM [SANDBOX_BISE].[DBO].[AddressDetails] WHERE (County LIKE @Search)";
                    SqlCommand command5 = new SqlCommand(sqlCommand5, cnn);
                    command5.Parameters.AddWithValue("@Search", "%" + txtSearchCounty.Text + "%");
                    SqlDataAdapter socket5 = new SqlDataAdapter(command5);
                    DataTable dt5 = new DataTable();
                    socket5.Fill(dt5);
                    dataGridView1.DataSource = dt5;

                    break;

                case 6:
                    string sqlCommand6 = "SELECT * FROM [SANDBOX_BISE].[DBO].[AddressDetails] WHERE (Age LIKE @Search)";
                    SqlCommand command6 = new SqlCommand(sqlCommand6, cnn);
                    command6.Parameters.AddWithValue("@Search", "%" + txtSearchAge.Text + "%");
                    SqlDataAdapter socket6 = new SqlDataAdapter(command6);
                    DataTable dt6 = new DataTable();
                    socket6.Fill(dt6);
                    dataGridView1.DataSource = dt6;
                    break;

                case 7:
                    string sqlCommand7 = "SELECT * FROM [SANDBOX_BISE].[DBO].[AddressDetails] WHERE (TelNo LIKE @Search)";
                    SqlCommand command7 = new SqlCommand(sqlCommand7, cnn);
                    command7.Parameters.AddWithValue("@Search", "%" + txtSearchTelNo.Text + "%");
                    SqlDataAdapter socket7 = new SqlDataAdapter(command7);
                    DataTable dt7 = new DataTable();
                    socket7.Fill(dt7);
                    dataGridView1.DataSource = dt7;
                    break;

                case 8:
                    string sqlCommand8 = "SELECT * FROM [SANDBOX_BISE].[DBO].[AddressDetails] WHERE (PostCode LIKE @Search)";
                    SqlCommand command8 = new SqlCommand(sqlCommand8, cnn);
                    command8.Parameters.AddWithValue("@Search", "%" + txtSearchPostCode.Text + "%");
                    SqlDataAdapter socket8 = new SqlDataAdapter(command8);
                    DataTable dt8 = new DataTable();
                    socket8.Fill(dt8);
                    dataGridView1.DataSource = dt8;
                    break;

            }

        }
        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            this.filter_OnSearch(1);

        }

        private void txtSearchAdd1_TextChanged(object sender, EventArgs e)
        {
            this.filter_OnSearch(2);
        }

        private void txtSearchAdd2_TextChanged(object sender, EventArgs e)
        {
            this.filter_OnSearch(3);
        }

        private void txtSearchTownC_TextChanged(object sender, EventArgs e)
        {
            this.filter_OnSearch(4);
        }

        private void txtSearchCounty_TextChanged(object sender, EventArgs e)
        {
            this.filter_OnSearch(5);
        }

        private void txtSearchAge_TextChanged(object sender, EventArgs e)
        {
            this.filter_OnSearch(6);
        }
        private void txtSearchTelNo_TextChanged(object sender, EventArgs e)
        {
            this.filter_OnSearch(7);
        }
        private void txtSearchPostCode_TextChanged(object sender, EventArgs e)
        {
            this.filter_OnSearch(8);
        }

        
        private void btnNewUser_Click(object sender, EventArgs e)
        {
            Form2 init = new Form2();
            init.Show();
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String message = $"Regretfully your user a/c {txtUserName.Text} is not recognised, do you need to register a new a/c ?";
            String caption = "User account security";

            if (txtUserName.Text == "")
            {
                MessageBox.Show("Please enter both your user name and password, thanks", caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            String connectionString;
            String sqlSearch;
            String userName;
            String password;
            String clearPassword;
            Form2 cePassword = new Form2();

            try
            {
                connectionString = @"Data Source=agem-se1.agem-bisenhs.org.uk;Database=SANDBOX_BISE;User=AHirst;Password=Coniston125";
                sqlSearch = @"SELECT UserName, Password FROM [SANDBOX_BISE].[dbo].[UserAccounts] where UserName = @UserName";

                SqlConnection cnn = new SqlConnection(connectionString);

                SqlCommand command = new SqlCommand(sqlSearch, cnn);

                command.Parameters.AddWithValue("@UserName", txtUserName.Text);
                cnn.Open();
                command.ExecuteNonQuery();

                /* Assigning a string to the result of the above SELECT SQL query */

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                userName = reader.GetString(0);
                password = reader.GetString(1);
                clearPassword = Decrypt(password);
                string enPassword = txtPassword.Text;
                

            reader.Close();
            command.Dispose();
                cnn.Close();

                if (clearPassword == txtPassword.Text)
                {
                    MessageBox.Show($"User {userName} and password Matched", "Password authentication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                }
                else
                {
                    MessageBox.Show($"Sorry, User {userName} or password have not matched", "Password authentication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }

            catch(InvalidOperationException ex)
            {
                string error = $"The User {txtUserName.Text} was not recognised in the database, do you need to register a new User A/C";

                    MessageBox.Show(error,"User A/C error",MessageBoxButtons.OK,MessageBoxIcon.Question);
                    txtUserName.Text = "";
                     txtPassword.Text = "";
                return;

            }
            
        }

    }
}
