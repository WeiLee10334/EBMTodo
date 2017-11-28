using Microsoft.Owin;
using Owin;
using System;
using System.IO;

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
                LogInfo(context.Response.StatusCode.ToString() + ":" + context.Request.Path.Value);
                //if (context.Response.StatusCode == 404)
                //{
                //    context.Request.Path = PathString.FromUriComponent("/ngapp");
                //    context.Response.StatusCode = 200;
                //    await next();
                //}
            });
            ConfigureAuth(app);
        }
        private void LogInfo(string pMessage)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Logfiles/");
            string tFilePath = path + "Log" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            FileStream fs;
            if (!Directory.Exists(path))
            {
                //新增資料夾
                Directory.CreateDirectory(path);
            }
            if (!System.IO.File.Exists(tFilePath))
            {
                fs = new FileStream(tFilePath, FileMode.Create);
            }
            else
            {
                fs = new FileStream(tFilePath, FileMode.Append);
            }
            pMessage = DateTime.Now.ToString("HH:mm:ss    ") + pMessage;
            StreamWriter sw = new StreamWriter(fs);
            //開始寫入
            sw.WriteLine(pMessage);
            //清空緩衝區
            sw.Flush();
            //關閉流
            sw.Close();
            fs.Close();

        }
    }
}
