<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transaction.aspx.cs" Inherits="bankingTask.Transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Transaction</h2>
    <!-- begin panel -->
    <div class="panel pagination-danger clearfix m-b-0" style="padding: 4px;" runat="server" id="dvGrid">
        <div id="data-table_wrapper" class="dataTables_wrapper form-inline dt-bootstrap no-footer">
            <div class="row">
                
                <div class="col-sm-5" style="display: none">
                    <div id="data-table_filter" class="dataTables_filter">
                        <label style="width: 98%;">Search:<asp:TextBox ID="txtSerach" Style="width: 75%;" runat="server" CssClass="form-control input-sm" placeholder="User Name" AutoPostBack="true" OnTextChanged="txtSerach_TextChanged"></asp:TextBox></label>
                    </div>
                </div>
                <div class="col-sm-4" style="display: none">
                    <div class="dataTables_filter">
                    </div>
                </div>
                <div class="col-sm-3" style="display: none">
                    <div class="dataTables_length" id="data-table_length">
                        <label>
                            Show
                                    <asp:DropDownList ID="ddlPageSize" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                        <asp:ListItem Text="10" Value="10" Enabled="true"></asp:ListItem>
                                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                        <asp:ListItem Text="250" Value="250"></asp:ListItem>
                                        <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                        <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                        <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                            entries</label>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-sm-12">
                    <h3>Account Balance</h3>
                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" AllowPaging="true" ShowFooter="false"
                        DataKeyNames="CustomerUserName"
                        OnPageIndexChanging="gvDetails_PageIndexChanging"
                       HeaderStyle-BackColor="#3AC0F2"
        HeaderStyle-ForeColor="White" RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"
        RowStyle-ForeColor="#3A3A3A"
                        HeaderStyle-CssClass="alert alert-warning  alert-bordered fade in m-b-10"
                        PageSize="10" CssClass="table table-bordered table-hover dataTable no-footer dtr-inline">
                        <Columns>                         

                            <asp:TemplateField HeaderText="CustomerName">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("CustomerName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="CustomerType">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerType" runat="server" Text='<%# Eval("CustomerType")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="CustomerBankAccountNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerBankAccountNo" runat="server" Text='<%# Eval("CustomerBankAccountNo")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MasterAmount">
                                <ItemTemplate>
                                    <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("MasterAmount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="BalanceAmount">
                                <ItemTemplate>
                                    <asp:Label ID="lblPassword" runat="server" Text='<%# Eval("BalanceAmount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-warning alert-bordered fade in m-b-10">
                                <strong>Warning!</strong>
                                No data found!
                            <span class="close" data-dismiss="alert">×</span>
                            </div>

                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div class="col-sm-12" style="display: none">
                    <label id="data_table_info" runat="server"></label>
                </div>
            </div>

        </div>
    </div>


    <!-- end panel -->
    <!-- begin section-container -->
    <div class="section-container section-with-top-border" runat="server" id="dvInputs">
        <fieldset>
            <legend>Transfer Info</legend>

            <table>
                <tr>
                     <th>Account Number</th>
                    <td>
                        <%--<asp:TextBox ID="txtAccountNumber" runat="server" class="form-control" MaxLength="15"></asp:TextBox>--%>
                        <asp:DropDownList ID="ddlAccountNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAccountNumber_SelectedIndexChanged" ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red"  ControlToValidate="ddlAccountNumber" ValidationGroup="grp" ErrorMessage="Account Number is required." SetFocusOnError="true" InitialValue="--Choose--"></asp:RequiredFieldValidator>
                    </td>

                    <th>Customer Name</th>
                    <td>
                        <asp:TextBox ID="txtCustomerName" runat="server" class="form-control" MaxLength="250" ReadOnly="true"></asp:TextBox>

                         <asp:RequiredFieldValidator ID="reqUsername" runat="server" ForeColor="Red"  ControlToValidate="txtCustomerName" ValidationGroup="grp" ErrorMessage="Customer Name is required." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>

                </tr>               
                 <tr>
                    <th>Bank Name</th>
                    <td>
                        <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" MaxLength="50" ReadOnly="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red" ControlToValidate="txtBankName" ValidationGroup="grp" ErrorMessage="Bank Name is required." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>                  
                             
                    <th>IFSC Code</th>
                    <td>
                        <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="form-control" MaxLength="10" ReadOnly="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red" ControlToValidate="txtIFSCCode" ValidationGroup="grp" ErrorMessage="IFSC Code is required." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                     <th>Amount</th>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ControlToValidate="txtAmount" ValidationGroup="grp" ErrorMessage="amount is required." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="Regex2" runat="server" ValidationExpression="^[0-9]{0,10}$" ForeColor="Red" ControlToValidate="txtAmount" SetFocusOnError="true" ValidationGroup="grp"
ErrorMessage="Please enter valid amount."/>
                    </td>
                </tr>
            </table>
        </fieldset>

        
        <br />

        <table>
            <tr><th colspan="2"> <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label></th></tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-lime" OnClick="btnSave_Click" ValidationGroup="grp" Style="float: right;" Text="Save" />
                </td>
                <td>
                    <asp:Button ID="btnClose" runat="server" CssClass="btn btn-inverse" ValidationGroup="grp1" OnClick="btnClose_Click" Style="float: right;" Text="Close" /></td>
            </tr>
        </table>
          <br />
        <table>
            <tr>
                <th>Transaction History</th><th>Credit History</th>
            </tr>
            <tr>
                <td>
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="false" ShowFooter="false"
                       
                      
                       HeaderStyle-BackColor="#FFBAD2"
        HeaderStyle-ForeColor="White" RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"
        RowStyle-ForeColor="#3A3A3A"
                        HeaderStyle-CssClass="alert alert-warning  alert-bordered fade in m-b-10"
                        PageSize="10" CssClass="table table-bordered table-hover dataTable no-footer dtr-inline">
                        <Columns>          
                             <asp:TemplateField HeaderText="Dest. Holder Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerType" runat="server" Text='<%# Eval("DestinationAccountHolderName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    

                            <asp:TemplateField HeaderText="Dest. Acc. No">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerBankAccountNo" runat="server" Text='<%# Eval("CustomerBankAccountNo")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="TranferAmount">
                                <ItemTemplate>
                                    <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("TranferAmount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="BalanceAmount">
                                <ItemTemplate>
                                    <asp:Label ID="lblPassword" runat="server" Text='<%# Eval("BalanceAmount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-warning alert-bordered fade in m-b-10">
                                <strong>Warning!</strong>
                                No data found!
                            <span class="close" data-dismiss="alert">×</span>
                            </div>

                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
                <td>
                      <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" AllowPaging="false" ShowFooter="false"
                       
                      
                       HeaderStyle-BackColor="#90EE90"
        HeaderStyle-ForeColor="White" RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"
        RowStyle-ForeColor="#3A3A3A"
                        HeaderStyle-CssClass="alert alert-warning  alert-bordered fade in m-b-10"
                        PageSize="10" CssClass="table table-bordered table-hover dataTable no-footer dtr-inline">
                        <Columns>          
                             <asp:TemplateField HeaderText="Source. Holder Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerType" runat="server" Text='<%# Eval("SourceAccountHolderName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    

                            <asp:TemplateField HeaderText="Source. Acc. No">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerBankAccountNo" runat="server" Text='<%# Eval("SourceBankAccountNo")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Credit Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("CreditAmount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                          
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-warning alert-bordered fade in m-b-10">
                                <strong>Warning!</strong>
                                No data found!
                            <span class="close" data-dismiss="alert">×</span>
                            </div>

                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
      
    </div>
    <!-- end section-container -->
   
    <asp:HiddenField ID="hdnStatus" runat="server" />
    <asp:HiddenField ID="hdnUserID" runat="server" />
</asp:Content>

