using GalaSoft.MvvmLight;

namespace Cyclades.Shuffler.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public string FileName { get; private set; }

        public CardViewModel(string cardName)
        {
            FileName = cardName.ToLower() + ".png";
        }
    }
}