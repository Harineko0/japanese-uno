using System;
using System.Collections.Generic;
using System.Linq;

namespace JapaneseUno
{

    class Table
    {
        private List<Player> _players = new List<Player>();
        private Stack<Card> _layout = new Stack<Card>(); // 場札

        public Player Playing => _players[_nextOrder];
        public int _nextOrder;
        public int turn;

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

            return "Turn: " + turn + "\nNextOrder: " + _nextOrder + "\nPlayers: " + logPlayer + "\nLayout: " + (_layout.Count == 0 ? "nothing" : _layout.Peek().ToString());
        }

        public bool IsEnd()
        {
            return _players.FindAll(player => player.Cards.Count != 0).Count <= 1;
        }

        public Table Clone()
        {
            Table table = new Table();
            foreach (var player in _players)
            {
                table.AddPlayer(new Player(player.Cards.ToList()));
            }
            table._layout = new Stack<Card>(_layout);
            table._nextOrder = _nextOrder;
            table.turn = turn;
            return table;
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
            Console.WriteLine("Passed successfully.");
            _layout.Clear();
            NextTurn();
        }

        public bool Passable()
        {
            return _layout.Count != 0 && _players.FindAll(player => player.Cards.Max() > _layout.Peek()).Count == 0;
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }

        private void NextTurn()
        {
            turn += 1;
            _nextOrder += 1;
            if (_nextOrder > _players.Count - 1)
            {
                _nextOrder = 0;
            }
        }

        public bool CanPlayCard(Card card)
        {
            return _layout.Count == 0 || card > _layout.Peek();
        }
    }

}