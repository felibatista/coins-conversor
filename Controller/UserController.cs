using conversor_coin.Data;
using conversor_coin.Models.DTO;
using conversor_coin.Models.Repository.Implementations;
using conversor_coin.Models.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace conversor_coin.Controller;

[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userContext;
    private readonly ISubscriptionRepository _subscriptionContext;
    
    public UserController(IUserRepository _userContext, ISubscriptionRepository _subscriptionContext)
    {
        this._userContext = _userContext;
        this._subscriptionContext = _subscriptionContext;
    }
    
    [Route("all")]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_userContext.GetUsers());
    }

    
    [HttpGet("{userId}")]
    public ActionResult<User> GetUser(int id)
    {
        User? user = _userContext.GetUser(id);
        
        if (user == null)
        {
            return NotFound();
        }
        
        return user;
    }

    [HttpPost]
    public ActionResult<User> PostUser(UserForCreationDTO userForCreationDto)
    {
        _userContext.AddUser(userForCreationDto);
        return Ok("User created successfully");
    }
    
    [HttpPut("{userId}")]
    public ActionResult<User> PutUser(UserForUpdateDTO userForUpdateDto)
    {
        if (_userContext.GetUser(userForUpdateDto.UserToChangeID) == null)
        {
            return NotFound("Invalid user");
        }
        
        _userContext.UpdateUser(userForUpdateDto);
        return Ok("User updated successfully");
    }
    
    [HttpPut("subscription/{userId}")]
    public ActionResult<User> PutSubscriptionUser(int userId, int subscriptionId)
    {
        if (_userContext.GetUser(userId) == null)
        {
            return NotFound("Invalid user");
        }
        if (_subscriptionContext.GetSubscription(subscriptionId) == null)
        {
            return NotFound("Invalid subscription");
        }

        SubscriptionUserUpdateDTO subscriptionUserUpdateDto = new()
        {
            UserId = userId,
            SubscriptionId = subscriptionId
        };
        
        _userContext.UpdateSubscriptionUser(subscriptionUserUpdateDto);
        return Ok("User subscription updated successfully");
    }

    [HttpDelete("{userId}")]
    public ActionResult<User> DeleteUser(int id)
    {
        User? user = _userContext.GetUser(id);

        if (user == null)
        {
            return NotFound();
        }

        _userContext.DeleteUser(user);
        return Ok("User deleted successfully");
    }
}