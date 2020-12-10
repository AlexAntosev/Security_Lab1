using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Security_Lab5.Models;
using Security_Lab5.Services;

namespace Security_Lab5.Controllers
{
    [ApiController]
    [Route("sensitive")]
    public class SensitiveDataController : ControllerBase
    {
        private readonly ISensitiveDataService _sensitiveDataService;

        public SensitiveDataController(ISensitiveDataService sensitiveDataService)
        {
            _sensitiveDataService = sensitiveDataService;
        }
        
        [HttpPost]
        public async Task PostSensitiveData([FromBody]UserModel userModel)
        {
            await _sensitiveDataService.Post(userModel, userModel.CreditCard);
        }
        
        [HttpGet("{email}")]
        public async Task<string> GetSensitiveData(string email)
        {
            var creditCard = await _sensitiveDataService.Get(email);
            
            return creditCard;
        }
    }
}