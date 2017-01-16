using System.Windows.Input;
using Cyclades.Shuffler.Views;
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
            MainText = "boe";
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
            ((NavigationPage) Application.Current.MainPage).PushAsync(new GamePage());
        }

        public ICommand Start2PlayerGameCommand { get; set; }
        public ICommand Start3PlayerGameCommand { get; set; }
        public ICommand Start4PlayerGameCommand { get; set; }
        public ICommand Start5PlayerGameCommand { get; set; }
    }
}
