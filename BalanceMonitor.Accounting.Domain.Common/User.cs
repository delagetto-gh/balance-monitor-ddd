using System.Runtime.Serialization;
namespace BalanceMonitor.Accounting.Domain.Common
{
  [DataContract]
  public class User
  {
    public User(string name)
    {
      this.Name = name;
    }

    string Name { get; set; }
  }
}
