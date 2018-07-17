<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="bankingTask.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">   
     <title>Bank | Login</title>
    <link href='http://fonts.googleapis.com/css?family=Titillium+Web:400,300,600' rel='stylesheet' type='text/css' />
    <link rel="stylesheet" href="css/normalize.css" />
    <link rel="stylesheet" href="css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="form">
       <%-- <div class="image">
            <img src="../image/door-step-logo.jpg" alt="" style="width: 100%; max-height: 85px;" />
        </div>--%>
        <ul class="tab-group">
            <li class="tab active"><a href="SignIn.aspx">Sign In </a></li>
            <li class="tab"><a href="SignUp.aspx">Sign Up</a></li>
        </ul>
        <div class="tab-content">
            <div id="login">
                <h1>Welcome Back!</h1>               
                <div class="field-wrap">                    
                    <asp:TextBox ID="txtUsername" runat="server" MaxLength="10" PlaceHolder="Username*" ToolTip="Username"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqUsername" runat="server" ControlToValidate="txtUsername" ValidationGroup="grp" ErrorMessage="Username is required."></asp:RequiredFieldValidator>
                </div>

                <div class="field-wrap">                   
                    <asp:TextBox ID="txtPassword" runat="server" MaxLength="10" PlaceHolder="Password*" ToolTip="Password" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="txtPassword" ValidationGroup="grp" ErrorMessage="Password is required."></asp:RequiredFieldValidator>
                </div>               
                <asp:Button ID="btnSignIn" CssClass="button button-block" runat="server" Text="Sign In" ValidationGroup="grp" OnClick="btnSignIn_Click" />
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <script src="../js/jquery-1.11.3.min.js"></script>
    <script src="js/index.js"></script>

    </form>
</body>
</html>
