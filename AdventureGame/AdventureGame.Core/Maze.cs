using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Core
{
    public class Maze
    {
        
        public int Width { get; }
        public int Height { get; }

        private readonly TileType[,] _grid;

        private readonly Random _random = new();

        /// <summary>
        /// Creates a starting maze of 15x15 that is filled with walls
        /// </summary>
        public Maze(int width = 15, int height = 15)
        {
            Width = width;
            Height = height;
            _grid = new TileType[width, height];
        }
        public TileType GetTile(int x, int y) => _grid[x, y];
        public void SetTile(int x, int y, TileType type) => _grid[x, y] = type;
        public bool IsInside(int x, int y) => x >= 0 && y >= 0 && x < Width && y < Height;

        /// <summary>
        /// This is where the maze is created, its always places the character at the [1,1] and 
        /// also starts the carvepath function to get the maze to be a maze
        /// </summary>
        public static Maze GenerateRandomMaze(int width = 15, int height = 15)
        {
            var maze = new Maze(width, height);
            var random = new Random();
           
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++) 
                    maze._grid[x, y] = TileType.Wall;
            
            maze.CarvePath(1, 1); // Starts the hallways at the same spot player is placed
            maze._grid[width - 2, height - 2] = TileType.Exit; // Exit will ALWAYS be bottom right

            // This will place all the things around the maze
            maze.PlaceRandomEntities(TileType.Monster, count: width / 2);
            maze.PlaceRandomEntities(TileType.Weapon, count: width / 3);
            maze.PlaceRandomEntities(TileType.Potion, count: width / 3);
            return maze;
        }


        /// <summary>
        /// At this point the maze is 15x15 of just walls, but once this recursive algorithm is called, 
        /// it begins to turn wall tiles into empty tiles, keeps going to the next adjacent tile and the 
        /// next and the next, always resulting in a way to get out and randomizing.
        /// </summary>
        private void CarvePath(int x, int y) 
        {
            _grid[x, y] = TileType.Empty;
            var directions = new (int dx, int dy)[]
            {
                (1,0), (-1,0), (0,1), (0,-1)
            };

            for (int i = 0; i < directions.Length; i++)
            {
                var j = _random.Next(directions.Length);
                (directions[i], directions[j]) = (directions[j], directions[i]);
            }

            foreach (var (dx, dy) in directions)
            {
                int nx = x + dx * 2;
                int ny = y + dy * 2;
                if (IsInside(nx, ny) && _grid[nx, ny] == TileType.Wall)
                {
                    _grid[x + dx, y + dy] = TileType.Empty;
                    CarvePath(nx, ny);
                }
            }
        }
        private void PlaceRandomEntities(TileType entity, int count)
        {
            int placed = 0;
            while (placed < count)
            {
                int x = _random.Next(1, Width - 1);
                int y = _random.Next(1, Height - 1);
                if (_grid[x, y] == TileType.Empty &&
                    !(x == 1 && y == 1) &&                        
                    !(x == Width - 2 && y == Height - 2))          
                {
                    _grid[x, y] = entity;
                    placed++;
                }
            }
        }
    }
}
