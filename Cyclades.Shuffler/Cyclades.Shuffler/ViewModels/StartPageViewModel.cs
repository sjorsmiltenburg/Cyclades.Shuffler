using System.Windows.Input;
using Cyclades.Shuffler.Helpers;
using Cyclades.Shuffler.Views;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace Cyclades.Shuffler.ViewModels
{
    public class StartPageViewModel : PropertyChangedBase
    {
        private string _mainText;

        public string MainText
        {
            get { return _mainText; }
            set
            {
                _mainText = value;
                OnPropertyChanged();
            }
        }

        public StartPageViewModel()
        {
            Start2PlayerGameCommand = new Command(() =>
            {
                StartGame(2);
            });
            Start3PlayerGameCommand = new Command(() =>
            {
                StartGame(3);
            });
            Start4PlayerGameCommand = new Command(() =>
            {
                StartGame(4);
            });
            Start5PlayerGameCommand = new Command(() =>
            {
                StartGame(5);
            });
        }

        private void StartGame(int nrOfPlayers)
        {
            //((NavigationPage) Application.Current.MainPage).PushAsync(new GamePage());
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            var gamePageViewModel = ServiceLocator.Current.GetInstance<GamePageViewModel>();
            gamePageViewModel.Initialize(nrOfPlayers);
            navigationService.NavigateTo(ViewModelLocator.GamePageKey, gamePageViewModel);
        }

        public ICommand Start2PlayerGameCommand { get; set; }
        public ICommand Start3PlayerGameCommand { get; set; }
        public ICommand Start4PlayerGameCommand { get; set; }
        public ICommand Start5PlayerGameCommand { get; set; }
    }
}
