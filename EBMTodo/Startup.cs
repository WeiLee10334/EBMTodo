using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EBMTodo.Startup))]
namespace EBMTodo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.Use(async (context, next) =>
            {
                await next();
            });
            ConfigureAuth(app);
        }
     
    }
}
