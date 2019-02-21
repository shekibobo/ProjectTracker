using ProjectTracker.Tests.Mocks;
using Xunit;
using ProjectTracker.ViewModels;

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
