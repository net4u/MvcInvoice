﻿using System;
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

namespace Invoice.Site.Controllers
{
    public partial class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public HomeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
        public virtual ActionResult Create(PostEditModel post)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Where(e => e.Value.Errors.Count > 0).Select(a => a.Key) });
            }

            try
            {
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

                return Json(new { success = true, post = model });
            }
            catch (Exception e)
            {
                return Json(new { success = false, errors = string.Format("Unhandled exception on server side: {0}", e.Message) });
            }

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
            return Json(models, JsonRequestBehavior.AllowGet);
        }
    }
}