using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
namespace Online_Polling_Project
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string ConnectString;
        SqlConnection MySqlConnect;               
        protected void Page_Load(object sender, EventArgs e)
        {
            ////////input box control
            //if inputing, clear text
            //else, will recover
            this.ID_inputBox.Attributes.Add("onFocus", "if (this.value=='input ID'){this.value='';}");
            this.Name_inputBox.Attributes.Add("onFocus", "if (this.value=='input name'){this.value='';}");
            this.Password_inputBox.Attributes.Add("onFocus", "if (this.value=='input password'){this.value='';}");
            this.authenticationBox.Attributes.Add("onFocus", "if (this.value=='Input Authentication Code'){this.value='';}");
            //this.Category_inputBox.Attributes.Add("onFocus", "if (this.value=='input category'){this.value='';}");
            this.ID_inputBox.Attributes.Add("onblur", "if (this.value==''){this.value='input ID';}");
            this.Name_inputBox.Attributes.Add("onblur", "if (this.value==''){this.value='input name';}");
            this.Password_inputBox.Attributes.Add("onblur", "if (this.value==''){this.value='input password';}");
            this.authenticationBox.Attributes.Add("onblur", "if (this.value==''){this.value='Input Authentication Code';}");
            //this.Category_inputBox.Attributes.Add("onblur", "if (this.value==''){this.value='input category';}");

            //SQL connection
            ConnectString = Getconnectionstring("mySqlConnectionName");
            MySqlConnect = new SqlConnection(ConnectString);
            MySqlConnect.Open();

        }
        public void Selection_Change(Object sender, EventArgs e)
        {
            if(CategoryDropDownList1.SelectedItem.Value != "student")
            {
                Response.Write("You choose " + CategoryDropDownList1.SelectedItem.Value + "<br />so you need input Authentication Code to register <br />If you don't have, please contact manager to get<br />");
                authenticationBox.Visible = true;
            }
            else
            {
                authenticationBox.Visible = false;
            }
        }
        public static string Getconnectionstring(string keyname)//get the connect string 
        {
            string connection = string.Empty;
            switch (keyname)
            {
                case "mySqlConnectionName":
                    connection = ConfigurationManager.ConnectionStrings["mySqlConnectionName"].ConnectionString;
                    break;
                default:
                    break;
            }
            return connection;
        }
        public void RegisterButtonClick(Object sender, EventArgs e)
        {
            if(CategoryDropDownList1.SelectedValue!="student" && authenticationBox.Text!="1234")
            {
                RegisterButtonLabel.Text = "Error Authentication Code";
                RegisterButtonLabel.Visible = true;
                return;
            }
            // When the button is clicked,
            // change the button text, and disable it.
            System.Web.UI.WebControls.Button clickedButton = (System.Web.UI.WebControls.Button)sender;
            clickedButton.Text = "Rigistered";
            

            string id = ID_inputBox.Text;
            string name = Name_inputBox.Text;
            string password = Password_inputBox.Text;
            //string category = Category_inputBox.Text;
            string category = CategoryDropDownList1.SelectedValue;
            if (id != "" && name != "" && password != "")
            {
                try
                {

                    string SQLinsert = @"INSERT INTO Online_Polling_DB.dbo.[user] 
                            ([id],[name],[password],[category],[build_permission]) 
                            VALUES(@value1, @value2, @value3,@value4,@value5)";

                    SqlCommand MyCommand = new SqlCommand(SQLinsert, MySqlConnect);

                    MyCommand.Parameters.AddWithValue("@value1", id);
                    MyCommand.Parameters.AddWithValue("@value2", name);
                    MyCommand.Parameters.AddWithValue("@value3", password);
                    MyCommand.Parameters.AddWithValue("@value4", category);
                    if(category== "manager")
                    {
                        MyCommand.Parameters.AddWithValue("@value5",1);
                    }
                    else
                    {
                        MyCommand.Parameters.AddWithValue("@value5", 0);
                    }
                    
                    //執行SQL INSERT 語法
                    MyCommand.ExecuteNonQuery();
                    clickedButton.Enabled = false;
                    //關閉資料庫
                    MySqlConnect.Close();
                }
                catch (Exception exception)
                {
                    //error message
                    MessageBox.Show(exception.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {

                }
                // Display the label text.
                RegisterButtonLabel.Text = "Register successful!";
                RegisterButtonLabel.Visible = true;

            }



        }
    }
}