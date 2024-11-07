using StudentAdministration;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Lib
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(in int x, in int y)
        {
            X = x;
            Y = y;
        }
        public Point((int, int) tuple)
        {
            X = tuple.Item1;
            Y = tuple.Item2;
        }
    }
    public class Parser
    {
        public static T? Parse<T>(string val)
        {
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(val) ?? default;
        }
    }
    
    public class ConsoleTable
    {
        public class ConsoleTableBorders
        {
            public string TopLeftCorner { get; set; } = "┌";
            public string BottomLeftCorner { get; set; } = "└";
            public string TopRightCorner { get; set; } = "┐";
            public string BottomRightCorner { get; set; } = "┘";
            public string Cross { get; set; } = "┼";
            public string LeftTCross { get; set; } = "├";
            public string RightTCross { get; set; } = "┤";
            public string TopTCross { get; set; } = "┬";
            public string BottomTCross { get; set; } = "┴";
            public string Vertical { get; set; } = "│";
            public string Horizontal { get; set; } = "─";
        }
    }
    public static class Input
    {
        /// <summary>
        /// Ask for a string, depending on question
        /// </summary>
        /// <param name="qestion"></param>
        /// <param name="newline"></param>
        /// <param name="qu_color"></param>
        /// <param name="ans_color"></param>
        /// <returns></returns>
        public static T? Ask<T>(string qestion, bool newline, ConsoleColor quColor = ConsoleColor.DarkBlue, ConsoleColor ansColor = ConsoleColor.Green)
        {
            var std_color = Console.ForegroundColor;
            Console.ForegroundColor = quColor;
            if (newline) Console.WriteLine(">> " + qestion);
            else Console.Write(">> " + qestion);
            Console.ForegroundColor = ansColor;
            var ans = Console.ReadLine();
            Console.ForegroundColor = std_color;
            return Parser.Parse<T>(ans ?? "");
        }

        /// <summary>
        /// Ask for a string, depending on question
        /// </summary>
        /// <param name="qestion"></param>
        /// <param name="newline"></param>
        /// <param name="qu_color"></param>
        /// <param name="ans_color"></param>
        /// <returns></returns>
        public static T? AskReg<T>(string qestion, bool newline, Regex regex, ConsoleColor quColor = ConsoleColor.DarkBlue, ConsoleColor ansColor = ConsoleColor.Green)
        {
            var (x, _) = Console.GetCursorPosition();
            var std_color = Console.ForegroundColor;
            Console.ForegroundColor = quColor;
            if (newline) Console.WriteLine(">> " + qestion);
            else Console.Write(">> " + qestion);
            Console.ForegroundColor = ansColor;

            string output = "";
            ConsoleKeyInfo c;
            while ((c = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                switch (c.Key)
                {
                    case ConsoleKey.Backspace:
                        output = output.Remove(output.Length - 1);
                        break;
                    default:
                        output += c.KeyChar;
                        break;
                }

                if (regex.IsMatch(output))
                    Console.Write(output);

            }
            Console.ForegroundColor = std_color;
            return Parser.Parse<T>(output);
        }

        /// <summary>
        /// Ask for key depending on question
        /// </summary>
        /// <param name="qestion"></param>
        /// <param name="newline"></param>
        /// <param name="print_val"></param>
        /// <param name="qu_color"></param>
        /// <param name="ans_color"></param>
        /// <returns></returns>
        public static ConsoleKey AskKey(string qestion, bool newline, bool printVal, ConsoleColor quColor = ConsoleColor.DarkBlue, ConsoleColor ansColor = ConsoleColor.Green)
        {
            var std_color = Console.ForegroundColor;
            Console.ForegroundColor = quColor;
            if (newline) Console.WriteLine(">> " + qestion);
            else Console.Write(">> " + qestion);
            Console.ForegroundColor = ansColor;
            var ans = Console.ReadKey(!printVal).Key;
            Console.WriteLine();
            Console.ForegroundColor = std_color;
            return ans;
        }

        public static Point DynMove(Point min, Point max)
        {
            var (x, y) = Console.GetCursorPosition();
            ConsoleKey key;
            while ((key = Console.ReadKey().Key) != ConsoleKey.Enter)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (y - 1 >= min.Y) y--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (y + 1 <= max.Y) y++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (x - 1 >= min.X) x--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (x + 1 <= max.X) x++;
                        break;
                }
                Console.SetCursorPosition(x, y);
            }
            return new Point(x, y);
        }

        public static MethodInfo GenerateMenu(Type type, Func<MethodInfo, bool> expr, Func<string, string> name_gen)
        {
            var origin = new Point(Console.GetCursorPosition());
            var flags = BindingFlags.Default | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            var items = type.GetMethods(flags).Where(expr).ToArray();
            foreach (var item in items)
                Console.WriteLine(name_gen(item.Name));
            Console.SetCursorPosition(origin.X, origin.Y);
            var move = DynMove(new Point(origin.X, origin.Y), new Point(origin.X, origin.Y + items.Length - 1));
            var delta_index = Math.Abs(origin.Y - move.Y);
            return items[delta_index];
        }
    }
}
