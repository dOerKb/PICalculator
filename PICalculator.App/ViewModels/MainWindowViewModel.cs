using PICalculator.App.Abstracts;
using PICalculator.App.Windows;
using PICalculator.Client;
using PICalculator.Data;
using System;
using System.Windows.Input;

namespace PICalculator.App.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private const string DEFAULT_CHARACTER_NAME = "Please Login via ESI";

        private readonly DelegateCommand loginCommand;
        private readonly DelegateCommand showInfoCommand;

        private readonly DelegateCommand closeAppCommand;
        private readonly EsiClient esiClient;

        private string characterName;
        private Character character;

        public MainWindowViewModel()
        {
            this.loginCommand = new DelegateCommand(this.OnLogin, this.CanLogin);
            this.showInfoCommand = new DelegateCommand(this.OnShowInfo, this.CanShowInfo);

            this.closeAppCommand = new DelegateCommand(this.OnCloseApp, this.CanCloseApp);

            this.characterName = DEFAULT_CHARACTER_NAME;

            this.esiClient = new EsiClient();
        }

        public string CharacterName
        {
            get
            {
                return this.characterName;
            }
            set
            {
                base.SetProperty(ref this.characterName, value);
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

        private async void OnLogin(object parameter)
        {
            this.character = await this.esiClient.Authenticate();
            this.CharacterName = this.character.Name;
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