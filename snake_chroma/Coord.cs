﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_chroma
{
    class Coord
    {
        public int x { get; set; }
        public int y { get; set; }
        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Coord Clone()
        {
            return new Coord(x, y);
        }
    }
}
