namespace BalanceMonitor.Accounting.Domain.Model
{
  using BalanceMonitor.Infrastructure.Interfaces.DDD;

  public class User : Entity
  {
    string FirstName { get; set; }

    string LastName { get; set; }
  }
}
