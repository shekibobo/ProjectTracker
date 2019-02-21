using System;

namespace ProjectTracker.Models
{
  internal class Segment
  {
    public Segment(DateTime startTimeUtc)
    {
      StartTimeUtc = startTimeUtc;
    }

    public System.DateTime StartTimeUtc { get; set; }
    public System.DateTime? EndTimeUtc { get; set; }
  }
}