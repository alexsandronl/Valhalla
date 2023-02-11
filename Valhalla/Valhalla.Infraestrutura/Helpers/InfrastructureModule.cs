using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Caching.Memory;
using Valhalla.Domain.Exceptions;
using Valhalla.Domain.Interfaces;
using Valhalla.Infraestrutura.Servicos;

namespace Valhalla.Infraestrutura.Helpers
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
                .AsImplementedInterfaces()
                .AsSelf().InstancePerLifetimeScope();

            //builder.RegisterType<MemoryCache>()
            //     .WithParameter("optionsAccessor", new MemoryCacheOptions())
            //     .As<IMemoryCache>()
            //     .SingleInstance();

            builder.RegisterType<ServicoDatabase>().As<IServicoDatabase>().AsSelf().SingleInstance();
        }

     
    }

}
