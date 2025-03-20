using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Deneme.Controllers
{

    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: /Login
        [HttpGet]
        public IActionResult Index(string returnUrl = null)
        {
            // returnUrl, kullanıcının login işlemi sonrası gitmek istediği sayfadır.
            return View(new Login { ReturnUrl = returnUrl });
        }

        // POST: /Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcıyı giriş yapmaya çalışıyoruz.
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    // Giriş başarılıysa, kullanıcıyı "Home" sayfasına yönlendiriyoruz.
                    // Eğer returnUrl varsa, ona yönlendirilecek.
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        // ReturnUrl yoksa Home/Index'e yönlendirilir.
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    // Giriş başarısızsa hata mesajı ekliyoruz
                    ModelState.AddModelError(string.Empty, "Geçersiz giriş.");
                }
            }

            // Eğer model geçerli değilse veya başka bir hata varsa, tekrar login sayfasını göster
            return View(model);
        }
    }


}


