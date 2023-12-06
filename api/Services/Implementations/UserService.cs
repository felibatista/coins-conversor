using System.Linq;
using conversor_coin.Data;
using conversor_coin.Models.DTO;
using conversor_coin.Models.Repository.Interface;

namespace conversor_coin.Models.Repository.Implementations;

public class UserService : IUserService
{
    private readonly ConversorContext _context;
    
    public UserService (ConversorContext context)
    {
        _context = context;
    }
    
    public List<User> GetUsers()
    {
        return _context.Users.ToList();
    }

    public UserDTO GetUserFull(int id)
    {
        User? user = _context.Users.FirstOrDefault((users) => users.Id == id);

        if (user == null)
        {
           throw APIException.CreateException(
                           APIException.Code.US_01,
                           "User not found",
                           APIException.Type.NOT_FOUND);
        }

        UserDTO userDto = new()
        {
            UserId = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Subscription = _context.Subscriptions.FirstOrDefault((subscription) => subscription.Id == user.SubscriptionId)?.Name
        };

        return userDto;
    }
    
    public User GetUser(int id)
    {
        User? user = _context.Users.FirstOrDefault((users) => users.Id == id);

        if (user == null)
        {
            throw APIException.CreateException(
                APIException.Code.US_01,
                "User not found",
                APIException.Type.NOT_FOUND);
        }

        return user;
    }
    
    public User AddUser(UserForCreationDTO userForCreationDto)
    {
        User? userExist = _context.Users.FirstOrDefault((users) => users.Email == userForCreationDto.Email);
        if (userExist != null)
        {
            throw APIException.CreateException(
                APIException.Code.US_02,
                "User email already exists",
                APIException.Type.BAD_REQUEST);
        }
        
        User user = new()
        {
            UserName = userForCreationDto.UserName,
            FirstName = userForCreationDto.FirstName,
            LastName = userForCreationDto.LastName,
            Email = userForCreationDto.Email,
            SubscriptionId = 1 //default subscription free
        };
        
        Auth auth = new()
        {
            Password = userForCreationDto.Password,
            Role = "User",
            Id = user.Id
        };
        
        try
        {
            _context.Users.Add(user);
            _context.Auth.Add(auth);
        }
        catch (Exception e)
        {
            throw APIException.CreateException(
                APIException.Code.DB_01,
                "An error occurred while setting the data in the database",
                APIException.Type.INTERNAL_SERVER_ERROR);
        }
        
        try
        {
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw APIException.CreateException(
                APIException.Code.DB_02,
                "An error occurred while saving the data in the database",
                APIException.Type.INTERNAL_SERVER_ERROR);
        }

        return user;
    }

    public void UpdateSubscriptionUser(SubscriptionUserUpdateDTO subscriptionUserUpdateDto)
    {
        User? toChange = GetUser(subscriptionUserUpdateDto.UserId);

        Subscription? subscription = _context.Subscriptions.FirstOrDefault((subscription) => subscription.Id == subscriptionUserUpdateDto.SubscriptionId);

        if (subscription == null)
        {
            throw APIException.CreateException(
                APIException.Code.SB_01,
                "Subscription not found",
                APIException.Type.NOT_FOUND);
        }

        toChange.SubscriptionId = subscriptionUserUpdateDto.SubscriptionId;
        
        try
        { 
            _context.Users.Update(toChange);
        }
        catch (Exception e)
        {
            throw APIException.CreateException(
                APIException.Code.DB_01,
                "An error occurred while setting the data in the database",
                APIException.Type.INTERNAL_SERVER_ERROR);
        }
        
        try
        {
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw APIException.CreateException(
                APIException.Code.DB_02,
                "An error occurred while saving the data in the database",
                APIException.Type.INTERNAL_SERVER_ERROR);
        }
    }

    public void UpdateUser(UserForUpdateDTO userForUpdateDto)
    {
        User? toChange = GetUser(userForUpdateDto.UserToChangeID);
        
        User? userExist = _context.Users.FirstOrDefault((users) => users.Email == userForUpdateDto.Email);
        if (userExist != null && userExist.Id != toChange.Id)
        {
            throw APIException.CreateException(
                APIException.Code.US_02,
                "User email already exists",
                APIException.Type.BAD_REQUEST);
        }
        
        toChange.FirstName = userForUpdateDto.FirstName;
        toChange.LastName = userForUpdateDto.LastName;
        
        if (userForUpdateDto.Email != null)
        {
            toChange.Email = userForUpdateDto.Email;
        }

        try
        { 
            _context.Users.Update(toChange);
        }
        catch (Exception e)
        {
            throw APIException.CreateException(
                APIException.Code.DB_01,
                "An error occurred while setting the data in the database",
                APIException.Type.INTERNAL_SERVER_ERROR);
        }
        
        try
        {
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw APIException.CreateException(
                APIException.Code.DB_02,
                "An error occurred while saving the data in the database",
                APIException.Type.INTERNAL_SERVER_ERROR);
        }
    }

    public void DeleteUser(int userId)
    {
        User? toRemove = GetUser(userId);

        try
        { 

            _context.Users.Remove(toRemove);
        }
        catch (Exception e)
        {
            throw APIException.CreateException(
                APIException.Code.DB_01,
                "An error occurred while setting the data in the database",
                APIException.Type.INTERNAL_SERVER_ERROR);
        }
        
        try
        {
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw APIException.CreateException(
                APIException.Code.DB_02,
                "An error occurred while saving the data in the database",
                APIException.Type.INTERNAL_SERVER_ERROR);
        }
    }

    public int counter()
    {
        return _context.Users.Count();
    }
    
    public List<User> getUsersByPage(int page)
    {
        return _context.Users.Skip((page - 1) * 10).Take(10).ToList();
    }

    public List<User> findUserByInput(string input)
    {
        return _context.Users.Where((user) => user.UserName.ToLower().Replace(" ", "").Contains(input.ToLower().Replace(" ", ""))).Take(10).ToList();
    }
}