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
    class Snake
    {
        public bool IsDead { get; set; } = false;
        public bool IsDeadRendered { get; set; } = false;
        int deadAnimationIdx = 0;
        Coord positon;
        int length;
        Queue<Coord> tail;
        SnakeDirection direction;
        SnakeDirection proposedDirection = SnakeDirection.NONE;
        public Snake(int startX, int startY, int length)
        {

            positon = new Coord(startX, startY);
            this.length = length;
            direction = SnakeDirection.RIGHT;
            tail = new Queue<Coord>();
            tail.Enqueue(positon.Clone());
        }


        public void Render(SnakeGrid grid)
        {
            ChromaColor[] colors = { ChromaColor.White };
            int i = 0;
            foreach (var pos in tail)
            {
                ChromaColor color = colors[i++ % colors.Length];
                if (i == tail.Count)
                    color = ChromaColor.Yellow;
                if (IsDead)
                {
                    var coordHead = tail.Last();
                    grid.SetDataPixel(coordHead.x, coordHead.y, GridDataType.SNAKE, ChromaColor.Red);

                    color = ChromaColor.Black;
                    if (i >= deadAnimationIdx)
                    {
                        deadAnimationIdx++;
                        grid.SetDataPixel(pos.x, pos.y, GridDataType.SNAKE, color);
                        return;
                    }
                }
                grid.SetDataPixel(pos.x, pos.y, GridDataType.SNAKE, color);
            }
            if (IsDead)
            {
                IsDeadRendered = true;
            }
        }

        public void AcceptInput()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (direction != SnakeDirection.DOWN)
                            proposedDirection = SnakeDirection.UP;
                        break;
                    case ConsoleKey.DownArrow:
                        if (direction != SnakeDirection.UP)
                            proposedDirection = SnakeDirection.DOWN;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (direction != SnakeDirection.RIGHT)
                            proposedDirection = SnakeDirection.LEFT;
                        break;
                    case ConsoleKey.RightArrow:
                        if (direction != SnakeDirection.LEFT)
                            proposedDirection = SnakeDirection.RIGHT;
                        break;
                }
            }
        }

        public void update(SnakeGrid grid, ref bool foodEaten)
        {
            if (IsDead)
                return;
            if (proposedDirection != SnakeDirection.NONE)
            {
                direction = proposedDirection;
                proposedDirection = SnakeDirection.NONE;
            }
            switch (direction)
            {
                case SnakeDirection.UP:
                    positon.y--;
                    if (positon.y < 1)
                        positon.y = SnakeGrid.Height - 2;
                    break;
                case SnakeDirection.LEFT:
                    positon.x--;
                    if (positon.x < 1)
                        positon.x = SnakeGrid.Width - 2;
                    break;
                case SnakeDirection.RIGHT:
                    positon.x++;
                    if (positon.x >= SnakeGrid.Width - 1)
                        positon.x = 1;
                    break;
                case SnakeDirection.DOWN:
                    positon.y++;
                    if (positon.y >= SnakeGrid.Height - 1)
                        positon.y = 1;
                    break;
            }
            if (grid.GetGridDataAt(positon.x, positon.y) == GridDataType.FOOD)
            {
                ++length;
                foodEaten = true;
            }

            tail.Enqueue(positon.Clone());
            while (tail.Count > length)
            {
                Coord removedTail = tail.Dequeue();
                grid.SetDataPixel(removedTail.x, removedTail.y, GridDataType.EMPTY);
            }

            if (grid.GetGridDataAt(positon.x, positon.y) == GridDataType.SNAKE)
            {
                IsDead = true;
            }

        }


    }
}
