using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Mvc.Client.Extensions;

namespace Valhalla.App.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpPost("~/Autenticacao/Conectar")]
        public async Task<IActionResult> Conectar([FromForm] string provider)
        {
            if (string.IsNullOrWhiteSpace(provider))
            {
                return BadRequest();
            }

            if (!await HttpContext.IsProviderSupportedAsync(provider))
            {
                return BadRequest();
            }

            return Challenge(new AuthenticationProperties { RedirectUri = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value }, provider);
        }

        [HttpGet("~/Autenticacao/Desconectar")]
        [HttpPost("~/Autenticacao/Desconectar")]
        public IActionResult Desconectar()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}

