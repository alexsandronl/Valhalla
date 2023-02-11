using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Valhalla.Domain.Exceptions;
using Valhalla.Domain.Interfaces;

namespace Valhalla.Domain.Helpers
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(DomainException).Assembly)
                .AsImplementedInterfaces()
                .AsSelf().InstancePerLifetimeScope();
        }

     
    }

}
