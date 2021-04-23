using System;
using System.Collections.Generic;
using Raylib_cs;

namespace Slutprojektet
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Att fixa:
            - så att ClearMatches faktiskt tar bort ur rutnätet, FUNKAR på horisontella matcher
            - queues per rad som är 5 långa hela tiden och är redo att lägga till nya godisar  ....no
            - faktisk tilläggning av nya godisar genom "gravity" oooo
            - the miracle of interaction with the actual game
            */

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
            PrintGrid();
            Console.ReadLine();
            matches = CheckMatches(matches);
            while (matches.Count > 0)
            {
                if (matches.Count > 0)
            {
                ClearMatches(matches);
            System.Console.WriteLine("cleared current matches");
            //skriver ut rutnätet
            PrintGrid();
            }
            else
            {
                System.Console.WriteLine("no matches found!");
            }
            matches = CheckMatches(matches);
            Console.ReadLine();
            }
            
            System.Console.WriteLine("no matches found!");
            
            
            Console.WriteLine();
            Console.ReadLine();
            
            
        }

        static void PrintGrid()
        {
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
        }
        
        //kollar om det är fler än 3 godisar av samma färg i rad 
        static Queue<Match> CheckMatches(Queue<Match> m)
        {
            //variabel som håller koll på
            int l = 0;
            //endast för att skriva ut färgen i debugging texten
            string color = "";
            int currentX = 0;

            //horisontellt
            for (int y = 0; y < Candy.grid.GetLength(1); y++)
            {
                for (int x = 1; x < Candy.grid.GetLength(0)-1; x++)
                {
                    //kollar om det finns 3 godisar av samma färg i rad, lägg isåfall till 1 i räknaren i
                    if(Candy.grid[x,y].color == Candy.grid[x-1,y].color && Candy.grid[x,y].color == Candy.grid[x+1,y].color)
                    {
                        l++;
                        color = Candy.grid[x,y].color;
                        //sparar matchens x-värde för att kunna lägga till det i kön senare 
                        if(l==1)
                        {
                            currentX = x;
                        }
                    }
                    
                }
                //om l är större än 0 fanns det en match och därmed läggs den till i kön
                if (l > 0)
                {
                    //skapar en ny match i kön m. l + 2 är hur många godisar i rad matchen är
                    m.Enqueue(new Match("horizontal", currentX, y, Candy.grid[currentX,y].color, l + 2));
                }
                
                
                //sätter l till 0 för att kunna kolla nästa rad
                l = 0;
            }

            l = 0;
            int currentY = 0;

            //clearar horisontella matcher först för att de vertikalas y-värde inte ska kunna ändras efter de har checkats
            ClearMatches(m);

            //vertikalt, omvänt här att den kollar "åt andra hållet", alltså kolumner från vänster till höger istället för rader uppifrån och ned.
            for (int x = 0; x < Candy.grid.GetLength(0); x++)
            {
                for (int y = 1; y < Candy.grid.GetLength(1)-1; y++)
                {

                    if(Candy.grid[x,y].color == Candy.grid[x,y-1].color && Candy.grid[x,y].color == Candy.grid[x,y+1].color)
                    {
                        l++;
                        color = Candy.grid[x,y].color;
                        //lägger till i kön om det är en match
                        if(l==1)
                        {
                            currentY = y;
                        }
                    }
                    
                    
                }

                //om l är större än 0 fanns det en match och därmed läggs den till i kön
                if (l > 0)
                {
                    //skapar en ny match i kön m. l + 2 är hur många godisar i rad matchen är
                    m.Enqueue(new Match("vertical", x, currentY, Candy.grid[x,currentY].color, l + 2));
                }
                
                //sätter l till 0 för att kunna kolla nästa rad
                l = 0;
            }

            return m;
        }

        

        static void ClearMatches(Queue<Match> m)
        {
            
            //körs så länge kön inte är tom
            while (m.Count > 0)
            {
                
                Match currentMatch = m.Dequeue();
                //lista för att lagra x-koordinaten för där matcher korsas
                List<int> crossLocations = new List<int>();
                
                System.Console.WriteLine(currentMatch.color + currentMatch.orientation + " length: " + currentMatch.length);
                
                //hanterar clearing av horisontella matcher
                if (currentMatch.orientation == "horizontal")
                {
                    //kollar först efter om matchen korsar en annan match
                    int adjacentVertical = 0;
                    for (int i = -1; i < currentMatch.length - 1; i++)
                    {
                        System.Console.WriteLine("currently checking down from x = " + currentMatch.x + i);
                        //kollar efter fler i rad nedåt
                        if (currentMatch.y < 4 && Candy.grid[currentMatch.x + i, currentMatch.y + 1].color == currentMatch.color)
                        {
                            Console.WriteLine("found 1 down on x = " + currentMatch.x + i);
                            adjacentVertical++;
                            if (currentMatch.y < 3 && Candy.grid[currentMatch.x + i, currentMatch.y + 2].color == currentMatch.color)
                            {
                                Console.WriteLine("found 2 down on x = " + currentMatch.x + i);
                                adjacentVertical++;
                                if (currentMatch.y < 2 && Candy.grid[currentMatch.x + i, currentMatch.y + 3].color == currentMatch.color)
                                {
                                    Console.WriteLine("found 3 down on x = " + currentMatch.x + i);
                                    adjacentVertical++;
                                    if (currentMatch.y < 1 && Candy.grid[currentMatch.x + i, currentMatch.y + 4].color == currentMatch.color)
                                    {
                                        Console.WriteLine("found 4 down on x = " + currentMatch.x + i);
                                        adjacentVertical++;
                                    }
                                }
                            }
                            
                        }
                        else
                            {
                                System.Console.WriteLine("negative");
                                
                            }
                        //kollar efter fler i rad uppåt.
                        System.Console.WriteLine("currently checking up from x = " + currentMatch.x + i);
                        if (currentMatch.y > 0 && Candy.grid[currentMatch.x + i, currentMatch.y - 1].color == currentMatch.color)
                        {
                            Console.WriteLine("found 1 up on x = " + currentMatch.x + i);

                            adjacentVertical++;
                            if (currentMatch.y > 1 && Candy.grid[currentMatch.x + i, currentMatch.y - 2].color == currentMatch.color)
                            {
                                Console.WriteLine("found 2 up on x = " + currentMatch.x + i);
                                adjacentVertical++;
                                if (currentMatch.y > 2 && Candy.grid[currentMatch.x + i, currentMatch.y - 3].color == currentMatch.color)
                                {
                                    Console.WriteLine("found 3 up on x = " + currentMatch.x + i);
                                    adjacentVertical++;
                                    if (currentMatch.y > 3 && Candy.grid[currentMatch.x + i, currentMatch.y - 4].color == currentMatch.color)
                                    {
                                        Console.WriteLine("found 4 up on x = " + currentMatch.x + i);
                                        adjacentVertical++;
                                    }
                                }
                            }
                            
                        }
                        else
                            {
                                System.Console.WriteLine("negative");
                            }

                        if (adjacentVertical > 1)
                        {
                            //lägger till vart det har korsats i en lista för att kunna lagra flera korsningar och checka mot dem senare.
                            crossLocations.Add(currentMatch.x + i);
                            System.Console.WriteLine("THERE's BEEN A CROSSING ");
                        }

                        adjacentVertical = 0;
                    }
                    
                    

                    //körs tills currentMatch.y är 0, då har algoritmen kommit till längst upp i rutnätet och är därmed klar
                    while (currentMatch.y > -1)
                    {
                        //
                        for (int j = 0; j < currentMatch.length; j++)
                        {
                            //if sats som ser till att där matchen har korsats med en vertikal match flyttas ingenting
                            if (crossLocations.Contains(currentMatch.x - 1 + j) == false)
                            {
                                if (currentMatch.y > 0)
                                {
                                    //flyttar ner godisar från raden över
                                   Candy.grid[currentMatch.x -1 + j, currentMatch.y] = Candy.grid[currentMatch.x -1 + j, currentMatch.y-1];

                                }
                                else
                                {
                                    //när currentMatch.y < 0 är vi längst upp och då behöver det genereras nya godisar eftersom det inte finns en rad ovanför 
                                    Candy.grid[currentMatch.x -1 + j, currentMatch.y] = new Candy();
                                }
                            }
                            
                         
                        }
                        
                        System.Console.WriteLine("current iteration:");
                        System.Console.WriteLine("y = " + currentMatch.y);
                        System.Console.WriteLine();
                        PrintGrid();
                        Console.ReadLine();
                        currentMatch.y--;
                    }
                    
                }

                //hanterar clearing av vertikala matcher
                if (currentMatch.orientation == "vertical")
                {

                    //om y är 1 är matchen så långt upp i rutnätet den kan vara och därmed behöver alla godisar vara nygenererade.
                    if (currentMatch.y == 1)
                    {
                        for (int i = -1; i < currentMatch.length - 1; i++)
                        {
                                
                            Candy.grid[currentMatch.x, currentMatch.y + i] = new Candy();
                            
                        }
                    }
                        
                    else if (currentMatch.y == 2)
                    {
                        //flyttar ned godisen över matchen till platsen längst ned i matchen
                        Candy.grid[currentMatch.x, currentMatch.y + currentMatch.length - 2] = Candy.grid[currentMatch.x, currentMatch.y - 2];

                        //genererar nya godisar för resten av platserna i matchen
                        for (int i = -2; i < currentMatch.length - 2; i++)
                        {
                            
                            Candy.grid[currentMatch.x, currentMatch.y + i] = new Candy();
                            
                        }
                    }

                    else if (currentMatch.y == 3)
                    {
                        //flyttar ned godisen över matchen till platsen längst ned i matchen
                        Candy.grid[currentMatch.x, currentMatch.y + currentMatch.length - 2] = Candy.grid[currentMatch.x, currentMatch.y - 2];
                        //flyttar ned godisen längst upp i rutnätet till näst längst ned
                        Candy.grid[currentMatch.x, currentMatch.y] = Candy.grid[currentMatch.x, currentMatch.y - 3];

                        //genererar nya godisar för resten av platserna i matchen
                        for (int i = -3; i < currentMatch.length - 3; i++)
                        {
                                
                             Candy.grid[currentMatch.x, currentMatch.y + i] = new Candy();
                            
                        }
                    }
                        
                    System.Console.WriteLine("y should be good now:");
                    System.Console.WriteLine();
                    PrintGrid();
                    Console.ReadLine();
                        
                    
                }
                
            }
            


        }


    }
}
