using Philta.Base;
using Philta.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Philta.ViewModels
{
    public class PhiltaViewModel : ObservableObject, IPhiltaViewModel
    {
        private readonly IPhiltaModel _philtaModel;

        public PhiltaViewModel(IPhiltaModel philtaModel)
        {
            _philtaModel = philtaModel;
        }

        public string TestLabel
        {
            get { return _philtaModel.TestLabel; }
            set
            {
                _philtaModel.TestLabel = value;
                NotifyPropertyChanged("TestLabel");
            }
        }
    }
}
