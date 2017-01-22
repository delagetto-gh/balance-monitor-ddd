using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceMonitor.Infrastructure.Interfaces.DDD
{
  public interface IRepository<T>
  {
    T Get(Guid id);
    void Save(T entity);
  }
}
