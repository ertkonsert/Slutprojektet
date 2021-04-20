using System;
using System.Collections.Generic;
using Raylib_cs;

namespace Slutprojektet
{
    class Program
    {
        static void Main(string[] args)
        {

            Random generator = new Random();
            Queue<Match> matches = new Queue<Match>();

            //skapar godisar på varje plats i rutnätet
            for (int y = 0; y < Candy.grid.GetLength(1); y++)
            {
                for (int x = 0; x < Candy.grid.GetLength(0); x++)
                {
                    Candy.grid[x,y] = new Candy();
                }
            }

            //skriver ut rutnätet med godisarna i
            for (int y = 0; y < Candy.grid.GetLength(1); y++)
            {
                for (int x = 0; x < Candy.grid.GetLength(0); x++)
                {
                    if(x == Candy.grid.GetLength(0)-1)
                    {
                        Console.WriteLine(Candy.grid[x,y].color);
                    }
                    else
                    {
                        Console.Write(Candy.grid[x,y].color + " ");
                    }
                }
            }

            matches = CheckMatches(matches);
            Console.ReadLine();
            ClearMatches(matches);

            Console.ReadLine();
            
            
        }

        //kollar om det är fler än 3 godisar av samma färg i rad 
        static Queue<Match> CheckMatches(Queue<Match> m)
        {
            
            

            int i = 0;
            //endast för att skriva ut färgen i debugging texten
            string color = "";
            

            //horisontellt
            for (int y = 0; y < Candy.grid.GetLength(1); y++)
            {
                
                for (int x = 1; x < Candy.grid.GetLength(0)-1; x++)
                {
                    //kollar om det finns 3 godisar av samma färg i rad, lägg isåfall till 1 i räknaren i
                    if(Candy.grid[x,y].color == Candy.grid[x-1,y].color && Candy.grid[x,y].color == Candy.grid[x+1,y].color)
                    {
                        i++;
                        color = Candy.grid[x,y].color;
                        //lägger till i kön om det är en match 
                        if(i==1)
                        {
                            m.Enqueue(new Match(x, y, Candy.grid[x,y].color));
                        }
                    }
                    
                }
                //beroende på i:s värde var det antingen 3, 4 eller 5 i rad. nope är bara med för att underlätta för mig att hitta på vilken rad det är
                if (i == 1)
                {
                    Console.WriteLine("tre i rad av " + color);
                    
                }
                else if (i == 2)
                {
                    Console.WriteLine("fyra i rad av " + color);
                }
                else if (i == 3)
                {
                    Console.WriteLine("fem i rad av " + color);
                }
                else
                {
                    Console.WriteLine("nope");
                }
                
                i = 0;
            }

            i = 0;
            System.Console.WriteLine("WE CHECKING VERTICAL NOW ALRIGHT");
            //vertikalt, omvänt här att den kollar "åt andra hållet", alltså kolumner från vänster till höger istället för rader uppifrån och ned.
            for (int x = 0; x < Candy.grid.GetLength(0); x++)
            {
                for (int y = 1; y < Candy.grid.GetLength(1)-1; y++)
                {

                    if(Candy.grid[x,y].color == Candy.grid[x,y-1].color && Candy.grid[x,y].color == Candy.grid[x,y+1].color)
                    {
                        i++;
                        color = Candy.grid[x,y].color;
                        //lägger till i kön om det är en match
                        if(i==1)
                        {
                            m.Enqueue(new Match(x, y, Candy.grid[x,y].color));
                        }
                    }
                    
                    
                }

                //beroende på i:s värde var det antingen 3, 4 eller 5 i rad. nope är bara med för att underlätta för mig att hitta på vilken rad det är
                if (i == 1)
                {
                    Console.WriteLine("tre i rad av " + color);
                }
                else if (i == 2)
                {
                    Console.WriteLine("fyra i rad av " + color);
                }
                else if (i == 3)
                {
                    Console.WriteLine("fem i rad av " + color);
                }
                else
                {
                    Console.WriteLine("nope");
                }
                
                i = 0;
            }

            return m;
        }

        static void ClearMatches(Queue<Match> m)
        {
                System.Console.WriteLine(m.Count);

            for (int i = 0; i < m.Count+1; i++)
            {
                /*Match currentMatch;
                bool y = m.TryDequeue(out currentMatch);*/
                Console.WriteLine(m.Dequeue().color);
            }
            


        }


    }
}
