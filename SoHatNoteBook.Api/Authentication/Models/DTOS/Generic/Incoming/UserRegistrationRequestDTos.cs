namespace SoHatNoteBook.Api.Authentication.Models.DTOS.Incoming;

public class UserRegistrationRequestDTos {
   public required string FirstName { get; set; }
   public required string LastName { get; set;}
   public required string Email { get; set;}
   public required string Password { get; set;}

}