using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Lifetime;
using Unity;
using KoboErrorFinder.Extensions.Services.Application;

namespace KoboErrorFinder.Extensions
{
    public static class UnityConfig
    {
        public static IUnityContainer RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IPathReaderService, PathReaderService>(new HierarchicalLifetimeManager());


            return container;
        }
    }
}
