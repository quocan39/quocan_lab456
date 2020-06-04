using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nguyenquocan_lab456.Startup))]
namespace Nguyenquocan_lab456
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
