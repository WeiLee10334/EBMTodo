using EBMTodo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EBMTodo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //TODO 發布時資料庫預設為NULL
            Database.SetInitializer<ApplicationDbContext>(null);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;
            Response.StatusCode = httpException.GetHttpCode();
            Response.Clear();
            Server.ClearError();
            //LogInfo(HttpContext.Current.Request.Path.Trim().Split('/')[0] + "," + HttpContext.Current.Request.Path.Trim().Split('/')[1]);
            string basePath = HttpContext.Current.Request.Path.Trim().Split('/')[1].ToLower();
            if (basePath == "ngapp")
            {
                if (httpException != null)
                {
                    var httpContext = HttpContext.Current;

                    httpContext.RewritePath("/Errors/InternalError", false);
                    // MVC 3 running on IIS 7+
                    if (HttpRuntime.UsingIntegratedPipeline)
                    {
                        switch (Response.StatusCode)
                        {
                            case 404:
                                Response.WriteFile($"~/ngApp/index.html");
                                Response.StatusCode = 200;
                                //httpContext.Server.TransferRequest("/ngapp", true);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (Response.StatusCode)
                        {
                            case 404:
                                Response.WriteFile($"~/ngApp/index.html");
                                Response.StatusCode = 200;
                                break;
                            default:
                                break;
                        }

                        IHttpHandler httpHandler = new MvcHttpHandler();
                        httpHandler.ProcessRequest(httpContext);
                    }
                }
            }

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
