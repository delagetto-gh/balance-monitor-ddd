namespace BalanceMonitor.Infrastructure.Core.Interfaces.DDD
{
  public interface IAggregateRootRepository<TAggregateRoot> : IEntityRepository<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot, new()
  { }
}
