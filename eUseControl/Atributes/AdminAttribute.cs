using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Controllers;
using eUseControl.Domain.Enums;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eUseControl.Attributes
{
     public class AdminAttribute : ActionFilterAttribute
     {
          public override void OnActionExecuting(ActionExecutingContext filterContext)
          {
               var baseController = filterContext.Controller as BaseController;
               if (baseController != null && !baseController.IsAdmin())
               {
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                    return;
               }

               base.OnActionExecuting(filterContext);
          }
     }
}
