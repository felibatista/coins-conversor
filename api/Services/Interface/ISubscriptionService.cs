using conversor_coin.Models.DTO;

namespace conversor_coin.Models.Repository.Interface;

public interface ISubscriptionService
{
    public List<Subscription> GetSubscriptions();
    public Subscription? GetSubscriptionId(int id);
    public Subscription? GetSubscriptionName(string name);
    public Subscription AddSubscription(SubscriptionForCreationDTO subscriptionForCreationDto);
    public void UpdateSubscription(SubscriptionForUpdateDTO subscriptionForUpdateDto);
    public void DeleteSubscription(int subscriptionId);
}