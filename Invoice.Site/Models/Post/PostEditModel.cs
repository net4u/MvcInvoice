using Invoice.Site.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Invoice.Site.Models.Post
{
    public class PostEditModel
    {
        public int Id { get; set; }

        [DisplayName("Header *")]
        [Required(ErrorMessage = "Header is required")]
        [MaxLength(50, ErrorMessage="ZToo much characters, max: 50")]
        public string Header { get; set; }

        [DisplayName("Message *")]
        [Required(ErrorMessage = "Message is required")]
        [MaxLength(255, ErrorMessage = "Too much characters, max: 255")]
        public string Message { get; set; }

        [DisplayName("File *")]
        [Required(ErrorMessage = "File is required")]
        [ValidateFileType("jpg,png")]
        [MaxFileLength(1)]
        public HttpPostedFileBase File { get; set; }

        [DisplayName("Important")]
        public bool IsImportant { get; set; }

        public List<PostCategoryViewModel> Categories { get; set; }
        [DisplayName("Categories *")]
        [Required(ErrorMessage = "Category is required")]
        public IEnumerable<int> SelectedCategories { get; set; }
        //public List<PostCategoryViewModel> SelectedCategories { get; set; }
    }
}