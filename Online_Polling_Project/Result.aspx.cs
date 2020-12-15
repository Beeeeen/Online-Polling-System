using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Polling_Project
{
    public partial class Result : System.Web.UI.Page
    {
        static string topic_IN;
        protected void Page_Load(object sender, EventArgs e)
        {
            _Default.MySQLconnect();
            SqlConnection Sqlconnect = _Default.GetSqlConnection();
            string SQLquery = @"SELECT * FROM Online_Polling_DB.dbo.[vote] ORDER BY ID ASC;";
            SqlCommand MyCommand = new SqlCommand(SQLquery, Sqlconnect);
            SqlDataReader MysqlDataReader = MyCommand.ExecuteReader();
            MysqlDataReader.Read();



            //read all
            do
            {
                if (MysqlDataReader["topic"].ToString() != "")
                {
                    topic_IN = MysqlDataReader["topic"].ToString().Replace(" ", string.Empty);
                    string enddate = "";
                    enddate = MysqlDataReader["enddate"].ToString().Replace(" ", string.Empty);
                    int day = Int32.Parse(enddate.Substring(3, 2));
                    int month = Int32.Parse(enddate.Substring(0, 2));

                    //Response.Write(DateTime.UtcNow.Date.ToString("d").Substring(5,2));
                    int nowDay = Int32.Parse(DateTime.UtcNow.Date.ToString("d").Substring(8, 2));
                    int nowMonth = Int32.Parse(DateTime.UtcNow.Date.ToString("d").Substring(5, 2));
                    if ((nowMonth > month && nowDay > day) || (nowMonth == month && nowDay > day))
                    {
                     
                    }
                    else
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
        protected void BulletedList1_Click(object sender, System.Web.UI.WebControls.BulletedListEventArgs e)
        {
            //print vote
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
                    Label1.Text += "<br /><br />";
                    Label1.Visible = true;                    
                }
            }
            catch (Exception exep)
            {
                Response.Write(exep.ToString());
            }
            MysqlDataReader.Close();
            //print result                                    
            SQLquery = @"SELECT * 
                               FROM Online_Polling_DB.dbo.[result]
                                WHERE topic = '" + VoteList.Items[e.Index].Text
                                + "' ;";
            MyCommand = new SqlCommand(SQLquery, Sqlconnect);
            MysqlDataReader = MyCommand.ExecuteReader();
            MysqlDataReader.Read();
            Label1.Text += "投票結果:<br />";

            //for draw graph
            List<double> yval = new List<double>();
            List<string> xval = new List<string>();
            try
            {
                do
                {
                    if (MysqlDataReader["result_id"].ToString() != null)
                    {
                        string option = MysqlDataReader["option"].ToString().Replace(" ", string.Empty);
                        if (!xval.Contains(option))
                        {
                            xval.Add(option);
                            yval.Add(0); 
                        }
                        int index=xval.IndexOf(option);
                        yval[index] += 1;
                        Label1.Text += "選項:";
                        Label1.Text += option;
                        Label1.Text += "<br />";
                        Label1.Text += "投票人:";
                        Label1.Text += MysqlDataReader["voter_id"].ToString().Replace(" ", string.Empty);
                        Label1.Text += "<br /><br />";

                    }
                }
                while (MysqlDataReader.Read());

            }
            catch (Exception exep)
            {
                Response.Write(exep.ToString());
            }

                       
            //lsit<string> xval = { "Peter", "Andrew", "Julie", "Mary", "Dave" };
            Chart1.Series["Series1"].Points.DataBindXY(xval, yval);

        }
    }
}