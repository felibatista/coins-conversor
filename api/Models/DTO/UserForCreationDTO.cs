using System.ComponentModel.DataAnnotations;

namespace conversor_coin.Models.DTO;

public class UserForCreationDTO
{
    public String UserName { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public String Email { get; set; }
    [Required, RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")]
    public String Password { get; set; }
}