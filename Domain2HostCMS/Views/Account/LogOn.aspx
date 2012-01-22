<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Domain2HostCMS.Models.LogOnModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitle" runat="server">
    LogOn
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <% using (Html.BeginForm())
           { %>
        <ul class="ul">
            <li>
                <h2>
                    Authenticate yourself</h2>
            </li>
            <li><span class="error">
                <%= Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.") %>
            </span></li>
            <li>
                <%= Html.LabelFor(m => m.UserName) %>
            </li>
            <li>
                <%= Html.TextBoxFor(m => m.UserName) %>
                <%= Html.ValidationMessageFor(m => m.UserName) %>
            </li>
            <li>
                <%= Html.LabelFor(m => m.Password) %>
            </li>
            <li>
                <%= Html.PasswordFor(m => m.Password) %>
                <%= Html.ValidationMessageFor(m => m.Password) %>
            </li>
            <li>
                <%= Html.CheckBoxFor(m => m.RememberMe) %>
                <%= Html.LabelFor(m => m.RememberMe) %>
            </li>
            <li>
                <input type="submit" value="Log On" />
            </li>
        </ul>
        <% } %>
    </div>
    <link href="/Content/FormLayout.css" rel="stylesheet" type="text/css" />
</asp:Content>