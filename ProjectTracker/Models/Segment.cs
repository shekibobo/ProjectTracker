using System;

namespace ProjectTracker.Models
{
  internal class Segment
  {
    public Segment(DateTime startTime)
    {
      StartTime = startTime;
    }

    public System.DateTime StartTime { get; set; }
    public System.DateTime? EndTime { get; set; }
  }
}