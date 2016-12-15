using AutoMapper;
using Invoice.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Invoice.Site.Helpers
{
    public static class AutoMapperWebConfig
    {
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Company, BankAccount>();
            });
        }
    }
}