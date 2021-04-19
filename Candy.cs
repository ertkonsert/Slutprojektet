using System;
using System.Collections.Generic;

namespace Slutprojektet
{
    public class Candy
    {
        static Random generator = new Random();

        string[] colors = {"red", "green", "yellow", "blue",};

        string color;
        int x = 0;
        int y = 0;

        public static Candy[,] grid = new Candy[10, 10];

        public Candy()
        {
            color = colors[generator.Next(4)];
            grid[x,y] = this;
        }



    }
}
