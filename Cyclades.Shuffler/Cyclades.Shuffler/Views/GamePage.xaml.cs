using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyclades.Shuffler.ViewModels;
using Xamarin.Forms;

namespace Cyclades.Shuffler.Views
{
    public partial class GamePage : ContentPage
    {
        public GamePage(GamePageViewModel gamePageViewModel)
        {
            BindingContext = gamePageViewModel;
            InitializeComponent();
        }
    }
}
