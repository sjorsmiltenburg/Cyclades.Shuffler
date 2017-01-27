using System.Threading.Tasks;
using System.Windows.Input;
using Cyclades.Shuffler.Helpers;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace Cyclades.Shuffler.ViewModels
{
    public class StartPageViewModel : PropertyChangedBase
    {
        public StartPageViewModel()
        {
            StartGameCommand = new Command((nrOfPlayers) =>
            {
                StartGame(int.Parse((string)nrOfPlayers));
            });
        }

        private async void StartGame(int nrOfPlayers)
        {
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            var gamePageViewModel = ServiceLocator.Current.GetInstance<GamePageViewModel>();
            navigationService.NavigateTo(ViewModelLocator.GamePageKey, gamePageViewModel);
            gamePageViewModel.Initialize(nrOfPlayers);
        }

        public ICommand StartGameCommand { get; set; }
        public ICommand ScaleUpCommand { get; set; }
    }
}
