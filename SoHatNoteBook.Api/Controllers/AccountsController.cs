using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SoHatNoteBook.Api.Authentication.Models.DTOS.Incoming;
using SoHatNoteBook.Api.Authentication.Models.DTOS.Outgoing;
using SoHatNoteBook.Api.Config.Models;
using SoHatNoteBook.Api.IConfiguration;

namespace SoHatNoteBook.Api.Controllers;

public class AccountsController:BaseController {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtConfig _jwtConfig;
    public AccountsController(IUnitOfWork unitOfWork,
    UserManager<IdentityUser> userManager,
    IOptionsMonitor<JwtConfig> optionsMonitor
    ): base(unitOfWork){
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
    }
    // Register User
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Post([FromBody] UserRegistrationRequestDTos dTos ){
        // check the model or obj we are recieving is valid
        if(ModelState.IsValid){
            var useExist = await _userManager.FindByEmailAsync(dTos.Email);
            if(useExist != null){
                return BadRequest(new UserRegistrationResponseDTos(){
                    Success = false,
                    Errors = new List<string>{
                        "Email already in use"
                    }
                });
            }
        }
        // Add the user 
        var newUser = new IdentityUser(){
            Email = dTos.Email,
            UserName = dTos.Email,
            EmailConfirmed = true,

        };
        // Adding the user to table 
        var isCreate = await _userManager.CreateAsync(newUser, dTos.Password);
        if(!isCreate.Succeeded){
            return BadRequest(new UserRegistrationResponseDTos(){
                    Success = isCreate.Succeeded,
                    Errors = isCreate.Errors.Select(e => e.Description).ToList()
                });
        }
        var token= GenerateJwtToken(newUser);
        return Ok(new UserRegistrationResponseDTos(){
                    Success = true,
                    Token = token,
                });
    }
    private string GenerateJwtToken (IdentityUser user){
        var jwtHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
        var tokenDescription = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(new [] {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,  Guid.NewGuid().ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials= new SigningCredentials(         
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
            )
        };
        var token = jwtHandler.CreateToken(tokenDescription);
        var jwtToken = jwtHandler.WriteToken(token);
        return jwtToken;
    }
}