using System;
using System.Collections.Generic;

namespace Slutprojektet
{
    public class Candy
    {
        static Random generator = new Random();

        string[] colors = {"red   ", "green ", "yellow",};
        

        public string color;
        

        public static Candy[,] grid = new Candy[5,5];

        public Candy()
        {
            color = colors[generator.Next(3)];
            //grid[x,y] = this;
        }



    }
}
