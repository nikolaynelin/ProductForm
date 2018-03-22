using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Valtech.ProductForm.Startup))]
namespace Valtech.ProductForm
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
