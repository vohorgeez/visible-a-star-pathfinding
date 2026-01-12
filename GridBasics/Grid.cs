using System.Collections.Generic;

public class Grid
{
    public int Width { get; }
    public int Height { get; }

    private bool[,] blocked;

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
        blocked = new bool[width, height];
    }

    public void SetBlocked(int x, int y, bool value)
    {
        blocked[x, y] = value;
    }

    public bool IsBlocked(int x, int y)
    {
        return blocked[x, y];
    }

    public IEnumerable<(int x, int y)> GetNeighbors4(int x, int y)
    {
        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };

        for (int i = 0; i < 4 ; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];

            if (nx >= 0 && nx < Width &&
                ny >= 0 && ny < Height &&
                !IsBlocked(nx, ny))
            {
                yield return (nx, ny);
            }
        }
    }
}