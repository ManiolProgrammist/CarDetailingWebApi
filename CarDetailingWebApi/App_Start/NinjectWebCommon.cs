[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CarDetailingWebApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CarDetailingWebApi.App_Start.NinjectWebCommon), "Stop")]

namespace CarDetailingWebApi.App_Start
{
    using System;
    using System.Web;
    using CarDetailingWebApi.Models;
    using CarDetailingWebApi.Models.db;
    using CarDetailingWebApi.Models.Repositories.UserRightsRepo;
    using CarDetailingWebApi.Models.Repositories.OrdersTemplateRepositoryF;
    using CarDetailingWebApi.Models.Services.OrderTemplateServicesF;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using CarDetailingWebApi.Models.Services.UserRightsService;

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
            kernel.Bind<IUsersRepository>().To<UsersRepository>();
            kernel.Bind<IUserService>().To<UserServices>();

            kernel.Bind<IOrdersRepository>().To<OrdersRepository>();
            kernel.Bind<IOrderServices>().To<OrderServices>();

            kernel.Bind<IOrdersTemplateRepository>().To<OrdersTemplateRepository>();
            kernel.Bind<IOrdersTemplateService>().To<OrdersTemplateService>();

            kernel.Bind<IUserRightsRepository>().To<UserRightsRepository>();
            kernel.Bind<IUserRightsService>().To<UserRightsService>();


        }        
        public static IKernel GetKernel()
        {
            return bootstrapper.Kernel;
        }
    }
}
