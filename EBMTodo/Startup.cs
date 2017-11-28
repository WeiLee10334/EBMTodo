using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EBMTodo.Startup))]
namespace EBMTodo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
            //app.Use(async (context, next) =>
            //{
            //    await next();
            //    if (context.Response.StatusCode == 404)
            //    {
            //        context.Request.Path = PathString.FromUriComponent("/ngapp");
            //        context.Response.StatusCode = 200;
            //        await next();
            //    }
            //});
            ConfigureAuth(app);
        }
    }
}
