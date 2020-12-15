using System;   
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;//for get connect string 
using System.Data.SqlClient;//for sql connection
using System.Windows.Forms;

namespace Online_Polling_Project
{
    public partial class _Default : Page
    {
        static string ConnectString;
        static SqlConnection MySqlConnect;
        string SqlQuery;
        SqlCommand MyCommand;
        SqlDataReader MysqlDataReader;
        protected void Page_Load(object sender, EventArgs e)
        {
            InputBoxControl();//Use to control text box 

        }
        public void LoginButtonClick(Object sender, EventArgs e)
        {
            // When the button is clicked,
            // change the button text, and disable it.
            System.Web.UI.WebControls.Button clickedButton = (System.Web.UI.WebControls.Button)sender;            
            clickedButton.Text = "...loging...";
            //clickedButton.Enabled = false;

            string id = ID_inputBox.Text;            
            string password = Password_inputBox.Text;
            string SQLid="";
            string SQLpassword="";
            /*
            TextBox1.Text += "click login button!";
            TextBox1.Text += Environment.NewLine;*/
            if (id != ""  && password != "")
            {
                /*TextBox1.Text += id;
                TextBox1.Text += " ";
                TextBox1.Text += password;
                TextBox1.Text += Environment.NewLine;
                */
                try
                {
                    MySQLconnect();
                    SqlQuery = @"SELECT * FROM Online_Polling_DB.dbo.[user];";
                    MyCommand = new SqlCommand(SqlQuery, MySqlConnect);
                    MysqlDataReader = MyCommand.ExecuteReader();
                    MysqlDataReader.Read();                  

                    //read all
                    do
                    {
                        if (MysqlDataReader["id"].ToString() != "")
                        {

                            SQLid = MysqlDataReader["id"].ToString().Replace(" ", string.Empty);
                            SQLpassword = MysqlDataReader["password"].ToString().Replace(" ", string.Empty);
                            /*
                            TextBox1.Text += "sql:'";
                            TextBox1.Text += SQLid;
                            TextBox1.Text += "' ";
                            TextBox1.Text += SQLpassword;
                            TextBox1.Text += Environment.NewLine;*/
                            if(id.Equals(SQLid) && password.Equals(SQLpassword))
                            {
                                LoginButtonLabel.Text = "Login successful!";
                                LoginButtonLabel.Visible = true;                                
                                break;
                            }
                            
                        }
                    } while (MysqlDataReader.Read());
                    MysqlDataReader.Close();

                }
                catch (Exception exception)
                {
                    //error message
                    MessageBox.Show(exception.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    clickedButton.Text = "login";
                    LoginButtonLabel.Visible = true;
                    if (LoginButtonLabel.Text == "Login successful!")
                    {
                        HttpCookie myCookie = new HttpCookie("cook");//store the state of sign in
                        myCookie.Values.Add("UserID", id);
                        myCookie.Expires = DateTime.Now.AddMinutes(20);//overtime will clear
                        Response.Cookies.Add(myCookie);

                        Response.Redirect("vote.aspx");
                    }
                }
                // Display the label text.
                //LoginButtonLabel.Text = "Register successful!";
                //LoginButtonLabel.Visible = true;

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
        public static void MySQLconnect()
        {
            ConnectString = Getconnectionstring("mySqlConnectionName");
            MySqlConnect = new SqlConnection(ConnectString);
            MySqlConnect.Open();
        }
        public static SqlConnection GetSqlConnection()
        {
            return MySqlConnect;
        }
        public void SelectAll()
        {
            ////////sql read ///////
            MySQLconnect();
            SqlQuery = @"SELECT * FROM Online_Polling_DB.dbo.[user];";
            MyCommand = new SqlCommand(SqlQuery, MySqlConnect);
            MysqlDataReader = MyCommand.ExecuteReader();
            MysqlDataReader.Read();

            System.Web.UI.WebControls.TextBox MyTextBox = new System.Web.UI.WebControls.TextBox
            {
                ID = "TextBox1",
                Text = ""
            };

            //read all
            do
            {
                if (MysqlDataReader["id"].ToString() != "")
                {
                    TextBox1.Text += MysqlDataReader["id"].ToString();
                    TextBox1.Text += " : ";
                    TextBox1.Text += MysqlDataReader["name"].ToString().Replace(" ", string.Empty);
                    TextBox1.Text += " : ";
                    TextBox1.Text += MysqlDataReader["password"].ToString().Replace(" ", string.Empty);
                    TextBox1.Text += " : ";
                    TextBox1.Text += MysqlDataReader["category"].ToString();
                    TextBox1.Text += Environment.NewLine;                 //newline
                }
            } while (MysqlDataReader.Read());
            MysqlDataReader.Close();
        }
        public void InputBoxControl()
        {
            //if inputing, clear text
            //else, will recover
            this.ID_inputBox.Attributes.Add("onFocus", "if (this.value=='input ID'){this.value='';}");
            this.Password_inputBox.Attributes.Add("onFocus", "if (this.value=='input password'){this.value='';}");
            this.ID_inputBox.Attributes.Add("onblur", "if (this.value==''){this.value='input ID';}");
            this.Password_inputBox.Attributes.Add("onblur", "if (this.value==''){this.value='input password';}");
        }
     
    }
}