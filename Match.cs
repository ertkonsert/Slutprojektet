using System;

namespace Slutprojektet
{
    public class Match
    {
        public int x;
        public int y;
        public string orientation = "";
        public string color = "";
        public int length;

        public Match(string o, int coordX, int coordY, string c, int l)
        {
            x = coordX;
            y = coordY;
            color = c;
            length = l;
            orientation = o;
        }

    }
}
