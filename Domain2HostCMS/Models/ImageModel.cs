using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Domain2HostCMS.Models
{
    public class ImageModel
    {
        [Required]
        [DisplayName("Image")]
        public HttpPostedFileBase Image { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("ID")]
        public string Id { get; set; }
    }
}