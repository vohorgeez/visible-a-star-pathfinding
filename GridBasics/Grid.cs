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
}