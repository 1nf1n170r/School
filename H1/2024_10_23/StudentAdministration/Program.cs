using Lib;

namespace StudentAdministration
{
    /*
    public static class Solution
    {
        private static Dictionary<string, List<int>> students = new();
        private static List<string> grade_types = ["Math", "Danish", "Physics", "English", "CS",];
        private static float passing_grade = 7.0f;
        static void New_Student_menuitem()
        {
            var name = Input.Ask<string>("Name: ", false);
            if (name != null && (!students.ContainsKey(name) || ConsoleKey.Y == Input.AskKey("Student already exists... Override (y/n)? ", false, false)))
            {
                students[name] = [];
                foreach (var type in grade_types)
                    students[name].Add(Input.Ask<int>($"{type}: ", false));
            }
        }
        static void View_Students_menuitem()
        {
            foreach (var student in students)
            {
                var sum_of_grades = student.Value.Sum(x => x) / student.Value.Count;
                Console.WriteLine($"{student.Key} : {sum_of_grades} - Bestået: {sum_of_grades >= passing_grade}");
                for (var i = 0; i < grade_types.Count; i++)
                    Console.WriteLine($"\t{grade_types[i]}: {student.Value[i]}");
            }
        }
        public static void Start()
        {
            do
            {
                var item = Input.GenerateMenu(typeof(Solution), x => x.Name.EndsWith("_menuitem") && x.IsStatic && x.IsPrivate, x => x.Replace("_menuitem", "").Replace("_", " "));
                Console.Clear();
                item?.Invoke(null, null);

            } while (Input.AskKey("Execute more (y/n)? ", false, false) == ConsoleKey.Y);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            RealMain();
        }
        static void RealMain()
        {
            Solution.Start();
        }
    }
    */
    namespace StudentAdministration
    {
        public class Game : IDisposable
        {
            private string Name { get; set; }
            public Game(string name)
            {
                this.Name = name;
                Console.WriteLine("Constructed: " + Name);
            }
            public void Dispose()
            {
                Console.WriteLine("Destructed: " + Name);
            }
        }

        public class Program
        {
            public static void Main()
            {
                Game game = new("Game1");

                

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
