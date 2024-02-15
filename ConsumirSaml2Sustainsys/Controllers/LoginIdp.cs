using Microsoft.AspNetCore.Mvc;

namespace ConsumirSaml2Sustainsys.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class LoginIdp:Microsoft.AspNetCore.Mvc.ControllerBase
    {        
        [Microsoft.AspNetCore.Mvc.HttpGet(Name = "login-idp")]
        public async Task<IActionResult> LoginAdp() {            
            return Ok("Login satisfactorio");
        }

    }
}
