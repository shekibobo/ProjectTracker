using System;
using System.Collections.Generic;
using ProjectTracker.Models;
using ProjectTracker.ViewModels;
using Xamarin.Forms;

namespace ProjectTracker.Pages
{
  public partial class ProjectTrackerPage : ContentPage
  {
    public ProjectTrackerPage()
    {
      InitializeComponent();
      var vm = new ProjectTrackerViewModel(new FormsTimer());
      BindingContext = vm;
      Projects.ItemTapped += (sender, e) =>
      {
        vm.ToggleProjectCommand.Execute(e.Item);
        Projects.SelectedItem = null;
      };
    }
  }
}
