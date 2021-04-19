using System;
using Raylib_cs;

namespace Slutprojektet
{
    class Program
    {
        static void Main(string[] args)
        {

            Random generator = new Random();

            int[,] grid = new int[5,5];

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    grid[x,y] = generator.Next(3);
                }
            }

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if(x == grid.GetLength(0)-1)
                    {
                        Console.WriteLine(grid[x,y]);
                    }
                    else
                    {
                        Console.Write(grid[x,y]);
                    }
                }
            }

            int i = 0;


            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 1; x < grid.GetLength(0)-1; x++)
                {
                    System.Console.WriteLine("i = " + i + "x = " + x);
                    if(grid[x,y] == grid[x-1,y] && grid[x,y] == grid[x+1,y])
                    {
                        i++;

                        if (i == 2)
                        {
                            Console.WriteLine("Four in a horizontal row on x-axis " + x  + " y-axis " + y);
                        }
                        else if (i == 3)
                        {
                            Console.WriteLine("oh wow five in a horizontal row on y-axis " + y);
                        }
                        else
                        {
                            Console.WriteLine("Three in a horizontal row on x-axis " + x + " y-axis " + y);
                        }
                        
                        

                    }
                    if (x == 3)
                    {
                        i = 0;
                    }
                }
            }

            i = 0;

            for (int y = 1; y < grid.GetLength(1)-1; y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {

                    if(grid[x,y] == grid[x,y-1] && grid[x,y] == grid[x,y+1])
                    {
                        i++;

                        if (i == 2)
                        {
                            Console.WriteLine("Four in a vertical row on x-axis " + x  + " y-axis " + y);
                        }
                        else if (i == 3)
                        {
                            Console.WriteLine("fucking five in a goddang vertical row on y-axis " + y);
                        }
                        else
                        {
                            Console.WriteLine("Three in a vertical row on x-axis " + x + " y-axis " + y);
                        }
                        
                        

                    }
                    if (x == 3)
                    {
                        i = 0;
                    }
                }
            }

            /*Candy test = new Candy();

            for (int y = 0; y < Candy.grid.GetLength(1); y++)
{
  for (int x = 0; x < Candy.grid.GetLength(0); x++)
  {
    Candy.grid[x, y] = test;
  }
}*/
Console.ReadLine();
            /*
            Raylib.InitWindow(700, 800, "Candy Crush, but with worse graphics");


            while (!Raylib.WindowShouldClose())
            {
                
                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.PINK);

                Raylib.EndDrawing();



            }
*/
            
        }
    }
}
