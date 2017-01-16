using System;
using System.Collections.Generic;
using System.Linq;

namespace Cyclades.Shuffler.Domain
{
    public static class Helper
    {
        public static List<Card> Cards { get; } = new List<Card>(){
                new Card("Zeus"),
                new Card("Athena"),
                new Card("Ares"),
                new Card("Posseidon")
            };
    }

    public class RandomHelper
    {
        public Random Random { get; private set; } = new Random(DateTime.Now.Millisecond);

        private static RandomHelper _instance;
        public static RandomHelper Instance
        {
            get
            {
                if (_instance == null) { _instance = new RandomHelper(); }
                return _instance;
            }
        }
    }

    public class Game
    {
        public List<Round> Rounds { get; set; } = new List<Round>();

        private int CurrentRoundNr { get; set; }

        public Round CurrentRound { get; set; }

        public void MoveToNextRound()
        {
            var previousRound = CurrentRound;
            CurrentRoundNr++;
            var newRound = Rounds.FirstOrDefault(x => x.RoundNr == CurrentRoundNr);
            if (newRound == null)
            {
                newRound = new Round(CurrentRoundNr, previousRound);
                Rounds.Add(newRound);
            }
            CurrentRound = newRound;
        }

        public void MoveToPreviousRound()
        {
            if (CurrentRoundNr > 1)
            {
                CurrentRoundNr--;
            }
            CurrentRound = Rounds.FirstOrDefault(x => x.RoundNr == CurrentRoundNr);
        }
    }

    public class Round
    {
        public int RoundNr { get; set; }
        public List<Card> OpenCards { get; set; } = new List<Card>();
        public Card ClosedCard { get; set; }


        public Round(int roundNr, Round previousRound)
        {
            RoundNr = roundNr;
            var cardsToChooseFrom = Helper.Cards.ToList();
            var nrOfCardsToChoose = 2;
            if (previousRound == null) //first round
            { 
                nrOfCardsToChoose = 3;
            }
            else
            {
                OpenCards.Add(previousRound.ClosedCard);
                cardsToChooseFrom.Remove(previousRound.ClosedCard);
            }

            for (int i = 0; i < nrOfCardsToChoose; i++)
            {
                var card = cardsToChooseFrom[RandomHelper.Instance.Random.Next(cardsToChooseFrom.Count - 1)];
                cardsToChooseFrom.Remove(card);
                OpenCards.Add(card);
            }
            ClosedCard = cardsToChooseFrom[0];
        }
    }

    public class Card
    {
        public string Name { get; set; }

        public Card(string name)
        {
            Name = name;
        }
    }
}
