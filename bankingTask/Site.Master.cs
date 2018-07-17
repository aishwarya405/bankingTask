using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bankingTask
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {          
            string USERCODE = Convert.ToString(Session["USERCODE"]);
            string USERNAME = Convert.ToString(Session["USERNAME"]);
            string ROLENAME = Convert.ToString(Session["ROLENAME"]);
            string ROLECODE = Convert.ToString(Session["ROLECODE"]);
            lblRole.Text = ROLENAME;
            lblUser.Text = USERNAME;
            if (string.IsNullOrEmpty(USERCODE) && string.IsNullOrEmpty(USERNAME))
            {
                Response.Redirect(ResolveUrl("~/Login.aspx"));
            }
        }
    }
}