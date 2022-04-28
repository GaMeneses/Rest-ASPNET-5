using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestASPNET.Business;
using RestASPNET.Data.VO;

namespace RestASPNET.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null) return BadRequest("invalid client request");
            var token = _loginBusiness.ValidateCredentials(user);           
            if (token == null) return Unauthorized();
            return Ok(token);
        }

        [HttpPost]
        [Route("Refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            if (tokenVo is null) return BadRequest("invalid client request");
            var token = _loginBusiness.ValidateCredentials(tokenVo);
            if (token == null) return BadRequest("invalid client request");
            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var result = _loginBusiness.RevokeToken(User.Identity.Name);
            if (!result) return BadRequest("invalid client request");
            return NoContent();
        }
    }
}
