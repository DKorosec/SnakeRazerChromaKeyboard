using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_chroma
{
    using Corale.Colore.Core;
    using ChromaColor = Corale.Colore.Core.Color;
    using KeyboardCustom = Corale.Colore.Razer.Keyboard.Effects.Custom;
    class SnakeSnack
    {
        Random random = new Random();
        Coord position;
        ChromaColor color;
        int color_i = 0;
        public SnakeSnack(int x, int y)
        {
            position = new Coord(x, y);
        }
        public void update(SnakeGrid grid)
        {
            ChromaColor[] colors = { ChromaColor.Red, ChromaColor.Green, ChromaColor.Blue };
            color = colors[color_i++ % colors.Length];
            grid.SetDataPixel(position.x, position.y, GridDataType.FOOD, color);
        }

        public void reposition(SnakeGrid grid)
        {
            position = grid.FindRandomEmptyCoord();
            update(grid);
        }
    }
}
