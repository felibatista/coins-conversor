using conversor_coin.Models.DTO;

namespace conversor_coin.Models.Repository.Interface;

public interface IUserRepository
{
    public List<User> GetUsers();
    public User GetUser(int id);
    public User GetUser(String email);
    public void AddUser(UserForCreationDTO userForCreationDto);
    public void UpdateUser(UserForUpdateDTO userForUpdateDto);
    public void UpdateSubscriptionUser(SubscriptionUpdateDTO subscriptionUpdateDto);
    public void DeleteUser(User user);
}