
using Microsoft.AspNetCore.Mvc;
using SohatNoteBook.DataService.Data;
using SohatNoteBook.DataService.Dtos;
using SohatNoteBook.Entities.DbSet;
using SoHatNoteBook.Api.IConfiguration;
namespace SoHatNoteBook.Api.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController{
        // private readonly AppDbContext _dbContext;
        public UsersController(IUnitOfWork unitOfWork): base(unitOfWork
        ){
            
        }
        [Route("Users")]
        [HttpGet]
        public IActionResult GetUsers(){
            var users = _unitOfWork.Users.All();
            return Ok(users);
        }
       
        [Route("Users")]
        [HttpPost]
      async public Task< IActionResult> PostUser(UserDto dto){
            var _user = new User(){};
            _user.Email = "vandinh410807@gmail.com";
            _user.Phone = "01235644";
            _user.Country = "VN";
            _user.FirstName = dto.FirstName;
            _user.LastName = dto.LastName;
            _user.UpdateDate = DateTime.UtcNow;
            _user.DateOfBirth = dto.DateOfBirth;
            await _unitOfWork.Users.Add(_user);
            await _unitOfWork.CompleteAsync();
            return  Ok(_user);
        }
        [Route("GetUser")]
        [HttpGet]
        public IActionResult GetUser(Guid id){
            var user = _unitOfWork.Users.GetById(id);
            return Ok(user);
        }
        [Route("DeleteUser")]
        [HttpDelete]
        public IActionResult DeleteUser(Guid id){
            var user = _unitOfWork.Users.Delete(id);
            return Ok(user);
        }
    }
}