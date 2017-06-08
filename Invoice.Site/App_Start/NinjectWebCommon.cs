using Invoice.Database.Context;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Invoice.Site.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Invoice.Site.App_Start.NinjectWebCommon), "Stop")]

namespace Invoice.Site.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using AutoMapper;
    using Invoice.Site.Helpers;
    using Invoice.Definitions.Interfaces;
    using Ninject.Modules;
    using Ninject.Extensions.Logging.Log4net;
    using Ninject.Extensions.Logging;
    using Invoice.Service.Interfaces;
    using Invoice.Service.Services;
    using Invoice.Database.Interfaces;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            log4net.Config.XmlConfigurator.Configure();
            var settings = new NinjectSettings { LoadExtensions = false };

            //var kernel = new StandardKernel(settings, new INinjectModule[] { new Log4NetModule() });
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var mapperConfig = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>());
            mapperConfig.AssertConfigurationIsValid();

            kernel
                .Bind<IUnitOfWork>()
                .To<EfUnitOfWork>()
                .InRequestScope()
                .WithConstructorArgument("connectionString", 
                    "Data Source=.\\SQLEXPRESS;Initial Catalog=MvcInvoiceDb;user id=sa;password=fortuna;MultipleActiveResultSets=True");
            kernel
                .Bind<IContext>()
                .To<InvoiceDbContext>()
                .InRequestScope()
                .WithConstructorArgument("connectionString",
                    "Data Source=.\\SQLEXPRESS;Initial Catalog=MvcInvoiceDb;user id=sa;password=fortuna;MultipleActiveResultSets=True");
            kernel
                .Bind<IPostService>()
                .To<PostService>()
                .InRequestScope();
            kernel
                .Bind<IParameterService>()
                .To<ParameterService>()
                .InRequestScope();
            kernel
                .Bind<ICompanyService>()
                .To<CompanyService>()
                .InRequestScope();
            kernel
                .Bind<IMapper>()
                .ToConstant(mapperConfig.CreateMapper());

            kernel
                .Bind<ICurrencyFeedReader>()
                .To<CurrencyFeedReader>()
                .InRequestScope();

        }        
    }
}
