using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyclades.Shuffler.Helpers;
using Cyclades.Shuffler.ViewModels;
using Xamarin.Forms;

namespace Cyclades.Shuffler.Views
{
    public partial class GamePage : ContentPage
    {
        public GamePage(GamePageViewModel gamePageViewModel)
        {
            BindingContext = gamePageViewModel;
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            ((GamePageViewModel)this.BindingContext).EndGameCommand.Execute(null);
            return true; //default cancel
        }
        
    }
}
