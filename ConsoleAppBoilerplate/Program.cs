using System;
using Autofac;
using AutoMapper;
using NLog;

namespace ConsoleAppBoilerplate
{
    internal class Program
    {
        public static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private static IContainer Container { get; set; }

        private static void Main(string[] args)
        {
            // config auto mapper

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<string, int>().ConvertUsing(s => Convert.ToInt32(s));
                cfg.CreateMap<string, bool>().ConvertUsing(s => Convert.ToBoolean(s));
                cfg.CreateMap<string, DateTime>().ConvertUsing(s => Convert.ToDateTime(s));
            });

            // config autofac

            var builder = new ContainerBuilder();
            // builder.RegisterType<SampleDbContext>().As<ISampleDbContext>().InstancePerLifetimeScope(); // Keep it for later use
            builder.RegisterType<ConsoleBase>().As<IConsoleBase>();
            builder.Register(ctx => config).As<IConfigurationProvider>();
            builder.Register(ctx =>
                {
                    var scope = ctx.Resolve<ILifetimeScope>();
                    return new Mapper(
                        ctx.Resolve<IConfigurationProvider>(),
                        scope.Resolve);
                })
                .As<IMapper>()
                .InstancePerLifetimeScope();

            Container = builder.Build();

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var consoleBase = new ConsoleBase(scope.Resolve<IMapper>());
                    consoleBase.Start();
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Logger.Error(e);
                throw;
            }
        }
    }
}