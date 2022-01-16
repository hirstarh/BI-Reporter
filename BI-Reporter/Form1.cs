using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

        /* Method to remove record from the database */
        private void clkDataDelete_Click(object sender, EventArgs e)
        {
            ID = txtID.Text;
            Name = txtName.Text;
            string message = $"WARNING! - You are Deleting the record for {Name}, do you want to proceed";
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

            }
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
            this.clkDataDelete_Click(this, EventArgs.Empty);
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

        
    }
}
