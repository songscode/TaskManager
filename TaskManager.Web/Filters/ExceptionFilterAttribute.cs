using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManager.Common.Log;

namespace TaskManager.Web.Filters
{
    public class ExceptionFilterAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            var exception = filterContext.Exception;
            LogHelper.SystemLog.Error(string.Format("系统异常：{0}",exception.Message),exception);
            if (filterContext.Controller.ControllerContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.InternalServerError,
                    StringToISO_8859_1("内部异常："+exception.Message));
            }
            else
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error"
                };
            }
            filterContext.ExceptionHandled = true;
        }

        /// <summary>
        /// 转换为ISO_8859_1
        /// 根据 http 协议，StatusDescription 是写在 http header 中的，默认所有header是用iso-8859-1编码的，但是中文实际是用uft8编码。所以就出现了乱码问题。
        /// </summary>
        /// <param name="srcText"></param>
        /// <returns></returns>
        private string StringToISO_8859_1(string srcText)
        {
            string dst = "";
            char[] src = srcText.ToCharArray();
            for (int i = 0; i < src.Length; i++)
            {
                string str = @"&#" + (int)src[i] + ";";
                dst += str;
            }
            return dst;
        }
    }
}