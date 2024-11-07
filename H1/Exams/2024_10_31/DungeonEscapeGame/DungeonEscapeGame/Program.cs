

namespace Dungeon_Escape_Game
{
    using Game;
    internal class Program
    {
        static void Main(string[] args)
        {
            RunGame();
        }
        static void RunGame()
        {
            _ = new Game(new Size(51, 51));
        }
    }
}
namespace GameConfig
{
    internal static class Settings
    {
        public static ConsoleColor UserColor = ConsoleColor.Green;
        public static ConsoleColor NormalColor = ConsoleColor.Black;
        public static ConsoleColor WallColor = ConsoleColor.Red;
        public static ConsoleColor DoorColor = ConsoleColor.DarkGreen;
        public static ConsoleColor KeyColor = ConsoleColor.Yellow;
        public static ConsoleColor TrapColor = ConsoleColor.DarkGray;
        public static ConsoleColor FrameColor = ConsoleColor.Blue;
        public static ConsoleColor HealthColor = ConsoleColor.White;
        public static int TrapCount = 3;
        public static int KeyCount = 5;
        public static int DoorCount = 1;
        public static int HealthPointCount = 3;
        public static string StepSpace = "  ";
    }
}

namespace Game
{
    using GameConfig;
    internal class Game
    {
        public Game(Size size)
        {
            new GameArea(MazeGenerator.GenerateMaze(size.Width, size.Height)).Render();
        }
    }
    #region Game Structs
    internal struct Coords(int x, int y)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
    }
    internal struct Size(int width, int height)
    {
        public int Width { get; set; } = width;
        public int Height { get; set; } = height;
    }
    internal enum StepType
    {
        Normal,
        Wall,
        Door,
        Key,
        Trap,
        Frame,
        Health,
    }
    internal class User
    {
        public int FoundKeys { get; set; }
        public int Health { get; set; }
        public Coords CurrentCoords { get; set; }
        public User()
        {
            Health = 5;
            CurrentCoords = new Coords(1, 1);
        }

        public void Render()
        {
            Console.BackgroundColor = Settings.UserColor;
            Console.Write(Settings.StepSpace);
        }
        public void Move(ref Step up, ref Step down, ref Step left, ref Step right)
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if (up._type != StepType.Wall && up._type != StepType.Frame)
                        CurrentCoords = up.Coords;
                    break;
                case ConsoleKey.DownArrow:
                    if (down._type != StepType.Wall && down._type != StepType.Frame)
                        CurrentCoords = down.Coords;
                    break;
                case ConsoleKey.RightArrow:
                    if (right._type != StepType.Wall && right._type != StepType.Frame)
                        CurrentCoords = right.Coords;
                    break;
                case ConsoleKey.LeftArrow:
                    if (left._type != StepType.Wall && left._type != StepType.Frame)
                        CurrentCoords = left.Coords;
                    break;
            }
        }
    }
    internal class Step
    {
        public Coords Coords { get; set; }
        public StepType _type;
        /// <summary>
        /// Each step is represented in a gamearea.txt
        /// </summary>
        /// <param name="type"></param>
        public Step(StepType type, Coords coords)
        {
            _type = type;
            Coords = coords;
        }
        /// <summary>
            // Initialize the maze with walls
        /// Event to trigger when user steps on a step
        /// </summary>
        public void OnUser(ref User user)
        {
            switch (_type)
            {
                case StepType.Normal:
                    break;
                case StepType.Wall:
                    break;
                case StepType.Door:
                    if (Settings.KeyCount == user.FoundKeys)
                    {
                        Console.Clear();
                        Console.WriteLine("You won!");
                        Environment.Exit(0);
                    }
                    break;
                case StepType.Key:
                    _type = StepType.Normal;
                    user.FoundKeys++;
                    break;
                case StepType.Trap:
                    user.Health -= 1;
                    if (user.Health == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("You lost!");
                        Environment.Exit(0);
                    }
                    break;
                case StepType.Health:
                    user.Health += 1;
                    _type = StepType.Normal;
                    break;
            }
        }
        public void Render()
        {
            Console.BackgroundColor = _type switch
            {
                StepType.Normal => Settings.NormalColor,
                StepType.Door => Settings.DoorColor,
                StepType.Key => Settings.KeyColor,
                StepType.Trap => Settings.TrapColor,
                StepType.Wall => Settings.WallColor,
                StepType.Frame => Settings.FrameColor,
                StepType.Health => Settings.HealthColor,
                _ => throw new NotImplementedException()
            };
            Console.Write(Settings.StepSpace);
        }
    }
    internal class GameArea
    {
        User _user;
        Size _size;
        private readonly Step[,] _steps;

        public GameArea(StepType[,] stepstypes)
        {
            _user = new User();
            _size.Width = stepstypes.GetLength(1);
            _size.Height = stepstypes.GetLength(0);
            _steps = new Step[_size.Height, _size.Width];
            for (int r = 0; r < _size.Height; r++)
                for (int c = 0; c < _size.Width; c++)
                    _steps[r, c] = new Step(stepstypes[r, c], new Coords(c, r));
        }
        public void Render()
        {
            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                Console.WriteLine($"Health: {_user.Health}");
                Console.WriteLine($"Keys found: {_user.FoundKeys}");
                for (int r = 0; r < _size.Height; r++)
                {
                    for (int c = 0; c < _size.Width; c++)
                    {
                        if (_user.CurrentCoords.X == c && _user.CurrentCoords.Y == r)
                        {
                            _steps[r, c].OnUser(ref _user);
                            _user.Render();
                        }
                        else
                            _steps[r, c].Render();
                    }
                    Console.WriteLine();
                }
                var x = _user.CurrentCoords.X;
                var y = _user.CurrentCoords.Y;
                _user.Move(ref _steps[y - 1, x], ref _steps[y + 1, x], ref _steps[y, x - 1], ref _steps[y, x + 1]);
            }
        }
    }
    internal class MazeGenerator
    {
        public static StepType[,] GenerateMaze(int width, int height)
        {
            StepType[,] maze = new StepType[height, width];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    maze[y, x] = StepType.Wall;

            for (int i = 0; i < width; i++)
            {
                maze[0, i] = StepType.Frame;
                maze[height - 1, i] = StepType.Frame;
            }
            for (int i = 0; i < height; i++)
            {
                maze[i, 0] = StepType.Frame;
                maze[i, width - 1] = StepType.Frame;
            }

            Stack<(int x, int y)> stack = new Stack<(int x, int y)>();
            var start = (x: 1, y: 1);
            maze[start.y, start.x] = StepType.Normal;
            stack.Push(start);

            Random rand = new();

            (int dx, int dy)[] directions =
            [
                (-2, 0), // Left
                (2, 0),  // Right
                (0, -2), // Up
                (0, 2)   // Down
            ];

            while (stack.Count > 0)
            {
                var current = stack.Peek();
                int x = current.x;
                int y = current.y;

                List<(int x, int y)> neighbors = new List<(int x, int y)>();

                foreach (var (dx, dy) in directions)
                {
                    int nx = x + dx;
                    int ny = y + dy;

                    if (nx > 0 && nx < width - 1 && ny > 0 && ny < height - 1 && maze[ny, nx] == StepType.Wall)
                        neighbors.Add((nx, ny));
                }

                if (neighbors.Count > 0)
                {
                    var chosen = neighbors[rand.Next(neighbors.Count)];
                    int cx = chosen.x;
                    int cy = chosen.y;

                    maze[cy, cx] = StepType.Normal;
                    maze[y + (cy - y) / 2, x + (cx - x) / 2] = StepType.Normal;

                    stack.Push(chosen);
                }
                else
                {
                    stack.Pop();
                }
            }

            // Place items in the maze
            PlaceRandomItem(maze, width, height, StepType.Door, Settings.DoorCount);
            PlaceRandomItem(maze, width, height, StepType.Key, Settings.KeyCount);
            PlaceRandomItem(maze, width, height, StepType.Trap, Settings.TrapCount);
            PlaceRandomItem(maze, width, height, StepType.Health, Settings.HealthPointCount);

            return maze;
        }

        /// <summary>
        /// Places a specified number of items randomly in accessible locations within the maze.
        /// </summary>
        /// <param name="maze">The maze array.</param>
        /// <param name="width">Width of the maze.</param>
        /// <param name="height">Height of the maze.</param>
        /// <param name="item">The StepType item to place.</param>
        /// <param name="count">Number of items to place.</param>
        private static void PlaceRandomItem(StepType[,] maze, int width, int height, StepType item, int count)
        {
            Random rand = new();
            int placed = 0;

            while (placed < count)
            {
                int x = rand.Next(1, width - 1);
                int y = rand.Next(1, height - 1);

                // Place item only on accessible paths
                if (maze[y, x] == StepType.Normal)
                {
                    maze[y, x] = item;
                    placed++;
                }
            }
        }
    }
    #endregion
}
