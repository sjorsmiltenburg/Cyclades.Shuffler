using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Cyclades.Shuffler.Domain;
using Cyclades.Shuffler.Views;
using Xamarin.Forms;

namespace Cyclades.Shuffler.ViewModels
{
    public class GamePageViewModel : PropertyChangedBase
    {
        public ICommand NextRoundCommand { get; set; }
        public ICommand PreviousRoundCommand { get; set; }
        public ICommand EndGameCommand { get; set; }

        private string _item1Text;
        public string Item1Text
        {
            get { return _item1Text; }
            set
            {
                _item1Text = value;
                OnPropertyChanged();
            }
        }

        private string _item2Text;
        public string Item2Text
        {
            get { return _item2Text; }
            set
            {
                _item2Text = value;
                OnPropertyChanged();
            }
        }

        private string _item3Text;
        public string Item3Text
        {
            get { return _item3Text; }
            set
            {
                _item3Text = value;
                OnPropertyChanged();
            }
        }

        public string RoundText
        {
            get { return _roundText; }
            set
            {
                _roundText = value; 
                OnPropertyChanged();
            }
        }

        private readonly Game _game;
        private string _roundText;


        public GamePageViewModel()
        {
            _game = new Game();

            NextRoundCommand = new Command(() =>
            {
                _game.MoveToNextRound();
                Item1Text = _game.CurrentRound.OpenCards[0].Name;
                Item2Text = _game.CurrentRound.OpenCards[1].Name;
                Item3Text = _game.CurrentRound.OpenCards[2].Name;
                RoundText = $"Round {_game.CurrentRound.RoundNr}";
            });
            PreviousRoundCommand = new Command(() =>
            {
                _game.MoveToPreviousRound();
                Item1Text = _game.CurrentRound.OpenCards[0].Name;
                Item2Text = _game.CurrentRound.OpenCards[1].Name;
                Item3Text = _game.CurrentRound.OpenCards[2].Name;
                RoundText = $"Round {_game.CurrentRound.RoundNr}";
            });
            EndGameCommand = new Command(() =>
            {
                App.Current.MainPage = new StartPage();
            });
        }
    }
}
