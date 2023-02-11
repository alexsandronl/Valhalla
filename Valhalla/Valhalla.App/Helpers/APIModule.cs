using Autofac;
using Microsoft.Extensions.Caching.Memory;
using Valhalla.App;
using Valhalla.Domain.Exceptions;

namespace PainelDeSenha.Helpers
{
    public class APIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ApiException).Assembly)
                .AsImplementedInterfaces()
                .AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<MemoryCache>()
                 .WithParameter("optionsAccessor", new MemoryCacheOptions())
                 .As<IMemoryCache>()
                 .SingleInstance();

            builder.RegisterType<ParametrosGlobais>().SingleInstance();
        }

    }

}
