<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Status
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <% using (Html.BeginForm())
           { %>
        <ul class="ul">
            <li>
                <h2>
                    Error</h2>
            </li>
            <li><span class="error">
                <h3>
        <%= ViewData["Status"].ToString() %></h3>
            </span></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>