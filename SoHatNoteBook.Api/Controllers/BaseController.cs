
using Microsoft.AspNetCore.Mvc;
using SoHatNoteBook.Api.IConfiguration;

public class BaseController : ControllerBase{
      public IUnitOfWork _unitOfWork;
      public BaseController(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
      }
}