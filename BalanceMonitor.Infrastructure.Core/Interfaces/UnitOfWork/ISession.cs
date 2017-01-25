﻿using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork
{
  public interface ISession : IDisposable
  {
    void Commit();
  }
}
