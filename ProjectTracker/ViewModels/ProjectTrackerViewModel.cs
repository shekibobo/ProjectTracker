using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ProjectTracker.Models;
using Xamarin.Forms;

namespace ProjectTracker.ViewModels
{
  public class ProjectTrackerViewModel : BindableObjectBase
  {
    private readonly ITimer timer;
    private Project currentProject;

    public ProjectTrackerViewModel(ITimer timer)
    {
      this.timer = timer;
      AddCommand = new Command(addProject);
      ResetCommand = new Command(reset);
      ToggleProjectCommand = new Command<Project>(toggleProject);

      timer.Start(TimeSpan.FromSeconds(1));
      timer.Elapsed += (sender, tickUtc) => currentProject.Tick(tickUtc);
    }

    public ICommand AddCommand { get; }
    public ICommand ResetCommand { get; }
    public ICommand ToggleProjectCommand { get; }

    private string newProjectName;
    public string NewProjectName
    {
      get => newProjectName;
      set
      {
        newProjectName = value;
        RaisePropertyChanged(nameof(NewProjectName));
      }
    }

    private ObservableCollection<Project> projects = new ObservableCollection<Project>();

    public ObservableCollection<Project> Projects
    {
      get => projects;
      set
      {
        projects = value;
        RaisePropertyChanged(nameof(Projects));
      }
    }


    private void addProject()
    {
      if (string.IsNullOrWhiteSpace(NewProjectName)) return;

      Projects.Add(new Project(NewProjectName));
      NewProjectName = "";
    }

    private void toggleProject(Project project)
    {
      throw new NotImplementedException();
    }

    private void reset(object project)
    {
      throw new NotImplementedException();
    }
  }
}