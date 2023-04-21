using Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpd_323.Snake
{
    internal class Snake
    {

        // The current length of the snake
        int snakeLength = 5;

        // The positions of the snake's elements
        List<(int left, int top)> snakePositions = new List<(int left, int top)>();

        // Initialize random
        Random random = new Random();

        (int left, int top) foodPosition;

        // Set start position
        DirectionTypes direction = DirectionTypes.Right;

        public void Run()
        {

            SpawnFood();
            Console.SetCursorPosition(5, 5);

            // Game loop
            while (true)
            {
                // Player input
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

                // Move snake in a direction
                switch (direction)
                {

                    case DirectionTypes.Up:
                        Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                        break;
                    case DirectionTypes.Down:
                        Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 1);
                        break;
                    case DirectionTypes.Left:
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        break;
                    case DirectionTypes.Right:
                        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                        break;
                    default:
                        break;
                }

                // Check if the snake is colliding with itself
                foreach ((int left, int top) item in snakePositions)
                {
                    (int left, int top) pos = (Console.CursorLeft, Console.CursorTop);

                    if (item == pos)
                    {
                        Environment.Exit(0);
                    }
                }

                // Check if the snake is on the position of the food
                if (foodPosition == (Console.CursorLeft, Console.CursorTop))
                {
                    // Grow snake
                    snakeLength++;
                    SpawnFood();
                    SpawnFood();

                    // SpawnFood('Ö', ConsoleColor.Cyan);
                }

                // Write a new character for the snake
                Console.Write('#');
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                // Save the position of the new snake element
                snakePositions.Add((Console.CursorLeft, Console.CursorTop));

                // Delete the last character from the snake
                if (snakePositions.Count > snakeLength)
                {
                    (int left, int top) cursorPos = (Console.CursorLeft, Console.CursorTop);
                    Console.SetCursorPosition(snakePositions[0].left, snakePositions[0].top);
                    Console.Write(" ");
                    Console.SetCursorPosition(cursorPos.left, cursorPos.top);

                    snakePositions.RemoveAt(0);
                }

                Thread.Sleep(50);
            }

        }

        /// <summary>
        /// Write a symbol!
        /// </summary>
        /// <param name="food">The character that will be written</param>
        /// <param name="color"></param>
        void SpawnFood(char food, ConsoleColor color)
        {
            (int left, int top) cursorPos = (Console.CursorLeft, Console.CursorTop);
            foodPosition = (random.Next(0, Console.WindowWidth), random.Next(0, Console.WindowHeight));
            Console.SetCursorPosition(foodPosition.left, foodPosition.top);
            Console.ForegroundColor = color;
            Console.Write(food);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(cursorPos.left, cursorPos.top);
        }

        private void SpawnFood()
        {
            SpawnFood('*', ConsoleColor.DarkCyan);
        }
    }
}
