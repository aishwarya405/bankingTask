using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bankingTask
{
    public partial class Customer : System.Web.UI.Page
    {
        #region Declarations       
        StringBuilder sb;
        string action = string.Empty, empCode = string.Empty, compCode = string.Empty;
        string Username = string.Empty, COMPCODE = string.Empty, ROLENAME = string.Empty;
        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["USERCODE"])))
            {
                this.lblMsg.Text = string.Empty;              
                ROLENAME = Convert.ToString(Session["ROLENAME"]);   
                
                if(ROLENAME!= "Teller")
                {
                    lnkCreateNewUser.Visible = false;
                }
                empCode = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["UserID"])) ? "" : Convert.ToString(Request.QueryString["UserID"]);
                Username = Session["USERNAME"].ToString();
                action =string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Action"]))?"": Convert.ToString(Request.QueryString["Action"]);

            if (!IsPostBack)
            {

                fBindData();
                    dvInputs.Visible = false; dvGrid.Visible = true;


                    if(action.Equals("Create"))
                    {
                        dvInputs.Visible = true; dvGrid.Visible = false;
                    }
                    if (!string.IsNullOrEmpty(empCode) && action.Equals("Edit"))
                    {
                        fGetEmpDetails();
                            dvInputs.Visible = true; dvGrid.Visible = false;
                        txtAccountNumber.Enabled = false;txtUserName.Enabled = false;
                    }
            }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void fGetEmpDetails()
        {
            using (Bank_DBEntities dbContext = new Bank_DBEntities())
            {
                var grdEmp = dbContext.CustomerMasters.Where(x => x.CustomerUserName.Equals(empCode)).SingleOrDefault();
                if (grdEmp != null)
                {

                    txtCustomerName.Text = grdEmp.CustomerName;
                    rblGender.SelectedValue = Convert.ToString(grdEmp.Gender);                    
                    chkIsEnabled.Checked = grdEmp.IsEnabled;
                    txtUserName.Text = grdEmp.CustomerUserName;
                    txtPassword.Text = (grdEmp.Password);
                    rblCustomerType.SelectedValue = Convert.ToString(grdEmp.CustomerType);
                    txtBankName.Text = grdEmp.CustomerBankName;
                    txtAccountNumber.Text = (grdEmp.CustomerBankAccountNo);
                    txtIFSCCode.Text = (grdEmp.CustomerIFSCNo);
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            #region Create
            if (action == "Create")
            {
                using (Bank_DBEntities dbContext = new Bank_DBEntities())
                {

                    //Check Name Found
                    var chkUser = dbContext.CustomerMasters.Where(x => x.CustomerUserName.ToLower().Equals(txtUserName.Text.ToLower())).ToList();
                    if (chkUser.Count > 0)
                    {
                        this.lblMsg.Text = "Username is Found.";
                        txtUserName.Focus();
                        return;
                    }

                     chkUser = dbContext.CustomerMasters.Where(x => x.CustomerBankAccountNo.ToLower().Equals(txtAccountNumber.Text.ToLower())).ToList();
                    if (chkUser.Count > 0)
                    {
                        this.lblMsg.Text = "Account Number is Found.";
                        txtAccountNumber.Focus();
                        return;
                    }
                    var grdEmp = dbContext.CustomerMasters.Create();
                    grdEmp.CustomerBankName = this.txtBankName.Text.Trim();
                    grdEmp.CustomerBankAccountNo = this.txtAccountNumber.Text.Trim();
                    grdEmp.CustomerIFSCNo = this.txtIFSCCode.Text.Trim();
                    grdEmp.CustomerUserName = this.txtUserName.Text.Trim();
                    grdEmp.Password = this.txtPassword.Text.Trim();
                    grdEmp.CustomerName = this.txtCustomerName.Text.Trim();
                    grdEmp.Gender = rblGender.SelectedValue;
                    grdEmp.CustomerType = rblCustomerType.SelectedValue;
                    grdEmp.IsEnabled = this.chkIsEnabled.Checked;
                    dbContext.CustomerMasters.Add(grdEmp);
                    dbContext.SaveChanges();

                    var role = dbContext.LinkUserToRoles.Create();
                    role.UserCode = this.txtUserName.Text.Trim();
                    role.RoleCode = 2;
                    dbContext.LinkUserToRoles.Add(role);
                    dbContext.SaveChanges();

                    var grdBank = dbContext.BalanceMasters.Create();
                    grdBank.CustomerBankAccountNo = this.txtAccountNumber.Text.Trim();
                    grdBank.CustomerIFSCNo = this.txtIFSCCode.Text.Trim();                    
                    if (rblCustomerType.SelectedValue == "Premier") { grdBank.MasterAmount = 10000; grdBank.BalanceAmount = 10000; } else { grdBank.MasterAmount = 1000; grdBank.BalanceAmount = 1000; }                                                        
                    dbContext.BalanceMasters.Add(grdBank);
                    dbContext.SaveChanges();
                    lblMsg.Text = " Customer Created successfully";
                }
            }
            #endregion

            #region Edit
            if (action == "Edit")
            {
                using (Bank_DBEntities dbContext = new Bank_DBEntities())
                {                  

                    var grdEmp = dbContext.CustomerMasters.Where(x => x.CustomerUserName.ToLower().Equals(empCode.ToLower())).SingleOrDefault();                   
                    grdEmp.CustomerBankName = this.txtBankName.Text.Trim();                  
                    grdEmp.CustomerIFSCNo = this.txtIFSCCode.Text.Trim();                   
                    grdEmp.Password = this.txtPassword.Text.Trim();
                    grdEmp.CustomerName = this.txtCustomerName.Text.Trim();
                    grdEmp.Gender = rblGender.SelectedValue;
                    grdEmp.CustomerType = rblCustomerType.SelectedValue;
                    grdEmp.IsEnabled = this.chkIsEnabled.Checked;
                    dbContext.SaveChanges();

                    var grdBank = dbContext.BalanceMasters.Where(x => x.CustomerBankAccountNo.ToLower().Equals(this.txtAccountNumber.Text.Trim())).SingleOrDefault();
                    if (grdBank != null)
                    {
                        grdBank.CustomerBankAccountNo = this.txtAccountNumber.Text.Trim();
                        grdBank.CustomerIFSCNo = this.txtIFSCCode.Text.Trim();
                        if (rblCustomerType.SelectedValue == "Premier") { grdBank.MasterAmount = 10000; grdBank.BalanceAmount = 10000; } else { grdBank.MasterAmount = 1000; grdBank.BalanceAmount = 1000; }

                        dbContext.SaveChanges();
                    }
                    else
                    {
                        grdBank = dbContext.BalanceMasters.Create();
                        grdBank.CustomerBankAccountNo = this.txtAccountNumber.Text.Trim();
                        grdBank.CustomerIFSCNo = this.txtIFSCCode.Text.Trim();
                        if (rblCustomerType.SelectedValue == "Premier") { grdBank.MasterAmount = 10000; grdBank.BalanceAmount = 10000; } else { grdBank.MasterAmount = 1000; grdBank.BalanceAmount = 1000; }
                        dbContext.BalanceMasters.Add(grdBank);
                        dbContext.SaveChanges();
                    }
                    lblMsg.Text = "Customer details modified successfully";
                }

            }
            #endregion

            fBindData();
            fGetEmpDetails();
        }

        protected void txtSerach_TextChanged(object sender, EventArgs e)
        {
            fBindData();
        }

        #endregion

        #region Method


        private void fBindData()
        {
            using (Bank_DBEntities dbContext = new Bank_DBEntities())
            {

                var grdData = (from u in dbContext.CustomerMasters
                               orderby u.CustomerUserName ascending
                               select new { u.CustomerName, u.CustomerType, u.CustomerBankAccountNo, u.CustomerIFSCNo,u.CustomerUserName, u.Password, u.IsEnabled }).ToList();
                // Search 
                if (!string.IsNullOrEmpty(txtSerach.Text))
                {
                    grdData = grdData.Where(x => x.CustomerName.ToLower().StartsWith(txtSerach.Text.Trim().ToLower())).ToList();
                }

                // Allow paging
                if (ddlPageSize.SelectedValue == "0") { gvDetails.AllowPaging = false; } else { int pageSize = Convert.ToInt16(ddlPageSize.SelectedValue); gvDetails.AllowPaging = true; gvDetails.PageSize = pageSize; }

                gvDetails.DataSource = grdData;
                gvDetails.DataBind();
                int pageCount = gvDetails.PageCount;
                int currentPage = gvDetails.PageIndex;
                int recordCount = grdData.Count;
                data_table_info.InnerText = "Page " + (currentPage + 1) + " of " + pageCount + " Total Records: " + recordCount + " entries";
            }
        }
        #endregion

        #region Grid View
        protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            fBindData();
            gvDetails.PageIndex = e.NewPageIndex;
            gvDetails.DataBind();
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblIsEnabled = (Label)e.Row.FindControl("lblIsEnabled");
                if (lblIsEnabled.Text.ToLower().Equals("true"))
                {
                    lblIsEnabled.Text = "Active";
                    lblIsEnabled.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblIsEnabled.Text = "In-Active";
                    lblIsEnabled.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void lnkRemove_Click(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
            using (Bank_DBEntities dbContext = new Bank_DBEntities())
            {
                var grdDelete = dbContext.CustomerMasters.Where(x => x.CustomerUserName.Equals(lnkRemove.CommandArgument)).SingleOrDefault();
                if (grdDelete != null)
                {
                    dbContext.CustomerMasters.Attach(grdDelete);
                    dbContext.CustomerMasters.Remove(grdDelete);
                    dbContext.SaveChanges();
                }
                fBindData();
                lblMsg.Text = "Customer details deleted successfully";

            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            hdnStatus.Value = "Edit";
            LinkButton lnkEdit = (LinkButton)sender;
            hdnUserID.Value = lnkEdit.CommandArgument.Split('&')[0].ToString();

            Response.Redirect(string.Format("~/Customer.aspx?Action=Edit&UserID={0}", hdnUserID.Value));
        }

        protected void lnkCreateNewEmployee_Click(object sender, EventArgs e)
        {
            hdnStatus.Value = "Create";
            dvInputs.Visible = true; dvGrid.Visible = false;
            Response.Redirect(string.Format("~/Customer.aspx?Action=Create"));
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            hdnStatus.Value = "Close";
            dvInputs.Visible = false; dvGrid.Visible = true;
            Response.Redirect(string.Format("~/Customer.aspx?Action=Close"));
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            fBindData();
        }

        #endregion
    }
}