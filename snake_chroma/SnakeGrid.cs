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
    class SnakeGrid
    {
        KeyboardCustom grid;
        public const int Width = 14;
        public const int Height = 6;

        GridDataType[,] gameGrid = new GridDataType[Height, Width];

        private void initGrid()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    var pixelType = (i != 0 && i != Height - 1 && j != 0 && j != Width - 1) ? GridDataType.EMPTY : GridDataType.WALL;
                    SetDataPixel(j, i, pixelType);
                }
            }
        }

        private void clearGrid()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    SetDataPixel(j, i, GridDataType.EMPTY);
                }
            }
        }

        public GridDataType GetGridDataAt(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                throw new IndexOutOfRangeException("x or y coordinates out of range!");
            return gameGrid[y, x];
        }

        public void SetDataPixel(int x, int y, GridDataType pixelType)
        {
            ChromaColor c = ChromaColor.Black;
            switch (pixelType)
            {
                case GridDataType.WALL:
                    c = ChromaColor.Green;
                    break;
                case GridDataType.EMPTY:
                    c = ChromaColor.Black;
                    break;
            }
            SetDataPixel(x, y, pixelType, c);
        }

        public Coord FindRandomEmptyCoord()
        {
            Random r = new Random();
            while (true)
            {
                int x = r.Next(1, Width - 1);
                int y = r.Next(1, Height);
                var data = GetGridDataAt(x, y);
                if (data == GridDataType.EMPTY)
                    return new Coord(x, y);
            }
        }

        public void SetDataPixel(int x, int y, GridDataType pixelType, ChromaColor color)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                throw new IndexOutOfRangeException("x or y coordinates out of range!");
            grid[y, x + 1] = color;
            gameGrid[y, x] = pixelType;
        }

        public void RenderOnKeyboard()
        {
            Chroma.Instance.Keyboard.SetCustom(grid);
        }

        public void RenderLose()
        {
            clearGrid();
            //L
            ChromaColor c = ChromaColor.Red;
            for (int i = 1; i < 6; i++)
            {
                grid[i, 1] = c;
            }
            for (int i = 0; i < 3; i++)
            {
                grid[5, i + 1] = c;
            }

            //O
            c = ChromaColor.Green;
            for (int i = 1; i < 5; i++)
            {
                grid[i, 3 + (i == 4 ? 1 : 0)] = c;
            }
            for (int i = 1; i < 5; i++)
            {
                grid[i, 5 + (i == 4 ? 1 : 0)] = c;
            }
            grid[1, 4] = c;
            grid[4, 5] = c;

            //S
            c = ChromaColor.Red;
            for (int i = 0; i < 3; i++)
            {
                grid[0, i + 7] = c;
            }
            grid[1, 7] = c;
            for (int i = 0; i < 3; i++)
            {
                grid[2, i + 7] = c;
            }
            grid[3, 9] = c;
            for (int i = 0; i < 3; i++)
            {
                grid[4, i + 7] = c;
            }
            //E
            c = ChromaColor.Green;
            for (int i = 0; i < 4; i++)
            {
                grid[0, i + 11] = c;
            }
            grid[1, 12] = c;
            grid[2, 11] = c;
            for (int i = 1; i < 3; i++)
            {
                grid[3, i + 11] = c;
            }
            grid[3, 11] = c;
            grid[4, 11] = c;
            for (int i = 0; i < 4; i++)
            {
                grid[5, i + 11] = c;
            }

            RenderOnKeyboard();
        }

        public SnakeGrid()
        {
            Chroma.Instance.Keyboard.Clear();
            grid = KeyboardCustom.Create();
            initGrid();
        }
    }
}
