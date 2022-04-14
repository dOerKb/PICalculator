using PICalculator.App.Abstracts;
using PICalculator.App.Windows;
using System;
using System.Windows;
using System.Windows.Input;

namespace PICalculator.App.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private const string DEFAULT_CHARACTER_NAME = "Please Login via ESI";

        private readonly DelegateCommand loginCommand;
        private readonly DelegateCommand showInfoCommand;

        private readonly DelegateCommand closeAppCommand;

        private string accountName;

        public MainWindowViewModel()
        {
            this.loginCommand = new DelegateCommand(this.OnLogin, this.CanLogin);
            this.showInfoCommand = new DelegateCommand(this.OnShowInfo, this.CanShowInfo);

            this.closeAppCommand = new DelegateCommand(this.OnCloseApp, this.CanCloseApp);

            this.accountName = DEFAULT_CHARACTER_NAME;
        }

        public string AccountName
        {
            get
            {
                return this.accountName;
            }
            set
            {
                base.SetProperty(ref this.accountName, value);
            }
        }

        public ICommand ShowInfoCommand
        {
            get
            {
                return this.showInfoCommand;
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                return this.loginCommand;
            }
        }

        public ICommand CloseAppCommand
        {
            get
            {
                return this.closeAppCommand;
            }
        }

        private bool CanLogin(object parameter)
        {
            return true;
        }

        private void OnLogin(object parameter)
        {
            MessageBox.Show("Work in progress");
        }

        private bool CanShowInfo(object parameter)
        {
            return true;
        }

        private void OnShowInfo(object parameter)
        {
            InfoWindow infoWindow = new InfoWindow();
            infoWindow.ShowDialog();
        }


        private bool CanCloseApp(object parameter)
        {
            return true;
        }

        private void OnCloseApp(object parameter)
        {
            Environment.Exit(0);
        }
    }
}