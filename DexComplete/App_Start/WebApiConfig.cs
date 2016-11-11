using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace DexComplete
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			var container = new UnityContainer();
			container.RegisterType<Repository.Abilities, Repository.Abilities>(new HierarchicalLifetimeManager());
			container.RegisterType<Repository.Berries, Repository.Berries>(new HierarchicalLifetimeManager());
			container.RegisterType<Repository.EggGroups, Repository.EggGroups>(new HierarchicalLifetimeManager());
			container.RegisterType<Repository.Games, Repository.Games>(new HierarchicalLifetimeManager());
			container.RegisterType<Repository.Generations, Repository.Generations>(new HierarchicalLifetimeManager());
			container.RegisterType<Repository.Pokedexes, Repository.Pokedexes>(new HierarchicalLifetimeManager());
			container.RegisterType<Repository.TMs, Repository.TMs>(new HierarchicalLifetimeManager());
			container.RegisterType<Repository.Updates, Repository.Updates>(new HierarchicalLifetimeManager());
			container.RegisterType<Repository.Users, Repository.Users>(new HierarchicalLifetimeManager());

			container.RegisterType<Services.AbilityService, Services.AbilityService>(new HierarchicalLifetimeManager());
			container.RegisterType<Services.BerryService, Services.BerryService>(new HierarchicalLifetimeManager());
			container.RegisterType<Services.EggGroupService, Services.EggGroupService>(new HierarchicalLifetimeManager());
			container.RegisterType<Services.EmailService, Services.EmailService>(new HierarchicalLifetimeManager());
			container.RegisterType<Services.GameService, Services.GameService>(new HierarchicalLifetimeManager());
			container.RegisterType<Services.PokedexService, Services.PokedexService>(new HierarchicalLifetimeManager());
			container.RegisterType<Services.ServerService, Services.ServerService>(new HierarchicalLifetimeManager());
			container.RegisterType<Services.TmService, Services.TmService>(new HierarchicalLifetimeManager());
			container.RegisterType<Services.UpdatesService, Services.UpdatesService>(new HierarchicalLifetimeManager());
			container.RegisterType<Services.UserService, Services.UserService>(new HierarchicalLifetimeManager());


			container.RegisterType<Data.PokedexModel, Data.PokedexModel>(new HierarchicalLifetimeManager());


			config.DependencyResolver = new Utilities.UnityResolver(container);
            // Web API configuration and services
			config.Services.Replace(typeof(IExceptionHandler), new Code.OopsExceptionHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
