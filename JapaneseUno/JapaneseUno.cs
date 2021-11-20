namespace JapaneseUno
{
    class JapaneseUno
    {
        static void Main(string[] args)
        {
            TableController controller = new TableController();
            controller.Start(new GameConfig
            {
                maxCard = 3,
                playerNumber = 2,
            });
        }
    }
}