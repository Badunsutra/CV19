using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
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

        #endregion


        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            #endregion

            var data_points = new List<DataPoint>((int)(360/0.1));
            for(var x = 0d; x < 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }

            TestDataPoints = data_points;
        }
    }
}
