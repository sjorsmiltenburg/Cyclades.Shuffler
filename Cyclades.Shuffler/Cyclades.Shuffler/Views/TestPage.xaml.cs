using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Cyclades.Shuffler.Views
{
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
            mybutton.Clicked += ((sender, args) => ClickHandler());
        }

        private void ClickHandler()
        {
            Animate();
        }

        async void Animate()
        {
            //await posseidon.ScaleTo(2, 5000, Easing.Linear);
            posseidon.AnchorY = 0;
            posseidon.AnchorX = 0;
            posseidon.RotationY = -90;
            await posseidon.RotateYTo(0, 1000, Easing.Linear);
        }
    }
}
