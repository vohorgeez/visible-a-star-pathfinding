using System;

class Program
{
    static void Main()
    {
        int width = 3;
        int height = 3;

        bool[,] blocked = new bool[width, height];
        blocked[1,2] = true;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                string cellState = blocked[x, y] ? "X" : ".";
                Console.WriteLine($"Cell ({x},{y}) = {cellState}");
            }
        }
    }
}