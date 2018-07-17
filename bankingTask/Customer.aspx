<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="bankingTask.Customer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Customer</h2>
    <!-- begin panel -->
    <div class="panel pagination-danger clearfix m-b-0" style="padding: 4px;" runat="server" id="dvGrid">
        <div id="data-table_wrapper" class="dataTables_wrapper form-inline dt-bootstrap no-footer">
            <div class="row">
                <div class="col-sm-12" runat="server" id="dvCreateNewUser">
                    <asp:Button ID="lnkCreateNewUser" runat="server" CssClass="btn btn-lime" BackColor="Green" ForeColor="White" Font-Bold="true" OnClick="lnkCreateNewEmployee_Click" Text="Create Customer" />

                </div>
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
                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" AllowPaging="true" ShowFooter="false"
                        DataKeyNames="CustomerUserName"
                        OnPageIndexChanging="gvDetails_PageIndexChanging"
                        OnRowDataBound="gvDetails_RowDataBound"
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

                            <asp:TemplateField HeaderText="Username">
                                <ItemTemplate>
                                    <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("CustomerUserName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <asp:Label ID="lblPassword" runat="server" Text='<%# Eval("Password")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsEnabled" runat="server" Text='<%# Eval("IsEnabled")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-warning btn-xs" CommandName="Edit" CommandArgument='<%# Eval("CustomerUserName")%>' ToolTip="Click to Edit" OnClick="lnkEdit_Click">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span> Edit
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkRemove" runat="server" CssClass="btn btn-danger btn-xs" CommandName="Remove" CommandArgument='<%# Eval("CustomerUserName")%>' ToolTip="Click to Remove"
                                        OnClientClick="return confirm('Do you want to delete?')" OnClick="lnkRemove_Click">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>  Delete
                                    </asp:LinkButton>
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
            <legend>Customer Info</legend>

            <table>
                <tr>
                    <th>Customer Name</th>
                    <td>
                        <asp:TextBox ID="txtCustomerName" runat="server" class="form-control" MaxLength="250"></asp:TextBox>

                         <asp:RequiredFieldValidator ID="reqUsername" runat="server" ForeColor="Red"  ControlToValidate="txtCustomerName" ValidationGroup="grp" ErrorMessage="Customer Name is required." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>

                </tr>

                <tr>

                    <th>Gender</th>
                    <td>
                        <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="Female" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <th>User Name</th>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red"  runat="server" ControlToValidate="txtUserName" ValidationGroup="grp" ErrorMessage="Username is required." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>

                    <th>Password</th>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" class="form-control" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ValidationGroup="grp" ErrorMessage="Password is required." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>

                    <th>Active
                    </th>
                    <td>
                        <asp:CheckBox ID="chkIsEnabled" runat="server" Checked></asp:CheckBox>
                    </td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend>Bank Info</legend>
            <table>
                <tr>
                    <th>Bank Name</th>
                    <td>
                        <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"  ControlToValidate="txtBankName" ValidationGroup="grp" ErrorMessage="Bank Name is required." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>

                    <th>Account Number</th>
                    <td>
                        <asp:TextBox ID="txtAccountNumber" runat="server" class="form-control" MaxLength="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red"  ControlToValidate="txtAccountNumber" ValidationGroup="grp" ErrorMessage="Account Number is required." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>Account Type</th>
                    <td>
                        <asp:RadioButtonList ID="rblCustomerType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Premier" Value="Premier"></asp:ListItem>
                            <asp:ListItem Text="Average" Value="Average" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList></td>
                    <th>IFSC Code</th>
                    <td>
                        <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red" ControlToValidate="txtIFSCCode" ValidationGroup="grp" ErrorMessage="IFSC Code is required." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>


                </tr>
            </table>
        </fieldset>
        <br />

        <table>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-lime" OnClick="btnSave_Click" ValidationGroup="grp" Style="float: right;" Text="Save" />
                </td>
                <td>
                    <asp:Button ID="btnClose" runat="server" CssClass="btn btn-inverse" ValidationGroup="grp1" OnClick="btnClose_Click" Style="float: right;" Text="Close" /></td>
            </tr>
        </table>

    </div>
    <!-- end section-container -->
    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:HiddenField ID="hdnStatus" runat="server" />
    <asp:HiddenField ID="hdnUserID" runat="server" />
</asp:Content>
