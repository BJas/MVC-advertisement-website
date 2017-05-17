using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcKrisha.Startup))]
namespace MvcKrisha
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
