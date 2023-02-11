using Autofac;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Valhalla.Domain.Helpers;
using Valhalla.Infraestrutura.Helpers;

namespace PainelDeSenha.Helpers
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder AddAutofacRegistration(this ContainerBuilder builder)
        {
            builder.RegisterModule<APIModule>();
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<DomainModule>();

            return builder;
        }
    }
}
