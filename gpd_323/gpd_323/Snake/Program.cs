

//the current length of the snake
int snakeLength = 5;

//the positions of the snakes elements
List<(int left, int top)> SnakePos = new List<(int left, int top)>();

//init random
Random random = new Random();

//spawn first food
(int left, int top) foodPos = (random.Next(0, Console.WindowWidth), random.Next(0, Console.WindowHeight));
Console.SetCursorPosition(foodPos.left, foodPos.top);
Console.Write('*');

//set start position
DirectionTypes direction = DirectionTypes.Right;
Console.SetCursorPosition(5, 5);


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

    //move snake in a direction
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

    //check if the snake is colliding with itself
    foreach ((int left, int top) item in SnakePos)
    {
        (int left, int top) pos = (Console.CursorLeft, Console.CursorTop);

        if (item == pos)
        {
            Environment.Exit(0);
        }
    }

    //check if the snake is on the position of the food
    if (foodPos == (Console.CursorLeft, Console.CursorTop))
    {
        //grow snake
        snakeLength++;

        (int left, int top) cursorPos = (Console.CursorLeft, Console.CursorTop);
        foodPos = (random.Next(0, Console.WindowWidth), random.Next(0, Console.WindowHeight));
        Console.SetCursorPosition(foodPos.left, foodPos.top);
        Console.Write('*');
        Console.SetCursorPosition(cursorPos.left, cursorPos.top);
    }

    //write a new character for the snake
    Console.Write('#');
    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

    //save the position of the new snake element
    SnakePos.Add((Console.CursorLeft, Console.CursorTop));

    //delete the last character from the snake
    if (SnakePos.Count > snakeLength)
    {
        (int left, int top) curserPos = (Console.CursorLeft, Console.CursorTop);
        Console.SetCursorPosition(SnakePos[0].left, SnakePos[0].top);
        Console.Write(" ");
        Console.SetCursorPosition(curserPos.left, curserPos.top);

        SnakePos.RemoveAt(0);
    }

    Thread.Sleep(50);
}



public enum DirectionTypes
{
    Up,
    Down,
    Left,
    Right,
}