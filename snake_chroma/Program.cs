using Corale.Colore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_chroma
{

    enum GridDataType : int { SNAKE = 1, WALL = 2, FOOD = 3, EMPTY = 4 };
    enum SnakeDirection : int { LEFT = 0, UP = 1, RIGHT = 2, DOWN = 3, NONE = 9999};
    
    class Program
    {
        static void Main(string[] args)
        {
            var game = new SnakeGame();

            while (true)
            {
                bool restart = false;
                //press R to restart on death
                game.UpdateAndRender(ref restart);
                if(restart)
                {
                    game = new SnakeGame();
                }
            }
        }
    }
}
