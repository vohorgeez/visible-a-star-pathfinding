using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public int Width { get; }
    public int Height { get; }

    private bool[,] blocked;
    private int[,] cost;

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;

        blocked = new bool[width, height];
        cost = new int[width, height];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                cost[x, y] = 1;
    }

    public void SetBlocked(int x, int y, bool value)
    {
        blocked[x, y] = value;
    }

    public bool IsBlocked(int x, int y)
    {
        return blocked[x, y];
    }

    public IEnumerable<Vector2Int> GetNeighbors4(int x, int y)
    {
        if (x > 0 && !blocked[x - 1, y])
            yield return new Vector2Int(x - 1, y);

        if (x < Width - 1 && !blocked[x + 1, y])
            yield return new Vector2Int(x + 1, y);

        if (y > 0 && !blocked[x, y - 1])
            yield return new Vector2Int(x, y - 1);

        if (y < Height - 1 && !blocked[x, y + 1])
            yield return new Vector2Int(x, y + 1);
    }

    public void SetCost(int x, int y, int value)
    {
        if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Cost must be > 0.");
    }

    public int GetCost(int x, int y)
    {
        return cost[x, y];
    }
}