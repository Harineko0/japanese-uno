using System;

namespace JapaneseUno
{
    public struct Card : IComparable
    {
        private int _number;
        public int Number => _number;

        public Card(int number)
        {
            this._number = number;
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
            return (_number + 2).ToString();
        }

        public int CompareTo(object? obj)
        {
            if (obj is Card card)
            {
                return _number - card._number;
            }

            return -1;
        }
    }
}