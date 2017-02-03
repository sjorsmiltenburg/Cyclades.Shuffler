using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cyclades.Shuffler.Domain;
using Cyclades.Shuffler.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace Cyclades.Shuffler.ViewModels
{
    public class GamePageViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;

        public ICommand NextRoundCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _game.MoveToNextRound();
                    AnimateMovingToNextRound();
                });
            }
        }

        public List<Card> CurrentRoundOpenCards { get { return _game.CurrentRound.OpenCards; } }

        private void AnimateMovingToNextRound()
        {
            Messenger.Default.Send(new StartRoundChangeAnimationMessage(_game.CurrentRound.RoundNr, _game.CurrentRound.OpenCards.Select(x=>new CardViewModel(x.Name)).ToList()));
        }

        public ICommand PreviousRoundCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _game.MoveToPreviousRound();
                    AnimateMovingToNextRound();
                },()=> { return _game !=null && _game.CurrentRound.RoundNr > 1; });
            }
        }

        public ICommand EndGameCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var canClose = await CanClose();
                    if (canClose)
                    {
                        ServiceLocator.Current.GetInstance<INavigationService>().GoBack();
                    }
                });
            }
        }

        private async Task<bool> CanClose()
        {
            var result = false;
            await _dialogService.ShowMessage(
                message: "Are you sure you want to leave the game?",
                title: "Exit game?",
                buttonConfirmText: "Yes",
                buttonCancelText: "No",
                afterHideCallback: (confirmed) =>
                {
                    result = confirmed;
                });
            return result;
        }


        private Game _game;
        
        public GamePageViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            Device.StartTimer(TimeSpan.FromSeconds(1), IncrementTimer);
        }

        private TimeSpan _gameTimer = new TimeSpan();

        private bool IncrementTimer()
        {
            _gameTimer = _gameTimer.Add(TimeSpan.FromSeconds(1));
            RaisePropertyChanged(propertyName: "GameTimeValue");
            return true;
        }

        public string GameTimeValue
        {
            get
            {
                if (_gameTimer.Hours > 1)
                {
                    return _gameTimer.ToString("h':'mm':'ss");
                }
                else
                {
                    return _gameTimer.ToString("mm':'ss");
                }
            }
        }

        public void Initialize(int nrOfPlayers)
        {
            _game = new Game(nrOfPlayers);
            AnimateMovingToNextRound();
            NrOfPlayers = $"{nrOfPlayers} Players";
            RaisePropertyChanged("NrOfPlayers");
        }

        public string NrOfPlayers { get; set; }

        public async Task<bool> CanNavigateBack()
        {
            return await CanClose();
        }
    }
}