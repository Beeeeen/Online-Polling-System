using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace Online_Polling_Project
{
    public partial class WebForm3 : System.Web.UI.Page
    {        
        static string topic_IN;
        SqlConnection Sqlconnect;
        SqlCommand MyCommand;
        
        string SQLquery;
        protected void Page_Load(object sender, EventArgs e)
        {            
            
            if (!IsPostBack)
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
                    exep.ToString();
                    //Response.Write(exep.ToString());
                    //Literal1.Text = "尚未登入，無法進行投票";
                    //Literal1.Visible = true;
                    _Default.MySQLconnect();
                    SqlConnection Sqlconnect2 = _Default.GetSqlConnection();
                    string SQLquery2 = @"SELECT * FROM Online_Polling_DB.dbo.[vote] WHERE Is_registered = 0  ORDER BY ID ASC;";
                    SqlCommand MyCommand2 = new SqlCommand(SQLquery2, Sqlconnect2);
                    SqlDataReader MysqlDataReader2 = MyCommand2.ExecuteReader();
                    MysqlDataReader2.Read();



                    //read all
                    do
                    {
                        try
                        {
                            if (MysqlDataReader2["topic"].ToString() != "")
                            {

                                topic_IN = MysqlDataReader2["topic"].ToString().Replace(" ", string.Empty);



                                ListItem topic = new ListItem(topic_IN);
                                if (!VoteList.Items.Contains(topic))
                                {
                                    VoteList.Items.Add(topic_IN);

                                }
                            }
                        }
                        catch(Exception exep2)
                        {
                            exep2.ToString();
                            break;
                        }
                        
                    } while (MysqlDataReader2.Read());
                    MysqlDataReader2.Close();
                    return;
                }
                Literal1.Visible = true;
            
                //get category
                string category = "";
                _Default.MySQLconnect();
                Sqlconnect = _Default.GetSqlConnection();
                string build_permission = "0";
                SQLquery = @"SELECT * FROM Online_Polling_DB.dbo.[user] ;";
                MyCommand = new SqlCommand(SQLquery, Sqlconnect);
                SqlDataReader MysqlDataReader = MyCommand.ExecuteReader();
                MysqlDataReader.Read();
                do
                {
                    if (MysqlDataReader["id"].ToString() != "")
                    {
                        if (MysqlDataReader["id"].ToString() == userID)
                        {
                            category = MysqlDataReader["category"].ToString().Replace(" ", string.Empty);
                            build_permission = MysqlDataReader["build_permission"].ToString();
                            break;
                        }
                    }
                } while (MysqlDataReader.Read());
                MysqlDataReader.Close();


                if (category == "manager")
                {
                    ToPermissionManage.Visible = true;
                }
                if (build_permission == "1")
                {
                    VoteManage.Visible = true;
                }                
                //print the vote activity
                _Default.MySQLconnect();
                Sqlconnect = _Default.GetSqlConnection();
                SQLquery = @"SELECT * FROM Online_Polling_DB.dbo.[vote] ORDER BY ID ASC;";
                MyCommand = new SqlCommand(SQLquery, Sqlconnect);
                MysqlDataReader = MyCommand.ExecuteReader();
                MysqlDataReader.Read();


                //print the vote activity
                //read all
                do
                {
                    if (MysqlDataReader["topic"].ToString() != "")
                    {
                        string permission = MysqlDataReader["permission"].ToString().Replace(" ", string.Empty);
                        
                        if (category == "manager")
                        {
                            if (permission[0] != '1')
                            {                                
                                continue;
                            }                     
                        }
                        else if (category == "student")
                        {
                            if (permission[1] != '1')
                            {
                                continue;
                            }
                        }
                        else if (category == "professor")
                        {
                            if (permission[2] != '1')
                            {
                                continue;
                            }
                        }
                        else if (category == "staff")
                        {
                            if (permission[3] != '1')
                            {
                                continue;
                            }
                        }

                        topic_IN = MysqlDataReader["topic"].ToString().Replace(" ", string.Empty);
                        string enddate = "";
                        enddate = MysqlDataReader["enddate"].ToString().Replace(" ", string.Empty);
                        int day = Int32.Parse( enddate.Substring(3,2));
                        int month= Int32.Parse(enddate.Substring(0, 2));
                        
                        //Response.Write(DateTime.UtcNow.Date.ToString("d").Substring(5,2));
                        int nowDay = Int32.Parse(DateTime.UtcNow.Date.ToString("d").Substring(8, 2) );
                        int nowMonth = Int32.Parse(DateTime.UtcNow.Date.ToString("d").Substring(5, 2));
                        
                        if ((nowMonth>month &&nowDay>day) || (nowMonth == month && nowDay > day))
                        {
                            continue;
                        }
                        

                        ListItem topic = new ListItem(topic_IN);
                        if (!VoteList.Items.Contains(topic))
                        {
                            VoteList.Items.Add(topic_IN);

                        }
                    }
                } while (MysqlDataReader.Read());
                MysqlDataReader.Close();

                               
                
                
                

                
                

                

            }
        }
        public void ResultClick(Object sender, EventArgs e)
        {
            Response.Redirect("Result.aspx");
        }
        public void VoteManageClick(Object sender, EventArgs e)
        {
            Response.Redirect("VoteManagement.aspx");
        }
        public void ManageClick(Object sender, EventArgs e)
        {
            Response.Redirect("PermissionManagement.aspx");
        }
        protected void BulletedList1_Click(object sender, System.Web.UI.WebControls.BulletedListEventArgs e)
        {
            Response.Write("You selected: " + e.Index + ": " + VoteList.Items[e.Index].Text);
            topic_IN = VoteList.Items[e.Index].Text;
            _Default.MySQLconnect();
            SqlConnection Sqlconnect = _Default.GetSqlConnection();
            string SQLquery = @"SELECT * 
                               FROM Online_Polling_DB.dbo.[vote]
                                WHERE topic = '" + VoteList.Items[e.Index].Text
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
                    StartVoteButton.Visible = true;
                }
            }
            catch (Exception exep)
            {
                Response.Write(exep.ToString());
            }
        }
        public void StartVoteButtonClick(Object sender, EventArgs e)
        {
            // When the button is clicked,
            // change the button text, and disable it.
            System.Web.UI.WebControls.Button clickedButton = (System.Web.UI.WebControls.Button)sender;
            clickedButton.Text = "開始投票";
            Response.Redirect("voting.aspx?topic=" + topic_IN);

        }
        
    }
}