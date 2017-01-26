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

        private string _item1Text;
        public string Item1Text
        {
            get { return _item1Text; }
            set
            {
                _item1Text = value;
                RaisePropertyChanged(() => Item1Text);
            }
        }

        private string _item2Text;
        public string Item2Text
        {
            get { return _item2Text; }
            set
            {
                _item2Text = value;
                RaisePropertyChanged(() => Item2Text);
            }
        }

        private string _item3Text;
        public string Item3Text
        {
            get { return _item3Text; }
            set
            {
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