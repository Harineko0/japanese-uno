using System.Collections.Generic;

namespace JapaneseUno
{
    public struct Player
    {
        private List<Card> _cards;
        public IReadOnlyList<Card> Cards => _cards;

        public Player(List<Card> cards)
        {
            _cards = cards;
        }
        
        public void PlayCard(Card card)
        {
            _cards.Remove(card);
        }
    }
}