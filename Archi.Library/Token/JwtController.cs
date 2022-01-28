using Microsoft.AspNetCore.Mvc;

namespace Archi.Library.Token
{
    [ApiController]
    //[Route("api/[Controller]")]
    public class JwtController : ControllerBase
    {
       [HttpGet]
       [Route("connect")] 
        public ActionResult<string> Connect(string login, string password)
        {
            if (password == "Tokinizer")
            {
                return new ObjectResult(JwtToken.GenerateJwtToken());
            }
            else
            {
                return BadRequest("eugneugneu mauvais mot de passe");
            }
        }
    }
}