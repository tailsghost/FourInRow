namespace FourInRow
{
    internal class Program
    {
       static Game game;
       static Computer computer;
        static void Main(string[] args)
        {
            Start();
            Console.ReadKey();
        }

        static void Start()
        {
            game = new Game();
            while (game.IsPlayable())
            {
                Print();

               int column =  IsValidChar(Console.ReadKey());

                if (column >= 0)
                {
                    game.MakeMove(column);

                    computer = new Computer(game.GetMap());
                    column = computer.FindMove(2);
                    game.MakeMove(column);
                }
            }
            Print();
            Console.WriteLine(game.GetGameStatus().ToString());
        }

        static int IsValidChar(ConsoleKeyInfo key)
        {
            Console.WriteLine();
            if (char.IsDigit(key.KeyChar))
                return int.Parse(key.KeyChar.ToString());
            return -1;
        }

        static void Print()
        {
            for (int y = 5; y >= 0; y--)
            {
                for (int x = 0; x < 7; x++)
                {
                    Console.Write(game.GetSymbolAt(x, y) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("0 1 2 3 4 5 6");
        }
    }
}
