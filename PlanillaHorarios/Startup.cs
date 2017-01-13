using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlanillaHorarios.Startup))]
namespace PlanillaHorarios
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
