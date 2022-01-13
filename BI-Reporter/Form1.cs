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

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
                      
            
        }

       
        private void toolStripButton1_Click(object sender, EventArgs e)
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
            var sql_Insert = "Insert into SANDBOX_BISE.[dbo].[AddressDetails] (ID,Name, AddressLine1, AddressLine2, TownCity, County, PostCode, Age, TelNo)";
            sql_Insert += "values(@ID, @Name, @AddressLine1, @AddressLine2, @TownCity, @County, @PostCode,  @Age, @TelNo)";
            
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
            dataGridView1.Refresh();

            command.Dispose();
            cnn.Close();


            MessageBox.Show($"Data for {Name} Entered",caption,MessageBoxButtons.OK,MessageBoxIcon.Information);
            

            
        }

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

        private void txtCloseApp_Click(object sender, EventArgs e)
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
        /* button1_click */
        private void button1_click(object sender, EventArgs e)
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


            MessageBox.Show($"Data for {Name} Entered", caption, MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        

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
            dataGridView1.Refresh();

            command.Dispose();
            cnn.Close();


            MessageBox.Show($"Data for {Name} Entered", caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAmend_Click_1(object sender, EventArgs e)
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

        private void btnSearch_Click(object sender, EventArgs e)
        {

            ArrayList searchList = new ArrayList();
                searchList.Add(txtSearchName.Text);
                searchList.Add(txtSearchAdd1.Text);
                searchList.Add(txtSearchAdd2.Text);
                searchList.Add(txtSearchTownC.Text);
                searchList.Add(txtSearchCounty.Text);
                searchList.Add(txtSearchAge.Text);
                searchList.Add(txtSearchTelNo.Text);

            
            string searchValue = txtSearchName.Text;
                string message = $"Unable to find {searchValue} in the database";
            string caption = "Record not found!";
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                try
                {
                    bool valueResult = false;

                foreach (var NameSearchItem in searchList)
                {
                    
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            
                            if (row.Cells[i].Value != null && row.Cells[i].Value.ToString().Equals(NameSearchItem))
                            {
                                dataGridView1.ClearSelection();
                                int rowIndex = row.Index;
                                dataGridView1.Rows[rowIndex].Selected = true;
                                valueResult = true;
                                break;
                            }
                            /*   else if ((string)searchList[0] == "")
                           {
                               MessageBox.Show("No data entered to search", "Data Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           } */
                        }

                    }
                }
                    if (!valueResult)
                    {
                        MessageBox.Show(message,caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            
        }
    }
}
