using System.Collections.Generic;
using System.Linq;

namespace JapaneseUno
{

    class Table
    {
        private List<Player> _players = new List<Player>();
        public Stack<Card> Layout = new Stack<Card>(); // 場札

        public Player Playing => _players[_order];
        public int _order;
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

            return "Turn: " + turn + "\nLayout: " + (Layout.Count == 0 ? "0" : Layout.Peek().ToString())+ "\nOrder: " + _order + "\nPlayers: " + logPlayer;
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
            table.Layout = new Stack<Card>(Layout);
            table._order = _order;
            table.turn = turn;
            return table;
        }

        public void PlayCard(Card card)
        {
            bool played = Playing.PlayCard(card);
            if (played)
            {
                Layout.Push(card);
                ProceedOrder();
            }
        }

        public void Pass()
        {
            if (Passable())
            {
                Layout.Clear();
                ProceedOrder();
            }
        }

        public bool Passable()
        {
            return Layout.Count != 0 && _players.FindAll(player => player.Cards.Max() > Layout.Peek()).Count <= 1;
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }

        private void ProceedOrder()
        {
            turn += 1;
            _order += 1;
            if (_order > _players.Count - 1)
            {
                _order = 0;
            }
        }
    }

}