using System;
using System.Collections.Generic;
using System.Linq;

namespace JapaneseUno
{

    public class Table
    {
        private List<Player> _players;
        public Stack<Card> Layout = new Stack<Card>(); // 場札
        private List<Table> _history = new List<Table>();
        public Player Playing => _players[_nextOrder];
        private int _nextOrder;

        public IReadOnlyList<Player> Players => _players;
        public int NextOrder => _nextOrder;

        private int PrevOrder
        {
            get
            {
                int order = _nextOrder - 1;
                if (order < 0)
                {
                    order = _players.Count - 1;
                }

                return order;
            }
        }
        
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

            var layout = Layout.Select(x => x.ToString());
            return "Turn: " + _history.Count + "\nNextOrder: " + _nextOrder + "\nPlayers: " + logPlayer + "\nLayout: " + string.Join(", ", layout);
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
                Layout = new Stack<Card>(Layout),
                _nextOrder = _nextOrder,
                _history = new List<Table>(_history)
            };
        }

        public bool IsEnd()
        {
            return _players.FindAll(player => player.Cards.Count != 0).Count <= 1;
        }

        public bool CanPlayCard(Card card)
        {
            return Layout.Count == 0 || card > Layout.Peek();
        }

        public void PlayCard(Card card)
        {
            Playing.PlayCard(card);
            Layout.Push(card);
            NextTurn();
        }

        public bool CanPass()
        {
            return Layout.Count != 0 
                   && _players.FindAll(player => player.Cards.Count != 0 
                                                 && _players.IndexOf(player) != PrevOrder
                                                 && player.Cards.Max() > Layout.Peek()).Count == 0;
        }

        public void Pass()
        {
            Layout.Clear();
            
            _nextOrder += 1;
            if (_nextOrder > _players.Count - 1)
            {
                _nextOrder = 0;
            }
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