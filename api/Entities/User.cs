using SQLite;
using SQLiteNetExtensions.Attributes;

namespace conversor_coin;

public class User
{
    [PrimaryKey, AutoIncrement]
    public int Id {get; set;}
    public String UserName {get; set;}
    public String FirstName {get; set;}
    public String LastName {get; set;}
    public String Email {get; set;}
    [OneToMany (CascadeOperations = CascadeOperation.CascadeRead)]
    public List<CurrencyConversion> Conversions { get; set; } = new List<CurrencyConversion>();
    public int AuthId { get; set; }
    public Auth? Auth { get; set; }
    public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; } = null!;
}