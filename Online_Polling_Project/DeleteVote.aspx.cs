using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Polling_Project
{
    public partial class DeleteVote : System.Web.UI.Page
    {
        SqlConnection Sqlconnect;
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
                exep.ToString();
                Literal1.Text = "尚未登入，無法刪除投票";
                DeleteButton.Visible = false;
                Literal1.Visible = true;                
                return;
            }

            //print the user's vote activity
            _Default.MySQLconnect();
            Sqlconnect = _Default.GetSqlConnection();
            string SQLquery = @"SELECT * FROM Online_Polling_DB.dbo.[vote] WHERE host_id = '" + userID + "' ;";
            SqlCommand MyCommand = new SqlCommand(SQLquery, Sqlconnect);
            SqlDataReader MysqlDataReader = MyCommand.ExecuteReader();
            MysqlDataReader.Read();

            string topic_IN;
            //read all
            do
            {
                
                try
                {
                    topic_IN = MysqlDataReader["topic"].ToString().Replace(" ", string.Empty);


                    ListItem topic = new ListItem(topic_IN);
                    if (!VoteList.Items.Contains(topic))
                    {
                        VoteList.Items.Add(topic_IN);

                    }
                }
                catch(Exception exep)
                {
                    exep.ToString();
                    Literal1.Text = "You don't have vote activity";
                    DeleteButton.Visible = false;
                }

                    
                
            } while (MysqlDataReader.Read());
            MysqlDataReader.Close();
        }
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            for(int i=0;i<VoteList.Items.Count;i++)
            {
                //VoteList.Items[i].Enabled = false;
                if(VoteList.Items[i].Selected==true)
                {
                    //result id auto increment                       
                    string SQLDelete = @"Delete FROM Online_Polling_DB.dbo.[vote]
                            WHERE topic = @value1 ;";

                    SqlCommand MyCommand = new SqlCommand(SQLDelete, Sqlconnect);                    
                    
                    MyCommand.Parameters.AddWithValue("@value1", VoteList.Items[i].Text);                    
                    //執行SQL 語法
                    MyCommand.ExecuteNonQuery();
                    VoteList.Items.Remove(VoteList.Items[i].Text);
                }
            }
            if(VoteList.Items.Count==0)
            {
                DeleteButton.Visible = false;
                Literal1.Text = "You don't have vote activity";
            }

            //DeleteButton.Text = "刪除成功";
            //DeleteButton.Enabled = false;
        }
    }
}