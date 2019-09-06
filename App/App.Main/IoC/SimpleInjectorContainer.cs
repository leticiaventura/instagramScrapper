using App.Application.Features.Places;
using App.Application.Features.Users;
using App.Domain.Interfaces.Places;
using App.Domain.Interfaces.Users;
using App.Infrastructure.Features.Places;
using App.Infrastructure.Features.Users;
using App.Infrastructure.Scraping;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Main.IoC
{
    public class SimpleInjectorContainer
    {
        public static Container Initialize()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            RegisterServices(container);

            container.Verify();

            return container;
        }

        public static void RegisterServices(Container container)
        {
            container.Register<IVenueService, VenueService>();
            container.Register<IVenueRepository, VenueRepository>();

            container.Register<ILocationService, LocationService>();
            container.Register<ILocationRepository, LocationRepository>();

            container.Register<ICategoryService, CategoryService>();
            container.Register<ICategoryRepository, CategoryRepository>();

            container.Register<IInstagramUserService, InstagramUserService>();
            container.Register<IInstagramUserRepository, InstagramUserRepository>();

            container.Register<InstagramScraper>();
        }
    }
}
