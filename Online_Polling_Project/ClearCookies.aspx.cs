using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Polling_Project
{
    public partial class ClearCookies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Request.Cookies["cook"] != null)
            {
                Response.Cookies["cook"].Expires = DateTime.Now.AddDays(-1);
            }
            Response.Redirect("~/");
        }
    }
}