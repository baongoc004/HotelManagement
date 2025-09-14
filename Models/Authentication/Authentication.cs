using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HotelManagement.Models.Authentication
{
    public class Authentication : ActionFilterAttribute
    {
        //OnActionExecuting sẽ gọi trước khi hàm action mà nó được kèm theo
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //nếu tài khoản user và admin và nhanvien đều null thì đăng nhập
            if (context.HttpContext.Session.GetString("UserName") == null 
                && context.HttpContext.Session.GetString("admin") == null 
                && context.HttpContext.Session.GetString("nhanvien") == null)
            {
                // Nếu cả ba session đều null thì chuyển hướng người dùng về trang đăng nhập
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller","Login" },
                    { "action","Index" }
                });
            }
            // Nếu có ít nhất một trong ba session (UserName, admin, hoặc nhanvien) tồn tại
            // thì không làm gì và để action tiếp tục thực thi
        }
    }
}
