using BalanceMonitor.Infrastructure.Interfaces.DDD;
using System;

namespace BalanceMonitor.Infrastructure.Interfaces.DDD
{
  public abstract class Entity : IEntity
  {
    private readonly Guid id;

    protected Entity()
      : this(Guid.NewGuid())
    {
    }

    protected Entity(Guid id)
    {
      this.id = id;
    }

    public Guid Id
    {
      get { return this.id; }
    }
  }
}
