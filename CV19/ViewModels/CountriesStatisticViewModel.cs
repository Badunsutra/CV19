using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
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

        private void OnRefreshDataCommandExecute(object p)
        {
            Countries = _DataService.GetData();
        }
        #endregion

        #endregion


        public CountriesStatisticViewModel(MainWindowViewModel MainModel)
        {
            _MainWindow = MainModel;
            _DataService = new DataService();


            #region Commands

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecute); 

            #endregion
        }
    }
}
