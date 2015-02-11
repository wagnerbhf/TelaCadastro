using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cadastro.Startup))]
namespace Cadastro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
