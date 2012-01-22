<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Domain2HostCMS.Models.MenuModel>" %>

<%@ Import Namespace="Domain2HostCMSDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Content/FormLayout.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <% using (Html.BeginForm("EditMenu", "Controlpanel", FormMethod.Post, new { enctype = "multipart/form-data" }))
           {%>
        <ul class="ul">
            <li>
                <h2>
                    Edit Menu</h2>
            </li>
            <li>
                <%=Html.ValidationSummary(true,"Unable to edit menu. Please correct the errors and try again.")%>
            </li>
            <li>
                <label>
                    Name</label>
            </li>
            <li>
                <%=Html.TextBoxFor(m => m.MenuName)%>
                <%=Html.ValidationMessageFor(m => m.MenuName)%>
            </li>
            <li>
                <label>
                    Parent Menu
                </label>
            </li>
            <li>
                <select id="CmbParentMenu" name="CmbParentMenu">
                    <option value="Root" title="Root">--Root--</option>
                    <%
                        var menus = (List<Domain2HostCMSDL.Menu>)ViewData["menus"];%>
                    <% foreach (var menu in menus)
                       {
                           if (ViewData["ParentMenuId"].ToString() == menu.Id)
                           {
                    %>
                    <option value="<%=menu.Id%>" title="<%=menu.Name%>" selected="selected">
                        <%=menu.Name%>
                    </option>
                    <%
                        }
                           else
                           {%>
                    <option value="<%=menu.Id%>" title="<%=menu.Name%>">
                        <%=menu.Name%>
                    </option>
                    <%
                        }
                       }%>
                </select>
            </li>
            <li>
                <label>
                    Page</label>
            </li>
            <li>
                <select id="CmbPages" name="CmbPages">
                    <option value="Select receiver" title="Select receiver">--Select page--</option>
                    <%
                        var contentPages = (List<ContentPage>)ViewData["ContentPages"];%>
                    <% foreach (var contentPage in contentPages)
                       {
                           if (ViewData["Pagename"].ToString() == contentPage.Name)
                           {
                    %>
                    <option value="<%=contentPage.Id%>" title="<%=contentPage.Name%>" selected="selected" />
                    <%
                        }
                           else
                           {%>
                    <option value="<%=contentPage.Id%>" title="<%=contentPage.Name%>" />
                    <%
                        }%>
                    <%= contentPage.Name %>
                    <% } %>
                </select>
            </li>
            <li>
                <input type="submit" value="Update Menu" />
                <input type="button" value="Cancel" onclick="window.location = '/controlpanel/Menus';" />
                <%= Html.HiddenFor(m => m.Id) %>
            </li>
        </ul>
        <%
            }%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitle" runat="server">
    Edit Menu
</asp:Content>