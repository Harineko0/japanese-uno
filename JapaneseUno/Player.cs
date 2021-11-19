using System.Collections.Generic;

namespace JapaneseUno
{
    struct Player
    {
        private List<Card> _cards;
        public IReadOnlyList<Card> Cards => _cards;

        public Player(List<Card> cards)
        {
            _cards = cards;
        }
        
        public bool PlayCard(Card card)
        {
            return _cards.Remove(card);
        }
    }
}