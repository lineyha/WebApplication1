using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.UserAccount.ToList());
        }

        public IActionResult Registration()
        {
            return View();  
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
           
            if(ModelState.IsValid)
            {
                UserAccount account = new UserAccount();
                account.Email = model.Email;
                account.UserName= model.UserName;  
                account.Password = model.Password;
                try
                {

                    _context.UserAccount.Add(account);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{account.UserName} registered successfully. Please Login. ";
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "The mail address is already used.");
                    return View(model);
            
                }
                return View();
  
            }

            return View(model);
        }



        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = _context.UserAccount.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.Role,"User"),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("SecurePage");
                   
                }
                else
                {
                    ModelState.AddModelError("", "mail or password is not correct!");
                }

            }
            return View(model);
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
    }
}
