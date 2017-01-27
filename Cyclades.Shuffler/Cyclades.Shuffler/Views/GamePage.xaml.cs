using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cyclades.Shuffler.Messages;
using Cyclades.Shuffler.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using Xamarin.Forms;

namespace Cyclades.Shuffler.Views
{
    public partial class GamePage : ContentPage
    {
        public GamePage(GamePageViewModel gamePageViewModel)
        {
            BindingContext = gamePageViewModel;
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            Messenger.Default.Register<StartRoundChangeAnimationMessage>(this, StartRoundChangeAnimation);
        }

        

        private async void StartRoundChangeAnimation(StartRoundChangeAnimationMessage message)
        {
            if (!_appeared)
            {
                _parkedMessage = message;
                return;
            }
            NextRoundButton.IsEnabled = false;
            PreviousRoundButton.IsEnabled = false;
            EndGameButton.IsEnabled = false;

            //animate images up
            await AnimateImagesRotateUp();
            //refresh images
            ResetImageOrder(message.OpenCardsInNewRound);
            RoundLabel.Text = $"Round {message.RoundNr}";
            //animate round
            await RoundLabel.ScaleTo(2, 500, Easing.CubicIn);
            await RoundLabel.ScaleTo(1, 500, Easing.CubicIn);
            //animate images down
            await AnimateImagesRotateDown();

            NextRoundButton.IsEnabled = true;
            PreviousRoundButton.IsEnabled = true;
            EndGameButton.IsEnabled = true;
        }

        private async Task AnimateImagesRotateDown()
        {
            foreach (var image in StackLayout.Children.Where(x => x is Image))
            {
                await image.RotateYTo(0, 100, Easing.Linear);
            };
        }

        private void ResetImageOrder(List<CardViewModel> cardViewModels)
        {
            //clean out current images
            var ImageControls = StackLayout.Children.Where(x => x is Image).ToList();
            foreach (var imageControl in ImageControls)
            {
                StackLayout.Children.Remove(imageControl);
            }
            if (StackLayout.Children.Count(x => x is Image) > 0)
            {
              Debugger.Break();  
            };
            //insert new images
            foreach (var openCard in cardViewModels.Reverse<CardViewModel>())
            {
                StackLayout.Children.Insert(1, new Image() { Source = openCard.FileName, AnchorY = 0, AnchorX = 0, RotationY = -90 });
            }
        }

        private async Task AnimateImagesRotateUp()
        {
            foreach (var image in StackLayout.Children.Where(x => x is Image))
            {
                await image.RotateYTo(90, 100, Easing.Linear);
            };
        }

        protected override bool OnBackButtonPressed()
        {
            ((GamePageViewModel)this.BindingContext).EndGameCommand.Execute(null);
            return true; //default cancel
        }

        private StartRoundChangeAnimationMessage _parkedMessage;

        private bool _appeared = false;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _appeared = true;
            if (_parkedMessage != null)
            {
                StartRoundChangeAnimation(_parkedMessage);
                _parkedMessage = null;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _appeared = false;
        }
    }
}
