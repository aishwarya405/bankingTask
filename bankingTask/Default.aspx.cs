using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bankingTask
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string USERCODE = Convert.ToString(Session["USERCODE"]);
            string USERNAME = Convert.ToString(Session["USERNAME"]);
            string ROLENAME = Convert.ToString(Session["ROLENAME"]);
            string ROLECODE = Convert.ToString(Session["ROLECODE"]);
            if (string.IsNullOrEmpty(USERCODE) && string.IsNullOrEmpty(USERNAME))
            {
                Response.Redirect(ResolveUrl("~/Login.aspx"));
            }
        }
    }
}