using System.Collections.Generic;
using System.Linq;

namespace Cyclades.Shuffler.Domain
{
    public class Game
    {
        public List<Round> Rounds { get; set; } = new List<Round>();

        private int CurrentRoundNr { get; set; }

        public Round CurrentRound { get; set; }

        public int NrOfPlayers { get; private set; }

        private int NrOfCardsFaceUp;

        public void MoveToNextRound()
        {
            var previousRound = CurrentRound;
            CurrentRoundNr++;
            var newRound = Rounds.FirstOrDefault(x => x.RoundNr == CurrentRoundNr);
            if (newRound == null)
            {
                newRound = new Round(CurrentRoundNr, previousRound, NrOfCardsFaceUp);
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

        public Game(int nrOfPlayers)
        {
            NrOfPlayers = nrOfPlayers;
            switch (nrOfPlayers)
            {
                case 2:
                case 4:
                    NrOfCardsFaceUp = 3;
                    break;
                case 3:
                    NrOfCardsFaceUp = 2;
                    break;
                case 5:
                    NrOfCardsFaceUp = 4;
                    break;
            }
            MoveToNextRound();
        }
    }
}
