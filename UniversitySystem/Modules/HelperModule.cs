using Autofac;
using University.API.Helpers;

namespace University.API.Modules
{
    public class HelperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JwtTokenHelper>()
                .As<IJwtTokenHelper>()
                .InstancePerLifetimeScope();
        }
    }
}
