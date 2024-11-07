using System.Text.RegularExpressions;

namespace Program
{
    internal class Program
    {
        static void Main()
        {
            var text = ""; // Dont want to copy everything in, since it would be hardcoding, which is bad
            System.Console.WriteLine(WordCount.CountInstances(text, new Regex(@"\btype")));
        }
    }
    internal static class WordCount
    {
        public static uint CountInstances(string text, Regex regex)
        {
            return (uint)regex.Count(text);
        }
    }
}
