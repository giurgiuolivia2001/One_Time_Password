using Microsoft.AspNetCore.Mvc;
using One_Time_Password.Service;

namespace One_Time_Password.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OneTimePasswordController : ControllerBase
    {
        private readonly OneTimePasswordService _oneTimePasswordService;

        public OneTimePasswordController(OneTimePasswordService oneTimePasswordService)
        {
            _oneTimePasswordService = oneTimePasswordService;
        }

        [HttpPost("GenerateOTP/{email}")]
        public String GenerateOTP(string email)
        {
            return _oneTimePasswordService.GenerateOtpForUser(email);
        }

        [HttpPost("CheckOTP/{email}/{password}")]
        public String CheckOTP(string email, string password)
        {
            return _oneTimePasswordService.CheckOtpForUser(email, password);
        }
    }
}
