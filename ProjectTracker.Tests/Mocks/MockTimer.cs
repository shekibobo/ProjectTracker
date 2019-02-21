using System;
using ProjectTracker.Models;

namespace ProjectTracker.Tests.Mocks
{
  public class MockTimer : ITimer
  {
    private DateTime utcNow;

    public DateTime UtcNow
    {
      get => utcNow;
      set
      {
        utcNow = value;
        Elapsed.Invoke(this, UtcNow);
      }
    }

    public TimeSpan Interval { get; private set; }

    public event EventHandler<DateTime> Elapsed;

    public void Start(TimeSpan interval)
    {
      Interval = interval;
    }

    public void AdvanceSeconds(int seconds)
    {
      UtcNow = UtcNow.AddSeconds(seconds);
    }
  }
}
