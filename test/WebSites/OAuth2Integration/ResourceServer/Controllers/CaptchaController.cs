using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NCaptcha.Abstractions;
using NCaptcha.AspNetCore.Extensions;

namespace OAuth2Integration.ResourceServer.Controllers
{
    [Route("captcha")]
    public class CaptchaController : Controller
    {
        private readonly ICaptchaGenerator _captchaGenerator;

        public CaptchaController(ICaptchaGenerator captchaGenerator)
        {
            _captchaGenerator = captchaGenerator;
        }

        [Produces("image/gif", Type = typeof(FileContentResult))]
        [HttpGet]
        public async Task<IActionResult> OnGetCaptchaAsync() => await _captchaGenerator.GetCaptchaFileResultAsync();
    }
}
