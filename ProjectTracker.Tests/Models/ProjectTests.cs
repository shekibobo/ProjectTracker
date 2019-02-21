using System;
using ProjectTracker.Models;
using Xunit;

namespace ProjectTracker.Tests.Models
{
  public class ProjectTests
  {
    [Fact]
    public void NewInstanceCanReadName()
    {
      var project = new Project("Name");
      Assert.Equal("Name", project.Name);
    }

    [Fact]
    public void StartTrackingSetsActive()
    {
      var project = new Project("Name");
      Assert.False(project.IsActive);
      project.StartTracking(DateTime.UtcNow);
      Assert.True(project.IsActive);
    }

    [Fact]
    public void StopTrackingSetsInactive()
    {
      var project = new Project("Name");
      project.StartTracking(DateTime.UtcNow);
      project.StopTracking(DateTime.UtcNow);
      Assert.False(project.IsActive);
    }

    [Fact]
    public void TickSegmentInProgressDurationBasedOnCurrentTime()
    {
      var startingTime = DateTime.UtcNow;
      var secondsToAdvance = 10;
      var project = new Project("Name");

      Assert.Equal(0, project.TotalDurationSeconds);
      project.StartTracking(startingTime);
      project.Tick(startingTime.AddSeconds(secondsToAdvance));
      Assert.Equal(secondsToAdvance, project.TotalDurationSeconds);
    }

    [Fact] public void TickSegmentNotInProgressDurationBasedOnStoredTime()
    {
      var startingTime = DateTime.UtcNow;
      var secondsToAdvance = 10;
      var project = new Project("name");
      var stopTime = startingTime.AddSeconds(secondsToAdvance);

      project.StartTracking(startingTime);
      project.StopTracking(stopTime);
      project.Tick(stopTime.AddMinutes(1));
      Assert.Equal(secondsToAdvance, project.TotalDurationSeconds);
    }
  }
}
