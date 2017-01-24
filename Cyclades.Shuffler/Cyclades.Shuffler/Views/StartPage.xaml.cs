using Cyclades.Shuffler.ViewModels;
using Xamarin.Forms;

namespace Cyclades.Shuffler.Views
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new StartPageViewModel();
            InitializeComponent();
        }
    }
}
