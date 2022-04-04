using CV19.Services;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CV19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private DataService _DataService;

        private MainWindowViewModel _MainWindow { get; }
        public CountriesStatisticViewModel(MainWindowViewModel MainModel)
        {
            _MainWindow = MainModel;
            _DataService = new DataService();
        }
    }
}
