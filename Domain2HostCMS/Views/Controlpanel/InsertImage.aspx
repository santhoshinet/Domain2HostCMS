<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Domain2HostCMS.Models.ImageModel>" %>

<%@ Import Namespace="Domain2HostCMSDL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Insert Image</title>
    <style type="text/css">
        .ImageContainer
        {
            width: 620px;
            background-color: #222;
        }
        .ImageContainer img
        {
            float: left;
            width: 150px;
            height: 150px;
            margin: 5px;
            border: 1px solid #aaa;
        }
        .ImageContainer img:hover
        {
            border: 1px solid #333;
        }
    </style>
</head>
<body>
    <div class="ImageContainer">
        <%
            var fileList = (List<File>)ViewData["fileList"];
        %>
        <%
            if (fileList.Count > 0)
            {
                foreach (var file in fileList)
                {
        %>
        <img alt="" class="product_image" src="/Photo/<%=file.Id%>" title="<%=file.Filename%>" />
        <%
            }
            }
            else
            {%>
        There is no images uploaded still.
        <%
            }
        %>
    </div>

    <script language="javascript" type="text/javascript">
        $().ready(function() {
            $('.ImageContainer img').click(function() {
                closeFancyBox();
                $.fancybox.close();
                //var range = $($('iframe')[0].contentDocument.body).getSelection();
                //$($('iframe')[0].contentDocument.body).insertAtCaret("<img src='" + $(this).attr('src') + "' />");
                var element = document.createElement('img');
                element.src = $(this).attr('src');
                $($('iframe')[0].contentDocument.body).append(element);
                //$($('iframe')[0].contentDocument.body).insertAtCaret("<img src='" + $(this).attr('src') + "' />");
            });
        });
        $.fn.extend({
            insertAtCaret: function(myValue) {
                if (document.selection) {
                    this.focus();
                    sel = document.selection.createRange();
                    sel.text = myValue;
                    this.focus();
                }
                else if (this.selectionStart || this.selectionStart == '0') {
                    var startPos = this.selectionStart;
                    var endPos = this.selectionEnd;
                    var scrollTop = this.scrollTop;
                    this.value = this.value.substring(0, startPos) + myValue + this.value.substring(endPos, this.value.length);
                    this.focus();
                    this.selectionStart = startPos + myValue.length;
                    this.selectionEnd = startPos + myValue.length;
                    this.scrollTop = scrollTop;
                } else {
                    this.value += myValue;
                    this.focus();
                }
            }
        });
        function closeFancyBox() {
            $('#fancybox-overlay').trigger('click');
            $('#fancybox-outer').trigger('click');
            $('.fancybox-bg').trigger('click');
        }
    </script>

</body>
</html>