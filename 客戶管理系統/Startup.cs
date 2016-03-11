using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(客戶管理系統.Startup))]
namespace 客戶管理系統
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
