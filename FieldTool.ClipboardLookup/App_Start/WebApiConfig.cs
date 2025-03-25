using FieldTool.ClipboardLookup.Controllers;
using FieldTool.ClipboardLookup.Controllers.CB;
using FieldTool.ClipboardLookup.Controllers.EN;
using FieldTool.ClipboardLookup.DAL;
using FieldTool.ClipboardLookup.DAL.CB;
using FieldTool.ClipboardLookup.DAL.EN;
using FieldTool.ClipboardLookup.Helpers;
using FieldTool.ClipboardLookup.Logging;
using FieldTool.ClipboardLookup.Models;
using FieldTool.ClipboardLookup.Models.EN;
using FieldTool.ClipboardLookup.Resolver;
using FieldTool.Constants.Logging;
using FieldTool.Entity;
using Microsoft.Practices.Unity;
using System.Web.Http;

namespace FieldTool.ClipboardLookup
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IClipBoardUpload, ClipBoardUpload>(new InjectionFactory(x => new ClipBoardUpload()));
            container.RegisterType<IAzureStorageService, AzureStorageService>(new InjectionFactory(x => new AzureStorageService()));
            container.RegisterType<ILogger, Logger>(new HierarchicalLifetimeManager());

            // Repositories & controllers
            container.RegisterType<IRepository<Company, CompanyDTO>, CompanyRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepository<AuditProject, AuditProjectDTO>, AuditProjectRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepository<Retrofit, RetrofitDTO>, RetrofitRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<CompaniesController>(new HierarchicalLifetimeManager());

            // CB Repositories & controllers
            container.RegisterType<ICbRepository<AuditUploadBackup>, ProjectUploadRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICbRepository<DiUploadBackup>, DiUploadRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ProjectController>(new HierarchicalLifetimeManager());
            container.RegisterType<DiController>(new HierarchicalLifetimeManager());

            // EN Repositories & controllers
            container.RegisterType<IRepository<Company, AccountInfo>, AccountRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepository<Building, ProjectInfo>, ProjectRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepository<Building, PropertyInfo>, PropertyRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<AccountsController>(new HierarchicalLifetimeManager());
            container.RegisterType<ProjectsController>(new HierarchicalLifetimeManager());
            container.RegisterType<PropertiesController>(new HierarchicalLifetimeManager());

            // register instance of EmailHelper that will be used as a singleton
            container.RegisterInstance<IEmailHelper>(new EmailHelper(System.Web.Hosting.HostingEnvironment.MapPath("~/EmailTemplates")));

            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new UnhandledExceptionFilter(new Logger()));

            // avoid circular reference loops in JSON results
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // configure properties that will be ignored in JSON serialization
            var jsonResolver = new IgnorableContractResolver();
            jsonResolver.Ignore<Building>(x => x.AuditProject);
            jsonResolver.Ignore<Building>(x => x.Recommendations);
            jsonResolver.Ignore<Building>(x => x.Retrofits);
            jsonResolver.Ignore<Contact>(x => x.Company);
            jsonResolver.Ignore<Recommendation>(x => x.AuditProject);
            jsonResolver.Ignore<Recommendation>(x => x.Building);
            jsonResolver.Ignore<AccountInfo>(x => x.BuildingIds);
            jsonResolver.Ignore<PropertyInfo>(x => x.BuildingType);
            jsonResolver.Ignore<PropertyInfo>(x => x.Projects);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = jsonResolver;
        }
    }
}