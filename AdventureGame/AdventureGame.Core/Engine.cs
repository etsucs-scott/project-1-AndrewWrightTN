namespace AdventureGame.Core
{
    public class Engine
    {
        private readonly Maze _maze;

        private readonly Player _player;

        private (int X, int Y) _playerPosition;

        private readonly Random _random = new();

        /// <summary>
        /// Runs the maze and gets the player and potition 
        /// </summary>
        public Engine(Maze maze, Player player)
        {
            _maze = maze;
            _player = player;
            FindStartPosition();
        }

        public (int X, int Y) PlayerPosition => _playerPosition;

        /// <summary>
        /// This ensures the first tile [1,1] is empty and the player is placed
        /// </summary>
        private void FindStartPosition()
        {
            for (int y = 1; y < _maze.Height; y++)
            {
                for (int x = 1; x < _maze.Width; x++)
                {
                    if (_maze.GetTile(x, y) == TileType.Empty)
                    {
                        _playerPosition = (x, y);
                        return;
                    }
                }
            }
            _playerPosition = (1, 1);

        }

        /// <summary>
        /// This is how the player moves and keeps it specific on only moving to empty tiles 
        /// instead of ontop of wall tiles
        /// </summary>
        public void Move(int dx, int dy)
        {
            int newX = _playerPosition.X + dx;
            int newY = _playerPosition.Y + dy;
            var target = _maze.GetTile(newX, newY);
            if (target == TileType.Wall)
            {
                return;
            }
            HandleTile(newX, newY);
            _playerPosition = (newX, newY);
        }
        /// <summary>
        /// This function is how the game handles jumping from the other tiles other than wall or 
        /// an empty spot
        /// </summary>
        private void HandleTile(int x, int y)
        {
            var tile = _maze.GetTile(x, y);
            switch (tile)
            {
                case TileType.Monster:
                    Console.WriteLine("A monster appears!");
                    RunBattle(new Monster("Goblin", _random.Next(30, 51)));
                    _maze.SetTile(x, y, TileType.Empty);
                    break;
                case TileType.Weapon:
                    Console.WriteLine("You found a weapon!");
                    _player.AddItem(new Weapon("Sword", _random.Next(2, 6)));
                    _maze.SetTile(x, y, TileType.Empty);
                    break;
                case TileType.Potion:
                    Console.WriteLine("You found a potion!");
                    _player.AddItem(new Potion("Healing Potion"));
                    _maze.SetTile(x, y, TileType.Empty);
                    break;
                case TileType.Exit:
                    Console.WriteLine("You reached the exit! You win!");
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// This is how the fighting is understood, it handles all the attacks and the result 
        /// of the fight
        /// </summary>
        private void RunBattle(Monster monster)
        {
            while (_player.IsDead && monster.IsDead)
            {
                _player.Attack(monster);
                if (!monster.IsDead)
                {
                    Console.WriteLine($"{monster.Name} is defeated!");
                    return;
                }
                monster.Attack(_player);
                Console.WriteLine($"Monster hits you! HP: {_player.Health}");
                if (!_player.IsDead)
                {
                    Console.WriteLine("You died! Game over!");
                    Environment.Exit(0);
                }
            }
        }
        
    }

}

