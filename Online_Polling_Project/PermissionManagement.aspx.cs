using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Polling_Project
{
    public partial class PermissionManagement : System.Web.UI.Page
    {
        SqlConnection Sqlconnect;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _Default.MySQLconnect();
                Sqlconnect = _Default.GetSqlConnection();

                string SQLquery = @"SELECT * FROM Online_Polling_DB.dbo.[user];";
                SqlCommand MyCommand = new SqlCommand(SQLquery, Sqlconnect);
                SqlDataReader MysqlDataReader = MyCommand.ExecuteReader();
                MysqlDataReader.Read();
                int permission = 0;
                do
                {
                    if (MysqlDataReader["id"].ToString() != "")
                    {
                        permission = Int32.Parse(MysqlDataReader["build_permission"].ToString().Replace(" ", string.Empty));
                        string name = MysqlDataReader["name"].ToString().Replace(" ", string.Empty);
                        string category = MysqlDataReader["category"].ToString().Replace(" ", string.Empty);
                        if (category != "manager")
                        {
                            UserChecklist.Items.Add(name);
                            if (permission == 1)
                            {
                                UserChecklist.Items.FindByValue(name).Selected = true;
                            }
                            else
                            {
                                UserChecklist.Items.FindByValue(name).Selected = false;
                            }
                        }

                    }
                } while (MysqlDataReader.Read());
                MysqlDataReader.Close();
            }
                
        }
        protected void CheckBoxIndexChanged(object sender, EventArgs e)
        {
            Sqlconnect = _Default.GetSqlConnection();
            for (int i=0;i< UserChecklist.Items.Count; i++)
            {
                string SQLquery = "";
                if(UserChecklist.Items[i].Selected)
                {
                    SQLquery = @"UPDATE Online_Polling_DB.dbo.[user] SET build_permission = 1 WHERE name = @value1;";
                }
                else
                {
                    SQLquery = @"UPDATE Online_Polling_DB.dbo.[user] SET build_permission = 0 WHERE name = @value1;";
                }
                SqlCommand MyCommand = new SqlCommand(SQLquery, Sqlconnect);

                MyCommand.Parameters.AddWithValue("@value1", UserChecklist.Items[i].Text);

                MyCommand.ExecuteNonQuery();


            }
        }
    }
}