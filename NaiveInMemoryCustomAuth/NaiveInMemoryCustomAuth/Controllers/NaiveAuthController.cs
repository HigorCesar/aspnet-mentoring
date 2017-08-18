using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NaiveInMemoryCustomAuth.Infrastructure;

namespace NaiveInMemoryCustomAuth.Controllers
{
    public class AuthData
    {
        public string User { get; set; }
        public string Password { get; set; }
    }

    public class DeleteTokenData
    {
        public string Token { get; set; }
    }
    [Route("api/[controller]")]
    public class NaiveAuthController : Controller
    {
        private readonly Authenticator authenticator;

        public NaiveAuthController(Authenticator authenticator)
        {
            this.authenticator = authenticator;
        }
        [HttpPost]
        public IActionResult Post([FromBody]AuthData authData)
        {
            if (!authenticator.Authenticate(authData.User, authData.Password))
                return Unauthorized();


            return Ok(authenticator.CreateAuthKey());
        }
        [HttpDelete]
        public IActionResult Delete([FromBody]DeleteTokenData token)
        {
            authenticator.DeleteAuthKey(token.Token);
            return NoContent();
        }
    }
}
