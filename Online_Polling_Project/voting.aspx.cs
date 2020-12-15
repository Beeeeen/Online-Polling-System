using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Polling_Project
{
    public partial class voting : System.Web.UI.Page
    {
        static List<string> a = new List<string>();//for single choose
        SqlConnection Sqlconnect;
        string topic_click;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get the user id for check state of sign in
            HttpCookie myCookie = new HttpCookie("cook");
            myCookie = Request.Cookies["cook"];
            string userID = null;
            try
            {
                userID = myCookie.Values["UserID"].Replace(" ", string.Empty);
            }
            catch (Exception exep)
            {
                //Response.Write(exep.ToString());
                //Label1.Text = "尚未登入，無法進行投票";
                //Label1.Visible = true;
                exep.ToString();

                //return;
            }

            topic_click = Request.QueryString["topic"];
            if (topic_click == null)
            {
                return;
            }            

            _Default.MySQLconnect();
            Sqlconnect = _Default.GetSqlConnection();
            string SQLquery = @"SELECT * FROM Online_Polling_DB.dbo.[vote] WHERE topic = '" + topic_click + "' ;";
            SqlCommand MyCommand = new SqlCommand(SQLquery, Sqlconnect);
            SqlDataReader MysqlDataReader = MyCommand.ExecuteReader();
            MysqlDataReader.Read();

            string option_IN;
            
            string topic = MysqlDataReader["topic"].ToString().Replace(" ", string.Empty);
            string description_IN = MysqlDataReader["description"].ToString().Replace(" ", string.Empty);
            Label1.Text = "主題 : "+topic + "<br />";
            Label1.Text += "敘述 : " + description_IN + "<br />";

            //read all
            do
            {
                if (MysqlDataReader["option"].ToString() != "" )
                {
                    
                    option_IN = MysqlDataReader["option"].ToString().Replace(" ", string.Empty);
                    ListItem temp = new ListItem(option_IN);
                    if(topic_click.Equals(topic))
                    {
                        if(!CheckBoxList1.Items.Contains(temp))
                        {
                            CheckBoxList1.Items.Add(option_IN);
                        }                        
                    }                                        
                }
            } while (MysqlDataReader.Read());
            MysqlDataReader.Close();
        }
        //single choose
        protected void CheckBoxIndexChanged(object sender, EventArgs e)
        {          
            Literal1.Text = "You select Item : ";                     
            if(a.Count()==0)
            {                
                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    if (CheckBoxList1.Items[i].Selected)
                    {
                        Literal1.Text += CheckBoxList1.Items[i].Text + "<br />";
                        a.Add(CheckBoxList1.Items[i].Text);
                    }
                }                
            }
            else
            {
                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    if (CheckBoxList1.Items[i].Selected)
                    {
                        if (a.Contains(CheckBoxList1.Items[i].Text))
                        {
                            a.Remove(CheckBoxList1.Items[i].Text);
                            CheckBoxList1.Items[i].Selected = false;
                        }
                        else
                        {
                            a.Add(CheckBoxList1.Items[i].Text);
                            Literal1.Text += CheckBoxList1.Items[i].Text + "<br />";
                        }

                    }
                }
            }
            Button1.Visible = true;
            
        }
        protected void VoteButtonClick(object sender, EventArgs e)
        {
            //result id auto increment                       
            string SQLinsert = @"INSERT INTO Online_Polling_DB.dbo.[result]
                            ([topic],[option],[voter_id])
                            VALUES(@value1, @value2, @value3)";

            SqlCommand MyCommand = new SqlCommand(SQLinsert, Sqlconnect);            
            HttpCookie myCookie = Request.Cookies["cook"];

            string choose = CheckBoxList1.SelectedValue;
            string userID="";
            try
            {
                if (myCookie.Values["UserID"] != null)
                {
                    userID = myCookie.Values["UserID"];
                }
                else
                {
                    userID = null;
                }
            }
            catch(Exception exep)
            {
                exep.ToString();
            }
            
            
            MyCommand.Parameters.AddWithValue("@value1", topic_click);
            MyCommand.Parameters.AddWithValue("@value2", choose);
            MyCommand.Parameters.AddWithValue("@value3", userID);
            
            //執行SQL INSERT 語法
            MyCommand.ExecuteNonQuery();
            Button1.Text = "投票完成";
            Button1.Enabled = false;
        }
    }
}