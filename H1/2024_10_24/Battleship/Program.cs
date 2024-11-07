using Lib;

namespace battleship
{
    public class GameArea
    {
        // Null is for empty places
        // true is for hit/ship
        // false is for missed/hitship
        public bool?[,] Area;
        public System.Drawing.Size Size;
        public GameArea(bool is_input, System.Drawing.Size size)
        {
            Area = new bool?[size.Height, size.Width];
            Size = size;
            if (is_input)
            {
                for (int r = 0; r < size.Height; r++)
                {
                    for (int c = 0; c < size.Width; c++)
                    {
                        //Read the key 
                        var key = Console.ReadKey(false);
                        var point = Console.GetCursorPosition();
                        Console.SetCursorPosition(point.Left - 1, point.Top);
                        if (key.Key == ConsoleKey.Y)
                        {
                            Area[r, c] = true;
                            Console.Write("■");
                        }
                        else
                        {
                            Console.Write(" ");
                            Area[r, c] = null;
                        }
                    }

                    Console.WriteLine();
                }
            }

        }
    }
    public class Player
    {
        public GameArea area;
        public GameArea shots_fired;
        public Player(System.Drawing.Size size)
        {
            area = new(true, size);
            shots_fired = new(false, size);
        }
        public bool ShotAt(Point point)
        {
            return area.Area[point.Y, point.X] != null;
        }
        public Point Shoot()
        {
            var origin = new Point(Console.GetCursorPosition());
            ShowShots();
            Console.SetCursorPosition(origin.X, origin.Y);
            var move = Input.DynMove(origin, new Point(origin.X + area.Size.Width - 1, origin.Y + area.Size.Height - 1));
            var delta = new Point(move.X - origin.X, move.Y - origin.Y);
            return delta;
        }
        public bool IsDead()
        {
            for (int r = 0; r < area.Size.Height; r++)
                for (int c = 0; c < area.Size.Width; c++)
                    if (area.Area[r, c] == true)
                        return false;
            return true;
        }
        public void ShowShots()
        {
            for (int r = 0; r < shots_fired.Size.Height; r++)
            {
                for (int c = 0; c < shots_fired.Size.Width; c++)
                    Console.Write(shots_fired.Area[r, c] == null ? " " : shots_fired.Area[r, c] == true ? "H" : "M");
                Console.WriteLine();
            }
        }
        public void ShowArea()
        {
            for (int r = 0; r < area.Size.Height; r++)
            {
                for (int c = 0; c < area.Size.Width; c++)
                    Console.Write(area.Area[r, c] == null ? " " : area.Area[r, c] == true ? "■" : " ");
                Console.WriteLine();
            }
        }
    }
    public class Game
    {
        //       mygame    shots 
        private Player m_1player;
        private Player m_2player;

        public Game(System.Drawing.Size size)
        {
            Console.WriteLine("Player 1:");
            m_1player = new Player(size);
            Console.WriteLine("\nPlayer 2:");
            m_2player = new Player(size);
            StartGame();
        }
        private void StartGame()
        {
            bool turn = true;
            while (!IsGameEnd())
            {
                Console.Clear();
                Console.WriteLine();
                var (current_player, other_player) = (turn ? m_1player : m_2player, turn ? m_2player : m_1player);
                Console.WriteLine(turn ? "Player 1" : "Player 2");
                current_player.ShowArea();
                Console.WriteLine();
                Console.WriteLine(turn ? "Player 1: Shoot" : "Player 2: Shoot");
                var point = current_player.Shoot();
                var did_hit = other_player.ShotAt(point);
                Console.WriteLine(did_hit ? "Bim Bim Bam Bam!!!" : "Missed!");
                if (did_hit)
                    other_player.area.Area[point.Y, point.X] = null;
                current_player.shots_fired.Area[point.Y, point.X] = did_hit;
                turn = !turn;
                Console.ReadKey();
            }
            if (m_1player.IsDead())
                Console.WriteLine("Player 2 won!");
            else
                Console.WriteLine("Player 1 won!");
        }
        private bool IsGameEnd()
        {
            return m_1player.IsDead() || m_2player.IsDead();
        }
    }
    internal class Program
    {
        static void Main()
        {
            var game = new Game(new System.Drawing.Size(4, 3));
        }
    }
}
