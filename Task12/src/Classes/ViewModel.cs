using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace Task12.Classes
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TaskModel> Tasks { get; set; }
        private string _newTaskName = string.Empty;
        private string _filter = string.Empty;
        private TaskModel? _selectedTask;

        public string NewTaskName
        {
            get => _newTaskName;
            set
            {
                _newTaskName = value;
                OnPropertyChanged(nameof(NewTaskName));
            }
        }

        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                OnPropertyChanged(nameof(Filter));
                OnPropertyChanged(nameof(FilteredTasks));
            }
        }
        public TaskModel? SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
                NewTaskName = _selectedTask?.Name ?? string.Empty;
            }
        }

        public ObservableCollection<TaskModel> FilteredTasks
        {
            get
            {
                if (string.IsNullOrEmpty(Filter) || Filter == "All")
                    return Tasks;

                return new ObservableCollection<TaskModel>(Tasks.Where(t =>
                    (Filter == "Completed" && t.IsCompleted) ||
                    (Filter == "Not Completed" && !t.IsCompleted)));
            }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand RemoveTaskCommand { get; }
        public ICommand EditTaskCommand { get; }
        public ICommand SaveTasksCommand { get; }
        public ICommand LoadTasksCommand { get; }
        public ICommand MarkAsCompletedCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public TaskViewModel()
        {
            Tasks = new ObservableCollection<TaskModel>();
            AddTaskCommand = new RelayCommand(_ => AddTask());
            RemoveTaskCommand = new RelayCommand<TaskModel>(RemoveTask);
            EditTaskCommand = new RelayCommand(_ => EditTask());
            SaveTasksCommand = new RelayCommand(_ => SaveTasks());
            LoadTasksCommand = new RelayCommand(_ => LoadTasks());
            MarkAsCompletedCommand = new RelayCommand<TaskModel>(MarkAsCompleted);
        }

        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskName))
            {
                Tasks.Add(new TaskModel { Name = NewTaskName });
                NewTaskName = string.Empty;
            }
        }

        private void RemoveTask(object task)
        {
            if (task is TaskModel taskModel)
            {
                Tasks.Remove(taskModel);
            }
        }
        private void EditTask()
        {
            if (SelectedTask != null && !string.IsNullOrWhiteSpace(NewTaskName))
            {
                SelectedTask.Name = NewTaskName;
                NewTaskName = string.Empty;
                SelectedTask = null;
            }
        }
        private void MarkAsCompleted(TaskModel task)
        {
            if (task != null)
            {
                task.IsCompleted = true; 
            }
        }
        private void SaveTasks()
        {
            var json = JsonSerializer.Serialize(Tasks);
            File.WriteAllText("tasks.json", json);
        }
        private void LoadTasks()
        {
            if (File.Exists("tasks.json"))
            {
                var json = File.ReadAllText("tasks.json");
                var tasks = JsonSerializer.Deserialize<ObservableCollection<TaskModel>>(json);
                if (tasks != null)
                {
                    Tasks.Clear();
                    foreach (var task in tasks)
                    {
                        Tasks.Add(task);
                    }
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
