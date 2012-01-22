using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain2HostCMS.Models
{
    public class PageModel
    {
        [Required]
        [DisplayName("Page Title")]
        public string PageTitle { get; set; }

        [Required]
        [DataType(DataType.Html)]
        [DisplayName("Content")]
        public string Content { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("ID")]
        public string Id { get; set; }
    }

    public class MenuModel
    {
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Menu name")]
        public string MenuName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("ID")]
        public string Id { get; set; }
    }
}