using System.Web.Mvc;

namespace EBMTodo.Areas.Line
{
    public class LineAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Line";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.MapRoute(
            //    "Line_default",
            //    "Line/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);

            context.MapRoute(
                "LineAPI_default",
                "api/Line/{controller}/{id}",
                defaults:  new { id = UrlParameter.Optional }
            );

        }
    }
}