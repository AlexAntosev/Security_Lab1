using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Security_Lab5.Models;
using Security_Lab5.Services;

namespace Security_Lab5.Controllers
{
    [ApiController]
    [Route("authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpGet]
        public async Task Get()
        {
            return;
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<bool> Register([FromBody]UserModel userModel)
        {
            return await _authService.Register(userModel);
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<UserModel> Login(UserModel userModel)
        {
            var user =  await _authService.Login(userModel);

            return user;
        }
    }
}