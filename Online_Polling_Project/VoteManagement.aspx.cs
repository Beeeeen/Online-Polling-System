using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Polling_Project
{
    public partial class VoteManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void AddVoteClick(object sender, EventArgs e)
        {
            Response.Redirect("~/AddVote");
        }
        protected void DeleteVoteClick(object sender, EventArgs e)
        {
            Response.Redirect("~/DeleteVote");
        }
        protected void ModifyVoteClick(object sender, EventArgs e)
        {
            Response.Redirect("~/ModifyVote");
        }
    }
}