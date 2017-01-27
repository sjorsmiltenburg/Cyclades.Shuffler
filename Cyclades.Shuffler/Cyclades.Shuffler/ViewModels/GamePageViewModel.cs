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
            Messenger.Default.Send(new StartMoveCardsUpAnimationMessage(_game.CurrentRound.OpenCards.Select(x=>new CardViewModel(x.Name)).ToList()));
        }

        private void MoveCardsUpAnimationFinished(MoveCardsUpAnimationFinishedMessage obj)
        {
            RoundText = $"Round {_game.CurrentRound.RoundNr}";
            Messenger.Default.Send(new StartRoundLabelAnimationMessage());
        }

        private void RoundLabelAnimationFinished(RoundLabelAnimationFinishedMessage obj)
        {
            Messenger.Default.Send(new StartMoveCardsDownAnimationMessage());
        }

        public ICommand PreviousRoundCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _game.MoveToPreviousRound();
                    AnimateMovingToNextRound();
                });
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

        public GamePageViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            Messenger.Default.Register<RoundLabelAnimationFinishedMessage>(this,RoundLabelAnimationFinished);
            Messenger.Default.Register<MoveCardsUpAnimationFinishedMessage>(this, MoveCardsUpAnimationFinished);
        }

        public void Initialize(int nrOfPlayers)
        {
            _game = new Game(nrOfPlayers);
            AnimateMovingToNextRound();
        }

        public async Task<bool> CanNavigateBack()
        {
            return await CanClose();
        }
    }
}