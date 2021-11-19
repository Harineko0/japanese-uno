using System;

namespace JapaneseUno
{
    struct Card : IComparable
    {
        public int number;
        public int Number => number;

        public Card(int number)
        {
            this.number = number;
        }
        
        public static bool operator >(Card a, Card b)
        {
            return a.Number > b.Number;
        }
        
        public static bool operator <(Card a, Card b)
        {
            return a.Number < b.Number;
        }

        public override string ToString()
        {
            return "" + number;
        }

        public int CompareTo(object? obj)
        {
            if (obj is Card card)
            {
                return number - card.number;
            }

            return -1;
        }
    }
}