using System.Collections.Generic;
using System.Linq;

namespace Cyclades.Shuffler.Domain
{
    public class Round
    {
        public int RoundNr { get; private set; }
        public List<Card> OpenCards { get; } = new List<Card>();
        public List<Card> ClosedCards { get; }


        public Round(int roundNr, Round previousRound, int nrOfOpenCards)
        {
            RoundNr = roundNr;

            var cardsToChooseFrom = Helper.Cards.ToList(); //copy

            var nrOfCardsToChoose = nrOfOpenCards;
            if (previousRound != null)
            {
                var closedCardOfPreviousRoundToChooseFrom = previousRound.ClosedCards.ToList(); //copy
                for (int i = 0; i < previousRound.ClosedCards.Count; i++)
                {
                    var chosenClosedCardFromPreviousRound = closedCardOfPreviousRoundToChooseFrom[RandomHelper.Instance.Random.Next(0, closedCardOfPreviousRoundToChooseFrom.Count)];
                    closedCardOfPreviousRoundToChooseFrom.Remove(chosenClosedCardFromPreviousRound);
                    cardsToChooseFrom.Remove(chosenClosedCardFromPreviousRound);
                    nrOfCardsToChoose--;
                    OpenCards.Add(chosenClosedCardFromPreviousRound);
                }
            }

            for (int i = 0; i < nrOfCardsToChoose; i++)
            {
                var card = cardsToChooseFrom[RandomHelper.Instance.Random.Next(0,cardsToChooseFrom.Count)];
                cardsToChooseFrom.Remove(card);
                OpenCards.Add(card);
            }
            ClosedCards = cardsToChooseFrom;
        }
    }
}