


using gpd_323.Snake;

namespace Program
{

    public static class Program
    {
        public static void Main()
        {
            Snake snake = new Snake();
            snake.Run();
            
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





