using PICalculator.App.Abstracts;
using System.Diagnostics;
using System.Windows.Input;

namespace PICalculator.App.ViewModels
{
    internal class InfoWindowViewModel : ViewModelBase
    {
        private const string GITHUB_NAVIGATION_LINK = "https://github.com/dOerKb/PICalculator";

        private readonly DelegateCommand navigateCommand;
        private readonly DelegateCommand okCommand;

        public InfoWindowViewModel()
        {
            this.navigateCommand = new DelegateCommand(this.OnNavigate, this.CanNavigate);
            this.okCommand = new DelegateCommand(this.OnOk, this.CanOk);

        }

        public string GithubNAvigationLink
        {
            get
            {
                return GITHUB_NAVIGATION_LINK;
            }
        }

        public ICommand NavigateCommand
        {
            get
            {
                return this.navigateCommand;
            }
        }

        public ICommand OkCommand
        {
            get
            {
                return this.okCommand;
            }
        }

        private bool CanNavigate(object parameter)
        {
            return true;
        }

        private void OnNavigate(object parameter)
        {
            string navigationUri = parameter as string;
            Process.Start(navigationUri);
        }

        private bool CanOk(object parameter)
        {
            return true;
        }

        private void OnOk(object parameter)
        {
        }
    }
}
