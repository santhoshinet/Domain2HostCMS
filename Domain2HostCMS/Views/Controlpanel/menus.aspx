<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Content/FormLayout.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <%
            var menus = (List<Domain2HostCMSDL.Menu>)ViewData["menus"];
            int index = 1; %>
        <% if (menus.Count > 0)
           {
        %>
        <form method="post" action="#">
        <ul class="ul">
            <li>
                <h2>
                    List of menu.</h2>
            </li>
            <li>
                <table id="Cart_Table">
                    <thead>
                        <tr>
                            <th style="width: 20px">
                                Sno
                            </th>
                            <th style="width: 150px">
                                Name
                            </th>
                            <th style="width: 250px">
                                Page name
                            </th>
                            <th style="width: 150px">
                                Parent
                            </th>
                            <th style="width: 200px" colspan="2">
                                Actions
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <%
                            foreach (var menu in menus)
                            {%>
                        <tr id='<%= menu.Id %>'>
                            <td>
                                <%=index++%>
                            </td>
                            <td>
                                <%=menu.Name%>
                            </td>
                            <td>
                                <%=menu.Page.Name%>
                            </td>
                            <td>
                                <%
                                    var parent = menu.Parent;
                                    if (parent != null)
                                    {%>
                                <%= parent.Name%>
                                <%
                                    }
                                    else
                                    {%>
                                ---Root---
                                <%
                                    }%>
                            </td>
                            <td>
                                <span class="edit_button">
                                    <img src="/Images/edit.gif" />
                                    <a href="/editmenu/<%= menu.Id %>" class="ViewProfile">edit</a> </span>
                            </td>
                            <td>
                                <span class="delete_button">
                                    <img src="/Images/ico-delete.gif" />
                                    <a href="/deletemenu/<%= menu.Id %>" class="ViewProfile">delete</a></span>
                            </td>
                        </tr>
                        <%
                            }%>
                    </tbody>
                </table>
            </li>
            <li>
                <input type="button" value="Add menu" onclick="window.location = '/controlpanel/addmenu';" />
                <input type="button" value="Home" onclick="window.location = '/controlpanel';" />
            </li>
        </ul>
        </form>
        <%
            }
           else
           {%>
        <form method="post" action="">
        <ul class="ul">
            <li>
                <h2>
                    There is no menu added yet.</h2>
            </li>
            <li>
                <input type="button" value="Add new menu" onclick="window.location = '/controlpanel/addmenu';" />
                <input type="button" value="Home" onclick="window.location = '/controlpanel';" />
            </li>
        </ul>
        </form>
        <%}%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitle" runat="server">
    List of Menu
</asp:Content>