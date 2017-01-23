using System.Windows.Input;
using Cyclades.Shuffler.Domain;
using Cyclades.Shuffler.Helpers;
using Cyclades.Shuffler.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace Cyclades.Shuffler.ViewModels
{
    public class GamePageViewModel : ViewModelBase
    {
        public ICommand NextRoundCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _game.MoveToNextRound();
                    Item1Text = _game.CurrentRound.OpenCards[0].Name;
                    Item2Text = _game.CurrentRound.OpenCards[1].Name;
                    if (_game.CurrentRound.OpenCards.Count > 2)
                    {
                        Item3Text = _game.CurrentRound.OpenCards[2].Name;
                    }
                    if (_game.CurrentRound.OpenCards.Count > 3)
                    {
                        Item4Text = _game.CurrentRound.OpenCards[3].Name;
                    }
                    RoundText = $"Round {_game.CurrentRound.RoundNr}";
                });
            } 
        }

        public ICommand PreviousRoundCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _game.MoveToPreviousRound();
                    Item1Text = _game.CurrentRound.OpenCards[0].Name;
                    Item2Text = _game.CurrentRound.OpenCards[1].Name;
                    Item3Text = _game.CurrentRound.OpenCards[2].Name;
                    Item4Text = _game.CurrentRound.OpenCards[3].Name;
                    RoundText = $"Round {_game.CurrentRound.RoundNr}";
                });
            }
        }

        public ICommand EndGameCommand {
            get
            {
                return new RelayCommand(() =>
                {
                    ServiceLocator.Current.GetInstance<INavigationService>().NavigateTo(ViewModelLocator.StartPageKey);
                });
            }
        }

            private Game _game;

        private string _item1Text;
        public string Item1Text
        {
            get { return _item1Text; }
            set
            {
                _item1Text = value;
                RaisePropertyChanged(()=>Item1Text);
            }
        }

        private string _item2Text;
        public string Item2Text
        {
            get { return _item2Text; }
            set
            {
                _item2Text = value;
                RaisePropertyChanged(()=>Item2Text);
            }
        }

        private string _item3Text;
        public string Item3Text
        {
            get { return _item3Text; }
            set {
                _item3Text = value;
                RaisePropertyChanged(() => Item3Text);
            }
        }

        private string _item4Text;
        public string Item4Text
        {
            get { return _item4Text; }
            set
            {
                _item4Text = value;
                RaisePropertyChanged(() => Item4Text);
            }
        }

        private string _roundText;
        public string RoundText
        {
            get { return _roundText; }
            set
            {
                _roundText = value;
                RaisePropertyChanged(() => RoundText);
            }
        }
        
        public GamePageViewModel()
        {
        }

        public void Initialize(int nrOfPlayers)
        {
            _game = new Game(nrOfPlayers);
        }
    }
}