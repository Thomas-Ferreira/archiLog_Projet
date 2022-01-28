using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

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
            if (password == "archilog")
            {
                return new ObjectResult(JwtToken.GenerateJwtToken());
            }
            else
            {

            }
            {
                return BadRequest("compte invalide");
            }
        }
    }
}