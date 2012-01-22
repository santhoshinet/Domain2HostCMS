<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Domain2HostCMS.Models.ChangePasswordModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Content/FormLayout.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <% using (Html.BeginForm())
           { %>
        <ul class="ul">
            <li>
                <h2>
                    Change Password</h2>
            </li>
            <li>
                <%= Html.ValidationSummary(true, "Password change was unsuccessful. Please correct the errors and try again.") %>
            </li>
            <li>
                <%= Html.LabelFor(m => m.OldPassword) %>
            </li>
            <li>
                <%= Html.PasswordFor(m => m.OldPassword) %>
                <%= Html.ValidationMessageFor(m => m.OldPassword) %>
            </li>
            <li>
                <%= Html.LabelFor(m => m.NewPassword) %>
            </li>
            <li>
                <%= Html.PasswordFor(m => m.NewPassword) %>
                <%= Html.ValidationMessageFor(m => m.NewPassword) %>
            </li>
            <li>
                <%= Html.LabelFor(m => m.ConfirmPassword) %>
            </li>
            <li>
                <%= Html.PasswordFor(m => m.ConfirmPassword) %>
                <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
            </li>
            <li>
                <input type="submit" value="Change Password" />
            </li>
        </ul>
        <% } %>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitle" runat="server">
    Change Password
</asp:Content>