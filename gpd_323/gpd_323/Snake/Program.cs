
using System;

namespace Program
{
    public static class Program
    {

        //the current length of the snake
        static int snakeLength = 100;

        //the positions of the snakes elements
        static List<(int left, int top)> SnakePos = new List<(int left, int top)>();
        static (int left, int top) SnakeHeadPos;

        //init random
        static Random random = new Random();

        //spawn first food
        static (int left, int top) foodPos = (0, 0);
        public static void Main()
        {
            //set start position
            DirectionTypes direction = DirectionTypes.Right;
            Console.SetCursorPosition(5, 5);
            SnakeHeadPos = (Console.CursorLeft, Console.CursorTop);

            SpawnFood();

            //game Loop
            while (true)
            {
                //player input
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);


                    if (key.Key == ConsoleKey.W)
                        direction = DirectionTypes.Up;

                    if (key.Key == ConsoleKey.A)
                        direction = DirectionTypes.Left;

                    if (key.Key == ConsoleKey.S)
                        direction = DirectionTypes.Down;

                    if (key.Key == ConsoleKey.D)
                        direction = DirectionTypes.Right;
                }

                (int left, int top) newPos = (0, 0);

                //move snake in a direction
                switch (direction)
                {
                    case DirectionTypes.Up:
                        newPos = (SnakeHeadPos.left, SnakeHeadPos.top - 1);
                        break;
                    case DirectionTypes.Down:
                        newPos = (SnakeHeadPos.left, SnakeHeadPos.top + 1);
                        break;
                    case DirectionTypes.Left:
                        newPos = (SnakeHeadPos.left - 2, SnakeHeadPos.top);
                        break;
                    case DirectionTypes.Right:
                        newPos = (SnakeHeadPos.left + 2, SnakeHeadPos.top);
                        break;
                    default:
                        break;
                }

                if (newPos.left < 0)
                    newPos.left = Console.WindowWidth - 1;

                if (newPos.left > Console.WindowWidth - 1)
                    newPos.left = 0;

                if (newPos.top < 0)
                    newPos.top = Console.WindowHeight - 1;

                if (newPos.top > Console.WindowHeight - 1)
                    newPos.top = 0;


                SnakeHeadPos = newPos;

                

                //check if the snake is colliding with itself
                foreach ((int left, int top) item in SnakePos)
                {
                    (int left, int top) pos = SnakeHeadPos;

                    if (item == pos)
                    {
                        Environment.Exit(0);
                    }
                }

                //check if the snake is on the position of the food
                if (foodPos == SnakeHeadPos)
                {
                    //grow snake
                    snakeLength++;
                    SpawnFood();
                }

                //write a new character for the snake
                Console.SetCursorPosition(SnakeHeadPos.left, SnakeHeadPos.top);
                Console.Write('#');

                //save the position of the new snake element
                SnakePos.Add(SnakeHeadPos);

                //delete the last character from the snake
                if (SnakePos.Count > snakeLength)
                {
                    Console.SetCursorPosition(SnakePos[0].left, SnakePos[0].top);
                    Console.Write(" ");
                    SnakePos.RemoveAt(0);
                }

                Thread.Sleep(50);
            }



        }

        public static void SpawnFood()
        {
            do
            {
                foodPos = (random.Next(0, Console.WindowWidth), random.Next(0, Console.WindowHeight));
            } while ((foodPos.left % 2) == 0);

            Console.SetCursorPosition(foodPos.left, foodPos.top);
            Console.Write('*');
        }
    }
    public enum DirectionTypes
    {
        Up,
        Down,
        Left,
        Right,
    }
}

