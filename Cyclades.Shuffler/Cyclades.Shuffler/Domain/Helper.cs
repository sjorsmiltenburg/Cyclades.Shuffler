using System.Collections.Generic;

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
}