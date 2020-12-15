using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Polling_Project
{
    public partial class ModifyVote : System.Web.UI.Page
    {
        static string host_id;
        static string modify_topic;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get the user id for check state of sign in
            HttpCookie myCookie = new HttpCookie("cook");
            myCookie = Request.Cookies["cook"];            
            try
            {
                host_id = myCookie.Values["UserID"].Replace(" ", string.Empty);
            }
            catch (Exception exep)
            {
                //Response.Write(exep.ToString());
                exep.ToString();
                Literal1.Text = "尚未登入，無法修改投票";                
                return;
            }

            _Default.MySQLconnect();
            SqlConnection Sqlconnect = _Default.GetSqlConnection();
            string SQLquery = @"SELECT * FROM Online_Polling_DB.dbo.[vote] WHERE host_id = '"+ host_id + "' ORDER BY ID ASC;";
            SqlCommand MyCommand = new SqlCommand(SQLquery, Sqlconnect);
            SqlDataReader MysqlDataReader = MyCommand.ExecuteReader();
            MysqlDataReader.Read();

            string topic_IN;            
            //read all
            do
            {
                if (MysqlDataReader["topic"].ToString() != "")
                {

                    topic_IN = MysqlDataReader["topic"].ToString().Replace(" ", string.Empty);



                    ListItem topic = new ListItem(topic_IN);
                    if (!VoteList.Items.Contains(topic))
                    {
                        VoteList.Items.Add(topic_IN);
                    }
                }
            } while (MysqlDataReader.Read());
            MysqlDataReader.Close();

        }
        protected void BulletedList1_Click(object sender, System.Web.UI.WebControls.BulletedListEventArgs e)
        {
            //Response.Write("You selected: " + e.Index + ": " + VoteList.Items[e.Index].Text);
            modify_topic = VoteList.Items[e.Index].Text;
            //Response.Write("modify topic:" + modify_topic);
            _Default.MySQLconnect();
            SqlConnection Sqlconnect = _Default.GetSqlConnection();
            string SQLquery = @"SELECT * 
                               FROM Online_Polling_DB.dbo.[vote]
                                WHERE topic = '" + modify_topic
                                + "' ORDER BY ID ASC;";
            SqlCommand MyCommand = new SqlCommand(SQLquery, Sqlconnect);
            SqlDataReader MysqlDataReader = MyCommand.ExecuteReader();
            MysqlDataReader.Read();
            try
            {
                if (MysqlDataReader["id"].ToString() != null)
                {
                    Label1.Text = "投票主題:";
                    Label1.Text += MysqlDataReader["topic"].ToString().Replace(" ", string.Empty);
                    Label1.Text += "<br />";
                    Label1.Text += "主辦人:";
                    Label1.Text += MysqlDataReader["host_id"].ToString().Replace(" ", string.Empty);
                    Label1.Text += "<br />";
                    Label1.Text += "投票敘述:";
                    Label1.Text += MysqlDataReader["description"].ToString().Replace(" ", string.Empty);
                    Label1.Text += "<br />";
                    Label1.Text += "投票開始日期:";
                    Label1.Text += MysqlDataReader["startdate"].ToString().Replace(" ", string.Empty);
                    Label1.Text += "<br />";
                    Label1.Text += "投票結束日期:";
                    Label1.Text += MysqlDataReader["enddate"].ToString().Replace(" ", string.Empty);
                    Label1.Text += "<br />";
                    Label1.Visible = true;
                    StartModifyButton.Visible = true;
                }
            }
            catch(Exception exep)
            {
                Response.Write(exep.ToString());

                return;
            }
            
        }
        public void StartModifyButtonClick(Object sender, EventArgs e)
        {
            // When the button is clicked,
            // change the button text, and disable it.
            System.Web.UI.WebControls.Button clickedButton = (System.Web.UI.WebControls.Button)sender;
            clickedButton.Text = "開始修改";
            
            Response.Redirect("ModifyingVote.aspx?ModifyTopic=" + modify_topic);
            

        }
    }
}