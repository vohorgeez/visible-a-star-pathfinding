using System;

class GridBasics
{
    static void Main()
    {
        bool[,] blocked = new bool[3,3];
        blocked[1,2] = true;

        Console.WriteLine(blocked[1,2]);
    }
}