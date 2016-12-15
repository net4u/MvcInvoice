using AutoMapper;
using Invoice.Database;
using Invoice.Site.Models.Post;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Invoice.Site.Helpers
{
    public class AutoMapperProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Post, PostViewModel>()
                .ForMember(a => a.ImageBaseStr, o => o.ResolveUsing(a => 
                    {
                        return System.Convert.ToBase64String(a.Image, 0, a.Image.Length);
                    }))
                .ForMember(a => a.Categories, o => o.MapFrom(a => a.PostCategory_SDIC));
            CreateMap<PostEditModel, Post>()
                .ForMember(a => a.Image, o => o.MapFrom(a => a.File))
                .ForMember(a => a.PostCategory_SDIC, o => o.Ignore());
            CreateMap<HttpPostedFileBase, byte[]>().ConstructUsing(a => 
                {
                    MemoryStream target = new MemoryStream();
                    a.InputStream.CopyTo(target);
                    return target.ToArray();
                });
            CreateMap<PostCategory_SDIC, PostCategoryViewModel>();
        }

    }
}