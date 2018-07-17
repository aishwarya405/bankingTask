using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bankingTask
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                lblMsg.Text = "Username is required.";
                txtUsername.Focus(); lblMsg.ForeColor = Color.Red;
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                lblMsg.Text = "Password Code is required.";
                txtPassword.Focus(); lblMsg.ForeColor = Color.Red;
                return;
            }

            using (Bank_DBEntities dbContext = new Bank_DBEntities())
            {
                #region Username Check
                var chkTeller = (from t in dbContext.TellerMasters
                                 where t.TellerUserName.ToLower().Equals(txtUsername.Text.ToLower())
                                 select new { t.TellerUserName, t.TellerName, t.TellerCode, t.IsEnabled }).SingleOrDefault();

                var chkCustomer = (from c in dbContext.CustomerMasters
                                   where c.CustomerUserName.ToLower().Equals(txtUsername.Text.ToLower())
                                   select new { c.CustomerUserName, c.CustomerName, c.IsEnabled }).SingleOrDefault();

                if (chkTeller == null && chkCustomer == null)
                {

                    lblMsg.Text = "User not exist, Please try different.";
                    return;
                }
                else
                {
                    if (chkTeller != null)
                    {
                        if (chkTeller.IsEnabled.Equals(false))
                        {
                            lblMsg.Text = "User has been De Activated, Please contact admin.";
                            return;
                        }

                        Session["USERCODE"] = chkTeller.TellerCode;
                        Session["USERNAME"] = chkTeller.TellerName;
                    }
                    else
                    {
                        if (chkCustomer.IsEnabled.Equals(false))
                        {
                            lblMsg.Text = "User has been De Activated, Please contact admin.";
                            return;
                        }
                        Session["USERCODE"] = chkCustomer.CustomerUserName;
                        Session["USERNAME"] = chkCustomer.CustomerName;
                    }

                }
                #endregion

                #region Role Check
                var chkRole = (from l in dbContext.LinkUserToRoles
                               where l.UserCode.ToLower().Equals(txtUsername.Text.ToLower())
                               select new { l.Role.RoleName, l.Role.RoleCode }).SingleOrDefault();
                if (chkRole == null)
                {
                    lblMsg.Text = "User not mapped the role, Please try different.";
                    return;
                }
               
                Session["ROLENAME"] = chkRole.RoleName;
                Session["ROLECODE"] = chkRole.RoleCode;
               
                #endregion

                #region Username,Password Check

                chkTeller = (from t in dbContext.TellerMasters
                             where t.TellerUserName.ToLower().Equals(txtUsername.Text.ToLower()) && t.Password.ToLower().Equals(txtPassword.Text.ToLower())
                             select new { t.TellerUserName, t.TellerName, t.TellerCode, t.IsEnabled }).SingleOrDefault();

                chkCustomer = (from c in dbContext.CustomerMasters
                               where c.CustomerUserName.ToLower().Equals(txtUsername.Text.ToLower()) && c.Password.ToLower().Equals(txtPassword.Text.ToLower())
                               select new { c.CustomerUserName, c.CustomerName, c.IsEnabled }).SingleOrDefault();

                if (chkTeller == null && chkCustomer == null)
                {
                    lblMsg.Text = "Password not matched, Please try different.";
                    return;
                }
                #endregion



                Response.Redirect(ResolveUrl("~/Default.aspx"));

            }


        }
    }
}