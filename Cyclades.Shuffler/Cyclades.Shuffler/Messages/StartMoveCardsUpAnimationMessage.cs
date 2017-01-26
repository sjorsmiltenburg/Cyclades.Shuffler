using System.Collections.Generic;
using Cyclades.Shuffler.ViewModels;

namespace Cyclades.Shuffler.Messages
{
    public class StartMoveCardsUpAnimationMessage
    {
        public List<CardViewModel> OpenCardsInNewRound { get; private set; }

        public StartMoveCardsUpAnimationMessage(List<CardViewModel> openCardsInNewRound)
        {
            OpenCardsInNewRound = openCardsInNewRound;
        }
    }
}