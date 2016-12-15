using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Invoice.Site.Models.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Message { get; set; }
        public IEnumerable<PostCategoryViewModel> Categories { get; set; }
        public string ImageBaseStr { get; set; }
        public bool IsImportant { get; set; }
    }
}