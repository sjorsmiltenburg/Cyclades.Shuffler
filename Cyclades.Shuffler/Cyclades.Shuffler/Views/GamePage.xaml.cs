using System.Collections.Generic;
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

            Messenger.Default.Register<StartRoundLabelAnimationMessage>(this, StartRoundLabelAnimation);
            Messenger.Default.Register<StartMoveCardsDownAnimationMessage>(this, StartMoveCardsDownAnimation);
            Messenger.Default.Register<StartMoveCardsUpAnimationMessage>(this, StartMoveCardsUpAnimation);
        }

        private async void StartMoveCardsUpAnimation(StartMoveCardsUpAnimationMessage message)
        {
            await AnimateImagesRotateUp();
            ResetImageOrder(message.OpenCardsInNewRound);
            Messenger.Default.Send(new MoveCardsUpAnimationFinishedMessage());
        }

        private async void StartMoveCardsDownAnimation(StartMoveCardsDownAnimationMessage obj)
        {
            await AnimateImagesRotateDown();
            NextRoundButton.IsEnabled = true;
            PreviousRoundButton.IsEnabled = true;
            EndGameButton.IsEnabled = true;
        }

        private async void StartRoundLabelAnimation(StartRoundLabelAnimationMessage message)
        {
            NextRoundButton.IsEnabled = false;
            PreviousRoundButton.IsEnabled = false;
            EndGameButton.IsEnabled = false;

            await RoundLabel.ScaleTo(2, 500, Easing.CubicIn);
            await RoundLabel.ScaleTo(1, 500, Easing.CubicIn);
            await AnimateImagesRotateDown();

            Messenger.Default.Send(new RoundLabelAnimationFinishedMessage());
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            StartRoundLabelAnimation(null);
        }
    }
}
