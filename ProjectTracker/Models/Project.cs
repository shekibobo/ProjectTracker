using System;
using System.Collections.Generic;
using System.Linq;
using Javax.Xml.Datatype;
using ProjectTracker.ViewModels;

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

    public bool IsActive
    {
      get { return IsActive; }
      set
      {
        IsActive = value;
        RaisePropertyChanged(nameof(IsActive));
      }
    }

    public int TotalDurationSeconds { get; set; }

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

      activeSegment.EndTime = stopTime;
      activeSegment = null;

      IsActive = false;
    }

    public void Tick(DateTime dateTime)
    {
      TotalDurationSeconds = (int)segments.Sum(segment => (segment.EndTime ?? dateTime).Subtract(segment.StartTime).TotalSeconds);
    }
  }
}
