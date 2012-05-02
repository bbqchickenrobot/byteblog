using System.Web.Mvc;
using System.Web.Security;
using Byte.Blog.Editorial.Models;

namespace Byte.Blog.Editorial.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            if (FormsAuthentication.Authenticate(loginViewModel.Username, loginViewModel.Password))
            {
                FormsAuthentication.SetAuthCookie(loginViewModel.Username, false);
                return this.Redirect(loginViewModel.ReturnUrl);
            }

            this.ModelState.AddModelError("", "Login failed!");
            return this.View();
        }
    }
}
