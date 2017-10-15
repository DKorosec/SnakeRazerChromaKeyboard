using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_chroma
{
    class SnakeGame
    {
        SnakeGrid grid;
        Snake snake;
        SnakeSnack snack;
        long currentTimeStamp = 0;
        float msPerFrame = 150;
        public SnakeGame()
        {
            grid = new SnakeGrid();
            snake = new Snake(1, 3, 1);
            snack = new SnakeSnack(3, 4);
            snack.reposition(grid);
        }
        private long UnixTimeNow()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
        public void UpdateAndRender(ref bool restart)
        {
            if (snake.IsDeadRendered)
            {
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.R)
                    {
                        restart = true;
                    }
                }
                grid.RenderLose();
                return;
            }
            long timeNow = UnixTimeNow();
            long diff = timeNow - currentTimeStamp;
            bool foodEaten = false;
            snake.AcceptInput();
            if (diff >= msPerFrame)
            {
                snake.update(grid, ref foodEaten);
                if (foodEaten)
                    snack.reposition(grid);
                snack.update(grid);
                Render();
                currentTimeStamp = timeNow;
            }
        }

        private void Render()
        {
            snake.Render(grid);
            grid.RenderOnKeyboard();
        }
    }
}
