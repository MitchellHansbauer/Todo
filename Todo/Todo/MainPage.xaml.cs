using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Todo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            LoadTasks();
        }

        public ObservableCollection<TodoTask> TodoTasks { get; set; }
        public TodoTask SelectedTask { get; set; }

        async void LoadTasks()
        {
            TodoTasks = new ObservableCollection<TodoTask>(await App.Database.GetToDoAsync());
        }

        async void OnSaveTaskClicked(object sender, EventArgs e)
        {
            var todoTask = new TodoTask
            {
                Name = taskNameEntry.Text,
                Notes = notesEditor.Text,
                Done = false // Default value for Done
            };

            await App.Database.SaveTodoTaskAsync(todoTask);
            TodoTasks.Add(todoTask);

            taskNameEntry.Text = string.Empty;
            notesEditor.Text = string.Empty;

            await DisplayAlert("Success", "Task saved successfully", "OK");
        }

        async void OnMarkAsCompletedClicked(object sender, EventArgs e)
        {
            if (SelectedTask != null)
            {
                SelectedTask.Done = true;
                await App.Database.SaveTodoTaskAsync(SelectedTask);
                await DisplayAlert("Success", "Task marked as completed", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Please select a task", "OK");
            }
        }
    }
}
