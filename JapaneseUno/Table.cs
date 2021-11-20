using System.Collections.Generic;
using System.Linq;

namespace JapaneseUno
{

    public class Table
    {
        private List<Player> _players;
        private Stack<Card> _layout = new Stack<Card>(); // 場札
        private List<Table> _history = new List<Table>();
        public Player Playing => _players[_nextOrder];
        private int _nextOrder;

        public IReadOnlyList<Player> Players => _players;
        public IReadOnlyCollection<Card> Layout => _layout;
        public int NextOrder => _nextOrder;
        public IReadOnlyList<Table> History => _history;
        public int WinNumber => _players.FindIndex(x => x.Cards.Count == 0);
        public int Turn => _history.Count - 1;
        
        public Table(List<Player> players)
        {
            _players = players;
            _history.Add(this);
        }
        
        public override string ToString()
        {
            string logPlayer = "";
            foreach (var player in _players)
            {
                logPlayer += "\n" + _players.IndexOf(player) + "; ";
                foreach (var card in player.Cards)
                {
                    logPlayer += card + ", ";
                }
            }

            return "Turn: " + _history.Count + "\nNextOrder: " + _nextOrder + "\nPlayers: " + logPlayer + "\nLayout: " + (_layout.Count == 0 ? "nothing" : _layout.Peek().ToString());
        }
        
        public bool IsEnd()
        {
            return _players.FindAll(player => player.Cards.Count != 0).Count <= 1;
        }

        public bool IsPassable()
        {
            return _layout.Count != 0 && _players.FindAll(player => player.Cards.Max() > _layout.Peek()).Count == 0;
        }

        public bool CanPlayCard(Card card)
        {
            return _layout.Count == 0 || card > _layout.Peek();
        }

        public Table Clone()
        {
            List<Player> players = new List<Player>();
            foreach (var player in _players)
            {
                players.Add(new Player(player.Cards.ToList()));
            }

            return new Table(players)
            {
                _layout = new Stack<Card>(_layout),
                _nextOrder = _nextOrder,
                _history = new List<Table>(_history)
            };
        }

        public void PlayCard(Card card)
        {
            bool played = Playing.PlayCard(card);
            if (played)
            {
                _layout.Push(card);
                NextTurn();
            }
        }

        public void Pass()
        {
            _layout.Clear();
            NextTurn();
        }

        private void NextTurn()
        {
            _nextOrder += 1;
            if (_nextOrder > _players.Count - 1)
            {
                _nextOrder = 0;
            }
            
            _history.Add(this);
        }
    }
}