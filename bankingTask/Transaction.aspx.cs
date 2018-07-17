using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bankingTask
{
    public partial class Transaction : System.Web.UI.Page
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

                
                empCode = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["UserID"])) ? "" : Convert.ToString(Request.QueryString["UserID"]);
                empCode = Session["USERNAME"].ToString();
                action = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Action"])) ? "" : Convert.ToString(Request.QueryString["Action"]);

                if (!IsPostBack)
                {
                    fLoadAccount();
                    fBindData();                                  
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region Transaction
           
                using (Bank_DBEntities dbContext = new Bank_DBEntities())
                {
                empCode = Session["USERNAME"].ToString();
                //Check Name Found
                var chkUser = (from a in dbContext.BalanceMasters
                               join b in dbContext.CustomerMasters on a.CustomerBankAccountNo equals b.CustomerBankAccountNo
                               where b.CustomerUserName.ToLower().Equals(empCode.ToLower())
                               select new { a.CustomerBankAccountNo,a.CustomerIFSCNo,b.CustomerBankName,b.CustomerName, a.MasterAmount, a.BalanceAmount }).SingleOrDefault();                                  
                                  
                if (chkUser !=null)
                {
                    // Store to TransferHistories
                    var grdEmp = dbContext.TransferHistories.Create();
                    grdEmp.CustomerBankAccountNo = chkUser.CustomerBankAccountNo;
                    grdEmp.DestinationBankAccountNo = this.ddlAccountNumber.SelectedValue.Trim();
                    grdEmp.DestinationIFSCNo = this.txtIFSCCode.Text.Trim();
                    grdEmp.DestinationBankName = this.txtBankName.Text.Trim();
                    grdEmp.DestinationAccountHolderName = this.txtCustomerName.Text.Trim();
                    grdEmp.MasterAmount = chkUser.BalanceAmount;
                    grdEmp.TranferAmount =Convert.ToDecimal(txtAmount.Text);
                    grdEmp.BalanceAmount =(chkUser.BalanceAmount- grdEmp.TranferAmount);
                    // check the balance
                    if (chkUser.BalanceAmount< grdEmp.TranferAmount)
                    {
                        this.lblMsg.Text = "In sufficient Amout.";
                        fLoadAccountInfo(ddlAccountNumber.SelectedValue);
                        return;
                    }                  

                    dbContext.TransferHistories.Add(grdEmp);
                    dbContext.SaveChanges();

                    // Store to CreditHistories
                    var grdCredit = dbContext.CreditHistories.Create();
                    grdCredit.CreditrBankAccountNo = ddlAccountNumber.SelectedValue;
                    grdCredit.SourceBankAccountNo = chkUser.CustomerBankAccountNo;
                    grdCredit.SourceBankName = chkUser.CustomerBankName;
                    grdCredit.SourceIFSCNo = chkUser.CustomerIFSCNo;
                    grdCredit.SourceAccountHolderName = chkUser.CustomerName;                    
                    grdCredit.CreditAmount = Convert.ToDecimal(txtAmount.Text);
                    dbContext.CreditHistories.Add(grdCredit);
                    dbContext.SaveChanges();

                    // deduct balance
                    var grdBank = dbContext.BalanceMasters.Where(x=>x.CustomerBankAccountNo.ToLower().Equals(chkUser.CustomerBankAccountNo.ToLower())).FirstOrDefault();
                    grdBank.BalanceAmount = (grdBank.BalanceAmount - Convert.ToDecimal(txtAmount.Text));
                    dbContext.SaveChanges();

                    // Add balance
                    var grdAdsdBank = dbContext.BalanceMasters.Where(x => x.CustomerBankAccountNo.ToLower().Equals(ddlAccountNumber.SelectedValue.ToLower())).FirstOrDefault();
                    grdAdsdBank.BalanceAmount = (grdAdsdBank.BalanceAmount + Convert.ToDecimal(txtAmount.Text));
                    dbContext.SaveChanges();


                    fBindData();

                }
                else
                {
                    this.lblMsg.Text = "Amount is not found.";
                    fLoadAccountInfo(ddlAccountNumber.SelectedValue);
                    return;
                }                

                    
                lblMsg.Text = " Transaction Created successfully";
                }
            #endregion

            fBindData();           
        }

        protected void txtSerach_TextChanged(object sender, EventArgs e)
        {
            fBindData();
        }

        #endregion

        #region Method

        private void fLoadAccountInfo(string accountNo)
        {
            using (Bank_DBEntities dbContext = new Bank_DBEntities())
            { 
                var data = dbContext.CustomerMasters.Where(x=>x.CustomerBankAccountNo.ToLower().Equals(accountNo.ToLower())).Select(x => new {x.CustomerName, x.CustomerIFSCNo,x.CustomerBankName }).SingleOrDefault();

                if (data != null)
                {
                    txtBankName.Text = data.CustomerBankName;
                    txtIFSCCode.Text = data.CustomerIFSCNo;
                    txtCustomerName.Text = data.CustomerName;
                }
            }
        }

        protected void ddlAccountNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            fLoadAccountInfo(ddlAccountNumber.SelectedValue);
        }

        private void fLoadAccount()
        {
            using (Bank_DBEntities dbContext = new Bank_DBEntities())
            {
                // Bind account information
                var data = dbContext.CustomerMasters.Where(x => x.CustomerUserName.ToLower() != empCode.ToLower()).Select(x => new { Value = x.CustomerBankAccountNo, Text = x.CustomerBankAccountNo + "[ " + x.CustomerName + "]" }).ToList();
                ddlAccountNumber.DataSource = data;
                ddlAccountNumber.DataTextField = "Text";
                ddlAccountNumber.DataValueField = "Value";
                ddlAccountNumber.DataBind();
                ddlAccountNumber.Items.Insert(0, "--Choose--");
            }
        }
        private void fBindData()
        {
            using (Bank_DBEntities dbContext = new Bank_DBEntities())
            {              

                empCode = Session["USERNAME"].ToString();
                var grdData = (from a in dbContext.BalanceMasters
                               group a by a.CustomerBankAccountNo into g
                               join b in dbContext.CustomerMasters on g.FirstOrDefault().CustomerBankAccountNo equals b.CustomerBankAccountNo
                               where b.CustomerUserName.ToLower().Equals(empCode.ToLower())
                               //let balamt = g.FirstOrDefault().MasterAmount - g.Sum(g => g.BalanceAmount)
                               select new { b.CustomerName,b.CustomerType, b.CustomerUserName, b.CustomerBankAccountNo, g.FirstOrDefault().MasterAmount, g.FirstOrDefault().BalanceAmount }).ToList(); 
               

                // Allow paging
                if (ddlPageSize.SelectedValue == "0") { gvDetails.AllowPaging = false; } else { int pageSize = Convert.ToInt16(ddlPageSize.SelectedValue); gvDetails.AllowPaging = true; gvDetails.PageSize = pageSize; }

                gvDetails.DataSource = grdData;
                gvDetails.DataBind();

                if (grdData.Count > 0)
                {
                    int pageCount = gvDetails.PageCount;
                    int currentPage = gvDetails.PageIndex;

                    var dd = grdData[0].CustomerBankAccountNo;

                    var history = (from a in dbContext.TransferHistories
                                   where a.CustomerBankAccountNo.ToLower().Equals(dd.ToLower())
                                   select new { a.CustomerBankAccountNo, a.DestinationBankAccountNo, a.DestinationAccountHolderName, a.MasterAmount, a.BalanceAmount, a.TranferAmount }).ToList();
                    GridView1.DataSource = history;
                    GridView1.DataBind();

                    var credit = (from a in dbContext.CreditHistories
                                   where a.CreditrBankAccountNo.ToLower().Equals(dd.ToLower())
                                   select new { a.SourceAccountHolderName, a.SourceBankAccountNo, a.CreditAmount }).ToList();
                    GridView2.DataSource = credit;
                    GridView2.DataBind();
                }
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

       

        protected void btnClose_Click(object sender, EventArgs e)
        {
            hdnStatus.Value = "Close";
            dvInputs.Visible = false; dvGrid.Visible = true;
            Response.Redirect(string.Format("~/Transaction.aspx?Action=Close"));
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            fBindData();
        }

        #endregion
    }
}