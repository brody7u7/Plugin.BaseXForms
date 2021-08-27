using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace WaspApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public Page Context { get; set; }
        public Command BackCommand { get; set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (value != _isBusy)
                {
                    _isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
        }

        public ViewModelBase(Page context)
        {
            Navigation = context.Navigation;
            Context = context;

            BackCommand = new Command(async () => await Navigation.PopAsync());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Device.BeginInvokeOnMainThread(() =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));
        }
    }
}
