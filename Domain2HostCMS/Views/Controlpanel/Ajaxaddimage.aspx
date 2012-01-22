<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Domain2HostCMS.Models.ImageModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/Content/Ajaxaddimage.css" rel="Stylesheet" type="text/css" />
    <link href="/Content/FormLayout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="loginContent">
        <% using (Html.BeginForm("Ajaxaddimage", "Controlpanel", FormMethod.Post, new { enctype = "multipart/form-data" }))
           {%>
        <ul class="ul">
            <li>
                <h2>
                    Add Image</h2>
            </li>
            <li>
                <%=Html.ValidationSummary(true,"Unable to add product. Please correct the errors and try again.")%>
            </li>
            <li>
                <label>
                    File</label>
            </li>
            <li>
                <input type="file" name="image" />
                <%=Html.ValidationMessageFor(m => m.Image)%>
            </li>
            <li>
                <%= ViewData["Status"].ToString() %>
            </li>
            <li>
                <input type="submit" value="Add images" />
                <%= Html.HiddenFor(m => m.Id) %>
            </li>
        </ul>
        <%
            }%>
    </div>
</body>
</html>