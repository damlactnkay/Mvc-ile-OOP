using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebSiteDeneme.Controllers
{
    public class RegisterController : Controller
    {
        
    private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public RegisterController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new Kullanici();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(Kullanici model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        Console.WriteLine("✅ Kullanıcı başarıyla oluşturuldu: " + user.Email);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Console.WriteLine("🚨 Kullanıcı oluşturulamadı! Hatalar:");
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine("❌ " + error.Description);
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Kayıt sırasında bir hata oluştu: " + ex.Message);
                    ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("❌ ModelState geçerli değil!");
                ModelState.AddModelError("", "Model doğrulama başarısız!");
            }

            return View(model);
        }


    }
}
