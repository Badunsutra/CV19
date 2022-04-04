using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private DataService _DataService;
        private MainWindowViewModel _MainWindow { get; }

        #region Countries
        private IEnumerable<CountryInfo> _Countries;
        /// <summary>
        /// Статистика по странам
        /// </summary>
        public IEnumerable<CountryInfo> Countries
        {
            get => _Countries;
            private set => Set(ref _Countries, value);
        } 
        #endregion

        #region Commands

        #region RefreshDataCommand
        public ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommandExecuted(object p) => Countries = _DataService.GetData();
        private bool CanRefreshDataCommandExecute(object p) => true;
        #endregion

        #endregion

        public CountriesStatisticViewModel(MainWindowViewModel MainModel)
        {
            _MainWindow = MainModel;
            _DataService = new DataService();


            #region Commands

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted, CanRefreshDataCommandExecute); 

            #endregion
        }

        /// <summary>
        /// Отладочный конструктор, используемый в процессе разработки в визуальном дизайнере
        /// </summary>
        public CountriesStatisticViewModel() : this(null)
        {
            _Countries = Enumerable.Range(1, 10)
                .Select(i => new CountryInfo
                {
                    Name = $"Country {i}",
                    ProvinceCounts = Enumerable.Range(1, 10).Select(j => new PlaceInfo
                    {
                        Name = $"Province {i}",
                        Location = new Point(i, j),
                        Counts = Enumerable.Range(1, 10).Select(k => new ConfirmedCount
                        {
                            Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - k)),
                            Count = k
                        }).ToArray()
                    }).ToArray()
                }).ToArray();
        }
    }
}
