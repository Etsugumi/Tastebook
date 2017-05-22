using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tastebook.Startup))]
namespace Tastebook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
