using AdventureGame.Core;
using System.Security.Claims;
namespace AdventureGame
{
    public static class Program
    {
        public static void Main()
        {
            Console.Title = "Adventure Game";
            // Create player and maze
            var player = new Player("Hero");
            var maze = Maze.GenerateRandomMaze(width: 15, height: 15);
            var engine = new Engine(maze, player);

            // Game loop
            while (true)
            {
                Console.Clear();
                DrawMaze(maze, engine.PlayerPosition);
                Console.WriteLine();
                Console.WriteLine($"Health: {player.Health}");
                Console.WriteLine("Move using W, A, S, D — press Q to quit");
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.W: engine.Move(0, -1); break;
                    case ConsoleKey.S: engine.Move(0, 1); break;
                    case ConsoleKey.A: engine.Move(-1, 0); break;
                    case ConsoleKey.D: engine.Move(1, 0); break;
                    case ConsoleKey.Q:
                        Console.WriteLine("You have quit the game.");
                        return;
                    default:
                        Console.WriteLine("Invalid key. Use W, A, S, D or Q.");
                        break;
                }
            }
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="maze"></param>
       /// <param name="playerPos"></param>
        private static void DrawMaze(Maze maze, (int X, int Y) playerPos)
        {
            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    if ((x, y) == playerPos)
                    {
                        Console.Write("@ ");
                        continue;
                    }
                    char symbol = maze.GetTile(x, y) switch
                    {
                        TileType.Wall => '#',
                        TileType.Empty => '.',
                        TileType.Monster => 'M',
                        TileType.Weapon => 'W',
                        TileType.Potion => 'P',
                        TileType.Exit => 'E',
                        _ => '?'
                    };
                    Console.Write(symbol + " ");
                }
                Console.WriteLine();
            }
        }
    }
}