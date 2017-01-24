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

            Messenger.Default.Register<StartEnlargeAnimationMessage>(this, PlayEnlargeAnimation);
        }

        private async void PlayEnlargeAnimation(StartEnlargeAnimationMessage message)
        {
            await RoundLabel.ScaleTo(2, 500, Easing.CubicIn);
            await RoundLabel.ScaleTo(1, 500, Easing.CubicIn);
        }

        protected override bool OnBackButtonPressed()
        {
            ((GamePageViewModel)this.BindingContext).EndGameCommand.Execute(null);
            return true; //default cancel
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RoundLabel.ScaleTo(2, 500, Easing.CubicIn);
            await RoundLabel.ScaleTo(1, 500, Easing.CubicIn);
        }
    }
}
