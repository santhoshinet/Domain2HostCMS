<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Content/FormLayout.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <form action="" method="post">
        <ul class="ul">
            <li>
                <h2>
                    Control panel</h2>
            </li>
            <li>
                <p>
                    Manage your site content.</p>
            </li>
            <li>
                <input type="button" value="Manage Images" onclick="window.location = '/controlpanel/Images';" />
                <input type="button" value="Manage Pages" onclick="window.location = '/controlpanel/Pages';" />
                <input type="button" value="Manage Menu" onclick="window.location = '/controlpanel/Menus';" />
            </li>
        </ul>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitle" runat="server">
    Control panel
</asp:Content>