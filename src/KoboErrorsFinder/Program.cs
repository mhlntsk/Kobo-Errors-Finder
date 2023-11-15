using KoboErrorFinder.Services;
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
            
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IPathReaderService, PathReaderService>();
            container.RegisterType<ISheetsService, SheetsService>();
            container.RegisterType<IHeadersService, HeadersService>();
            container.RegisterType<IConductorService, ConductorService>(new ContainerControlledLifetimeManager());

            container.RegisterType<AmbulanceExecutor, AmbulanceExecutor>();
            container.RegisterType<BasicExecutor, BasicExecutor>();
            container.RegisterType<EDExecutor, EDExecutor>();
            container.RegisterType<MHGroupExecutor, MHGroupExecutor>();
            container.RegisterType<OpdExecutor, OpdExecutor>();
            container.RegisterType<OT_ICUExecutor, OT_ICUExecutor>();

            container.RegisterType<IMapper<AmbulanceMapper>, AmbulanceMapper>();
            container.RegisterType<IMapper<BasicMapper>, BasicMapper>();
            container.RegisterType<IMapper<EDMapper>, EDMapper>();
            container.RegisterType<IMapper<MHGroupMapper>, MHGroupMapper>();
            container.RegisterType<IMapper<OT_ICUMapper>, OT_ICUMapper>();

            container.RegisterType<IOperator<AmbulanceOperator>, AmbulanceOperator>();
            container.RegisterType<IOperator<BasicOperator>, BasicOperator>();
            container.RegisterType<IOperator<MHGroupOperator>, MHGroupOperator>();

            container.RegisterType<IPrinter<AmbulancePrinter>, AmbulancePrinter>();
            container.RegisterType<IPrinter<BasicPrinter>, BasicPrinter>();
            container.RegisterType<IPrinter<MHGroupPrinter>, MHGroupPrinter>();
            container.RegisterType<IPrinter<OpdPrinter>, OpdPrinter>();


            var conductorService = container.Resolve<IConductorService>();
            conductorService.RegisterTable("Ambulance register- Dnipro E...", container.Resolve<AmbulanceExecutor>());
            conductorService.RegisterTable("MH_Group Session_Dnipro_East", container.Resolve<MHGroupExecutor>());
            conductorService.RegisterTable("MH 1st consultation_Dnipro East", container.Resolve<BasicExecutor>());
            conductorService.RegisterTable("MH_Follow Up consultation_Dn...", container.Resolve<BasicExecutor>());
            conductorService.RegisterTable("OPD_NCD register_Dnipro East", container.Resolve<OpdExecutor>());
            conductorService.RegisterTable("ED register_Dnipro East", container.Resolve<EDExecutor>());
            conductorService.RegisterTable("SRH Register_Dnipro East", container.Resolve<BasicExecutor>());
            conductorService.RegisterTable("ICU_Dnipro East", container.Resolve<OT_ICUExecutor>());
            conductorService.RegisterTable("OT_Dnipro East", container.Resolve<OT_ICUExecutor>());
           


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