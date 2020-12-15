using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Polling_Project
{
    public partial class AddVote : System.Web.UI.Page
    {
        static int i = 0;//the number of option
        string ConnectString;
        SqlConnection MySqlConnect;
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
                Response.Write(exep.ToString());
                Literal1.Text = "尚未登入，無法建立投票";
                Literal1.Visible = true;
                BuildButton.Enabled = false;
                return;
            }



            //SQL connection
            ConnectString = Getconnectionstring("mySqlConnectionName");
            MySqlConnect = new SqlConnection(ConnectString);
            MySqlConnect.Open();

            //if inputing, clear text
            //else, will recover
            this.TextBox1.Attributes.Add("onFocus", "if (this.value=='請輸入主題'){this.value='';}");
            this.TextBox1.Attributes.Add("onblur", "if (this.value==''){this.value='請輸入主題';}");
            this.TextBox2.Attributes.Add("onFocus", "if (this.value=='請輸入說明'){this.value='';}");
            this.TextBox2.Attributes.Add("onblur", "if (this.value==''){this.value='請輸入說明';}");
            this.StartDate.Attributes.Add("onblur", "if (this.value==''){this.value='請輸入開始日期';}");
            this.StartDate.Attributes.Add("onFocus", "if (this.value=='請輸入開始日期'){this.value='';}");
            this.EndDate.Attributes.Add("onblur", "if (this.value==''){this.value='請輸入結束日期';}");
            this.EndDate.Attributes.Add("onFocus", "if (this.value=='請輸入結束日期'){this.value='';}");
            if(!IsPostBack)
            {
                CheckBoxList1.Items.FindByValue("記名/不記名").Selected = true;
            }

            LabelPlaceHolder.Controls.Clear();
            //dynamic add textbox 
            TextBox tb;
            for (int j = 0; j < i; j++)
            {
                tb = new TextBox();
                tb.ID = "tbx" + j.ToString();
                tb.Text = "請輸入option" + j.ToString();
                tb.Attributes.Add("onFocus", "if (this.value=='" + tb.Text + "'){this.value='';}");
                tb.Attributes.Add("onblur", "if (this.value==''){this.value='" + tb.Text + "';}");
                LabelPlaceHolder.Controls.Add(tb);
                LabelPlaceHolder.Controls.Add(new LiteralControl("<br>"));
            }

        }
        protected bool CheckNull(TextBox tb)
        {
            if(tb.Text.Length < 3)
            {
                return true ;
            }
            string check = tb.Text.Substring(0, 3);
            if (tb.Text == null || check=="請輸入")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void BuildClick(object sender, EventArgs e)
        {
            if(CheckNull(TextBox1) && CheckNull(TextBox2) && CheckNull(StartDate) && CheckNull(EndDate) && CheckNull(TextBox1) )
            {
                for(int j=0;j<i;j++)
                {
                    var tb = LabelPlaceHolder.FindControl("tbx" + j.ToString()) as TextBox;
                    if(tb == null )
                    {
                        Literal1.Text = "Please input completely";
                        return;                        
                    }                        
                    if (CheckNull(tb))
                    {
                        Literal1.Text = "Successful!";
                    }
                    else
                    {
                        Literal1.Text = "Please input completely";
                        return;                        
                    }
                }
            }
            else
            {
                Literal1.Text = "Please input completely";
                return;
            }

            for (int k=0;k<i;k++)
            {
                string SQLinsert = @"INSERT INTO Online_Polling_DB.dbo.[vote]
                            ([host_id],[topic],[description],[option],[is_registered],[startdate],[enddate],[permission])
                            VALUES(@value1, @value2, @value3, @value4, @value5, @value6, @value7, @value8)";

                SqlCommand MyCommand = new SqlCommand(SQLinsert, MySqlConnect);
                HttpCookie myCookie = Request.Cookies["cook"];
                string userID = "";
                try
                {
                    if (myCookie.Values["UserID"] != null)
                    {
                        userID = myCookie.Values["UserID"];
                    }
                    else
                    {
                        Literal1.Text = "Please input completely";
                        return;
                    }
                }
                catch (Exception exce)
                {
                    Literal1.Text = exce.ToString(); 
                    exce.ToString();
                    return;
                }


                MyCommand.Parameters.AddWithValue("@value1", userID);
                MyCommand.Parameters.AddWithValue("@value2", TextBox1.Text);
                MyCommand.Parameters.AddWithValue("@value3", TextBox2.Text);
                var tbTemp = LabelPlaceHolder.FindControl("tbx" + k.ToString()) as TextBox;
                MyCommand.Parameters.AddWithValue("@value4", tbTemp.Text);
                MyCommand.Parameters.AddWithValue("@value5", CheckBoxList1.Items.FindByValue("記名/不記名").Selected);
                MyCommand.Parameters.AddWithValue("@value6", StartDate.Text);
                MyCommand.Parameters.AddWithValue("@value7", EndDate.Text);
                //manager student professor staff
                string permissionTemp = "";
                for (int j = 1; j <= 4; j++)
                {
                    if (CheckBoxList1.Items[j].Selected == true)
                    {
                        permissionTemp += "1";
                    }
                    else
                    {
                        permissionTemp += "0";
                    }
                }
                MyCommand.Parameters.AddWithValue("@value8", permissionTemp);

                //執行SQL INSERT 語法
                MyCommand.ExecuteNonQuery();
                Literal1.Text = "Successful!";
                BuildButton.Text = "建立完成";
                BuildButton.Enabled = false;
            }                                    

        }
        protected void AddOptionClick(object sender, EventArgs e)
        {
            List<string> option_a = new List<string>();
            for (int j = 0; j < i; j++)
            {
                var tbTemp = LabelPlaceHolder.FindControl("tbx" + j.ToString()) as TextBox;
                option_a.Add(tbTemp.Text);
            }
            i++;
            
            LabelPlaceHolder.Controls.Clear();
            //dynamic add textbox 
            TextBox tb;
            for (int j = 0; j < i; j++)
            {          
                tb = new TextBox();
                tb.ID = "tbx" + j.ToString();
                if(j<i-1)
                {
                    tb.Text = option_a[j].ToString();
                }
                else
                {
                    tb.Text = "請輸入option" + j.ToString();
                }

                tb.Attributes.Add("onFocus", "if (this.value=='"+tb.Text+"'){this.value='';}");
                tb.Attributes.Add("onblur", "if (this.value==''){this.value='"+ tb.Text + "';}");
                LabelPlaceHolder.Controls.Add(tb);
                LabelPlaceHolder.Controls.Add(new LiteralControl("<br>"));
            }
        }
        protected void Selection_Change(object sender, EventArgs e)
        {
            if(CheckBoxList1.Items.FindByValue("記名/不記名").Selected)//記名
            {
                for (int i = 1; i < CheckBoxList1.Items.Count; i++)
                {                    
                    CheckBoxList1.Items[i].Enabled = true;
                }
            }
            else//不記名
            {
                for(int i = 1;i<CheckBoxList1.Items.Count;i++)
                {
                    CheckBoxList1.Items[i].Selected = true;
                    CheckBoxList1.Items[i].Enabled = false;
                }
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
    }
}