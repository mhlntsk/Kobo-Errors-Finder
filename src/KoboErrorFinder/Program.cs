using KoboErrorFinder.Extensions;
using KoboErrorFinder.Extensions.Services.Application;
using KoboErrorFinder.Extensions.Services.Domain;
using KoboErrorFinder.TablesExtensions.Executors;
using KoboErrorFinder.TablesExtensions.Mappers;
using KoboErrorFinder.TablesExtensions.Operators;
using KoboErrorFinder.TablesExtensions.Printers;
using Unity;
using Unity.Lifetime;

namespace KoboErrorFinder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            IUnityContainer container = UnityConfig.RegisterComponents();
            container.RegisterType<IPathReaderService, PathReaderService>();
            container.RegisterType<ISheetsService, SheetsService>();
            container.RegisterType<IHeadersService, HeadersService>();
            container.RegisterType<IConductorService, ConductorService>(new ContainerControlledLifetimeManager());
            container.RegisterType<AmbulanceExecutor, AmbulanceExecutor>();
            container.RegisterType<IAmbulanceMapper, AmbulanceMapper>();
            container.RegisterType<IAmbulanceOperator, AmbulanceOperator>();
            container.RegisterType<IAmbulancePrinter, AmbulancePrinter>();
            container.RegisterType<MHGroupExecutor, MHGroupExecutor>();
            container.RegisterType<IMHGroupMapper, MHGroupMapper>();
            container.RegisterType<IMHGroupOperator, MHGroupOperator>();
            container.RegisterType<IMHGroupPrinter, MHGroupPrinter>();
            container.RegisterType<BasicExecutor, BasicExecutor>();
            container.RegisterType<IBasicMapper, BasicMapper>();
            container.RegisterType<IBasicOperator, BasicOperator>();
            container.RegisterType<IBasicPrinter, BasicPrinter>();


            var conductorService = container.Resolve<IConductorService>();
            conductorService.RegisterTable("Ambulance register- Dnipro E...", container.Resolve<AmbulanceExecutor>());
            conductorService.RegisterTable("MH_Group Session_Dnipro_East", container.Resolve<MHGroupExecutor>());
            conductorService.RegisterTable("MH 1st consultation_Dnipro East", container.Resolve<BasicExecutor>());
            conductorService.RegisterTable("MH_Follow Up consultation_Dn...", container.Resolve<BasicExecutor>());
            conductorService.RegisterTable("OPD/NCD register_Dnipro East", container.Resolve<BasicExecutor>());
            conductorService.RegisterTable("ED register_Dnipro East", container.Resolve<BasicExecutor>());
            conductorService.RegisterTable("SRH Register_Dnipro East", container.Resolve<BasicExecutor>());
            conductorService.RegisterTable("ICU_Dnipro East", container.Resolve<BasicExecutor>());
            conductorService.RegisterTable("OT_Dnipro East", container.Resolve<BasicExecutor>());
           

            StartApplication(container.Resolve<IPathReaderService>(), container.Resolve<ISheetsService>());
        }

        public static void StartApplication(IPathReaderService pathReaderService, ISheetsService sheetsService)
        {
            while (true)
            {
                string filePath = pathReaderService.GetPath();

                if (File.Exists(filePath))
                {
                    sheetsService.CheckAllSheets(filePath);
                }
                else
                {
                    Console.WriteLine("File doesn`t exist");
                }
            }
        }
    }
}