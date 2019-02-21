using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTracker.Models
{
  public class Project : BindableObjectBase
  {
    public Project(string name)
    {
      Name = name;
    }

    public string Name { get; private set; }

    private Segment activeSegment;

    private bool isActive;
    public bool IsActive
    {
      get => isActive;
      set
      {
        isActive = value;
        RaisePropertyChanged(nameof(IsActive));
      }
    }

    private int totalDurationSeconds;
    public int TotalDurationSeconds
    {
      get => totalDurationSeconds;
      set
      {
        totalDurationSeconds = value;
        RaisePropertyChanged(nameof(TotalDurationSeconds));
      }
    }

    private List<Segment> segments = new List<Segment>();

    public void StartTracking(DateTime startTime)
    {
      if (activeSegment != null)
      {
        StopTracking(startTime);
      }
      activeSegment = new Segment(startTime);
      segments.Add(activeSegment);
      IsActive = true;
    }

    public void StopTracking(DateTime stopTime)
    {
      if (activeSegment == null)
      {
        return;
      }

      activeSegment.EndTimeUtc = stopTime;
      Tick(stopTime);
      activeSegment = null;

      IsActive = false;
    }

    public void Tick(DateTime dateTime)
    {
      TotalDurationSeconds = (int)segments.Sum(segment => (segment.EndTimeUtc ?? dateTime).Subtract(segment.StartTimeUtc).TotalSeconds);
    }
  }
}
