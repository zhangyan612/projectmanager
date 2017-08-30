using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using ProjectManager.DAL.Repository;
//using SocialGoal.Data.Infrastructure;
//using SocialGoal.Service;
//using SocialGoal.Mappings;
//using SocialGoal.Web.Core.Authentication;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectManager.Models;
using ProjectManager.DAL;
using Microsoft.AspNet.Identity;


namespace ProjectManager
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            //Configure AutoMapper
            AutoMapperConfiguration.Configure();
        }
        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerHttpRequest();
                
             //builder.RegisterAssemblyTypes(typeof(DefaultFormsAuthentication).Assembly)
             //.Where(t => t.Name.EndsWith("Authentication"))
             //.AsImplementedInterfaces().InstancePerHttpRequest();

            builder.Register(c => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new PMContext())))
                .As<UserManager<ApplicationUser>>().InstancePerHttpRequest();

            builder.RegisterFilterProvider();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}