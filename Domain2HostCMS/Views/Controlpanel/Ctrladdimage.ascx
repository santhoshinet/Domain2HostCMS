<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Domain2HostCMS.Models.ImageModel>" %>
<div class="loginContent">
    <% using (Html.BeginForm("AddImage", "Controlpanel", FormMethod.Post, new { enctype = "multipart/form-data" }))
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
            <input type="submit" value="Add images" />
            <input type="button" value="Cancel" onclick="window.location = '/controlpanel/Images';" />
            <%= Html.HiddenFor(m => m.Id) %>
        </li>
    </ul>
    <%
        }%>
</div>