using ProjectTracker.Tests.Mocks;
using Xunit;
using ProjectTracker.ViewModels;
using System.Linq;
using System;

namespace ProjectTracker.Tests.ViewModels
{
  public class ProjectTrackerViewModelTests
  {
    private readonly MockTimer timer;

    public ProjectTrackerViewModelTests()
    {
      timer = new MockTimer();
    }

    [Fact]
    public void ConstructorStartsTimerWithOneSecondInterval()
    {
      var viewModel = getViewModel();
      Assert.Equal(1, timer.Interval.TotalSeconds);
    }

    [Fact]
    public void AddCommandEmptyProjectNameDoesNothing()
    {
      var viewModel = getViewModel();
      viewModel.NewProjectName = string.Empty;
      viewModel.AddCommand.Execute(null);
      Assert.Empty(viewModel.Projects);
    }

    [Fact]
    public void AddCommandNoActiveProjectsAddsNewInactiveProject()
    {
      var viewModel = getViewModel();
      var projectName = "ProjectTracker";
      viewModel.NewProjectName = projectName;
      viewModel.AddCommand.Execute(null);

      Assert.Equal(1, viewModel.Projects.Count);
      Assert.Equal(projectName, viewModel.Projects[0].Name);
      Assert.False(viewModel.Projects[0].IsActive);
      Assert.Equal(string.Empty, viewModel.NewProjectName);
    }

    [Fact]
    public void AddCommandExistingActiveProjectLeavesExistingProjectActiveAndAddsNewInactiveProject()
    {
      var viewModel = getViewModel("first project");
      var firstProject = viewModel.Projects.Single();
      viewModel.ToggleProjectCommand.Execute(firstProject);
      Assert.True(firstProject.IsActive);

      viewModel.NewProjectName = "second project";
      viewModel.AddCommand.Execute(null);
      Assert.Equal(2, viewModel.Projects.Count);
      Assert.True(viewModel.Projects[0].IsActive);
      Assert.False(viewModel.Projects[1].IsActive);
    }

    [Fact]
    public void TimeElapsed_OneProjectActive_ActiveProjectGetsTime()
    {
      var startingTime = new DateTime(2019, 2, 21, 0, 0, 0);
      var secondsToAdvance = 10;
      timer.UtcNow = startingTime;

      var viewModel = getViewModel("first project", "second project");
      var firstProject = viewModel.Projects.First();
      var secondProject = viewModel.Projects.Last();
      viewModel.ToggleProjectCommand.Execute(firstProject);

      timer.AdvanceSeconds(secondsToAdvance);

      Assert.Equal(secondsToAdvance, firstProject.TotalDurationSeconds);
      Assert.Equal(0, secondProject.TotalDurationSeconds);
    }

    [Fact]
    public void ToggleProject_NoProjectActive_ActiveProjectGetsTime()
    {
      var startingTime = new DateTime(2019, 2, 21, 0, 0, 0);
      timer.UtcNow = startingTime;

      var viewModel = getViewModel("first project", "second project");
      var firstProject = viewModel.Projects.First();
      var secondProject = viewModel.Projects.Last();

      Assert.False(firstProject.IsActive);
      Assert.False(secondProject.IsActive);

      viewModel.ToggleProjectCommand.Execute(firstProject);

      Assert.True(firstProject.IsActive);
      Assert.False(secondProject.IsActive);
    }

    [Fact]
    public void ToggleProject_OneProjectActive_StopsAndClearsCurrentProject()
    {
      var startingTime = new DateTime(2019, 2, 21, 0, 0, 0);
      var secondsToAdvance = 10;
      timer.UtcNow = startingTime;

      var viewModel = getViewModel("first project", "second project");
      var firstProject = viewModel.Projects.First();
      var secondProject = viewModel.Projects.Last();
      viewModel.ToggleProjectCommand.Execute(firstProject);

      timer.AdvanceSecondsSecretly(secondsToAdvance);

      Assert.Equal(startingTime.AddSeconds(secondsToAdvance), timer.UtcNow);
      Assert.Equal(0, firstProject.TotalDurationSeconds);
      Assert.Equal(0, secondProject.TotalDurationSeconds);

      viewModel.ToggleProjectCommand.Execute(firstProject);

      Assert.All(viewModel.Projects, (project) => Assert.False(project.IsActive));
      Assert.Equal(secondsToAdvance, firstProject.TotalDurationSeconds);
      Assert.Equal(0, secondProject.TotalDurationSeconds);
    }

    [Fact]
    public void ToggleProject_OneProjectActive_StopsAndClearsCurrentProject_ActivatesNewProject()
    {
      var startingTime = new DateTime(2019, 2, 21, 0, 0, 0);
      var secondsToAdvance = 10;
      timer.UtcNow = startingTime;

      var viewModel = getViewModel("first project", "second project");
      var firstProject = viewModel.Projects.First();
      var secondProject = viewModel.Projects.Last();
      viewModel.ToggleProjectCommand.Execute(firstProject);

      timer.AdvanceSeconds(secondsToAdvance);

      Assert.Equal(secondsToAdvance, firstProject.TotalDurationSeconds);
      Assert.Equal(0, secondProject.TotalDurationSeconds);

      timer.AdvanceSecondsSecretly(secondsToAdvance);

      viewModel.ToggleProjectCommand.Execute(secondProject);

      Assert.False(firstProject.IsActive);
      Assert.True(secondProject.IsActive);

      Assert.Equal(2 * secondsToAdvance, firstProject.TotalDurationSeconds);
      Assert.Equal(0, secondProject.TotalDurationSeconds);

      timer.AdvanceSeconds(secondsToAdvance);

      Assert.Equal(2 * secondsToAdvance, firstProject.TotalDurationSeconds);
      Assert.Equal(secondsToAdvance, secondProject.TotalDurationSeconds);
    }

    [Fact]
    public void TimeElapsed_NoProjectActive_ActiveProjectGetsTime()
    {
      var startingTime = new DateTime(2019, 2, 21, 0, 0, 0);
      var secondsToAdvance = 10;
      timer.UtcNow = startingTime;

      var viewModel = getViewModel("first project", "second project");
      var firstProject = viewModel.Projects.First();
      var secondProject = viewModel.Projects.Last();

      timer.AdvanceSeconds(secondsToAdvance);

      Assert.Equal(0, firstProject.TotalDurationSeconds);
      Assert.Equal(0, secondProject.TotalDurationSeconds);
    }

    private ProjectTrackerViewModel getViewModel(params string[] projectNames)
    {
      var viewModel = new ProjectTrackerViewModel(timer);
      foreach (string name in projectNames)
      {
        viewModel.NewProjectName = name;
        viewModel.AddCommand.Execute(null);
      };
      return viewModel;
    }
  }
}
