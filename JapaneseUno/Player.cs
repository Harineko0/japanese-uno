using System.Collections.Generic;

namespace JapaneseUno
{
    public readonly struct Player
    {
        private readonly List<Card> _cards;
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