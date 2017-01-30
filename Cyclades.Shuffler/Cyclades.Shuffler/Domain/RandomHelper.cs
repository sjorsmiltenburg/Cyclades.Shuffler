using System;

namespace Cyclades.Shuffler.Domain
{
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
}