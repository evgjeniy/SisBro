using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace SisBro.Controllers
{
    public class AccountController : Controller
    {
        [Route("facebook-login")]
        public ChallengeResult FacebookLogin()
        {
            var properties = new AuthenticationProperties {RedirectUri = Url.Action("FacebookResponse")};
            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }

        [Route("facebook-response")]
        public async Task<IActionResult> FacebookResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            return RedirectToAction("Index", "Home");
        }

        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties {RedirectUri = Url.Action("GoogleResponse")};
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogOut()
        {
            foreach (string cookiesKey in Request.Cookies.Keys)
                Response.Cookies.Delete(cookiesKey);
            return RedirectToAction("Index", "Home");
        }
    }
}