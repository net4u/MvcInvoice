using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Invoice.Database.Context;
using Invoice.Site.Models.Toolbar;
using Invoice.Site.Helpers;
using Invoice.Definitions;
using Invoice.Site.Models.Post;
using System.IO;
using System.Net;
using AutoMapper;
using Invoice.Database;
using System.Data.Entity;
using Invoice.Site.Attributes;
using Ninject.Extensions.Logging;

namespace Invoice.Site.Controllers
{
    [LogError(View = Consts.ErrorViewName.Error)]
    public partial class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ILogger _logger;

        public HomeController(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public virtual ActionResult Index()
        {
            var post = new PostEditModel();
            var categories = _unitOfWork.PostCategorySdicRepository.GetAll();
            post.Categories = _mapper.Map<List<PostCategoryViewModel>>(categories);

            return View(post);
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [AjaxOnly]
        public virtual ActionResult Create(PostEditModel post)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Where(e => e.Value.Errors.Count > 0).Select(a => a.Key) });
            }

                var dbPost = _mapper.Map<Post>(post);
                var categories = _unitOfWork.PostCategorySdicRepository.GetAll();
                foreach (var id in post.SelectedCategories)
                {
                    dbPost.PostCategory_SDIC
                          .Add(categories.First(a => a.Id == id));
                }
                _unitOfWork.PostRepository.Add(dbPost);

                _unitOfWork.Commit();

                var model = _mapper.Map<PostViewModel>(dbPost);
                model.Categories = _mapper.Map<List<PostCategoryViewModel>>(categories);

                return Json(new { success = true, data = model });

        }

        [AjaxOnly]
        public virtual JsonResult GetPosts(int pageIndex, int pageSize)
        {
            var dbPosts = _unitOfWork.PostRepository
                                     .AsQueryable()
                                     .Include(e => e.PostCategory_SDIC)
                                     .OrderBy(e => e.Id)
                                     .Skip(pageIndex * pageSize)
                                     .Take(pageSize)
                                     .ToList<Post>();
            var models = _mapper.Map<List<PostViewModel>>(dbPosts);
            return Json(new { success = true, data = models }, JsonRequestBehavior.AllowGet);
        }
    }
}