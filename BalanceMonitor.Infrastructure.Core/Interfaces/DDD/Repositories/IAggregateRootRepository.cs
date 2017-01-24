namespace BalanceMonitor.Infrastructure.Core.Interfaces.DDD
{
  public interface IAggregateRootRepository<TAggregateRoot> : IEntityRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot, new()
  { }
}
