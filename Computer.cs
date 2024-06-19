namespace FourInRow;

public class Computer
{
    int[,] map;
    public Computer(int[,] map)
    {
        this.map = map;
    }

    public int FindMove(int coin)
    {

        int bestPoints = 0;
        int bestColumn = 0;

        for(int column = 0; column < 7; column++)
        {
            if (!DownCoin(coin, column))
                continue;

            int points = 0;

            points += FindPattern("oooo") * 1000;
            points += FindPattern(".ooo") * 100;
            points += FindPattern("ooo.") * 100;
            points += FindPattern("oo.") * 30;
            points += FindPattern(".oo") * 30;
            points += FindPattern("oo.o") * 50;
            points += FindPattern("o.oo") * 50;

            points += FindPattern("xxxx") * 1000;
            points += FindPattern(".xxx") * 100;
            points += FindPattern("xxx.") * 100;
            points += FindPattern("xx.") * 30;
            points += FindPattern(".xx") * 30;
            points += FindPattern("xx.x") * 50;
            points += FindPattern("x.xx") * 50;

            Console.WriteLine(column + " " + points);

            if (points > bestPoints)
            {
                bestPoints = points;
                bestColumn = column;
            }


            UpCoin(column);
        }

        return bestColumn;
    }


   private int FindPattern(string pattern)
    {
        int points = 0;

        for(int x = 0; x < 7; x++) 
            for(int y = 0; y < 6; y++)
            {
                if (IsPattern(x, y, +1, 0, pattern)) points ++;
                if (IsPattern(x, y, +1, +1, pattern)) points ++;
                if (IsPattern(x, y, 0, +1, pattern)) points ++;
                if (IsPattern(x, y, -1, -1, pattern)) points ++;
            }

        return points;
    }

    private bool IsPattern(int x0, int y0, int sx, int sy, string pattern)
    {

        for (int j = 0; j < pattern.Length; j++)
        {
            int x = x0 + j * sx;
            int y = y0 + j * sy;
            if (!OnMap(x, y))
                return false;
            if (Compare(map[x, y], pattern[j]))
                return false;
        }

        return true;
    }

    private bool Compare(int coin, char symbol)
    {
        return coin switch
        {
            0 => symbol == '.',
            1 => symbol == 'x',
            2 => symbol == 'o',
            _ => false
        };
    }

    private bool OnMap(int x, int y)
    {
        return x >= 0 && x < 7 && y >= 0 && y < 6;
    }

    private bool DownCoin(int coin, int column)
    {
        for (int i = 0; i < 6; i++)
        {
            if (column > 6)
                return false;
            if (map[column, i] == 0)
            {
                map[column, i] = coin;
                return true;
            }
        }
        return false;
    }

    private void UpCoin(int column)
    {
        for (int i = 5; i >=0; i--)
            if (map[column, i] != 0)
            {
                map[column, i] = 0;
                return;
            }
    }
}
