using System.Text;

namespace FourInRow;

public enum GameStatus
{
    WhiteMove,
    BlackMove,
    WhiteWin,
    BlackWin,
    Draw
}

public class Game
{

    private bool isWhiteMove;
    private int[,] map;
    int moves;
    GameStatus status;

    public Game()
    {
        Restart();
    }

    public void Restart()
    {
        map = new int[7, 6];
        status = GameStatus.WhiteMove;
        moves = 0;
    }

    public void MakeMove(int column)
    {
        switch (status)
        {
            case GameStatus.WhiteMove:
                if (!DownCoin(1, column))
                    return;
                if (Win(1))
                    status = GameStatus.WhiteWin;
                else
                    status = GameStatus.BlackMove;
                break;
            case GameStatus.BlackMove:
                if (!DownCoin(2, column))
                    return;
                if (Win(2))
                    status = GameStatus.BlackWin;
                else if (moves == 42)
                    status = GameStatus.Draw;
                else
                    status = GameStatus.WhiteMove;
                break;
        }
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
                moves++;
                return true;
            }
        }
        return false;
    }

    private bool Win(int coin)
    {
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                if (IsFourCoins(x, y, +1, 0, coin)) return true;
                if (IsFourCoins(x, y, +1, +1, coin)) return true;
                if (IsFourCoins(x, y, 0, +1, coin)) return true;
                if (IsFourCoins(x, y, -1, -1, coin)) return true;
            }
        }
        return false;
    }

    private bool IsFourCoins(int x0, int y0, int sx, int sy, int coin)
    {
        for (int j = 0; j < 4; j++)
        {
            int x = x0 + j * sx;
            int y = y0 + j * sy;
            if (!OnMap(x, y))
                return false;
            if (map[x, y] != coin)
                return false;
        }

        return true;
    }

    private bool OnMap(int x, int y)
    {
        return x >= 0 && x < 7 && y >= 0 && y < 6;
    }

    public char GetSymbolAt(int x, int y)
    {
        return map[x, y] switch
        {
            0 => '.',
            1 => 'x',
            2 => 'o',
            _ => ' '
        };
    }

    public GameStatus GetGameStatus()
    {
        return status;
    }

    public bool IsPlayable()
    {
        return status == GameStatus.BlackMove ||
                status == GameStatus.WhiteMove;
    }

    public int[,] GetMap() => (int[,])map.Clone();

}