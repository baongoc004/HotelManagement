using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HotelManagement.Models.Authentication
{
    // Lớp AdminAuthentication kế thừa từ ActionFilterAttribute để tạo filter xác thực admin
    public class AdminAuthentication : ActionFilterAttribute
    {
        // Override phương thức OnActionExecuting để thực hiện logic xác thực trước khi action được thực thi
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Kiểm tra xem session có chứa key "admin" hay không
            if (context.HttpContext.Session.GetString("admin") == null)
            {
                // Nếu session không có thông tin admin (user chưa đăng nhập hoặc không phải admin)
                // Thì chuyển hướng người dùng về trang đăng nhập
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "Controller","Login" }, // Chỉ định Controller đích là "Login"
                    {"action","Index" }       // Chỉ định Action đích là "Index"
                });
            }
            // Nếu session có thông tin admin, không làm gì và để action tiếp tục thực thi
        }
    }
}
