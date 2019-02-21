using System;
namespace ProjectTracker.Models
{
  public interface ITimer
  {
    DateTime UtcNow { get;  }
    void Start(TimeSpan interval);
    event EventHandler<DateTime> Elapsed;
  }
}
