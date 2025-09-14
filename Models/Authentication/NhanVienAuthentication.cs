using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HotelManagement.Models.Authentication
{
    public class NhanVienAuthentication : ActionFilterAttribute
    {
        //OnActionExecuting sẽ gọi trước khi hàm action mà nó được kèm theo
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Kiểm tra xem session có chứa key "nhanvien" hay không
            // GetString("nhanvien") trả về null nếu session không tồn tại hoặc đã hết hạn
            if (context.HttpContext.Session.GetString("nhanvien") == null)
            {
                // Nếu session "nhanvien" không tồn tại (user chưa đăng nhập)
                // Chuyển hướng user về trang đăng nhập
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller","Login" },
                    { "action","Index" }
                });
            }
            // Nếu session "nhanvien" tồn tại, không làm gì cả và cho phép action tiếp tục thực thi
        }
    }
}   
