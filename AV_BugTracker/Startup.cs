using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AV_BugTracker.Startup))]
namespace AV_BugTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
