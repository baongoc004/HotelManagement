using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HotelManagement.Models.Authentication
{
    // Lớp AdminOrNhanVienAuthentication kế thừa từ ActionFilterAttribute để tạo filter xác thực cho cả admin và nhân viên
    public class AdminOrNhanVienAuthentication : ActionFilterAttribute
    {
        //OnActionExecuting sẽ gọi trước khi hàm action mà nó được kèm theo
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //nếu tài khoản admin và nhanvien đều null thì đăng nhập
            // Kiểm tra cả hai session "admin" và "nhanvien" có tồn tại hay không
            if (context.HttpContext.Session.GetString("admin") == null && context.HttpContext.Session.GetString("nhanvien") == null)
            {
                // Nếu cả hai session đều null (nghĩa là user chưa đăng nhập với bất kỳ vai trò nào)
                // Thì chuyển hướng người dùng về trang đăng nhập
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller","Login" }, // Chỉ định Controller đích là "Login"
                    { "action","Index" }      // Chỉ định Action đích là "Index"
                });
            }
            // Nếu có ít nhất một trong hai session (admin hoặc nhanvien) tồn tại
            // thì không làm gì và để action tiếp tục thực thi
        }
    }
}
