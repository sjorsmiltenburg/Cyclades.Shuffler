using System.Collections.Generic;
using Cyclades.Shuffler.ViewModels;

namespace Cyclades.Shuffler.Messages
{
    public class StartRoundChangeAnimationMessage
    {
        public int RoundNr { get; set; }
        public List<CardViewModel> OpenCardsInNewRound { get; private set; }

        public StartRoundChangeAnimationMessage(int roundNr, List<CardViewModel> openCardsInNewRound)
        {
            RoundNr = roundNr;
            OpenCardsInNewRound = openCardsInNewRound;
        }

    }
}