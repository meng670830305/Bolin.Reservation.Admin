using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Login;
using Model.Dto.User;
using Model.Other;
using webapi.Config;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserService _userService;
        private readonly ICustomJWTService _jwtService;
        public LoginController(ILogger<LoginController> logger, IUserService userService, ICustomJWTService jwtService)
        {
            _logger = logger;
            _userService = userService;
            _jwtService = jwtService;
        }
        [HttpPost]
        public async Task<ApiResult> GetToken([FromBody] LoginReq req)
        {
            //Model vertify
            if (ModelState.IsValid)
            {
                UserRes user = await _userService.GetUser(req);
                if (user == null)
                {
                    return ResultHelper.Error("アカウントが見つかりませんでした。アカウントとパスワードを確認してください。");
                }
                _logger.LogInformation("ログイン失敗");
                return ResultHelper.Success(await _jwtService.GetToken(user));//return JWTToken

            }
            else
            {
                return ResultHelper.Error("パラメータが不正です。");
            }
        }
    }
}
