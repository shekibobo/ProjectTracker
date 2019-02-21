using System;
namespace ProjectTracker.Models
{
  public class FormsTimer : ITimer
  {
    public FormsTimer()
    {
    }

    public DateTime UtcNow => DateTime.UtcNow;

    public event EventHandler<DateTime> Elapsed;

    public void Start(TimeSpan interval)
    {
      Xamarin.Forms.Device.StartTimer(interval, () =>
      {
        Elapsed?.Invoke(this, UtcNow);
        return true;
      });
    }
  }
}
