using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        
        public ObservableCollection<Group> Groups { get; }


        private Group _selectedGroup;
        public Group SelectedGroup { get => _selectedGroup; set => Set(ref _selectedGroup, value); }

        public object[] CompositeObject { get; }


        private object _selectedCompositeObject;
        public object SelectedCompositeObject { get => _selectedCompositeObject; set => Set(ref _selectedCompositeObject, value); }



        #region SelectedIndex
        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => _selectedIndex = value;
        } 
        #endregion

        #region TestDataPoints

        private IEnumerable<DataPoint> _testDataPoints;
        /// <summary>Тестовый набор данных для визуализации графиков</summary>
        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _testDataPoints;
            set => Set(ref _testDataPoints, value);
        } 

        #endregion

        #region Title

        private string _title = "Анализ статистики CV19";
        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        #endregion

        #region Status

        private string _status = "Готов!";
        /// <summary>Статус программы</summary>
        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        #endregion

        /*----------------------------------------------------------------------------------------------------------------------------------------*/

        #region Команды

        #region CloseApplicationCommand

        /// <summary>Команда закрытия программы</summary>
        public ICommand CloseApplicationCommand { get; }
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        private bool CanCloseApplicationCommandExecute(object p) => true;

        #endregion

        #region ChangeTabIndexCommand
        public ICommand ChangeTabIndexCommand { get; }
        private bool CanChangeTabIndexCommandExecute(object p) => SelectedIndex >= 0;
        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null) return;
            SelectedIndex += Convert.ToInt32(p);
        }
        #endregion

        #region CreateNewGroupCommand
        public ICommand CreateNewGroupCommand { get; }
        private bool CanCreateNewGroupCommandExecute(object p) => true;
        private void OnCreateNewGroupCommandExecuted(object p)
        {
            var group_max_index = Groups.Count + 1;
            var new_group = new Group
            {
                Name = $"Группа {group_max_index}",
                Students = new ObservableCollection<Student>(),
            };

            Groups.Add(new_group);
        } 
        #endregion

        #region DeleteGroupCommand
        public ICommand DeleteGroupCommand { get; }
        private bool CanDeleteGroupCommandExecute(object p) => p is Group group && Groups.Contains(group);
        private void OnDeleteGroupCommandExecuted(object p)
        {
            if (!(p is Group group)) return;
            var group_index = Groups.IndexOf(group);
            Groups.Remove(group);
            if(group_index < Groups.Count)
                SelectedGroup = Groups[group_index];
        } 
        #endregion

        #endregion

        /*----------------------------------------------------------------------------------------------------------------------------------------*/

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);
            CreateNewGroupCommand = new LambdaCommand(OnCreateNewGroupCommandExecuted, CanCreateNewGroupCommandExecute);
            DeleteGroupCommand = new LambdaCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExecute);

            #endregion

            var data_points = new List<DataPoint>((int)(360/0.1));
            for(var x = 0d; x < 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }

            TestDataPoints = data_points;

            var student_index = 1;
            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name = $"Name {student_index}",
                Surname = $"Surname {student_index}",
                Patronymic = $"Patronymic {student_index++}",
                Birthday = DateTime.Now,
                Rating = 0
            });

            var groups = Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(students)
            });

            Groups = new ObservableCollection<Group>(groups);

            var data_List = new List<object>();
            data_List.Add("Hello World!");
            data_List.Add(42);
            var group = Groups[1];
            data_List.Add(group);
            data_List.Add(group.Students[0]);

            CompositeObject = data_List.ToArray();
        }
    }
}
