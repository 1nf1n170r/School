using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Assignment
{
    static class CM
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
            Console.WriteLine("");
            try
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(ans) ?? default;
            }
            catch (Exception)
            {
                return default;
            }
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
            var std_color = Console.ForegroundColor;
            Console.ForegroundColor = quColor;
            if (newline) Console.WriteLine(">> " + qestion);
            else Console.Write(">> " + qestion);
            Console.ForegroundColor = ansColor;
            var ans = Console.ReadLine();
            Console.WriteLine();
            Console.ForegroundColor = std_color;

            while (true)
            {

            }
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
    }
    internal class Program
    {
        private static void Main(string[] args)
        {
            RealMain();
        }

        /// <summary>
        /// Using custom methods
        /// </summary>
        private static void RealMain()
        {
            Console.WriteLine("Select difficulty: ");
            var from = CM.Ask<int>("From: ", false);
            var to = CM.Ask<int>("To: ", false);
            var rnd = new Random();
            int ans = -1;
            while (rnd.Next(from, to) != ans)
                ans = CM.Ask<int>($"Guess a number ({from}-{to}): ", false);
            Console.WriteLine("Correct!");
        }

        /// <summary>
        /// Using no custom methods
        /// </summary>
        private static void RealMain2()
        {
            Console.WriteLine("Select difficulty: ");
            Console.Write("From: ");
            var from = int.Parse(Console.ReadLine() ?? "");
            Console.Write("To: ");
            var to = int.Parse(Console.ReadLine() ?? "");
            var rnd = new Random();
            int ans = -1;
            while (rnd.Next(from, to) != ans)
            {
                Console.Write($"Guess a number ({from}-{to}): ");
                _ = int.TryParse(Console.ReadLine(), out ans);
            }
            Console.WriteLine("Correct!");
        }
    }
}
/*
namespace BasicProgrammingDag1
{
    static class CM
    {
        /// <summary>
        /// Ask for a string, depending on question
        /// </summary>
        /// <param name="qestion"></param>
        /// <param name="newline"></param>
        /// <param name="qu_color"></param>
        /// <param name="ans_color"></param>
        /// <returns></returns>
        public static string? Ask(string qestion, bool newline, ConsoleColor quColor = ConsoleColor.DarkBlue, ConsoleColor ansColor = ConsoleColor.Green)
        {
            var std_color = Console.ForegroundColor;
            Console.ForegroundColor = quColor;
            if (newline) Console.WriteLine(">> " + qestion);
            else Console.Write(">> " + qestion);
            Console.ForegroundColor = ansColor;
            var ans = Console.ReadLine();
            Console.WriteLine();
            Console.ForegroundColor = std_color;
            return ans;
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
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            RealMain();
        }

        private static void RealMain()
        {
            switch (CM.AskKey("Are you gaming (y/n)? ", false, true))
            {
                //Handle 'yes'
                case ConsoleKey.J:
                case ConsoleKey.Y:
                    HandleYes();
                    break;

                //Handle 'no'
                case ConsoleKey.N:
                    HandleNo();
                    break;

                //Handle 'fallback'
                default:
                    Fallback();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void HandleYes()
        {
            string? ans;
            while ((ans = CM.Ask("Gamertag? ", false)) == null || ans?.Length == 0)
                Console.WriteLine("Did not get that, try again: ");
            Console.WriteLine("Hello! " + ans);
        }

        /// <summary>
        /// 
        /// </summary>
        private static void HandleNo()
        {
            Console.WriteLine("Ohh, bye bye!");
        }

        /// <summary>
        /// 
        /// </summary>
        private static void Fallback()
        {
            Console.WriteLine("Ohh, bye bye!");
        }
    }
}
*/
